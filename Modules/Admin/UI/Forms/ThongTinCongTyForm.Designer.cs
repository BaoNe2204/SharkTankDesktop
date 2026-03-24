using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class ThongTinCongTyForm
    {
        private Label lblTitle;
        private Label lblTenCongTy;
        private TextBox txtTenCongTy;
        private Label lblDiaChi;
        private TextBox txtDiaChi;
        private Label lblMaSoThue;
        private TextBox txtMaSoThue;
        private Label lblDienThoai;
        private TextBox txtDienThoai;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblWebsite;
        private TextBox txtWebsite;
        private Label lblSoDangKy;
        private TextBox txtSoDangKy;
        private Label lblLogo;
        private PictureBox picLogo;
        private Button btnChonAnh;
        private Button btnLuu;
        private Button btnHuy;
        private Label lblNguoiDaiDien;
        private TextBox txtNguoiDaiDien;
        private Label lblChucVu;
        private TextBox txtChucVu;
        private Label lblHotline;
        private TextBox txtHotline;
        private GroupBox grpThongTinChinh;
        private GroupBox grpThongTinPhu;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpThongTinChinh = new System.Windows.Forms.GroupBox();
            this.txtSoDangKy = new System.Windows.Forms.TextBox();
            this.lblSoDangKy = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.txtMaSoThue = new System.Windows.Forms.TextBox();
            this.lblMaSoThue = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtTenCongTy = new System.Windows.Forms.TextBox();
            this.lblTenCongTy = new System.Windows.Forms.Label();
            this.grpThongTinPhu = new System.Windows.Forms.GroupBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.lblLogo = new System.Windows.Forms.Label();
            this.txtHotline = new System.Windows.Forms.TextBox();
            this.lblHotline = new System.Windows.Forms.Label();
            this.txtChucVu = new System.Windows.Forms.TextBox();
            this.lblChucVu = new System.Windows.Forms.Label();
            this.txtNguoiDaiDien = new System.Windows.Forms.TextBox();
            this.lblNguoiDaiDien = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.grpThongTinChinh.SuspendLayout();
            this.grpThongTinPhu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitle.Location = new System.Drawing.Point(17, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thông Tin Công Ty";
            // 
            // grpThongTinChinh
            // 
            this.grpThongTinChinh.Controls.Add(this.txtSoDangKy);
            this.grpThongTinChinh.Controls.Add(this.lblSoDangKy);
            this.grpThongTinChinh.Controls.Add(this.txtWebsite);
            this.grpThongTinChinh.Controls.Add(this.lblWebsite);
            this.grpThongTinChinh.Controls.Add(this.txtEmail);
            this.grpThongTinChinh.Controls.Add(this.lblEmail);
            this.grpThongTinChinh.Controls.Add(this.txtDienThoai);
            this.grpThongTinChinh.Controls.Add(this.lblDienThoai);
            this.grpThongTinChinh.Controls.Add(this.txtMaSoThue);
            this.grpThongTinChinh.Controls.Add(this.lblMaSoThue);
            this.grpThongTinChinh.Controls.Add(this.txtDiaChi);
            this.grpThongTinChinh.Controls.Add(this.lblDiaChi);
            this.grpThongTinChinh.Controls.Add(this.txtTenCongTy);
            this.grpThongTinChinh.Controls.Add(this.lblTenCongTy);
            this.grpThongTinChinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpThongTinChinh.Location = new System.Drawing.Point(17, 65);
            this.grpThongTinChinh.Name = "grpThongTinChinh";
            this.grpThongTinChinh.Size = new System.Drawing.Size(386, 421);
            this.grpThongTinChinh.TabIndex = 1;
            this.grpThongTinChinh.TabStop = false;
            this.grpThongTinChinh.Text = "Thông Tin Chính";
            // 
            // txtSoDangKy
            // 
            this.txtSoDangKy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoDangKy.Location = new System.Drawing.Point(13, 256);
            this.txtSoDangKy.Name = "txtSoDangKy";
            this.txtSoDangKy.Size = new System.Drawing.Size(352, 25);
            this.txtSoDangKy.TabIndex = 13;
            // 
            // lblSoDangKy
            // 
            this.lblSoDangKy.AutoSize = true;
            this.lblSoDangKy.Location = new System.Drawing.Point(13, 238);
            this.lblSoDangKy.Name = "lblSoDangKy";
            this.lblSoDangKy.Size = new System.Drawing.Size(135, 15);
            this.lblSoDangKy.TabIndex = 12;
            this.lblSoDangKy.Text = "Số đăng ký kinh doanh:";
            // 
            // txtWebsite
            // 
            this.txtWebsite.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWebsite.Location = new System.Drawing.Point(197, 208);
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(168, 25);
            this.txtWebsite.TabIndex = 11;
            // 
            // lblWebsite
            // 
            this.lblWebsite.AutoSize = true;
            this.lblWebsite.Location = new System.Drawing.Point(197, 191);
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Size = new System.Drawing.Size(56, 15);
            this.lblWebsite.TabIndex = 10;
            this.lblWebsite.Text = "Website:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(13, 208);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(172, 25);
            this.txtEmail.TabIndex = 9;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(13, 191);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "Email:";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDienThoai.Location = new System.Drawing.Point(197, 160);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(168, 25);
            this.txtDienThoai.TabIndex = 7;
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Location = new System.Drawing.Point(197, 143);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(67, 15);
            this.lblDienThoai.TabIndex = 6;
            this.lblDienThoai.Text = "Điện thoại:";
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaSoThue.Location = new System.Drawing.Point(13, 160);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.Size = new System.Drawing.Size(172, 25);
            this.txtMaSoThue.TabIndex = 5;
            // 
            // lblMaSoThue
            // 
            this.lblMaSoThue.AutoSize = true;
            this.lblMaSoThue.Location = new System.Drawing.Point(13, 143);
            this.lblMaSoThue.Name = "lblMaSoThue";
            this.lblMaSoThue.Size = new System.Drawing.Size(71, 15);
            this.lblMaSoThue.TabIndex = 4;
            this.lblMaSoThue.Text = "Mã số thuế:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiaChi.Location = new System.Drawing.Point(13, 91);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(352, 44);
            this.txtDiaChi.TabIndex = 3;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(13, 74);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(47, 15);
            this.lblDiaChi.TabIndex = 2;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtTenCongTy
            // 
            this.txtTenCongTy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenCongTy.Location = new System.Drawing.Point(13, 43);
            this.txtTenCongTy.Name = "txtTenCongTy";
            this.txtTenCongTy.Size = new System.Drawing.Size(352, 25);
            this.txtTenCongTy.TabIndex = 1;
            // 
            // lblTenCongTy
            // 
            this.lblTenCongTy.AutoSize = true;
            this.lblTenCongTy.Location = new System.Drawing.Point(13, 26);
            this.lblTenCongTy.Name = "lblTenCongTy";
            this.lblTenCongTy.Size = new System.Drawing.Size(74, 15);
            this.lblTenCongTy.TabIndex = 0;
            this.lblTenCongTy.Text = "Tên công ty:";
            // 
            // grpThongTinPhu
            // 
            this.grpThongTinPhu.Controls.Add(this.btnChonAnh);
            this.grpThongTinPhu.Controls.Add(this.lblLogo);
            this.grpThongTinPhu.Controls.Add(this.txtHotline);
            this.grpThongTinPhu.Controls.Add(this.lblHotline);
            this.grpThongTinPhu.Controls.Add(this.txtChucVu);
            this.grpThongTinPhu.Controls.Add(this.lblChucVu);
            this.grpThongTinPhu.Controls.Add(this.txtNguoiDaiDien);
            this.grpThongTinPhu.Controls.Add(this.lblNguoiDaiDien);
            this.grpThongTinPhu.Controls.Add(this.picLogo);
            this.grpThongTinPhu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpThongTinPhu.Location = new System.Drawing.Point(420, 65);
            this.grpThongTinPhu.Name = "grpThongTinPhu";
            this.grpThongTinPhu.Size = new System.Drawing.Size(557, 421);
            this.grpThongTinPhu.TabIndex = 2;
            this.grpThongTinPhu.TabStop = false;
            this.grpThongTinPhu.Text = "Thông Tin Bổ Sung";
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChonAnh.Location = new System.Drawing.Point(170, 191);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(103, 28);
            this.btnChonAnh.TabIndex = 8;
            this.btnChonAnh.Text = "Chọn Ảnh...";
            this.btnChonAnh.UseVisualStyleBackColor = true;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Location = new System.Drawing.Point(13, 130);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(37, 15);
            this.lblLogo.TabIndex = 6;
            this.lblLogo.Text = "Logo:";
            // 
            // txtHotline
            // 
            this.txtHotline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHotline.Location = new System.Drawing.Point(13, 91);
            this.txtHotline.Name = "txtHotline";
            this.txtHotline.Size = new System.Drawing.Size(172, 25);
            this.txtHotline.TabIndex = 5;
            // 
            // lblHotline
            // 
            this.lblHotline.AutoSize = true;
            this.lblHotline.Location = new System.Drawing.Point(13, 74);
            this.lblHotline.Name = "lblHotline";
            this.lblHotline.Size = new System.Drawing.Size(51, 15);
            this.lblHotline.TabIndex = 4;
            this.lblHotline.Text = "Hotline:";
            // 
            // txtChucVu
            // 
            this.txtChucVu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtChucVu.Location = new System.Drawing.Point(280, 43);
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.Size = new System.Drawing.Size(168, 25);
            this.txtChucVu.TabIndex = 3;
            // 
            // lblChucVu
            // 
            this.lblChucVu.AutoSize = true;
            this.lblChucVu.Location = new System.Drawing.Point(280, 26);
            this.lblChucVu.Name = "lblChucVu";
            this.lblChucVu.Size = new System.Drawing.Size(55, 15);
            this.lblChucVu.TabIndex = 2;
            this.lblChucVu.Text = "Chức vụ:";
            // 
            // txtNguoiDaiDien
            // 
            this.txtNguoiDaiDien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNguoiDaiDien.Location = new System.Drawing.Point(13, 43);
            this.txtNguoiDaiDien.Name = "txtNguoiDaiDien";
            this.txtNguoiDaiDien.Size = new System.Drawing.Size(172, 25);
            this.txtNguoiDaiDien.TabIndex = 1;
            // 
            // lblNguoiDaiDien
            // 
            this.lblNguoiDaiDien.AutoSize = true;
            this.lblNguoiDaiDien.Location = new System.Drawing.Point(13, 26);
            this.lblNguoiDaiDien.Name = "lblNguoiDaiDien";
            this.lblNguoiDaiDien.Size = new System.Drawing.Size(92, 15);
            this.lblNguoiDaiDien.TabIndex = 0;
            this.lblNguoiDaiDien.Text = "Người đại diện:";
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.White;
            this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLogo.InitialImage = null;
            this.picLogo.Location = new System.Drawing.Point(13, 152);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(129, 129);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 7;
            this.picLogo.TabStop = false;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(866, 508);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(111, 35);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "💾 Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Location = new System.Drawing.Point(746, 508);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(103, 35);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // ThongTinCongTyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.grpThongTinPhu);
            this.Controls.Add(this.grpThongTinChinh);
            this.Controls.Add(this.lblTitle);
            this.Name = "ThongTinCongTyForm";
            this.Size = new System.Drawing.Size(1065, 592);
            this.grpThongTinChinh.ResumeLayout(false);
            this.grpThongTinChinh.PerformLayout();
            this.grpThongTinPhu.ResumeLayout(false);
            this.grpThongTinPhu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
