using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class DinhDangTienTeForm
    {
        private Label lblTitle;
        private TabControl tabDinhDang;
        private TabPage tabTienTe;
        private TabPage tabNgayThang;
        private TabPage tabSo;

        // Tab Tien Te
        private GroupBox grpTienTe;
        private Label lblTienTeMacDinh;
        private ComboBox cboTienTeMacDinh;
        private Label lblMauTienTe;
        private Panel panPreview;
        private Label lblMauTienTeValue;
        private Label lblSoThapPhan;
        private ComboBox cboSoThapPhan;
        private Label lblKyHieuNgang;
        private TextBox txtKyHieuNgang;
        private Label lblKyHieuDoc;
        private TextBox txtKyHieuDoc;
        private CheckBox chkHienThiKyHieu;

        // Tab Ngay Thang
        private GroupBox grpDinhDangNgay;
        private Label lblDinhDangNgay;
        private ComboBox cboDinhDangNgay;
        private Label lblMauNgay;
        private Label lblDinhDangThoiGian;
        private ComboBox cboDinhDangThoiGian;
        private CheckBox chkDungMuiGio;
        private Label lblMuaChuan;
        private ComboBox cboMuaChuan;
        private Label lblNgayBatDauTuan;
        private ComboBox cboNgayBatDauTuan;

        // Tab So
        private GroupBox grpDinhDangSo;
        private Label lblKyHieuThapPhan;
        private ComboBox cboKyHieuThapPhan;
        private Label lblKyHieuNganCach;
        private ComboBox cboKyHieuNganCach;
        private CheckBox chkLamTron;

        private Button btnLuu;
        private Button btnMacDinh;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabDinhDang = new System.Windows.Forms.TabControl();
            this.tabTienTe = new System.Windows.Forms.TabPage();
            this.grpTienTe = new System.Windows.Forms.GroupBox();
            this.chkHienThiKyHieu = new System.Windows.Forms.CheckBox();
            this.txtKyHieuDoc = new System.Windows.Forms.TextBox();
            this.lblKyHieuDoc = new System.Windows.Forms.Label();
            this.txtKyHieuNgang = new System.Windows.Forms.TextBox();
            this.lblKyHieuNgang = new System.Windows.Forms.Label();
            this.cboSoThapPhan = new System.Windows.Forms.ComboBox();
            this.lblSoThapPhan = new System.Windows.Forms.Label();
            this.panPreview = new System.Windows.Forms.Panel();
            this.lblMauTienTeValue = new System.Windows.Forms.Label();
            this.lblMauTienTe = new System.Windows.Forms.Label();
            this.cboTienTeMacDinh = new System.Windows.Forms.ComboBox();
            this.lblTienTeMacDinh = new System.Windows.Forms.Label();
            this.tabNgayThang = new System.Windows.Forms.TabPage();
            this.grpDinhDangNgay = new System.Windows.Forms.GroupBox();
            this.cboNgayBatDauTuan = new System.Windows.Forms.ComboBox();
            this.lblNgayBatDauTuan = new System.Windows.Forms.Label();
            this.cboMuaChuan = new System.Windows.Forms.ComboBox();
            this.lblMuaChuan = new System.Windows.Forms.Label();
            this.chkDungMuiGio = new System.Windows.Forms.CheckBox();
            this.cboDinhDangThoiGian = new System.Windows.Forms.ComboBox();
            this.lblDinhDangThoiGian = new System.Windows.Forms.Label();
            this.lblMauNgay = new System.Windows.Forms.Label();
            this.cboDinhDangNgay = new System.Windows.Forms.ComboBox();
            this.lblDinhDangNgay = new System.Windows.Forms.Label();
            this.tabSo = new System.Windows.Forms.TabPage();
            this.grpDinhDangSo = new System.Windows.Forms.GroupBox();
            this.chkLamTron = new System.Windows.Forms.CheckBox();
            this.cboKyHieuNganCach = new System.Windows.Forms.ComboBox();
            this.lblKyHieuNganCach = new System.Windows.Forms.Label();
            this.cboKyHieuThapPhan = new System.Windows.Forms.ComboBox();
            this.lblKyHieuThapPhan = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnMacDinh = new System.Windows.Forms.Button();
            this.tabDinhDang.SuspendLayout();
            this.tabTienTe.SuspendLayout();
            this.grpTienTe.SuspendLayout();
            this.panPreview.SuspendLayout();
            this.tabNgayThang.SuspendLayout();
            this.grpDinhDangNgay.SuspendLayout();
            this.tabSo.SuspendLayout();
            this.grpDinhDangSo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitle.Location = new System.Drawing.Point(17, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Định Dạng Tiền Tệ & Ngày Tháng";
            // 
            // tabDinhDang
            // 
            this.tabDinhDang.Controls.Add(this.tabTienTe);
            this.tabDinhDang.Controls.Add(this.tabNgayThang);
            this.tabDinhDang.Controls.Add(this.tabSo);
            this.tabDinhDang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabDinhDang.Location = new System.Drawing.Point(17, 52);
            this.tabDinhDang.Name = "tabDinhDang";
            this.tabDinhDang.SelectedIndex = 0;
            this.tabDinhDang.Size = new System.Drawing.Size(968, 440);
            this.tabDinhDang.TabIndex = 1;
            // 
            // tabTienTe
            // 
            this.tabTienTe.Controls.Add(this.grpTienTe);
            this.tabTienTe.Location = new System.Drawing.Point(4, 24);
            this.tabTienTe.Name = "tabTienTe";
            this.tabTienTe.Size = new System.Drawing.Size(960, 412);
            this.tabTienTe.TabIndex = 0;
            this.tabTienTe.Text = "Tiền tệ";
            this.tabTienTe.UseVisualStyleBackColor = true;
            // 
            // grpTienTe
            // 
            this.grpTienTe.Controls.Add(this.chkHienThiKyHieu);
            this.grpTienTe.Controls.Add(this.txtKyHieuDoc);
            this.grpTienTe.Controls.Add(this.lblKyHieuDoc);
            this.grpTienTe.Controls.Add(this.txtKyHieuNgang);
            this.grpTienTe.Controls.Add(this.lblKyHieuNgang);
            this.grpTienTe.Controls.Add(this.cboSoThapPhan);
            this.grpTienTe.Controls.Add(this.lblSoThapPhan);
            this.grpTienTe.Controls.Add(this.panPreview);
            this.grpTienTe.Controls.Add(this.lblMauTienTe);
            this.grpTienTe.Controls.Add(this.cboTienTeMacDinh);
            this.grpTienTe.Controls.Add(this.lblTienTeMacDinh);
            this.grpTienTe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpTienTe.Location = new System.Drawing.Point(9, 9);
            this.grpTienTe.Name = "grpTienTe";
            this.grpTienTe.Size = new System.Drawing.Size(936, 361);
            this.grpTienTe.TabIndex = 0;
            this.grpTienTe.TabStop = false;
            this.grpTienTe.Text = "Cấu hình tiền tệ";
            // 
            // chkHienThiKyHieu
            // 
            this.chkHienThiKyHieu.AutoSize = true;
            this.chkHienThiKyHieu.Checked = true;
            this.chkHienThiKyHieu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHienThiKyHieu.Location = new System.Drawing.Point(13, 191);
            this.chkHienThiKyHieu.Name = "chkHienThiKyHieu";
            this.chkHienThiKyHieu.Size = new System.Drawing.Size(200, 19);
            this.chkHienThiKyHieu.TabIndex = 10;
            this.chkHienThiKyHieu.Text = "Hiển thị ký hiệu tiền tệ (₫, $, ...)";
            // 
            // txtKyHieuDoc
            // 
            this.txtKyHieuDoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtKyHieuDoc.Location = new System.Drawing.Point(120, 156);
            this.txtKyHieuDoc.Name = "txtKyHieuDoc";
            this.txtKyHieuDoc.Size = new System.Drawing.Size(69, 25);
            this.txtKyHieuDoc.TabIndex = 9;
            this.txtKyHieuDoc.Text = ".";
            // 
            // lblKyHieuDoc
            // 
            this.lblKyHieuDoc.AutoSize = true;
            this.lblKyHieuDoc.Location = new System.Drawing.Point(120, 139);
            this.lblKyHieuDoc.Name = "lblKyHieuDoc";
            this.lblKyHieuDoc.Size = new System.Drawing.Size(167, 15);
            this.lblKyHieuDoc.TabIndex = 8;
            this.lblKyHieuDoc.Text = "Ký hiệu ngăn cách thập phân:";
            // 
            // txtKyHieuNgang
            // 
            this.txtKyHieuNgang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtKyHieuNgang.Location = new System.Drawing.Point(13, 156);
            this.txtKyHieuNgang.Name = "txtKyHieuNgang";
            this.txtKyHieuNgang.Size = new System.Drawing.Size(69, 25);
            this.txtKyHieuNgang.TabIndex = 7;
            this.txtKyHieuNgang.Text = ",";
            // 
            // lblKyHieuNgang
            // 
            this.lblKyHieuNgang.AutoSize = true;
            this.lblKyHieuNgang.Location = new System.Drawing.Point(13, 139);
            this.lblKyHieuNgang.Name = "lblKyHieuNgang";
            this.lblKyHieuNgang.Size = new System.Drawing.Size(173, 15);
            this.lblKyHieuNgang.TabIndex = 6;
            this.lblKyHieuNgang.Text = "Ký hiệu ngăn cách phần nghìn:";
            // 
            // cboSoThapPhan
            // 
            this.cboSoThapPhan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSoThapPhan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSoThapPhan.Items.AddRange(new object[] {
            "0 (không có)",
            "1",
            "2",
            "3",
            "4"});
            this.cboSoThapPhan.Location = new System.Drawing.Point(13, 100);
            this.cboSoThapPhan.Name = "cboSoThapPhan";
            this.cboSoThapPhan.Size = new System.Drawing.Size(172, 25);
            this.cboSoThapPhan.TabIndex = 5;
            this.cboSoThapPhan.SelectedIndexChanged += new System.EventHandler(this.cboSoThapPhan_SelectedIndexChanged);
            // 
            // lblSoThapPhan
            // 
            this.lblSoThapPhan.AutoSize = true;
            this.lblSoThapPhan.Location = new System.Drawing.Point(13, 82);
            this.lblSoThapPhan.Name = "lblSoThapPhan";
            this.lblSoThapPhan.Size = new System.Drawing.Size(121, 15);
            this.lblSoThapPhan.TabIndex = 4;
            this.lblSoThapPhan.Text = "Số chữ số thập phân:";
            // 
            // panPreview
            // 
            this.panPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPreview.Controls.Add(this.lblMauTienTeValue);
            this.panPreview.Location = new System.Drawing.Point(398, 43);
            this.panPreview.Name = "panPreview";
            this.panPreview.Size = new System.Drawing.Size(257, 52);
            this.panPreview.TabIndex = 3;
            // 
            // lblMauTienTeValue
            // 
            this.lblMauTienTeValue.AutoSize = true;
            this.lblMauTienTeValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMauTienTeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblMauTienTeValue.Location = new System.Drawing.Point(326, 23);
            this.lblMauTienTeValue.Name = "lblMauTienTeValue";
            this.lblMauTienTeValue.Size = new System.Drawing.Size(64, 21);
            this.lblMauTienTeValue.TabIndex = 3;
            this.lblMauTienTeValue.Text = "₫ 1,000";
            // 
            // lblMauTienTe
            // 
            this.lblMauTienTe.AutoSize = true;
            this.lblMauTienTe.Location = new System.Drawing.Point(398, 30);
            this.lblMauTienTe.Name = "lblMauTienTe";
            this.lblMauTienTe.Size = new System.Drawing.Size(34, 15);
            this.lblMauTienTe.TabIndex = 2;
            this.lblMauTienTe.Text = "Mẫu:";
            // 
            // cboTienTeMacDinh
            // 
            this.cboTienTeMacDinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTienTeMacDinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTienTeMacDinh.Items.AddRange(new object[] {
            "VND - Việt Nam Đồng (₫)",
            "USD - US Dollar ($)",
            "EUR - Euro (€)",
            "JPY - Japanese Yen (¥)"});
            this.cboTienTeMacDinh.Location = new System.Drawing.Point(13, 43);
            this.cboTienTeMacDinh.Name = "cboTienTeMacDinh";
            this.cboTienTeMacDinh.Size = new System.Drawing.Size(258, 25);
            this.cboTienTeMacDinh.TabIndex = 1;
            this.cboTienTeMacDinh.SelectedIndexChanged += new System.EventHandler(this.cboTienTeMacDinh_SelectedIndexChanged);
            // 
            // lblTienTeMacDinh
            // 
            this.lblTienTeMacDinh.AutoSize = true;
            this.lblTienTeMacDinh.Location = new System.Drawing.Point(13, 26);
            this.lblTienTeMacDinh.Name = "lblTienTeMacDinh";
            this.lblTienTeMacDinh.Size = new System.Drawing.Size(141, 15);
            this.lblTienTeMacDinh.TabIndex = 0;
            this.lblTienTeMacDinh.Text = "Đơn vị tiền tệ mặc định:";
            // 
            // tabNgayThang
            // 
            this.tabNgayThang.Controls.Add(this.grpDinhDangNgay);
            this.tabNgayThang.Location = new System.Drawing.Point(4, 24);
            this.tabNgayThang.Name = "tabNgayThang";
            this.tabNgayThang.Size = new System.Drawing.Size(592, 258);
            this.tabNgayThang.TabIndex = 1;
            this.tabNgayThang.Text = "Ngày tháng";
            this.tabNgayThang.UseVisualStyleBackColor = true;
            // 
            // grpDinhDangNgay
            // 
            this.grpDinhDangNgay.Controls.Add(this.cboNgayBatDauTuan);
            this.grpDinhDangNgay.Controls.Add(this.lblNgayBatDauTuan);
            this.grpDinhDangNgay.Controls.Add(this.cboMuaChuan);
            this.grpDinhDangNgay.Controls.Add(this.lblMuaChuan);
            this.grpDinhDangNgay.Controls.Add(this.chkDungMuiGio);
            this.grpDinhDangNgay.Controls.Add(this.cboDinhDangThoiGian);
            this.grpDinhDangNgay.Controls.Add(this.lblDinhDangThoiGian);
            this.grpDinhDangNgay.Controls.Add(this.lblMauNgay);
            this.grpDinhDangNgay.Controls.Add(this.cboDinhDangNgay);
            this.grpDinhDangNgay.Controls.Add(this.lblDinhDangNgay);
            this.grpDinhDangNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpDinhDangNgay.Location = new System.Drawing.Point(9, 9);
            this.grpDinhDangNgay.Name = "grpDinhDangNgay";
            this.grpDinhDangNgay.Size = new System.Drawing.Size(566, 234);
            this.grpDinhDangNgay.TabIndex = 0;
            this.grpDinhDangNgay.TabStop = false;
            this.grpDinhDangNgay.Text = "Định dạng ngày & tháng";
            // 
            // cboNgayBatDauTuan
            // 
            this.cboNgayBatDauTuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNgayBatDauTuan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNgayBatDauTuan.Items.AddRange(new object[] {
            "Thứ Hai",
            "Chủ Nhật"});
            this.cboNgayBatDauTuan.Location = new System.Drawing.Point(257, 186);
            this.cboNgayBatDauTuan.Name = "cboNgayBatDauTuan";
            this.cboNgayBatDauTuan.Size = new System.Drawing.Size(129, 25);
            this.cboNgayBatDauTuan.TabIndex = 9;
            // 
            // lblNgayBatDauTuan
            // 
            this.lblNgayBatDauTuan.AutoSize = true;
            this.lblNgayBatDauTuan.Location = new System.Drawing.Point(257, 169);
            this.lblNgayBatDauTuan.Name = "lblNgayBatDauTuan";
            this.lblNgayBatDauTuan.Size = new System.Drawing.Size(111, 15);
            this.lblNgayBatDauTuan.TabIndex = 8;
            this.lblNgayBatDauTuan.Text = "Ngày bắt đầu tuần:";
            // 
            // cboMuaChuan
            // 
            this.cboMuaChuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMuaChuan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboMuaChuan.Items.AddRange(new object[] {
            "VN (UTC+07:00)",
            "US Eastern (UTC-05:00)",
            "US Pacific (UTC-08:00)",
            "UK (UTC+00:00)",
            "Japan (UTC+09:00)"});
            this.cboMuaChuan.Location = new System.Drawing.Point(13, 186);
            this.cboMuaChuan.Name = "cboMuaChuan";
            this.cboMuaChuan.Size = new System.Drawing.Size(215, 25);
            this.cboMuaChuan.TabIndex = 7;
            // 
            // lblMuaChuan
            // 
            this.lblMuaChuan.AutoSize = true;
            this.lblMuaChuan.Location = new System.Drawing.Point(13, 169);
            this.lblMuaChuan.Name = "lblMuaChuan";
            this.lblMuaChuan.Size = new System.Drawing.Size(70, 15);
            this.lblMuaChuan.TabIndex = 6;
            this.lblMuaChuan.Text = "Múa chuẩn:";
            // 
            // chkDungMuiGio
            // 
            this.chkDungMuiGio.AutoSize = true;
            this.chkDungMuiGio.Location = new System.Drawing.Point(13, 134);
            this.chkDungMuiGio.Name = "chkDungMuiGio";
            this.chkDungMuiGio.Size = new System.Drawing.Size(174, 19);
            this.chkDungMuiGio.TabIndex = 5;
            this.chkDungMuiGio.Text = "Đúng múi giờ (UTC+07:00)";
            // 
            // cboDinhDangThoiGian
            // 
            this.cboDinhDangThoiGian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDinhDangThoiGian.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDinhDangThoiGian.Items.AddRange(new object[] {
            "HH:mm:ss  (14:30:00)",
            "hh:mm:ss tt  (02:30:00 PM)",
            "HH:mm  (14:30)"});
            this.cboDinhDangThoiGian.Location = new System.Drawing.Point(13, 100);
            this.cboDinhDangThoiGian.Name = "cboDinhDangThoiGian";
            this.cboDinhDangThoiGian.Size = new System.Drawing.Size(215, 25);
            this.cboDinhDangThoiGian.TabIndex = 4;
            // 
            // lblDinhDangThoiGian
            // 
            this.lblDinhDangThoiGian.AutoSize = true;
            this.lblDinhDangThoiGian.Location = new System.Drawing.Point(13, 82);
            this.lblDinhDangThoiGian.Name = "lblDinhDangThoiGian";
            this.lblDinhDangThoiGian.Size = new System.Drawing.Size(118, 15);
            this.lblDinhDangThoiGian.TabIndex = 3;
            this.lblDinhDangThoiGian.Text = "Định dạng thời gian:";
            // 
            // lblMauNgay
            // 
            this.lblMauNgay.AutoSize = true;
            this.lblMauNgay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMauNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblMauNgay.Location = new System.Drawing.Point(291, 26);
            this.lblMauNgay.Name = "lblMauNgay";
            this.lblMauNgay.Size = new System.Drawing.Size(96, 21);
            this.lblMauNgay.TabIndex = 2;
            this.lblMauNgay.Text = "23/03/2026";
            // 
            // cboDinhDangNgay
            // 
            this.cboDinhDangNgay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDinhDangNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDinhDangNgay.Items.AddRange(new object[] {
            "dd/MM/yyyy  (23/03/2026)",
            "MM/dd/yyyy  (03/23/2026)",
            "yyyy-MM-dd  (2026-03-23)",
            "dd-MM-yyyy  (23-03-2026)"});
            this.cboDinhDangNgay.Location = new System.Drawing.Point(13, 43);
            this.cboDinhDangNgay.Name = "cboDinhDangNgay";
            this.cboDinhDangNgay.Size = new System.Drawing.Size(258, 25);
            this.cboDinhDangNgay.TabIndex = 1;
            this.cboDinhDangNgay.SelectedIndexChanged += new System.EventHandler(this.cboDinhDangNgay_SelectedIndexChanged);
            // 
            // lblDinhDangNgay
            // 
            this.lblDinhDangNgay.AutoSize = true;
            this.lblDinhDangNgay.Location = new System.Drawing.Point(13, 26);
            this.lblDinhDangNgay.Name = "lblDinhDangNgay";
            this.lblDinhDangNgay.Size = new System.Drawing.Size(95, 15);
            this.lblDinhDangNgay.TabIndex = 0;
            this.lblDinhDangNgay.Text = "Định dạng ngày:";
            // 
            // tabSo
            // 
            this.tabSo.Controls.Add(this.grpDinhDangSo);
            this.tabSo.Location = new System.Drawing.Point(4, 24);
            this.tabSo.Name = "tabSo";
            this.tabSo.Size = new System.Drawing.Size(592, 258);
            this.tabSo.TabIndex = 2;
            this.tabSo.Text = "Số";
            this.tabSo.UseVisualStyleBackColor = true;
            // 
            // grpDinhDangSo
            // 
            this.grpDinhDangSo.Controls.Add(this.chkLamTron);
            this.grpDinhDangSo.Controls.Add(this.cboKyHieuNganCach);
            this.grpDinhDangSo.Controls.Add(this.lblKyHieuNganCach);
            this.grpDinhDangSo.Controls.Add(this.cboKyHieuThapPhan);
            this.grpDinhDangSo.Controls.Add(this.lblKyHieuThapPhan);
            this.grpDinhDangSo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpDinhDangSo.Location = new System.Drawing.Point(9, 9);
            this.grpDinhDangSo.Name = "grpDinhDangSo";
            this.grpDinhDangSo.Size = new System.Drawing.Size(566, 234);
            this.grpDinhDangSo.TabIndex = 0;
            this.grpDinhDangSo.TabStop = false;
            this.grpDinhDangSo.Text = "Định dạng số";
            // 
            // chkLamTron
            // 
            this.chkLamTron.AutoSize = true;
            this.chkLamTron.Checked = true;
            this.chkLamTron.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLamTron.Location = new System.Drawing.Point(13, 147);
            this.chkLamTron.Name = "chkLamTron";
            this.chkLamTron.Size = new System.Drawing.Size(156, 19);
            this.chkLamTron.TabIndex = 4;
            this.chkLamTron.Text = "Làm tròn số khi hiển thị";
            // 
            // cboKyHieuNganCach
            // 
            this.cboKyHieuNganCach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKyHieuNganCach.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKyHieuNganCach.Items.AddRange(new object[] {
            ", (phẩy) VD: 1,000",
            ". (chấm) VD: 1.000",
            "Khoảng trắng VD: 1 000"});
            this.cboKyHieuNganCach.Location = new System.Drawing.Point(13, 100);
            this.cboKyHieuNganCach.Name = "cboKyHieuNganCach";
            this.cboKyHieuNganCach.Size = new System.Drawing.Size(258, 25);
            this.cboKyHieuNganCach.TabIndex = 3;
            // 
            // lblKyHieuNganCach
            // 
            this.lblKyHieuNganCach.AutoSize = true;
            this.lblKyHieuNganCach.Location = new System.Drawing.Point(13, 82);
            this.lblKyHieuNganCach.Name = "lblKyHieuNganCach";
            this.lblKyHieuNganCach.Size = new System.Drawing.Size(173, 15);
            this.lblKyHieuNganCach.TabIndex = 2;
            this.lblKyHieuNganCach.Text = "Ký hiệu ngăn cách hàng nghìn:";
            // 
            // cboKyHieuThapPhan
            // 
            this.cboKyHieuThapPhan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKyHieuThapPhan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKyHieuThapPhan.Items.AddRange(new object[] {
            ". (chấm) VD: 1.50",
            ", (phẩy) VD: 1,50"});
            this.cboKyHieuThapPhan.Location = new System.Drawing.Point(13, 43);
            this.cboKyHieuThapPhan.Name = "cboKyHieuThapPhan";
            this.cboKyHieuThapPhan.Size = new System.Drawing.Size(215, 25);
            this.cboKyHieuThapPhan.TabIndex = 1;
            // 
            // lblKyHieuThapPhan
            // 
            this.lblKyHieuThapPhan.AutoSize = true;
            this.lblKyHieuThapPhan.Location = new System.Drawing.Point(13, 26);
            this.lblKyHieuThapPhan.Name = "lblKyHieuThapPhan";
            this.lblKyHieuThapPhan.Size = new System.Drawing.Size(132, 15);
            this.lblKyHieuThapPhan.TabIndex = 0;
            this.lblKyHieuThapPhan.Text = "Ký hiệu dấu thập phân:";
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(733, 521);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(103, 33);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "💾 Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnMacDinh
            // 
            this.btnMacDinh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMacDinh.Location = new System.Drawing.Point(860, 521);
            this.btnMacDinh.Name = "btnMacDinh";
            this.btnMacDinh.Size = new System.Drawing.Size(121, 33);
            this.btnMacDinh.TabIndex = 2;
            this.btnMacDinh.Text = "Khôi phục mặc định";
            this.btnMacDinh.UseVisualStyleBackColor = true;
            this.btnMacDinh.Click += new System.EventHandler(this.btnMacDinh_Click);
            // 
            // DinhDangTienTeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnMacDinh);
            this.Controls.Add(this.tabDinhDang);
            this.Controls.Add(this.lblTitle);
            this.Name = "DinhDangTienTeForm";
            this.Size = new System.Drawing.Size(1065, 592);
            this.tabDinhDang.ResumeLayout(false);
            this.tabTienTe.ResumeLayout(false);
            this.grpTienTe.ResumeLayout(false);
            this.grpTienTe.PerformLayout();
            this.panPreview.ResumeLayout(false);
            this.panPreview.PerformLayout();
            this.tabNgayThang.ResumeLayout(false);
            this.grpDinhDangNgay.ResumeLayout(false);
            this.grpDinhDangNgay.PerformLayout();
            this.tabSo.ResumeLayout(false);
            this.grpDinhDangSo.ResumeLayout(false);
            this.grpDinhDangSo.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
