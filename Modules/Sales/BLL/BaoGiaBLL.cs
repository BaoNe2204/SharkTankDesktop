using System;
using System.Data;
using System.Data.SqlClient;

// Lưu ý: Đổi namespace này cho khớp với cấu trúc thư mục của bạn nếu cần
namespace SharkTankDesktop.Modules.Sales.BLL
{
    public class BaoGiaBLL
    {
        // Chuỗi kết nối Database
        private string strCon = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True;TrustServerCertificate=True";

        // ==========================================
        // CÁC HÀM DÀNH CHO FORM CHÍNH (Quản lý báo giá)
        // ==========================================

        // 1. Hàm lấy danh sách tất cả Báo Giá (HÀM ĐANG BỊ THIẾU)
        public DataTable GetDanhSachBaoGia()
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string sql = @"SELECT bg.MaBG AS [Mã BG], kh.HoTen AS [Khách Hàng], 
                                      bg.NgayLap AS [Ngày Lập], bg.TongTien AS [Tổng Tiền], 
                                      bg.TrangThai AS [Trạng Thái], bg.GhiChu AS [Ghi Chú]
                               FROM BaoGia bg
                               JOIN KhachHang kh ON bg.MaKH = kh.MaKH
                               ORDER BY bg.NgayLap DESC";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 2. Hàm duyệt báo giá (HÀM ĐANG BỊ THIẾU)
        public bool DuyetBaoGia(string maBG)
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string sql = "UPDATE BaoGia SET TrangThai = N'Đã chốt' WHERE MaBG = @ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", maBG);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ==========================================
        // CÁC HÀM DÀNH CHO FORM POPUP (Tạo báo giá mới)
        // ==========================================

        // 3. Hàm lấy danh sách khách hàng đổ vào ComboBox
        public DataTable GetDanhSachKhachHang()
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaKH, HoTen FROM KhachHang", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 4. Hàm lấy danh sách sản phẩm đổ vào ComboBox
        public DataTable GetDanhSachSanPham()
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaSP, TenSP, GiaBan FROM SanPham WHERE IsActive = 1", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 5. Hàm lưu Báo Giá và Chi Tiết (Dùng Transaction)
        public bool LuuBaoGiaMoi(string maKH, DateTime ngayHetHan, string ghiChu, decimal tongTien, DataTable chiTietSP)
        {
            string maBG = "BG" + DateTime.Now.ToString("yyMMddHHmmss");

            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Lưu bảng BaoGia
                        string sqlBG = "INSERT INTO BaoGia (MaBG, MaKH, NgayLap, NgayHetHan, TongTien, GhiChu, TrangThai) " +
                                       "VALUES (@maBG, @maKH, GETDATE(), @ngayHH, @tong, @ghiChu, N'Mới lập')";
                        SqlCommand cmdBG = new SqlCommand(sqlBG, conn, trans);
                        cmdBG.Parameters.AddWithValue("@maBG", maBG);
                        cmdBG.Parameters.AddWithValue("@maKH", maKH);
                        cmdBG.Parameters.AddWithValue("@ngayHH", ngayHetHan);
                        cmdBG.Parameters.AddWithValue("@tong", tongTien);
                        cmdBG.Parameters.AddWithValue("@ghiChu", ghiChu);
                        cmdBG.ExecuteNonQuery();

                        // Lưu bảng ChiTietBaoGia
                        string sqlCT = "INSERT INTO ChiTietBaoGia (MaBG, MaSP, SoLuong, DonGia, ThanhTien) " +
                                       "VALUES (@maBG, @maSP, @sl, @gia, @thanhTien)";
                        foreach (DataRow row in chiTietSP.Rows)
                        {
                            SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                            cmdCT.Parameters.AddWithValue("@maBG", maBG);
                            cmdCT.Parameters.AddWithValue("@maSP", row["MaSP"].ToString());
                            cmdCT.Parameters.AddWithValue("@sl", Convert.ToInt32(row["SoLuong"]));
                            cmdCT.Parameters.AddWithValue("@gia", Convert.ToDecimal(row["DonGia"]));
                            cmdCT.Parameters.AddWithValue("@thanhTien", Convert.ToDecimal(row["ThanhTien"]));
                            cmdCT.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}