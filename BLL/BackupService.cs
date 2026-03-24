using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace SharkTank.BLL
{
    public class BackupService : DAL.IBackupService
    {
        private readonly string _connectionString;

        public BackupService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SharkTankDB"]?.ConnectionString;
        }

        private string GetDatabaseName()
        {
            if (string.IsNullOrEmpty(_connectionString))
                return "SharkTankDB";

            var builder = new SqlConnectionStringBuilder(_connectionString);
            return builder.InitialCatalog;
        }

        /// <summary>RESTORE/ALTER DATABASE phải chạy trên session kết nối tới master, không phải DB đang restore.</summary>
        private string GetMasterConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
                return null;
            var builder = new SqlConnectionStringBuilder(_connectionString);
            builder.InitialCatalog = "master";
            return builder.ConnectionString;
        }

        public async Task<bool> CreateBackupAsync(string folder, string fileName, string dataType, Action<int, string> progressCallback)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        progressCallback?.Invoke(0, "Không tìm thấy kết nối database!");
                        return false;
                    }

                    progressCallback?.Invoke(5, "Bắt đầu sao lưu dữ liệu...");

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string backupPath = Path.Combine(folder, fileName);
                    if (!backupPath.EndsWith(".bak", StringComparison.OrdinalIgnoreCase))
                        backupPath = Path.ChangeExtension(backupPath, ".bak");

                    if (File.Exists(backupPath))
                        File.Delete(backupPath);

                    progressCallback?.Invoke(20, "Đang kết nối SQL Server...");
                    string dbName = GetDatabaseName();

                    progressCallback?.Invoke(40, "Đang sao lưu database...");

                    using (var conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();

                        // Không dùng COMPRESSION — SQL Server Express không hỗ trợ
                        string backupSql = $@"
                            BACKUP DATABASE [{dbName}]
                            TO DISK = @BackupPath
                            WITH FORMAT, INIT,
                                NAME = N'SharkTank-Full-Backup',
                                DESCRIPTION = N'SharkTank ERP Full Backup - {dataType}'";

                        using (var cmd = new SqlCommand(backupSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@BackupPath", backupPath);
                            cmd.CommandTimeout = 300;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    progressCallback?.Invoke(85, "Đang tạo metadata...");

                    // Save metadata alongside backup
                    string metaPath = Path.ChangeExtension(backupPath, ".txt");
                    File.WriteAllText(metaPath, $"BackupType={dataType}\nCreatedAt={DateTime.Now:yyyy-MM-dd HH:mm:ss}\nDatabase={dbName}\nVersion=1.0.0");

                    progressCallback?.Invoke(100, "Sao lưu hoàn tất!");
                    return true;
                }
                catch (Exception ex)
                {
                    progressCallback?.Invoke(0, "Lỗi: " + ex.Message);
                    return false;
                }
            });
        }

        public async Task<bool> RestoreBackupAsync(string filePath, bool overwrite, Action<int, string> progressCallback)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        progressCallback?.Invoke(0, "Không tìm thấy kết nối database!");
                        return false;
                    }

                    if (!File.Exists(filePath))
                    {
                        progressCallback?.Invoke(0, "File backup không tồn tại!");
                        return false;
                    }

                    progressCallback?.Invoke(5, "Bắt đầu khôi phục dữ liệu...");

                    string dbName = GetDatabaseName();

                    progressCallback?.Invoke(20, "Đang kiểm tra file backup...");

                    if (File.Exists(filePath.Replace(".bak", ".txt")))
                    {
                        var meta = File.ReadAllLines(filePath.Replace(".bak", ".txt"));
                        foreach (var line in meta)
                        {
                            if (line.StartsWith("BackupType="))
                            {
                                progressCallback?.Invoke(30, $"Loại backup: {line.Substring(11)}");
                            }
                        }
                    }

                    progressCallback?.Invoke(40, "Đang kết nối SQL Server (master)...");

                    string masterCs = GetMasterConnectionString();
                    if (string.IsNullOrEmpty(masterCs))
                    {
                        progressCallback?.Invoke(0, "Không tạo được chuỗi kết nối master.");
                        return false;
                    }

                    using (var conn = new SqlConnection(masterCs))
                    {
                        conn.Open();

                        progressCallback?.Invoke(55, "Đang khóa database...");

                        using (var cmd = new SqlCommand(
                            $"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", conn))
                        {
                            cmd.CommandTimeout = 120;
                            cmd.ExecuteNonQuery();
                        }

                        progressCallback?.Invoke(65, "Đang khôi phục database...");

                        string restoreSql = $@"
                            RESTORE DATABASE [{dbName}]
                            FROM DISK = @RestorePath
                            WITH REPLACE, NOUNLOAD, STATS = 10";

                        using (var cmd = new SqlCommand(restoreSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@RestorePath", filePath);
                            cmd.CommandTimeout = 300;
                            cmd.ExecuteNonQuery();
                        }

                        progressCallback?.Invoke(90, "Đang mở khóa database...");

                        using (var cmd = new SqlCommand(
                            $"ALTER DATABASE [{dbName}] SET MULTI_USER", conn))
                        {
                            cmd.CommandTimeout = 120;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    progressCallback?.Invoke(100, "Khôi phục hoàn tất!");
                    return true;
                }
                catch (Exception ex)
                {
                    // Try to restore multi-user mode on error
                    try
                    {
                        string masterCs = GetMasterConnectionString();
                        if (!string.IsNullOrEmpty(masterCs))
                        {
                            using (var conn = new SqlConnection(masterCs))
                            {
                                conn.Open();
                                string dbName = GetDatabaseName();
                                using (var cmd = new SqlCommand(
                                    $"ALTER DATABASE [{dbName}] SET MULTI_USER", conn))
                                {
                                    cmd.CommandTimeout = 120;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    catch { }

                    progressCallback?.Invoke(0, "Lỗi: " + ex.Message);
                    return false;
                }
            });
        }
    }
}
