using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.Core.Models;
using SharkTank.DAL;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class KhoiPhucDuLieuForm : UserControl
    {
        private string _backupFolder;
        private string _selectedFilePath;
        private readonly IBackupService _backupService;

        public KhoiPhucDuLieuForm()
        {
            InitializeComponent();
            _backupService = new BackupService();
            _backupFolder = GetDefaultBackupFolder();
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

        private void KhoiPhucForm_Load(object sender, EventArgs e)
        {
            LoadBackupFiles();
        }

        private void LoadBackupFiles()
        {
            try
            {
                dgvBackupFiles.Rows.Clear();

                if (!Directory.Exists(_backupFolder))
                {
                    Directory.CreateDirectory(_backupFolder);
                    return;
                }

                var files = Directory.GetFiles(_backupFolder, "*.bak")
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.CreationTime)
                    .ToList();

                foreach (var file in files)
                {
                    dgvBackupFiles.Rows.Add(
                        file.Name,
                        file.CreationTime.ToString("dd/MM/yyyy HH:mm:ss"),
                        FormatFileSize(file.Length)
                    );
                }

                lblTrangThai.Text = $"Tìm thấy {files.Count} file backup trong thư mục.";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = "Lỗi tải danh sách: " + ex.Message;
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

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                dialog.Title = "Chọn file backup để khôi phục";
                dialog.InitialDirectory = _backupFolder;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedFilePath = dialog.FileName;
                    txtDuongDan.Text = _selectedFilePath;
                    LoadFileInfo(_selectedFilePath);
                }
            }
        }

        private void LoadFileInfo(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    txtNgayTao.Text = "(File không tồn tại)";
                    txtKichThuoc.Text = "-";
                    txtLoaiDuLieu.Text = "-";
                    return;
                }

                var fileInfo = new FileInfo(filePath);
                txtNgayTao.Text = fileInfo.CreationTime.ToString("dd/MM/yyyy HH:mm:ss");
                txtKichThuoc.Text = FormatFileSize(fileInfo.Length);

                string fileName = fileInfo.Name;
                if (fileName.Contains("CRM"))
                    txtLoaiDuLieu.Text = "Dữ liệu CRM";
                else if (fileName.Contains("Kho"))
                    txtLoaiDuLieu.Text = "Dữ liệu Kho hàng";
                else if (fileName.Contains("NhanSu") || fileName.Contains("HR"))
                    txtLoaiDuLieu.Text = "Dữ liệu Nhân sự";
                else if (fileName.Contains("TaiChinh") || fileName.Contains("Finance"))
                    txtLoaiDuLieu.Text = "Dữ liệu Tài chính";
                else
                    txtLoaiDuLieu.Text = "Toàn bộ dữ liệu";

                lblTrangThai.Text = $"Đã chọn: {fileInfo.Name}";
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = "Lỗi đọc thông tin file: " + ex.Message;
            }
        }

        private void dgvBackupFiles_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBackupFiles.SelectedRows.Count > 0)
            {
                string fileName = dgvBackupFiles.SelectedRows[0].Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(fileName))
                {
                    string filePath = Path.Combine(_backupFolder, fileName);
                    LoadFileInfo(filePath);
                }
            }
        }

        private void dgvBackupFiles_DoubleClick(object sender, EventArgs e)
        {
            if (dgvBackupFiles.SelectedRows.Count > 0)
            {
                string fileName = dgvBackupFiles.SelectedRows[0].Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(fileName))
                {
                    _selectedFilePath = Path.Combine(_backupFolder, fileName);
                    txtDuongDan.Text = _selectedFilePath;
                    LoadFileInfo(_selectedFilePath);
                }
            }
        }

        private async void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedFilePath) || !File.Exists(_selectedFilePath))
            {
                MessageBox.Show("Vui lòng chọn file backup hợp lệ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                "⚠ CẢNH BÁO: Việc khôi phục sẽ thay thế toàn bộ dữ liệu hiện tại!\n\n" +
                "Bạn có chắc chắn muốn khôi phục không?\n\n" +
                $"File: {Path.GetFileName(_selectedFilePath)}\n" +
                $"Ghi đè: " + (chkGhiDe.Checked ? "Có" : "Không"),
                "Xác nhận khôi phục",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes)
                return;

            var finalConfirm = MessageBox.Show(
                "Bạn đã chắc chắn 100% chưa?\n\n" +
                "👉 Nên sao lưu dữ liệu hiện tại trước khi khôi phục!\n" +
                "👉 Mọi dữ liệu sau ngày backup sẽ bị mất!",
                "Xác nhận cuối cùng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop);

            if (finalConfirm != DialogResult.Yes)
                return;

            btnKhoiPhuc.Enabled = false;
            btnChonFile.Enabled = false;
            progressBar.Value = 0;
            lblTrangThai.Text = "Đang khôi phục dữ liệu...";

            try
            {
                var task = _backupService.RestoreBackupAsync(_selectedFilePath, chkGhiDe.Checked, (progress, message) =>
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
                    lblTrangThai.Text = "Khôi phục thành công!";
                    LoadBackupFiles();

                    AuditService.CreateDefault().LogAction("RESTORE", "KhoiPhucDuLieuForm",
                        $"Đã khôi phục từ: {Path.GetFileName(_selectedFilePath)}", "Backup", "Thành công");

                    MessageBox.Show(
                        "Khôi phục dữ liệu thành công!\n\n" +
                        "Vui lòng khởi động lại ứng dụng để áp dụng thay đổi.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = "Lỗi: " + ex.Message;

                AuditService.CreateDefault().LogAction("RESTORE_FAILED", "KhoiPhucDuLieuForm",
                    $"Khôi phục thất bại: {ex.Message}", "Backup", "Thất bại");

                MessageBox.Show(
                    "Khôi phục thất bại!\n\nChi tiết: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnKhoiPhuc.Enabled = true;
                btnChonFile.Enabled = true;
                progressBar.Value = 0;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadBackupFiles();
        }

        private void btnXoaFile_Click(object sender, EventArgs e)
        {
            if (dgvBackupFiles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn file backup cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = dgvBackupFiles.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(fileName))
                return;

            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa file backup '{fileName}'?\n\nHành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string filePath = Path.Combine(_backupFolder, fileName);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);

                        AuditService.CreateDefault().LogAction("DELETE_BACKUP", "KhoiPhucDuLieuForm",
                            $"Đã xóa backup: {fileName}", "Backup", "Xóa");

                        MessageBox.Show("Đã xóa file backup!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadBackupFiles();

                        if (_selectedFilePath == filePath)
                        {
                            _selectedFilePath = null;
                            txtDuongDan.Text = "";
                            txtNgayTao.Text = "(Chưa chọn file)";
                            txtKichThuoc.Text = "-";
                            txtLoaiDuLieu.Text = "-";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Không thể xóa file!\n\nChi tiết: " + ex.Message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
