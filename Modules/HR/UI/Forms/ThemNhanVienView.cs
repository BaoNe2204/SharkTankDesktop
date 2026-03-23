using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class ThemNhanVienView : UserControl
    {
        private string _nhanVienId;
        private bool _readOnly;
        private bool _isEditMode;

        // Tab
        private TabControl tabMain;
        private TabPage tabThongTin, tabGiayTo, tabPhongBan, tabHopDong;

        // Tab 1 - Thông tin cá nhân
        private TextBox txtNhanVienId, txtHoTen, txtSoDienThoai, txtEmail, txtDiaChi, txtGhiChu;
        private ComboBox cmbGioiTinh;
        private DateTimePicker dtpNgaySinh;
        private PictureBox picAnhDaiDien;
        private Button btnChonAnh;
        private string _anhDuongDan = "";

        // Tab 2 - Giấy tờ
        private ComboBox cmbLoaiGiayTo;
        private TextBox txtSoGiayTo, txtNoiCap;
        private DateTimePicker dtpNgayCap, dtpNgayHetHan;

        // Tab 3 - Phòng ban & Chức vụ
        private ComboBox cmbPhongBan, cmbChucVu;
        private DateTimePicker dtpNgayVaoLam;

        // Tab 4 - Hợp đồng
        private TextBox txtSoHopDong;
        private ComboBox cmbLoaiHopDong, cmbHinhThucTraLuong;
        private DateTimePicker dtpHDNgayBatDau, dtpHDNgayKetThuc;
        private NumericUpDown nudLuongCoBan, nudPhuCap;
        private CheckBox chkTaoHopDong;

        // Buttons
        private Button btnLuu, btnHuy;

        public ThemNhanVienView(string nhanVienId = null, bool readOnly = false)
        {
            _nhanVienId = nhanVienId;
            _readOnly = readOnly;
            _isEditMode = !string.IsNullOrEmpty(nhanVienId);

            InitializeComponent();
            LoadComboBoxData();

            if (_isEditMode)
                LoadNhanVienData();
            else
                txtNhanVienId.Text = SinhMaNhanVien();

            SetReadOnly(_readOnly);
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
            this.Padding = new Padding(15);

            // ── TIÊU ĐỀ ──
            var lblTitle = new Label
            {
                Text = _isEditMode ? (_readOnly ? "👁 Chi tiết nhân viên" : "✏️ Chỉnh sửa nhân viên") : "➕ Thêm nhân viên mới",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 80, 160),
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // ── TAB CONTROL ──
            tabMain = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };

            tabThongTin = new TabPage("👤 Thông tin cá nhân");
            tabGiayTo = new TabPage("📋 Giấy tờ");
            tabPhongBan = new TabPage("🏢 Phòng ban & Chức vụ");
            tabHopDong = new TabPage("📄 Hợp đồng");

            tabMain.TabPages.AddRange(new[] { tabThongTin, tabGiayTo, tabPhongBan, tabHopDong });

            BuildTabThongTin();
            BuildTabGiayTo();
            BuildTabPhongBan();
            BuildTabHopDong();

            // ── BUTTONS ──
            var panelBtn = new Panel { Dock = DockStyle.Bottom, Height = 50, BackColor = Color.FromArgb(245, 247, 250) };

            btnLuu = new Button
            {
                Text = "💾 Lưu",
                Size = new Size(110, 33),
                Location = new Point(15, 8),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Visible = !_readOnly
            };
            btnLuu.FlatAppearance.BorderSize = 0;

            btnHuy = new Button
            {
                Text = "Đóng",
                Size = new Size(90, 33),
                Location = new Point(135, 8),
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            panelBtn.Controls.AddRange(new Control[] { btnLuu, btnHuy });

            this.Controls.Add(tabMain);
            this.Controls.Add(lblTitle);
            this.Controls.Add(panelBtn);

            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += (s, e) => this.ParentForm?.Close();
        }

        // ── TAB 1: THÔNG TIN CÁ NHÂN ──
        private void BuildTabThongTin()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15), AutoScroll = true };

            // Ảnh bên phải
            picAnhDaiDien = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(650, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(230, 235, 240)
            };

            btnChonAnh = new Button
            {
                Text = "📷 Chọn ảnh",
                Size = new Size(120, 28),
                Location = new Point(650, 148),
                Font = new Font("Segoe UI", 9),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnChonAnh.Click += BtnChonAnh_Click;

            // Fields bên trái
            int y = 20;
            txtNhanVienId = AddTextField(panel, "Mã nhân viên *", ref y, readOnly: true);
            txtHoTen = AddTextField(panel, "Họ tên *", ref y);

            // Giới tính
            AddLabel(panel, "Giới tính", y);
            cmbGioiTinh = new ComboBox { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGioiTinh.Items.AddRange(new[] { "Nam", "Nữ", "Khác" });
            cmbGioiTinh.SelectedIndex = 0;
            panel.Controls.Add(cmbGioiTinh);
            y += 35;

            // Ngày sinh
            AddLabel(panel, "Ngày sinh", y);
            dtpNgaySinh = new DateTimePicker { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panel.Controls.Add(dtpNgaySinh);
            y += 35;

            txtSoDienThoai = AddTextField(panel, "Số điện thoại", ref y);
            txtEmail = AddTextField(panel, "Email", ref y);

            AddLabel(panel, "Địa chỉ", y);
            txtDiaChi = new TextBox { Location = new Point(160, y), Width = 450, Height = 60, Font = new Font("Segoe UI", 10), Multiline = true };
            panel.Controls.Add(txtDiaChi);
            y += 70;

            AddLabel(panel, "Ghi chú", y);
            txtGhiChu = new TextBox { Location = new Point(160, y), Width = 450, Height = 50, Font = new Font("Segoe UI", 10), Multiline = true };
            panel.Controls.Add(txtGhiChu);

            panel.Controls.AddRange(new Control[] { picAnhDaiDien, btnChonAnh });
            tabThongTin.Controls.Add(panel);
        }

        // ── TAB 2: GIẤY TỜ ──
        private void BuildTabGiayTo()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            int y = 20;

            AddLabel(panel, "Loại giấy tờ", y);
            cmbLoaiGiayTo = new ComboBox { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLoaiGiayTo.Items.AddRange(new[] { "CCCD", "Hộ chiếu" });
            cmbLoaiGiayTo.SelectedIndex = 0;
            panel.Controls.Add(cmbLoaiGiayTo);
            y += 35;

            txtSoGiayTo = AddTextField(panel, "Số giấy tờ", ref y);

            AddLabel(panel, "Ngày cấp", y);
            dtpNgayCap = new DateTimePicker { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panel.Controls.Add(dtpNgayCap);
            y += 35;

            txtNoiCap = AddTextField(panel, "Nơi cấp", ref y);

            AddLabel(panel, "Ngày hết hạn", y);
            dtpNgayHetHan = new DateTimePicker { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panel.Controls.Add(dtpNgayHetHan);

            tabGiayTo.Controls.Add(panel);
        }

        // ── TAB 3: PHÒNG BAN ──
        private void BuildTabPhongBan()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            int y = 20;

            AddLabel(panel, "Phòng ban *", y);
            cmbPhongBan = new ComboBox { Location = new Point(160, y), Width = 250, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            panel.Controls.Add(cmbPhongBan);
            y += 35;

            AddLabel(panel, "Chức vụ *", y);
            cmbChucVu = new ComboBox { Location = new Point(160, y), Width = 250, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            panel.Controls.Add(cmbChucVu);
            y += 35;

            AddLabel(panel, "Ngày vào làm", y);
            dtpNgayVaoLam = new DateTimePicker { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panel.Controls.Add(dtpNgayVaoLam);

            tabPhongBan.Controls.Add(panel);
        }

        // ── TAB 4: HỢP ĐỒNG ──
        private void BuildTabHopDong()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            int y = 20;

            chkTaoHopDong = new CheckBox
            {
                Text = "Tạo hợp đồng ngay khi thêm nhân viên",
                Location = new Point(10, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Checked = true
            };
            chkTaoHopDong.CheckedChanged += (s, e) => SetHopDongEnabled(chkTaoHopDong.Checked);
            panel.Controls.Add(chkTaoHopDong);
            y += 40;

            txtSoHopDong = AddTextField(panel, "Số hợp đồng", ref y, readOnly: true);
            txtSoHopDong.Text = SinhSoHopDong();

            AddLabel(panel, "Loại hợp đồng", y);
            cmbLoaiHopDong = new ComboBox { Location = new Point(160, y), Width = 250, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLoaiHopDong.Items.AddRange(new[] { "Xác định thời hạn", "Không xác định thời hạn", "Thử việc", "Theo mùa vụ" });
            cmbLoaiHopDong.SelectedIndex = 0;
            cmbLoaiHopDong.SelectedIndexChanged += (s, e) =>
                dtpHDNgayKetThuc.Enabled = cmbLoaiHopDong.Text == "Xác định thời hạn" || cmbLoaiHopDong.Text == "Thử việc";
            panel.Controls.Add(cmbLoaiHopDong);
            y += 35;

            AddLabel(panel, "Ngày bắt đầu", y);
            dtpHDNgayBatDau = new DateTimePicker { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panel.Controls.Add(dtpHDNgayBatDau);
            y += 35;

            AddLabel(panel, "Ngày kết thúc", y);
            dtpHDNgayKetThuc = new DateTimePicker { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            dtpHDNgayKetThuc.Value = DateTime.Now.AddYears(1);
            panel.Controls.Add(dtpHDNgayKetThuc);
            y += 35;

            AddLabel(panel, "Lương cơ bản", y);
            nudLuongCoBan = new NumericUpDown { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Maximum = 999999999, DecimalPlaces = 0, ThousandsSeparator = true };
            panel.Controls.Add(nudLuongCoBan);
            y += 35;

            AddLabel(panel, "Phụ cấp", y);
            nudPhuCap = new NumericUpDown { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Maximum = 999999999, DecimalPlaces = 0, ThousandsSeparator = true };
            panel.Controls.Add(nudPhuCap);
            y += 35;

            AddLabel(panel, "Hình thức trả lương", y);
            cmbHinhThucTraLuong = new ComboBox { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbHinhThucTraLuong.Items.AddRange(new[] { "Theo tháng", "Theo tuần", "Theo ngày", "Theo giờ" });
            cmbHinhThucTraLuong.SelectedIndex = 0;
            panel.Controls.Add(cmbHinhThucTraLuong);

            tabHopDong.Controls.Add(panel);
        }

        // ── HELPER: thêm label + textbox ──
        private TextBox AddTextField(Panel panel, string labelText, ref int y, bool readOnly = false, int width = 300)
        {
            AddLabel(panel, labelText, y);
            var txt = new TextBox
            {
                Location = new Point(160, y),
                Width = width,
                Font = new Font("Segoe UI", 10),
                ReadOnly = readOnly,
                BackColor = readOnly ? Color.FromArgb(240, 240, 240) : Color.White
            };
            panel.Controls.Add(txt);
            y += 35;
            return txt;
        }

        private void AddLabel(Panel panel, string text, int y)
        {
            panel.Controls.Add(new Label
            {
                Text = text,
                Location = new Point(10, y + 3),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(60, 60, 60)
            });
        }

        private void SetHopDongEnabled(bool enabled)
        {
            foreach (Control c in tabHopDong.Controls[0].Controls)
                if (c != chkTaoHopDong) c.Enabled = enabled;
        }

        private void SetReadOnly(bool ro)
        {
            foreach (TabPage tab in tabMain.TabPages)
                SetControlsReadOnly(tab, ro);
            btnLuu.Visible = !ro;
        }

        private void SetControlsReadOnly(Control parent, bool ro)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox tb) tb.ReadOnly = ro;
                else if (c is ComboBox cb) cb.Enabled = !ro;
                else if (c is DateTimePicker dtp) dtp.Enabled = !ro;
                else if (c is NumericUpDown nud) nud.Enabled = !ro;
                else if (c is CheckBox chk) chk.Enabled = !ro;
                else if (c is Button btn && btn != btnChonAnh) btn.Enabled = !ro;
                else SetControlsReadOnly(c, ro);
            }
        }

        // ── LOAD DỮ LIỆU COMBO ──
        private void LoadComboBoxData()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    var cmdPB = new SqlCommand("SELECT PhongBanId, TenPhongBan FROM PhongBan ORDER BY TenPhongBan", conn);
                    var rdrPB = cmdPB.ExecuteReader();
                    while (rdrPB.Read())
                        cmbPhongBan.Items.Add(new ComboItem(rdrPB["TenPhongBan"].ToString(), (int)rdrPB["PhongBanId"]));
                    rdrPB.Close();
                    if (cmbPhongBan.Items.Count > 0) cmbPhongBan.SelectedIndex = 0;

                    var cmdCV = new SqlCommand("SELECT ChucVuId, TenChucVu FROM ChucVu ORDER BY TenChucVu", conn);
                    var rdrCV = cmdCV.ExecuteReader();
                    while (rdrCV.Read())
                        cmbChucVu.Items.Add(new ComboItem(rdrCV["TenChucVu"].ToString(), (int)rdrCV["ChucVuId"]));
                    rdrCV.Close();
                    if (cmbChucVu.Items.Count > 0) cmbChucVu.SelectedIndex = 0;
                }
            }
            catch { /* DB chưa sẵn sàng */ }
        }

        // ── LOAD DỮ LIỆU NHÂN VIÊN KHI SỬA ──
        private void LoadNhanVienData()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    // Thông tin nhân viên
                    var cmd = new SqlCommand("SELECT * FROM NhanVien WHERE NhanVienId = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", _nhanVienId);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        txtNhanVienId.Text = rdr["NhanVienId"].ToString();
                        txtHoTen.Text = rdr["HoTen"].ToString();
                        txtSoDienThoai.Text = rdr["SoDienThoai"].ToString();
                        txtEmail.Text = rdr["Email"].ToString();
                        txtDiaChi.Text = rdr["DiaChi"].ToString();
                        txtGhiChu.Text = rdr["GhiChu"].ToString();
                        cmbGioiTinh.Text = rdr["GioiTinh"].ToString();
                        if (rdr["NgaySinh"] != DBNull.Value)
                            dtpNgaySinh.Value = (DateTime)rdr["NgaySinh"];
                        if (rdr["NgayVaoLam"] != DBNull.Value)
                            dtpNgayVaoLam.Value = (DateTime)rdr["NgayVaoLam"];

                        // Phòng ban & Chức vụ
                        int pbId = rdr["PhongBanId"] != DBNull.Value ? (int)rdr["PhongBanId"] : 0;
                        int cvId = rdr["ChucVuId"] != DBNull.Value ? (int)rdr["ChucVuId"] : 0;
                        foreach (ComboItem item in cmbPhongBan.Items)
                            if (item.Value == pbId) { cmbPhongBan.SelectedItem = item; break; }
                        foreach (ComboItem item in cmbChucVu.Items)
                            if (item.Value == cvId) { cmbChucVu.SelectedItem = item; break; }

                        // Ảnh
                        _anhDuongDan = rdr["AnhDaiDien"].ToString();
                        if (!string.IsNullOrEmpty(_anhDuongDan) && File.Exists(_anhDuongDan))
                            picAnhDaiDien.Image = Image.FromFile(_anhDuongDan);
                    }
                    rdr.Close();

                    // Giấy tờ
                    var cmdGT = new SqlCommand("SELECT TOP 1 * FROM GiayToNhanVien WHERE NhanVienId = @Id ORDER BY Id DESC", conn);
                    cmdGT.Parameters.AddWithValue("@Id", _nhanVienId);
                    var rdrGT = cmdGT.ExecuteReader();
                    if (rdrGT.Read())
                    {
                        cmbLoaiGiayTo.Text = rdrGT["LoaiGiayTo"].ToString();
                        txtSoGiayTo.Text = rdrGT["SoGiayTo"].ToString();
                        txtNoiCap.Text = rdrGT["NoiCap"].ToString();
                        if (rdrGT["NgayCap"] != DBNull.Value) dtpNgayCap.Value = (DateTime)rdrGT["NgayCap"];
                        if (rdrGT["NgayHetHan"] != DBNull.Value) dtpNgayHetHan.Value = (DateTime)rdrGT["NgayHetHan"];
                    }
                    rdrGT.Close();

                    // Hợp đồng hiệu lực
                    var cmdHD = new SqlCommand("SELECT TOP 1 * FROM HopDongLaoDong WHERE NhanVienId = @Id AND TrangThai = N'Hiệu lực' ORDER BY NgayBatDau DESC", conn);
                    cmdHD.Parameters.AddWithValue("@Id", _nhanVienId);
                    var rdrHD = cmdHD.ExecuteReader();
                    if (rdrHD.Read())
                    {
                        txtSoHopDong.Text = rdrHD["SoHopDong"].ToString();
                        cmbLoaiHopDong.Text = rdrHD["LoaiHopDong"].ToString();
                        cmbHinhThucTraLuong.Text = rdrHD["HinhThucTraLuong"].ToString();
                        nudLuongCoBan.Value = rdrHD["LuongCoBan"] != DBNull.Value ? (decimal)rdrHD["LuongCoBan"] : 0;
                        nudPhuCap.Value = rdrHD["PhuCap"] != DBNull.Value ? (decimal)rdrHD["PhuCap"] : 0;
                        if (rdrHD["NgayBatDau"] != DBNull.Value) dtpHDNgayBatDau.Value = (DateTime)rdrHD["NgayBatDau"];
                        if (rdrHD["NgayKetThuc"] != DBNull.Value) dtpHDNgayKetThuc.Value = (DateTime)rdrHD["NgayKetThuc"];
                        chkTaoHopDong.Checked = true;
                    }
                    rdrHD.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── LƯU ──
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (!Validate_()) return;

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var tran = conn.BeginTransaction();
                    try
                    {
                        int pbId = (cmbPhongBan.SelectedItem as ComboItem)?.Value ?? 0;
                        int cvId = (cmbChucVu.SelectedItem as ComboItem)?.Value ?? 0;

                        NhanVienAuditSnapshot oldSnap = null;
                        if (_isEditMode)
                            oldSnap = ReadNhanVienSnapshot(conn, tran, _nhanVienId);

                        if (_isEditMode)
                        {
                            // UPDATE
                            var cmd = new SqlCommand(@"
                                UPDATE NhanVien SET
                                    HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh,
                                    DiaChi=@DiaChi, Email=@Email, SoDienThoai=@SDT,
                                    AnhDaiDien=@Anh, PhongBanId=@PBId, ChucVuId=@CVId,
                                    NgayVaoLam=@NVL, GhiChu=@GhiChu
                                WHERE NhanVienId=@Id", conn, tran);
                            cmd.Parameters.AddWithValue("@Id", _nhanVienId);
                            SetNhanVienParams(cmd, pbId, cvId);
                            cmd.ExecuteNonQuery();

                            // Xóa giấy tờ cũ rồi insert mới
                            new SqlCommand($"DELETE FROM GiayToNhanVien WHERE NhanVienId='{_nhanVienId}'", conn, tran).ExecuteNonQuery();
                        }
                        else
                        {
                            // INSERT
                            var cmd = new SqlCommand(@"
                                INSERT INTO NhanVien (NhanVienId,HoTen,NgaySinh,GioiTinh,DiaChi,Email,SoDienThoai,AnhDaiDien,PhongBanId,ChucVuId,NgayVaoLam,GhiChu)
                                VALUES (@Id,@HoTen,@NgaySinh,@GioiTinh,@DiaChi,@Email,@SDT,@Anh,@PBId,@CVId,@NVL,@GhiChu)", conn, tran);
                            cmd.Parameters.AddWithValue("@Id", txtNhanVienId.Text.Trim());
                            SetNhanVienParams(cmd, pbId, cvId);
                            cmd.ExecuteNonQuery();
                        }

                        // Mã NV dùng cho Giấy tờ / Hợp đồng / audit (chỉ khai báo một lần)
                        string nvId = _isEditMode ? _nhanVienId : txtNhanVienId.Text.Trim();

                        // Giấy tờ
                        var cmdGT = new SqlCommand(@"
                            INSERT INTO GiayToNhanVien (NhanVienId,LoaiGiayTo,SoGiayTo,NgayCap,NoiCap,NgayHetHan)
                            VALUES (@NVId,@Loai,@So,@NgayCap,@NoiCap,@HetHan)", conn, tran);
                        cmdGT.Parameters.AddWithValue("@NVId", nvId);
                        cmdGT.Parameters.AddWithValue("@Loai", cmbLoaiGiayTo.Text);
                        cmdGT.Parameters.AddWithValue("@So", txtSoGiayTo.Text);
                        cmdGT.Parameters.AddWithValue("@NgayCap", dtpNgayCap.Value.Date);
                        cmdGT.Parameters.AddWithValue("@NoiCap", txtNoiCap.Text);
                        cmdGT.Parameters.AddWithValue("@HetHan", dtpNgayHetHan.Value.Date);
                        cmdGT.ExecuteNonQuery();

                        // Hợp đồng
                        if (chkTaoHopDong.Checked && nudLuongCoBan.Value > 0)
                        {
                            // Đánh dấu hợp đồng cũ hết hạn
                            if (_isEditMode)
                                new SqlCommand($"UPDATE HopDongLaoDong SET TrangThai=N'Hết hạn' WHERE NhanVienId='{nvId}' AND TrangThai=N'Hiệu lực'", conn, tran).ExecuteNonQuery();

                            var cmdHD = new SqlCommand(@"
                                INSERT INTO HopDongLaoDong (SoHopDong,NhanVienId,LoaiHopDong,NgayBatDau,NgayKetThuc,LuongCoBan,PhuCap,HinhThucTraLuong,TrangThai)
                                VALUES (@SoHD,@NVId,@Loai,@BatDau,@KetThuc,@Luong,@PhuCap,@HinhThuc,N'Hiệu lực')", conn, tran);
                            cmdHD.Parameters.AddWithValue("@SoHD", txtSoHopDong.Text);
                            cmdHD.Parameters.AddWithValue("@NVId", nvId);
                            cmdHD.Parameters.AddWithValue("@Loai", cmbLoaiHopDong.Text);
                            cmdHD.Parameters.AddWithValue("@BatDau", dtpHDNgayBatDau.Value.Date);
                            cmdHD.Parameters.AddWithValue("@KetThuc", dtpHDNgayKetThuc.Value.Date);
                            cmdHD.Parameters.AddWithValue("@Luong", nudLuongCoBan.Value);
                            cmdHD.Parameters.AddWithValue("@PhuCap", nudPhuCap.Value);
                            cmdHD.Parameters.AddWithValue("@HinhThuc", cmbHinhThucTraLuong.Text);
                            cmdHD.ExecuteNonQuery();
                        }

                        tran.Commit();

                        // Ghi vào DataChangeLogs (màn "THEO DÕI THAY ĐỔI DỮ LIỆU") — không chỉ AuditLogs
                        var newSnap = BuildNhanVienSnapshotFromForm(pbId, cvId);
                        try
                        {
                            if (_isEditMode)
                                AuditService.CreateDefault().LogActionWithChanges(
                                    "UPDATE", "NhanVien", nvId, txtHoTen.Text.Trim(),
                                    oldSnap, newSnap, null);
                            else
                                AuditService.CreateDefault().LogActionWithChanges(
                                    "INSERT", "NhanVien", nvId, txtHoTen.Text.Trim(),
                                    null, newSnap, null);
                        }
                        catch { /* không chặn lưu nếu audit lỗi */ }

                        MessageBox.Show("✅ Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ParentForm?.Close();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetNhanVienParams(SqlCommand cmd, int pbId, int cvId)
        {
            cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
            cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value.Date);
            cmd.Parameters.AddWithValue("@GioiTinh", cmbGioiTinh.Text);
            cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@SDT", txtSoDienThoai.Text);
            cmd.Parameters.AddWithValue("@Anh", _anhDuongDan);
            cmd.Parameters.AddWithValue("@PBId", pbId > 0 ? (object)pbId : DBNull.Value);
            cmd.Parameters.AddWithValue("@CVId", cvId > 0 ? (object)cvId : DBNull.Value);
            cmd.Parameters.AddWithValue("@NVL", dtpNgayVaoLam.Value.Date);
            cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
        }

        private bool Validate_()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabMain.SelectedTab = tabThongTin;
                txtHoTen.Focus();
                return false;
            }
            if (cmbPhongBan.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn phòng ban!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabMain.SelectedTab = tabPhongBan;
                return false;
            }
            return true;
        }

        private void BtnChonAnh_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                dlg.Title = "Chọn ảnh đại diện";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _anhDuongDan = dlg.FileName;
                    picAnhDaiDien.Image = Image.FromFile(dlg.FileName);
                }
            }
        }

        /// <summary>Snapshot để so sánh và ghi DataChangeLogs (LogActionWithChanges).</summary>
        private sealed class NhanVienAuditSnapshot
        {
            public string HoTen { get; set; }
            public string NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string DiaChi { get; set; }
            public string Email { get; set; }
            public string SoDienThoai { get; set; }
            public string PhongBanId { get; set; }
            public string ChucVuId { get; set; }
            public string NgayVaoLam { get; set; }
            public string GhiChu { get; set; }
        }

        private static NhanVienAuditSnapshot ReadNhanVienSnapshot(SqlConnection conn, SqlTransaction tran, string nhanVienId)
        {
            using (var cmd = new SqlCommand(@"
                SELECT HoTen, NgaySinh, GioiTinh, DiaChi, Email, SoDienThoai, PhongBanId, ChucVuId, NgayVaoLam, GhiChu
                FROM NhanVien WHERE NhanVienId = @Id", conn, tran))
            {
                cmd.Parameters.AddWithValue("@Id", nhanVienId);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read()) return null;
                    return new NhanVienAuditSnapshot
                    {
                        HoTen = rdr["HoTen"]?.ToString(),
                        NgaySinh = rdr["NgaySinh"] == DBNull.Value ? "" : ((DateTime)rdr["NgaySinh"]).ToString("yyyy-MM-dd"),
                        GioiTinh = rdr["GioiTinh"]?.ToString(),
                        DiaChi = rdr["DiaChi"]?.ToString(),
                        Email = rdr["Email"]?.ToString(),
                        SoDienThoai = rdr["SoDienThoai"]?.ToString(),
                        PhongBanId = rdr["PhongBanId"] == DBNull.Value ? "" : rdr["PhongBanId"].ToString(),
                        ChucVuId = rdr["ChucVuId"] == DBNull.Value ? "" : rdr["ChucVuId"].ToString(),
                        NgayVaoLam = rdr["NgayVaoLam"] == DBNull.Value ? "" : ((DateTime)rdr["NgayVaoLam"]).ToString("yyyy-MM-dd"),
                        GhiChu = rdr["GhiChu"]?.ToString()
                    };
                }
            }
        }

        private NhanVienAuditSnapshot BuildNhanVienSnapshotFromForm(int pbId, int cvId)
        {
            return new NhanVienAuditSnapshot
            {
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value.Date.ToString("yyyy-MM-dd"),
                GioiTinh = cmbGioiTinh.Text,
                DiaChi = txtDiaChi.Text ?? "",
                Email = txtEmail.Text ?? "",
                SoDienThoai = txtSoDienThoai.Text ?? "",
                PhongBanId = pbId > 0 ? pbId.ToString() : "",
                ChucVuId = cvId > 0 ? cvId.ToString() : "",
                NgayVaoLam = dtpNgayVaoLam.Value.Date.ToString("yyyy-MM-dd"),
                GhiChu = txtGhiChu.Text ?? ""
            };
        }

        // ── SINH MÃ ──
        private string SinhMaNhanVien()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT COUNT(*) FROM NhanVien", conn);
                    int count = (int)cmd.ExecuteScalar();
                    return $"NV{(count + 1):D4}";
                }
            }
            catch { return $"NV{new Random().Next(1000, 9999)}"; }
        }

        private string SinhSoHopDong()
        {
            return $"HD-{DateTime.Now:yyyy}-{new Random().Next(100, 999):D3}";
        }
    }
}