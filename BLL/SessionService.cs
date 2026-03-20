using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using SharkTank.Core.Data;
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
                IpAddress = ResolvePublicIpAddress(),
                DeviceInfo = Environment.MachineName
            };

            PersistSessionStart(_currentSession);
        }

        public void EndSession()
        {
            if (_currentSession != null)
            {
                _currentSession.IsActive = false;
                _currentSession.LogoutTime = DateTime.Now;
                PersistSessionEnd(_currentSession);
            }

            _currentUser = null;
        }

        public bool IsCurrentSessionActive()
        {
            if (_currentSession == null)
            {
                return false;
            }

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TOP 1 IsActive FROM dbo.LoginSessions WHERE SessionId = @SessionId;";
                    cmd.Parameters.AddWithValue("@SessionId", _currentSession.SessionId);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        return false;
                    }

                    bool isActive = Convert.ToBoolean(result);
                    if (!isActive)
                    {
                        _currentSession.IsActive = false;
                        if (!_currentSession.LogoutTime.HasValue)
                        {
                            _currentSession.LogoutTime = DateTime.Now;
                        }
                    }

                    return isActive;
                }
            }
            catch (Exception ex)
            {
                // DB lỗi tạm thời thì không kick user nhầm
                System.Diagnostics.Debug.WriteLine($"IsCurrentSessionActive error: {ex.Message}");
                return true;
            }
        }

        private static void PersistSessionStart(LoginSession session)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
INSERT INTO dbo.LoginSessions (SessionId, UserId, LoginTime, LogoutTime, IpAddress, DeviceInfo, IsActive)
VALUES (@SessionId, @UserId, @LoginTime, @LogoutTime, @IpAddress, @DeviceInfo, @IsActive);";

                    cmd.Parameters.AddWithValue("@SessionId", session.SessionId);
                    cmd.Parameters.AddWithValue("@UserId", session.UserId);
                    cmd.Parameters.AddWithValue("@LoginTime", session.LoginTime);
                    cmd.Parameters.AddWithValue("@LogoutTime", (object)session.LogoutTime ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IpAddress", (object)session.IpAddress ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DeviceInfo", (object)session.DeviceInfo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", session.IsActive);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PersistSessionStart error: {ex.Message}");
            }
        }

        private static void PersistSessionEnd(LoginSession session)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
UPDATE dbo.LoginSessions
SET IsActive = 0,
    LogoutTime = @LogoutTime
WHERE SessionId = @SessionId;";

                    cmd.Parameters.AddWithValue("@SessionId", session.SessionId);
                    cmd.Parameters.AddWithValue("@LogoutTime", (object)session.LogoutTime ?? DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PersistSessionEnd error: {ex.Message}");
            }
        }

        private static string ResolvePublicIpAddress()
        {
            string[] providers =
            {
                "https://api.ipify.org",
                "https://ifconfig.me/ip",
                "https://checkip.amazonaws.com"
            };

            foreach (string url in providers)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.Timeout = 3000;
                    request.ReadWriteTimeout = 3000;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string raw = (reader.ReadToEnd() ?? string.Empty).Trim();
                        if (IsValidIp(raw))
                        {
                            return raw;
                        }
                    }
                }
                catch
                {
                    // thử provider tiếp theo
                }
            }

            return string.Empty;
        }

        private static bool IsValidIp(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return System.Net.IPAddress.TryParse(value, out _);
        }
    }
}
