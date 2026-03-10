using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class LamThemGioView : UserControl
    {
        private ComboBox cmbNhanVien, cmbLocNhanVien;
        private DateTimePicker dtpNgay, dtpBatDau, dtpKetThuc;
        private TextBox txtLyDo;
        private Label lblSoGio;
        private Button btnLuu, btnLoc;
        private DataGridView dgvOT;
        private Label lblTongGio;

        public LamThemGioView()
        {
            InitializeComponent();
            LoadNhanVien();
            LoadOT();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = "⏱️ Làm thêm giờ (OT)",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 80, 160),
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            // ── PANEL NHẬP ──
            var panelInput = new Panel { Dock = DockStyle.Top, Height = 165, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(15) };
            panelInput.Controls.Add(new Label { Text = "📝 Ghi nhận làm thêm giờ", Location = new Point(10, 5), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) });

            int y = 30;
            AddLbl(panelInput, "Nhân viên *", 10, y);
            cmbNhanVien = new ComboBox { Location = new Point(140, y), Width = 260, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            panelInput.Controls.Add(cmbNhanVien);
            y += 36;

            AddLbl(panelInput, "Ngày OT *", 10, y);
            dtpNgay = new DateTimePicker { Location = new Point(140, y), Width = 150, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panelInput.Controls.Add(dtpNgay);
            y += 36;

            AddLbl(panelInput, "Từ giờ *", 10, y);
            dtpBatDau = new DateTimePicker { Location = new Point(140, y), Width = 120, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Time, ShowUpDown = true };
            dtpBatDau.Value = DateTime.Today.AddHours(17);
            dtpBatDau.ValueChanged += TinhSoGio;
            panelInput.Controls.Add(dtpBatDau);

            AddLbl(panelInput, "Đến giờ *", 290, y);
            dtpKetThuc = new DateTimePicker { Location = new Point(360, y), Width = 120, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Time, ShowUpDown = true };
            dtpKetThuc.Value = DateTime.Today.AddHours(19);
            dtpKetThuc.ValueChanged += TinhSoGio;
            panelInput.Controls.Add(dtpKetThuc);

            lblSoGio = new Label { Location = new Point(495, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 120, 215), Text = "= 2.0 giờ OT" };
            panelInput.Controls.Add(lblSoGio);
            y += 36;

            AddLbl(panelInput, "Lý do", 10, y);
            txtLyDo = new TextBox { Location = new Point(140, y), Width = 350, Font = new Font("Segoe UI", 10) };
            panelInput.Controls.Add(txtLyDo);

            btnLuu = new Button
            {
                Text = "💾 Lưu OT",
                Location = new Point(510, y - 2),
                Size = new Size(100, 30),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += BtnLuu_Click;
            panelInput.Controls.Add(btnLuu);

            // ── PANEL LỌC ──
            var panelLoc = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.FromArgb(235, 245, 255), Padding = new Padding(10) };
            panelLoc.Controls.Add(new Label { Text = "📋 Danh sách OT", Location = new Point(10, 12), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) });

            AddLbl(panelLoc, "Nhân viên:", 230, 12);
            cmbLocNhanVien = new ComboBox { Location = new Point(310, 10), Width = 220, Font = new Font("Segoe UI", 9), DropDownStyle = ComboBoxStyle.DropDownList };
            panelLoc.Controls.Add(cmbLocNhanVien);

            btnLoc = new Button { Text = "🔍 Lọc", Location = new Point(540, 8), Size = new Size(80, 26), Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnLoc.FlatAppearance.BorderSize = 0;
            btnLoc.Click += (s, e) => LoadOT();
            panelLoc.Controls.Add(btnLoc);

            // ── BOTTOM ──
            var panelBot = new Panel { Dock = DockStyle.Bottom, Height = 30, BackColor = Color.FromArgb(245, 247, 250) };
            lblTongGio = new Label { Location = new Point(10, 7), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), Text = "Tổng: 0 giờ OT" };
            panelBot.Controls.Add(lblTongGio);

            // ── DGV ──
            dgvOT = new DataGridView
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
            dgvOT.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 160);
            dgvOT.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOT.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvOT.EnableHeadersVisualStyles = false;
            dgvOT.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);

            dgvOT.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaNV", HeaderText = "Mã NV", FillWeight = 70 });
            dgvOT.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHoTen", HeaderText = "Họ tên", FillWeight = 150 });
            dgvOT.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNgay", HeaderText = "Ngày OT", FillWeight = 90 });
            dgvOT.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBatDau", HeaderText = "Từ giờ", FillWeight = 80 });
            dgvOT.Columns.Add(new DataGridViewTextBoxColumn { Name = "colKetThuc", HeaderText = "Đến giờ", FillWeight = 80 });
            dgvOT.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSoGio", HeaderText = "Số giờ OT", FillWeight = 80 });
            dgvOT.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLyDo", HeaderText = "Lý do", FillWeight = 150 });
            dgvOT.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTT", HeaderText = "Trạng thái", FillWeight = 90 });

            var colXoa = new DataGridViewButtonColumn { Name = "colXoa", HeaderText = "", Text = "🗑 Xóa", UseColumnTextForButtonValue = true, FillWeight = 60 };
            dgvOT.Columns.Add(colXoa);
            dgvOT.CellClick += DgvOT_CellClick;

            this.Controls.Add(dgvOT);
            this.Controls.Add(panelBot);
            this.Controls.Add(panelLoc);
            this.Controls.Add(panelInput);
            this.Controls.Add(lblTitle);
        }

        private void TinhSoGio(object sender, EventArgs e)
        {
            double soGio = (dtpKetThuc.Value - dtpBatDau.Value).TotalHours;
            lblSoGio.Text = soGio > 0 ? $"= {soGio:F1} giờ OT" : "⚠️ Giờ không hợp lệ";
            lblSoGio.ForeColor = soGio > 0 ? Color.FromArgb(0, 120, 215) : Color.Red;
        }

        private void LoadNhanVien()
        {
            try
            {
                cmbLocNhanVien.Items.Add(new ComboItem("-- Tất cả --", 0));
                cmbLocNhanVien.SelectedIndex = 0;
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var rdr = new SqlCommand("SELECT NhanVienId, HoTen FROM NhanVien ORDER BY HoTen", conn).ExecuteReader();
                    while (rdr.Read())
                    {
                        cmbNhanVien.Items.Add(new ComboItem($"{rdr["HoTen"]} [{rdr["NhanVienId"]}]", 0));
                        cmbLocNhanVien.Items.Add(new ComboItem($"{rdr["HoTen"]} [{rdr["NhanVienId"]}]", 0));
                    }
                    cmbNhanVien.DisplayMember = "Text";
                    cmbLocNhanVien.DisplayMember = "Text";
                }
            }
            catch { }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedIndex < 0) { MessageBox.Show("Vui lòng chọn nhân viên!"); return; }
            double soGio = (dtpKetThuc.Value - dtpBatDau.Value).TotalHours;
            if (soGio <= 0) { MessageBox.Show("Giờ kết thúc phải sau giờ bắt đầu!"); return; }

            string nvId = LayNVId(cmbNhanVien.SelectedItem.ToString());
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        INSERT INTO LamThemGio (NhanVienId, Ngay, GioBatDau, GioKetThuc, SoGioOT, LyDo, TrangThai)
                        VALUES (@NVId, @Ngay, @BatDau, @KetThuc, @SoGio, @LyDo, N'Đã duyệt')", conn);
                    cmd.Parameters.AddWithValue("@NVId", nvId);
                    cmd.Parameters.AddWithValue("@Ngay", dtpNgay.Value.Date);
                    cmd.Parameters.AddWithValue("@BatDau", dtpBatDau.Value.TimeOfDay.ToString(@"hh\:mm"));
                    cmd.Parameters.AddWithValue("@KetThuc", dtpKetThuc.Value.TimeOfDay.ToString(@"hh\:mm"));
                    cmd.Parameters.AddWithValue("@SoGio", Math.Round((decimal)soGio, 1));
                    cmd.Parameters.AddWithValue("@LyDo", txtLyDo.Text);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("✅ Lưu OT thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLyDo.Text = "";
                cmbNhanVien.SelectedIndex = -1;
                LoadOT();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void LoadOT()
        {
            try
            {
                dgvOT.Rows.Clear();
                string nvId = "";
                if (cmbLocNhanVien.SelectedIndex > 0)
                    nvId = LayNVId(cmbLocNhanVien.SelectedItem.ToString());

                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        SELECT ot.Id, ot.NhanVienId, nv.HoTen, ot.Ngay,
                               ot.GioBatDau, ot.GioKetThuc, ot.SoGioOT, ot.LyDo, ot.TrangThai
                        FROM LamThemGio ot JOIN NhanVien nv ON ot.NhanVienId = nv.NhanVienId
                        WHERE (@NVId = '' OR ot.NhanVienId = @NVId)
                        ORDER BY ot.Ngay DESC", conn);
                    cmd.Parameters.AddWithValue("@NVId", nvId);
                    var rdr = cmd.ExecuteReader();
                    decimal tongGio = 0;
                    while (rdr.Read())
                    {
                        int row = dgvOT.Rows.Add(
                            rdr["NhanVienId"], rdr["HoTen"],
                            ((DateTime)rdr["Ngay"]).ToString("dd/MM/yyyy"),
                            rdr["GioBatDau"], rdr["GioKetThuc"],
                            $"{rdr["SoGioOT"]} giờ",
                            rdr["LyDo"], rdr["TrangThai"]
                        );
                        dgvOT.Rows[row].Tag = rdr["Id"];
                        tongGio += (decimal)rdr["SoGioOT"];
                    }
                    lblTongGio.Text = $"Tổng: {tongGio:F1} giờ OT";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void DgvOT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != dgvOT.Columns["colXoa"].Index || e.RowIndex < 0) return;
            if (MessageBox.Show("Xóa bản ghi OT này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            int id = (int)dgvOT.Rows[e.RowIndex].Tag;
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("DELETE FROM LamThemGio WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadOT();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private string LayNVId(string text) =>
            text.Contains("[") ? text.Substring(text.LastIndexOf('[') + 1).TrimEnd(']') : "";

        private void AddLbl(Panel p, string text, int x, int y) =>
            p.Controls.Add(new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10) });
    }
}