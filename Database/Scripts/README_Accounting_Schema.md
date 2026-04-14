# Hướng dẫn cài đặt Database cho Module Kế Toán (Accounting)

## File SQL Script

**File:** `Database\Scripts\Accounting_Schema.sql`

## Cách chạy Script

### Cách 1: Chạy trực tiếp trong SQL Server Management Studio (SSMS)

1. Mở **SQL Server Management Studio (SSMS)**
2. Kết nối đến SQL Server của bạn
3. Chọn Database cần tạo bảng (hoặc tạo mới database)
4. Nhấn **Ctrl + O** hoặc vào **File > Open > File**
5. Chọn file `Accounting_Schema.sql`
6. Nhấn **F5** hoặc nhấn nút **Execute**
7. Kiểm tra thông báo "completed successfully" trong cửa sổ Messages

### Cách 2: Chạy bằng Command Line (SQLCMD)

```bash
sqlcmd -S ServerName -d DatabaseName -i "E:\Dự Án\SharkTankDesktop\Database\Scripts\Accounting_Schema.sql"
```

### Cách 3: Chạy qua PowerShell

```powershell
Invoke-Sqlcmd -ServerInstance "ServerName" -Database "DatabaseName" -InputFile "E:\Dự Án\SharkTankDesktop\Database\Scripts\Accounting_Schema.sql"
```

## Các bảng được tạo

### 1. ChiPhi - Bảng ghi nhận chi phí

| Column | Type | Mô tả |
|--------|------|-------|
| MaChiPhi | NVARCHAR(50) | Mã chi phí (Primary Key) |
| LoaiChiPhi | NVARCHAR(100) | Loại chi phí |
| NgayChi | DATE | Ngày chi |
| NoiDung | NVARCHAR(500) | Nội dung chi |
| SoTien | DECIMAL(18,2) | Số tiền |
| TrangThai | NVARCHAR(50) | Trạng thái |
| GhiChu | NVARCHAR(500) | Ghi chú |
| NgayTao | DATETIME | Ngày tạo |

**Loại chi phí:**
- Chi phí văn phòng
- Chi phí điện nước
- Chi phí nhân sự
- Chi phí marketing
- Chi phí vận chuyển
- Chi phí thuê mặt bằng
- Chi phí bảo trì
- Chi phí khác

**Trạng thái:**
- Đã thanh toán
- Chưa thanh toán
- Đang xử lý

---

### 2. DoanhThu - Bảng ghi nhận doanh thu

| Column | Type | Mô tả |
|--------|------|-------|
| MaDoanhThu | NVARCHAR(50) | Mã doanh thu (Primary Key) |
| LoaiDoanhThu | NVARCHAR(100) | Loại doanh thu |
| NgayThu | DATE | Ngày thu |
| NoiDung | NVARCHAR(500) | Nội dung |
| SoTien | DECIMAL(18,2) | Số tiền |
| TrangThai | NVARCHAR(50) | Trạng thái |
| GhiChu | NVARCHAR(500) | Ghi chú |
| NgayTao | DATETIME | Ngày tạo |

**Loại doanh thu:**
- Doanh thu bán hàng
- Doanh thu dịch vụ
- Doanh thu khác
- Thu hồi công nợ
- Lãi đầu tư
- Hoàn nhập dự phòng

**Trạng thái:**
- Đã thu
- Chưa thu
- Đang xử lý

---

### 3. DonHangSales - Bảng đơn hàng từ Sales

| Column | Type | Mô tả |
|--------|------|-------|
| MaDonHang | NVARCHAR(50) | Mã đơn hàng (Primary Key) |
| NgayDatHang | DATE | Ngày đặt hàng |
| TenKhachHang | NVARCHAR(200) | Tên khách hàng |
| SanPham | NVARCHAR(200) | Sản phẩm |
| SoLuong | INT | Số lượng |
| DonGia | DECIMAL(18,2) | Đơn giá |
| ThanhTien | DECIMAL(18,2) | Thành tiền (tự tính) |
| TrangThai | NVARCHAR(50) | Trạng thái Sales |
| TrangThaiKetoan | NVARCHAR(50) | Trạng thái kế toán |
| GhiChu | NVARCHAR(500) | Ghi chú |
| NgayTao | DATETIME | Ngày tạo |

**Trạng thái Kế toán:**
- Chưa nhập (mặc định)
- Đã nhập
- Từ chối

---

## Dữ liệu mẫu

Script đã bao gồm dữ liệu mẫu để test:

### ChiPhi (5 bản ghi)
- CP001: Chi phí văn phòng - 1,500,000đ
- CP002: Chi phí điện nước - 3,500,000đ
- CP003: Chi phí nhân sự - 50,000,000đ
- CP004: Chi phí marketing - 8,000,000đ
- CP005: Chi phí vận chuyển - 2,500,000đ

### DoanhThu (5 bản ghi)
- DT001: Doanh thu bán hàng - 25,000,000đ
- DT002: Doanh thu dịch vụ - 15,000,000đ
- DT003: Thu hồi công nợ - 10,000,000đ
- DT004: Doanh thu bán hàng - 3,500,000đ
- DT005: Doanh thu bán hàng - 75,000,000đ

### DonHangSales (6 bản ghi)
- DH001: Công ty ABC - Chưa nhập
- DH002: Công ty XYZ - Chưa nhập
- DH003: Khách hàng lẻ - Đã nhập
- DH004: Công ty DEF - Chưa nhập
- DH005: Trung tâm LMN - Từ chối
- DH006: Công ty GHI - Chưa nhập

---

## Lưu ý

1. **Database Name:** Thay đổi `DatabaseName` trong connection string trong file `App.config` để kết nối đúng database
2. **Quyền truy cập:** Đảm bảo tài khoản SQL của bạn có quyền CREATE TABLE, INSERT, SELECT, UPDATE, DELETE
3. **Index:** Các bảng đã được tạo index để tối ưu tìm kiếm theo ngày và loại
4. **Reset dữ liệu:** Nếu muốn reset dữ liệu, chạy lại script sẽ xóa và tạo lại dữ liệu mẫu

---

## Kết nối từ ứng dụng

Connection String trong `App.config`:
```xml
<connectionStrings>
  <add name="SharkTankDB" connectionString="Data Source=ServerName;Initial Catalog=DatabaseName;Integrated Security=True" />
</connectionStrings>
```

Đảm bảo `DatabaseName` trong connection string khớp với database bạn đã chạy script.
