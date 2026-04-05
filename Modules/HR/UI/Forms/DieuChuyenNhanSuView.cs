using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class DieuChuyenNhanSuView : UserControl
    {
        // Form điều chuyển
        private ComboBox cmbNhanVien, cmbPhongBanMoi, cmbChucVuMoi;
        private Label lblPhongBanHienTai, lblChucVuHienTai;
        private DateTimePicker dtpNgayHieuLuc;
        private TextBox txtLyDo;
        private Button btnDieuChuyen;
        private Panel panelNVInfo;

        // Lịch sử
        private DataGridView dgvLichSu;
        private ComboBox cmbLocNV;
        private Button btnLoc;

        private string _nhanVienId = "";

        public DieuChuyenNhanSuView()
        {
            InitializeComponent();
            LoadNhanVien();
            LoadPhongBan();
            LoadChucVu();
            LoadLichSu();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label { Text = "🔄 Điều chuyển nhân sự", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) };

            // ── PANEL ĐIỀU CHUYỂN ──
            var panelDC = new Panel { Dock = DockStyle.Top, Height = 280, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(15) };
            panelDC.Controls.Add(new Label { Text = "📋 Điều chuyển nhân viên", Location = new Point(10, 5), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) });

            int y = 30;
            AddLbl(panelDC, "Nhân viên *", 10, y);
            cmbNhanVien = new ComboBox { Location = new Point(170, y), Width = 300, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbNhanVien.SelectedIndexChanged += CmbNhanVien_Changed;
            panelDC.Controls.Add(cmbNhanVien);
            y += 38;

            // Thông tin hiện tại
            panelNVInfo = new Panel { Location = new Point(170, y), Size = new Size(550, 55), BackColor = Color.FromArgb(220, 235, 255), BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(8), Visible = false };
            lblPhongBanHienTai = new Label { Location = new Point(8, 8), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(0, 60, 130) };
            lblChucVuHienTai = new Label { Location = new Point(8, 28), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(0, 60, 130) };
            panelNVInfo.Controls.AddRange(new Control[] { lblPhongBanHienTai, lblChucVuHienTai });
            panelDC.Controls.Add(panelNVInfo);
            y += 65;

            AddLbl(panelDC, "Phòng ban mới *", 10, y);
            cmbPhongBanMoi = new ComboBox { Location = new Point(170, y), Width = 250, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            panelDC.Controls.Add(cmbPhongBanMoi);
            y += 38;

            AddLbl(panelDC, "Chức vụ mới", 10, y);
            cmbChucVuMoi = new ComboBox { Location = new Point(170, y), Width = 250, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            panelDC.Controls.Add(cmbChucVuMoi);
            y += 38;

            AddLbl(panelDC, "Ngày hiệu lực *", 10, y);
            dtpNgayHieuLuc = new DateTimePicker { Location = new Point(170, y), Width = 180, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panelDC.Controls.Add(dtpNgayHieuLuc);
            y += 38;

            AddLbl(panelDC, "Lý do", 10, y);
            txtLyDo = new TextBox { Location = new Point(170, y), Width = 380, Font = new Font("Segoe UI", 10) };
            panelDC.Controls.Add(txtLyDo);

            btnDieuChuyen = new Button
            {
                Text = "✅ Xác nhận điều chuyển",
                Location = new Point(560, y - 2),
                Size = new Size(180, 30),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDieuChuyen.FlatAppearance.BorderSize = 0;
            btnDieuChuyen.Click += BtnDieuChuyen_Click;
            panelDC.Controls.Add(btnDieuChuyen);

            // ── PANEL LỊCH SỬ ──
            var panelLS = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.FromArgb(235, 245, 255), Padding = new Padding(10) };
            panelLS.Controls.Add(new Label { Text = "🕓 Lịch sử điều chuyển", Location = new Point(10, 12), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) });
            AddLbl(panelLS, "Nhân viên:", 280, 12);
            cmbLocNV = new ComboBox { Location = new Point(355, 10), Width = 220, Font = new Font("Segoe UI", 9), DropDownStyle = ComboBoxStyle.DropDownList };
            panelLS.Controls.Add(cmbLocNV);
            btnLoc = new Button { Text = "🔍 Lọc", Location = new Point(585, 8), Size = new Size(80, 26), Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnLoc.FlatAppearance.BorderSize = 0;
            btnLoc.Click += (s, e) => LoadLichSu();
            panelLS.Controls.Add(btnLoc);

            // ── DGV LỊCH SỬ ──
            dgvLichSu = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvLichSu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 160);
            dgvLichSu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLichSu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvLichSu.EnableHeadersVisualStyles = false;
            dgvLichSu.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);

            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày HL", FillWeight = 90 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã NV", FillWeight = 70 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ tên", FillWeight = 140 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "PB cũ", FillWeight = 120 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "PB mới", FillWeight = 120 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "CV mới", FillWeight = 100 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Lý do", FillWeight = 150 });

            this.Controls.Add(dgvLichSu);
            this.Controls.Add(panelLS);
            this.Controls.Add(panelDC);
            this.Controls.Add(lblTitle);
        }

        private void LoadNhanVien()
        {
            try
            {
                cmbLocNV.Items.Add(new ComboItem("-- Tất cả --", 0));
                cmbLocNV.SelectedIndex = 0;
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var rdr = new SqlCommand("SELECT NhanVienId, HoTen FROM NhanVien ORDER BY HoTen", conn).ExecuteReader();
                    while (rdr.Read())
                    {
                        var item = new ComboItem($"{rdr["HoTen"]} [{rdr["NhanVienId"]}]", 0);
                        cmbNhanVien.Items.Add(item);
                        cmbLocNV.Items.Add(new ComboItem($"{rdr["HoTen"]} [{rdr["NhanVienId"]}]", 0));
                    }
                    cmbNhanVien.DisplayMember = "Text";
                    cmbLocNV.DisplayMember = "Text";
                }
            }
            catch { }
        }

        private void LoadPhongBan()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var rdr = new SqlCommand("SELECT PhongBanId, TenPhongBan FROM PhongBan ORDER BY TenPhongBan", conn).ExecuteReader();
                    while (rdr.Read())
                        cmbPhongBanMoi.Items.Add(new ComboItem(rdr["TenPhongBan"].ToString(), (int)rdr["PhongBanId"]));
                    cmbPhongBanMoi.DisplayMember = "Text";
                    if (cmbPhongBanMoi.Items.Count > 0) cmbPhongBanMoi.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private void LoadChucVu()
        {
            try
            {
                cmbChucVuMoi.Items.Add(new ComboItem("-- Giữ nguyên --", 0));
                cmbChucVuMoi.SelectedIndex = 0;
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var rdr = new SqlCommand("SELECT ChucVuId, TenChucVu FROM ChucVu ORDER BY TenChucVu", conn).ExecuteReader();
                    while (rdr.Read())
                        cmbChucVuMoi.Items.Add(new ComboItem(rdr["TenChucVu"].ToString(), (int)rdr["ChucVuId"]));
                    cmbChucVuMoi.DisplayMember = "Text";
                }
            }
            catch { }
        }

        private void CmbNhanVien_Changed(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedIndex < 0) return;
            _nhanVienId = LayNVId(cmbNhanVien.SelectedItem.ToString());
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        SELECT pb.TenPhongBan, cv.TenChucVu
                        FROM NhanVien nv
                        LEFT JOIN PhongBan pb ON nv.PhongBanId=pb.PhongBanId
                        LEFT JOIN ChucVu   cv ON nv.ChucVuId=cv.ChucVuId
                        WHERE nv.NhanVienId=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", _nhanVienId);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        lblPhongBanHienTai.Text = $"🏢 Phòng ban hiện tại: {rdr["TenPhongBan"]}";
                        lblChucVuHienTai.Text = $"💼 Chức vụ hiện tại: {rdr["TenChucVu"]}";
                        panelNVInfo.Visible = true;
                    }
                }
            }
            catch { }
        }

        private void BtnDieuChuyen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_nhanVienId)) { MessageBox.Show("Vui lòng chọn nhân viên!"); return; }
            if (cmbPhongBanMoi.SelectedIndex < 0) { MessageBox.Show("Vui lòng chọn phòng ban mới!"); return; }

            int pbMoiId = (cmbPhongBanMoi.SelectedItem as ComboItem)?.Value ?? 0;
            int cvMoiId = (cmbChucVuMoi.SelectedItem as ComboItem)?.Value ?? 0;

            string pbMoiTen = (cmbPhongBanMoi.SelectedItem as ComboItem)?.Text ?? "";

            if (MessageBox.Show($"Xác nhận điều chuyển nhân viên sang phòng ban '{pbMoiTen}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var tran = conn.BeginTransaction();
                    try
                    {
                        // Lấy PB cũ
                        var cmdCu = new SqlCommand("SELECT ISNULL(pb.TenPhongBan,'--') FROM NhanVien nv LEFT JOIN PhongBan pb ON nv.PhongBanId=pb.PhongBanId WHERE nv.NhanVienId=@Id", conn, tran);
                        cmdCu.Parameters.AddWithValue("@Id", _nhanVienId);
                        string pbCuTen = cmdCu.ExecuteScalar()?.ToString() ?? "--";

                        // Update nhân viên
                        string sqlUpd = cvMoiId > 0
                            ? "UPDATE NhanVien SET PhongBanId=@PBId, ChucVuId=@CVId WHERE NhanVienId=@Id"
                            : "UPDATE NhanVien SET PhongBanId=@PBId WHERE NhanVienId=@Id";
                        var cmdUpd = new SqlCommand(sqlUpd, conn, tran);
                        cmdUpd.Parameters.AddWithValue("@PBId", pbMoiId);
                        if (cvMoiId > 0) cmdUpd.Parameters.AddWithValue("@CVId", cvMoiId);
                        cmdUpd.Parameters.AddWithValue("@Id", _nhanVienId);
                        cmdUpd.ExecuteNonQuery();

                        // Ghi lịch sử vào LichSuThayDoiHoSo
                        var cmdLS = new SqlCommand(@"
                            INSERT INTO LichSuThayDoiHoSo (NhanVienId, TruongThayDoi, GiaTriCu, GiaTriMoi, NguoiThayDoi)
                            VALUES (@NVId, @Truong, @Cu, @Moi, 'HR User')", conn, tran);
                        cmdLS.Parameters.AddWithValue("@NVId", _nhanVienId);
                        cmdLS.Parameters.AddWithValue("@Truong", "Điều chuyển phòng ban");
                        cmdLS.Parameters.AddWithValue("@Cu", pbCuTen);
                        cmdLS.Parameters.AddWithValue("@Moi", $"{pbMoiTen} | Ngày HL: {dtpNgayHieuLuc.Value:dd/MM/yyyy} | Lý do: {txtLyDo.Text}");
                        cmdLS.ExecuteNonQuery();

                        tran.Commit();
                        MessageBox.Show("✅ Điều chuyển thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reset
                        cmbNhanVien.SelectedIndex = -1;
                        panelNVInfo.Visible = false;
                        txtLyDo.Text = "";
                        _nhanVienId = "";
                        LoadLichSu();
                    }
                    catch { tran.Rollback(); throw; }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void LoadLichSu()
        {
            try
            {
                dgvLichSu.Rows.Clear();
                string nvId = "";
                if (cmbLocNV.SelectedIndex > 0)
                    nvId = LayNVId(cmbLocNV.SelectedItem.ToString());

                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        SELECT ls.NgayThayDoi, ls.NhanVienId, nv.HoTen,
                               ls.GiaTriCu, ls.GiaTriMoi
                        FROM LichSuThayDoiHoSo ls
                        JOIN NhanVien nv ON ls.NhanVienId = nv.NhanVienId
                        WHERE ls.TruongThayDoi = N'Điều chuyển phòng ban'
                          AND (@NVId = '' OR ls.NhanVienId = @NVId)
                        ORDER BY ls.NgayThayDoi DESC", conn);
                    cmd.Parameters.AddWithValue("@NVId", nvId);
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        // Parse GiaTriMoi: "PB mới | Ngày HL: dd/MM/yyyy | Lý do: ..."
                        string moiRaw = rdr["GiaTriMoi"].ToString();
                        string[] parts = moiRaw.Split('|');
                        string pbMoi = parts.Length > 0 ? parts[0].Trim() : moiRaw;
                        string ngayHL = parts.Length > 1 ? parts[1].Replace("Ngày HL:", "").Trim() : "--";
                        string lyDo = parts.Length > 2 ? parts[2].Replace("Lý do:", "").Trim() : "";
                        string cvMoi = parts.Length > 3 ? parts[3].Trim() : "--";

                        dgvLichSu.Rows.Add(
                            ngayHL,
                            rdr["NhanVienId"],
                            rdr["HoTen"],
                            rdr["GiaTriCu"],
                            pbMoi, cvMoi, lyDo
                        );
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private string LayNVId(string text) =>
            text.Contains("[") ? text.Substring(text.LastIndexOf('[') + 1).TrimEnd(']') : "";

        private void AddLbl(Panel p, string text, int x, int y) =>
            p.Controls.Add(new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(60, 60, 60) });
    }
}