using System;
using System.Security.Cryptography;
using System.Text;

namespace SharkTank.Core.Security
{
    /// <summary>
    /// Password hashing helper (SHA256 + Salt).
    /// Khuyến nghị: dùng BCrypt/Argon2 nếu chuyển sang Web/API hoặc có thư viện phù hợp.
    /// </summary>
    public static class PasswordHasher
    {
        public static string GenerateSalt(int size = 16)
        {
            var bytes = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return BytesToHex(bytes);
        }

        public static string HashPassword(string password, string salt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (salt == null) throw new ArgumentNullException(nameof(salt));

            using (var sha256 = SHA256.Create())
            {
                // Dùng Unicode (UTF-16LE) để tương thích hashing trong SQL Server (NVARCHAR -> VARBINARY)
                var combined = Encoding.Unicode.GetBytes(password + salt);
                var hash = sha256.ComputeHash(combined);
                return BytesToHex(hash);
            }
        }

        public static bool VerifyPassword(string password, string salt, string expectedHash)
        {
            var hash = HashPassword(password, salt);
            return SlowEquals(hash, expectedHash);
        }

        // So sánh chống timing attack
        private static bool SlowEquals(string a, string b)
        {
            if (a == null || b == null) return false;
            var ba = Encoding.Unicode.GetBytes(a);
            var bb = Encoding.Unicode.GetBytes(b);

            uint diff = (uint)ba.Length ^ (uint)bb.Length;
            var len = Math.Min(ba.Length, bb.Length);
            for (int i = 0; i < len; i++)
            {
                diff |= (uint)(ba[i] ^ bb[i]);
            }
            return diff == 0;
        }

        private static string BytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}

