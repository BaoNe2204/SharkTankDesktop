using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SharkTank.Core.Models;

namespace SharkTank.DAL.Sql
{
    public class SqlRoleRepository : IRoleRepository
    {
        public Role GetById(int roleId)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT TOP 1 RoleId, RoleName, [Description], IsSystemRole
FROM dbo.Roles
WHERE RoleId = @RoleId;";
                cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = roleId });

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new Role
                    {
                        RoleId = r.GetInt32(0),
                        RoleName = r.GetString(1),
                        Description = r.IsDBNull(2) ? null : r.GetString(2),
                        IsSystemRole = r.GetBoolean(3)
                    };
                }
            }
        }

        public IEnumerable<Role> GetAll()
        {
            var list = new List<Role>();
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT RoleId, RoleName, [Description], IsSystemRole
FROM dbo.Roles;";

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Role
                        {
                            RoleId = r.GetInt32(0),
                            RoleName = r.GetString(1),
                            Description = r.IsDBNull(2) ? null : r.GetString(2),
                            IsSystemRole = r.GetBoolean(3)
                        });
                    }
                }
            }

            return list;
        }
    }
}

