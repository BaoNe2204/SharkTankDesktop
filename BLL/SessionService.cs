using System;
using SharkTank.Core.Models;

namespace SharkTank.BLL
{
    public class SessionService
    {
        private LoginSession _currentSession;
        private User _currentUser;

        public User CurrentUser => _currentUser;
        public LoginSession CurrentSession => _currentSession;

        public void StartSession(User user)
        {
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            _currentSession = new LoginSession
            {
                SessionId = Guid.NewGuid(),
                UserId = user.UserId,
                LoginTime = DateTime.Now,
                IsActive = true,
                IpAddress = string.Empty,
                DeviceInfo = Environment.MachineName
            };
        }

        public void EndSession()
        {
            if (_currentSession != null)
            {
                _currentSession.IsActive = false;
                _currentSession.LogoutTime = DateTime.Now;
            }

            _currentUser = null;
        }
    }
}

