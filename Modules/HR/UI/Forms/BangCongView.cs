using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class BangCongView : UserControl
    {
        private ComboBox cmbNhanVien, cmbThang, cmbNam;
        private Button btnXem, btnXuatCSV;
        private DataGridView dgvBangCong;
        private Panel panelTongKet;
        private Label lblTongCong, lblTongMuon, lblTongSom, lblTongVang, lblTongNghiPhep;

        public BangCongView()
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
                Text = "📅 Bảng công theo tháng",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 80, 160),
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            // ── PANEL LỌC ──
            var panelLoc = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(10) };

            AddLbl(panelLoc, "Nhân viên:", 10, 15);
            cmbNhanVien = new ComboBox { Location = new Point(90, 12), Width = 240, Font = new Font("Segoe UI", 9), DropDownStyle = ComboBoxStyle.DropDownList };
            panelLoc.Controls.Add(cmbNhanVien);

            AddLbl(panelLoc, "Tháng:", 345, 15);
            cmbThang = new ComboBox { Location = new Point(395, 12), Width = 70, Font = new Font("Segoe UI", 9), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 1; i <= 12; i++) cmbThang.Items.Add(i);
            cmbThang.SelectedItem = DateTime.Today.Month;
            panelLoc.Controls.Add(cmbThang);

            AddLbl(panelLoc, "Năm:", 475, 15);
            cmbNam = new ComboBox { Location = new Point(505, 12), Width = 80, Font = new Font("Segoe UI", 9), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int y = DateTime.Today.Year; y >= DateTime.Today.Year - 5; y--) cmbNam.Items.Add(y);
            cmbNam.SelectedIndex = 0;
            panelLoc.Controls.Add(cmbNam);

            btnXem = new Button
            {
                Text = "🔍 Xem bảng công",
                Location = new Point(600, 10),
                Size = new Size(130, 28),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnXem.FlatAppearance.BorderSize = 0;
            btnXem.Click += (s, e) => LoadBangCong();
            panelLoc.Controls.Add(btnXem);

            btnXuatCSV = new Button
            {
                Text = "📊 Xuất CSV",
                Location = new Point(740, 10),
                Size = new Size(100, 28),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(16, 137, 62),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnXuatCSV.FlatAppearance.BorderSize = 0;
            btnXuatCSV.Click += BtnXuatCSV_Click;
            panelLoc.Controls.Add(btnXuatCSV);

            // ── PANEL TỔNG KẾT ──
            panelTongKet = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(235, 245, 255),
                Padding = new Padding(10),
                Visible = false
            };
            lblTongCong = MakeStatLabel(new Point(10, 12), "Công: 0");
            lblTongMuon = MakeStatLabel(new Point(130, 12), "Đi muộn: 0");
            lblTongSom = MakeStatLabel(new Point(270, 12), "Về sớm: 0");
            lblTongVang = MakeStatLabel(new Point(400, 12), "Vắng: 0");
            lblTongNghiPhep = MakeStatLabel(new Point(500, 12), "Nghỉ phép: 0");
            panelTongKet.Controls.AddRange(new Control[] { lblTongCong, lblTongMuon, lblTongSom, lblTongVang, lblTongNghiPhep });

            // ── DATAGRIDVIEW ──
            dgvBangCong = new DataGridView
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
            dgvBangCong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 160);
            dgvBangCong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBangCong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvBangCong.EnableHeadersVisualStyles = false;
            dgvBangCong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);

            dgvBangCong.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNgay", HeaderText = "Ngày", FillWeight = 90 });
            dgvBangCong.Columns.Add(new DataGridViewTextBoxColumn { Name = "colThu", HeaderText = "Thứ", FillWeight = 60 });
            dgvBangCong.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVao", HeaderText = "Giờ vào", FillWeight = 80 });
            dgvBangCong.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRa", HeaderText = "Giờ ra", FillWeight = 80 });
            dgvBangCong.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSoCong", HeaderText = "Số công", FillWeight = 70 });
            dgvBangCong.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTT", HeaderText = "Trạng thái", FillWeight = 120 });
            dgvBangCong.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMuon", HeaderText = "Muộn (ph)", FillWeight = 80 });
            dgvBangCong.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSom", HeaderText = "Sớm (ph)", FillWeight = 80 });
            dgvBangCong.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGhiChu", HeaderText = "Ghi chú", FillWeight = 130 });

            this.Controls.Add(dgvBangCong);
            this.Controls.Add(panelTongKet);
            this.Controls.Add(panelLoc);
            this.Controls.Add(lblTitle);
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
                        cmbNhanVien.Items.Add(new ComboItem($"{rdr["HoTen"]} [{rdr["NhanVienId"]}]", 0));
                    cmbNhanVien.DisplayMember = "Text";
                }
            }
            catch { }
        }

        private void LoadBangCong()
        {
            if (cmbThang.SelectedItem == null || cmbNam.SelectedItem == null) return;

            int thang = (int)cmbThang.SelectedItem;
            int nam = (int)cmbNam.SelectedItem;

            string nvId = "";
            if (cmbNhanVien.SelectedIndex > 0)
            {
                string text = cmbNhanVien.SelectedItem.ToString();
                nvId = text.Contains("[") ? text.Substring(text.LastIndexOf('[') + 1).TrimEnd(']') : "";
            }

            try
            {
                dgvBangCong.Rows.Clear();
                int tongCong = 0, tongMuon = 0, tongSom = 0, tongVang = 0, tongNghiPhep = 0;
                int soNgayTrongThang = DateTime.DaysInMonth(nam, thang);

                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    for (int ngay = 1; ngay <= soNgayTrongThang; ngay++)
                    {
                        var date = new DateTime(nam, thang, ngay);
                        bool laCuoiTuan = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
                        string tenThu = GetTenThu(date.DayOfWeek);

                        if (laCuoiTuan)
                        {
                            int row = dgvBangCong.Rows.Add(date.ToString("dd/MM"), tenThu, "", "", "", "Cuối tuần", "", "", "");
                            dgvBangCong.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                            dgvBangCong.Rows[row].DefaultCellStyle.ForeColor = Color.Gray;
                            continue;
                        }

                        string sql = string.IsNullOrEmpty(nvId)
                            ? "SELECT * FROM ChamCong WHERE Ngay=@Ngay"
                            : "SELECT * FROM ChamCong WHERE Ngay=@Ngay AND NhanVienId=@NVId";

                        var cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Ngay", date);
                        if (!string.IsNullOrEmpty(nvId)) cmd.Parameters.AddWithValue("@NVId", nvId);

                        var rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            string tt = rdr["TrangThai"].ToString();
                            double soCong = tt == "Đúng giờ" ? 1.0 : tt.Contains("Muộn") || tt.Contains("Sớm") ? 0.5 : 0;
                            int muon = (int)rdr["PhutDiMuon"], som = (int)rdr["PhutVeSom"];

                            int row = dgvBangCong.Rows.Add(
                                date.ToString("dd/MM"), tenThu,
                                rdr["GioVao"]?.ToString() ?? "",
                                rdr["GioRa"]?.ToString() ?? "",
                                soCong,
                                tt, muon, som,
                                rdr["GhiChu"]
                            );

                            dgvBangCong.Rows[row].DefaultCellStyle.BackColor =
                                tt == "Đúng giờ" ? Color.FromArgb(220, 255, 220) :
                                tt.Contains("Muộn") ? Color.FromArgb(255, 235, 200) :
                                tt.Contains("Sớm") ? Color.FromArgb(255, 245, 200) :
                                Color.White;

                            tongCong += (int)soCong;
                            if (muon > 0) tongMuon++;
                            if (som > 0) tongSom++;
                        }
                        else
                        {
                            // Kiểm tra nghỉ phép
                            rdr.Close();
                            var cmdNP = new SqlCommand("SELECT TOP 1 LoaiPhep FROM NghiPhep WHERE NhanVienId=@NVId AND @Ngay BETWEEN TuNgay AND DenNgay AND TrangThai=N'Đã duyệt'", conn);
                            cmdNP.Parameters.AddWithValue("@NVId", string.IsNullOrEmpty(nvId) ? "" : nvId);
                            cmdNP.Parameters.AddWithValue("@Ngay", date);
                            var rdrNP = cmdNP.ExecuteReader();

                            if (!string.IsNullOrEmpty(nvId) && rdrNP.Read())
                            {
                                int row = dgvBangCong.Rows.Add(date.ToString("dd/MM"), tenThu, "", "", 1, $"Nghỉ phép: {rdrNP["LoaiPhep"]}", 0, 0, "");
                                dgvBangCong.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);
                                tongNghiPhep++;
                            }
                            else
                            {
                                int row = dgvBangCong.Rows.Add(date.ToString("dd/MM"), tenThu, "--", "--", 0, "Vắng mặt", 0, 0, "");
                                dgvBangCong.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(255, 225, 225);
                                tongVang++;
                            }
                            rdrNP.Close();
                            continue;
                        }
                        rdr.Close();
                    }
                }

                lblTongCong.Text = $"✅ Công: {tongCong} ngày";
                lblTongMuon.Text = $"⏰ Đi muộn: {tongMuon} lần";
                lblTongSom.Text = $"🏃 Về sớm: {tongSom} lần";
                lblTongVang.Text = $"❌ Vắng: {tongVang} ngày";
                lblTongNghiPhep.Text = $"📋 Nghỉ phép: {tongNghiPhep} ngày";
                panelTongKet.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXuatCSV_Click(object sender, EventArgs e)
        {
            if (dgvBangCong.Rows.Count == 0) { MessageBox.Show("Không có dữ liệu để xuất!"); return; }
            try
            {
                var sfd = new SaveFileDialog { Filter = "CSV|*.csv", FileName = $"BangCong_{cmbThang.Text}_{cmbNam.Text}" };
                if (sfd.ShowDialog() != DialogResult.OK) return;
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("Ngày,Thứ,Giờ vào,Giờ ra,Số công,Trạng thái,Muộn (phút),Sớm (phút),Ghi chú");
                foreach (DataGridViewRow row in dgvBangCong.Rows)
                    sb.AppendLine(string.Join(",", row.Cells["colNgay"].Value, row.Cells["colThu"].Value, row.Cells["colVao"].Value, row.Cells["colRa"].Value, row.Cells["colSoCong"].Value, row.Cells["colTT"].Value, row.Cells["colMuon"].Value, row.Cells["colSom"].Value, row.Cells["colGhiChu"].Value));
                System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);
                MessageBox.Show("✅ Xuất thành công!\n" + sfd.FileName);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private string GetTenThu(DayOfWeek dow)
        {
            switch (dow)
            {
                case DayOfWeek.Monday: return "Thứ 2";
                case DayOfWeek.Tuesday: return "Thứ 3";
                case DayOfWeek.Wednesday: return "Thứ 4";
                case DayOfWeek.Thursday: return "Thứ 5";
                case DayOfWeek.Friday: return "Thứ 6";
                case DayOfWeek.Saturday: return "Thứ 7";
                default: return "Chủ nhật";
            }
        }

        private void AddLbl(Panel p, string text, int x, int y) =>
            p.Controls.Add(new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9) });

        private Label MakeStatLabel(Point loc, string text) =>
            new Label { Text = text, Location = loc, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) };
    }
}