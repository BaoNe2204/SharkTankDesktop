-- =============================================
-- SQL Script tạo bảng cấu hình hệ thống
-- =============================================

-- Bảng Thông Tin Công Ty
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Companies' AND xtype = 'U')
BEGIN
    CREATE TABLE Companies (
        Id INT PRIMARY KEY IDENTITY(1,1),
        CompanyName NVARCHAR(200) NOT NULL,
        Address NVARCHAR(500),
        TaxCode NVARCHAR(50),
        Phone NVARCHAR(20),
        Email NVARCHAR(100),
        Website NVARCHAR(200),
        BusinessLicense NVARCHAR(100),
        LogoPath NVARCHAR(500),
        RepresentativeName NVARCHAR(100),
        RepresentativePosition NVARCHAR(100),
        Hotline NVARCHAR(20),
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETDATE(),
        UpdatedAt DATETIME DEFAULT GETDATE()
    )
    
    -- Insert default company
    INSERT INTO Companies (CompanyName, Address, TaxCode, Phone, Email, Website, RepresentativeName, RepresentativePosition, Hotline)
    VALUES (N'Công Ty TNHH SharkTank', N'123 Đường ABC, Quận 1, TP.HCM', '0123456789', '028 1234 5678', 'contact@sharktank.com', 'www.sharktank.com', N'Nguyễn Văn A', N'Giám đốc', '1900 1234')
END
GO

-- Bảng Cấu Hình Hệ Thống (Key-Value)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'SystemConfigs' AND xtype = 'U')
BEGIN
    CREATE TABLE SystemConfigs (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ConfigKey NVARCHAR(100) NOT NULL UNIQUE,
        ConfigValue NVARCHAR(MAX),
        ConfigGroup NVARCHAR(50),
        Description NVARCHAR(255),
        DataType NVARCHAR(20) DEFAULT 'string', -- string, int, bool, datetime
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETDATE(),
        UpdatedAt DATETIME DEFAULT GETDATE()
    )
    
    -- Insert default configs
    INSERT INTO SystemConfigs (ConfigKey, ConfigValue, ConfigGroup, Description, DataType) VALUES
    -- General
    (N'AppName', N'SharkTank ERP', N'General', N'Tên ứng dụng', 'string'),
    (N'AppDescription', N'Hệ thống quản lý doanh nghiệp', N'General', N'Mô tả', 'string'),
    (N'AppVersion', N'1.0.0', N'General', N'Phiên bản', 'string'),
    (N'DefaultLanguage', N'vi-VN', N'General', N'Ngôn ngữ mặc định', 'string'),
    (N'AutoBackup', N'1', N'General', N'Tự động sao lưu', 'bool'),
    (N'BackupInterval', N'7', N'General', N'Khoảng sao lưu (ngày)', 'int'),
    
    -- Security
    (N'PasswordMinLength', N'8', N'Security', N'Độ dài tối thiểu mật khẩu', 'int'),
    (N'PasswordRequireUppercase', N'1', N'Security', N'Yêu cầu chữ HOA', 'bool'),
    (N'PasswordRequireLowercase', N'1', N'Security', N'Yêu cầu chữ thường', 'bool'),
    (N'PasswordRequireDigit', N'1', N'Security', N'Yêu cầu số', 'bool'),
    (N'PasswordRequireSpecial', N'0', N'Security', N'Yêu cầu ký tự đặc biệt', 'bool'),
    (N'PasswordExpiryDays', N'90', N'Security', N'Hạn hiệu lực mật khẩu', 'int'),
    (N'LockAccountOnFail', N'1', N'Security', N'Tự động khóa tài khoản', 'bool'),
    (N'MaxLoginAttempts', N'5', N'Security', N'Số lần đăng nhập thất bại tối đa', 'int'),
    
    -- Display
    (N'ThemeColor', N'Blue', N'Display', N'Màu nền', 'string'),
    (N'ThemeStyle', N'Modern', N'Display', N'Phong cách giao diện', 'string'),
    (N'ShowUsername', N'1', N'Display', N'Hiển thị tên người dùng', 'bool'),
    (N'ShowAvatar', N'1', N'Display', N'Hiển thị ảnh đại diện', 'bool'),
    (N'FontSize', N'10', N'Display', N'Kích thước chữ', 'int'),
    
    -- Email
    (N'SmtpServer', N'smtp.gmail.com', N'Email', N'SMTP Server', 'string'),
    (N'SmtpPort', N'587', N'Email', N'SMTP Port', 'int'),
    (N'SmtpEncryption', N'TLS', N'Email', N'Mã hóa', 'string'),
    (N'EmailFrom', N'', N'Email', N'Email gửi', 'string'),
    (N'EmailUsername', N'', N'Email', N'Tài khoản email', 'string'),
    (N'EmailPassword', N'', N'Email', N'Mật khẩu email', 'string'),
    (N'UseDefaultCredentials', N'1', N'Email', N'Dùng credentials mặc định', 'bool'),
    
    -- Currency & Format
    (N'DefaultCurrency', N'VND', N'Format', N'Đơn vị tiền tệ mặc định', 'string'),
    (N'DecimalPlaces', N'2', N'Format', N'Số chữ số thập phân', 'int'),
    (N'ThousandSeparator', N',', N'Format', N'Ký hiệu ngăn cách hàng nghìn', 'string'),
    (N'DecimalSeparator', N'.', N'Format', N'Ký hiệu ngăn cách thập phân', 'string'),
    (N'DateFormat', N'dd/MM/yyyy', N'Format', N'Định dạng ngày', 'string'),
    (N'TimeFormat', N'HH:mm:ss', N'Format', N'Định dạng thời gian', 'string'),
    (N'WeekStartDay', N'Monday', N'Format', N'Ngày bắt đầu tuần', 'string'),
    (N'TimeZone', N'UTC+07:00', N'Format', N'Múi giờ', 'string'),
    (N'NumberDecimalSeparator', N'.', N'Format', N'Dấu thập phân cho số', 'string'),
    (N'NumberThousandSeparator', N',', N'Format', N'Dấu ngăn cách hàng nghìn cho số', 'string'),
    (N'AutoRoundNumbers', N'1', N'Format', N'Tự động làm tròn số', 'bool'),
    (N'ShowCurrencySymbol', N'1', N'Format', N'Hiển thị ký hiệu tiền tệ', 'bool'),
    (N'UseTimeZone', N'1', N'Format', N'Dùng múi giờ hệ thống', 'bool')
END
GO

-- Bổ sung key mới nếu DB đã tạo trước đó (chạy an toàn nhiều lần)
IF NOT EXISTS (SELECT 1 FROM SystemConfigs WHERE ConfigKey = N'ShowCurrencySymbol')
    INSERT INTO SystemConfigs (ConfigKey, ConfigValue, ConfigGroup, Description, DataType)
    VALUES (N'ShowCurrencySymbol', N'1', N'Format', N'Hiển thị ký hiệu tiền tệ', 'bool');
IF NOT EXISTS (SELECT 1 FROM SystemConfigs WHERE ConfigKey = N'UseTimeZone')
    INSERT INTO SystemConfigs (ConfigKey, ConfigValue, ConfigGroup, Description, DataType)
    VALUES (N'UseTimeZone', N'1', N'Format', N'Dùng múi giờ hệ thống', 'bool');
GO
