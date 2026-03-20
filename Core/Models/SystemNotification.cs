using System;

namespace SharkTank.Core.Models
{
    public class SystemNotification
    {
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; } // Info, Warning, Error
        public string TargetType { get; set; } // ALL, ROLE, USER
        public string TargetValue { get; set; } // Admin, HR, username...
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
