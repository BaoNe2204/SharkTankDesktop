using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SharkTank.Core.Models;

namespace SharkTank.DAL.Sql
{
    public class SqlPermissionRepository : IPermissionRepository
    {
        public IEnumerable<Permission> GetByRoleId(int roleId)
        {
            var list = new List<Permission>();
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT p.PermissionId, p.PermissionCode, p.PermissionName, p.Module
FROM dbo.RolePermissions rp
JOIN dbo.Permissions p ON p.PermissionId = rp.PermissionId
WHERE rp.RoleId = @RoleId;";
                cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = roleId });

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Permission
                        {
                            PermissionId = r.GetInt32(0),
                            PermissionCode = r.GetString(1),
                            PermissionName = r.IsDBNull(2) ? null : r.GetString(2),
                            Module = r.IsDBNull(3) ? null : r.GetString(3)
                        });
                    }
                }
            }

            return list;
        }
    }
}

