using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class ThongTinCaNhanView : UserControl
    {
        private ComboBox cmbNhanVien;
        private Button btnTim, btnLuu, btnXemLichSu;
        private Panel panelForm, panelLichSu;

        // Fields
        private TextBox txtMaNV, txtHoTen, txtSoDT, txtEmail, txtDiaChi, txtGhiChu;
        private ComboBox cmbGioiTinh;
        private DateTimePicker dtpNgaySinh, dtpNgayVaoLam;
        private Label lblPhongBan, lblChucVu, lblTrangThai;

        // Lịch sử
        private DataGridView dgvLichSu;

        private string _nhanVienIdDaChon = "";

        public ThongTinCaNhanView()
        {
            InitializeComponent();
            LoadNhanVien();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = "👤 Thông tin cá nhân",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 80, 160),
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            // ── PANEL TÌM KIẾM ──
            var panelTim = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(10) };
            panelTim.Controls.Add(new Label { Text = "Chọn nhân viên:", Location = new Point(10, 17), AutoSize = true, Font = new Font("Segoe UI", 10) });
            cmbNhanVien = new ComboBox { Location = new Point(140, 14), Width = 300, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            btnTim = MakeBtn("🔍 Xem hồ sơ", new Point(450, 12), Color.FromArgb(0, 120, 215));
            btnTim.Click += BtnTim_Click;
            panelTim.Controls.AddRange(new Control[] { cmbNhanVien, btnTim });

            // ── PANEL FORM ──
            panelForm = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15), AutoScroll = true, Visible = false };

            // Thẻ thông tin tổng quan (readonly)
            var panelCard = new Panel { Location = new Point(10, 10), Size = new Size(750, 60), BackColor = Color.FromArgb(235, 245, 255), BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(10) };
            lblPhongBan = new Label { Location = new Point(10, 10), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray };
            lblChucVu = new Label { Location = new Point(250, 10), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray };
            lblTrangThai = new Label { Location = new Point(500, 10), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            panelCard.Controls.AddRange(new Control[] { lblPhongBan, lblChucVu, lblTrangThai });
            panelForm.Controls.Add(panelCard);

            int y = 80;
            txtMaNV = AddField(panelForm, "Mã nhân viên", ref y, readOnly: true);
            txtHoTen = AddField(panelForm, "Họ tên *", ref y);

            AddLbl(panelForm, "Giới tính", y);
            cmbGioiTinh = new ComboBox { Location = new Point(160, y), Width = 150, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGioiTinh.Items.AddRange(new[] { "Nam", "Nữ", "Khác" });
            panelForm.Controls.Add(cmbGioiTinh);
            y += 35;

            AddLbl(panelForm, "Ngày sinh", y);
            dtpNgaySinh = new DateTimePicker { Location = new Point(160, y), Width = 180, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panelForm.Controls.Add(dtpNgaySinh);
            y += 35;

            txtSoDT = AddField(panelForm, "Số điện thoại", ref y);
            txtEmail = AddField(panelForm, "Email", ref y);

            AddLbl(panelForm, "Địa chỉ", y);
            txtDiaChi = new TextBox { Location = new Point(160, y), Width = 450, Height = 55, Font = new Font("Segoe UI", 10), Multiline = true };
            panelForm.Controls.Add(txtDiaChi);
            y += 65;

            AddLbl(panelForm, "Ngày vào làm", y);
            dtpNgayVaoLam = new DateTimePicker { Location = new Point(160, y), Width = 180, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panelForm.Controls.Add(dtpNgayVaoLam);
            y += 35;

            AddLbl(panelForm, "Ghi chú", y);
            txtGhiChu = new TextBox { Location = new Point(160, y), Width = 450, Height = 50, Font = new Font("Segoe UI", 10), Multiline = true };
            panelForm.Controls.Add(txtGhiChu);
            y += 60;

            // Buttons
            btnLuu = MakeBtn("💾 Lưu thay đổi", new Point(160, y), Color.FromArgb(16, 137, 62));
            btnLuu.Click += BtnLuu_Click;
            btnXemLichSu = MakeBtn("🕓 Lịch sử thay đổi", new Point(310, y), Color.FromArgb(100, 100, 100));
            btnXemLichSu.Width = 160;
            btnXemLichSu.Click += (s, e) => { panelLichSu.Visible = !panelLichSu.Visible; if (panelLichSu.Visible) LoadLichSu(); };
            panelForm.Controls.AddRange(new Control[] { btnLuu, btnXemLichSu });
            y += 45;

            // Panel lịch sử
            panelLichSu = new Panel { Location = new Point(10, y), Size = new Size(750, 200), Visible = false, BorderStyle = BorderStyle.FixedSingle };
            panelLichSu.Controls.Add(new Label { Text = "🕓 Lịch sử thay đổi", Dock = DockStyle.Top, Height = 28, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(5, 0, 0, 0) });
            dgvLichSu = BuildLichSuDgv();
            panelLichSu.Controls.Add(dgvLichSu);
            panelForm.Controls.Add(panelLichSu);

            this.Controls.Add(panelForm);
            this.Controls.Add(panelTim);
            this.Controls.Add(lblTitle);
        }

        private void LoadNhanVien()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var rdr = new SqlCommand("SELECT NhanVienId, HoTen FROM NhanVien ORDER BY HoTen", conn).ExecuteReader();
                    while (rdr.Read())
                        cmbNhanVien.Items.Add(new ComboItem($"{rdr["HoTen"]} [{rdr["NhanVienId"]}]", 0));
                    cmbNhanVien.DisplayMember = "Text";
                }
            }
            catch { }
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedIndex < 0) { MessageBox.Show("Vui lòng chọn nhân viên!"); return; }
            _nhanVienIdDaChon = LayNVId();
            LoadHoSo();
            panelForm.Visible = true;
            panelLichSu.Visible = false;
        }

        private void LoadHoSo()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        SELECT nv.*, pb.TenPhongBan, cv.TenChucVu
                        FROM NhanVien nv
                        LEFT JOIN PhongBan pb ON nv.PhongBanId = pb.PhongBanId
                        LEFT JOIN ChucVu cv   ON nv.ChucVuId   = cv.ChucVuId
                        WHERE nv.NhanVienId = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", _nhanVienIdDaChon);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        txtMaNV.Text = rdr["NhanVienId"].ToString();
                        txtHoTen.Text = rdr["HoTen"].ToString();
                        txtSoDT.Text = rdr["SoDienThoai"].ToString();
                        txtEmail.Text = rdr["Email"].ToString();
                        txtDiaChi.Text = rdr["DiaChi"].ToString();
                        txtGhiChu.Text = rdr["GhiChu"].ToString();
                        cmbGioiTinh.Text = rdr["GioiTinh"].ToString();
                        if (rdr["NgaySinh"] != DBNull.Value) dtpNgaySinh.Value = (DateTime)rdr["NgaySinh"];
                        if (rdr["NgayVaoLam"] != DBNull.Value) dtpNgayVaoLam.Value = (DateTime)rdr["NgayVaoLam"];

                        lblPhongBan.Text = $"🏢 Phòng ban: {rdr["TenPhongBan"]}";
                        lblChucVu.Text = $"💼 Chức vụ: {rdr["TenChucVu"]}";
                        string tt = rdr["TrangThai"]?.ToString() ?? "Đang làm";
                        lblTrangThai.Text = $"● {tt}";
                        lblTrangThai.ForeColor = tt == "Đang làm" ? Color.FromArgb(16, 137, 62) : Color.OrangeRed;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_nhanVienIdDaChon)) return;
            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Họ tên không được để trống!"); return; }

            try
            {
                // Lấy giá trị cũ để ghi lịch sử
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var tran = conn.BeginTransaction();
                    try
                    {
                        // Đọc giá trị cũ
                        var cmdOld = new SqlCommand("SELECT * FROM NhanVien WHERE NhanVienId=@Id", conn, tran);
                        cmdOld.Parameters.AddWithValue("@Id", _nhanVienIdDaChon);
                        var rdrOld = cmdOld.ExecuteReader();
                        string hoTenCu = "", sdtCu = "", emailCu = "", diaChiCu = "", gioiTinhCu = "";
                        if (rdrOld.Read())
                        {
                            hoTenCu = rdrOld["HoTen"].ToString();
                            sdtCu = rdrOld["SoDienThoai"].ToString();
                            emailCu = rdrOld["Email"].ToString();
                            diaChiCu = rdrOld["DiaChi"].ToString();
                            gioiTinhCu = rdrOld["GioiTinh"].ToString();
                        }
                        rdrOld.Close();

                        // Update
                        var cmdUpd = new SqlCommand(@"
                            UPDATE NhanVien SET HoTen=@HoTen, GioiTinh=@GT, NgaySinh=@NS,
                            SoDienThoai=@SDT, Email=@Email, DiaChi=@DiaChi,
                            NgayVaoLam=@NVL, GhiChu=@GhiChu
                            WHERE NhanVienId=@Id", conn, tran);
                        cmdUpd.Parameters.AddWithValue("@Id", _nhanVienIdDaChon);
                        cmdUpd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmdUpd.Parameters.AddWithValue("@GT", cmbGioiTinh.Text);
                        cmdUpd.Parameters.AddWithValue("@NS", dtpNgaySinh.Value.Date);
                        cmdUpd.Parameters.AddWithValue("@SDT", txtSoDT.Text);
                        cmdUpd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmdUpd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                        cmdUpd.Parameters.AddWithValue("@NVL", dtpNgayVaoLam.Value.Date);
                        cmdUpd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                        cmdUpd.ExecuteNonQuery();

                        // Ghi lịch sử các trường thay đổi
                        GhiLichSu(conn, tran, "Họ tên", hoTenCu, txtHoTen.Text.Trim());
                        GhiLichSu(conn, tran, "Số điện thoại", sdtCu, txtSoDT.Text);
                        GhiLichSu(conn, tran, "Email", emailCu, txtEmail.Text);
                        GhiLichSu(conn, tran, "Địa chỉ", diaChiCu, txtDiaChi.Text);
                        GhiLichSu(conn, tran, "Giới tính", gioiTinhCu, cmbGioiTinh.Text);

                        tran.Commit();
                        MessageBox.Show("✅ Lưu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadHoSo();
                    }
                    catch { tran.Rollback(); throw; }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void GhiLichSu(SqlConnection conn, SqlTransaction tran, string truong, string cu, string moi)
        {
            if (cu == moi) return;
            var cmd = new SqlCommand(@"
                INSERT INTO LichSuThayDoiHoSo (NhanVienId, TruongThayDoi, GiaTriCu, GiaTriMoi, NguoiThayDoi)
                VALUES (@NVId, @Truong, @Cu, @Moi, @Nguoi)", conn, tran);
            cmd.Parameters.AddWithValue("@NVId", _nhanVienIdDaChon);
            cmd.Parameters.AddWithValue("@Truong", truong);
            cmd.Parameters.AddWithValue("@Cu", cu);
            cmd.Parameters.AddWithValue("@Moi", moi);
            cmd.Parameters.AddWithValue("@Nguoi", "HR User");
            cmd.ExecuteNonQuery();
        }

        private void LoadLichSu()
        {
            try
            {
                dgvLichSu.Rows.Clear();
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        SELECT TOP 50 * FROM LichSuThayDoiHoSo
                        WHERE NhanVienId=@Id ORDER BY NgayThayDoi DESC", conn);
                    cmd.Parameters.AddWithValue("@Id", _nhanVienIdDaChon);
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                        dgvLichSu.Rows.Add(
                            ((DateTime)rdr["NgayThayDoi"]).ToString("dd/MM/yyyy HH:mm"),
                            rdr["TruongThayDoi"], rdr["GiaTriCu"], rdr["GiaTriMoi"], rdr["NguoiThayDoi"]);
                }
            }
            catch { }
        }

        private DataGridView BuildLichSuDgv()
        {
            var dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, RowHeadersVisible = false, Font = new Font("Segoe UI", 8), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White, BorderStyle = BorderStyle.None };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 70, 70);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thời gian", FillWeight = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trường", FillWeight = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giá trị cũ", FillWeight = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giá trị mới", FillWeight = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Người sửa", FillWeight = 100 });
            return dgv;
        }

        private TextBox AddField(Panel p, string label, ref int y, bool readOnly = false)
        {
            AddLbl(p, label, y);
            var txt = new TextBox { Location = new Point(160, y), Width = 300, Font = new Font("Segoe UI", 10), ReadOnly = readOnly, BackColor = readOnly ? Color.FromArgb(240, 240, 240) : Color.White };
            p.Controls.Add(txt); y += 35;
            return txt;
        }

        private void AddLbl(Panel p, string text, int y) =>
            p.Controls.Add(new Label { Text = text, Location = new Point(10, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(60, 60, 60) });

        private Button MakeBtn(string text, Point loc, Color color)
        {
            var btn = new Button { Text = text, Location = loc, Size = new Size(140, 32), Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private string LayNVId()
        {
            string text = cmbNhanVien.SelectedItem?.ToString() ?? "";
            return text.Contains("[") ? text.Substring(text.LastIndexOf('[') + 1).TrimEnd(']') : "";
        }
    }
}