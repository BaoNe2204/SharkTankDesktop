using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class DanhSachPhongBanView : UserControl
    {
        private DataGridView dgvPhongBan;
        private TextBox txtTenPB, txtMoTa, txtTimKiem;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy;
        private Panel panelForm;
        private Label lblFormTitle;
        private int _editingId = -1;

        public DanhSachPhongBanView()
        {
            InitializeComponent();
            LoadPhongBan();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label { Text = "🏢 Danh sách phòng ban", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) };

            // ── TOOLBAR ──
            var panelBar = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(10) };

            txtTimKiem = new TextBox { Location = new Point(10, 12), Width = 220, Font = new Font("Segoe UI", 10), Text = "🔍 Tìm kiếm phòng ban..." };
            txtTimKiem.ForeColor = Color.Gray;
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text.StartsWith("🔍")) { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrEmpty(txtTimKiem.Text)) { txtTimKiem.Text = "🔍 Tìm kiếm phòng ban..."; txtTimKiem.ForeColor = Color.Gray; } };
            txtTimKiem.TextChanged += (s, e) => LoadPhongBan(txtTimKiem.Text.StartsWith("🔍") ? "" : txtTimKiem.Text);

            btnThem = MakeBtn("➕ Thêm phòng ban", new Point(245, 10), Color.FromArgb(0, 120, 215), 155);
            btnThem.Click += (s, e) => ShowForm(false);

            btnSua = MakeBtn("✏️ Sửa", new Point(410, 10), Color.FromArgb(100, 150, 200), 90);
            btnSua.Click += BtnSua_Click;

            btnXoa = MakeBtn("🗑️ Xóa", new Point(510, 10), Color.FromArgb(200, 50, 50), 90);
            btnXoa.Click += BtnXoa_Click;

            panelBar.Controls.AddRange(new Control[] { txtTimKiem, btnThem, btnSua, btnXoa });

            // ── PANEL FORM THÊM/SỬA ──
            panelForm = new Panel { Dock = DockStyle.Bottom, Height = 160, BackColor = Color.FromArgb(235, 245, 255), Padding = new Padding(15), Visible = false, BorderStyle = BorderStyle.FixedSingle };

            lblFormTitle = new Label { Location = new Point(10, 8), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) };
            panelForm.Controls.Add(lblFormTitle);

            panelForm.Controls.Add(new Label { Text = "Tên phòng ban *:", Location = new Point(10, 38), AutoSize = true, Font = new Font("Segoe UI", 10) });
            txtTenPB = new TextBox { Location = new Point(150, 35), Width = 300, Font = new Font("Segoe UI", 10) };
            panelForm.Controls.Add(txtTenPB);

            panelForm.Controls.Add(new Label { Text = "Mô tả:", Location = new Point(10, 78), AutoSize = true, Font = new Font("Segoe UI", 10) });
            txtMoTa = new TextBox { Location = new Point(150, 75), Width = 400, Height = 45, Font = new Font("Segoe UI", 10), Multiline = true };
            panelForm.Controls.Add(txtMoTa);

            btnLuu = MakeBtn("💾 Lưu", new Point(150, 125), Color.FromArgb(16, 137, 62), 100);
            btnLuu.Click += BtnLuu_Click;
            btnHuy = MakeBtn("Hủy", new Point(260, 125), Color.FromArgb(120, 120, 120), 80);
            btnHuy.Click += (s, e) => { panelForm.Visible = false; _editingId = -1; };
            panelForm.Controls.AddRange(new Control[] { btnLuu, btnHuy });

            // ── DATAGRIDVIEW ──
            dgvPhongBan = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvPhongBan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 160);
            dgvPhongBan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPhongBan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvPhongBan.EnableHeadersVisualStyles = false;
            dgvPhongBan.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);
            dgvPhongBan.RowTemplate.Height = 32;

            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", FillWeight = 50 });
            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên phòng ban", FillWeight = 200 });
            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMoTa", HeaderText = "Mô tả", FillWeight = 300 });
            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSoNV", HeaderText = "Số NV", FillWeight = 80 });

            this.Controls.Add(dgvPhongBan);
            this.Controls.Add(panelForm);
            this.Controls.Add(panelBar);
            this.Controls.Add(lblTitle);
        }

        private void LoadPhongBan(string search = "")
        {
            try
            {
                dgvPhongBan.Rows.Clear();
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        SELECT pb.PhongBanId, pb.TenPhongBan, pb.MoTa,
                               COUNT(nv.NhanVienId) AS SoNV
                        FROM PhongBan pb
                        LEFT JOIN NhanVien nv ON pb.PhongBanId = nv.PhongBanId
                        WHERE (@Search = '' OR pb.TenPhongBan LIKE '%' + @Search + '%')
                        GROUP BY pb.PhongBanId, pb.TenPhongBan, pb.MoTa
                        ORDER BY pb.TenPhongBan", conn);
                    cmd.Parameters.AddWithValue("@Search", search);
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int row = dgvPhongBan.Rows.Add(rdr["PhongBanId"], rdr["TenPhongBan"], rdr["MoTa"], $"{rdr["SoNV"]} người");
                        dgvPhongBan.Rows[row].Tag = rdr["PhongBanId"];
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void ShowForm(bool isEdit)
        {
            lblFormTitle.Text = isEdit ? "✏️ Sửa phòng ban" : "➕ Thêm phòng ban mới";
            panelForm.Visible = true;
            if (!isEdit) { txtTenPB.Text = ""; txtMoTa.Text = ""; }
            txtTenPB.Focus();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvPhongBan.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn phòng ban cần sửa!"); return; }
            _editingId = (int)dgvPhongBan.SelectedRows[0].Tag;
            txtTenPB.Text = dgvPhongBan.SelectedRows[0].Cells["colTen"].Value.ToString();
            txtMoTa.Text = dgvPhongBan.SelectedRows[0].Cells["colMoTa"].Value.ToString();
            ShowForm(true);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhongBan.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn phòng ban cần xóa!"); return; }
            string ten = dgvPhongBan.SelectedRows[0].Cells["colTen"].Value.ToString();
            string soNV = dgvPhongBan.SelectedRows[0].Cells["colSoNV"].Value.ToString();

            if (!soNV.StartsWith("0"))
            {
                MessageBox.Show($"Không thể xóa! Phòng ban '{ten}' đang có {soNV}.", "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Xóa phòng ban '{ten}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                int id = (int)dgvPhongBan.SelectedRows[0].Tag;
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("DELETE FROM PhongBan WHERE PhongBanId=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadPhongBan();
                MessageBox.Show("✅ Xóa thành công!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPB.Text)) { MessageBox.Show("Vui lòng nhập tên phòng ban!"); txtTenPB.Focus(); return; }
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd;
                    if (_editingId > 0)
                    {
                        cmd = new SqlCommand("UPDATE PhongBan SET TenPhongBan=@Ten, MoTa=@MoTa WHERE PhongBanId=@Id", conn);
                        cmd.Parameters.AddWithValue("@Id", _editingId);
                    }
                    else
                    {
                        cmd = new SqlCommand("INSERT INTO PhongBan (TenPhongBan, MoTa) VALUES (@Ten, @MoTa)", conn);
                    }
                    cmd.Parameters.AddWithValue("@Ten", txtTenPB.Text.Trim());
                    cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
                panelForm.Visible = false;
                _editingId = -1;
                LoadPhongBan();
                MessageBox.Show("✅ Lưu thành công!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private Button MakeBtn(string text, Point loc, Color color, int width = 120)
        {
            var btn = new Button { Text = text, Location = loc, Size = new Size(width, 30), Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}