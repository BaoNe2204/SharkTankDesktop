using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using SharkTank.Core.Data;
using SharkTank.Core.Models;

namespace SharkTank.BLL
{
    /// <summary>
    /// Helper tổng hợp ghi audit log cho TẤT CẢ các form.
    /// Luôn ghi vào BOTH: AuditLogs + DataChangeLogs.
    /// 
    /// Cách dùng:
    ///   AuditHelper.Insert("NhanVien", nvId, entityName, newObj);
    ///   AuditHelper.Update("NhanVien", nvId, entityName, oldObj, newObj);
    ///   AuditHelper.Delete("NhanVien", nvId, entityName);
    /// </summary>
    public static class AuditHelper
    {
        /// <summary>Ghi INSERT - tự tạo snapshot từ entity object.</summary>
        public static void Insert(string tableName, string recordId, string recordName, object entity)
        {
            WriteAudit("INSERT", tableName, recordId, recordName, null, entity);
        }

        /// <summary>Ghi UPDATE - tự so sánh và ghi từng trường thay đổi.</summary>
        public static void Update(string tableName, string recordId, string recordName, object oldEntity, object newEntity)
        {
            WriteAudit("UPDATE", tableName, recordId, recordName, oldEntity, newEntity);
        }

        /// <summary>Ghi DELETE - tự tạo snapshot trước khi xóa (đọc từ DB).</summary>
        public static void Delete(string tableName, string recordId, string recordName, string idColumnName = "Id")
        {
            WriteAudit("DELETE", tableName, recordId, recordName, null, null, idColumnName);
        }

        /// <summary>
        /// Ghi INSERT/UPDATE/DELETE với entity object có sẵn.
        /// action: "INSERT" | "UPDATE" | "DELETE"
        /// </summary>
        public static void WriteAudit(string action, string tableName, string recordId, string recordName,
            object oldEntity, object newEntity, string idColumnName = null)
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    int? auditId = null;
                    var changes = new List<(string field, string oldVal, string newVal)>();

                    if (action == "DELETE")
                    {
                        // Đọc entity từ DB trước khi xóa
                        var oldObj = ReadEntityById(conn, tableName, recordId, idColumnName);
                        var oldSnap = CreateSnapshot(oldObj);
                        var newSnap = CreateSnapshot(new object());
                        changes = ComputeChanges(tableName, oldSnap, newSnap);

                        // Tạo AuditLog trước
                        auditId = InsertAuditLog(conn, "DELETE", tableName, recordId, recordName, Serialize(oldSnap), Serialize(newSnap));
                    }
                    else if (action == "INSERT")
                    {
                        var newSnap = CreateSnapshot(newEntity);
                        changes = ComputeChanges(tableName, null, newSnap);

                        auditId = InsertAuditLog(conn, "INSERT", tableName, recordId, recordName, null, Serialize(newSnap));
                    }
                    else // UPDATE
                    {
                        var oldSnap = CreateSnapshot(oldEntity);
                        var newSnap = CreateSnapshot(newEntity);
                        changes = ComputeChanges(tableName, oldSnap, newSnap);

                        auditId = InsertAuditLog(conn, "UPDATE", tableName, recordId, recordName, Serialize(oldSnap), Serialize(newSnap));
                    }

                    // Ghi chi tiết từng field vào DataChangeLogs
                    if (changes.Count > 0 && auditId.HasValue)
                    {
                        InsertDataChangeLogs(conn, auditId.Value, tableName, recordId, action, changes);
                    }
                }
            }
            catch { /* Không throw để không ảnh hưởng thao tác chính */ }
        }

        // ==================== PRIVATE HELPERS ====================

        private static int? InsertAuditLog(SqlConnection conn, string action, string tableName,
            string recordId, string recordName, string oldValues, string newValues)
        {
            // Lấy thông tin user từ SessionService nếu có
            string username = GetCurrentUsername();
            int? userId = GetCurrentUserId();

            string sql = @"
INSERT INTO dbo.AuditLogs
(UserId, Username, Action, EntityType, EntityId, EntityName, Description, IpAddress, DeviceInfo, OldValues, NewValues)
VALUES
(@UserId, @Username, @Action, @EntityType, @EntityId, @EntityName, @Description, @IpAddress, @DeviceInfo, @OldValues, @NewValues);
SELECT SCOPE_IDENTITY();";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", (object)userId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Username", (object)username ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Action", action);
                cmd.Parameters.AddWithValue("@EntityType", tableName);
                cmd.Parameters.AddWithValue("@EntityId", (object)recordId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EntityName", (object)recordName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", GetDescription(action, tableName, recordName));
                cmd.Parameters.AddWithValue("@IpAddress", "127.0.0.1");
                cmd.Parameters.AddWithValue("@DeviceInfo", GetDeviceInfo());
                cmd.Parameters.AddWithValue("@OldValues", (object)oldValues ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NewValues", (object)newValues ?? DBNull.Value);

                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                    return id;
            }
            return null;
        }

        private static void InsertDataChangeLogs(SqlConnection conn, int auditId, string tableName,
            string recordId, string action, List<(string field, string oldVal, string newVal)> changes)
        {
            foreach (var change in changes)
            {
                string sql = @"
INSERT INTO dbo.DataChangeLogs
(AuditLogId, TableName, RecordId, FieldName, OldValue, NewValue, ChangeType)
VALUES
(@AuditLogId, @TableName, @RecordId, @FieldName, @OldValue, @NewValue, @ChangeType)";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AuditLogId", auditId);
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@RecordId", recordId);
                    cmd.Parameters.AddWithValue("@FieldName", change.field);
                    cmd.Parameters.AddWithValue("@OldValue", (object)change.oldVal ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NewValue", (object)change.newVal ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ChangeType", action);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static object ReadEntityById(SqlConnection conn, string tableName, string recordId, string idColumnName)
        {
            string col = idColumnName ?? "Id";
            string sql = $"SELECT * FROM {tableName} WHERE {col} = @id";

            try
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", recordId);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            // Tạo dynamic object với tất cả columns
                            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                var val = rdr.GetValue(i);
                                dict[rdr.GetName(i)] = val == DBNull.Value ? null : val.ToString();
                            }
                            return dict;
                        }
                    }
                }
            }
            catch { /* Bảng có thể không tồn tại */ }

            return null;
        }

        private static Dictionary<string, string> CreateSnapshot(object entity)
        {
            var snap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (entity == null) return snap;

            // Dùng reflection thay vì pattern matching (C# 7.3 không hỗ trợ recursive patterns)
            var entityType = entity.GetType();
            if (entityType == typeof(Dictionary<string, string>))
            {
                var dict = (Dictionary<string, string>)entity;
                foreach (var kvp in dict)
                    snap[kvp.Key] = kvp.Value;
                return snap;
            }

            // Dùng Reflection cho object thường
            if (entityType.IsClass && entityType != typeof(string))
            {
                foreach (var prop in entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!prop.CanRead) continue;
                    try
                    {
                        var val = prop.GetValue(entity);
                        snap[prop.Name] = val == null ? null : val.ToString();
                    }
                    catch { }
                }
            }

            return snap;
        }

        private static List<(string field, string oldVal, string newVal)> ComputeChanges(
            string tableName, Dictionary<string, string> oldSnap, Dictionary<string, string> newSnap)
        {
            var changes = new List<(string, string, string)>();

            if (oldSnap == null) oldSnap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (newSnap == null) newSnap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Ignore these common auto-generated fields
            var ignoreFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "CreatedAt", "UpdatedAt", "CreatedBy", "UpdatedBy",
                "Timestamp", "RowVersion", "ModifiedDate"
            };

            var allKeys = new HashSet<string>(oldSnap.Keys, StringComparer.OrdinalIgnoreCase);
            allKeys.UnionWith(newSnap.Keys);

            foreach (var key in allKeys)
            {
                if (ignoreFields.Contains(key)) continue;

                oldSnap.TryGetValue(key, out var ov);
                newSnap.TryGetValue(key, out var nv);

                if (!Equals(ov, nv))
                    changes.Add((key, ov, nv));
            }

            return changes;
        }

        private static string Serialize(Dictionary<string, string> snap)
        {
            if (snap == null || snap.Count == 0) return null;

            var parts = new List<string>();
            foreach (var kvp in snap)
            {
                var val = kvp.Value ?? "null";
                parts.Add($"\"{kvp.Key}\":\"{EscapeJson(val)}\"");
            }
            return "{" + string.Join(",", parts) + "}";
        }

        private static string EscapeJson(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private static string GetCurrentUsername()
        {
            try
            {
                if (AuditService.GetCurrentUserFunc != null)
                {
                    var user = AuditService.GetCurrentUserFunc();
                    return user?.Username;
                }
            }
            catch { }
            return null;
        }

        private static int? GetCurrentUserId()
        {
            try
            {
                if (AuditService.GetCurrentUserFunc != null)
                {
                    var user = AuditService.GetCurrentUserFunc();
                    return user?.UserId;
                }
            }
            catch { }
            return null;
        }

        private static string GetDeviceInfo()
        {
            try { return Environment.MachineName + " / " + Environment.OSVersion; }
            catch { return "Unknown"; }
        }

        private static string GetDescription(string action, string tableName, string recordName)
        {
            string actVi;
            switch (action)
            {
                case "INSERT": actVi = "Thêm mới"; break;
                case "UPDATE": actVi = "Cập nhật"; break;
                case "DELETE": actVi = "Xóa"; break;
                default: actVi = action; break;
            }

            return actVi + " " + tableName + ": " + recordName;
        }
    }

    // ==================== SNAPSHOT CLASSES ====================

    /// <summary>
    /// Snapshot để ghi audit cho NhanVien.
    /// Dùng với AuditHelper.Update().
    /// </summary>
    public sealed class NhanVienSnapshot
    {
        public string HoTen { get; set; }
        public string NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string PhongBanId { get; set; }
        public string ChucVuId { get; set; }
        public string NgayVaoLam { get; set; }
        public string GhiChu { get; set; }

        public static NhanVienSnapshot FromDb(string nhanVienId, SqlConnection conn = null, SqlTransaction tran = null)
        {
            bool ownConn = conn == null;
            conn = conn ?? DBHelper.GetConnection();
            try
            {
                if (ownConn) conn.Open();
                using (var cmd = new SqlCommand(@"
                    SELECT HoTen, NgaySinh, GioiTinh, DiaChi, Email, SoDienThoai, PhongBanId, ChucVuId, NgayVaoLam, GhiChu
                    FROM NhanVien WHERE NhanVienId = @Id", conn, tran ?? null))
                {
                    cmd.Parameters.AddWithValue("@Id", nhanVienId);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new NhanVienSnapshot
                        {
                            HoTen = rdr["HoTen"]?.ToString(),
                            NgaySinh = rdr["NgaySinh"] == DBNull.Value ? "" : ((DateTime)rdr["NgaySinh"]).ToString("yyyy-MM-dd"),
                            GioiTinh = rdr["GioiTinh"]?.ToString(),
                            DiaChi = rdr["DiaChi"]?.ToString(),
                            Email = rdr["Email"]?.ToString(),
                            SoDienThoai = rdr["SoDienThoai"]?.ToString(),
                            PhongBanId = rdr["PhongBanId"] == DBNull.Value ? "" : rdr["PhongBanId"].ToString(),
                            ChucVuId = rdr["ChucVuId"] == DBNull.Value ? "" : rdr["ChucVuId"].ToString(),
                            NgayVaoLam = rdr["NgayVaoLam"] == DBNull.Value ? "" : ((DateTime)rdr["NgayVaoLam"]).ToString("yyyy-MM-dd"),
                            GhiChu = rdr["GhiChu"]?.ToString()
                        };
                    }
                }
            }
            finally { if (ownConn) conn.Dispose(); }
        }
    }

    /// <summary>Snapshot cho KhachHang.</summary>
    public sealed class KhachHangSnapshot
    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }

        public static KhachHangSnapshot FromDb(string maKH)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT MaKH, HoTen, DienThoai, DiaChi, Email FROM KhachHang WHERE MaKH = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", maKH);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new KhachHangSnapshot
                        {
                            MaKH = rdr["MaKH"]?.ToString(),
                            HoTen = rdr["HoTen"]?.ToString(),
                            DienThoai = rdr["DienThoai"]?.ToString(),
                            DiaChi = rdr["DiaChi"]?.ToString(),
                            Email = rdr["Email"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho Kho.</summary>
    public sealed class KhoSnapshot
    {
        public string MaKho { get; set; }
        public string TenKho { get; set; }
        public string DiaChi { get; set; }

        public static KhoSnapshot FromDb(string maKho)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT MaKho, TenKho, DiaChi FROM Kho WHERE MaKho = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", maKho);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new KhoSnapshot
                        {
                            MaKho = rdr["MaKho"]?.ToString(),
                            TenKho = rdr["TenKho"]?.ToString(),
                            DiaChi = rdr["DiaChi"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho ViTriKho.</summary>
    public sealed class ViTriKhoSnapshot
    {
        public string MaViTri { get; set; }
        public string TenViTri { get; set; }
        public string MaKho { get; set; }

        public static ViTriKhoSnapshot FromDb(string maViTri)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT MaViTri, TenViTri, MaKho FROM ViTriKho WHERE MaViTri = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", maViTri);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new ViTriKhoSnapshot
                        {
                            MaViTri = rdr["MaViTri"]?.ToString(),
                            TenViTri = rdr["TenViTri"]?.ToString(),
                            MaKho = rdr["MaKho"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho PhongBan.</summary>
    public sealed class PhongBanSnapshot
    {
        public string PhongBanId { get; set; }
        public string TenPhongBan { get; set; }
        public string MoTa { get; set; }

        public static PhongBanSnapshot FromDb(string phongBanId)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT PhongBanId, TenPhongBan, MoTa FROM PhongBan WHERE PhongBanId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", phongBanId);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new PhongBanSnapshot
                        {
                            PhongBanId = rdr["PhongBanId"]?.ToString(),
                            TenPhongBan = rdr["TenPhongBan"]?.ToString(),
                            MoTa = rdr["MoTa"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho ChucVu.</summary>
    public sealed class ChucVuSnapshot
    {
        public string ChucVuId { get; set; }
        public string TenChucVu { get; set; }
        public string MoTa { get; set; }

        public static ChucVuSnapshot FromDb(string chucVuId)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT ChucVuId, TenChucVu, MoTa FROM ChucVu WHERE ChucVuId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", chucVuId);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new ChucVuSnapshot
                        {
                            ChucVuId = rdr["ChucVuId"]?.ToString(),
                            TenChucVu = rdr["TenChucVu"]?.ToString(),
                            MoTa = rdr["MoTa"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho Users.</summary>
    public sealed class UsersSnapshot
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string IsActive { get; set; }

        public static UsersSnapshot FromDb(string userId)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT UserId, Username, FullName, Email, RoleId, IsActive FROM Users WHERE UserId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new UsersSnapshot
                        {
                            UserId = rdr["UserId"]?.ToString(),
                            Username = rdr["Username"]?.ToString(),
                            FullName = rdr["FullName"]?.ToString(),
                            Email = rdr["Email"]?.ToString(),
                            RoleId = rdr["RoleId"]?.ToString(),
                            IsActive = rdr["IsActive"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho Leads.</summary>
    public sealed class LeadsSnapshot
    {
        public string LeadID { get; set; }
        public string Ten { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string Nguon { get; set; }
        public string TrangThai { get; set; }

        public static LeadsSnapshot FromDb(string leadId)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT LeadID, Ten, SoDienThoai, Email, Nguon, TrangThai FROM Leads WHERE LeadID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", leadId);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new LeadsSnapshot
                        {
                            LeadID = rdr["LeadID"]?.ToString(),
                            Ten = rdr["Ten"]?.ToString(),
                            SoDienThoai = rdr["SoDienThoai"]?.ToString(),
                            Email = rdr["Email"]?.ToString(),
                            Nguon = rdr["Nguon"]?.ToString(),
                            TrangThai = rdr["TrangThai"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho ChamSocKhachHang.</summary>
    public sealed class ChamSocKhachHangSnapshot
    {
        public string Id { get; set; }
        public string KhachHang { get; set; }
        public string Ngay { get; set; }
        public string Loai { get; set; }
        public string NoiDung { get; set; }

        public static ChamSocKhachHangSnapshot FromDb(string id)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT Id, KhachHang, Ngay, Loai, NoiDung FROM ChamSocKhachHang WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new ChamSocKhachHangSnapshot
                        {
                            Id = rdr["Id"]?.ToString(),
                            KhachHang = rdr["KhachHang"]?.ToString(),
                            Ngay = rdr["Ngay"] == DBNull.Value ? "" : ((DateTime)rdr["Ngay"]).ToString("yyyy-MM-dd"),
                            Loai = rdr["Loai"]?.ToString(),
                            NoiDung = rdr["NoiDung"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho QLCoHoiBanHang.</summary>
    public sealed class QLCoHoiBanHangSnapshot
    {
        public string QLCoHoiBanHangID { get; set; }
        public string TenCoHoi { get; set; }
        public string GiaTriDuKien { get; set; }
        public string XacSuat { get; set; }
        public string TrangThai { get; set; }

        public static QLCoHoiBanHangSnapshot FromDb(string id)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT QLCoHoiBanHangID, TenCoHoi, GiaTriDuKien, XacSuat, TrangThai FROM QLCoHoiBanHang WHERE QLCoHoiBanHangID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new QLCoHoiBanHangSnapshot
                        {
                            QLCoHoiBanHangID = rdr["QLCoHoiBanHangID"]?.ToString(),
                            TenCoHoi = rdr["TenCoHoi"]?.ToString(),
                            GiaTriDuKien = rdr["GiaTriDuKien"]?.ToString(),
                            XacSuat = rdr["XacSuat"]?.ToString(),
                            TrangThai = rdr["TrangThai"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho NhapKho.</summary>
    public sealed class NhapKhoSnapshot
    {
        public string PhieuNhap { get; set; }
        public string MaKho { get; set; }
        public string MaSP { get; set; }
        public string NhaCungCap { get; set; }
        public string GiaNhap { get; set; }
        public string SoLuong { get; set; }

        public static NhapKhoSnapshot FromDb(string phieuNhap)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT PhieuNhap, MaKho, MaSP, NhaCungCap, GiaNhap, SoLuong FROM NhapKho WHERE PhieuNhap = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", phieuNhap);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new NhapKhoSnapshot
                        {
                            PhieuNhap = rdr["PhieuNhap"]?.ToString(),
                            MaKho = rdr["MaKho"]?.ToString(),
                            MaSP = rdr["MaSP"]?.ToString(),
                            NhaCungCap = rdr["NhaCungCap"]?.ToString(),
                            GiaNhap = rdr["GiaNhap"]?.ToString(),
                            SoLuong = rdr["SoLuong"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho XuatKho.</summary>
    public sealed class XuatKhoSnapshot
    {
        public string PhieuXuat { get; set; }
        public string MaKho { get; set; }
        public string MaSP { get; set; }
        public string LoaiXuat { get; set; }
        public string SoLuong { get; set; }

        public static XuatKhoSnapshot FromDb(string phieuXuat)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT PhieuXuat, MaKho, MaSP, LoaiXuat, SoLuong FROM XuatKho WHERE PhieuXuat = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", phieuXuat);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new XuatKhoSnapshot
                        {
                            PhieuXuat = rdr["PhieuXuat"]?.ToString(),
                            MaKho = rdr["MaKho"]?.ToString(),
                            MaSP = rdr["MaSP"]?.ToString(),
                            LoaiXuat = rdr["LoaiXuat"]?.ToString(),
                            SoLuong = rdr["SoLuong"]?.ToString()
                        };
                    }
                }
            }
        }
    }

    /// <summary>Snapshot cho NghiPhep.</summary>
    public sealed class NghiPhepSnapshot
    {
        public string Id { get; set; }
        public string NhanVienId { get; set; }
        public string LoaiPhep { get; set; }
        public string SoNgay { get; set; }
        public string TrangThai { get; set; }

        public static NghiPhepSnapshot FromDb(string id)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT Id, NhanVienId, LoaiPhep, SoNgay, TrangThai FROM NghiPhep WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return null;
                        return new NghiPhepSnapshot
                        {
                            Id = rdr["Id"]?.ToString(),
                            NhanVienId = rdr["NhanVienId"]?.ToString(),
                            LoaiPhep = rdr["LoaiPhep"]?.ToString(),
                            SoNgay = rdr["SoNgay"]?.ToString(),
                            TrangThai = rdr["TrangThai"]?.ToString()
                        };
                    }
                }
            }
        }
    }
}
