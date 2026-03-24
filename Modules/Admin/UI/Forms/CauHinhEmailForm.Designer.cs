using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class CauHinhEmailForm
    {
        private Label lblTitle;
        private TabControl tabEmail;
        private TabPage tabCauHinh;
        private TabPage tabMauEmail;

        // Tab Cau Hinh
        private GroupBox grpServer;
        private Label lblSmtpServer;
        private TextBox txtSmtpServer;
        private Label lblSmtpPort;
        private NumericUpDown numSmtpPort;
        private Label lblEncryption;
        private ComboBox cboEncryption;

        private GroupBox grpTaiKhoan;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblUsername;
        private TextBox txtUsername;
        private CheckBox chkUseDefaultCredentials;

        // Tab Mau Email
        private GroupBox grpTestEmail;
        private Label lblToEmail;
        private TextBox txtToEmail;
        private Button btnGuiThu;
        private Label lblStatus;

        private Button btnLuu;
        private Button btnHuy;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabEmail = new System.Windows.Forms.TabControl();
            this.tabCauHinh = new System.Windows.Forms.TabPage();
            this.grpTaiKhoan = new System.Windows.Forms.GroupBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.chkUseDefaultCredentials = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.grpServer = new System.Windows.Forms.GroupBox();
            this.cboEncryption = new System.Windows.Forms.ComboBox();
            this.lblEncryption = new System.Windows.Forms.Label();
            this.numSmtpPort = new System.Windows.Forms.NumericUpDown();
            this.lblSmtpPort = new System.Windows.Forms.Label();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.lblSmtpServer = new System.Windows.Forms.Label();
            this.tabMauEmail = new System.Windows.Forms.TabPage();
            this.grpTestEmail = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnGuiThu = new System.Windows.Forms.Button();
            this.txtToEmail = new System.Windows.Forms.TextBox();
            this.lblToEmail = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.tabEmail.SuspendLayout();
            this.tabCauHinh.SuspendLayout();
            this.grpTaiKhoan.SuspendLayout();
            this.grpServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSmtpPort)).BeginInit();
            this.tabMauEmail.SuspendLayout();
            this.grpTestEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitle.Location = new System.Drawing.Point(17, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(257, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cấu Hình Email";
            // 
            // tabEmail
            // 
            this.tabEmail.Controls.Add(this.tabCauHinh);
            this.tabEmail.Controls.Add(this.tabMauEmail);
            this.tabEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabEmail.Location = new System.Drawing.Point(17, 52);
            this.tabEmail.Name = "tabEmail";
            this.tabEmail.SelectedIndex = 0;
            this.tabEmail.Size = new System.Drawing.Size(992, 438);
            this.tabEmail.TabIndex = 1;
            // 
            // tabCauHinh
            // 
            this.tabCauHinh.Controls.Add(this.grpTaiKhoan);
            this.tabCauHinh.Controls.Add(this.grpServer);
            this.tabCauHinh.Location = new System.Drawing.Point(4, 24);
            this.tabCauHinh.Name = "tabCauHinh";
            this.tabCauHinh.Padding = new System.Windows.Forms.Padding(3);
            this.tabCauHinh.Size = new System.Drawing.Size(984, 410);
            this.tabCauHinh.TabIndex = 0;
            this.tabCauHinh.Text = "Cấu hình SMTP";
            this.tabCauHinh.UseVisualStyleBackColor = true;
            // 
            // grpTaiKhoan
            // 
            this.grpTaiKhoan.Controls.Add(this.txtUsername);
            this.grpTaiKhoan.Controls.Add(this.lblUsername);
            this.grpTaiKhoan.Controls.Add(this.chkUseDefaultCredentials);
            this.grpTaiKhoan.Controls.Add(this.txtPassword);
            this.grpTaiKhoan.Controls.Add(this.lblPassword);
            this.grpTaiKhoan.Controls.Add(this.txtEmail);
            this.grpTaiKhoan.Controls.Add(this.lblEmail);
            this.grpTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpTaiKhoan.Location = new System.Drawing.Point(9, 175);
            this.grpTaiKhoan.Name = "grpTaiKhoan";
            this.grpTaiKhoan.Size = new System.Drawing.Size(969, 215);
            this.grpTaiKhoan.TabIndex = 1;
            this.grpTaiKhoan.TabStop = false;
            this.grpTaiKhoan.Text = "Tài khoản gửi email";
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsername.Location = new System.Drawing.Point(13, 0);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(172, 25);
            this.txtUsername.TabIndex = 6;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(13, 100);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(67, 15);
            this.lblUsername.TabIndex = 5;
            this.lblUsername.Text = "Username:";
            this.lblUsername.Visible = false;
            // 
            // chkUseDefaultCredentials
            // 
            this.chkUseDefaultCredentials.AutoSize = true;
            this.chkUseDefaultCredentials.Location = new System.Drawing.Point(13, 74);
            this.chkUseDefaultCredentials.Name = "chkUseDefaultCredentials";
            this.chkUseDefaultCredentials.Size = new System.Drawing.Size(225, 19);
            this.chkUseDefaultCredentials.TabIndex = 4;
            this.chkUseDefaultCredentials.Text = "Dùng thông tin đăng nhập mặc định";
            this.chkUseDefaultCredentials.CheckedChanged += new System.EventHandler(this.chkUseDefaultCredentials_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(291, 43);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(253, 25);
            this.txtPassword.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(291, 26);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(62, 15);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Mật khẩu:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(13, 43);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(258, 25);
            this.txtEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(13, 26);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email:";
            // 
            // grpServer
            // 
            this.grpServer.Controls.Add(this.cboEncryption);
            this.grpServer.Controls.Add(this.lblEncryption);
            this.grpServer.Controls.Add(this.numSmtpPort);
            this.grpServer.Controls.Add(this.lblSmtpPort);
            this.grpServer.Controls.Add(this.txtSmtpServer);
            this.grpServer.Controls.Add(this.lblSmtpServer);
            this.grpServer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpServer.Location = new System.Drawing.Point(9, 9);
            this.grpServer.Name = "grpServer";
            this.grpServer.Size = new System.Drawing.Size(969, 137);
            this.grpServer.TabIndex = 0;
            this.grpServer.TabStop = false;
            this.grpServer.Text = "Máy chủ SMTP";
            // 
            // cboEncryption
            // 
            this.cboEncryption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEncryption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboEncryption.Items.AddRange(new object[] {
            "TLS",
            "SSL",
            "None"});
            this.cboEncryption.Location = new System.Drawing.Point(13, 91);
            this.cboEncryption.Name = "cboEncryption";
            this.cboEncryption.Size = new System.Drawing.Size(129, 25);
            this.cboEncryption.TabIndex = 5;
            // 
            // lblEncryption
            // 
            this.lblEncryption.AutoSize = true;
            this.lblEncryption.Location = new System.Drawing.Point(13, 74);
            this.lblEncryption.Name = "lblEncryption";
            this.lblEncryption.Size = new System.Drawing.Size(50, 15);
            this.lblEncryption.TabIndex = 4;
            this.lblEncryption.Text = "Mã hóa:";
            // 
            // numSmtpPort
            // 
            this.numSmtpPort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSmtpPort.Location = new System.Drawing.Point(291, 43);
            this.numSmtpPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numSmtpPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSmtpPort.Name = "numSmtpPort";
            this.numSmtpPort.Size = new System.Drawing.Size(69, 25);
            this.numSmtpPort.TabIndex = 3;
            this.numSmtpPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSmtpPort
            // 
            this.lblSmtpPort.AutoSize = true;
            this.lblSmtpPort.Location = new System.Drawing.Point(291, 26);
            this.lblSmtpPort.Name = "lblSmtpPort";
            this.lblSmtpPort.Size = new System.Drawing.Size(69, 15);
            this.lblSmtpPort.TabIndex = 2;
            this.lblSmtpPort.Text = "SMTP Port:";
            // 
            // txtSmtpServer
            // 
            this.txtSmtpServer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSmtpServer.Location = new System.Drawing.Point(13, 43);
            this.txtSmtpServer.Name = "txtSmtpServer";
            this.txtSmtpServer.Size = new System.Drawing.Size(258, 25);
            this.txtSmtpServer.TabIndex = 1;
            // 
            // lblSmtpServer
            // 
            this.lblSmtpServer.AutoSize = true;
            this.lblSmtpServer.Location = new System.Drawing.Point(13, 26);
            this.lblSmtpServer.Name = "lblSmtpServer";
            this.lblSmtpServer.Size = new System.Drawing.Size(83, 15);
            this.lblSmtpServer.TabIndex = 0;
            this.lblSmtpServer.Text = "SMTP Server:";
            // 
            // tabMauEmail
            // 
            this.tabMauEmail.Controls.Add(this.grpTestEmail);
            this.tabMauEmail.Location = new System.Drawing.Point(4, 24);
            this.tabMauEmail.Name = "tabMauEmail";
            this.tabMauEmail.Size = new System.Drawing.Size(592, 258);
            this.tabMauEmail.TabIndex = 1;
            this.tabMauEmail.Text = "Gửi thử email";
            this.tabMauEmail.UseVisualStyleBackColor = true;
            // 
            // grpTestEmail
            // 
            this.grpTestEmail.Controls.Add(this.lblStatus);
            this.grpTestEmail.Controls.Add(this.btnGuiThu);
            this.grpTestEmail.Controls.Add(this.txtToEmail);
            this.grpTestEmail.Controls.Add(this.lblToEmail);
            this.grpTestEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpTestEmail.Location = new System.Drawing.Point(13, 13);
            this.grpTestEmail.Name = "grpTestEmail";
            this.grpTestEmail.Size = new System.Drawing.Size(566, 217);
            this.grpTestEmail.TabIndex = 0;
            this.grpTestEmail.TabStop = false;
            this.grpTestEmail.Text = "Kiểm tra cấu hình email";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(13, 104);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(66, 19);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Sẵn sàng";
            // 
            // btnGuiThu
            // 
            this.btnGuiThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnGuiThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuiThu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuiThu.ForeColor = System.Drawing.Color.White;
            this.btnGuiThu.Location = new System.Drawing.Point(369, 54);
            this.btnGuiThu.Name = "btnGuiThu";
            this.btnGuiThu.Size = new System.Drawing.Size(111, 26);
            this.btnGuiThu.TabIndex = 2;
            this.btnGuiThu.Text = "📧 Gửi thử";
            this.btnGuiThu.UseVisualStyleBackColor = false;
            this.btnGuiThu.Click += new System.EventHandler(this.btnGuiThu_Click);
            // 
            // txtToEmail
            // 
            this.txtToEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtToEmail.Location = new System.Drawing.Point(13, 56);
            this.txtToEmail.Name = "txtToEmail";
            this.txtToEmail.Size = new System.Drawing.Size(343, 25);
            this.txtToEmail.TabIndex = 1;
            // 
            // lblToEmail
            // 
            this.lblToEmail.AutoSize = true;
            this.lblToEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblToEmail.Location = new System.Drawing.Point(13, 35);
            this.lblToEmail.Name = "lblToEmail";
            this.lblToEmail.Size = new System.Drawing.Size(145, 19);
            this.lblToEmail.TabIndex = 0;
            this.lblToEmail.Text = "Email người nhận test:";
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(786, 533);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(103, 33);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "💾 Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Location = new System.Drawing.Point(906, 533);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(103, 33);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // CauHinhEmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.tabEmail);
            this.Controls.Add(this.lblTitle);
            this.Name = "CauHinhEmailForm";
            this.Size = new System.Drawing.Size(1065, 592);
            this.Load += new System.EventHandler(this.CauHinhEmailForm_Load);
            this.tabEmail.ResumeLayout(false);
            this.tabCauHinh.ResumeLayout(false);
            this.grpTaiKhoan.ResumeLayout(false);
            this.grpTaiKhoan.PerformLayout();
            this.grpServer.ResumeLayout(false);
            this.grpServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSmtpPort)).EndInit();
            this.tabMauEmail.ResumeLayout(false);
            this.grpTestEmail.ResumeLayout(false);
            this.grpTestEmail.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
