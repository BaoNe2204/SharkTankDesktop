/*
  Nhật ký hành động người dùng (Audit Log)
  Ghi lại mọi thao tác CRUD của user trên hệ thống.
  Dùng cho "Lịch sử thao tác" trong Admin.
*/

SET NOCOUNT ON;

IF OBJECT_ID('dbo.AuditLogs', 'U') IS NOT NULL DROP TABLE dbo.AuditLogs;

CREATE TABLE dbo.AuditLogs
(
    AuditLogId   INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_AuditLogs PRIMARY KEY,
    UserId       INT            NULL,
    Username     NVARCHAR(100)  NULL,
    FullName     NVARCHAR(200)  NULL,
    Action       NVARCHAR(50)   NOT NULL,   -- 'CREATE' | 'UPDATE' | 'DELETE' | 'LOGIN' | 'LOGOUT' | 'EXPORT' | 'IMPORT'
    EntityType   NVARCHAR(100) NOT NULL,   -- 'Users' | 'Employees' | 'Departments' | ...
    EntityId     NVARCHAR(100) NULL,
    EntityName   NVARCHAR(255) NULL,
    Description  NVARCHAR(MAX)  NULL,
    IpAddress    NVARCHAR(64)   NULL,
    DeviceInfo   NVARCHAR(255)  NULL,
    OldValues    NVARCHAR(MAX)   NULL,
    NewValues    NVARCHAR(MAX)   NULL,
    Timestamp    DATETIME       NOT NULL CONSTRAINT DF_AuditLogs_Timestamp DEFAULT(GETDATE())
);

CREATE NONCLUSTERED INDEX IX_AuditLogs_UserId    ON dbo.AuditLogs(UserId);
CREATE NONCLUSTERED INDEX IX_AuditLogs_Timestamp ON dbo.AuditLogs(Timestamp DESC);
CREATE NONCLUSTERED INDEX IX_AuditLogs_EntityType ON dbo.AuditLogs(EntityType);
CREATE NONCLUSTERED INDEX IX_AuditLogs_Action    ON dbo.AuditLogs(Action);

PRINT 'dbo.AuditLogs created successfully.';
