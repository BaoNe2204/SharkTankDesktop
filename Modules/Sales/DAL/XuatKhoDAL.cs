using System;
using System.Data;
using System.Data.SqlClient;

namespace SharkTank.Modules.Sales.DAL
{
    public class XuatKhoDAL
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True";

        public bool LuuPhieuXuatKho(string phieuXuat, string maKho, DataTable dtChiTiet)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string query = @"INSERT INTO dbo.XuatKho (PhieuXuat, MaKho, MaSP, SoLuong, GiaXuat, NgayXuat) 
                                     VALUES (@PhieuXuat, @MaKho, @MaSP, @SoLuong, @GiaXuat, GETDATE())";

                    foreach (DataRow row in dtChiTiet.Rows)
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@PhieuXuat", phieuXuat);
                            cmd.Parameters.AddWithValue("@MaKho", maKho);
                            cmd.Parameters.AddWithValue("@MaSP", row["Mã SP"].ToString());
                            cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row["Số Lượng"]));
                            cmd.Parameters.AddWithValue("@GiaXuat", Convert.ToDecimal(row["Giá Xuất"]));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}