/*
  Create database for SharkTank ERP (SQL Server)
  Database name: SharkTankERP
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

