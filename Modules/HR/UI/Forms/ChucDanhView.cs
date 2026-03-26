using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class ChucDanhView : UserControl
    {
        private DataGridView dgvChucVu;
        private TextBox txtTenCV, txtMoTa, txtTimKiem;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy;
        private Panel panelForm;
        private Label lblFormTitle;
        private int _editingId = -1;

        public ChucDanhView()
        {
            InitializeComponent();
            LoadChucVu();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label { Text = "💼 Chức danh", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) };

            // ── TOOLBAR ──
            var panelBar = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(10) };

            txtTimKiem = new TextBox { Location = new Point(10, 12), Width = 220, Font = new Font("Segoe UI", 10), Text = "🔍 Tìm kiếm chức danh...", ForeColor = Color.Gray };
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text.StartsWith("🔍")) { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrEmpty(txtTimKiem.Text)) { txtTimKiem.Text = "🔍 Tìm kiếm chức danh..."; txtTimKiem.ForeColor = Color.Gray; } };
            txtTimKiem.TextChanged += (s, e) => LoadChucVu(txtTimKiem.Text.StartsWith("🔍") ? "" : txtTimKiem.Text);

            btnThem = MakeBtn("➕ Thêm chức danh", new Point(245, 10), Color.FromArgb(0, 120, 215), 150);
            btnThem.Click += (s, e) => ShowForm(false);
            btnSua = MakeBtn("✏️ Sửa", new Point(405, 10), Color.FromArgb(100, 150, 200), 90);
            btnSua.Click += BtnSua_Click;
            btnXoa = MakeBtn("🗑️ Xóa", new Point(505, 10), Color.FromArgb(200, 50, 50), 90);
            btnXoa.Click += BtnXoa_Click;
            panelBar.Controls.AddRange(new Control[] { txtTimKiem, btnThem, btnSua, btnXoa });

            // ── PANEL FORM ──
            panelForm = new Panel { Dock = DockStyle.Bottom, Height = 145, BackColor = Color.FromArgb(235, 245, 255), Padding = new Padding(15), Visible = false, BorderStyle = BorderStyle.FixedSingle };
            lblFormTitle = new Label { Location = new Point(10, 8), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) };
            panelForm.Controls.Add(lblFormTitle);

            panelForm.Controls.Add(new Label { Text = "Tên chức danh *:", Location = new Point(10, 38), AutoSize = true, Font = new Font("Segoe UI", 10) });
            txtTenCV = new TextBox { Location = new Point(155, 35), Width = 280, Font = new Font("Segoe UI", 10) };
            panelForm.Controls.Add(txtTenCV);

            panelForm.Controls.Add(new Label { Text = "Mô tả:", Location = new Point(10, 75), AutoSize = true, Font = new Font("Segoe UI", 10) });
            txtMoTa = new TextBox { Location = new Point(155, 72), Width = 380, Height = 40, Font = new Font("Segoe UI", 10), Multiline = true };
            panelForm.Controls.Add(txtMoTa);

            btnLuu = MakeBtn("💾 Lưu", new Point(155, 118), Color.FromArgb(16, 137, 62), 100);
            btnLuu.Click += BtnLuu_Click;
            btnHuy = MakeBtn("Hủy", new Point(265, 118), Color.FromArgb(120, 120, 120), 80);
            btnHuy.Click += (s, e) => { panelForm.Visible = false; _editingId = -1; };
            panelForm.Controls.AddRange(new Control[] { btnLuu, btnHuy });

            // ── DGV ──
            dgvChucVu = new DataGridView
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
            dgvChucVu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 160);
            dgvChucVu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChucVu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvChucVu.EnableHeadersVisualStyles = false;
            dgvChucVu.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);
            dgvChucVu.RowTemplate.Height = 32;

            dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", FillWeight = 50 });
            dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên chức danh", FillWeight = 200 });
            dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMoTa", HeaderText = "Mô tả", FillWeight = 300 });
            dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSoNV", HeaderText = "Số NV", FillWeight = 80 });

            this.Controls.Add(dgvChucVu);
            this.Controls.Add(panelForm);
            this.Controls.Add(panelBar);
            this.Controls.Add(lblTitle);
        }

        private void LoadChucVu(string search = "")
        {
            try
            {
                dgvChucVu.Rows.Clear();
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        SELECT cv.ChucVuId, cv.TenChucVu, cv.MoTa,
                               COUNT(nv.NhanVienId) AS SoNV
                        FROM ChucVu cv
                        LEFT JOIN NhanVien nv ON cv.ChucVuId = nv.ChucVuId
                        WHERE (@Search = '' OR cv.TenChucVu LIKE '%' + @Search + '%')
                        GROUP BY cv.ChucVuId, cv.TenChucVu, cv.MoTa
                        ORDER BY cv.TenChucVu", conn);
                    cmd.Parameters.AddWithValue("@Search", search);
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int row = dgvChucVu.Rows.Add(rdr["ChucVuId"], rdr["TenChucVu"], rdr["MoTa"], $"{rdr["SoNV"]} người");
                        dgvChucVu.Rows[row].Tag = rdr["ChucVuId"];
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void ShowForm(bool isEdit)
        {
            lblFormTitle.Text = isEdit ? "✏️ Sửa chức danh" : "➕ Thêm chức danh mới";
            panelForm.Visible = true;
            if (!isEdit) { txtTenCV.Text = ""; txtMoTa.Text = ""; }
            txtTenCV.Focus();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvChucVu.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn chức danh cần sửa!"); return; }
            _editingId = (int)dgvChucVu.SelectedRows[0].Tag;
            txtTenCV.Text = dgvChucVu.SelectedRows[0].Cells["colTen"].Value.ToString();
            txtMoTa.Text = dgvChucVu.SelectedRows[0].Cells["colMoTa"].Value.ToString();
            ShowForm(true);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChucVu.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn chức danh cần xóa!"); return; }
            string ten = dgvChucVu.SelectedRows[0].Cells["colTen"].Value.ToString();
            string soNV = dgvChucVu.SelectedRows[0].Cells["colSoNV"].Value.ToString();

            if (!soNV.StartsWith("0"))
            {
                MessageBox.Show($"Không thể xóa! Chức danh '{ten}' đang có {soNV}.", "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Xóa chức danh '{ten}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                int id = (int)dgvChucVu.SelectedRows[0].Tag;
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("DELETE FROM ChucVu WHERE ChucVuId=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadChucVu();
                MessageBox.Show("✅ Xóa thành công!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenCV.Text)) { MessageBox.Show("Vui lòng nhập tên chức danh!"); return; }
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd;
                    if (_editingId > 0)
                    {
                        cmd = new SqlCommand("UPDATE ChucVu SET TenChucVu=@Ten, MoTa=@MoTa WHERE ChucVuId=@Id", conn);
                        cmd.Parameters.AddWithValue("@Id", _editingId);
                    }
                    else
                    {
                        cmd = new SqlCommand("INSERT INTO ChucVu (TenChucVu, MoTa) VALUES (@Ten, @MoTa)", conn);
                    }
                    cmd.Parameters.AddWithValue("@Ten", txtTenCV.Text.Trim());
                    cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
                panelForm.Visible = false;
                _editingId = -1;
                LoadChucVu();
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