/*
  Core System ERP - Login & RBAC (SQL Server)
  - Tables: Roles, Users, Permissions, RolePermissions, LoginSessions
  - Seed: 5 roles + permissions + 5 demo users (admin/hr/sales/inventory/accounting)

  Notes:
  - PasswordHash = LOWER(hex(SHA2_256( NVARCHAR(password + salt) ))) to match C# PasswordHasher (Encoding.Unicode + hex lowercase)
  - PasswordSalt = random hex (32 chars)
*/

SET NOCOUNT ON;

IF OBJECT_ID('dbo.RolePermissions', 'U') IS NOT NULL DROP TABLE dbo.RolePermissions;
IF OBJECT_ID('dbo.LoginSessions', 'U') IS NOT NULL DROP TABLE dbo.LoginSessions;
IF OBJECT_ID('dbo.Permissions', 'U') IS NOT NULL DROP TABLE dbo.Permissions;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL DROP TABLE dbo.Roles;

CREATE TABLE dbo.Roles
(
    RoleId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Roles PRIMARY KEY,
    RoleName NVARCHAR(100) NOT NULL CONSTRAINT UQ_Roles_RoleName UNIQUE,
    [Description] NVARCHAR(255) NULL,
    IsSystemRole BIT NOT NULL CONSTRAINT DF_Roles_IsSystemRole DEFAULT(1)
);

CREATE TABLE dbo.Users
(
    UserId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL CONSTRAINT UQ_Users_Username UNIQUE,
    PasswordHash NVARCHAR(128) NOT NULL,
    PasswordSalt NVARCHAR(64) NOT NULL,
    FullName NVARCHAR(200) NULL,
    Email NVARCHAR(200) NULL,
    Phone NVARCHAR(50) NULL,
    RoleId INT NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Users_IsActive DEFAULT(1),
    IsLocked BIT NOT NULL CONSTRAINT DF_Users_IsLocked DEFAULT(0),
    FailedLoginAttempts INT NOT NULL CONSTRAINT DF_Users_Failed DEFAULT(0),
    LastLoginAt DATETIME NULL,
    CreatedAt DATETIME NOT NULL CONSTRAINT DF_Users_Created DEFAULT(GETDATE()),
    UpdatedAt DATETIME NOT NULL CONSTRAINT DF_Users_Updated DEFAULT(GETDATE()),
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles(RoleId)
);

CREATE TABLE dbo.Permissions
(
    PermissionId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Permissions PRIMARY KEY,
    PermissionCode NVARCHAR(100) NOT NULL CONSTRAINT UQ_Permissions_Code UNIQUE,
    PermissionName NVARCHAR(200) NULL,
    [Module] NVARCHAR(100) NULL
);

CREATE TABLE dbo.RolePermissions
(
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    CONSTRAINT PK_RolePermissions PRIMARY KEY (RoleId, PermissionId),
    CONSTRAINT FK_RolePermissions_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles(RoleId),
    CONSTRAINT FK_RolePermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES dbo.Permissions(PermissionId)
);

CREATE TABLE dbo.LoginSessions
(
    SessionId UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_LoginSessions PRIMARY KEY,
    UserId INT NOT NULL,
    LoginTime DATETIME NOT NULL,
    LogoutTime DATETIME NULL,
    IpAddress NVARCHAR(64) NULL,
    DeviceInfo NVARCHAR(255) NULL,
    IsActive BIT NOT NULL,
    CONSTRAINT FK_LoginSessions_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId)
);

-- Seed Roles
INSERT INTO dbo.Roles (RoleName, [Description], IsSystemRole) VALUES
 (N'Admin', N'System Administrator', 1),
 (N'HR', N'Human Resources', 1),
 (N'Sales', N'Sales', 1),
 (N'Inventory', N'Inventory/Warehouse', 1),
 (N'Accounting', N'Accounting', 1);

-- Seed Permissions
INSERT INTO dbo.Permissions (PermissionCode, PermissionName, [Module]) VALUES
 (N'HR.VIEW', N'HR - View', N'HR'),
 (N'HR.CREATE', N'HR - Create', N'HR'),
 (N'HR.UPDATE', N'HR - Update', N'HR'),
 (N'HR.DELETE', N'HR - Delete', N'HR'),

 (N'SALES.VIEW', N'Sales - View', N'Sales'),
 (N'SALES.CREATE', N'Sales - Create', N'Sales'),
 (N'SALES.UPDATE', N'Sales - Update', N'Sales'),

 (N'INVENTORY.VIEW', N'Inventory - View', N'Inventory'),
 (N'INVENTORY.CREATE', N'Inventory - Create', N'Inventory'),
 (N'INVENTORY.UPDATE', N'Inventory - Update', N'Inventory'),

 (N'ACCOUNTING.VIEW', N'Accounting - View', N'Accounting'),
 (N'ACCOUNTING.CREATE', N'Accounting - Create', N'Accounting'),
 (N'ACCOUNTING.UPDATE', N'Accounting - Update', N'Accounting'),

 (N'ADMIN.VIEW', N'Admin - View', N'Admin');

DECLARE @RoleAdmin INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'Admin');
DECLARE @RoleHR INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'HR');
DECLARE @RoleSales INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'Sales');
DECLARE @RoleInventory INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'Inventory');
DECLARE @RoleAccounting INT = (SELECT RoleId FROM dbo.Roles WHERE RoleName = N'Accounting');

-- RolePermissions
-- Admin: tất cả quyền
INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleAdmin, PermissionId FROM dbo.Permissions;

-- HR: full CRUD HR
INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleHR, PermissionId FROM dbo.Permissions WHERE PermissionCode LIKE N'HR.%';

-- Sales: VIEW/CREATE/UPDATE
INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleSales, PermissionId FROM dbo.Permissions WHERE PermissionCode IN (N'SALES.VIEW', N'SALES.CREATE', N'SALES.UPDATE');

-- Inventory: VIEW/CREATE/UPDATE
INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleInventory, PermissionId FROM dbo.Permissions WHERE PermissionCode IN (N'INVENTORY.VIEW', N'INVENTORY.CREATE', N'INVENTORY.UPDATE');

-- Accounting: VIEW/CREATE/UPDATE
INSERT INTO dbo.RolePermissions(RoleId, PermissionId)
SELECT @RoleAccounting, PermissionId FROM dbo.Permissions WHERE PermissionCode IN (N'ACCOUNTING.VIEW', N'ACCOUNTING.CREATE', N'ACCOUNTING.UPDATE');

-- Helper: create user with salt + hash
DECLARE @now DATETIME = GETDATE();

-- Salt: 32 hex chars (GUID -> BINARY(16) -> style 2 hex)
DECLARE @salt_admin NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32), CONVERT(BINARY(16), NEWID()), 2));
DECLARE @salt_hr NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32), CONVERT(BINARY(16), NEWID()), 2));
DECLARE @salt_sales NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32), CONVERT(BINARY(16), NEWID()), 2));
DECLARE @salt_inventory NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32), CONVERT(BINARY(16), NEWID()), 2));
DECLARE @salt_accounting NVARCHAR(64) = LOWER(CONVERT(VARCHAR(32), CONVERT(BINARY(16), NEWID()), 2));

INSERT INTO dbo.Users
(Username, PasswordHash, PasswordSalt, FullName, Email, Phone, RoleId, IsActive, IsLocked, FailedLoginAttempts, CreatedAt, UpdatedAt)
VALUES
(
  N'admin',
  LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CONVERT(VARBINARY(MAX), N'admin123' + @salt_admin)), 2)),
  @salt_admin,
  N'System Administrator', N'admin@example.com', N'', @RoleAdmin, 1, 0, 0, @now, @now
),
(
  N'hr',
  LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CONVERT(VARBINARY(MAX), N'hr123' + @salt_hr)), 2)),
  @salt_hr,
  N'HR User', N'hr@example.com', N'', @RoleHR, 1, 0, 0, @now, @now
),
(
  N'sales',
  LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CONVERT(VARBINARY(MAX), N'sales123' + @salt_sales)), 2)),
  @salt_sales,
  N'Sales User', N'sales@example.com', N'', @RoleSales, 1, 0, 0, @now, @now
),
(
  N'inventory',
  LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CONVERT(VARBINARY(MAX), N'inventory123' + @salt_inventory)), 2)),
  @salt_inventory,
  N'Inventory User', N'inventory@example.com', N'', @RoleInventory, 1, 0, 0, @now, @now
),
(
  N'accounting',
  LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CONVERT(VARBINARY(MAX), N'accounting123' + @salt_accounting)), 2)),
  @salt_accounting,
  N'Accounting User', N'accounting@example.com', N'', @RoleAccounting, 1, 0, 0, @now, @now
);

SELECT 'Seed done' AS Status;

