-- 1. Bảng Khách hàng (Lưu thông tin cơ bản)
CREATE TABLE KhachHang (
    MaKH NVARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255),
    DienThoai NVARCHAR(15),
    Email NVARCHAR(100),
    NgayTao DATETIME DEFAULT GETDATE()
);

-- 2. Bảng Hóa đơn (Để tính Tổng chi tiêu và Số đơn hàng cho việc phân loại)
CREATE TABLE HoaDon (
    MaHD NVARCHAR(20) PRIMARY KEY,
    MaKH NVARCHAR(20),
    NgayLap DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18, 2),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

-- 3. Bảng Cấu hình Phân loại (Để sau này An đổi hạn mức VIP dễ dàng)
CREATE TABLE CauHinhPhanLoai (
    MaLoai NVARCHAR(20) PRIMARY KEY, -- VIP, LOYAL, POTENTIAL, NEW
    TenLoai NVARCHAR(50),
    ChiTieuToiThieu DECIMAL(18, 2),
    SoDonToiThieu INT,
    Icon NVARCHAR(10) -- Lưu emoji như 💎, ⭐
);

-- Chèn dữ liệu mẫu cho cấu hình
INSERT INTO CauHinhPhanLoai VALUES 
('VIP', N'Khách VIP', 100000000, 20, N'💎'),
('LOYAL', N'Thân thiết', 20000000, 10, N'⭐'),
('POTENTIAL', N'Tiềm năng', 1, 1, N'🟢'),
('NEW', N'Khách mới', 0, 0, N'⚪');