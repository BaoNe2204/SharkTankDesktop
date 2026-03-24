/*
  Lịch sử đăng nhập / đăng xuất
  Bảng này ghi lại mỗi lần user đăng nhập hoặc đăng xuất.
  Phiên đăng nhập mới được tạo trong SessionService khi login thành công,
  và LogoutTime được cập nhật khi user đăng xuất hoặc bị ép logout.
*/

SET NOCOUNT ON;

IF OBJECT_ID('dbo.LoginHistory', 'U') IS NOT NULL DROP TABLE dbo.LoginHistory;

CREATE TABLE dbo.LoginHistory
(
    LoginHistoryId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_LoginHistory PRIMARY KEY,
    UserId        INT          NOT NULL,
    Username      NVARCHAR(100) NULL,
    FullName      NVARCHAR(200) NULL,
    RoleName      NVARCHAR(100) NULL,
    LoginTime     DATETIME     NOT NULL,
    LogoutTime    DATETIME     NULL,
    IpAddress     NVARCHAR(64)  NULL,
    DeviceInfo    NVARCHAR(255) NULL,
    Status        NVARCHAR(50)  NOT NULL,  -- 'Success' | 'Failed' | 'Locked' | 'Expired'
    FailureReason NVARCHAR(255) NULL,
    CreatedAt     DATETIME     NOT NULL CONSTRAINT DF_LoginHistory_Created DEFAULT(GETDATE())
);

CREATE NONCLUSTERED INDEX IX_LoginHistory_UserId ON dbo.LoginHistory(UserId);
CREATE NONCLUSTERED INDEX IX_LoginHistory_LoginTime ON dbo.LoginHistory(LoginTime DESC);
CREATE NONCLUSTERED INDEX IX_LoginHistory_Username ON dbo.LoginHistory(Username);

PRINT 'dbo.LoginHistory created successfully.';
