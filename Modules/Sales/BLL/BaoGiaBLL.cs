using System;
using System.Data;
using System.Data.SqlClient;

namespace SharkTankDesktop.Modules.Sales.BLL
{
    public class BaoGiaBLL
    {
        // Chuỗi kết nối Database
        private string strCon = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True;TrustServerCertificate=True";


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
                        string sqlBG = "INSERT INTO BaoGia (MaBG, MaKH, NgayLap, NgayHetHan, TongTien, GhiChu, TrangThai) " +
                                       "VALUES (@maBG, @maKH, GETDATE(), @ngayHH, @tong, @ghiChu, N'Mới lập')";
                        SqlCommand cmdBG = new SqlCommand(sqlBG, conn, trans);
                        cmdBG.Parameters.AddWithValue("@maBG", maBG);
                        cmdBG.Parameters.AddWithValue("@maKH", maKH);
                        cmdBG.Parameters.AddWithValue("@ngayHH", ngayHetHan);
                        cmdBG.Parameters.AddWithValue("@tong", tongTien);
                        cmdBG.Parameters.AddWithValue("@ghiChu", ghiChu);
                        cmdBG.ExecuteNonQuery();

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