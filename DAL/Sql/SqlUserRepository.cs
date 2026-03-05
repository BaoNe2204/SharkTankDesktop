using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SharkTank.Core.Models;

namespace SharkTank.DAL.Sql
{
    public class SqlUserRepository : IUserRepository
    {
        public User GetByUsername(string username)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT TOP 1
  UserId, Username, PasswordHash, PasswordSalt, FullName, Email, Phone,
  RoleId, IsActive, IsLocked, FailedLoginAttempts, LastLoginAt, CreatedAt, UpdatedAt
FROM dbo.Users
WHERE Username = @Username;";
                cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 100) { Value = username });

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return MapUser(r);
                }
            }
        }

        public User GetById(int userId)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT TOP 1
  UserId, Username, PasswordHash, PasswordSalt, FullName, Email, Phone,
  RoleId, IsActive, IsLocked, FailedLoginAttempts, LastLoginAt, CreatedAt, UpdatedAt
FROM dbo.Users
WHERE UserId = @UserId;";
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return MapUser(r);
                }
            }
        }

        public void Update(User user)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
UPDATE dbo.Users
SET
  PasswordHash = @PasswordHash,
  PasswordSalt = @PasswordSalt,
  FullName = @FullName,
  Email = @Email,
  Phone = @Phone,
  RoleId = @RoleId,
  IsActive = @IsActive,
  IsLocked = @IsLocked,
  FailedLoginAttempts = @FailedLoginAttempts,
  LastLoginAt = @LastLoginAt,
  UpdatedAt = @UpdatedAt
WHERE UserId = @UserId;";

                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = user.UserId });
                cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 128) { Value = (object)user.PasswordHash ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@PasswordSalt", SqlDbType.NVarChar, 64) { Value = (object)user.PasswordSalt ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 200) { Value = (object)user.FullName ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200) { Value = (object)user.Email ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 50) { Value = (object)user.Phone ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = user.RoleId });
                cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = user.IsActive });
                cmd.Parameters.Add(new SqlParameter("@IsLocked", SqlDbType.Bit) { Value = user.IsLocked });
                cmd.Parameters.Add(new SqlParameter("@FailedLoginAttempts", SqlDbType.Int) { Value = user.FailedLoginAttempts });
                cmd.Parameters.Add(new SqlParameter("@LastLoginAt", SqlDbType.DateTime) { Value = (object)user.LastLoginAt ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@UpdatedAt", SqlDbType.DateTime) { Value = user.UpdatedAt });

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<User> GetAll()
        {
            var list = new List<User>();
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT
  UserId, Username, PasswordHash, PasswordSalt, FullName, Email, Phone,
  RoleId, IsActive, IsLocked, FailedLoginAttempts, LastLoginAt, CreatedAt, UpdatedAt
FROM dbo.Users;";

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(MapUser(r));
                    }
                }
            }
            return list;
        }

        private static User MapUser(SqlDataReader r)
        {
            return new User
            {
                UserId = r.GetInt32(r.GetOrdinal("UserId")),
                Username = r.GetString(r.GetOrdinal("Username")),
                PasswordHash = r.GetString(r.GetOrdinal("PasswordHash")),
                PasswordSalt = r.GetString(r.GetOrdinal("PasswordSalt")),
                FullName = r.IsDBNull(r.GetOrdinal("FullName")) ? null : r.GetString(r.GetOrdinal("FullName")),
                Email = r.IsDBNull(r.GetOrdinal("Email")) ? null : r.GetString(r.GetOrdinal("Email")),
                Phone = r.IsDBNull(r.GetOrdinal("Phone")) ? null : r.GetString(r.GetOrdinal("Phone")),
                RoleId = r.GetInt32(r.GetOrdinal("RoleId")),
                IsActive = r.GetBoolean(r.GetOrdinal("IsActive")),
                IsLocked = r.GetBoolean(r.GetOrdinal("IsLocked")),
                FailedLoginAttempts = r.GetInt32(r.GetOrdinal("FailedLoginAttempts")),
                LastLoginAt = r.IsDBNull(r.GetOrdinal("LastLoginAt")) ? (DateTime?)null : r.GetDateTime(r.GetOrdinal("LastLoginAt")),
                CreatedAt = r.GetDateTime(r.GetOrdinal("CreatedAt")),
                UpdatedAt = r.GetDateTime(r.GetOrdinal("UpdatedAt"))
            };
        }
    }
}

