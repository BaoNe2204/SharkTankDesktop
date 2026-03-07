using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class DanhSachNhanVienView : UserControl
    {
        // Controls
        private Panel panelTop;
        private TextBox txtTimKiem;
        private ComboBox cmbLocPhongBan;
        private Button btnTimKiem;
        private Button btnThemMoi;
        private DataGridView dgvNhanVien;
        private Panel panelBottom;
        private Button btnXemChiTiet;
        private Button btnChinhSua;
        private Button btnXoa;
        private Button btnGiaHan;
        private Label lblTongSo;

        public DanhSachNhanVienView()
        {
            InitializeComponent();
            LoadPhongBan();
            LoadDanhSach();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // ── PANEL TOP ──
            panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(10, 10, 10, 5)
            };

            txtTimKiem = new TextBox
            {
                Location = new Point(10, 15),
                Width = 250,
                Height = 28,
                Font = new Font("Segoe UI", 10),
                Text = "Tìm theo tên, mã NV...",
                ForeColor = Color.Gray
            };

            // Thêm sự kiện để xóa text khi click vào
            txtTimKiem.Enter += (s, e) =>
            {
                if (txtTimKiem.Text == "Tìm theo tên, mã NV...")
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };
            txtTimKiem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = "Tìm theo tên, mã NV...";
                    txtTimKiem.ForeColor = Color.Gray;
                }
            };

            cmbLocPhongBan = new ComboBox
            {
                Location = new Point(270, 15),
                Width = 180,
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbLocPhongBan.Items.Add("-- Tất cả phòng ban --");
            cmbLocPhongBan.SelectedIndex = 0;

            btnTimKiem = new Button
            {
                Location = new Point(460, 13),
                Width = 90,
                Height = 30,
                Text = "Tìm kiếm",
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnTimKiem.FlatAppearance.BorderSize = 0;

            btnThemMoi = new Button
            {
                Location = new Point(560, 13),
                Width = 120,
                Height = 30,
                Text = "➕ Thêm mới",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(16, 137, 62),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnThemMoi.FlatAppearance.BorderSize = 0;

            panelTop.Controls.AddRange(new Control[] { txtTimKiem, cmbLocPhongBan, btnTimKiem, btnThemMoi });

            // ── PANEL BOTTOM ──
            panelBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(10, 8, 10, 8)
            };

            lblTongSo = new Label
            {
                Location = new Point(10, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Text = "Tổng: 0 nhân viên"
            };

            btnXemChiTiet = new Button
            {
                Location = new Point(300, 10),
                Width = 100,
                Height = 30,
                Text = "👁 Chi tiết",
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnXemChiTiet.FlatAppearance.BorderSize = 0;

            btnChinhSua = new Button
            {
                Location = new Point(410, 10),
                Width = 100,
                Height = 30,
                Text = "✏️ Sửa",
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(255, 140, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnChinhSua.FlatAppearance.BorderSize = 0;

            btnXoa = new Button
            {
                Location = new Point(520, 10),
                Width = 100,
                Height = 30,
                Text = "🗑 Xóa",
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(200, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnXoa.FlatAppearance.BorderSize = 0;

            btnGiaHan = new Button
            {
                Location = new Point(630, 10),
                Width = 130,
                Height = 30,
                Text = "🔄 Gia hạn HĐ",
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(100, 60, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnGiaHan.FlatAppearance.BorderSize = 0;

            panelBottom.Controls.AddRange(new Control[] { lblTongSo, btnXemChiTiet, btnChinhSua, btnXoa, btnGiaHan });

            // ── DATAGRIDVIEW ──
            dgvNhanVien = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvNhanVien.EnableHeadersVisualStyles = false;
            dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);

            // Thêm cột
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaNV", HeaderText = "Mã NV", FillWeight = 80 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHoTen", HeaderText = "Họ tên", FillWeight = 160 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGioiTinh", HeaderText = "Giới tính", FillWeight = 70 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPhongBan", HeaderText = "Phòng ban", FillWeight = 130 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "colChucVu", HeaderText = "Chức vụ", FillWeight = 110 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSoDT", HeaderText = "SĐT", FillWeight = 100 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHopDong", HeaderText = "Số HĐ", FillWeight = 110 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTrangThai", HeaderText = "Trạng thái", FillWeight = 100 });

            // Add controls
            this.Controls.Add(dgvNhanVien);
            this.Controls.Add(panelBottom);
            this.Controls.Add(panelTop);

            // Events
            btnTimKiem.Click += BtnTimKiem_Click;
            btnThemMoi.Click += BtnThemMoi_Click;
            btnXemChiTiet.Click += BtnXemChiTiet_Click;
            btnChinhSua.Click += BtnChinhSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnGiaHan.Click += BtnGiaHan_Click;
            dgvNhanVien.CellDoubleClick += DgvNhanVien_CellDoubleClick;
        }

        // ── LOAD DỮ LIỆU ──

        private void LoadPhongBan()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT PhongBanId, TenPhongBan FROM PhongBan ORDER BY TenPhongBan", conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cmbLocPhongBan.Items.Add(new ComboItem(reader["TenPhongBan"].ToString(), (int)reader["PhongBanId"]));
                    }
                }
            }
            catch { /* DB chưa có dữ liệu, bỏ qua */ }
        }

        private void LoadDanhSach(string tuKhoa = "", int phongBanId = 0)
        {
            try
            {
                dgvNhanVien.Rows.Clear();
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            nv.NhanVienId,
                            nv.HoTen,
                            nv.GioiTinh,
                            pb.TenPhongBan,
                            cv.TenChucVu,
                            nv.SoDienThoai,
                            hd.SoHopDong,
                            nv.TrangThai
                        FROM NhanVien nv
                        LEFT JOIN PhongBan pb ON nv.PhongBanId = pb.PhongBanId
                        LEFT JOIN ChucVu   cv ON nv.ChucVuId   = cv.ChucVuId
                        LEFT JOIN (
                            SELECT NhanVienId, SoHopDong 
                            FROM HopDongLaoDong 
                            WHERE TrangThai = N'Hiệu lực'
                        ) hd ON nv.NhanVienId = hd.NhanVienId
                        WHERE (@TuKhoa = '' OR nv.HoTen LIKE '%' + @TuKhoa + '%' OR nv.NhanVienId LIKE '%' + @TuKhoa + '%')
                          AND (@PhongBanId = 0 OR nv.PhongBanId = @PhongBanId)
                        ORDER BY nv.HoTen";

                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TuKhoa", tuKhoa);
                    cmd.Parameters.AddWithValue("@PhongBanId", phongBanId);

                    var reader = cmd.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        dgvNhanVien.Rows.Add(
                            reader["NhanVienId"].ToString(),
                            reader["HoTen"].ToString(),
                            reader["GioiTinh"].ToString(),
                            reader["TenPhongBan"].ToString(),
                            reader["TenChucVu"].ToString(),
                            reader["SoDienThoai"].ToString(),
                            reader["SoHopDong"].ToString(),
                            reader["TrangThai"].ToString()
                        );
                        count++;
                    }
                    lblTongSo.Text = $"Tổng: {count} nhân viên";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSelectedNhanVienId()
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return dgvNhanVien.SelectedRows[0].Cells["colMaNV"].Value?.ToString();
        }

        // ── SỰ KIỆN ──

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            int phongBanId = 0;
            if (cmbLocPhongBan.SelectedItem is ComboItem item)
                phongBanId = item.Value;

            LoadDanhSach(txtTimKiem.Text.Trim(), phongBanId);
        }

        private void BtnThemMoi_Click(object sender, EventArgs e)
        {
            var form = new ThemNhanVienView();
            form.Dock = DockStyle.Fill;
            var frm = new Form
            {
                Text = "Thêm nhân viên mới",
                Size = new Size(900, 650),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };
            frm.Controls.Add(form);
            frm.ShowDialog();
            LoadDanhSach(); // Refresh sau khi thêm
        }

        private void BtnXemChiTiet_Click(object sender, EventArgs e)
        {
            string id = GetSelectedNhanVienId();
            if (id == null) return;

            var form = new ThemNhanVienView(id, readOnly: true);
            form.Dock = DockStyle.Fill;
            var frm = new Form
            {
                Text = "Chi tiết nhân viên - " + id,
                Size = new Size(900, 650),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };
            frm.Controls.Add(form);
            frm.ShowDialog();
        }

        private void BtnChinhSua_Click(object sender, EventArgs e)
        {
            string id = GetSelectedNhanVienId();
            if (id == null) return;

            var form = new ThemNhanVienView(id, readOnly: false);
            form.Dock = DockStyle.Fill;
            var frm = new Form
            {
                Text = "Chỉnh sửa nhân viên - " + id,
                Size = new Size(900, 650),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };
            frm.Controls.Add(form);
            frm.ShowDialog();
            LoadDanhSach();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            string id = GetSelectedNhanVienId();
            if (id == null) return;

            string hoTen = dgvNhanVien.SelectedRows[0].Cells["colHoTen"].Value?.ToString();
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa nhân viên [{id}] {hoTen}?\nThao tác này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    // Xóa giấy tờ và hợp đồng liên quan trước
                    new SqlCommand($"DELETE FROM GiayToNhanVien WHERE NhanVienId = '{id}'", conn).ExecuteNonQuery();
                    new SqlCommand($"DELETE FROM HopDongLaoDong WHERE NhanVienId = '{id}'", conn).ExecuteNonQuery();
                    new SqlCommand($"DELETE FROM NhanVien WHERE NhanVienId = '{id}'", conn).ExecuteNonQuery();
                }
                MessageBox.Show("Đã xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGiaHan_Click(object sender, EventArgs e)
        {
            string id = GetSelectedNhanVienId();
            if (id == null) return;

            string soHD = dgvNhanVien.SelectedRows[0].Cells["colHopDong"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(soHD))
            {
                MessageBox.Show("Nhân viên này chưa có hợp đồng hiệu lực!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var form = new GiaHanHopDongForm(id, soHD);
            form.ShowDialog();
            LoadDanhSach();
        }

        private void DgvNhanVien_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            BtnXemChiTiet_Click(sender, e);
        }
    }

    // Helper class cho ComboBox
    public class ComboItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public ComboItem(string text, int value) { Text = text; Value = value; }
        public override string ToString() => Text;
    }
}