using System;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using SharkTank.Core.Models;
using SharkTank.DAL;
using SharkTank.DAL.Sql;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class CauHinhEmailForm : UserControl
    {
        private readonly ISystemConfigRepository _configRepo;
        private Label _lblDbTrangThai;

        public CauHinhEmailForm()
        {
            InitializeComponent();
            _configRepo = new SqlSystemConfigRepository(SqlConnectionFactory.Create());
            ThemNhanTrangThai();
            LoadData();
        }

        private void ThemNhanTrangThai()
        {
            _lblDbTrangThai = new Label
            {
                AutoSize = true,
                Location = new Point(20, 448),
                Font = new Font("Segoe UI", 8.25f, FontStyle.Italic),
                ForeColor = Color.DimGray
            };
            Controls.Add(_lblDbTrangThai);
        }

        private void CapNhatTrangThai(string text, Color? mau = null)
        {
            if (_lblDbTrangThai == null) return;
            _lblDbTrangThai.Text = text;
            _lblDbTrangThai.ForeColor = mau ?? Color.DimGray;
        }

        private void LoadData()
        {
            try
            {
                txtSmtpServer.Text = GetConfig("SmtpServer");
                var port = GetConfig("SmtpPort");
                if (int.TryParse(port, out int p))
                    numSmtpPort.Value = Math.Max(numSmtpPort.Minimum, Math.Min(numSmtpPort.Maximum, p));

                SetComboAnToan(cboEncryption, GetConfig("SmtpEncryption"));
                txtEmail.Text = GetConfig("EmailFrom");
                txtUsername.Text = GetConfig("EmailUsername");
                txtPassword.Text = GetConfig("EmailPassword");
                chkUseDefaultCredentials.Checked = GetConfig("UseDefaultCredentials") == "1";
                UpdateCredentialsEnabled();

                CapNhatTrangThai("Đã tải cấu hình từ SQL — " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));
                lblStatus.Text = "Sẵn sàng. Điền email test và bấm Gửi thử để kiểm tra SMTP thật.";
                lblStatus.ForeColor = Color.DimGray;
            }
            catch (Exception ex)
            {
                CapNhatTrangThai("Lỗi tải: " + ex.Message, Color.Red);
                MessageBox.Show("Lỗi tải cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetConfig(string key)
        {
            var config = _configRepo.GetByKey(key);
            return config?.ConfigValue ?? "";
        }

        /// <summary>
        /// Không bao giờ gán SelectedIndex khi Items rỗng (tránh lỗi WinForms).
        /// </summary>
        private static void SetComboAnToan(ComboBox cbo, string value)
        {
            if (cbo.Items.Count == 0)
                return;

            for (int i = 0; i < cbo.Items.Count; i++)
            {
                if (string.Equals(cbo.Items[i].ToString(), value, StringComparison.OrdinalIgnoreCase))
                {
                    cbo.SelectedIndex = i;
                    return;
                }
            }

            cbo.SelectedIndex = 0;
        }

        private void SaveConfig(string key, string value)
        {
            var config = new SystemConfig
            {
                ConfigKey = key,
                ConfigValue = value ?? "",
                ConfigGroup = "Email",
                DataType = "string"
            };
            _configRepo.Save(config);
        }

        private void UpdateCredentialsEnabled()
        {
            txtUsername.Enabled = !chkUseDefaultCredentials.Checked;
            txtPassword.Enabled = !chkUseDefaultCredentials.Checked;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email gửi (From) không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveConfig("SmtpServer", txtSmtpServer.Text.Trim());
                SaveConfig("SmtpPort", ((int)numSmtpPort.Value).ToString());
                SaveConfig("SmtpEncryption", cboEncryption.SelectedItem?.ToString() ?? "TLS");
                SaveConfig("EmailFrom", txtEmail.Text.Trim());
                SaveConfig("EmailUsername", txtUsername.Text.Trim());
                SaveConfig("EmailPassword", txtPassword.Text ?? "");
                SaveConfig("UseDefaultCredentials", chkUseDefaultCredentials.Checked ? "1" : "0");

                CapNhatTrangThai("Đã lưu lên SQL — " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Color.DarkGreen);
                MessageBox.Show(
                    "Đã lưu cấu hình email lên SQL.\n\n" +
                    "Cách kiểm tra:\n" +
                    "• Xem dòng trạng thái dưới cùng (thời gian lưu).\n" +
                    "• Đóng form, mở lại — dữ liệu phải giống vừa lưu.\n" +
                    "• Tab \"Gửi thử email\" — bấm Gửi thử để test SMTP thật.",
                    "Đã lưu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                CapNhatTrangThai("Lỗi lưu: " + ex.Message, Color.Red);
                MessageBox.Show("Lỗi lưu cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuiThu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email gửi (From)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtToEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email người nhận để test!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGuiThu.Enabled = false;
            lblStatus.Text = "Đang gửi qua SMTP...";
            lblStatus.ForeColor = Color.Orange;
            Application.DoEvents();

            try
            {
                var enc = (cboEncryption.SelectedItem?.ToString() ?? "TLS").Trim();
                bool useSsl = !string.Equals(enc, "None", StringComparison.OrdinalIgnoreCase);

                using (var client = new SmtpClient(txtSmtpServer.Text.Trim(), (int)numSmtpPort.Value))
                {
                    client.EnableSsl = useSsl;
                    client.Timeout = 30000;

                    if (chkUseDefaultCredentials.Checked)
                        client.UseDefaultCredentials = true;
                    else
                    {
                        client.UseDefaultCredentials = false;
                        var user = string.IsNullOrWhiteSpace(txtUsername.Text) ? txtEmail.Text.Trim() : txtUsername.Text.Trim();
                        var pass = txtPassword.Text ?? "";
                        client.Credentials = new NetworkCredential(user, pass);
                    }

                    using (var message = new MailMessage())
                    {
                        message.From = new MailAddress(txtEmail.Text.Trim());
                        message.To.Add(txtToEmail.Text.Trim());
                        message.Subject = "[SharkTank] Kiểm tra cấu hình email";
                        message.Body = "Đây là email thử từ SharkTank ERP.\nThời gian: " + DateTime.Now.ToString("O");
                        message.IsBodyHtml = false;

                        client.Send(message);
                    }
                }

                lblStatus.Text = "Gửi thử thành công — kiểm tra hộp thư đến (và thư mục Spam).";
                lblStatus.ForeColor = Color.Green;
                MessageBox.Show("Gửi email thử thành công.\nKiểm tra hộp thư đến của " + txtToEmail.Text.Trim() + " (kể cả Spam).", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SmtpException ex)
            {
                lblStatus.Text = "SMTP lỗi: " + ex.Message;
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show(
                    "Không gửi được email.\n\n" +
                    "Thường gặp với Gmail: bật \"Mật khẩu ứng dụng\" (App Password), không dùng mật khẩu đăng nhập thường.\n\n" +
                    "Chi tiết: " + ex.Message,
                    "Lỗi SMTP",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi: " + ex.Message;
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuiThu.Enabled = true;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Tải lại dữ liệu từ SQL (bỏ thay đổi chưa lưu)?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                LoadData();
        }

        private void chkUseDefaultCredentials_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCredentialsEnabled();
        }

        private void CauHinhEmailForm_Load(object sender, EventArgs e)
        {

        }
    }
}
