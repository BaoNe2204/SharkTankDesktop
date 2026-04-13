using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class GuiKH : Form
    {
        private string _duongDanFile;

        public GuiKH(string emailKhach, string tenKhach, string maChungTu, string duongDanFile)
        {
            InitializeComponent();
            _duongDanFile = duongDanFile;

            txtEmailTo.Text = emailKhach;
            txtTieuDe.Text = $"[SharkTank ERP] Gửi chứng từ {maChungTu}";
            txtFileDinhKem.Text = duongDanFile;

            txtNoiDung.Text = $"Kính gửi anh/chị {tenKhach},\r\n\r\n" +
                              "Cảm ơn anh/chị đã tin tưởng và sử dụng dịch vụ của SharkTank.\r\n" +
                              $"Chúng tôi xin gửi đính kèm chứng từ {maChungTu} trong email này.\r\n\r\n" +
                              "Trân trọng,\r\nPhòng Kinh Doanh - SharkTank ERP";

            btnHuy.Click += (s, e) => this.Close();
            btnGui.Click += BtnGui_Click;
        }

        private void BtnGui_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmailTo.Text))
            {
                MessageBox.Show("Vui lòng nhập Email người nhận!", "Lưu ý");
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                string emailNguoiGui = "emailcuaban@gmail.com";
                string matKhauUngDung = "xxxx yyyy zzzz wwww";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailNguoiGui, "SharkTank ERP System");
                mail.To.Add(txtEmailTo.Text);
                mail.Subject = txtTieuDe.Text;
                mail.Body = txtNoiDung.Text;

                if (!string.IsNullOrEmpty(_duongDanFile) && System.IO.File.Exists(_duongDanFile))
                {
                    mail.Attachments.Add(new Attachment(_duongDanFile));
                }
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential(emailNguoiGui, matKhauUngDung);
                smtp.EnableSsl = true;

                smtp.Send(mail);

                this.Cursor = Cursors.Default;
                MessageBox.Show("Đã gửi Email thành công cho khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Lỗi khi gửi mail: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}