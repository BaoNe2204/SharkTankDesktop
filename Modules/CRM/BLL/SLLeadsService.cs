using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SharkTank.Modules.CRM.Services
{
    public class LeadService 
    {
        // 🔥 Connection string đúng
        private string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True";

        // =====================================================
        // 📊 1. LẤY LEAD THEO NGÀY
        // =====================================================
        public DataTable GetLeadByDate(DateTime from, DateTime to)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        CONVERT(VARCHAR(10), NgayTao, 120) AS Ngay,
                        COUNT(*) AS SoLuong
                    FROM Leads
                    WHERE NgayTao BETWEEN @from AND @to
                    GROUP BY CONVERT(VARCHAR(10), NgayTao, 120)
                    ORDER BY Ngay";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // =====================================================
        // 📊 2. TỔNG LEAD
        // =====================================================
        public int GetTotal(DateTime from, DateTime to)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Leads WHERE NgayTao BETWEEN @from AND @to";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // =====================================================
        // 📊 3. LEAD HÔM NAY
        // =====================================================
        public int GetToday()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Leads 
                    WHERE CONVERT(DATE, NgayTao) = CONVERT(DATE, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // =====================================================
        // 📊 4. TĂNG TRƯỞNG
        // =====================================================
        public double GetGrowth(DateTime from, DateTime to)
        {
            TimeSpan range = to - from;

            // 🔥 kỳ trước
            DateTime prevFrom = from - range;
            DateTime prevTo = from;

            int current = GetTotal(from, to);
            int previous = GetTotal(prevFrom, prevTo);

            if (previous == 0) return current > 0 ? 100 : 0;

            return ((double)(current - previous) / previous) * 100;
        }

        // =====================================================
        // 📊 5. LẤY TOÀN BỘ LEAD
        // =====================================================
        public DataTable GetAllLeads()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Leads ORDER BY NgayTao DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}