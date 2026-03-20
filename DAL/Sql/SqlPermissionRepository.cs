using System;
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
                        list.Add(MapPermission(r));
                    }
                }
            }

            return list;
        }

        public IEnumerable<Permission> GetByUserId(int userId)
        {
            var list = new List<Permission>();

            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT p.PermissionId, p.PermissionCode, p.PermissionName, p.Module
FROM dbo.UserPermissions up
JOIN dbo.Permissions p ON p.PermissionId = up.PermissionId
WHERE up.UserId = @UserId;";
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(MapPermission(r));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Nếu DB chưa có bảng UserPermissions thì fallback về role-based trong PermissionService
                System.Diagnostics.Debug.WriteLine($"GetByUserId error: {ex.Message}");
            }

            return list;
        }

        private static Permission MapPermission(SqlDataReader r)
        {
            return new Permission
            {
                PermissionId = r.GetInt32(0),
                PermissionCode = r.GetString(1),
                PermissionName = r.IsDBNull(2) ? null : r.GetString(2),
                Module = r.IsDBNull(3) ? null : r.GetString(3)
            };
        }
    }
}
