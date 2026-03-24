using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class CauHinhHeThongForm
    {
        private Label lblTitle;
        private TabControl tabConfig;
        private TabPage tabChung;
        private TabPage tabBaoMat;
        private TabPage tabHienThi;
        private TabPage tabNgonNgu;

        // Tab Chung
        private GroupBox grpHeThong;
        private Label lblTenUngDung;
        private TextBox txtTenUngDung;
        private Label lblMoTa;
        private TextBox txtMoTa;
        private Label lblVersion;
        private TextBox txtVersion;
        private Label lblNgonNguMacDinh;
        private ComboBox cboNgongNgu;
        private CheckBox chkAutoBackup;
        private Label lblBackupInterval;
        private NumericUpDown numBackupInterval;
        private Label lblNgay;

        // Tab Bao Mat
        private GroupBox grpMatKhau;
        private Label lblMinLength;
        private NumericUpDown numMinLength;
        private CheckBox chkYeuCauChuHoa;
        private CheckBox chkYeuCauChuThuong;
        private CheckBox chkYeuCauSo;
        private CheckBox chkYeuCauKyTuDacBiet;
        private Label lblHanHieuLuc;
        private NumericUpDown numHanHieuLuc;
        private Label lblNgay2;
        private CheckBox chkKhoaTaiKhoan;
        private Label lblSoLanThatBai;
        private NumericUpDown numSoLanThatBai;

        // Tab Hien Thi
        private GroupBox grpGiaoDien;
        private Label lblMauNen;
        private ComboBox cboMauNen;
        private Label lblPhongCach;
        private ComboBox cboPhongCach;
        private CheckBox chkHienThiTen;
        private CheckBox chkHienThiAvatar;
        private Label lblKichThuocChu;
        private NumericUpDown numFontSize;
        private Label lblPt;

        // Tab Ngon Ngu
        private GroupBox grpNgonNgu;
        private DataGridView dgvNgonNgu;
        private Button btnThemNgonNgu;
        private Button btnXoaNgonNgu;

        private Button btnLuu;
        private Button btnMacDinh;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabConfig = new System.Windows.Forms.TabControl();
            this.tabChung = new System.Windows.Forms.TabPage();
            this.grpHeThong = new System.Windows.Forms.GroupBox();
            this.txtTenUngDung = new System.Windows.Forms.TextBox();
            this.lblTenUngDung = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.cboNgongNgu = new System.Windows.Forms.ComboBox();
            this.lblNgonNguMacDinh = new System.Windows.Forms.Label();
            this.numBackupInterval = new System.Windows.Forms.NumericUpDown();
            this.lblBackupInterval = new System.Windows.Forms.Label();
            this.chkAutoBackup = new System.Windows.Forms.CheckBox();
            this.tabBaoMat = new System.Windows.Forms.TabPage();
            this.grpMatKhau = new System.Windows.Forms.GroupBox();
            this.numSoLanThatBai = new System.Windows.Forms.NumericUpDown();
            this.lblSoLanThatBai = new System.Windows.Forms.Label();
            this.chkKhoaTaiKhoan = new System.Windows.Forms.CheckBox();
            this.lblNgay2 = new System.Windows.Forms.Label();
            this.numHanHieuLuc = new System.Windows.Forms.NumericUpDown();
            this.lblHanHieuLuc = new System.Windows.Forms.Label();
            this.chkYeuCauKyTuDacBiet = new System.Windows.Forms.CheckBox();
            this.chkYeuCauSo = new System.Windows.Forms.CheckBox();
            this.chkYeuCauChuThuong = new System.Windows.Forms.CheckBox();
            this.chkYeuCauChuHoa = new System.Windows.Forms.CheckBox();
            this.numMinLength = new System.Windows.Forms.NumericUpDown();
            this.lblMinLength = new System.Windows.Forms.Label();
            this.tabHienThi = new System.Windows.Forms.TabPage();
            this.grpGiaoDien = new System.Windows.Forms.GroupBox();
            this.lblPt = new System.Windows.Forms.Label();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.lblKichThuocChu = new System.Windows.Forms.Label();
            this.chkHienThiAvatar = new System.Windows.Forms.CheckBox();
            this.chkHienThiTen = new System.Windows.Forms.CheckBox();
            this.cboPhongCach = new System.Windows.Forms.ComboBox();
            this.lblPhongCach = new System.Windows.Forms.Label();
            this.cboMauNen = new System.Windows.Forms.ComboBox();
            this.lblMauNen = new System.Windows.Forms.Label();
            this.tabNgonNgu = new System.Windows.Forms.TabPage();
            this.btnXoaNgonNgu = new System.Windows.Forms.Button();
            this.btnThemNgonNgu = new System.Windows.Forms.Button();
            this.dgvNgonNgu = new System.Windows.Forms.DataGridView();
            this.grpNgonNgu = new System.Windows.Forms.GroupBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnMacDinh = new System.Windows.Forms.Button();
            this.lblNgay = new System.Windows.Forms.Label();
            this.tabConfig.SuspendLayout();
            this.tabChung.SuspendLayout();
            this.grpHeThong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBackupInterval)).BeginInit();
            this.tabBaoMat.SuspendLayout();
            this.grpMatKhau.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLanThatBai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHanHieuLuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinLength)).BeginInit();
            this.tabHienThi.SuspendLayout();
            this.grpGiaoDien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.tabNgonNgu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNgonNgu)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitle.Location = new System.Drawing.Point(17, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(274, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cấu Hình Hệ Thống";
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.tabChung);
            this.tabConfig.Controls.Add(this.tabBaoMat);
            this.tabConfig.Controls.Add(this.tabHienThi);
            this.tabConfig.Controls.Add(this.tabNgonNgu);
            this.tabConfig.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabConfig.Location = new System.Drawing.Point(17, 48);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.SelectedIndex = 0;
            this.tabConfig.Size = new System.Drawing.Size(994, 414);
            this.tabConfig.TabIndex = 1;
            // 
            // tabChung
            // 
            this.tabChung.Controls.Add(this.grpHeThong);
            this.tabChung.Location = new System.Drawing.Point(4, 24);
            this.tabChung.Name = "tabChung";
            this.tabChung.Padding = new System.Windows.Forms.Padding(3);
            this.tabChung.Size = new System.Drawing.Size(986, 386);
            this.tabChung.TabIndex = 0;
            this.tabChung.Text = "Cấu hình chung";
            this.tabChung.UseVisualStyleBackColor = true;
            // 
            // grpHeThong
            // 
            this.grpHeThong.Controls.Add(this.txtTenUngDung);
            this.grpHeThong.Controls.Add(this.lblTenUngDung);
            this.grpHeThong.Controls.Add(this.txtMoTa);
            this.grpHeThong.Controls.Add(this.lblMoTa);
            this.grpHeThong.Controls.Add(this.txtVersion);
            this.grpHeThong.Controls.Add(this.lblVersion);
            this.grpHeThong.Controls.Add(this.cboNgongNgu);
            this.grpHeThong.Controls.Add(this.lblNgonNguMacDinh);
            this.grpHeThong.Controls.Add(this.numBackupInterval);
            this.grpHeThong.Controls.Add(this.lblBackupInterval);
            this.grpHeThong.Controls.Add(this.chkAutoBackup);
            this.grpHeThong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpHeThong.Location = new System.Drawing.Point(9, 9);
            this.grpHeThong.Name = "grpHeThong";
            this.grpHeThong.Size = new System.Drawing.Size(971, 234);
            this.grpHeThong.TabIndex = 0;
            this.grpHeThong.TabStop = false;
            this.grpHeThong.Text = "Thông Tin Ứng Dụng";
            // 
            // txtTenUngDung
            // 
            this.txtTenUngDung.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenUngDung.Location = new System.Drawing.Point(13, 43);
            this.txtTenUngDung.Name = "txtTenUngDung";
            this.txtTenUngDung.Size = new System.Drawing.Size(258, 25);
            this.txtTenUngDung.TabIndex = 1;
            this.txtTenUngDung.Text = "SharkTank ERP";
            // 
            // lblTenUngDung
            // 
            this.lblTenUngDung.AutoSize = true;
            this.lblTenUngDung.Location = new System.Drawing.Point(13, 26);
            this.lblTenUngDung.Name = "lblTenUngDung";
            this.lblTenUngDung.Size = new System.Drawing.Size(86, 15);
            this.lblTenUngDung.TabIndex = 0;
            this.lblTenUngDung.Text = "Tên ứng dụng:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMoTa.Location = new System.Drawing.Point(291, 43);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(258, 25);
            this.txtMoTa.TabIndex = 3;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(291, 26);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(42, 15);
            this.lblMoTa.TabIndex = 2;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // txtVersion
            // 
            this.txtVersion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVersion.Location = new System.Drawing.Point(13, 95);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(129, 25);
            this.txtVersion.TabIndex = 5;
            this.txtVersion.Text = "1.0.0";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(13, 78);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(64, 15);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "Phiên bản:";
            // 
            // cboNgongNgu
            // 
            this.cboNgongNgu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNgongNgu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNgongNgu.Items.AddRange(new object[] {
            "Tiếng Việt",
            "English"});
            this.cboNgongNgu.Location = new System.Drawing.Point(291, 95);
            this.cboNgongNgu.Name = "cboNgongNgu";
            this.cboNgongNgu.Size = new System.Drawing.Size(172, 25);
            this.cboNgongNgu.TabIndex = 7;
            // 
            // lblNgonNguMacDinh
            // 
            this.lblNgonNguMacDinh.AutoSize = true;
            this.lblNgonNguMacDinh.Location = new System.Drawing.Point(291, 78);
            this.lblNgonNguMacDinh.Name = "lblNgonNguMacDinh";
            this.lblNgonNguMacDinh.Size = new System.Drawing.Size(119, 15);
            this.lblNgonNguMacDinh.TabIndex = 6;
            this.lblNgonNguMacDinh.Text = "Ngôn ngữ mặc định:";
            // 
            // numBackupInterval
            // 
            this.numBackupInterval.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numBackupInterval.Location = new System.Drawing.Point(13, 182);
            this.numBackupInterval.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numBackupInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBackupInterval.Name = "numBackupInterval";
            this.numBackupInterval.Size = new System.Drawing.Size(69, 25);
            this.numBackupInterval.TabIndex = 10;
            this.numBackupInterval.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // lblBackupInterval
            // 
            this.lblBackupInterval.AutoSize = true;
            this.lblBackupInterval.Location = new System.Drawing.Point(13, 165);
            this.lblBackupInterval.Name = "lblBackupInterval";
            this.lblBackupInterval.Size = new System.Drawing.Size(94, 15);
            this.lblBackupInterval.TabIndex = 9;
            this.lblBackupInterval.Text = "Khoảng sao lưu:";
            // 
            // chkAutoBackup
            // 
            this.chkAutoBackup.AutoSize = true;
            this.chkAutoBackup.Checked = true;
            this.chkAutoBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoBackup.Location = new System.Drawing.Point(13, 134);
            this.chkAutoBackup.Name = "chkAutoBackup";
            this.chkAutoBackup.Size = new System.Drawing.Size(156, 19);
            this.chkAutoBackup.TabIndex = 8;
            this.chkAutoBackup.Text = "Tự động sao lưu dữ liệu";
            // 
            // tabBaoMat
            // 
            this.tabBaoMat.Controls.Add(this.grpMatKhau);
            this.tabBaoMat.Location = new System.Drawing.Point(4, 24);
            this.tabBaoMat.Name = "tabBaoMat";
            this.tabBaoMat.Padding = new System.Windows.Forms.Padding(3);
            this.tabBaoMat.Size = new System.Drawing.Size(592, 258);
            this.tabBaoMat.TabIndex = 1;
            this.tabBaoMat.Text = "Bảo mật";
            this.tabBaoMat.UseVisualStyleBackColor = true;
            // 
            // grpMatKhau
            // 
            this.grpMatKhau.Controls.Add(this.numSoLanThatBai);
            this.grpMatKhau.Controls.Add(this.lblSoLanThatBai);
            this.grpMatKhau.Controls.Add(this.chkKhoaTaiKhoan);
            this.grpMatKhau.Controls.Add(this.lblNgay2);
            this.grpMatKhau.Controls.Add(this.numHanHieuLuc);
            this.grpMatKhau.Controls.Add(this.lblHanHieuLuc);
            this.grpMatKhau.Controls.Add(this.chkYeuCauKyTuDacBiet);
            this.grpMatKhau.Controls.Add(this.chkYeuCauSo);
            this.grpMatKhau.Controls.Add(this.chkYeuCauChuThuong);
            this.grpMatKhau.Controls.Add(this.chkYeuCauChuHoa);
            this.grpMatKhau.Controls.Add(this.numMinLength);
            this.grpMatKhau.Controls.Add(this.lblMinLength);
            this.grpMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpMatKhau.Location = new System.Drawing.Point(9, 9);
            this.grpMatKhau.Name = "grpMatKhau";
            this.grpMatKhau.Size = new System.Drawing.Size(566, 234);
            this.grpMatKhau.TabIndex = 0;
            this.grpMatKhau.TabStop = false;
            this.grpMatKhau.Text = "Chính sách mật khẩu";
            // 
            // numSoLanThatBai
            // 
            this.numSoLanThatBai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSoLanThatBai.Location = new System.Drawing.Point(13, 199);
            this.numSoLanThatBai.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numSoLanThatBai.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLanThatBai.Name = "numSoLanThatBai";
            this.numSoLanThatBai.Size = new System.Drawing.Size(60, 25);
            this.numSoLanThatBai.TabIndex = 11;
            this.numSoLanThatBai.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblSoLanThatBai
            // 
            this.lblSoLanThatBai.AutoSize = true;
            this.lblSoLanThatBai.Location = new System.Drawing.Point(13, 182);
            this.lblSoLanThatBai.Name = "lblSoLanThatBai";
            this.lblSoLanThatBai.Size = new System.Drawing.Size(123, 15);
            this.lblSoLanThatBai.TabIndex = 10;
            this.lblSoLanThatBai.Text = "Số lần thất bại tối đa:";
            // 
            // chkKhoaTaiKhoan
            // 
            this.chkKhoaTaiKhoan.AutoSize = true;
            this.chkKhoaTaiKhoan.Checked = true;
            this.chkKhoaTaiKhoan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKhoaTaiKhoan.Location = new System.Drawing.Point(13, 147);
            this.chkKhoaTaiKhoan.Name = "chkKhoaTaiKhoan";
            this.chkKhoaTaiKhoan.Size = new System.Drawing.Size(247, 19);
            this.chkKhoaTaiKhoan.TabIndex = 9;
            this.chkKhoaTaiKhoan.Text = "Tự động khóa tài khoản khi sai nhiều lần";
            // 
            // lblNgay2
            // 
            this.lblNgay2.AutoSize = true;
            this.lblNgay2.Location = new System.Drawing.Point(86, 107);
            this.lblNgay2.Name = "lblNgay2";
            this.lblNgay2.Size = new System.Drawing.Size(106, 15);
            this.lblNgay2.TabIndex = 8;
            this.lblNgay2.Text = "ngày (0 = vô hiệu)";
            // 
            // numHanHieuLuc
            // 
            this.numHanHieuLuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numHanHieuLuc.Location = new System.Drawing.Point(13, 104);
            this.numHanHieuLuc.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numHanHieuLuc.Name = "numHanHieuLuc";
            this.numHanHieuLuc.Size = new System.Drawing.Size(69, 25);
            this.numHanHieuLuc.TabIndex = 7;
            this.numHanHieuLuc.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // lblHanHieuLuc
            // 
            this.lblHanHieuLuc.AutoSize = true;
            this.lblHanHieuLuc.Location = new System.Drawing.Point(13, 87);
            this.lblHanHieuLuc.Name = "lblHanHieuLuc";
            this.lblHanHieuLuc.Size = new System.Drawing.Size(134, 15);
            this.lblHanHieuLuc.TabIndex = 6;
            this.lblHanHieuLuc.Text = "Hạn hiệu lực mật khẩu:";
            // 
            // chkYeuCauKyTuDacBiet
            // 
            this.chkYeuCauKyTuDacBiet.AutoSize = true;
            this.chkYeuCauKyTuDacBiet.Location = new System.Drawing.Point(471, 45);
            this.chkYeuCauKyTuDacBiet.Name = "chkYeuCauKyTuDacBiet";
            this.chkYeuCauKyTuDacBiet.Size = new System.Drawing.Size(104, 19);
            this.chkYeuCauKyTuDacBiet.TabIndex = 5;
            this.chkYeuCauKyTuDacBiet.Text = "Ký tự đặc biệt";
            // 
            // chkYeuCauSo
            // 
            this.chkYeuCauSo.AutoSize = true;
            this.chkYeuCauSo.Checked = true;
            this.chkYeuCauSo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYeuCauSo.Location = new System.Drawing.Point(360, 45);
            this.chkYeuCauSo.Name = "chkYeuCauSo";
            this.chkYeuCauSo.Size = new System.Drawing.Size(83, 19);
            this.chkYeuCauSo.TabIndex = 4;
            this.chkYeuCauSo.Text = "Yêu cầu số";
            // 
            // chkYeuCauChuThuong
            // 
            this.chkYeuCauChuThuong.AutoSize = true;
            this.chkYeuCauChuThuong.Checked = true;
            this.chkYeuCauChuThuong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYeuCauChuThuong.Location = new System.Drawing.Point(223, 45);
            this.chkYeuCauChuThuong.Name = "chkYeuCauChuThuong";
            this.chkYeuCauChuThuong.Size = new System.Drawing.Size(137, 19);
            this.chkYeuCauChuThuong.TabIndex = 3;
            this.chkYeuCauChuThuong.Text = "Yêu cầu chữ thường";
            // 
            // chkYeuCauChuHoa
            // 
            this.chkYeuCauChuHoa.AutoSize = true;
            this.chkYeuCauChuHoa.Checked = true;
            this.chkYeuCauChuHoa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYeuCauChuHoa.Location = new System.Drawing.Point(111, 45);
            this.chkYeuCauChuHoa.Name = "chkYeuCauChuHoa";
            this.chkYeuCauChuHoa.Size = new System.Drawing.Size(121, 19);
            this.chkYeuCauChuHoa.TabIndex = 2;
            this.chkYeuCauChuHoa.Text = "Yêu cầu chữ HOA";
            // 
            // numMinLength
            // 
            this.numMinLength.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numMinLength.Location = new System.Drawing.Point(13, 43);
            this.numMinLength.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numMinLength.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numMinLength.Name = "numMinLength";
            this.numMinLength.Size = new System.Drawing.Size(60, 25);
            this.numMinLength.TabIndex = 1;
            this.numMinLength.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // lblMinLength
            // 
            this.lblMinLength.AutoSize = true;
            this.lblMinLength.Location = new System.Drawing.Point(13, 26);
            this.lblMinLength.Name = "lblMinLength";
            this.lblMinLength.Size = new System.Drawing.Size(95, 15);
            this.lblMinLength.TabIndex = 0;
            this.lblMinLength.Text = "Độ dài tối thiểu:";
            // 
            // tabHienThi
            // 
            this.tabHienThi.Controls.Add(this.grpGiaoDien);
            this.tabHienThi.Location = new System.Drawing.Point(4, 24);
            this.tabHienThi.Name = "tabHienThi";
            this.tabHienThi.Size = new System.Drawing.Size(592, 258);
            this.tabHienThi.TabIndex = 2;
            this.tabHienThi.Text = "Hiển thị";
            this.tabHienThi.UseVisualStyleBackColor = true;
            // 
            // grpGiaoDien
            // 
            this.grpGiaoDien.Controls.Add(this.lblPt);
            this.grpGiaoDien.Controls.Add(this.numFontSize);
            this.grpGiaoDien.Controls.Add(this.lblKichThuocChu);
            this.grpGiaoDien.Controls.Add(this.chkHienThiAvatar);
            this.grpGiaoDien.Controls.Add(this.chkHienThiTen);
            this.grpGiaoDien.Controls.Add(this.cboPhongCach);
            this.grpGiaoDien.Controls.Add(this.lblPhongCach);
            this.grpGiaoDien.Controls.Add(this.cboMauNen);
            this.grpGiaoDien.Controls.Add(this.lblMauNen);
            this.grpGiaoDien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpGiaoDien.Location = new System.Drawing.Point(9, 9);
            this.grpGiaoDien.Name = "grpGiaoDien";
            this.grpGiaoDien.Size = new System.Drawing.Size(566, 234);
            this.grpGiaoDien.TabIndex = 0;
            this.grpGiaoDien.TabStop = false;
            this.grpGiaoDien.Text = "Giao diện";
            // 
            // lblPt
            // 
            this.lblPt.AutoSize = true;
            this.lblPt.Location = new System.Drawing.Point(69, 190);
            this.lblPt.Name = "lblPt";
            this.lblPt.Size = new System.Drawing.Size(19, 15);
            this.lblPt.TabIndex = 8;
            this.lblPt.Text = "pt";
            // 
            // numFontSize
            // 
            this.numFontSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numFontSize.Location = new System.Drawing.Point(13, 186);
            this.numFontSize.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numFontSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(51, 25);
            this.numFontSize.TabIndex = 7;
            this.numFontSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblKichThuocChu
            // 
            this.lblKichThuocChu.AutoSize = true;
            this.lblKichThuocChu.Location = new System.Drawing.Point(13, 169);
            this.lblKichThuocChu.Name = "lblKichThuocChu";
            this.lblKichThuocChu.Size = new System.Drawing.Size(95, 15);
            this.lblKichThuocChu.TabIndex = 6;
            this.lblKichThuocChu.Text = "Kích thước chữ:";
            // 
            // chkHienThiAvatar
            // 
            this.chkHienThiAvatar.AutoSize = true;
            this.chkHienThiAvatar.Checked = true;
            this.chkHienThiAvatar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHienThiAvatar.Location = new System.Drawing.Point(13, 130);
            this.chkHienThiAvatar.Name = "chkHienThiAvatar";
            this.chkHienThiAvatar.Size = new System.Drawing.Size(140, 19);
            this.chkHienThiAvatar.TabIndex = 5;
            this.chkHienThiAvatar.Text = "Hiển thị ảnh đại diện";
            // 
            // chkHienThiTen
            // 
            this.chkHienThiTen.AutoSize = true;
            this.chkHienThiTen.Checked = true;
            this.chkHienThiTen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHienThiTen.Location = new System.Drawing.Point(13, 95);
            this.chkHienThiTen.Name = "chkHienThiTen";
            this.chkHienThiTen.Size = new System.Drawing.Size(159, 19);
            this.chkHienThiTen.TabIndex = 4;
            this.chkHienThiTen.Text = "Hiển thị tên người dùng";
            // 
            // cboPhongCach
            // 
            this.cboPhongCach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhongCach.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboPhongCach.Items.AddRange(new object[] {
            "Hiện đại (Modern)",
            "Cổ điển (Classic)",
            "Tối giản (Minimal)"});
            this.cboPhongCach.Location = new System.Drawing.Point(214, 43);
            this.cboPhongCach.Name = "cboPhongCach";
            this.cboPhongCach.Size = new System.Drawing.Size(172, 25);
            this.cboPhongCach.TabIndex = 3;
            // 
            // lblPhongCach
            // 
            this.lblPhongCach.AutoSize = true;
            this.lblPhongCach.Location = new System.Drawing.Point(214, 26);
            this.lblPhongCach.Name = "lblPhongCach";
            this.lblPhongCach.Size = new System.Drawing.Size(73, 15);
            this.lblPhongCach.TabIndex = 2;
            this.lblPhongCach.Text = "Phong cách:";
            // 
            // cboMauNen
            // 
            this.cboMauNen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMauNen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboMauNen.Items.AddRange(new object[] {
            "Xanh dương (Mặc định)",
            "Tối (Dark)",
            "Sáng (Light)",
            "Xanh lá"});
            this.cboMauNen.Location = new System.Drawing.Point(13, 43);
            this.cboMauNen.Name = "cboMauNen";
            this.cboMauNen.Size = new System.Drawing.Size(172, 25);
            this.cboMauNen.TabIndex = 1;
            // 
            // lblMauNen
            // 
            this.lblMauNen.AutoSize = true;
            this.lblMauNen.Location = new System.Drawing.Point(13, 26);
            this.lblMauNen.Name = "lblMauNen";
            this.lblMauNen.Size = new System.Drawing.Size(58, 15);
            this.lblMauNen.TabIndex = 0;
            this.lblMauNen.Text = "Màu nền:";
            // 
            // tabNgonNgu
            // 
            this.tabNgonNgu.Controls.Add(this.btnXoaNgonNgu);
            this.tabNgonNgu.Controls.Add(this.btnThemNgonNgu);
            this.tabNgonNgu.Controls.Add(this.dgvNgonNgu);
            this.tabNgonNgu.Controls.Add(this.grpNgonNgu);
            this.tabNgonNgu.Location = new System.Drawing.Point(4, 24);
            this.tabNgonNgu.Name = "tabNgonNgu";
            this.tabNgonNgu.Size = new System.Drawing.Size(592, 258);
            this.tabNgonNgu.TabIndex = 3;
            this.tabNgonNgu.Text = "Ngôn ngữ";
            this.tabNgonNgu.UseVisualStyleBackColor = true;
            // 
            // btnXoaNgonNgu
            // 
            this.btnXoaNgonNgu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaNgonNgu.Location = new System.Drawing.Point(124, 204);
            this.btnXoaNgonNgu.Name = "btnXoaNgonNgu";
            this.btnXoaNgonNgu.Size = new System.Drawing.Size(103, 28);
            this.btnXoaNgonNgu.TabIndex = 2;
            this.btnXoaNgonNgu.Text = "Xóa ngôn ngữ";
            this.btnXoaNgonNgu.UseVisualStyleBackColor = true;
            this.btnXoaNgonNgu.Click += new System.EventHandler(this.btnXoaNgonNgu_Click);
            // 
            // btnThemNgonNgu
            // 
            this.btnThemNgonNgu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemNgonNgu.Location = new System.Drawing.Point(13, 204);
            this.btnThemNgonNgu.Name = "btnThemNgonNgu";
            this.btnThemNgonNgu.Size = new System.Drawing.Size(103, 28);
            this.btnThemNgonNgu.TabIndex = 1;
            this.btnThemNgonNgu.Text = "Thêm ngôn ngữ";
            this.btnThemNgonNgu.UseVisualStyleBackColor = true;
            this.btnThemNgonNgu.Click += new System.EventHandler(this.btnThemNgonNgu_Click);
            // 
            // dgvNgonNgu
            // 
            this.dgvNgonNgu.AllowUserToAddRows = false;
            this.dgvNgonNgu.AllowUserToDeleteRows = false;
            this.dgvNgonNgu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNgonNgu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvNgonNgu.Location = new System.Drawing.Point(13, 17);
            this.dgvNgonNgu.Name = "dgvNgonNgu";
            this.dgvNgonNgu.RowHeadersVisible = false;
            this.dgvNgonNgu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNgonNgu.Size = new System.Drawing.Size(557, 173);
            this.dgvNgonNgu.TabIndex = 0;
            // 
            // grpNgonNgu
            // 
            this.grpNgonNgu.Location = new System.Drawing.Point(0, 0);
            this.grpNgonNgu.Name = "grpNgonNgu";
            this.grpNgonNgu.Size = new System.Drawing.Size(4, 4);
            this.grpNgonNgu.TabIndex = 0;
            this.grpNgonNgu.TabStop = false;
            this.grpNgonNgu.Text = "Danh sách ngôn ngữ";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã ngôn ngữ";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Tên ngôn ngữ";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Mặc định";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(766, 534);
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
            this.btnMacDinh.Location = new System.Drawing.Point(886, 534);
            this.btnMacDinh.Name = "btnMacDinh";
            this.btnMacDinh.Size = new System.Drawing.Size(125, 33);
            this.btnMacDinh.TabIndex = 2;
            this.btnMacDinh.Text = "Khôi phục mặc định";
            this.btnMacDinh.UseVisualStyleBackColor = true;
            this.btnMacDinh.Click += new System.EventHandler(this.btnMacDinh_Click);
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(100, 214);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(38, 15);
            this.lblNgay.TabIndex = 11;
            this.lblNgay.Text = "ngày";
            // 
            // CauHinhHeThongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnMacDinh);
            this.Controls.Add(this.tabConfig);
            this.Controls.Add(this.lblTitle);
            this.Name = "CauHinhHeThongForm";
            this.Size = new System.Drawing.Size(1065, 592);
            this.Load += new System.EventHandler(this.CauHinhHeThongForm_Load);
            this.tabConfig.ResumeLayout(false);
            this.tabChung.ResumeLayout(false);
            this.grpHeThong.ResumeLayout(false);
            this.grpHeThong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBackupInterval)).EndInit();
            this.tabBaoMat.ResumeLayout(false);
            this.grpMatKhau.ResumeLayout(false);
            this.grpMatKhau.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLanThatBai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHanHieuLuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinLength)).EndInit();
            this.tabHienThi.ResumeLayout(false);
            this.grpGiaoDien.ResumeLayout(false);
            this.grpGiaoDien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            this.tabNgonNgu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNgonNgu)).EndInit();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}
