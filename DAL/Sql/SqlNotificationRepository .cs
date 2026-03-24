using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SharkTank.Core.Models;

namespace SharkTank.DAL.Sql
{
    public class SqlNotificationRepository : INotificationRepository
    {
        public IEnumerable<SystemNotification> GetAll()
        {
            var list = new List<SystemNotification>();

            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT NotificationId, Title, Content, Type, TargetType, TargetValue,
       StartAt, EndAt, IsActive, CreatedBy, CreatedAt
FROM dbo.SystemNotifications
ORDER BY CreatedAt DESC;";

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(Map(r));
                    }
                }
            }

            return list;
        }

        public IEnumerable<SystemNotification> GetActiveForUser(string roleName, string username)
        {
            var list = new List<SystemNotification>();

            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT NotificationId, Title, Content, Type, TargetType, TargetValue,
       StartAt, EndAt, IsActive, CreatedBy, CreatedAt
FROM dbo.SystemNotifications
WHERE IsActive = 1
  AND StartAt <= GETDATE()
  AND (EndAt IS NULL OR EndAt >= GETDATE())
  AND
  (
      TargetType = 'ALL'
      OR (TargetType = 'ROLE' AND TargetValue = @RoleName)
      OR (TargetType = 'USER' AND TargetValue = @Username)
  )
ORDER BY CreatedAt DESC;";

                cmd.Parameters.AddWithValue("@RoleName", (object)roleName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Username", (object)username ?? DBNull.Value);

                conn.Open();
            }

            return list;
        }

        public SystemNotification GetById(int id)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT TOP 1 NotificationId, Title, Content, Type, TargetType, TargetValue,
       StartAt, EndAt, IsActive, CreatedBy, CreatedAt
FROM dbo.SystemNotifications
WHERE NotificationId = @NotificationId;";

                cmd.Parameters.AddWithValue("@NotificationId", id);

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                }
            }

            return null;
        }

        public void Insert(SystemNotification notification)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
INSERT INTO dbo.SystemNotifications
(Title, Content, Type, TargetType, TargetValue, StartAt, EndAt, IsActive, CreatedBy, CreatedAt)
VALUES
(@Title, @Content, @Type, @TargetType, @TargetValue, @StartAt, @EndAt, @IsActive, @CreatedBy, @CreatedAt);";

                AddParameters(cmd, notification);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(SystemNotification notification)
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
                AddParameters(cmd, notification);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM dbo.SystemNotifications WHERE NotificationId = @NotificationId;";
                cmd.Parameters.AddWithValue("@NotificationId", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void AddParameters(SqlCommand cmd, SystemNotification notification)
        {
            cmd.Parameters.AddWithValue("@Title", notification.Title ?? string.Empty);
            cmd.Parameters.AddWithValue("@Content", notification.Content ?? string.Empty);
            cmd.Parameters.AddWithValue("@Type", notification.Type ?? "Info");
            cmd.Parameters.AddWithValue("@TargetType", notification.TargetType ?? "ALL");
            cmd.Parameters.AddWithValue("@TargetValue", (object)notification.TargetValue ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@StartAt", notification.StartAt);
            cmd.Parameters.AddWithValue("@EndAt", (object)notification.EndAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", notification.IsActive);
            cmd.Parameters.AddWithValue("@CreatedBy", (object)notification.CreatedBy ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedAt", notification.CreatedAt);
        }

        private static SystemNotification Map(SqlDataReader r)
        {
            return new SystemNotification
            {
                NotificationId = Convert.ToInt32(r["NotificationId"]),
                Title = r["Title"].ToString(),
                Content = r["Content"].ToString(),
                Type = r["Type"].ToString(),
                TargetType = r["TargetType"].ToString(),
                TargetValue = r["TargetValue"] == DBNull.Value ? null : r["TargetValue"].ToString(),
                StartAt = Convert.ToDateTime(r["StartAt"]),
                EndAt = r["EndAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["EndAt"]),
                IsActive = Convert.ToBoolean(r["IsActive"]),
                CreatedBy = r["CreatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["CreatedBy"]),
                CreatedAt = Convert.ToDateTime(r["CreatedAt"])
            };
        }
    }
}
