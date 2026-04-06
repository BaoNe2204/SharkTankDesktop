/*
=============================================================
  SharkTank ERP - FULL SCHEMA (SQL Server)
  Chạy theo thứ tự từ trên xuống dưới trên database SharkTankERP
=============================================================
*/

SET NOCOUNT ON;

IF DB_ID(N'SharkTankERP') IS NULL
BEGIN
    CREATE DATABASE [SharkTankERP];
END
GO

ALTER DATABASE [SharkTankERP] SET RECOVERY SIMPLE;
GO

SELECT 'Database ready: SharkTankERP' AS Status;
GO

USE [SharkTankERP];
SET NOCOUNT ON;
GO

-- ============================================================
-- 1. CORE: Roles, Users, Permissions, RolePermissions, LoginSessions
-- ============================================================

IF OBJECT_ID('dbo.RolePermissions','U') IS NOT NULL DROP TABLE dbo.RolePermissions;
IF OBJECT_ID('dbo.UserPermissions','U') IS NOT NULL DROP TABLE dbo.UserPermissions;
IF OBJECT_ID('dbo.LoginSessions','U')   IS NOT NULL DROP TABLE dbo.LoginSessions;
IF OBJECT_ID('dbo.Permissions','U')     IS NOT NULL DROP TABLE dbo.Permissions;
IF OBJECT_ID('dbo.Users','U')           IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Roles','U')           IS NOT NULL DROP TABLE dbo.Roles;
GO

CREATE TABLE dbo.Roles (
    RoleId      INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_Roles PRIMARY KEY,
    RoleName    NVARCHAR(100)  NOT NULL CONSTRAINT UQ_Roles_RoleName UNIQUE,
    [Description] NVARCHAR(255) NULL,
    IsSystemRole BIT           NOT NULL CONSTRAINT DF_Roles_IsSystemRole DEFAULT(1)
);
GO

CREATE TABLE dbo.Users (
    UserId               INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
    Username             NVARCHAR(100)  NOT NULL CONSTRAINT UQ_Users_Username UNIQUE,
    PasswordHash         NVARCHAR(128)  NOT NULL,
    PasswordSalt         NVARCHAR(64)   NOT NULL,
    FullName             NVARCHAR(200)  NULL,
    Email                NVARCHAR(200)  NULL,
    Phone                NVARCHAR(50)   NULL,
    Department           NVARCHAR(100)  NULL,
    RoleId               INT            NOT NULL,
    IsActive             BIT            NOT NULL CONSTRAINT DF_Users_IsActive  DEFAULT(1),
    IsLocked             BIT            NOT NULL CONSTRAINT DF_Users_IsLocked  DEFAULT(0),
    FailedLoginAttempts  INT            NOT NULL CONSTRAINT DF_Users_Failed    DEFAULT(0),
    LastLoginAt          DATETIME       NULL,
    LastPasswordResetAt  DATETIME       NULL,
    CreatedAt            DATETIME       NOT NULL CONSTRAINT DF_Users_Created   DEFAULT(GETDATE()),
    UpdatedAt            DATETIME       NOT NULL CONSTRAINT DF_Users_Updated   DEFAULT(GETDATE()),
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles(RoleId)
);
GO

CREATE TABLE dbo.Permissions (
    PermissionId   INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_Permissions PRIMARY KEY,
    PermissionCode NVARCHAR(100)  NOT NULL CONSTRAINT UQ_Permissions_Code UNIQUE,
    PermissionName NVARCHAR(200)  NULL,
    [Module]       NVARCHAR(100)  NULL
);
GO

CREATE TABLE dbo.RolePermissions (
    RoleId       INT NOT NULL,
    PermissionId INT NOT NULL,
    CONSTRAINT PK_RolePermissions PRIMARY KEY (RoleId, PermissionId),
    CONSTRAINT FK_RolePermissions_Roles       FOREIGN KEY (RoleId)       REFERENCES dbo.Roles(RoleId),
    CONSTRAINT FK_RolePermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES dbo.Permissions(PermissionId)
);
GO

-- Per-user permission overrides
CREATE TABLE dbo.UserPermissions (
    UserId       INT NOT NULL,
    PermissionId INT NOT NULL,
    CONSTRAINT PK_UserPermissions PRIMARY KEY (UserId, PermissionId),
    CONSTRAINT FK_UserPermissions_Users       FOREIGN KEY (UserId)       REFERENCES dbo.Users(UserId),
    CONSTRAINT FK_UserPermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES dbo.Permissions(PermissionId)
);
GO

CREATE TABLE dbo.LoginSessions (
    SessionId  UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_LoginSessions PRIMARY KEY,
    UserId     INT              NOT NULL,
    LoginTime  DATETIME         NOT NULL,
    LogoutTime DATETIME         NULL,
    IpAddress  NVARCHAR(64)     NULL,
    DeviceInfo NVARCHAR(255)    NULL,
    IsActive   BIT              NOT NULL,
    CONSTRAINT FK_LoginSessions_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId)
);
GO

-- ============================================================
-- 2. AUDIT: LoginHistory, AuditLogs, DataChangeLogs
-- ============================================================

IF OBJECT_ID('dbo.DataChangeLogs','U') IS NOT NULL DROP TABLE dbo.DataChangeLogs;
IF OBJECT_ID('dbo.AuditLogs','U')      IS NOT NULL DROP TABLE dbo.AuditLogs;
IF OBJECT_ID('dbo.LoginHistory','U')   IS NOT NULL DROP TABLE dbo.LoginHistory;
GO

CREATE TABLE dbo.LoginHistory (
    LoginHistoryId INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_LoginHistory PRIMARY KEY,
    UserId         INT            NOT NULL,
    Username       NVARCHAR(100)  NULL,
    FullName       NVARCHAR(200)  NULL,
    RoleName       NVARCHAR(100)  NULL,
    LoginTime      DATETIME       NOT NULL,
    LogoutTime     DATETIME       NULL,
    IpAddress      NVARCHAR(64)   NULL,
    DeviceInfo     NVARCHAR(255)  NULL,
    Status         NVARCHAR(50)   NOT NULL,  -- 'Success'|'Failed'|'Locked'|'Expired'
    FailureReason  NVARCHAR(255)  NULL,
    CreatedAt      DATETIME       NOT NULL CONSTRAINT DF_LoginHistory_Created DEFAULT(GETDATE())
);
CREATE NONCLUSTERED INDEX IX_LoginHistory_UserId    ON dbo.LoginHistory(UserId);
CREATE NONCLUSTERED INDEX IX_LoginHistory_LoginTime ON dbo.LoginHistory(LoginTime DESC);
CREATE NONCLUSTERED INDEX IX_LoginHistory_Username  ON dbo.LoginHistory(Username);
GO

CREATE TABLE dbo.AuditLogs (
    AuditLogId  INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_AuditLogs PRIMARY KEY,
    UserId      INT            NULL,
    Username    NVARCHAR(100)  NULL,
    FullName    NVARCHAR(200)  NULL,
    Action      NVARCHAR(50)   NOT NULL,   -- 'CREATE'|'UPDATE'|'DELETE'|'LOGIN'|'LOGOUT'|'EXPORT'|'IMPORT'
    EntityType  NVARCHAR(100)  NOT NULL,
    EntityId    NVARCHAR(100)  NULL,
    EntityName  NVARCHAR(255)  NULL,
    Description NVARCHAR(MAX)  NULL,
    IpAddress   NVARCHAR(64)   NULL,
    DeviceInfo  NVARCHAR(255)  NULL,
    OldValues   NVARCHAR(MAX)  NULL,
    NewValues   NVARCHAR(MAX)  NULL,
    Timestamp   DATETIME       NOT NULL CONSTRAINT DF_AuditLogs_Timestamp DEFAULT(GETDATE())
);
CREATE NONCLUSTERED INDEX IX_AuditLogs_UserId     ON dbo.AuditLogs(UserId);
CREATE NONCLUSTERED INDEX IX_AuditLogs_Timestamp  ON dbo.AuditLogs(Timestamp DESC);
CREATE NONCLUSTERED INDEX IX_AuditLogs_EntityType ON dbo.AuditLogs(EntityType);
CREATE NONCLUSTERED INDEX IX_AuditLogs_Action     ON dbo.AuditLogs(Action);
GO

CREATE TABLE dbo.DataChangeLogs (
    ChangeLogId INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_DataChangeLogs PRIMARY KEY,
    AuditLogId  INT            NULL,
    TableName   NVARCHAR(128)  NOT NULL,
    RecordId    NVARCHAR(100)  NOT NULL,
    FieldName   NVARCHAR(128)  NOT NULL,
    OldValue    NVARCHAR(MAX)  NULL,
    NewValue    NVARCHAR(MAX)  NULL,
    ChangeType  NVARCHAR(20)   NOT NULL,  -- 'INSERT'|'UPDATE'|'DELETE'
    ChangeTime  DATETIME       NOT NULL CONSTRAINT DF_DataChangeLogs_ChangeTime DEFAULT(GETDATE())
);
CREATE NONCLUSTERED INDEX IX_DataChangeLogs_TableName  ON dbo.DataChangeLogs(TableName);
CREATE NONCLUSTERED INDEX IX_DataChangeLogs_RecordId   ON dbo.DataChangeLogs(RecordId);
CREATE NONCLUSTERED INDEX IX_DataChangeLogs_ChangeTime ON dbo.DataChangeLogs(ChangeTime DESC);
CREATE NONCLUSTERED INDEX IX_DataChangeLogs_AuditLogId ON dbo.DataChangeLogs(AuditLogId);
GO

-- ============================================================
-- 3. CONFIG: Companies, SystemConfigs, SystemNotifications
-- ============================================================

IF OBJECT_ID('dbo.SystemNotifications','U') IS NOT NULL DROP TABLE dbo.SystemNotifications;
IF OBJECT_ID('dbo.SystemConfigs','U')        IS NOT NULL DROP TABLE dbo.SystemConfigs;
IF OBJECT_ID('dbo.Companies','U')            IS NOT NULL DROP TABLE dbo.Companies;
GO

CREATE TABLE dbo.Companies (
    Id                     INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_Companies PRIMARY KEY,
    CompanyName            NVARCHAR(200)  NOT NULL,
    Address                NVARCHAR(500)  NULL,
    TaxCode                NVARCHAR(50)   NULL,
    Phone                  NVARCHAR(20)   NULL,
    Email                  NVARCHAR(100)  NULL,
    Website                NVARCHAR(200)  NULL,
    BusinessLicense        NVARCHAR(100)  NULL,
    LogoPath               NVARCHAR(500)  NULL,
    RepresentativeName     NVARCHAR(100)  NULL,
    RepresentativePosition NVARCHAR(100)  NULL,
    Hotline                NVARCHAR(20)   NULL,
    IsActive               BIT            NOT NULL CONSTRAINT DF_Companies_IsActive DEFAULT(1),
    CreatedAt              DATETIME       NOT NULL CONSTRAINT DF_Companies_Created  DEFAULT(GETDATE()),
    UpdatedAt              DATETIME       NOT NULL CONSTRAINT DF_Companies_Updated  DEFAULT(GETDATE())
);
GO

CREATE TABLE dbo.SystemConfigs (
    Id          INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_SystemConfigs PRIMARY KEY,
    ConfigKey   NVARCHAR(100)  NOT NULL CONSTRAINT UQ_SystemConfigs_Key UNIQUE,
    ConfigValue NVARCHAR(MAX)  NULL,
    ConfigGroup NVARCHAR(50)   NULL,
    Description NVARCHAR(255)  NULL,
    DataType    NVARCHAR(20)   NOT NULL CONSTRAINT DF_SystemConfigs_DataType DEFAULT('string'),
    IsActive    BIT            NOT NULL CONSTRAINT DF_SystemConfigs_IsActive DEFAULT(1),
    CreatedAt   DATETIME       NOT NULL CONSTRAINT DF_SystemConfigs_Created  DEFAULT(GETDATE()),
    UpdatedAt   DATETIME       NOT NULL CONSTRAINT DF_SystemConfigs_Updated  DEFAULT(GETDATE())
);
GO

CREATE TABLE dbo.SystemNotifications (
    NotificationId INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_SystemNotifications PRIMARY KEY,
    Title          NVARCHAR(255)  NULL,
    Content        NVARCHAR(MAX)  NULL,
    Type           NVARCHAR(50)   NOT NULL CONSTRAINT DF_SysNotif_Type    DEFAULT('Info'),  -- 'Info'|'Warning'|'Error'
    TargetType     NVARCHAR(20)   NOT NULL CONSTRAINT DF_SysNotif_Target  DEFAULT('ALL'),   -- 'ALL'|'ROLE'|'USER'
    TargetValue    NVARCHAR(100)  NULL,
    StartAt        DATETIME       NOT NULL CONSTRAINT DF_SysNotif_StartAt DEFAULT(GETDATE()),
    EndAt          DATETIME       NULL,
    IsActive       BIT            NOT NULL CONSTRAINT DF_SysNotif_IsActive DEFAULT(1),
    CreatedBy      INT            NULL,
    CreatedAt      DATETIME       NOT NULL CONSTRAINT DF_SysNotif_CreatedAt DEFAULT(GETDATE())
);
GO

-- ============================================================
-- 4. HR MODULE
-- ============================================================

IF OBJECT_ID('dbo.HopDongLaoDong','U')  IS NOT NULL DROP TABLE dbo.HopDongLaoDong;
IF OBJECT_ID('dbo.GiayToNhanVien','U')  IS NOT NULL DROP TABLE dbo.GiayToNhanVien;
IF OBJECT_ID('dbo.NhanVien','U')        IS NOT NULL DROP TABLE dbo.NhanVien;
IF OBJECT_ID('dbo.ChucVu','U')          IS NOT NULL DROP TABLE dbo.ChucVu;
IF OBJECT_ID('dbo.PhongBan','U')        IS NOT NULL DROP TABLE dbo.PhongBan;
GO

CREATE TABLE dbo.PhongBan (
    PhongBanId   INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhongBan PRIMARY KEY,
    TenPhongBan  NVARCHAR(150)  NOT NULL,
    MoTa         NVARCHAR(500)  NULL,
    IsActive     BIT            NOT NULL CONSTRAINT DF_PhongBan_IsActive DEFAULT(1)
);
GO

CREATE TABLE dbo.ChucVu (
    ChucVuId   INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_ChucVu PRIMARY KEY,
    TenChucVu  NVARCHAR(150)  NOT NULL,
    MoTa       NVARCHAR(500)  NULL,
    IsActive   BIT            NOT NULL CONSTRAINT DF_ChucVu_IsActive DEFAULT(1)
);
GO

CREATE TABLE dbo.NhanVien (
    NhanVienId   NVARCHAR(20)  NOT NULL CONSTRAINT PK_NhanVien PRIMARY KEY,
    HoTen        NVARCHAR(200) NOT NULL,
    NgaySinh     DATE          NULL,
    GioiTinh     NVARCHAR(10)  NULL,
    DiaChi       NVARCHAR(500) NULL,
    Email        NVARCHAR(200) NULL,
    SoDienThoai  NVARCHAR(20)  NULL,
    AnhDaiDien   NVARCHAR(500) NULL,
    PhongBanId   INT           NULL,
    ChucVuId     INT           NULL,
    NgayVaoLam   DATE          NULL,
    TrangThai    NVARCHAR(50)  NOT NULL CONSTRAINT DF_NhanVien_TrangThai DEFAULT(N'Đang làm việc'),
    GhiChu       NVARCHAR(MAX) NULL,
    CONSTRAINT FK_NhanVien_PhongBan FOREIGN KEY (PhongBanId) REFERENCES dbo.PhongBan(PhongBanId),
    CONSTRAINT FK_NhanVien_ChucVu   FOREIGN KEY (ChucVuId)   REFERENCES dbo.ChucVu(ChucVuId)
);
CREATE NONCLUSTERED INDEX IX_NhanVien_PhongBan ON dbo.NhanVien(PhongBanId);
GO

CREATE TABLE dbo.GiayToNhanVien (
    Id          INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_GiayToNhanVien PRIMARY KEY,
    NhanVienId  NVARCHAR(20)   NOT NULL,
    LoaiGiayTo  NVARCHAR(50)   NOT NULL,  -- 'CCCD'|'Hộ chiếu'
    SoGiayTo    NVARCHAR(50)   NULL,
    NgayCap     DATE           NULL,
    NoiCap      NVARCHAR(200)  NULL,
    NgayHetHan  DATE           NULL,
    CONSTRAINT FK_GiayTo_NhanVien FOREIGN KEY (NhanVienId) REFERENCES dbo.NhanVien(NhanVienId)
);
GO

CREATE TABLE dbo.HopDongLaoDong (
    HopDongId        INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_HopDongLaoDong PRIMARY KEY,
    SoHopDong        NVARCHAR(50)   NOT NULL,
    NhanVienId       NVARCHAR(20)   NOT NULL,
    LoaiHopDong      NVARCHAR(100)  NULL,  -- 'Xác định thời hạn'|'Không xác định thời hạn'|'Thử việc'|'Theo mùa vụ'
    NgayBatDau       DATE           NULL,
    NgayKetThuc      DATE           NULL,
    LuongCoBan       DECIMAL(18,0)  NULL,
    PhuCap           DECIMAL(18,0)  NULL,
    HinhThucTraLuong NVARCHAR(50)   NULL,  -- 'Theo tháng'|'Theo tuần'|'Theo ngày'|'Theo giờ'
    TrangThai        NVARCHAR(50)   NOT NULL CONSTRAINT DF_HopDong_TrangThai DEFAULT(N'Hiệu lực'),
    CONSTRAINT FK_HopDong_NhanVien FOREIGN KEY (NhanVienId) REFERENCES dbo.NhanVien(NhanVienId)
);
CREATE NONCLUSTERED INDEX IX_HopDong_NhanVien ON dbo.HopDongLaoDong(NhanVienId);
GO

-- ============================================================
-- 5. SALES MODULE
-- ============================================================

IF OBJECT_ID('dbo.HoaDon','U')    IS NOT NULL DROP TABLE dbo.HoaDon;
IF OBJECT_ID('dbo.KhachHang','U') IS NOT NULL DROP TABLE dbo.KhachHang;
GO

CREATE TABLE dbo.KhachHang (
    MaKH      NVARCHAR(20)  NOT NULL CONSTRAINT PK_KhachHang PRIMARY KEY,
    HoTen     NVARCHAR(200) NOT NULL,
    DienThoai NVARCHAR(20)  NULL,
    DiaChi    NVARCHAR(500) NULL,
    Email     NVARCHAR(200) NULL,
    PhanLoai  NVARCHAR(100) NULL,
    GhiChu    NVARCHAR(MAX) NULL,
    IsActive  BIT           NOT NULL CONSTRAINT DF_KhachHang_IsActive DEFAULT(1),
    CreatedAt DATETIME      NOT NULL CONSTRAINT DF_KhachHang_Created  DEFAULT(GETDATE())
);
GO

CREATE TABLE dbo.HoaDon (
    MaHD      NVARCHAR(20)   NOT NULL CONSTRAINT PK_HoaDon PRIMARY KEY,
    MaKH      NVARCHAR(20)   NOT NULL,
    NgayLap   DATETIME       NOT NULL CONSTRAINT DF_HoaDon_NgayLap DEFAULT(GETDATE()),
    TongTien  DECIMAL(18,0)  NOT NULL CONSTRAINT DF_HoaDon_TongTien DEFAULT(0),
    GhiChu    NVARCHAR(MAX)  NULL,
    TrangThai NVARCHAR(50)   NOT NULL CONSTRAINT DF_HoaDon_TrangThai DEFAULT(N'Mới'),
    CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH)
);
CREATE NONCLUSTERED INDEX IX_HoaDon_MaKH    ON dbo.HoaDon(MaKH);
CREATE NONCLUSTERED INDEX IX_HoaDon_NgayLap ON dbo.HoaDon(NgayLap DESC);
GO

-- ============================================================
-- 6. INVENTORY MODULE
-- ============================================================

IF OBJECT_ID('dbo.XuatKho','U') IS NOT NULL DROP TABLE dbo.XuatKho;
IF OBJECT_ID('dbo.NhapKho','U') IS NOT NULL DROP TABLE dbo.NhapKho;
IF OBJECT_ID('dbo.SanPham','U') IS NOT NULL DROP TABLE dbo.SanPham;
GO

CREATE TABLE dbo.SanPham (
    MaSP      NVARCHAR(20)  NOT NULL CONSTRAINT PK_SanPham PRIMARY KEY,
    TenSP     NVARCHAR(200) NULL,
    NhomHang  NVARCHAR(100) NULL,
    DonViTinh NVARCHAR(30)  NULL,
    GiaNhap   DECIMAL(18,2) NOT NULL CONSTRAINT DF_SanPham_GiaNhap DEFAULT(0),
    GiaBan    DECIMAL(18,2) NOT NULL CONSTRAINT DF_SanPham_GiaBan  DEFAULT(0),
    IsActive  BIT           NOT NULL CONSTRAINT DF_SanPham_IsActive DEFAULT(1)
);
GO

CREATE TABLE dbo.NhapKho (
    Id           INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhapKho PRIMARY KEY,
    PhieuNhap    NVARCHAR(20)   NOT NULL,
    MaKho        NVARCHAR(20)   NOT NULL,
    MaSP         NVARCHAR(20)   NOT NULL,
    NhaCungCap   NVARCHAR(200)  NULL,
    SoLuong      INT            NOT NULL CONSTRAINT DF_NhapKho_SoLuong DEFAULT(0),
    GiaNhap      DECIMAL(18,2)  NOT NULL CONSTRAINT DF_NhapKho_GiaNhap DEFAULT(0),
    NgayNhap     DATETIME       NOT NULL CONSTRAINT DF_NhapKho_NgayNhap DEFAULT(GETDATE()),
    CONSTRAINT FK_NhapKho_SanPham FOREIGN KEY (MaSP) REFERENCES dbo.SanPham(MaSP)
);
CREATE NONCLUSTERED INDEX IX_NhapKho_MaSP ON dbo.NhapKho(MaSP);
GO

CREATE TABLE dbo.XuatKho (
    Id        INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_XuatKho PRIMARY KEY,
    PhieuXuat NVARCHAR(20)   NOT NULL,
    MaKho     NVARCHAR(20)   NOT NULL,
    MaSP      NVARCHAR(20)   NOT NULL,
    SoLuong   INT            NOT NULL CONSTRAINT DF_XuatKho_SoLuong DEFAULT(0),
    GiaXuat   DECIMAL(18,2)  NOT NULL CONSTRAINT DF_XuatKho_GiaXuat DEFAULT(0),
    NgayXuat  DATETIME       NOT NULL CONSTRAINT DF_XuatKho_NgayXuat DEFAULT(GETDATE()),
    CONSTRAINT FK_XuatKho_SanPham FOREIGN KEY (MaSP) REFERENCES dbo.SanPham(MaSP)
);
CREATE NONCLUSTERED INDEX IX_XuatKho_MaSP ON dbo.XuatKho(MaSP);
GO

-- ============================================================
-- 7. CRM MODULE
-- ============================================================

IF OBJECT_ID('dbo.ChamSocKhachHang','U') IS NOT NULL DROP TABLE dbo.ChamSocKhachHang;
IF OBJECT_ID('dbo.Leads','U')            IS NOT NULL DROP TABLE dbo.Leads;
GO

CREATE TABLE dbo.Leads (
    LeadID      INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_Leads PRIMARY KEY,
    Ten         NVARCHAR(200)  NOT NULL,
    SoDienThoai NVARCHAR(20)   NULL,
    Email       NVARCHAR(200)  NULL,
    Nguon       NVARCHAR(100)  NULL,
    TrangThai   NVARCHAR(50)   NOT NULL CONSTRAINT DF_Leads_TrangThai DEFAULT(N'Mới'),
    CreatedAt   DATETIME       NOT NULL CONSTRAINT DF_Leads_CreatedAt DEFAULT(GETDATE())
);
GO

CREATE TABLE dbo.ChamSocKhachHang (
    Id        INT            IDENTITY(1,1) NOT NULL CONSTRAINT PK_ChamSoc PRIMARY KEY,
    KhachHang NVARCHAR(200)  NOT NULL,
    Ngay      DATETIME       NOT NULL CONSTRAINT DF_ChamSoc_Ngay DEFAULT(GETDATE()),
    Loai      NVARCHAR(100)  NULL,
    NoiDung   NVARCHAR(MAX)  NULL
);
GO

-- ============================================================
-- 8. SEED DATA
-- ============================================================

-- Roles
INSERT INTO dbo.Roles (RoleName, [Description], IsSystemRole) VALUES
 (N'Admin',      N'System Administrator',               1),
 (N'HR',         N'Human Resources',                    1),
 (N'Sales',      N'Sales',                              1),
 (N'Inventory',  N'Inventory/Warehouse',                1),
 (N'Accounting', N'Accounting',                         1),
 (N'CRM',        N'Customer Relationship Management', 1);
GO

-- Permissions
INSERT INTO dbo.Permissions (PermissionCode, PermissionName, [Module]) VALUES
 (N'HR.VIEW',          N'HR - View',          N'HR'),
 (N'HR.CREATE',        N'HR - Create',        N'HR'),
 (N'HR.UPDATE',        N'HR - Update',        N'HR'),
 (N'HR.DELETE',        N'HR - Delete',        N'HR'),
 (N'SALES.VIEW',       N'Sales - View',       N'Sales'),
 (N'SALES.CREATE',     N'Sales - Create',     N'Sales'),
 (N'SALES.UPDATE',     N'Sales - Update',     N'Sales'),
 (N'INVENTORY.VIEW',   N'Inventory - View',   N'Inventory'),
 (N'INVENTORY.CREATE', N'Inventory - Create', N'Inventory'),
 (N'INVENTORY.UPDATE', N'Inventory - Update', N'Inventory'),
 (N'ACCOUNTING.VIEW',  N'Accounting - View',  N'Accounting'),
 (N'ACCOUNTING.CREATE',N'Accounting - Create',N'Accounting'),
 (N'ACCOUNTING.UPDATE',N'Accounting - Update',N'Accounting'),
 (N'ADMIN.VIEW',       N'Admin - View',       N'Admin'),
 (N'CRM.VIEW',         N'CRM - View',         N'CRM'),
 (N'CRM.CREATE',       N'CRM - Create',       N'CRM'),
 (N'CRM.UPDATE',       N'CRM - Update',       N'CRM'),
 (N'CRM.DELETE',       N'CRM - Delete',       N'CRM');
GO

DECLARE @RoleAdmin      INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'Admin');
DECLARE @RoleHR         INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'HR');
DECLARE @RoleSales      INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'Sales');
DECLARE @RoleInventory  INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'Inventory');
DECLARE @RoleAccounting INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'Accounting');
DECLARE @RoleCRM        INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'CRM');

INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleAdmin, PermissionId FROM dbo.Permissions;

INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleHR, PermissionId FROM dbo.Permissions WHERE PermissionCode LIKE N'HR.%';

INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleSales, PermissionId FROM dbo.Permissions WHERE PermissionCode IN (N'SALES.VIEW',N'SALES.CREATE',N'SALES.UPDATE');

INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleInventory, PermissionId FROM dbo.Permissions WHERE PermissionCode IN (N'INVENTORY.VIEW',N'INVENTORY.CREATE',N'INVENTORY.UPDATE');

INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleAccounting, PermissionId FROM dbo.Permissions WHERE PermissionCode IN (N'ACCOUNTING.VIEW',N'ACCOUNTING.CREATE',N'ACCOUNTING.UPDATE');

INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleCRM, PermissionId FROM dbo.Permissions WHERE PermissionCode LIKE N'CRM.%';
GO

-- Users (password = SHA2_256(plaintext + salt), salt = random GUID hex)
DECLARE @now DATETIME = GETDATE();
DECLARE @s1 NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32),CONVERT(BINARY(16),NEWID()),2));
DECLARE @s2 NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32),CONVERT(BINARY(16),NEWID()),2));
DECLARE @s3 NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32),CONVERT(BINARY(16),NEWID()),2));
DECLARE @s4 NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32),CONVERT(BINARY(16),NEWID()),2));
DECLARE @s5 NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32),CONVERT(BINARY(16),NEWID()),2));
DECLARE @s6 NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32),CONVERT(BINARY(16),NEWID()),2));

DECLARE @rAdmin INT=(SELECT RoleId FROM dbo.Roles WHERE RoleName=N'Admin');
DECLARE @rHR    INT=(SELECT RoleId FROM dbo.Roles WHERE RoleName=N'HR');
DECLARE @rSales INT=(SELECT RoleId FROM dbo.Roles WHERE RoleName=N'Sales');
DECLARE @rInv   INT=(SELECT RoleId FROM dbo.Roles WHERE RoleName=N'Inventory');
DECLARE @rAcc   INT=(SELECT RoleId FROM dbo.Roles WHERE RoleName=N'Accounting');
DECLARE @rCRM   INT=(SELECT RoleId FROM dbo.Roles WHERE RoleName=N'CRM');

INSERT INTO dbo.Users (Username,PasswordHash,PasswordSalt,FullName,Email,Phone,RoleId,IsActive,IsLocked,FailedLoginAttempts,CreatedAt,UpdatedAt) VALUES
(N'admin',      LOWER(CONVERT(VARCHAR(64),HASHBYTES('SHA2_256',CONVERT(VARBINARY(MAX),N'admin123'      +@s1)),2)),@s1,N'System Administrator',N'admin@example.com',     N'',@rAdmin,1,0,0,@now,@now),
(N'hr',         LOWER(CONVERT(VARCHAR(64),HASHBYTES('SHA2_256',CONVERT(VARBINARY(MAX),N'hr123'         +@s2)),2)),@s2,N'HR User',             N'hr@example.com',        N'',@rHR,   1,0,0,@now,@now),
(N'sales',      LOWER(CONVERT(VARCHAR(64),HASHBYTES('SHA2_256',CONVERT(VARBINARY(MAX),N'sales123'      +@s3)),2)),@s3,N'Sales User',          N'sales@example.com',     N'',@rSales,1,0,0,@now,@now),
(N'inventory',  LOWER(CONVERT(VARCHAR(64),HASHBYTES('SHA2_256',CONVERT(VARBINARY(MAX),N'inventory123'  +@s4)),2)),@s4,N'Inventory User',      N'inventory@example.com', N'',@rInv,  1,0,0,@now,@now),
(N'accounting', LOWER(CONVERT(VARCHAR(64),HASHBYTES('SHA2_256',CONVERT(VARBINARY(MAX),N'accounting123' +@s5)),2)),@s5,N'Accounting User',     N'accounting@example.com',N'',@rAcc,  1,0,0,@now,@now),
(N'crm',        LOWER(CONVERT(VARCHAR(64),HASHBYTES('SHA2_256',CONVERT(VARBINARY(MAX),N'crm123'        +@s6)),2)),@s6,N'CRM User',             N'crm@example.com',       N'',@rCRM,  1,0,0,@now,@now);
GO

-- Company default
INSERT INTO dbo.Companies (CompanyName,Address,TaxCode,Phone,Email,Website,RepresentativeName,RepresentativePosition,Hotline)
VALUES (N'Công Ty TNHH SharkTank',N'123 Đường ABC, Quận 1, TP.HCM',N'0123456789',N'028 1234 5678',N'contact@sharktank.com',N'www.sharktank.com',N'Nguyễn Văn A',N'Giám đốc',N'1900 1234');
GO

-- SystemConfigs defaults
INSERT INTO dbo.SystemConfigs (ConfigKey,ConfigValue,ConfigGroup,Description,DataType) VALUES
(N'AppName',                 N'SharkTank ERP',   N'General',  N'Tên ứng dụng',                   'string'),
(N'AppVersion',              N'1.0.0',            N'General',  N'Phiên bản',                       'string'),
(N'DefaultLanguage',         N'vi-VN',            N'General',  N'Ngôn ngữ mặc định',               'string'),
(N'AutoBackup',              N'1',                N'General',  N'Tự động sao lưu',                 'bool'),
(N'BackupInterval',          N'7',                N'General',  N'Khoảng sao lưu (ngày)',           'int'),
(N'PasswordMinLength',       N'8',                N'Security', N'Độ dài tối thiểu mật khẩu',       'int'),
(N'MaxLoginAttempts',        N'5',                N'Security', N'Số lần đăng nhập thất bại tối đa','int'),
(N'LockAccountOnFail',       N'1',                N'Security', N'Tự động khóa tài khoản',          'bool'),
(N'PasswordExpiryDays',      N'90',               N'Security', N'Hạn hiệu lực mật khẩu',          'int'),
(N'ThemeColor',              N'Blue',             N'Display',  N'Màu nền',                         'string'),
(N'FontSize',                N'10',               N'Display',  N'Kích thước chữ',                  'int'),
(N'SmtpServer',              N'smtp.gmail.com',   N'Email',    N'SMTP Server',                     'string'),
(N'SmtpPort',                N'587',              N'Email',    N'SMTP Port',                       'int'),
(N'EmailFrom',               N'',                 N'Email',    N'Email gửi',                       'string'),
(N'DefaultCurrency',         N'VND',              N'Format',   N'Đơn vị tiền tệ mặc định',         'string'),
(N'DateFormat',              N'dd/MM/yyyy',        N'Format',   N'Định dạng ngày',                  'string'),
(N'TimeZone',                N'UTC+07:00',         N'Format',   N'Múi giờ',                         'string'),
(N'DecimalPlaces',           N'2',                N'Format',   N'Số chữ số thập phân',             'int'),
(N'ShowCurrencySymbol',      N'1',                N'Format',   N'Hiển thị ký hiệu tiền tệ',        'bool');
GO

-- HR seed data
INSERT INTO dbo.PhongBan (TenPhongBan) VALUES
(N'Ban Giám Đốc'),(N'Phòng Nhân Sự'),(N'Phòng Kế Toán'),
(N'Phòng Kinh Doanh'),(N'Phòng Kho Vận'),(N'Phòng IT');

INSERT INTO dbo.ChucVu (TenChucVu) VALUES
(N'Giám Đốc'),(N'Phó Giám Đốc'),(N'Trưởng Phòng'),
(N'Nhân Viên'),(N'Thực Tập Sinh');
GO

SELECT 'SharkTank ERP - Full schema created successfully.' AS Status;
GO