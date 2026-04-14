using System;
using System.Data;
using System.Data.SqlClient;

namespace SharkTank.Modules.Sales.DAL
{
    public class DoanhThu_DAL
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True";

        public DataTable GetDoanhThuTheoNgay()
        {
            string query = @"
                SELECT 
                    CAST(NgayLap AS DATE) AS [Ngày], 
                    COUNT(MaHD) AS [Số Lượng Đơn], 
                    SUM(TongTien) AS [Doanh Thu]
                FROM HoaDon
                GROUP BY CAST(NgayLap AS DATE)
                ORDER BY [Ngày] DESC";
            return ExecuteQuery(query);
        }

        public DataTable GetDoanhThuTheoNhanVien()
        {
            string query = @"
                SELECT 
                    NV.HoTen AS [Tên Nhân Viên], 
                    COUNT(HD.MaHD) AS [Số Đơn Đã Bán], 
                    SUM(HD.TongTien) AS [Tổng Doanh Thu]
                FROM HoaDon HD
                JOIN NhanVien NV ON HD.NhanVienId = NV.NhanVienId
                GROUP BY NV.HoTen
                ORDER BY [Tổng Doanh Thu] DESC";

            return ExecuteQuery(query);
        }

        public DataTable GetDoanhThuTheoSanPham()
        {
            string query = @"
                SELECT 
                    SP.TenSP AS [Tên Sản Phẩm], 
                    SUM(CT.SoLuong) AS [Số Lượng Đã Bán], 
                    SUM(CT.SoLuong * CT.DonGia) AS [Tổng Thu]
                FROM ChiTietHoaDon CT
                JOIN SanPham SP ON CT.MaSP = SP.MaSP
                GROUP BY SP.TenSP
                ORDER BY [Số Lượng Đã Bán] DESC";
            return ExecuteQuery(query);
        }

        private DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}