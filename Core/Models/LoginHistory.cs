using System;

namespace SharkTank.Core.Models
{
    /// <summary>Ghi lại mỗi lần user đăng nhập / đăng xuất.</summary>
    public class LoginHistory
    {
        public int LoginHistoryId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string IpAddress { get; set; }
        public string DeviceInfo { get; set; }
        public string Status { get; set; }      // Success | Failed | Locked | Expired
        public string FailureReason { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>Thời lượng phiên (nếu đã đăng xuất).</summary>
        public TimeSpan? Duration
        {
            get
            {
                if (!LogoutTime.HasValue) return null;
                return LogoutTime.Value - LoginTime;
            }
        }
    }
}
