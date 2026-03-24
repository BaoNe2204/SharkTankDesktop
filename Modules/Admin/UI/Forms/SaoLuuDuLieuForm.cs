using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.Core.Models;
using SharkTank.DAL;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class SaoLuuDuLieuForm : UserControl
    {
        private string _backupFolder;
        private readonly IBackupService _backupService;

        public SaoLuuDuLieuForm()
        {
            InitializeComponent();
            _backupService = new BackupService();
            _backupFolder = GetDefaultBackupFolder();
            txtThuMuc.Text = _backupFolder;
            cboLoaiDuLieu.SelectedIndex = 0;
            GenerateDefaultFileName();
        }

        private string GetDefaultBackupFolder()
        {
            var config = ThemeService.Instance;
            var folder = config.Get("BackupFolder");
            if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
            {
                folder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "SharkTank", "Backup");
            }
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }

        private void GenerateDefaultFileName()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            txtTenFile.Text = $"Backup_SharkTank_{timestamp}.bak";
        }

        private void SaoLuuForm_Load(object sender, EventArgs e)
        {
            LoadBackupHistory();
        }

        private void LoadBackupHistory()
        {
            try
            {
                dgvLichSu.Rows.Clear();

                if (!Directory.Exists(_backupFolder))
                    return;

                var files = Directory.GetFiles(_backupFolder, "*.bak")
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.CreationTime)
                    .Take(50)
                    .ToList();

                foreach (var file in files)
                {
                    dgvLichSu.Rows.Add(
                        file.Name,
                        file.CreationTime.ToString("dd/MM/yyyy HH:mm:ss"),
                        FormatFileSize(file.Length)
                    );
                }

                lblTrangThai.Text = $"Tìm thấy {files.Count} file backup.";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = "Lỗi tải lịch sử: " + ex.Message;
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = bytes;
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }
            return $"{size:0.##} {sizes[order]}";
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Chọn thư mục lưu file backup";
                dialog.SelectedPath = _backupFolder;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _backupFolder = dialog.SelectedPath;
                    txtThuMuc.Text = _backupFolder;
                    LoadBackupHistory();
                }
            }
        }

        private async void btnSaoLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenFile.Text))
            {
                MessageBox.Show("Vui lòng nhập tên file backup!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenFile.Focus();
                return;
            }

            if (!txtTenFile.Text.EndsWith(".bak", StringComparison.OrdinalIgnoreCase))
            {
                txtTenFile.Text += ".bak";
            }

            string filePath = Path.Combine(_backupFolder, txtTenFile.Text);

            if (File.Exists(filePath))
            {
                var result = MessageBox.Show($"File '{txtTenFile.Text}' đã tồn tại!\nBạn có muốn ghi đè không?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
            }

            btnSaoLuu.Enabled = false;
            progressBar.Value = 0;
            lblTrangThai.Text = "Đang sao lưu dữ liệu...";

            try
            {
                string dataType = cboLoaiDuLieu.SelectedItem?.ToString() ?? "Toàn bộ dữ liệu";

                var task = _backupService.CreateBackupAsync(_backupFolder, txtTenFile.Text, dataType, (progress, message) =>
                {
                    this.Invoke(new Action(() =>
                    {
                        progressBar.Value = Math.Min(progress, 100);
                        lblTrangThai.Text = message;
                    }));
                });

                bool success = await task;

                if (success)
                {
                    lblTrangThai.Text = "Sao lưu thành công!";
                    LoadBackupHistory();
                    GenerateDefaultFileName();

                    AuditService.CreateDefault().LogAction("BACKUP", "SaoLuuDuLieuForm",
                        $"Đã tạo backup: {txtTenFile.Text} ({dataType})", "Backup", "Thành công");

                    MessageBox.Show("Sao lưu dữ liệu thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = "Lỗi: " + ex.Message;

                AuditService.CreateDefault().LogAction("BACKUP_FAILED", "SaoLuuDuLieuForm",
                    $"Backup thất bại: {ex.Message}", "Backup", "Thất bại");

                MessageBox.Show("Sao lưu thất bại!\n\nChi tiết: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSaoLuu.Enabled = true;
                progressBar.Value = 0;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadBackupHistory();
        }

        private void btnXoaBackup_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn file backup cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = dgvLichSu.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(fileName))
                return;

            var result = MessageBox.Show($"Bạn có chắc muốn xóa file backup '{fileName}'?\n\nHành động này không thể hoàn tác!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string filePath = Path.Combine(_backupFolder, fileName);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);

                        AuditService.CreateDefault().LogAction("DELETE_BACKUP", "SaoLuuDuLieuForm",
                            $"Đã xóa backup: {fileName}", "Backup", "Xóa");

                        MessageBox.Show("Đã xóa file backup!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadBackupHistory();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa file!\n\nChi tiết: " + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
