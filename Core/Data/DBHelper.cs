using System.Configuration;
using System.Data.SqlClient;

namespace SharkTank.Core.Data
{
    public static class DBHelper
    {
        public static SqlConnection GetConnection()
        {
            string connStr = ConfigurationManager
                .ConnectionStrings["SharkTankDB"]
                .ConnectionString;

            return new SqlConnection(connStr);
        }
    }
}