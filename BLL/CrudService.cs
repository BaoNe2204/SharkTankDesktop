using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SharkTank.BLL;
using SharkTank.Core.Data;

namespace SharkTank.BLL
{
    /// <summary>
    /// Generic CRUD Service - Tự động log mọi thao tác Create/Update/Delete
    /// Dùng chung cho tất cả các form, không cần thêm code log ở từng nơi.
    /// </summary>
    public class CrudService
    {
        private static readonly AuditService _audit = AuditService.CreateDefault();

        // ==================== INSERT ====================

        /// <summary>
        /// Chèn một dòng mới và tự động log CREATE.
        /// </summary>
        public void Insert(string tableName, Dictionary<string, object> data)
        {
            Insert(tableName, data, getId: null);
        }

        /// <summary>
        /// Chèn một dòng mới, tự động log CREATE, trả về ID nếu cần.
        /// </summary>
        public object Insert(string tableName, Dictionary<string, object> data, Func<SqlConnection, object> getId = null)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                var columns = string.Join(", ", data.Keys);
                var paramNames = string.Join(", ", data.Keys.Select(k => "@" + k));

                string sql = $"INSERT INTO {tableName} ({columns}) VALUES ({paramNames})";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    foreach (var kvp in data)
                        cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                object id = null;
                if (getId != null)
                    id = getId(conn);

                LogSafe(() => _audit.LogCreate(tableName, id?.ToString() ?? "unknown"));
                return id;
            }
        }

        // ==================== UPDATE ====================

        /// <summary>
        /// Cập nhật một dòng theo ID và tự động log UPDATE.
        /// </summary>
        public void Update(string tableName, string idColumn, object idValue, Dictionary<string, object> data)
        {
            Update(tableName, idColumn, idValue, data, oldData: null);
        }

        /// <summary>
        /// Cập nhật với chi tiết thay đổi (so sánh cũ - mới).
        /// </summary>
        public void Update(string tableName, string idColumn, object idValue,
            Dictionary<string, object> newData, Dictionary<string, object> oldData)
        {
            if (newData == null || newData.Count == 0) return;

            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                var setClause = string.Join(", ", newData.Keys.Select(k => k + "=@" + k));
                string sql = $"UPDATE {tableName} SET {setClause} WHERE {idColumn}=@id";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    foreach (var kvp in newData)
                        cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id", idValue);

                    cmd.ExecuteNonQuery();
                }

                // Log với chi tiết thay đổi
                if (oldData != null)
                {
                    var changes = CompareDictionaries(oldData, newData);
                    var desc = string.Join(", ", changes.Select(c => $"\"{c.Key}\": \"{c.Value.Old}\" → \"{c.Value.New}\""));
                    LogSafe(() => _audit.LogUpdate(tableName, idValue.ToString(), desc));
                }
                else
                {
                    LogSafe(() => _audit.LogUpdate(tableName, idValue.ToString()));
                }
            }
        }

        // ==================== DELETE ====================

        /// <summary>
        /// Xóa một dòng theo ID và tự động log DELETE.
        /// </summary>
        public void Delete(string tableName, string idColumn, object idValue)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                string sql = $"DELETE FROM {tableName} WHERE {idColumn}=@id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idValue);
                    cmd.ExecuteNonQuery();
                }

                LogSafe(() => _audit.LogDelete(tableName, idValue.ToString()));
            }
        }

        // ==================== SELECT ====================

        /// <summary>
        /// Lấy một dòng theo ID.
        /// </summary>
        public Dictionary<string, object> GetById(string tableName, string idColumn, object idValue)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                string sql = $"SELECT * FROM {tableName} WHERE {idColumn}=@id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idValue);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return ReadRow(reader);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Lấy tất cả dòng.
        /// </summary>
        public List<Dictionary<string, object>> GetAll(string tableName)
        {
            var result = new List<Dictionary<string, object>>();
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                string sql = $"SELECT * FROM {tableName}";
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add(ReadRow(reader));
                }
            }
            return result;
        }

        /// <summary>
        /// Tìm kiếm với điều kiện WHERE.
        /// </summary>
        public List<Dictionary<string, object>> Search(string tableName, string whereClause, params SqlParameter[] parameters)
        {
            var result = new List<Dictionary<string, object>>();
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                string sql = $"SELECT * FROM {tableName} WHERE {whereClause}";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            result.Add(ReadRow(reader));
                    }
                }
            }
            return result;
        }

        // ==================== HELPERS ====================

        private Dictionary<string, object> ReadRow(SqlDataReader reader)
        {
            var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
            }
            return row;
        }

        private Dictionary<string, (string Old, string New)> CompareDictionaries(
            Dictionary<string, object> oldDict, Dictionary<string, object> newDict)
        {
            var result = new Dictionary<string, (string, string)>();

            var allKeys = oldDict.Keys.Union(newDict.Keys);
            foreach (var key in allKeys)
            {
                var oldVal = oldDict.TryGetValue(key, out var ov) ? (ov?.ToString() ?? "") : "";
                var newVal = newDict.TryGetValue(key, out var nv) ? (nv?.ToString() ?? "") : "";

                if (oldVal != newVal)
                    result[key] = (oldVal, newVal);
            }
            return result;
        }

        private void LogSafe(Action action)
        {
            try { action(); } catch { }
        }

        // ==================== STATIC INSTANCE ====================

        private static CrudService _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Lấy instance duy nhất của CrudService.
        /// </summary>
        public static CrudService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new CrudService();
                    }
                }
                return _instance;
            }
        }
    }
}
