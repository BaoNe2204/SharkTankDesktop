using System;

namespace SharkTank.Core.Models
{
    public class LoginSession
    {
        public Guid SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string IpAddress { get; set; }
        public string DeviceInfo { get; set; }
        public bool IsActive { get; set; }
    }
}

