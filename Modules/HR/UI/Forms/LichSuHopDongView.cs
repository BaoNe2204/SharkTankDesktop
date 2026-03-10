using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class LichSuHopDongView : UserControl
    {
        private ComboBox cmbNhanVien, cmbTrangThai;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private Button btnTimKiem, btnXuatExcel;
        private DataGridView dgvLichSu;
        private Label lblTongSo;
        private Panel panelChiTiet;
        private Label lblChiTiet;

        public LichSuHopDongView()
        {
            InitializeComponent();
            LoadNhanVien();
            LoadLichSu();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = "📋 Lịch sử hợp đồng lao động",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 80, 160),
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            // ── PANEL LỌC ──
            var panelLoc = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(10) };

            // Hàng 1
            panelLoc.Controls.Add(new Label { Text = "Nhân viên:", Location = new Point(10, 12), AutoSize = true, Font = new Font("Segoe UI", 9) });
            cmbNhanVien = new ComboBox { Location = new Point(80, 9), Width = 220, Font = new Font("Segoe UI", 9), DropDownStyle = ComboBoxStyle.DropDownList };
            panelLoc.Controls.Add(cmbNhanVien);

            panelLoc.Controls.Add(new Label { Text = "Trạng thái:", Location = new Point(315, 12), AutoSize = true, Font = new Font("Segoe UI", 9) });
            cmbTrangThai = new ComboBox { Location = new Point(390, 9), Width = 160, Font = new Font("Segoe UI", 9), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTrangThai.Items.AddRange(new[] { "-- Tất cả --", "Hiệu lực", "Hết hạn", "Đã chấm dứt" });
            cmbTrangThai.SelectedIndex = 0;
            panelLoc.Controls.Add(cmbTrangThai);

            // Hàng 2
            panelLoc.Controls.Add(new Label { Text = "Từ ngày:", Location = new Point(10, 47), AutoSize = true, Font = new Font("Segoe UI", 9) });
            dtpTuNgay = new DateTimePicker { Location = new Point(70, 44), Width = 130, Font = new Font("Segoe UI", 9), Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddYears(-2) };
            panelLoc.Controls.Add(dtpTuNgay);

            panelLoc.Controls.Add(new Label { Text = "Đến ngày:", Location = new Point(215, 47), AutoSize = true, Font = new Font("Segoe UI", 9) });
            dtpDenNgay = new DateTimePicker { Location = new Point(280, 44), Width = 130, Font = new Font("Segoe UI", 9), Format = DateTimePickerFormat.Short };
            panelLoc.Controls.Add(dtpDenNgay);

            btnTimKiem = new Button
            {
                Text = "🔍 Tìm kiếm",
                Location = new Point(425, 44),
                Size = new Size(100, 28),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnTimKiem.FlatAppearance.BorderSize = 0;
            panelLoc.Controls.Add(btnTimKiem);

            btnXuatExcel = new Button
            {
                Text = "📊 Xuất Excel",
                Location = new Point(535, 44),
                Size = new Size(110, 28),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(16, 137, 62),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnXuatExcel.FlatAppearance.BorderSize = 0;
            panelLoc.Controls.Add(btnXuatExcel);

            // ── PANEL BOTTOM ──
            var panelBottom = new Panel { Dock = DockStyle.Bottom, Height = 35, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(10, 6, 10, 6) };
            lblTongSo = new Label { Location = new Point(10, 8), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, Text = "Tổng: 0 hợp đồng" };
            panelBottom.Controls.Add(lblTongSo);

            // ── PANEL CHI TIẾT ──
            panelChiTiet = new Panel { Dock = DockStyle.Bottom, Height = 120, BackColor = Color.FromArgb(250, 250, 255), Padding = new Padding(15), Visible = false, BorderStyle = BorderStyle.FixedSingle };
            lblChiTiet = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9), AutoSize = false };
            panelChiTiet.Controls.Add(new Label { Text = "📄 Chi tiết hợp đồng:", Dock = DockStyle.Top, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), Height = 20 });
            panelChiTiet.Controls.Add(lblChiTiet);

            // ── DATAGRIDVIEW ──
            dgvLichSu = new DataGridView
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
            dgvLichSu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 160);
            dgvLichSu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLichSu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvLichSu.EnableHeadersVisualStyles = false;
            dgvLichSu.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);

            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSoHD", HeaderText = "Số hợp đồng", FillWeight = 110 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaNV", HeaderText = "Mã NV", FillWeight = 70 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHoTen", HeaderText = "Họ tên", FillWeight = 140 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLoaiHD", HeaderText = "Loại HĐ", FillWeight = 120 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBatDau", HeaderText = "Ngày bắt đầu", FillWeight = 100 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colKetThuc", HeaderText = "Ngày kết thúc", FillWeight = 100 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLuong", HeaderText = "Lương (VNĐ)", FillWeight = 110 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTrangThai", HeaderText = "Trạng thái", FillWeight = 90 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNgayTao", HeaderText = "Ngày tạo", FillWeight = 90 });

            this.Controls.Add(dgvLichSu);
            this.Controls.Add(panelChiTiet);
            this.Controls.Add(panelBottom);
            this.Controls.Add(panelLoc);
            this.Controls.Add(lblTitle);

            btnTimKiem.Click += (s, e) => LoadLichSu();
            btnXuatExcel.Click += BtnXuatExcel_Click;
            dgvLichSu.SelectionChanged += DgvLichSu_SelectionChanged;
        }

        private void LoadNhanVien()
        {
            try
            {
                cmbNhanVien.Items.Add(new ComboItem("-- Tất cả nhân viên --", 0));
                cmbNhanVien.SelectedIndex = 0;
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var rdr = new SqlCommand("SELECT NhanVienId, HoTen FROM NhanVien ORDER BY HoTen", conn).ExecuteReader();
                    while (rdr.Read())
                        cmbNhanVien.Items.Add(new ComboItem($"{rdr["HoTen"]} - {rdr["NhanVienId"]}", 0));
                    cmbNhanVien.DisplayMember = "Text";
                }
            }
            catch { }
        }

        private void LoadLichSu()
        {
            try
            {
                dgvLichSu.Rows.Clear();
                panelChiTiet.Visible = false;

                string nvId = "";
                if (cmbNhanVien.SelectedIndex > 0)
                {
                    string text = cmbNhanVien.SelectedItem.ToString();
                    nvId = text.Contains(" - ") ? text.Split(new[] { " - " }, StringSplitOptions.None)[1] : "";
                }

                string trangThai = cmbTrangThai.Text == "-- Tất cả --" ? "" : cmbTrangThai.Text;

                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT hd.SoHopDong, nv.NhanVienId, nv.HoTen,
                               hd.LoaiHopDong, hd.NgayBatDau, hd.NgayKetThuc,
                               hd.LuongCoBan, hd.TrangThai, hd.NgayTao, hd.GhiChu,
                               hd.PhuCap, hd.HinhThucTraLuong
                        FROM HopDongLaoDong hd
                        JOIN NhanVien nv ON hd.NhanVienId = nv.NhanVienId
                        WHERE (@NVId = '' OR hd.NhanVienId = @NVId)
                          AND (@TrangThai = '' OR hd.TrangThai = @TrangThai)
                          AND hd.NgayBatDau >= @TuNgay
                          AND hd.NgayBatDau <= @DenNgay
                        ORDER BY hd.NgayTao DESC";

                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NVId", nvId);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    cmd.Parameters.AddWithValue("@TuNgay", dtpTuNgay.Value.Date);
                    cmd.Parameters.AddWithValue("@DenNgay", dtpDenNgay.Value.Date.AddDays(1));

                    var rdr = cmd.ExecuteReader();
                    int count = 0;
                    while (rdr.Read())
                    {
                        var row = dgvLichSu.Rows.Add(
                            rdr["SoHopDong"].ToString(),
                            rdr["NhanVienId"].ToString(),
                            rdr["HoTen"].ToString(),
                            rdr["LoaiHopDong"].ToString(),
                            rdr["NgayBatDau"] != DBNull.Value ? ((DateTime)rdr["NgayBatDau"]).ToString("dd/MM/yyyy") : "",
                            rdr["NgayKetThuc"] != DBNull.Value ? ((DateTime)rdr["NgayKetThuc"]).ToString("dd/MM/yyyy") : "Không xác định",
                            rdr["LuongCoBan"] != DBNull.Value ? ((decimal)rdr["LuongCoBan"]).ToString("N0") : "0",
                            rdr["TrangThai"].ToString(),
                            rdr["NgayTao"] != DBNull.Value ? ((DateTime)rdr["NgayTao"]).ToString("dd/MM/yyyy") : ""
                        );

                        // Tô màu theo trạng thái
                        string tt = rdr["TrangThai"].ToString();
                        Color rowColor = tt == "Hiệu lực" ? Color.FromArgb(220, 255, 220) :
                                         tt == "Hết hạn" ? Color.FromArgb(255, 245, 200) :
                                         tt == "Đã chấm dứt" ? Color.FromArgb(255, 225, 225) :
                                         Color.White;
                        dgvLichSu.Rows[row].DefaultCellStyle.BackColor = rowColor;
                        count++;
                    }
                    lblTongSo.Text = $"Tổng: {count} hợp đồng";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvLichSu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLichSu.SelectedRows.Count == 0) return;

            var row = dgvLichSu.SelectedRows[0];
            string soHD = row.Cells["colSoHD"].Value?.ToString() ?? "";

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT * FROM HopDongLaoDong WHERE SoHopDong=@SoHD", conn);
                    cmd.Parameters.AddWithValue("@SoHD", soHD);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        lblChiTiet.Text =
                            $"Số HĐ: {rdr["SoHopDong"]}   |   " +
                            $"Loại: {rdr["LoaiHopDong"]}   |   " +
                            $"Lương: {(rdr["LuongCoBan"] != DBNull.Value ? ((decimal)rdr["LuongCoBan"]).ToString("N0") : "0")} VNĐ   |   " +
                            $"Phụ cấp: {(rdr["PhuCap"] != DBNull.Value ? ((decimal)rdr["PhuCap"]).ToString("N0") : "0")} VNĐ   |   " +
                            $"Hình thức: {rdr["HinhThucTraLuong"]}\n" +
                            $"Ghi chú: {rdr["GhiChu"]}";
                        panelChiTiet.Visible = true;
                    }
                }
            }
            catch { }
        }

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var sfd = new SaveFileDialog
                {
                    Filter = "CSV Files|*.csv",
                    FileName = $"LichSuHopDong_{DateTime.Now:yyyyMMdd}",
                    Title = "Xuất file"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                var sb = new System.Text.StringBuilder();
                // Header
                sb.AppendLine("Số hợp đồng,Mã NV,Họ tên,Loại HĐ,Ngày bắt đầu,Ngày kết thúc,Lương (VNĐ),Trạng thái,Ngày tạo");

                // Data
                foreach (DataGridViewRow row in dgvLichSu.Rows)
                {
                    sb.AppendLine(string.Join(",",
                        row.Cells["colSoHD"].Value,
                        row.Cells["colMaNV"].Value,
                        row.Cells["colHoTen"].Value,
                        row.Cells["colLoaiHD"].Value,
                        row.Cells["colBatDau"].Value,
                        row.Cells["colKetThuc"].Value,
                        row.Cells["colLuong"].Value,
                        row.Cells["colTrangThai"].Value,
                        row.Cells["colNgayTao"].Value
                    ));
                }

                System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);
                MessageBox.Show("✅ Xuất file thành công!\n" + sfd.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message);
            }
        }
    }
}