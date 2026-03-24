using System;

namespace SharkTank.Core.Models
{
    /// <summary>
    /// Nhật ký hành động người dùng (Audit Log).
    /// </summary>
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Action { get; set; }       // CREATE | UPDATE | DELETE | LOGIN | LOGOUT | EXPORT | IMPORT
        public string EntityType { get; set; }   // Users | Employees | Departments | ...
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public string IpAddress { get; set; }
        public string DeviceInfo { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public DateTime Timestamp { get; set; }

        /// <summary>Action hiển thị tiếng Việt.</summary>
        public string ActionDisplay
        {
            get
            {
                var a = Action?.ToUpperInvariant();
                if (a == "CREATE") return "Tạo mới";
                if (a == "UPDATE") return "Cập nhật";
                if (a == "DELETE") return "Xóa";
                if (a == "LOGIN") return "Đăng nhập";
                if (a == "LOGOUT") return "Đăng xuất";
                if (a == "EXPORT") return "Xuất dữ liệu";
                if (a == "IMPORT") return "Nhập dữ liệu";
                if (a == "VIEW") return "Mở màn hình";
                return Action ?? "";
            }
        }
    }
}
