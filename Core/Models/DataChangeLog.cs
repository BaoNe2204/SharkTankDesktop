using System;

namespace SharkTank.Core.Models
{
    /// <summary>
    /// Theo dõi thay đổi chi tiết từng trường trên mỗi bản ghi.
    /// </summary>
    public class DataChangeLog
    {
        public int ChangeLogId { get; set; }
        public int? AuditLogId { get; set; }
        public string TableName { get; set; }
        public string RecordId { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ChangeType { get; set; }  // INSERT | UPDATE | DELETE
        public DateTime ChangeTime { get; set; }

        /// <summary>ChangeType hiển thị tiếng Việt.</summary>
        public string ChangeTypeDisplay
        {
            get
            {
                var c = ChangeType?.ToUpperInvariant();
                if (c == "INSERT") return "Thêm mới";
                if (c == "UPDATE") return "Sửa đổi";
                if (c == "DELETE") return "Xóa";
                return ChangeType ?? "";
            }
        }
    }
}
