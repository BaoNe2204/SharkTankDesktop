using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SharkTank.Core.Models;
using SharkTank.DAL.Sql;
using SharkTank.Core.Security;

namespace SharkTank.BLL
{
    /// <summary>
    /// Service for Admin operations - User management, sessions, audit, backup
    /// </summary>
    public class AdminService
    {
        private readonly SqlUserRepository _userRepo;
        private readonly SqlRoleRepository _roleRepo;
        private readonly SqlPermissionRepository _permRepo;

        public AdminService()
        {
            _userRepo = new SqlUserRepository();
            _roleRepo = new SqlRoleRepository();
            _permRepo = new SqlPermissionRepository();
        }

        #region User Management

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT u.UserId, u.Username, u.FullName, u.Email, u.Phone, u.RoleId, u.Department,
       u.IsActive, u.IsLocked, u.FailedLoginAttempts, u.LastLoginAt, u.LastPasswordResetAt,
       u.CreatedAt, u.UpdatedAt, r.RoleName
FROM dbo.Users u
LEFT JOIN dbo.Roles r ON u.RoleId = r.RoleId
ORDER BY u.UserId;";

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            users.Add(MapUserWithRole(r));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllUsers error: {ex.Message}");
            }
            return users;
        }

        public User GetUserById(int userId)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT u.UserId, u.Username, u.FullName, u.Email, u.Phone, u.RoleId, u.Department,
       u.IsActive, u.IsLocked, u.FailedLoginAttempts, u.LastLoginAt, u.LastPasswordResetAt,
       u.CreatedAt, u.UpdatedAt, r.RoleName
FROM dbo.Users u
LEFT JOIN dbo.Roles r ON u.RoleId = r.RoleId
WHERE u.UserId = @UserId;";
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                            return MapUserWithRole(r);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetUserById error: {ex.Message}");
            }
            return null;
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT u.UserId, u.Username, u.FullName, u.Email, u.Phone, u.RoleId, u.Department,
       u.IsActive, u.IsLocked, u.FailedLoginAttempts, u.LastLoginAt, u.LastPasswordResetAt,
       u.CreatedAt, u.UpdatedAt, r.RoleName
FROM dbo.Users u
LEFT JOIN dbo.Roles r ON u.RoleId = r.RoleId
WHERE u.Username = @Username;";
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                            return MapUserWithRole(r);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetUserByUsername error: {ex.Message}");
            }
            return null;
        }

        public bool CreateUser(User user, string plainPassword)
        {
            try
            {
                var salt = PasswordHasher.GenerateSalt();
                var hash = PasswordHasher.HashPassword(plainPassword, salt);

                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
INSERT INTO dbo.Users (Username, PasswordHash, PasswordSalt, FullName, Email, Phone, RoleId, Department, IsActive, IsLocked, FailedLoginAttempts, CreatedAt, UpdatedAt)
VALUES (@Username, @PasswordHash, @PasswordSalt, @FullName, @Email, @Phone, @RoleId, @Department, 1, 0, 0, GETDATE(), GETDATE());
SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 100) { Value = user.Username });
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 128) { Value = hash });
                    cmd.Parameters.Add(new SqlParameter("@PasswordSalt", SqlDbType.NVarChar, 64) { Value = salt });
                    cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 200) { Value = (object)user.FullName ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200) { Value = (object)user.Email ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 50) { Value = (object)user.Phone ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = user.RoleId });
                    cmd.Parameters.Add(new SqlParameter("@Department", SqlDbType.NVarChar, 100) { Value = (object)user.Department ?? DBNull.Value });

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return result != null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CreateUser error: {ex.Message}");
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
UPDATE dbo.Users
SET FullName = @FullName, Email = @Email, Phone = @Phone, RoleId = @RoleId, Department = @Department,
    IsActive = @IsActive, IsLocked = @IsLocked, UpdatedAt = GETDATE()
WHERE UserId = @UserId;";

                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = user.UserId });
                    cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 200) { Value = (object)user.FullName ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200) { Value = (object)user.Email ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 50) { Value = (object)user.Phone ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = user.RoleId });
                    cmd.Parameters.Add(new SqlParameter("@Department", SqlDbType.NVarChar, 100) { Value = (object)user.Department ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = user.IsActive });
                    cmd.Parameters.Add(new SqlParameter("@IsLocked", SqlDbType.Bit) { Value = user.IsLocked });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateUser error: {ex.Message}");
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM dbo.Users WHERE UserId = @UserId;";
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteUser error: {ex.Message}");
                return false;
            }
        }

        public bool ResetPassword(int userId, string newPassword)
        {
            try
            {
                var salt = PasswordHasher.GenerateSalt();
                var hash = PasswordHasher.HashPassword(newPassword, salt);

                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
UPDATE dbo.Users
SET PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt, LastPasswordResetAt = GETDATE(), UpdatedAt = GETDATE()
WHERE UserId = @UserId;";

                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 128) { Value = hash });
                    cmd.Parameters.Add(new SqlParameter("@PasswordSalt", SqlDbType.NVarChar, 64) { Value = salt });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ResetPassword error: {ex.Message}");
                return false;
            }
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            try
            {
                var user = _userRepo.GetById(userId);
                if (user == null) return false;

                if (!PasswordHasher.VerifyPassword(oldPassword, user.PasswordSalt, user.PasswordHash))
                    return false;

                var salt = PasswordHasher.GenerateSalt();
                var hash = PasswordHasher.HashPassword(newPassword, salt);

                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
UPDATE dbo.Users
SET PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt, UpdatedAt = GETDATE()
WHERE UserId = @UserId;";

                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 128) { Value = hash });
                    cmd.Parameters.Add(new SqlParameter("@PasswordSalt", SqlDbType.NVarChar, 64) { Value = salt });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ChangePassword error: {ex.Message}");
                return false;
            }
        }

        public bool LockUser(int userId)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE dbo.Users SET IsLocked = 1, UpdatedAt = GETDATE() WHERE UserId = @UserId;";
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LockUser error: {ex.Message}");
                return false;
            }
        }

        public bool UnlockUser(int userId)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE dbo.Users SET IsLocked = 0, FailedLoginAttempts = 0, UpdatedAt = GETDATE() WHERE UserId = @UserId;";
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UnlockUser error: {ex.Message}");
                return false;
            }
        }

        public bool AssignRole(int userId, int roleId)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE dbo.Users SET RoleId = @RoleId, UpdatedAt = GETDATE() WHERE UserId = @UserId;";
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                    cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = roleId });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AssignRole error: {ex.Message}");
                return false;
            }
        }

        public bool AssignDepartment(int userId, string department)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE dbo.Users SET Department = @Department, UpdatedAt = GETDATE() WHERE UserId = @UserId;";
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                    cmd.Parameters.Add(new SqlParameter("@Department", SqlDbType.NVarChar, 100) { Value = department });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AssignDepartment error: {ex.Message}");
                return false;
            }
        }

        public bool SetUserActive(int userId, bool isActive)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE dbo.Users SET IsActive = @IsActive, UpdatedAt = GETDATE() WHERE UserId = @UserId;";
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = isActive });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SetUserActive error: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Role Management

        public List<Role> GetAllRoles()
        {
            var roles = new List<Role>();
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT RoleId, RoleName, Description, IsSystemRole FROM dbo.Roles ORDER BY RoleName;";

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            roles.Add(new Role
                            {
                                RoleId = r.GetInt32(0),
                                RoleName = r.GetString(1),
                                Description = r.IsDBNull(2) ? null : r.GetString(2),
                                IsSystemRole = r.GetBoolean(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllRoles error: {ex.Message}");
            }
            return roles;
        }

        public List<Permission> GetAllPermissions()
        {
            var permissions = new List<Permission>();
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT PermissionId, PermissionCode, PermissionName, [Module] FROM dbo.Permissions ORDER BY [Module], PermissionName;";

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            permissions.Add(new Permission
                            {
                                PermissionId = r.GetInt32(0),
                                PermissionCode = r.GetString(1),
                                PermissionName = r.IsDBNull(2) ? null : r.GetString(2),
                                Module = r.IsDBNull(3) ? null : r.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllPermissions error: {ex.Message}");
            }
            return permissions;
        }

        public List<int> GetPermissionIdsByRole(int roleId)
        {
            var permissionIds = new List<int>();
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT PermissionId FROM dbo.RolePermissions WHERE RoleId = @RoleId;";
                    cmd.Parameters.AddWithValue("@RoleId", roleId);

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            permissionIds.Add(r.GetInt32(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetPermissionIdsByRole error: {ex.Message}");
            }
            return permissionIds;
        }

        public bool SavePermissionsForRole(int roleId, List<int> permissionIds)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = "DELETE FROM dbo.RolePermissions WHERE RoleId = @RoleId;";
                            cmd.Parameters.AddWithValue("@RoleId", roleId);
                            cmd.ExecuteNonQuery();
                        }

                        foreach (var permId in permissionIds)
                        {
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = trans;
                                cmd.CommandText = "INSERT INTO dbo.RolePermissions (RoleId, PermissionId) VALUES (@RoleId, @PermissionId);";
                                cmd.Parameters.AddWithValue("@RoleId", roleId);
                                cmd.Parameters.AddWithValue("@PermissionId", permId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SavePermissionsForRole error: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Session Management

        public List<LoginSession> GetActiveSessions()
        {
            var sessions = new List<LoginSession>();
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT s.SessionId, s.UserId, s.LoginTime, s.LogoutTime, s.IpAddress, s.DeviceInfo, s.IsActive, u.Username
FROM dbo.LoginSessions s
JOIN dbo.Users u ON s.UserId = u.UserId
ORDER BY s.LoginTime DESC;";

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            sessions.Add(new LoginSession
                            {
                                SessionId = r.GetGuid(0),
                                UserId = r.GetInt32(1),
                                LoginTime = r.GetDateTime(2),
                                LogoutTime = r.IsDBNull(3) ? (DateTime?)null : r.GetDateTime(3),
                                IpAddress = r.IsDBNull(4) ? null : r.GetString(4),
                                DeviceInfo = r.IsDBNull(5) ? null : r.GetString(5),
                                IsActive = r.GetBoolean(6)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetActiveSessions error: {ex.Message}");
            }
            return sessions;
        }

        public bool LogoutSession(Guid sessionId)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE dbo.LoginSessions SET LogoutTime = GETDATE(), IsActive = 0 WHERE SessionId = @SessionId;";
                    cmd.Parameters.Add(new SqlParameter("@SessionId", SqlDbType.UniqueIdentifier) { Value = sessionId });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LogoutSession error: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Audit Log

        public List<AuditLogEntry> GetLoginHistory(int limit = 50)
        {
            var entries = new List<AuditLogEntry>();
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
SELECT TOP {limit} s.SessionId, s.UserId, s.LoginTime, s.LogoutTime, s.IpAddress, s.DeviceInfo, s.IsActive, u.Username
FROM dbo.LoginSessions s
JOIN dbo.Users u ON s.UserId = u.UserId
ORDER BY s.LoginTime DESC;";

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            entries.Add(new AuditLogEntry
                            {
                                Time = r.GetDateTime(2),
                                Username = r.GetString(7),
                                Action = r.GetBoolean(6) ? "Đăng nhập" : "Đăng xuất",
                                Details = $"IP: {(r.IsDBNull(4) ? "N/A" : r.GetString(4))}, Thiết bị: {(r.IsDBNull(5) ? "N/A" : r.GetString(5))}"
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetLoginHistory error: {ex.Message}");
            }
            return entries;
        }

        public List<AuditLogEntry> GetActionHistory(int limit = 50)
        {
            var entries = new List<AuditLogEntry>();
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
SELECT TOP {limit} LoginTime, UserId, IpAddress, DeviceInfo
FROM dbo.LoginSessions
WHERE IsActive = 0
ORDER BY LogoutTime DESC;";

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            entries.Add(new AuditLogEntry
                            {
                                Time = r.GetDateTime(0),
                                Username = $"User #{r.GetInt32(1)}",
                                Action = "Thao tác",
                                Details = "Xem dashboard"
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetActionHistory error: {ex.Message}");
            }
            return entries;
        }

        #endregion

        #region Backup & Restore

        public bool BackupDatabase(string filePath)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"BACKUP DATABASE SharkTankDb TO DISK = '{filePath}' WITH FORMAT, MEDIANAME = 'SQLServerBackups', NAME = 'Full Backup of SharkTankDb';";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BackupDatabase error: {ex.Message}");
                return false;
            }
        }

        public bool RestoreDatabase(string filePath)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"RESTORE DATABASE SharkTankDb FROM DISK = '{filePath}' WITH REPLACE;";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RestoreDatabase error: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region System Config

        public Dictionary<string, string> GetSystemConfig()
        {
            var config = new Dictionary<string, string>();
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT ConfigKey, ConfigValue FROM dbo.SystemConfig;";

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            config[r.GetString(0)] = r.IsDBNull(1) ? "" : r.GetString(1);
                        }
                    }
                }
            }
            catch
            {
                // Table may not exist, return defaults
                config["CompanyName"] = "Công ty TNHH SharkTank";
                config["CompanyAddress"] = "123 Đường ABC, Quận 1, TP.HCM";
                config["CompanyPhone"] = "028 1234 5678";
                config["CompanyEmail"] = "contact@sharktank.com";
                config["TaxCode"] = "0123456789";
                config["Currency"] = "VND";
                config["DateFormat"] = "dd/MM/yyyy";
                config["TimeZone"] = "UTC+07:00";
            }
            return config;
        }

        public bool SaveSystemConfig(Dictionary<string, string> config)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    foreach (var kvp in config)
                    {
                        cmd.CommandText = @"
IF EXISTS (SELECT 1 FROM dbo.SystemConfig WHERE ConfigKey = @Key)
    UPDATE dbo.SystemConfig SET ConfigValue = @Value WHERE ConfigKey = @Key
ELSE
    INSERT INTO dbo.SystemConfig (ConfigKey, ConfigValue) VALUES (@Key, @Value);";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@Key", SqlDbType.NVarChar, 100) { Value = kvp.Key });
                        cmd.Parameters.Add(new SqlParameter("@Value", SqlDbType.NVarChar, 500) { Value = kvp.Value });
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SaveSystemConfig error: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Helpers

        private User MapUserWithRole(SqlDataReader r)
        {
            return new User
            {
                UserId = r.GetInt32(r.GetOrdinal("UserId")),
                Username = r.GetString(r.GetOrdinal("Username")),
                FullName = r.IsDBNull(r.GetOrdinal("FullName")) ? null : r.GetString(r.GetOrdinal("FullName")),
                Email = r.IsDBNull(r.GetOrdinal("Email")) ? null : r.GetString(r.GetOrdinal("Email")),
                Phone = r.IsDBNull(r.GetOrdinal("Phone")) ? null : r.GetString(r.GetOrdinal("Phone")),
                RoleId = r.GetInt32(r.GetOrdinal("RoleId")),
                Department = r.IsDBNull(r.GetOrdinal("Department")) ? null : r.GetString(r.GetOrdinal("Department")),
                IsActive = r.GetBoolean(r.GetOrdinal("IsActive")),
                IsLocked = r.GetBoolean(r.GetOrdinal("IsLocked")),
                FailedLoginAttempts = r.GetInt32(r.GetOrdinal("FailedLoginAttempts")),
                LastLoginAt = r.IsDBNull(r.GetOrdinal("LastLoginAt")) ? (DateTime?)null : r.GetDateTime(r.GetOrdinal("LastLoginAt")),
                LastPasswordResetAt = r.IsDBNull(r.GetOrdinal("LastPasswordResetAt")) ? (DateTime?)null : r.GetDateTime(r.GetOrdinal("LastPasswordResetAt")),
                CreatedAt = r.GetDateTime(r.GetOrdinal("CreatedAt")),
                UpdatedAt = r.GetDateTime(r.GetOrdinal("UpdatedAt")),
                Role = new Role { RoleName = r.IsDBNull(r.GetOrdinal("RoleName")) ? "" : r.GetString(r.GetOrdinal("RoleName")) }
            };
        }

        #endregion
    }

    public class AuditLogEntry
    {
        public DateTime Time { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
    }
}
