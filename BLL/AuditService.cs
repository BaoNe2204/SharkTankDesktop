using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharkTank.Core.Models;
using SharkTank.DAL;
using SharkTank.DAL.Sql;

namespace SharkTank.BLL
{
    /// <summary>
    /// Service ghi và đọc nhật ký hệ thống:
    /// - LoginHistory  (đăng nhập / đăng xuất)
    /// - AuditLog     (hành động CRUD)
    /// - DataChangeLog (thay đổi chi tiết từng trường)
    /// </summary>
    public class AuditService
    {
        private readonly IAuditRepository _repo;

        private static readonly object _inMemoryRepoLock = new object();
        private static InMemoryAuditRepository _sharedInMemoryAuditRepo;

        /// <summary>Thiết lập lambda lấy user hiện tại từ nơi gọi (tránh circular dependency).</summary>
        public static Func<User> GetCurrentUserFunc { get; set; }

        public AuditService(IAuditRepository repository)
        {
            _repo = repository;
        }

        /// <summary>
        /// Cùng logic chọn SQL / in-memory như form Admin — dùng khi ghi log đăng nhập từ LoginForm, SessionService.
        /// In-memory dùng một repository dùng chung (nếu không thì LoginForm ghi và Admin đọc hai bộ nhớ khác nhau).
        /// </summary>
        public static AuditService CreateDefault()
        {
            if (SqlConnectionFactory.HasConnectionString())
                return new AuditService(new SqlAuditRepository());

            lock (_inMemoryRepoLock)
            {
                if (_sharedInMemoryAuditRepo == null)
                    _sharedInMemoryAuditRepo = new InMemoryAuditRepository();
                return new AuditService(_sharedInMemoryAuditRepo);
            }
        }

        // ==================== LOGIN HISTORY ====================

        public void LogLogin(int userId, string username, string fullName, string roleName,
            string ipAddress, string deviceInfo, string status = "Success", string failureReason = null)
        {
            var item = new LoginHistory
            {
                UserId = userId,
                Username = username,
                FullName = fullName,
                RoleName = roleName,
                LoginTime = DateTime.Now,
                IpAddress = ipAddress,
                DeviceInfo = deviceInfo,
                Status = status,
                FailureReason = failureReason
            };
            _repo.InsertLoginHistory(item);
        }

        public void LogLoginSuccess(int userId, string username, string fullName, string roleName)
        {
            LogLogin(userId, username, fullName, roleName,
                GetIpAddress(), GetDeviceInfo(), "Success");
        }

        public void LogLoginFailed(string username, string reason)
        {
            LogLogin(0, username, null, null,
                GetIpAddress(), GetDeviceInfo(), "Failed", reason);
        }

        public void LogLoginLocked(string username)
        {
            LogLogin(0, username, null, null,
                GetIpAddress(), GetDeviceInfo(), "Locked", "Tài khoản bị khóa");
        }

        public void LogLogout(int userId)
        {
            // Không dùng GetLoginHistory(userId.ToString()) — đó là lọc Username LIKE, sai với userId số.
            var history = _repo.GetLoginHistory(null, null, null, null)
                .Where(x => x.UserId == userId && x.LogoutTime == null)
                .OrderByDescending(x => x.LoginTime)
                .FirstOrDefault();
            if (history != null)
                _repo.UpdateLogoutTime(history.LoginHistoryId, DateTime.Now);
        }

        public void LogLogoutByHistoryId(int loginHistoryId)
        {
            _repo.UpdateLogoutTime(loginHistoryId, DateTime.Now);
        }

        public IEnumerable<LoginHistory> SearchLoginHistory(string username = null,
            string status = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            return _repo.GetLoginHistory(username, status, fromDate, toDate);
        }

        // ==================== AUDIT LOG ====================

        /// <summary>
        /// Ghi một hành động đơn lẻ (không cần chi tiết field).
        /// </summary>
        public void LogAction(string action, string entityType, string entityId = null,
            string entityName = null, string description = null)
        {
            var user = GetCurrentUser();
            var item = BuildAuditLog(user, action, entityType, entityId, entityName, description);
            _repo.InsertAuditLog(item);
        }

        /// <summary>
        /// Ghi audit khi chưa có GetCurrentUserFunc (ví dụ ngay sau đăng nhập) hoặc cần chỉ định user rõ ràng.
        /// </summary>
        public void LogActionForUser(User user, string action, string entityType, string entityId = null,
            string entityName = null, string description = null)
        {
            var item = BuildAuditLog(user, action, entityType, entityId, entityName, description);
            _repo.InsertAuditLog(item);
        }

        /// <summary>Viết tắt: ghi Tạo mới.</summary>
        public void LogCreate(string entityType, string entityId, string entityName = null)
        {
            LogAction("CREATE", entityType, entityId, entityName);
        }

        /// <summary>Viết tắt: ghi Cập nhật.</summary>
        public void LogUpdate(string entityType, string entityId, string entityName = null, string description = null)
        {
            LogAction("UPDATE", entityType, entityId, entityName, description);
        }

        /// <summary>Viết tắt: ghi Xóa.</summary>
        public void LogDelete(string entityType, string entityId, string entityName = null)
        {
            LogAction("DELETE", entityType, entityId, entityName, "Đã xóa");
        }

        /// <summary>
        /// Một dòng AuditLog + một dòng DataChangeLog (dùng cho cấu hình / CRUD có old-new).
        /// </summary>
        public void LogDataChangeRow(string auditAction, string tableName, string recordId, string fieldName,
            string oldValue, string newValue, string changeType, string description = null)
        {
            var user = GetCurrentUser();
            var auditItem = BuildAuditLog(user, auditAction, tableName, recordId, fieldName, description);
            int auditId = _repo.InsertAuditLog(auditItem);
            _repo.InsertDataChangeLog(new DataChangeLog
            {
                AuditLogId = auditId,
                TableName = tableName ?? "Unknown",
                RecordId = recordId ?? "",
                FieldName = fieldName ?? "",
                OldValue = oldValue,
                NewValue = newValue,
                ChangeType = string.IsNullOrEmpty(changeType) ? (auditAction ?? "UPDATE").ToUpperInvariant() : changeType
            });
        }

        /// <summary>
        /// Ghi hành động kèm thay đổi chi tiết từng trường (so sánh object cũ / mới).
        /// </summary>
        public void LogActionWithChanges(string action, string entityType, string entityId,
            string entityName, object oldObj, object newObj, string description = null)
        {
            var user = GetCurrentUser();
            var auditItem = BuildAuditLog(user, action, entityType, entityId, entityName, description);
            auditItem.OldValues = oldObj != null ? SimpleSerialize(oldObj) : null;
            auditItem.NewValues = newObj != null ? SimpleSerialize(newObj) : null;

            var auditId = _repo.InsertAuditLog(auditItem);

            // Ghi chi tiết từng field
            var changes = ComputeChanges(oldObj, newObj);
            if (changes.Count > 0)
            {
                var logItems = changes.Select(kvp =>
                {
                    var (oldVal, newVal) = kvp.Value;
                    return new DataChangeLog
                    {
                        AuditLogId = auditId,
                        TableName = entityType,
                        RecordId = entityId,
                        FieldName = kvp.Key,
                        OldValue = oldVal,
                        NewValue = newVal,
                        ChangeType = action.ToUpperInvariant()
                    };
                });
                _repo.InsertDataChangeLogs(logItems);
            }
        }

        public IEnumerable<AuditLog> SearchAuditLogs(string username = null, string action = null,
            string entityType = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            return _repo.GetAuditLogs(username, action, entityType, fromDate, toDate);
        }

        public AuditLog GetAuditLogById(int id) => _repo.GetAuditLogById(id);

        public void DeleteAuditLog(int id) => _repo.DeleteAuditLog(id);

        public void DeleteOldAuditLogs(int keepDays = 90) => _repo.DeleteOldAuditLogs(keepDays);

        // ==================== DATA CHANGE LOG ====================

        public IEnumerable<DataChangeLog> SearchDataChangeLogs(int? auditLogId = null,
            string tableName = null, string recordId = null,
            DateTime? fromDate = null, DateTime? toDate = null)
        {
            return _repo.GetDataChangeLogs(auditLogId, tableName, recordId, fromDate, toDate);
        }

        public DataChangeLog GetDataChangeLogById(int id)
        {
            return _repo.GetDataChangeLogs(id, null, null, null, null)
                .FirstOrDefault(x => x.ChangeLogId == id);
        }

        // ==================== HELPERS ====================

        private AuditLog BuildAuditLog(User user, string action, string entityType,
            string entityId, string entityName, string description)
        {
            return new AuditLog
            {
                UserId = user?.UserId,
                Username = user?.Username,
                FullName = user?.FullName,
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                EntityName = entityName,
                Description = description,
                IpAddress = GetIpAddress(),
                DeviceInfo = GetDeviceInfo(),
                Timestamp = DateTime.Now
            };
        }

        private static User GetCurrentUser()
        {
            try { return GetCurrentUserFunc?.Invoke(); }
            catch { return null; }
        }

        private static string GetIpAddress()
        {
            // WinForms — không có HTTP context, trả về localhost
            return "127.0.0.1";
        }

        private static string GetDeviceInfo()
        {
            try { return Environment.MachineName + " / " + Environment.OSVersion; }
            catch { return "Unknown"; }
        }

        /// <summary>Serialize object thành chuỗi JSON thủ công (không cần System.Text.Json).</summary>
        private static string SimpleSerialize(object obj)
        {
            if (obj == null) return "null";
            var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var parts = new List<string>();
            foreach (var p in props)
            {
                if (!p.CanRead) continue;
                var val = p.GetValue(obj);
                var valStr = val == null ? "null" : $"\"{val}\"";
                parts.Add($"\"{p.Name}\":{valStr}");
            }
            return "{" + string.Join(",", parts) + "}";
        }

        /// <summary>So sánh 2 object, trả về dict[fieldName] = (oldValue, newValue).</summary>
        private static Dictionary<string, (string OldValue, string NewValue)> ComputeChanges(object oldObj, object newObj)
        {
            var result = new Dictionary<string, (string, string)>();
            if (oldObj == null && newObj == null) return result;

            var oldDict = oldObj != null ? FlattenObject(oldObj) : new Dictionary<string, string>();
            var newDict = newObj != null ? FlattenObject(newObj) : new Dictionary<string, string>();

            var allKeys = oldDict.Keys.Union(newDict.Keys).Distinct();
            foreach (var key in allKeys)
            {
                var ov = oldDict.TryGetValue(key, out var oldV) ? oldV : null;
                var nv = newDict.TryGetValue(key, out var newV) ? newV : null;
                if (!Equals(ov, nv))
                    result[key] = (ov, nv);
            }
            return result;
        }

        private static Dictionary<string, string> FlattenObject(object obj)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (obj == null) return result;

            var type = obj.GetType();
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanRead) continue;
                var val = prop.GetValue(obj);
                result[prop.Name] = val?.ToString();
            }
            return result;
        }
    }
}
