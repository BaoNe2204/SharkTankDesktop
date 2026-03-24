using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SharkTank.Core.Models;
using SharkTank.DAL.Sql;

namespace SharkTank.DAL.Sql
{
    public class SqlAuditRepository : IAuditRepository
    {
        // ==================== LOGIN HISTORY ====================

        public IEnumerable<LoginHistory> GetLoginHistory(string username = null,
            string status = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var list = new List<LoginHistory>();
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                var sql = @"
SELECT LoginHistoryId, UserId, Username, FullName, RoleName,
       LoginTime, LogoutTime, IpAddress, DeviceInfo, Status, FailureReason, CreatedAt
FROM dbo.LoginHistory
WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(username))
                {
                    sql += " AND Username LIKE @Username";
                    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 100) { Value = "%" + username + "%" });
                }
                if (!string.IsNullOrWhiteSpace(status))
                {
                    sql += " AND Status = @Status";
                    cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50) { Value = status });
                }
                if (fromDate.HasValue)
                {
                    sql += " AND LoginTime >= @FromDate";
                    cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value });
                }
                if (toDate.HasValue)
                {
                    sql += " AND LoginTime <= @ToDate";
                    cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value });
                }

                sql += " ORDER BY LoginTime DESC";
                cmd.CommandText = sql;

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(MapLoginHistory(r));
                }
            }
            return list;
        }

        public LoginHistory GetLoginHistoryById(int id)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT LoginHistoryId, UserId, Username, FullName, RoleName,
       LoginTime, LogoutTime, IpAddress, DeviceInfo, Status, FailureReason, CreatedAt
FROM dbo.LoginHistory
WHERE LoginHistoryId = @Id";
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read()) return MapLoginHistory(r);
                }
            }
            return null;
        }

        public void InsertLoginHistory(LoginHistory item)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
INSERT INTO dbo.LoginHistory
(UserId, Username, FullName, RoleName, LoginTime, LogoutTime, IpAddress, DeviceInfo, Status, FailureReason)
VALUES
(@UserId, @Username, @FullName, @RoleName, @LoginTime, @LogoutTime, @IpAddress, @DeviceInfo, @Status, @FailureReason)";
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = item.UserId });
                cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 100) { Value = (object)item.Username ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 200) { Value = (object)item.FullName ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.NVarChar, 100) { Value = (object)item.RoleName ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@LoginTime", SqlDbType.DateTime) { Value = item.LoginTime });
                cmd.Parameters.Add(new SqlParameter("@LogoutTime", SqlDbType.DateTime) { Value = (object)item.LogoutTime ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@IpAddress", SqlDbType.NVarChar, 64) { Value = (object)item.IpAddress ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@DeviceInfo", SqlDbType.NVarChar, 255) { Value = (object)item.DeviceInfo ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50) { Value = item.Status ?? "Success" });
                cmd.Parameters.Add(new SqlParameter("@FailureReason", SqlDbType.NVarChar, 255) { Value = (object)item.FailureReason ?? DBNull.Value });
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateLogoutTime(int loginHistoryId, DateTime logoutTime)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE dbo.LoginHistory SET LogoutTime=@LogoutTime WHERE LoginHistoryId=@Id";
                cmd.Parameters.Add(new SqlParameter("@LogoutTime", SqlDbType.DateTime) { Value = logoutTime });
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = loginHistoryId });
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ==================== AUDIT LOG ====================

        public IEnumerable<AuditLog> GetAuditLogs(string username = null, string action = null,
            string entityType = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var list = new List<AuditLog>();
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                var sql = @"
SELECT AuditLogId, UserId, Username, FullName, Action, EntityType, EntityId, EntityName,
       Description, IpAddress, DeviceInfo, OldValues, NewValues, Timestamp
FROM dbo.AuditLogs
WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(username))
                {
                    sql += " AND Username LIKE @Username";
                    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 100) { Value = "%" + username + "%" });
                }
                if (!string.IsNullOrWhiteSpace(action))
                {
                    sql += " AND Action = @Action";
                    cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar, 50) { Value = action });
                }
                if (!string.IsNullOrWhiteSpace(entityType))
                {
                    sql += " AND EntityType = @EntityType";
                    cmd.Parameters.Add(new SqlParameter("@EntityType", SqlDbType.NVarChar, 100) { Value = entityType });
                }
                if (fromDate.HasValue)
                {
                    sql += " AND Timestamp >= @FromDate";
                    cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value });
                }
                if (toDate.HasValue)
                {
                    sql += " AND Timestamp <= @ToDate";
                    cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value });
                }

                sql += " ORDER BY Timestamp DESC";
                cmd.CommandText = sql;

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(MapAuditLog(r));
                }
            }
            return list;
        }

        public AuditLog GetAuditLogById(int id)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT AuditLogId, UserId, Username, FullName, Action, EntityType, EntityId, EntityName,
       Description, IpAddress, DeviceInfo, OldValues, NewValues, Timestamp
FROM dbo.AuditLogs
WHERE AuditLogId = @Id";
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read()) return MapAuditLog(r);
                }
            }
            return null;
        }

        public int InsertAuditLog(AuditLog item)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
INSERT INTO dbo.AuditLogs
(UserId, Username, FullName, Action, EntityType, EntityId, EntityName, Description, IpAddress, DeviceInfo, OldValues, NewValues)
VALUES
(@UserId, @Username, @FullName, @Action, @EntityType, @EntityId, @EntityName, @Description, @IpAddress, @DeviceInfo, @OldValues, @NewValues);
SELECT SCOPE_IDENTITY();";
                cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = (object)item.UserId ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 100) { Value = (object)item.Username ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 200) { Value = (object)item.FullName ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar, 50) { Value = item.Action });
                cmd.Parameters.Add(new SqlParameter("@EntityType", SqlDbType.NVarChar, 100) { Value = item.EntityType });
                cmd.Parameters.Add(new SqlParameter("@EntityId", SqlDbType.NVarChar, 100) { Value = (object)item.EntityId ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@EntityName", SqlDbType.NVarChar, 255) { Value = (object)item.EntityName ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, -1) { Value = (object)item.Description ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@IpAddress", SqlDbType.NVarChar, 64) { Value = (object)item.IpAddress ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@DeviceInfo", SqlDbType.NVarChar, 255) { Value = (object)item.DeviceInfo ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@OldValues", SqlDbType.NVarChar, -1) { Value = (object)item.OldValues ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@NewValues", SqlDbType.NVarChar, -1) { Value = (object)item.NewValues ?? DBNull.Value });
                conn.Open();
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public void DeleteAuditLog(int id)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE dbo.AuditLogs WHERE AuditLogId = @Id";
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteOldAuditLogs(int keepDays)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE dbo.AuditLogs WHERE Timestamp < DATEADD(day, -@Days, GETDATE())";
                cmd.Parameters.Add(new SqlParameter("@Days", SqlDbType.Int) { Value = keepDays });
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ==================== DATA CHANGE LOG ====================

        public IEnumerable<DataChangeLog> GetDataChangeLogs(int? auditLogId = null,
            string tableName = null, string recordId = null,
            DateTime? fromDate = null, DateTime? toDate = null)
        {
            var list = new List<DataChangeLog>();
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                var sql = @"
SELECT ChangeLogId, AuditLogId, TableName, RecordId, FieldName,
       OldValue, NewValue, ChangeType, ChangeTime
FROM dbo.DataChangeLogs
WHERE 1=1";

                if (auditLogId.HasValue)
                {
                    sql += " AND AuditLogId = @AuditLogId";
                    cmd.Parameters.Add(new SqlParameter("@AuditLogId", SqlDbType.Int) { Value = auditLogId.Value });
                }
                if (!string.IsNullOrWhiteSpace(tableName))
                {
                    sql += " AND TableName = @TableName";
                    cmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 128) { Value = tableName });
                }
                if (!string.IsNullOrWhiteSpace(recordId))
                {
                    sql += " AND RecordId = @RecordId";
                    cmd.Parameters.Add(new SqlParameter("@RecordId", SqlDbType.NVarChar, 100) { Value = recordId });
                }
                if (fromDate.HasValue)
                {
                    sql += " AND ChangeTime >= @FromDate";
                    cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromDate.Value });
                }
                if (toDate.HasValue)
                {
                    sql += " AND ChangeTime <= @ToDate";
                    cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = toDate.Value });
                }

                sql += " ORDER BY ChangeTime DESC";
                cmd.CommandText = sql;

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(MapDataChangeLog(r));
                }
            }
            return list;
        }

        public void InsertDataChangeLog(DataChangeLog item)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
INSERT INTO dbo.DataChangeLogs
(AuditLogId, TableName, RecordId, FieldName, OldValue, NewValue, ChangeType)
VALUES
(@AuditLogId, @TableName, @RecordId, @FieldName, @OldValue, @NewValue, @ChangeType)";
                cmd.Parameters.Add(new SqlParameter("@AuditLogId", SqlDbType.Int) { Value = (object)item.AuditLogId ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 128) { Value = item.TableName });
                cmd.Parameters.Add(new SqlParameter("@RecordId", SqlDbType.NVarChar, 100) { Value = item.RecordId });
                cmd.Parameters.Add(new SqlParameter("@FieldName", SqlDbType.NVarChar, 128) { Value = item.FieldName });
                cmd.Parameters.Add(new SqlParameter("@OldValue", SqlDbType.NVarChar, -1) { Value = (object)item.OldValue ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@NewValue", SqlDbType.NVarChar, -1) { Value = (object)item.NewValue ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@ChangeType", SqlDbType.NVarChar, 20) { Value = item.ChangeType });
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertDataChangeLogs(IEnumerable<DataChangeLog> items)
        {
            if (items == null) return;
            foreach (var item in items)
                InsertDataChangeLog(item);
        }

        // ==================== MAPPERS ====================

        private static LoginHistory MapLoginHistory(SqlDataReader r)
        {
            return new LoginHistory
            {
                LoginHistoryId = r.GetInt32(r.GetOrdinal("LoginHistoryId")),
                UserId = r.GetInt32(r.GetOrdinal("UserId")),
                Username = r.IsDBNull(r.GetOrdinal("Username")) ? null : r.GetString(r.GetOrdinal("Username")),
                FullName = r.IsDBNull(r.GetOrdinal("FullName")) ? null : r.GetString(r.GetOrdinal("FullName")),
                RoleName = r.IsDBNull(r.GetOrdinal("RoleName")) ? null : r.GetString(r.GetOrdinal("RoleName")),
                LoginTime = r.GetDateTime(r.GetOrdinal("LoginTime")),
                LogoutTime = r.IsDBNull(r.GetOrdinal("LogoutTime")) ? (DateTime?)null : r.GetDateTime(r.GetOrdinal("LogoutTime")),
                IpAddress = r.IsDBNull(r.GetOrdinal("IpAddress")) ? null : r.GetString(r.GetOrdinal("IpAddress")),
                DeviceInfo = r.IsDBNull(r.GetOrdinal("DeviceInfo")) ? null : r.GetString(r.GetOrdinal("DeviceInfo")),
                Status = r.GetString(r.GetOrdinal("Status")),
                FailureReason = r.IsDBNull(r.GetOrdinal("FailureReason")) ? null : r.GetString(r.GetOrdinal("FailureReason")),
                CreatedAt = r.GetDateTime(r.GetOrdinal("CreatedAt"))
            };
        }

        private static AuditLog MapAuditLog(SqlDataReader r)
        {
            return new AuditLog
            {
                AuditLogId = r.GetInt32(r.GetOrdinal("AuditLogId")),
                UserId = r.IsDBNull(r.GetOrdinal("UserId")) ? (int?)null : r.GetInt32(r.GetOrdinal("UserId")),
                Username = r.IsDBNull(r.GetOrdinal("Username")) ? null : r.GetString(r.GetOrdinal("Username")),
                FullName = r.IsDBNull(r.GetOrdinal("FullName")) ? null : r.GetString(r.GetOrdinal("FullName")),
                Action = r.GetString(r.GetOrdinal("Action")),
                EntityType = r.GetString(r.GetOrdinal("EntityType")),
                EntityId = r.IsDBNull(r.GetOrdinal("EntityId")) ? null : r.GetString(r.GetOrdinal("EntityId")),
                EntityName = r.IsDBNull(r.GetOrdinal("EntityName")) ? null : r.GetString(r.GetOrdinal("EntityName")),
                Description = r.IsDBNull(r.GetOrdinal("Description")) ? null : r.GetString(r.GetOrdinal("Description")),
                IpAddress = r.IsDBNull(r.GetOrdinal("IpAddress")) ? null : r.GetString(r.GetOrdinal("IpAddress")),
                DeviceInfo = r.IsDBNull(r.GetOrdinal("DeviceInfo")) ? null : r.GetString(r.GetOrdinal("DeviceInfo")),
                OldValues = r.IsDBNull(r.GetOrdinal("OldValues")) ? null : r.GetString(r.GetOrdinal("OldValues")),
                NewValues = r.IsDBNull(r.GetOrdinal("NewValues")) ? null : r.GetString(r.GetOrdinal("NewValues")),
                Timestamp = r.GetDateTime(r.GetOrdinal("Timestamp"))
            };
        }

        private static DataChangeLog MapDataChangeLog(SqlDataReader r)
        {
            return new DataChangeLog
            {
                ChangeLogId = r.GetInt32(r.GetOrdinal("ChangeLogId")),
                AuditLogId = r.IsDBNull(r.GetOrdinal("AuditLogId")) ? (int?)null : r.GetInt32(r.GetOrdinal("AuditLogId")),
                TableName = r.GetString(r.GetOrdinal("TableName")),
                RecordId = r.GetString(r.GetOrdinal("RecordId")),
                FieldName = r.GetString(r.GetOrdinal("FieldName")),
                OldValue = r.IsDBNull(r.GetOrdinal("OldValue")) ? null : r.GetString(r.GetOrdinal("OldValue")),
                NewValue = r.IsDBNull(r.GetOrdinal("NewValue")) ? null : r.GetString(r.GetOrdinal("NewValue")),
                ChangeType = r.GetString(r.GetOrdinal("ChangeType")),
                ChangeTime = r.GetDateTime(r.GetOrdinal("ChangeTime"))
            };
        }
    }
}
