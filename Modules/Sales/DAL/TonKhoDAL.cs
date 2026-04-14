using System.Data;
using System.Data.SqlClient;

namespace SharkTank.Modules.Sales.DAL
{
    public class TonKhoDAL
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True";

        public DataTable GetDanhSachTonKho(string tuKhoa = "")
        {
            string query = @"
                SELECT 
                    SP.MaSP AS [Mã SP],
                    SP.TenSP AS [Tên Sản Phẩm],
                    SP.DonViTinh AS [ĐVT],
                    SP.GiaBan AS [Giá Bán],
                    (ISNULL(NK.TongNhap, 0) - ISNULL(XK.TongXuat, 0)) AS [Số Lượng Tồn]
                FROM SanPham SP
                LEFT JOIN (
                    SELECT MaSP, SUM(SoLuong) AS TongNhap FROM NhapKho GROUP BY MaSP
                ) NK ON SP.MaSP = NK.MaSP
                LEFT JOIN (
                    SELECT MaSP, SUM(SoLuong) AS TongXuat FROM XuatKho GROUP BY MaSP
                ) XK ON SP.MaSP = XK.MaSP
                WHERE SP.IsActive = 1";

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query += $" AND (SP.TenSP LIKE N'%{tuKhoa}%' OR SP.MaSP LIKE '%{tuKhoa}%')";
            }

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
}