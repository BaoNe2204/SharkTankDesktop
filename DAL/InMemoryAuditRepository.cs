using System;
using System.Collections.Generic;
using System.Linq;
using SharkTank.Core.Models;

namespace SharkTank.DAL
{
    /// <summary>
    /// In-memory fallback khi không có SQL.
    /// </summary>
    public class InMemoryAuditRepository : IAuditRepository
    {
        private readonly List<LoginHistory> _loginHistory = new List<LoginHistory>();
        private readonly List<AuditLog> _auditLogs = new List<AuditLog>();
        private readonly List<DataChangeLog> _dataChangeLogs = new List<DataChangeLog>();

        private int _loginId = 1;
        private int _auditId = 1;
        private int _changeId = 1;

        public IEnumerable<LoginHistory> GetLoginHistory(string username = null,
            string status = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var q = _loginHistory.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(username))
                q = q.Where(x => x.Username != null && x.Username.IndexOf(username, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrWhiteSpace(status))
                q = q.Where(x => x.Status == status);
            if (fromDate.HasValue)
                q = q.Where(x => x.LoginTime >= fromDate.Value);
            if (toDate.HasValue)
                q = q.Where(x => x.LoginTime <= toDate.Value);
            return q.OrderByDescending(x => x.LoginTime);
        }

        public LoginHistory GetLoginHistoryById(int id)
            => _loginHistory.FirstOrDefault(x => x.LoginHistoryId == id);

        public void InsertLoginHistory(LoginHistory item)
        {
            item.LoginHistoryId = _loginId++;
            if (item.CreatedAt == default(DateTime))
                item.CreatedAt = DateTime.Now;
            _loginHistory.Insert(0, item);
        }

        public void UpdateLogoutTime(int loginHistoryId, DateTime logoutTime)
        {
            var item = _loginHistory.FirstOrDefault(x => x.LoginHistoryId == loginHistoryId);
            if (item != null) item.LogoutTime = logoutTime;
        }

        public IEnumerable<AuditLog> GetAuditLogs(string username = null, string action = null,
            string entityType = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var q = _auditLogs.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(username))
                q = q.Where(x => x.Username != null && x.Username.IndexOf(username, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrWhiteSpace(action))
                q = q.Where(x => x.Action == action);
            if (!string.IsNullOrWhiteSpace(entityType))
                q = q.Where(x => x.EntityType == entityType);
            if (fromDate.HasValue)
                q = q.Where(x => x.Timestamp >= fromDate.Value);
            if (toDate.HasValue)
                q = q.Where(x => x.Timestamp <= toDate.Value);
            return q.OrderByDescending(x => x.Timestamp);
        }

        public AuditLog GetAuditLogById(int id)
            => _auditLogs.FirstOrDefault(x => x.AuditLogId == id);

        public int InsertAuditLog(AuditLog item)
        {
            item.AuditLogId = _auditId++;
            _auditLogs.Insert(0, item);
            return item.AuditLogId;
        }

        public void DeleteAuditLog(int id)
        {
            var item = _auditLogs.FirstOrDefault(x => x.AuditLogId == id);
            if (item != null) _auditLogs.Remove(item);
        }

        public void DeleteOldAuditLogs(int keepDays)
        {
            var cutoff = DateTime.Now.AddDays(-keepDays);
            _auditLogs.RemoveAll(x => x.Timestamp < cutoff);
        }

        public IEnumerable<DataChangeLog> GetDataChangeLogs(int? auditLogId = null,
            string tableName = null, string recordId = null,
            DateTime? fromDate = null, DateTime? toDate = null)
        {
            var q = _dataChangeLogs.AsEnumerable();
            if (auditLogId.HasValue)
                q = q.Where(x => x.AuditLogId == auditLogId.Value);
            if (!string.IsNullOrWhiteSpace(tableName))
                q = q.Where(x => x.TableName == tableName);
            if (!string.IsNullOrWhiteSpace(recordId))
                q = q.Where(x => x.RecordId == recordId);
            if (fromDate.HasValue)
                q = q.Where(x => x.ChangeTime >= fromDate.Value);
            if (toDate.HasValue)
                q = q.Where(x => x.ChangeTime <= toDate.Value);
            return q.OrderByDescending(x => x.ChangeTime);
        }

        public void InsertDataChangeLog(DataChangeLog item)
        {
            item.ChangeLogId = _changeId++;
            _dataChangeLogs.Insert(0, item);
        }

        public void InsertDataChangeLogs(IEnumerable<DataChangeLog> items)
        {
            if (items == null) return;
            foreach (var item in items)
                InsertDataChangeLog(item);
        }
    }
}
