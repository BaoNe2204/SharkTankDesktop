using System;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.BLL
{
    /// <summary>
    /// Helper để set CONTEXT_INFO cho SQL Triggers.
    /// Gọi hàm này TRƯỚC KHI thực hiện INSERT/UPDATE/DELETE để trigger ghi được username.
    /// </summary>
    public static class AuditContext
    {
        /// <summary>
        /// Set username vào SQL CONTEXT_INFO để trigger có thể đọc được.
        /// Gọi TRƯỚC KHI thực hiện thao tác database.
        /// </summary>
        public static void SetUsername(string username)
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    // Pad với spaces để đủ 128 bytes
                    string padded = (username ?? "").PadRight(100, ' ');
                    byte[] bytes = new byte[128];
                    System.Text.Encoding.Unicode.GetBytes(padded, 0, Math.Min(100, padded.Length), bytes, 0);
                    SqlConnection.ClearAllPools();
                    using (var cmd = new SqlCommand("SET CONTEXT_INFO @info", conn))
                    {
                        cmd.Parameters.AddWithValue("@info", bytes);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Xóa CONTEXT_INFO (gọi sau khi xong thao tác).
        /// </summary>
        public static void Clear()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    SqlConnection.ClearAllPools();
                    using (var cmd = new SqlCommand("SET CONTEXT_INFO 0x", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Helper: Thực hiện action với username được set tự động.
        /// </summary>
        public static void ExecuteWithAudit(string username, Action action)
        {
            SetUsername(username);
            try
            {
                action();
            }
            finally
            {
                Clear();
            }
        }
    }
}
