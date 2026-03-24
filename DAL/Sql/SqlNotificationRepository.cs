using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SharkTank.Core.Models;

namespace SharkTank.DAL.Sql
{
    public class SqlNotificationRepository : INotificationRepository
    {
        public IEnumerable<SystemNotification> GetAll()
        {
            var list = new List<SystemNotification>();
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT NotificationId, Title, Content, Type, TargetType, TargetValue, StartAt, EndAt, IsActive, CreatedBy, CreatedAt
FROM dbo.SystemNotifications
ORDER BY CreatedAt DESC;";

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(MapNotification(r));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SqlNotificationRepository.GetAll error: {ex.Message}");
            }
            return list;
        }

        public IEnumerable<SystemNotification> GetActiveForUser(string roleName, string username)
        {
            var list = new List<SystemNotification>();
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT NotificationId, Title, Content, Type, TargetType, TargetValue, StartAt, EndAt, IsActive, CreatedBy, CreatedAt
FROM dbo.SystemNotifications
WHERE IsActive = 1
  AND StartAt <= GETDATE()
  AND (EndAt IS NULL OR EndAt >= GETDATE())
  AND (
    TargetType = 'ALL'
    OR (TargetType = 'ROLE' AND TargetValue = @RoleName)
    OR (TargetType = 'USER' AND TargetValue = @Username)
  )
ORDER BY CreatedAt DESC;";
                    cmd.Parameters.AddWithValue("@RoleName", (object)roleName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Username", (object)username ?? DBNull.Value);

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(MapNotification(r));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SqlNotificationRepository.GetActiveForUser error: {ex.Message}");
            }
            return list;
        }

        public SystemNotification GetById(int id)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT NotificationId, Title, Content, Type, TargetType, TargetValue, StartAt, EndAt, IsActive, CreatedBy, CreatedAt
FROM dbo.SystemNotifications
WHERE NotificationId = @NotificationId;";
                    cmd.Parameters.Add(new SqlParameter("@NotificationId", SqlDbType.Int) { Value = id });

                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                            return MapNotification(r);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SqlNotificationRepository.GetById error: {ex.Message}");
            }
            return null;
        }

        public void Insert(SystemNotification notification)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
INSERT INTO dbo.SystemNotifications (Title, Content, Type, TargetType, TargetValue, StartAt, EndAt, IsActive, CreatedBy, CreatedAt)
VALUES (@Title, @Content, @Type, @TargetType, @TargetValue, @StartAt, @EndAt, @IsActive, @CreatedBy, @CreatedAt);";

                    cmd.Parameters.AddWithValue("@Title", notification.Title ?? "");
                    cmd.Parameters.AddWithValue("@Content", notification.Content ?? "");
                    cmd.Parameters.AddWithValue("@Type", notification.Type ?? "Info");
                    cmd.Parameters.AddWithValue("@TargetType", notification.TargetType ?? "ALL");
                    cmd.Parameters.AddWithValue("@TargetValue", (object)notification.TargetValue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@StartAt", notification.StartAt);
                    cmd.Parameters.AddWithValue("@EndAt", (object)notification.EndAt ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", notification.IsActive);
                    cmd.Parameters.AddWithValue("@CreatedBy", (object)notification.CreatedBy ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedAt", notification.CreatedAt);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SqlNotificationRepository.Insert error: {ex.Message}");
            }
        }

        public void Update(SystemNotification notification)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
UPDATE dbo.SystemNotifications
SET Title = @Title,
    Content = @Content,
    Type = @Type,
    TargetType = @TargetType,
    TargetValue = @TargetValue,
    StartAt = @StartAt,
    EndAt = @EndAt,
    IsActive = @IsActive
WHERE NotificationId = @NotificationId;";

                    cmd.Parameters.AddWithValue("@NotificationId", notification.NotificationId);
                    cmd.Parameters.AddWithValue("@Title", notification.Title ?? "");
                    cmd.Parameters.AddWithValue("@Content", notification.Content ?? "");
                    cmd.Parameters.AddWithValue("@Type", notification.Type ?? "Info");
                    cmd.Parameters.AddWithValue("@TargetType", notification.TargetType ?? "ALL");
                    cmd.Parameters.AddWithValue("@TargetValue", (object)notification.TargetValue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@StartAt", notification.StartAt);
                    cmd.Parameters.AddWithValue("@EndAt", (object)notification.EndAt ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", notification.IsActive);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SqlNotificationRepository.Update error: {ex.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var conn = SqlConnectionFactory.Create())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM dbo.SystemNotifications WHERE NotificationId = @NotificationId;";
                    cmd.Parameters.Add(new SqlParameter("@NotificationId", SqlDbType.Int) { Value = id });

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SqlNotificationRepository.Delete error: {ex.Message}");
            }
        }

        private static SystemNotification MapNotification(SqlDataReader r)
        {
            return new SystemNotification
            {
                NotificationId = r.GetInt32(r.GetOrdinal("NotificationId")),
                Title = r.IsDBNull(r.GetOrdinal("Title")) ? null : r.GetString(r.GetOrdinal("Title")),
                Content = r.IsDBNull(r.GetOrdinal("Content")) ? null : r.GetString(r.GetOrdinal("Content")),
                Type = r.IsDBNull(r.GetOrdinal("Type")) ? null : r.GetString(r.GetOrdinal("Type")),
                TargetType = r.IsDBNull(r.GetOrdinal("TargetType")) ? null : r.GetString(r.GetOrdinal("TargetType")),
                TargetValue = r.IsDBNull(r.GetOrdinal("TargetValue")) ? null : r.GetString(r.GetOrdinal("TargetValue")),
                StartAt = r.GetDateTime(r.GetOrdinal("StartAt")),
                EndAt = r.IsDBNull(r.GetOrdinal("EndAt")) ? (DateTime?)null : r.GetDateTime(r.GetOrdinal("EndAt")),
                IsActive = r.GetBoolean(r.GetOrdinal("IsActive")),
                CreatedBy = r.IsDBNull(r.GetOrdinal("CreatedBy")) ? (int?)null : r.GetInt32(r.GetOrdinal("CreatedBy")),
                CreatedAt = r.GetDateTime(r.GetOrdinal("CreatedAt"))
            };
        }
    }
}
