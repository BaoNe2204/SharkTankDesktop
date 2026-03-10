using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class CheckInOutView : UserControl
    {
        // Giờ chuẩn
        private static readonly TimeSpan GIO_VAO_CHUAN = new TimeSpan(8, 0, 0);
        private static readonly TimeSpan GIO_RA_CHUAN = new TimeSpan(17, 0, 0);

        private ComboBox cmbNhanVien;
        private DateTimePicker dtpNgay, dtpGioVao, dtpGioRa;
        private Label lblTrangThai, lblPhutMuon, lblPhutSom, lblHoTen, lblPhongBan;
        private TextBox txtGhiChu;
        private Button btnCheckIn, btnCheckOut, btnLuu;
        private DataGridView dgvHomNay;
        private Panel panelThongTin;

        public CheckInOutView()
        {
            InitializeComponent();
            LoadNhanVien();
            LoadChamCongHomNay();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = "⏰ Check-in / Check-out",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 80, 160),
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            // ── PANEL NHẬP LIỆU ──
            var panelInput = new Panel
            {
                Dock = DockStyle.Top,
                Height = 220,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(15)
            };

            // Cột trái - chọn nhân viên & ngày
            int y = 10;
            AddLbl(panelInput, "Nhân viên *", 10, y);
            cmbNhanVien = new ComboBox { Location = new Point(140, y), Width = 280, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbNhanVien.SelectedIndexChanged += CmbNhanVien_Changed;
            panelInput.Controls.Add(cmbNhanVien);
            y += 38;

            AddLbl(panelInput, "Ngày *", 10, y);
            dtpNgay = new DateTimePicker { Location = new Point(140, y), Width = 160, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panelInput.Controls.Add(dtpNgay);
            y += 38;

            AddLbl(panelInput, "Giờ vào", 10, y);
            dtpGioVao = new DateTimePicker { Location = new Point(140, y), Width = 130, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Time, ShowUpDown = true };
            dtpGioVao.Value = DateTime.Today.Add(GIO_VAO_CHUAN);
            dtpGioVao.ValueChanged += TinhTrangThai;
            panelInput.Controls.Add(dtpGioVao);

            btnCheckIn = new Button
            {
                Text = "✅ Check-in",
                Location = new Point(285, y),
                Size = new Size(100, 28),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(16, 137, 62),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCheckIn.FlatAppearance.BorderSize = 0;
            btnCheckIn.Click += (s, e) => { dtpGioVao.Value = DateTime.Now; TinhTrangThai(null, null); };
            panelInput.Controls.Add(btnCheckIn);
            y += 38;

            AddLbl(panelInput, "Giờ ra", 10, y);
            dtpGioRa = new DateTimePicker { Location = new Point(140, y), Width = 130, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Time, ShowUpDown = true };
            dtpGioRa.Value = DateTime.Today.Add(GIO_RA_CHUAN);
            dtpGioRa.ValueChanged += TinhTrangThai;
            panelInput.Controls.Add(dtpGioRa);

            btnCheckOut = new Button
            {
                Text = "🏁 Check-out",
                Location = new Point(285, y),
                Size = new Size(100, 28),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(200, 100, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCheckOut.FlatAppearance.BorderSize = 0;
            btnCheckOut.Click += (s, e) => { dtpGioRa.Value = DateTime.Now; TinhTrangThai(null, null); };
            panelInput.Controls.Add(btnCheckOut);
            y += 38;

            AddLbl(panelInput, "Ghi chú", 10, y);
            txtGhiChu = new TextBox { Location = new Point(140, y), Width = 280, Font = new Font("Segoe UI", 10) };
            panelInput.Controls.Add(txtGhiChu);

            // Cột phải - thông tin & trạng thái
            panelThongTin = new Panel
            {
                Location = new Point(460, 10),
                Size = new Size(320, 190),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblTTTitle = new Label { Text = "📊 Kết quả chấm công", Location = new Point(10, 8), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) };
            lblHoTen = MakeLbl(new Point(10, 32), "Nhân viên: --");
            lblPhongBan = MakeLbl(new Point(10, 55), "Phòng ban: --");

            lblTrangThai = new Label
            {
                Text = "Trạng thái: --",
                Location = new Point(10, 80),
                AutoSize = false,
                Width = 300,
                Height = 28,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray
            };

            lblPhutMuon = MakeLbl(new Point(10, 112), "Đi muộn: 0 phút");
            lblPhutSom = MakeLbl(new Point(10, 135), "Về sớm: 0 phút");

            panelThongTin.Controls.AddRange(new Control[] { lblTTTitle, lblHoTen, lblPhongBan, lblTrangThai, lblPhutMuon, lblPhutSom });
            panelInput.Controls.Add(panelThongTin);

            // Nút lưu
            btnLuu = new Button
            {
                Text = "💾 Lưu chấm công",
                Location = new Point(140, 185),
                Size = new Size(150, 33),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += BtnLuu_Click;
            panelInput.Controls.Add(btnLuu);

            // ── DATAGRIDVIEW chấm công hôm nay ──
            var lblToday = new Label
            {
                Text = $"📅 Chấm công hôm nay ({DateTime.Today:dd/MM/yyyy})",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 80, 160),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                BackColor = Color.FromArgb(235, 245, 255)
            };

            dgvHomNay = BuildDgv();

            this.Controls.Add(dgvHomNay);
            this.Controls.Add(lblToday);
            this.Controls.Add(panelInput);
            this.Controls.Add(lblTitle);

            TinhTrangThai(null, null);
        }

        private DataGridView BuildDgv()
        {
            var dgv = new DataGridView
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
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 160);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);

            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaNV", HeaderText = "Mã NV", FillWeight = 70 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHoTen", HeaderText = "Họ tên", FillWeight = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGioVao", HeaderText = "Giờ vào", FillWeight = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGioRa", HeaderText = "Giờ ra", FillWeight = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTT", HeaderText = "Trạng thái", FillWeight = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMuon", HeaderText = "Muộn (phút)", FillWeight = 90 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSom", HeaderText = "Sớm (phút)", FillWeight = 90 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGhiChu", HeaderText = "Ghi chú", FillWeight = 130 });
            return dgv;
        }

        private void LoadNhanVien()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var rdr = new SqlCommand("SELECT NhanVienId, HoTen FROM NhanVien WHERE TrangThai != N'Đã nghỉ việc' ORDER BY HoTen", conn).ExecuteReader();
                    while (rdr.Read())
                        cmbNhanVien.Items.Add(new ComboItem($"{rdr["HoTen"]} [{rdr["NhanVienId"]}]", 0));
                    cmbNhanVien.DisplayMember = "Text";
                }
            }
            catch { }
        }

        private void CmbNhanVien_Changed(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedIndex < 0) return;
            string nvId = LayNhanVienId();
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        SELECT nv.HoTen, pb.TenPhongBan 
                        FROM NhanVien nv
                        LEFT JOIN PhongBan pb ON nv.PhongBanId = pb.PhongBanId
                        WHERE nv.NhanVienId = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", nvId);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        lblHoTen.Text = $"Nhân viên: {rdr["HoTen"]}";
                        lblPhongBan.Text = $"Phòng ban: {rdr["TenPhongBan"]}";
                    }
                }
            }
            catch { }
            TinhTrangThai(null, null);
        }

        private void TinhTrangThai(object sender, EventArgs e)
        {
            TimeSpan gioVao = dtpGioVao.Value.TimeOfDay;
            TimeSpan gioRa = dtpGioRa.Value.TimeOfDay;

            int phutMuon = (int)Math.Max(0, (gioVao - GIO_VAO_CHUAN).TotalMinutes);
            int phutSom = (int)Math.Max(0, (GIO_RA_CHUAN - gioRa).TotalMinutes);

            lblPhutMuon.Text = $"Đi muộn: {phutMuon} phút";
            lblPhutSom.Text = $"Về sớm: {phutSom} phút";
            lblPhutMuon.ForeColor = phutMuon > 0 ? Color.OrangeRed : Color.FromArgb(16, 137, 62);
            lblPhutSom.ForeColor = phutSom > 0 ? Color.OrangeRed : Color.FromArgb(16, 137, 62);

            string tt;
            Color ttColor;
            if (phutMuon > 0 && phutSom > 0) { tt = "⚠️ Đi muộn & Về sớm"; ttColor = Color.OrangeRed; }
            else if (phutMuon > 0) { tt = "⏰ Đi muộn"; ttColor = Color.OrangeRed; }
            else if (phutSom > 0) { tt = "🏃 Về sớm"; ttColor = Color.Orange; }
            else { tt = "✅ Đúng giờ"; ttColor = Color.FromArgb(16, 137, 62); }

            lblTrangThai.Text = $"Trạng thái: {tt}";
            lblTrangThai.ForeColor = ttColor;
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nvId = LayNhanVienId();
            TimeSpan gioVao = dtpGioVao.Value.TimeOfDay;
            TimeSpan gioRa = dtpGioRa.Value.TimeOfDay;
            int phutMuon = (int)Math.Max(0, (gioVao - GIO_VAO_CHUAN).TotalMinutes);
            int phutSom = (int)Math.Max(0, (GIO_RA_CHUAN - gioRa).TotalMinutes);

            string trangThai = phutMuon > 0 && phutSom > 0 ? "Đi muộn & Về sớm" :
                               phutMuon > 0 ? "Đi muộn" :
                               phutSom > 0 ? "Về sớm" : "Đúng giờ";

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    // Kiểm tra đã chấm chưa
                    var cmdCheck = new SqlCommand("SELECT COUNT(*) FROM ChamCong WHERE NhanVienId=@NVId AND Ngay=@Ngay", conn);
                    cmdCheck.Parameters.AddWithValue("@NVId", nvId);
                    cmdCheck.Parameters.AddWithValue("@Ngay", dtpNgay.Value.Date);
                    int exists = (int)cmdCheck.ExecuteScalar();

                    if (exists > 0)
                    {
                        // Cập nhật
                        var cmdUpd = new SqlCommand(@"
                            UPDATE ChamCong SET GioVao=@GioVao, GioRa=@GioRa, TrangThai=@TT,
                            PhutDiMuon=@Muon, PhutVeSom=@Som, GhiChu=@GhiChu
                            WHERE NhanVienId=@NVId AND Ngay=@Ngay", conn);
                        cmdUpd.Parameters.AddWithValue("@GioVao", gioVao.ToString(@"hh\:mm"));
                        cmdUpd.Parameters.AddWithValue("@GioRa", gioRa.ToString(@"hh\:mm"));
                        cmdUpd.Parameters.AddWithValue("@TT", trangThai);
                        cmdUpd.Parameters.AddWithValue("@Muon", phutMuon);
                        cmdUpd.Parameters.AddWithValue("@Som", phutSom);
                        cmdUpd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                        cmdUpd.Parameters.AddWithValue("@NVId", nvId);
                        cmdUpd.Parameters.AddWithValue("@Ngay", dtpNgay.Value.Date);
                        cmdUpd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Chèn mới
                        var cmdIns = new SqlCommand(@"
                            INSERT INTO ChamCong (NhanVienId,Ngay,GioVao,GioRa,TrangThai,PhutDiMuon,PhutVeSom,GhiChu)
                            VALUES (@NVId,@Ngay,@GioVao,@GioRa,@TT,@Muon,@Som,@GhiChu)", conn);
                        cmdIns.Parameters.AddWithValue("@NVId", nvId);
                        cmdIns.Parameters.AddWithValue("@Ngay", dtpNgay.Value.Date);
                        cmdIns.Parameters.AddWithValue("@GioVao", gioVao.ToString(@"hh\:mm"));
                        cmdIns.Parameters.AddWithValue("@GioRa", gioRa.ToString(@"hh\:mm"));
                        cmdIns.Parameters.AddWithValue("@TT", trangThai);
                        cmdIns.Parameters.AddWithValue("@Muon", phutMuon);
                        cmdIns.Parameters.AddWithValue("@Som", phutSom);
                        cmdIns.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                        cmdIns.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("✅ Lưu chấm công thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadChamCongHomNay();
                txtGhiChu.Text = "";
                cmbNhanVien.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChamCongHomNay()
        {
            try
            {
                dgvHomNay.Rows.Clear();
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        SELECT cc.*, nv.HoTen FROM ChamCong cc
                        JOIN NhanVien nv ON cc.NhanVienId = nv.NhanVienId
                        WHERE cc.Ngay = @Ngay ORDER BY cc.GioVao", conn);
                    cmd.Parameters.AddWithValue("@Ngay", DateTime.Today);
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int row = dgvHomNay.Rows.Add(
                            rdr["NhanVienId"], rdr["HoTen"],
                            rdr["GioVao"]?.ToString() ?? "",
                            rdr["GioRa"]?.ToString() ?? "",
                            rdr["TrangThai"],
                            rdr["PhutDiMuon"],
                            rdr["PhutVeSom"],
                            rdr["GhiChu"]
                        );
                        string tt = rdr["TrangThai"].ToString();
                        dgvHomNay.Rows[row].DefaultCellStyle.BackColor =
                            tt == "Đúng giờ" ? Color.FromArgb(220, 255, 220) :
                            tt.Contains("Muộn") ? Color.FromArgb(255, 235, 200) :
                            tt.Contains("Sớm") ? Color.FromArgb(255, 245, 200) :
                            Color.White;
                    }
                }
            }
            catch { }
        }

        private string LayNhanVienId()
        {
            string text = cmbNhanVien.SelectedItem?.ToString() ?? "";
            if (text.Contains("[") && text.Contains("]"))
                return text.Substring(text.LastIndexOf('[') + 1).TrimEnd(']');
            return "";
        }

        private void AddLbl(Panel p, string text, int x, int y)
        {
            p.Controls.Add(new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10) });
        }

        private Label MakeLbl(Point loc, string text)
        {
            return new Label { Text = text, Location = loc, AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(50, 50, 50) };
        }
    }
}