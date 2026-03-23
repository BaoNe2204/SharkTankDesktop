using System.Collections.Generic;
using SharkTank.Core.Models;

namespace SharkTank.DAL
{
    /// <summary>
    /// Repository cho LoginHistory, AuditLog, DataChangeLog.
    /// </summary>
    public interface IAuditRepository
    {
        // ----- LoginHistory -----
        IEnumerable<LoginHistory> GetLoginHistory(string username = null, string status = null,
            System.DateTime? fromDate = null, System.DateTime? toDate = null);
        LoginHistory GetLoginHistoryById(int id);
        void InsertLoginHistory(LoginHistory item);
        void UpdateLogoutTime(int loginHistoryId, System.DateTime logoutTime);

        // ----- AuditLog -----
        IEnumerable<AuditLog> GetAuditLogs(string username = null, string action = null,
            string entityType = null, System.DateTime? fromDate = null, System.DateTime? toDate = null);
        AuditLog GetAuditLogById(int id);
        int InsertAuditLog(AuditLog item);
        void DeleteAuditLog(int id);
        void DeleteOldAuditLogs(int keepDays);

        // ----- DataChangeLog -----
        IEnumerable<DataChangeLog> GetDataChangeLogs(int? auditLogId = null,
            string tableName = null, string recordId = null,
            System.DateTime? fromDate = null, System.DateTime? toDate = null);
        void InsertDataChangeLog(DataChangeLog item);
        void InsertDataChangeLogs(IEnumerable<DataChangeLog> items);
    }
}
