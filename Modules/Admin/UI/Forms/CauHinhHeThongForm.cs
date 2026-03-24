using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.DAL;
using SharkTank.DAL.Sql;
using SharkTank.Core.Models;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class CauHinhHeThongForm : UserControl
    {
        private readonly ISystemConfigRepository _configRepo;
        private List<SystemConfig> _allConfigs;

        public CauHinhHeThongForm()
        {
            InitializeComponent();
            _configRepo = new SqlSystemConfigRepository(SqlConnectionFactory.Create());
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _allConfigs = _configRepo.GetAll().ToList();

                // General tab
                txtTenUngDung.Text = GetValue("AppName");
                txtMoTa.Text = GetValue("AppDescription");
                SetValue("DefaultLanguage", cboNgongNgu);
                SetValue("AutoBackup", chkAutoBackup);
                SetValue("BackupInterval", numBackupInterval);

                // Security tab
                SetValue("PasswordMinLength", numMinLength);
                SetValue("PasswordRequireUppercase", chkYeuCauChuHoa);
                SetValue("PasswordRequireLowercase", chkYeuCauChuThuong);
                SetValue("PasswordRequireDigit", chkYeuCauSo);
                SetValue("PasswordRequireSpecial", chkYeuCauKyTuDacBiet);
                SetValue("PasswordExpiryDays", numHanHieuLuc);
                SetValue("LockAccountOnFail", chkKhoaTaiKhoan);
                SetValue("MaxLoginAttempts", numSoLanThatBai);

                // Display tab
                SetValue("ThemeColor", cboMauNen);
                SetValue("ThemeStyle", cboPhongCach);
                SetValue("ShowUsername", chkHienThiTen);
                SetValue("ShowAvatar", chkHienThiAvatar);
                SetValue("FontSize", numFontSize);

                // Version readonly
                var ver = GetValue("AppVersion");
                if (!string.IsNullOrEmpty(ver)) txtVersion.Text = ver;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetValue(string key)
        {
            return _allConfigs?.FirstOrDefault(c => c.ConfigKey == key)?.ConfigValue ?? "";
        }

        private void SetValue(string key, TextBox txt)
        {
            txt.Text = GetValue(key);
        }

        private void SetValue(string key, ComboBox cbo)
        {
            var val = GetValue(key);
            if (cbo.Items.Count == 0)
                return;

            if (key == "ThemeColor")
            {
                string[] colors = { "Blue", "Dark", "Light", "Green" };
                int idx = Array.IndexOf(colors, val);
                cbo.SelectedIndex = idx >= 0 ? idx : 0;
            }
            else if (key == "ThemeStyle")
            {
                string[] styles = { "Modern", "Classic", "Minimal" };
                int idx = Array.IndexOf(styles, val);
                cbo.SelectedIndex = idx >= 0 ? idx : 0;
            }
            else if (key == "DefaultLanguage")
            {
                string[] langs = { "vi-VN", "en-US" };
                int idx = Array.IndexOf(langs, val);
                cbo.SelectedIndex = idx >= 0 ? idx : 0;
            }
            else
            {
                for (int i = 0; i < cbo.Items.Count; i++)
                {
                    if (cbo.Items[i].ToString().StartsWith(val) || cbo.Items[i].ToString().Contains(val))
                    {
                        cbo.SelectedIndex = i;
                        return;
                    }
                }
                if (cbo.SelectedIndex < 0)
                    cbo.SelectedIndex = 0;
            }
        }

        private void SetValue(string key, CheckBox chk)
        {
            chk.Checked = GetValue(key) == "1";
        }

        private void SetValue(string key, NumericUpDown num)
        {
            if (int.TryParse(GetValue(key), out int val))
            {
                num.Value = Math.Max(num.Minimum, Math.Min(num.Maximum, val));
            }
        }

        private void SaveValue(string key, TextBox txt)
        {
            SaveConfig(key, txt.Text);
        }

        private void SaveValue(string key, ComboBox cbo)
        {
            string value;
            if (key == "ThemeColor")
            {
                // Combo: 0=Xanh dương, 1=Tối, 2=Sáng, 3=Xanh lá
                string[] colors = { "Blue", "Dark", "Light", "Green" };
                value = colors[Math.Min(cbo.SelectedIndex, colors.Length - 1)];
            }
            else if (key == "ThemeStyle")
            {
                // Combo: 0=Modern, 1=Classic, 2=Minimal
                string[] styles = { "Modern", "Classic", "Minimal" };
                value = styles[Math.Min(cbo.SelectedIndex, styles.Length - 1)];
            }
            else if (key == "DefaultLanguage")
            {
                // Combo: 0=Tiếng Việt(vi-VN), 1=English(en-US)
                string[] langs = { "vi-VN", "en-US" };
                value = langs[Math.Min(cbo.SelectedIndex, langs.Length - 1)];
            }
            else
            {
                value = cbo.SelectedIndex.ToString();
            }
            SaveConfig(key, value);
        }

        private void SaveValue(string key, CheckBox chk)
        {
            SaveConfig(key, chk.Checked ? "1" : "0");
        }

        private void SaveValue(string key, NumericUpDown num)
        {
            SaveConfig(key, ((int)num.Value).ToString());
        }

        private void SaveConfig(string key, string value)
        {
            var config = _allConfigs.FirstOrDefault(c => c.ConfigKey == key);
            if (config != null)
            {
                string oldVal = config.ConfigValue;
                if (string.Equals(oldVal ?? "", value ?? "", StringComparison.Ordinal))
                    return;

                config.ConfigValue = value;
                _configRepo.Save(config);
                TryLogConfigChange("UPDATE", key, oldVal ?? "", value ?? "");
            }
            else
            {
                var neu = new SystemConfig
                {
                    ConfigKey = key,
                    ConfigValue = value,
                    ConfigGroup = "General",
                    DataType = "string"
                };
                _configRepo.Save(neu);
                _allConfigs.Add(neu);
                TryLogConfigChange("CREATE", key, "", value ?? "");
            }
        }

        private static void TryLogConfigChange(string auditAction, string key, string oldVal, string newVal)
        {
            try
            {
                AuditService.CreateDefault().LogDataChangeRow(
                    auditAction,
                    "SystemConfigs",
                    key,
                    key,
                    oldVal,
                    newVal,
                    auditAction == "CREATE" ? "INSERT" : "UPDATE",
                    "Cấu hình hệ thống");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Audit config: " + ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // General
                SaveValue("AppName", txtTenUngDung);
                SaveValue("AppDescription", txtMoTa);
                SaveValue("DefaultLanguage", cboNgongNgu);
                SaveValue("AutoBackup", chkAutoBackup);
                SaveValue("BackupInterval", numBackupInterval);

                // Security
                SaveValue("PasswordMinLength", numMinLength);
                SaveValue("PasswordRequireUppercase", chkYeuCauChuHoa);
                SaveValue("PasswordRequireLowercase", chkYeuCauChuThuong);
                SaveValue("PasswordRequireDigit", chkYeuCauSo);
                SaveValue("PasswordRequireSpecial", chkYeuCauKyTuDacBiet);
                SaveValue("PasswordExpiryDays", numHanHieuLuc);
                SaveValue("LockAccountOnFail", chkKhoaTaiKhoan);
                SaveValue("MaxLoginAttempts", numSoLanThatBai);

                // Display
                SaveValue("ThemeColor", cboMauNen);
                SaveValue("ThemeStyle", cboPhongCach);
                SaveValue("ShowUsername", chkHienThiTen);
                SaveValue("ShowAvatar", chkHienThiAvatar);
                SaveValue("FontSize", numFontSize);

                // Reload ThemeService → bắn OnThemeChanged → MainDashboard.ApplyTheme() ngay lập tức
                SharkTank.BLL.ThemeService.Instance.Reload();

                _allConfigs = _configRepo.GetAll().ToList();
                MessageBox.Show(
                    "Đã lưu cấu hình hệ thống lên SQL (" + DateTime.Now.ToString("HH:mm:ss") + ").\n\n" +
                    "✅ Giao diện đã được cập nhật ngay — màu nền, font sẽ thay đổi tức thì!",
                    "Đã lưu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Khôi phục cấu hình mặc định?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Reset to defaults
                txtTenUngDung.Text = "SharkTank ERP";
                txtMoTa.Text = "";
                cboNgongNgu.SelectedIndex = 0;
                chkAutoBackup.Checked = true;
                numBackupInterval.Value = 7;
                numMinLength.Value = 8;
                chkYeuCauChuHoa.Checked = true;
                chkYeuCauChuThuong.Checked = true;
                chkYeuCauSo.Checked = true;
                chkYeuCauKyTuDacBiet.Checked = false;
                numHanHieuLuc.Value = 90;
                chkKhoaTaiKhoan.Checked = true;
                numSoLanThatBai.Value = 5;
                cboMauNen.SelectedIndex = 0;
                cboPhongCach.SelectedIndex = 0;
                chkHienThiTen.Checked = true;
                chkHienThiAvatar.Checked = true;
                numFontSize.Value = 10;

                MessageBox.Show("Đã khôi phục cấu hình mặc định!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThemNgonNgu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng thêm ngôn ngữ đang phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoaNgonNgu_Click(object sender, EventArgs e)
        {
            if (dgvNgonNgu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ngôn ngữ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Xóa ngôn ngữ đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dgvNgonNgu.Rows.RemoveAt(dgvNgonNgu.SelectedRows[0].Index);
            }
        }

        private void CauHinhHeThongForm_Load(object sender, EventArgs e)
        {

        }
    }
}
