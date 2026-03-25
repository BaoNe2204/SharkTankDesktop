using System;
using System.Data;
using System.Data.SqlClient;

namespace SharkTank.Modules.CRM.BLL
{
    public class TyLeChuyenDoiService
    {
        private readonly string connStr =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True";

        public (int tongLead, int daChot, double tyLe) GetTyLeChuyenDoi()
        {
            int tongLead = 0;
            int daChot = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();

                    cmd.CommandText = @"
                        SELECT 
                            COUNT(*) AS TongLead,
                            SUM(CASE WHEN TrangThai = N'KhachHang' THEN 1 ELSE 0 END) AS KhachHang
                        FROM leads";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tongLead = reader["TongLead"] != DBNull.Value
                                ? Convert.ToInt32(reader["TongLead"]) : 0;

                            daChot = reader["KhachHang"] != DBNull.Value
                                ? Convert.ToInt32(reader["KhachHang"]) : 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi DB: " + ex.Message);
            }

            double tyLe = tongLead > 0 ? (double)daChot / tongLead * 100 : 0;

            return (tongLead, daChot, tyLe);
        }
    }
}