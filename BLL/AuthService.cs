using System;
using SharkTank.Core.Models;
using SharkTank.Core.Security;
using SharkTank.DAL;

namespace SharkTank.BLL
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public User User { get; set; }
    }

    public class AuthService
    {
        private const int MaxFailedAttempts = 5;

        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return new AuthResult { Success = false, ErrorMessage = "Vui lòng nhập đầy đủ username và mật khẩu." };
            }

            var user = _userRepository.GetByUsername(username);
            if (user == null)
            {
                return new AuthResult { Success = false, ErrorMessage = "Sai tên đăng nhập hoặc mật khẩu." };
            }

            if (!user.IsActive)
            {
                return new AuthResult { Success = false, ErrorMessage = "Tài khoản đã bị vô hiệu hóa." };
            }

            if (user.IsLocked)
            {
                return new AuthResult { Success = false, ErrorMessage = "Tài khoản đã bị khóa. Liên hệ quản trị viên." };
            }

            var isValid = PasswordHasher.VerifyPassword(password, user.PasswordSalt, user.PasswordHash);
            if (!isValid)
            {
                user.FailedLoginAttempts += 1;
                if (user.FailedLoginAttempts >= MaxFailedAttempts)
                {
                    user.IsLocked = true;
                }
                user.UpdatedAt = DateTime.Now;
                _userRepository.Update(user);

                var msg = user.IsLocked
                    ? "Sai mật khẩu quá nhiều lần, tài khoản đã bị khóa."
                    : "Sai tên đăng nhập hoặc mật khẩu.";

                return new AuthResult { Success = false, ErrorMessage = msg };
            }

            // Đúng mật khẩu
            user.FailedLoginAttempts = 0;
            user.IsLocked = false;
            user.LastLoginAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            _userRepository.Update(user);

            return new AuthResult
            {
                Success = true,
                User = user
            };
        }

        public bool ForgotPassword(string username, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newPassword))
            {
                return false;
            }

            var user = _userRepository.GetByUsername(username);
            if (user == null || !user.IsActive)
            {
                return false;
            }

            // Reset password về giá trị mới
            var salt = PasswordHasher.GenerateSalt();
            user.PasswordSalt = salt;
            user.PasswordHash = PasswordHasher.HashPassword(newPassword, salt);
            user.FailedLoginAttempts = 0;
            user.IsLocked = false;
            user.UpdatedAt = DateTime.Now;
            _userRepository.Update(user);

            return true;
        }
    }
}
