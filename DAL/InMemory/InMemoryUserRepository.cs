using System;
using System.Collections.Generic;
using System.Linq;
using SharkTank.Core.Models;
using SharkTank.Core.Security;

namespace SharkTank.DAL.InMemory
{
    /// <summary>
    /// Repository in-memory chỉ phục vụ chạy demo / phát triển.
    /// Sau này bạn có thể thay thế bằng SqlUserRepository dùng database thật.
    /// </summary>
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users;

        public InMemoryUserRepository()
        {
            _users = new List<User>
            {
                CreateUser(1, "admin", "admin123", 1, "System Administrator", "admin@example.com"),
                CreateUser(2, "hr", "hr123", 2, "HR User", "hr@example.com"),
                CreateUser(3, "sales", "sales123", 3, "Sales User", "sales@example.com"),
                CreateUser(4, "inventory", "inventory123", 4, "Inventory User", "inventory@example.com"),
                CreateUser(5, "accounting", "accounting123", 5, "Accounting User", "accounting@example.com")
            };
        }

        private static User CreateUser(int id, string username, string password, int roleId, string fullName, string email)
        {
            var salt = PasswordHasher.GenerateSalt();
            return new User
            {
                UserId = id,
                Username = username,
                PasswordSalt = salt,
                PasswordHash = PasswordHasher.HashPassword(password, salt),
                FullName = fullName,
                Email = email,
                Phone = "",
                RoleId = roleId,
                IsActive = true,
                IsLocked = false,
                FailedLoginAttempts = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public User GetByUsername(string username)
        {
            return _users.SingleOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public User GetById(int userId)
        {
            return _users.SingleOrDefault(u => u.UserId == userId);
        }

        public void Update(User user)
        {
            var existing = GetById(user.UserId);
            if (existing == null) return;

            var index = _users.IndexOf(existing);
            if (index >= 0)
            {
                _users[index] = user;
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }
    }
}

