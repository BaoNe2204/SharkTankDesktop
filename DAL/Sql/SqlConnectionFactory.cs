using System.Configuration;
using System.Data.SqlClient;

namespace SharkTank.DAL.Sql
{
    public static class SqlConnectionFactory
    {
        public static SqlConnection Create()
        {
            var cs = ConfigurationManager.ConnectionStrings["SharkTankDB"]?.ConnectionString;
            return new SqlConnection(cs);
        }

        public static bool HasConnectionString()
        {
            var cs = ConfigurationManager.ConnectionStrings["SharkTankDB"]?.ConnectionString;
            return !string.IsNullOrWhiteSpace(cs);
        }
    }
}

