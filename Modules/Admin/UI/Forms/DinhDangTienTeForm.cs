using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharkTank.DAL;
using SharkTank.DAL.Sql;
using SharkTank.Core.Models;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class DinhDangTienTeForm : UserControl
    {
        private readonly ISystemConfigRepository _configRepo;
        private List<SystemConfig> _allConfigs;

        public DinhDangTienTeForm()
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

                // Currency tab
                SetCombo(cboTienTeMacDinh, GetConfig("DefaultCurrency"));
                SetCombo(cboSoThapPhan, GetConfig("DecimalPlaces"));
                txtKyHieuNgang.Text = GetConfig("ThousandSeparator");
                txtKyHieuDoc.Text = GetConfig("DecimalSeparator");
                chkHienThiKyHieu.Checked = GetConfig("ShowCurrencySymbol") == "1";

                // Date tab
                SetCombo(cboDinhDangNgay, GetConfig("DateFormat"));
                SetCombo(cboDinhDangThoiGian, GetConfig("TimeFormat"));
                chkDungMuiGio.Checked = GetConfig("UseTimeZone") == "1";
                SetCombo(cboMuaChuan, GetConfig("TimeZone"));
                SetCombo(cboNgayBatDauTuan, GetConfig("WeekStartDay"));

                // Number tab
                SetCombo(cboKyHieuThapPhan, GetConfig("NumberDecimalSeparator"));
                SetCombo(cboKyHieuNganCach, GetConfig("NumberThousandSeparator"));
                chkLamTron.Checked = GetConfig("AutoRoundNumbers") == "1";

                CapNhatMauTienTe();
                CapNhatMauNgay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetConfig(string key)
        {
            return _allConfigs?.FirstOrDefault(c => c.ConfigKey == key)?.ConfigValue ?? "";
        }

        private void SetCombo(ComboBox cbo, string value)
        {
            if (cbo.Items.Count == 0)
                return;

            // Giá trị lưu trong DB có thể là chỉ số (0,1,2...) hoặc chuỗi (VND, Monday...)
            if (int.TryParse(value, out int idx) && idx >= 0 && idx < cbo.Items.Count)
            {
                cbo.SelectedIndex = idx;
                return;
            }

            if (string.IsNullOrEmpty(value))
            {
                cbo.SelectedIndex = 0;
                return;
            }

            for (int i = 0; i < cbo.Items.Count; i++)
            {
                var item = cbo.Items[i].ToString();
                if (item.Contains(value) || item.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                {
                    cbo.SelectedIndex = i;
                    return;
                }
            }

            if (cbo.SelectedIndex < 0)
                cbo.SelectedIndex = 0;
        }

        private void SaveConfig(string key, string value)
        {
            var config = _allConfigs.FirstOrDefault(c => c.ConfigKey == key);
            if (config != null)
            {
                config.ConfigValue = value;
                _configRepo.Save(config);
            }
            else
            {
                // Create new if not exists
                config = new SystemConfig
                {
                    ConfigKey = key,
                    ConfigValue = value,
                    ConfigGroup = "Format",
                    DataType = "string"
                };
                _configRepo.Save(config);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Currency
                SaveConfig("DefaultCurrency", cboTienTeMacDinh.SelectedIndex.ToString());
                SaveConfig("DecimalPlaces", cboSoThapPhan.SelectedIndex.ToString());
                SaveConfig("ThousandSeparator", txtKyHieuNgang.Text);
                SaveConfig("DecimalSeparator", txtKyHieuDoc.Text);
                SaveConfig("ShowCurrencySymbol", chkHienThiKyHieu.Checked ? "1" : "0");

                // Date
                SaveConfig("DateFormat", cboDinhDangNgay.SelectedIndex.ToString());
                SaveConfig("TimeFormat", cboDinhDangThoiGian.SelectedIndex.ToString());
                SaveConfig("UseTimeZone", chkDungMuiGio.Checked ? "1" : "0");
                SaveConfig("TimeZone", cboMuaChuan.SelectedIndex.ToString());
                SaveConfig("WeekStartDay", cboNgayBatDauTuan.SelectedIndex.ToString());

                // Number
                SaveConfig("NumberDecimalSeparator", cboKyHieuThapPhan.SelectedIndex.ToString());
                SaveConfig("NumberThousandSeparator", cboKyHieuNganCach.SelectedIndex.ToString());
                SaveConfig("AutoRoundNumbers", chkLamTron.Checked ? "1" : "0");

                _allConfigs = _configRepo.GetAll().ToList();
                MessageBox.Show(
                    "Đã lưu định dạng tiền tệ / ngày giờ lên SQL (" + DateTime.Now.ToString("HH:mm:ss") + ").\n\n" +
                    "Đóng rồi mở lại form này để kiểm tra dữ liệu đọc lại từ DB.\n" +
                    "Các màn hình khác trong app sẽ dùng các giá trị này khi bạn gắn code đọc SystemConfigs.",
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
            var result = MessageBox.Show("Khôi phục mặc định?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cboTienTeMacDinh.SelectedIndex = 0;
                cboSoThapPhan.SelectedIndex = 2;
                txtKyHieuNgang.Text = ",";
                txtKyHieuDoc.Text = ".";
                chkHienThiKyHieu.Checked = true;

                cboDinhDangNgay.SelectedIndex = 0;
                cboDinhDangThoiGian.SelectedIndex = 0;
                chkDungMuiGio.Checked = true;
                cboMuaChuan.SelectedIndex = 0;
                cboNgayBatDauTuan.SelectedIndex = 0;

                cboKyHieuThapPhan.SelectedIndex = 0;
                cboKyHieuNganCach.SelectedIndex = 0;
                chkLamTron.Checked = true;

                CapNhatMauTienTe();
                CapNhatMauNgay();

                MessageBox.Show("Đã khôi phục mặc định!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboTienTeMacDinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatMauTienTe();
        }

        private void cboSoThapPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatMauTienTe();
        }

        private void cboDinhDangNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatMauNgay();
        }

        private void CapNhatMauTienTe()
        {
            if (cboTienTeMacDinh.SelectedIndex < 0) return;

            string symbol = "";
            switch (cboTienTeMacDinh.SelectedIndex)
            {
                case 0: symbol = "₫"; break;
                case 1: symbol = "$"; break;
                case 2: symbol = "€"; break;
                case 3: symbol = "¥"; break;
            }

            string decimals = new string('0', cboSoThapPhan.SelectedIndex + 1);
            lblMauTienTe.Text = $"VD: {symbol} 1{','}{decimals}00";
        }

        private void CapNhatMauNgay()
        {
            if (cboDinhDangNgay.SelectedIndex < 0) return;

            DateTime now = DateTime.Now;
            switch (cboDinhDangNgay.SelectedIndex)
            {
                case 0: lblMauNgay.Text = now.ToString("dd/MM/yyyy"); break;
                case 1: lblMauNgay.Text = now.ToString("MM/dd/yyyy"); break;
                case 2: lblMauNgay.Text = now.ToString("yyyy-MM-dd"); break;
                case 3: lblMauNgay.Text = now.ToString("dd-MM-yyyy"); break;
            }
        }
    }
}
