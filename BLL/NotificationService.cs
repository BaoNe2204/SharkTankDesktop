using System;
using System.Collections.Generic;
using SharkTank.Core.Models;
using SharkTank.DAL;

namespace SharkTank.BLL
{
    public class NotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SystemNotification> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<SystemNotification> GetForUser(string roleName, string username)
        {
            return _repository.GetActiveForUser(roleName, username);
        }

        public void Save(SystemNotification notification)
        {
            if (string.IsNullOrWhiteSpace(notification.Title))
                throw new Exception("Tiêu đề không được để trống.");

            if (string.IsNullOrWhiteSpace(notification.Content))
                throw new Exception("Nội dung không được để trống.");

            if (notification.NotificationId == 0)
            {
                notification.CreatedAt = DateTime.Now;
                _repository.Insert(notification);
            }
            else
            {
                _repository.Update(notification);
            }
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
