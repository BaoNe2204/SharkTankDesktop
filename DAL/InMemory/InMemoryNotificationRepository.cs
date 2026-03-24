using System.Collections.Generic;
using SharkTank.Core.Models;

namespace SharkTank.DAL.InMemory
{
    /// <summary>
    /// Dùng khi không có SQL / bảng thông báo — tránh null NotificationService.
    /// </summary>
    public class InMemoryNotificationRepository : INotificationRepository
    {
        public IEnumerable<SystemNotification> GetAll() => new List<SystemNotification>();

        public IEnumerable<SystemNotification> GetActiveForUser(string roleName, string username) => new List<SystemNotification>();

        public SystemNotification GetById(int id) => null;

        public void Insert(SystemNotification notification) { }

        public void Update(SystemNotification notification) { }

        public void Delete(int id) { }
    }
}
