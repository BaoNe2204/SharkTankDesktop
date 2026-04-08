using System;
using System.Data;
using System.Data.SqlClient;

namespace SharkTank.Modules.Sales.BLL
{
    public class HoaDonBLL
    {
        private string strCon = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True;TrustServerCertificate=True";

        // 1. Lấy danh sách toàn bộ Hóa Đơn
        public DataTable GetDanhSachHoaDon()
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string sql = @"
                    SELECT 
                        hd.MaHD AS [Mã Hóa Đơn], 
                        kh.HoTen AS [Tên Khách Hàng], 
                        hd.NgayLap AS [Ngày Lập], 
                        hd.TongTien AS [Tổng Tiền], 
                        hd.DaThanhToan AS [Đã Thanh Toán],
                        (hd.TongTien - hd.DaThanhToan) AS [Còn Nợ],
                        hd.TrangThai AS [Trạng Thái], 
                        hd.GhiChu AS [Ghi Chú]
                    FROM HoaDon hd
                    JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                    ORDER BY hd.NgayLap DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

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

        public bool LuuHoaDonMoi(string maKH, DateTime ngayLap, string ghiChu, decimal tongTien, decimal daThanhToan, DataTable chiTietSP)
        {
            string maHD = "HD" + DateTime.Now.ToString("yyMMddHHmmss");

            string trangThai = (daThanhToan >= tongTien) ? "Đã thanh toán" : "Đang nợ";

            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlHD = @"INSERT INTO HoaDon (MaHD, MaKH, NgayLap, TongTien, DaThanhToan, GhiChu, TrangThai) 
                                       VALUES (@maHD, @maKH, @ngayLap, @tongTien, @daThanhToan, @ghiChu, @trangThai)";
                        SqlCommand cmdHD = new SqlCommand(sqlHD, conn, trans);
                        cmdHD.Parameters.AddWithValue("@maHD", maHD);
                        cmdHD.Parameters.AddWithValue("@maKH", maKH);
                        cmdHD.Parameters.AddWithValue("@ngayLap", ngayLap);
                        cmdHD.Parameters.AddWithValue("@tongTien", tongTien);
                        cmdHD.Parameters.AddWithValue("@daThanhToan", daThanhToan);
                        cmdHD.Parameters.AddWithValue("@ghiChu", ghiChu);
                        cmdHD.Parameters.AddWithValue("@trangThai", trangThai);
                        cmdHD.ExecuteNonQuery();

                        // Bước 2: Lưu từng sản phẩm vào bảng ChiTietHoaDon
                        string sqlCT = @"INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia, ThanhTien) 
                                       VALUES (@maHD, @maSP, @soLuong, @donGia, @thanhTien)";
                        foreach (DataRow row in chiTietSP.Rows)
                        {
                            SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                            cmdCT.Parameters.AddWithValue("@maHD", maHD);
                            cmdCT.Parameters.AddWithValue("@maSP", row["MaSP"].ToString());
                            cmdCT.Parameters.AddWithValue("@soLuong", Convert.ToInt32(row["SoLuong"]));
                            cmdCT.Parameters.AddWithValue("@donGia", Convert.ToDecimal(row["DonGia"]));
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