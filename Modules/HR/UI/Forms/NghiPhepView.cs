using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class NghiPhepView : UserControl
    {
        // Form nhập
        private ComboBox cmbNhanVien, cmbLoaiPhep;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private TextBox txtLyDo;
        private Label lblSoNgay;
        private Button btnDangKy;

        // Danh sách
        private ComboBox cmbLocNhanVien;
        private Button btnLoc;
        private DataGridView dgvNghiPhep;
        private Label lblTongPhep;

        public NghiPhepView()
        {
            InitializeComponent();
            LoadNhanVien();
            LoadNghiPhep();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = "📋 Đăng ký nghỉ phép",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 80, 160),
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            // ── PANEL ĐĂNG KÝ ──
            var panelDK = new Panel
            {
                Dock = DockStyle.Top,
                Height = 180,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(15)
            };
            panelDK.Controls.Add(new Label { Text = "📝 Đăng ký nghỉ phép mới", Location = new Point(10, 5), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) });

            int y = 30;
            AddLbl(panelDK, "Nhân viên *", 10, y);
            cmbNhanVien = new ComboBox { Location = new Point(140, y), Width = 260, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            panelDK.Controls.Add(cmbNhanVien);
            y += 36;

            AddLbl(panelDK, "Loại phép *", 10, y);
            cmbLoaiPhep = new ComboBox { Location = new Point(140, y), Width = 200, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLoaiPhep.Items.AddRange(new[] { "Nghỉ phép năm", "Nghỉ ốm", "Nghỉ không lương", "Nghỉ lễ", "Nghỉ việc riêng" });
            cmbLoaiPhep.SelectedIndex = 0;
            panelDK.Controls.Add(cmbLoaiPhep);
            y += 36;

            AddLbl(panelDK, "Từ ngày *", 10, y);
            dtpTuNgay = new DateTimePicker { Location = new Point(140, y), Width = 150, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            dtpTuNgay.ValueChanged += TinhSoNgay;
            panelDK.Controls.Add(dtpTuNgay);

            AddLbl(panelDK, "Đến ngày *", 320, y);
            dtpDenNgay = new DateTimePicker { Location = new Point(410, y), Width = 150, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            dtpDenNgay.ValueChanged += TinhSoNgay;
            panelDK.Controls.Add(dtpDenNgay);

            lblSoNgay = new Label { Location = new Point(575, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 120, 215), Text = "= 1 ngày" };
            panelDK.Controls.Add(lblSoNgay);
            y += 36;

            AddLbl(panelDK, "Lý do", 10, y);
            txtLyDo = new TextBox { Location = new Point(140, y), Width = 400, Font = new Font("Segoe UI", 10) };
            panelDK.Controls.Add(txtLyDo);

            btnDangKy = new Button
            {
                Text = "💾 Đăng ký nghỉ phép",
                Location = new Point(560, y - 2),
                Size = new Size(160, 30),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.Click += BtnDangKy_Click;
            panelDK.Controls.Add(btnDangKy);

            // ── PANEL LỌC ──
            var panelLoc = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.FromArgb(235, 245, 255), Padding = new Padding(10) };
            panelLoc.Controls.Add(new Label { Text = "📋 Danh sách nghỉ phép", Location = new Point(10, 12), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) });

            AddLbl(panelLoc, "Nhân viên:", 230, 12);
            cmbLocNhanVien = new ComboBox { Location = new Point(310, 10), Width = 220, Font = new Font("Segoe UI", 9), DropDownStyle = ComboBoxStyle.DropDownList };
            panelLoc.Controls.Add(cmbLocNhanVien);

            btnLoc = new Button
            {
                Text = "🔍 Lọc",
                Location = new Point(540, 8),
                Size = new Size(80, 26),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLoc.FlatAppearance.BorderSize = 0;
            btnLoc.Click += (s, e) => LoadNghiPhep();
            panelLoc.Controls.Add(btnLoc);

            // ── PANEL BOTTOM ──
            var panelBot = new Panel { Dock = DockStyle.Bottom, Height = 30, BackColor = Color.FromArgb(245, 247, 250) };
            lblTongPhep = new Label { Location = new Point(10, 7), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, Text = "Tổng: 0 đơn" };
            panelBot.Controls.Add(lblTongPhep);

            // ── DGV ──
            dgvNghiPhep = new DataGridView
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
            dgvNghiPhep.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 160);
            dgvNghiPhep.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNghiPhep.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvNghiPhep.EnableHeadersVisualStyles = false;
            dgvNghiPhep.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);

            dgvNghiPhep.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaNV", HeaderText = "Mã NV", FillWeight = 70 });
            dgvNghiPhep.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHoTen", HeaderText = "Họ tên", FillWeight = 150 });
            dgvNghiPhep.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLoai", HeaderText = "Loại phép", FillWeight = 110 });
            dgvNghiPhep.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTuNgay", HeaderText = "Từ ngày", FillWeight = 90 });
            dgvNghiPhep.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDenNgay", HeaderText = "Đến ngày", FillWeight = 90 });
            dgvNghiPhep.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSoNgay", HeaderText = "Số ngày", FillWeight = 70 });
            dgvNghiPhep.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLyDo", HeaderText = "Lý do", FillWeight = 150 });
            dgvNghiPhep.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTT", HeaderText = "Trạng thái", FillWeight = 90 });

            // Nút xóa
            var colXoa = new DataGridViewButtonColumn { Name = "colXoa", HeaderText = "", Text = "🗑 Xóa", UseColumnTextForButtonValue = true, FillWeight = 60 };
            dgvNghiPhep.Columns.Add(colXoa);
            dgvNghiPhep.CellClick += DgvNghiPhep_CellClick;

            this.Controls.Add(dgvNghiPhep);
            this.Controls.Add(panelBot);
            this.Controls.Add(panelLoc);
            this.Controls.Add(panelDK);
            this.Controls.Add(lblTitle);
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
                        var item = new ComboItem($"{rdr["HoTen"]} [{rdr["NhanVienId"]}]", 0);
                        cmbNhanVien.Items.Add(item);
                        cmbLocNhanVien.Items.Add(new ComboItem($"{rdr["HoTen"]} [{rdr["NhanVienId"]}]", 0));
                    }
                    cmbNhanVien.DisplayMember = "Text";
                    cmbLocNhanVien.DisplayMember = "Text";
                }
            }
            catch { }
        }

        private void TinhSoNgay(object sender, EventArgs e)
        {
            int soNgay = (int)(dtpDenNgay.Value.Date - dtpTuNgay.Value.Date).TotalDays + 1;
            lblSoNgay.Text = soNgay > 0 ? $"= {soNgay} ngày" : "⚠️ Ngày không hợp lệ";
            lblSoNgay.ForeColor = soNgay > 0 ? Color.FromArgb(0, 120, 215) : Color.Red;
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedIndex < 0) { MessageBox.Show("Vui lòng chọn nhân viên!"); return; }
            if (dtpDenNgay.Value.Date < dtpTuNgay.Value.Date) { MessageBox.Show("Ngày kết thúc phải >= ngày bắt đầu!"); return; }

            string nvId = LayNVId(cmbNhanVien.SelectedItem.ToString());
            int soNgay = (int)(dtpDenNgay.Value.Date - dtpTuNgay.Value.Date).TotalDays + 1;

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        INSERT INTO NghiPhep (NhanVienId, TuNgay, DenNgay, LoaiPhep, SoNgay, LyDo, TrangThai)
                        VALUES (@NVId, @TuNgay, @DenNgay, @Loai, @SoNgay, @LyDo, N'Đã duyệt')", conn);
                    cmd.Parameters.AddWithValue("@NVId", nvId);
                    cmd.Parameters.AddWithValue("@TuNgay", dtpTuNgay.Value.Date);
                    cmd.Parameters.AddWithValue("@DenNgay", dtpDenNgay.Value.Date);
                    cmd.Parameters.AddWithValue("@Loai", cmbLoaiPhep.Text);
                    cmd.Parameters.AddWithValue("@SoNgay", soNgay);
                    cmd.Parameters.AddWithValue("@LyDo", txtLyDo.Text);
                    cmd.ExecuteNonQuery();

                    // Ghi DataChangeLogs + AuditLogs
                    AuditHelper.Insert("NghiPhep", nvId, $"{nvId} - {cmbLoaiPhep.Text}",
                        new NghiPhepSnapshot
                        {
                            NhanVienId = nvId,
                            LoaiPhep = cmbLoaiPhep.Text,
                            SoNgay = soNgay.ToString(),
                            TrangThai = "Đã duyệt"
                        });
                }
                MessageBox.Show("✅ Đăng ký nghỉ phép thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLyDo.Text = "";
                cmbNhanVien.SelectedIndex = -1;
                LoadNghiPhep();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void LoadNghiPhep()
        {
            try
            {
                dgvNghiPhep.Rows.Clear();
                string nvId = "";
                if (cmbLocNhanVien.SelectedIndex > 0)
                    nvId = LayNVId(cmbLocNhanVien.SelectedItem.ToString());

                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT np.Id, np.NhanVienId, nv.HoTen, np.LoaiPhep,
                               np.TuNgay, np.DenNgay, np.SoNgay, np.LyDo, np.TrangThai
                        FROM NghiPhep np JOIN NhanVien nv ON np.NhanVienId = nv.NhanVienId
                        WHERE (@NVId = '' OR np.NhanVienId = @NVId)
                        ORDER BY np.TuNgay DESC";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NVId", nvId);
                    var rdr = cmd.ExecuteReader();
                    int count = 0;
                    while (rdr.Read())
                    {
                        int row = dgvNghiPhep.Rows.Add(
                            rdr["NhanVienId"], rdr["HoTen"], rdr["LoaiPhep"],
                            ((DateTime)rdr["TuNgay"]).ToString("dd/MM/yyyy"),
                            ((DateTime)rdr["DenNgay"]).ToString("dd/MM/yyyy"),
                            rdr["SoNgay"], rdr["LyDo"], rdr["TrangThai"]
                        );
                        dgvNghiPhep.Rows[row].Tag = rdr["Id"];
                        count++;
                    }
                    lblTongPhep.Text = $"Tổng: {count} đơn nghỉ phép";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void DgvNghiPhep_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != dgvNghiPhep.Columns["colXoa"].Index || e.RowIndex < 0) return;
            if (MessageBox.Show("Xóa đơn nghỉ phép này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            int id = (int)dgvNghiPhep.Rows[e.RowIndex].Tag;
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("DELETE FROM NghiPhep WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();

                    // Ghi DataChangeLogs + AuditLogs
                    AuditHelper.Delete("NghiPhep", id.ToString(), id.ToString(), "Id");
                }
                LoadNghiPhep();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private string LayNVId(string text) =>
            text.Contains("[") ? text.Substring(text.LastIndexOf('[') + 1).TrimEnd(']') : "";

        private void AddLbl(Panel p, string text, int x, int y) =>
            p.Controls.Add(new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10) });
    }
}