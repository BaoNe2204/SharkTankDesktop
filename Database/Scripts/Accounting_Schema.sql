-- =============================================
-- SHARK TANK DESKTOP - ACCOUNTING MODULE
-- Database Schema for ChiPhi, DoanhThu, DonHangSales
-- =============================================

-- =============================================
-- Bảng ChiPhi - Ghi nhận chi phí
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ChiPhi' AND xtype='U')
BEGIN
    CREATE TABLE ChiPhi (
        MaChiPhi NVARCHAR(50) PRIMARY KEY,
        LoaiChiPhi NVARCHAR(100) NULL,
        NgayChi DATE NOT NULL,
        NoiDung NVARCHAR(500) NULL,
        SoTien DECIMAL(18,2) NOT NULL DEFAULT 0,
        TrangThai NVARCHAR(50) NULL DEFAULT N'Chưa thanh toán',
        GhiChu NVARCHAR(500) NULL,
        NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
        NguoiTao NVARCHAR(100) NULL,
        NgaySua DATETIME NULL,
        NguoiSua NVARCHAR(100) NULL
    );

    -- Tạo index cho tìm kiếm
    CREATE NONCLUSTERED INDEX IX_ChiPhi_NgayChi ON ChiPhi(NgayChi);
    CREATE NONCLUSTERED INDEX IX_ChiPhi_LoaiChiPhi ON ChiPhi(LoaiChiPhi);
    CREATE NONCLUSTERED INDEX IX_ChiPhi_TrangThai ON ChiPhi(TrangThai);

    PRINT 'Table ChiPhi created successfully';
END
ELSE
BEGIN
    PRINT 'Table ChiPhi already exists';
END
GO

-- =============================================
-- Bảng DoanhThu - Ghi nhận doanh thu
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DoanhThu' AND xtype='U')
BEGIN
    CREATE TABLE DoanhThu (
        MaDoanhThu NVARCHAR(50) PRIMARY KEY,
        LoaiDoanhThu NVARCHAR(100) NULL,
        NgayThu DATE NOT NULL,
        NoiDung NVARCHAR(500) NULL,
        SoTien DECIMAL(18,2) NOT NULL DEFAULT 0,
        TrangThai NVARCHAR(50) NULL DEFAULT N'Chưa thu',
        GhiChu NVARCHAR(500) NULL,
        NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
        NguoiTao NVARCHAR(100) NULL,
        NgaySua DATETIME NULL,
        NguoiSua NVARCHAR(100) NULL
    );

    -- Tạo index cho tìm kiếm
    CREATE NONCLUSTERED INDEX IX_DoanhThu_NgayThu ON DoanhThu(NgayThu);
    CREATE NONCLUSTERED INDEX IX_DoanhThu_LoaiDoanhThu ON DoanhThu(LoaiDoanhThu);
    CREATE NONCLUSTERED INDEX IX_DoanhThu_TrangThai ON DoanhThu(TrangThai);

    PRINT 'Table DoanhThu created successfully';
END
ELSE
BEGIN
    PRINT 'Table DoanhThu already exists';
END
GO

-- =============================================
-- Bảng DonHangSales - Đơn hàng từ Sales
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DonHangSales' AND xtype='U')
BEGIN
    CREATE TABLE DonHangSales (
        MaDonHang NVARCHAR(50) PRIMARY KEY,
        NgayDatHang DATE NOT NULL,
        TenKhachHang NVARCHAR(200) NULL,
        SanPham NVARCHAR(200) NULL,
        SoLuong INT NOT NULL DEFAULT 0,
        DonGia DECIMAL(18,2) NOT NULL DEFAULT 0,
        ThanhTien AS (SoLuong * DonGia) PERSISTED,
        TrangThai NVARCHAR(50) NULL DEFAULT N'Hoàn thành',
        TrangThaiKetoan NVARCHAR(50) NULL DEFAULT N'Chưa nhập',
        GhiChu NVARCHAR(500) NULL,
        NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
        NguoiTao NVARCHAR(100) NULL,
        NgaySua DATETIME NULL,
        NguoiSua NVARCHAR(100) NULL
    );

    -- Tạo index cho tìm kiếm
    CREATE NONCLUSTERED INDEX IX_DonHangSales_NgayDatHang ON DonHangSales(NgayDatHang);
    CREATE NONCLUSTERED INDEX IX_DonHangSales_TrangThaiKetoan ON DonHangSales(TrangThaiKetoan);
    CREATE NONCLUSTERED INDEX IX_DonHangSales_TenKhachHang ON DonHangSales(TenKhachHang);

    PRINT 'Table DonHangSales created successfully';
END
ELSE
BEGIN
    PRINT 'Table DonHangSales already exists';
END
GO

-- =============================================
-- Dữ liệu mẫu cho ChiPhi (test)
-- =============================================
IF NOT EXISTS (SELECT * FROM ChiPhi WHERE MaChiPhi = 'CP001')
BEGIN
    INSERT INTO ChiPhi (MaChiPhi, LoaiChiPhi, NgayChi, NoiDung, SoTien, TrangThai, GhiChu, NgayTao)
    VALUES 
    (N'CP001', N'Chi phí văn phòng', '2026-04-01', N'Mua văn phòng phẩm tháng 4', 1500000, N'Đã thanh toán', N'Thanh toán tiền mặt', GETDATE()),
    (N'CP002', N'Chi phí điện nước', '2026-04-05', N'Tiền điện tháng 3/2026', 3500000, N'Đã thanh toán', N'Thanh toán chuyển khoản', GETDATE()),
    (N'CP003', N'Chi phí nhân sự', '2026-04-10', N'Lương nhân viên tháng 4', 50000000, N'Đã thanh toán', N'Chuyển khoản ngân hàng', GETDATE()),
    (N'CP004', N'Chi phí marketing', '2026-04-12', N'Quảng cáo Facebook tháng 4', 8000000, N'Chưa thanh toán', N'Đang chờ xác nhận', GETDATE()),
    (N'CP005', N'Chi phí vận chuyển', '2026-04-15', N'Giao hàng cho khách hàng A', 2500000, N'Đã thanh toán', N'Thanh toán COD', GETDATE());

    PRINT 'Sample data inserted into ChiPhi';
END
GO

-- =============================================
-- Dữ liệu mẫu cho DoanhThu (test)
-- =============================================
IF NOT EXISTS (SELECT * FROM DoanhThu WHERE MaDoanhThu = 'DT001')
BEGIN
    INSERT INTO DoanhThu (MaDoanhThu, LoaiDoanhThu, NgayThu, NoiDung, SoTien, TrangThai, GhiChu, NgayTao)
    VALUES 
    (N'DT001', N'Doanh thu bán hàng', '2026-04-01', N'Bán sản phẩm cho công ty ABC', 25000000, N'Đã thu', N'Thanh toán chuyển khoản', GETDATE()),
    (N'DT002', N'Doanh thu dịch vụ', '2026-04-05', N'Dịch vụ tư vấn tháng 4', 15000000, N'Đã thu', N'Khách hàng thanh toán đầy đủ', GETDATE()),
    (N'DT003', N'Thu hồi công nợ', '2026-04-08', N'Thu hồi công nợ từ công ty XYZ', 10000000, N'Đã thu', N'Công nợ tháng 2', GETDATE()),
    (N'DT004', N'Doanh thu bán hàng', '2026-04-12', N'Bán sản phẩm cho khách lẻ', 3500000, N'Chưa thu', N'Đang chờ thanh toán', GETDATE()),
    (N'DT005', N'Doanh thu bán hàng', '2026-04-15', N'Đơn hàng lớn công ty DEF', 75000000, N'Đã thu', N'Thanh toán 50% trước', GETDATE());

    PRINT 'Sample data inserted into DoanhThu';
END
GO

-- =============================================
-- Dữ liệu mẫu cho DonHangSales (test)
-- =============================================
IF NOT EXISTS (SELECT * FROM DonHangSales WHERE MaDonHang = 'DH001')
BEGIN
    INSERT INTO DonHangSales (MaDonHang, NgayDatHang, TenKhachHang, SanPham, SoLuong, DonGia, TrangThai, TrangThaiKetoan, NgayTao)
    VALUES 
    (N'DH001', '2026-04-01', N'Công ty ABC', N'Sản phẩm A', 10, 2500000, N'Hoàn thành', N'Chưa nhập', GETDATE()),
    (N'DH002', '2026-04-03', N'Công ty XYZ', N'Sản phẩm B', 5, 3000000, N'Hoàn thành', N'Chưa nhập', GETDATE()),
    (N'DH003', '2026-04-05', N'Khách hàng lẻ', N'Sản phẩm C', 2, 1500000, N'Hoàn thành', N'Đã nhập', GETDATE()),
    (N'DH004', '2026-04-08', N'Công ty DEF', N'Sản phẩm A', 20, 2500000, N'Hoàn thành', N'Chưa nhập', GETDATE()),
    (N'DH005', '2026-04-10', N'Trung tâm LMN', N'Sản phẩm D', 8, 4000000, N'Hoàn thành', N'Từ chối', GETDATE()),
    (N'DH006', '2026-04-12', N'Công ty GHI', N'Sản phẩm B', 15, 3000000, N'Hoàn thành', N'Chưa nhập', GETDATE());

    PRINT 'Sample data inserted into DonHangSales';
END
GO

PRINT '=============================================';
PRINT 'Database setup completed successfully!';
PRINT '=============================================';
GO
