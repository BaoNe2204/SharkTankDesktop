/*
  Theo dõi thay đổi dữ liệu chi tiết (Data Change Log)
  Lưu snapshot giá trị cũ / mới khi có bất kỳ thay đổi nào trên bảng chính.
  Dùng cho "Theo dõi thay đổi dữ liệu" trong Admin.
*/

SET NOCOUNT ON;

IF OBJECT_ID('dbo.DataChangeLogs', 'U') IS NOT NULL DROP TABLE dbo.DataChangeLogs;

CREATE TABLE dbo.DataChangeLogs
(
    ChangeLogId   INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_DataChangeLogs PRIMARY KEY,
    AuditLogId    INT            NULL,
    TableName     NVARCHAR(128)  NOT NULL,
    RecordId      NVARCHAR(100)  NOT NULL,
    FieldName     NVARCHAR(128)  NOT NULL,
    OldValue      NVARCHAR(MAX)  NULL,
    NewValue      NVARCHAR(MAX)  NULL,
    ChangeType    NVARCHAR(20)   NOT NULL,  -- 'INSERT' | 'UPDATE' | 'DELETE'
    ChangeTime    DATETIME       NOT NULL CONSTRAINT DF_DataChangeLogs_ChangeTime DEFAULT(GETDATE())
);

CREATE NONCLUSTERED INDEX IX_DataChangeLogs_TableName  ON dbo.DataChangeLogs(TableName);
CREATE NONCLUSTERED INDEX IX_DataChangeLogs_RecordId   ON dbo.DataChangeLogs(RecordId);
CREATE NONCLUSTERED INDEX IX_DataChangeLogs_ChangeTime ON dbo.DataChangeLogs(ChangeTime DESC);
CREATE NONCLUSTERED INDEX IX_DataChangeLogs_AuditLogId ON dbo.DataChangeLogs(AuditLogId);

PRINT 'dbo.DataChangeLogs created successfully.';
