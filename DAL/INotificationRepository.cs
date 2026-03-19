using System.Collections.Generic;
using SharkTank.Core.Models;

namespace SharkTank.DAL
{
    public interface INotificationRepository
    {
        IEnumerable<SystemNotification> GetAll();
        IEnumerable<SystemNotification> GetActiveForUser(string roleName, string username);
        SystemNotification GetById(int id);
        void Insert(SystemNotification notification);
        void Update(SystemNotification notification);
        void Delete(int id);
    }
}
