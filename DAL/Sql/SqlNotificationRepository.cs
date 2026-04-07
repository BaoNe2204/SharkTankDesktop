using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SharkTank.Core.Models;

namespace SharkTank.DAL.Sql
{
    public class SqlNotificationRepository : INotificationRepository
    {
        // ================= GET ALL =================
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

        // ================= GET ACTIVE =================
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

                cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value =
                    (object)roleName ?? DBNull.Value;

                cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value =
                    (object)username ?? DBNull.Value;

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

        // ================= GET BY ID =================
        public SystemNotification GetById(int id)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT TOP 1 NotificationId, Title, Content, Type, TargetType, TargetValue,
       StartAt, EndAt, IsActive, CreatedBy, CreatedAt
FROM dbo.SystemNotifications
WHERE NotificationId = @Id;";

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                conn.Open();

                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                        return Map(r);
                }
            }

            return null;
        }

        // ================= INSERT =================
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

        // ================= UPDATE =================
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
WHERE NotificationId = @Id;";

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = notification.NotificationId;

                AddParameters(cmd, notification);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ================= DELETE =================
        public void Delete(int id)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM dbo.SystemNotifications WHERE NotificationId = @Id;";
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ================= PARAM =================
        private static void AddParameters(SqlCommand cmd, SystemNotification n)
        {
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value =
                (object)n.Title ?? DBNull.Value;

            cmd.Parameters.Add("@Content", SqlDbType.NVarChar).Value =
                (object)n.Content ?? DBNull.Value;

            cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value =
                (object)n.Type ?? "Info";

            cmd.Parameters.Add("@TargetType", SqlDbType.NVarChar).Value =
                (object)n.TargetType ?? "ALL";

            cmd.Parameters.Add("@TargetValue", SqlDbType.NVarChar).Value =
                (object)n.TargetValue ?? DBNull.Value;

            cmd.Parameters.Add("@StartAt", SqlDbType.DateTime).Value =
                n.StartAt == default ? DateTime.Now : n.StartAt;

            cmd.Parameters.Add("@EndAt", SqlDbType.DateTime).Value =
                (object)n.EndAt ?? DBNull.Value;

            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                n.IsActive;

            cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value =
                (object)n.CreatedBy ?? DBNull.Value;

            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value =
                n.CreatedAt == default ? DateTime.Now : n.CreatedAt;
        }

        // ================= MAP =================
        private static SystemNotification Map(SqlDataReader r)
        {
            return new SystemNotification
            {
                NotificationId = Convert.ToInt32(r["NotificationId"]),
                Title = r["Title"]?.ToString(),
                Content = r["Content"]?.ToString(),
                Type = r["Type"]?.ToString(),
                TargetType = r["TargetType"]?.ToString(),
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