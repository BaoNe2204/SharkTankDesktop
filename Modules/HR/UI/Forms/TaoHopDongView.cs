using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class TaoHopDongView : UserControl
    {
        private ComboBox cmbNhanVien, cmbLoaiHopDong, cmbHinhThucTraLuong;
        private TextBox txtSoHopDong, txtGhiChu;
        private DateTimePicker dtpNgayBatDau, dtpNgayKetThuc;
        private NumericUpDown nudLuongCoBan, nudPhuCap;
        private Button btnTaoHopDong, btnLamMoi;
        private Label lblThongTinNV;

        public TaoHopDongView()
        {
            InitializeComponent();
            LoadNhanVien();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
            this.Padding = new Padding(20);

            var lblTitle = new Label
            {
                Text = "📄 Tạo hợp đồng lao động",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 80, 160),
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(10) };
            int y = 10;

            // Nhân viên
            AddLabel(panel, "Nhân viên *", y);
            cmbNhanVien = new ComboBox { Location = new Point(180, y), Width = 300, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbNhanVien.SelectedIndexChanged += CmbNhanVien_Changed;
            panel.Controls.Add(cmbNhanVien);
            y += 35;

            // Thông tin nhân viên
            lblThongTinNV = new Label
            {
                Location = new Point(180, y),
                Size = new Size(500, 40),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            panel.Controls.Add(lblThongTinNV);
            y += 50;

            // Số hợp đồng
            AddLabel(panel, "Số hợp đồng *", y);
            txtSoHopDong = new TextBox { Location = new Point(180, y), Width = 200, Font = new Font("Segoe UI", 10), ReadOnly = true, BackColor = Color.FromArgb(240, 240, 240) };
            txtSoHopDong.Text = $"HD-{DateTime.Now:yyyy}-{new Random().Next(100, 999):D3}";
            panel.Controls.Add(txtSoHopDong);
            y += 35;

            // Loại hợp đồng
            AddLabel(panel, "Loại hợp đồng *", y);
            cmbLoaiHopDong = new ComboBox { Location = new Point(180, y), Width = 280, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLoaiHopDong.Items.AddRange(new[] { "Xác định thời hạn", "Không xác định thời hạn", "Thử việc", "Theo mùa vụ" });
            cmbLoaiHopDong.SelectedIndex = 0;
            cmbLoaiHopDong.SelectedIndexChanged += (s, e) =>
                dtpNgayKetThuc.Enabled = cmbLoaiHopDong.Text != "Không xác định thời hạn";
            panel.Controls.Add(cmbLoaiHopDong);
            y += 35;

            // Ngày bắt đầu
            AddLabel(panel, "Ngày bắt đầu *", y);
            dtpNgayBatDau = new DateTimePicker { Location = new Point(180, y), Width = 200, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panel.Controls.Add(dtpNgayBatDau);
            y += 35;

            // Ngày kết thúc
            AddLabel(panel, "Ngày kết thúc", y);
            dtpNgayKetThuc = new DateTimePicker { Location = new Point(180, y), Width = 200, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            dtpNgayKetThuc.Value = DateTime.Now.AddYears(1);
            panel.Controls.Add(dtpNgayKetThuc);
            y += 35;

            // Lương cơ bản
            AddLabel(panel, "Lương cơ bản *", y);
            nudLuongCoBan = new NumericUpDown { Location = new Point(180, y), Width = 200, Font = new Font("Segoe UI", 10), Maximum = 999999999, ThousandsSeparator = true };
            panel.Controls.Add(nudLuongCoBan);
            y += 35;

            // Phụ cấp
            AddLabel(panel, "Phụ cấp", y);
            nudPhuCap = new NumericUpDown { Location = new Point(180, y), Width = 200, Font = new Font("Segoe UI", 10), Maximum = 999999999, ThousandsSeparator = true };
            panel.Controls.Add(nudPhuCap);
            y += 35;

            // Hình thức trả lương
            AddLabel(panel, "Hình thức trả lương", y);
            cmbHinhThucTraLuong = new ComboBox { Location = new Point(180, y), Width = 200, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbHinhThucTraLuong.Items.AddRange(new[] { "Theo tháng", "Theo tuần", "Theo ngày", "Theo giờ" });
            cmbHinhThucTraLuong.SelectedIndex = 0;
            panel.Controls.Add(cmbHinhThucTraLuong);
            y += 35;

            // Ghi chú
            AddLabel(panel, "Ghi chú", y);
            txtGhiChu = new TextBox { Location = new Point(180, y), Width = 350, Height = 60, Font = new Font("Segoe UI", 10), Multiline = true };
            panel.Controls.Add(txtGhiChu);
            y += 75;

            // Buttons
            btnTaoHopDong = new Button
            {
                Text = "💾 Tạo hợp đồng",
                Location = new Point(180, y),
                Size = new Size(150, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnTaoHopDong.FlatAppearance.BorderSize = 0;

            btnLamMoi = new Button
            {
                Text = "🔄 Làm mới",
                Location = new Point(340, y),
                Size = new Size(110, 35),
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            panel.Controls.AddRange(new Control[] { btnTaoHopDong, btnLamMoi });

            this.Controls.Add(panel);
            this.Controls.Add(lblTitle);

            btnTaoHopDong.Click += BtnTaoHopDong_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
        }

        private void LoadNhanVien()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT NhanVienId, HoTen FROM NhanVien ORDER BY HoTen", conn);
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                        cmbNhanVien.Items.Add(new ComboItem($"{rdr["HoTen"]} - {rdr["NhanVienId"]}", 0) { Text = $"{rdr["HoTen"]} - {rdr["NhanVienId"]}" });
                    cmbNhanVien.DisplayMember = "Text";
                }
            }
            catch { }
        }

        private void CmbNhanVien_Changed(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedItem == null) return;
            string text = cmbNhanVien.SelectedItem.ToString();
            string id = text.Contains(" - ") ? text.Split(new[] { " - " }, StringSplitOptions.None)[1] : "";

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        SELECT nv.HoTen, pb.TenPhongBan, cv.TenChucVu
                        FROM NhanVien nv
                        LEFT JOIN PhongBan pb ON nv.PhongBanId = pb.PhongBanId
                        LEFT JOIN ChucVu   cv ON nv.ChucVuId   = cv.ChucVuId
                        WHERE nv.NhanVienId = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                        lblThongTinNV.Text = $"Phòng ban: {rdr["TenPhongBan"]}  |  Chức vụ: {rdr["TenChucVu"]}";
                }
            }
            catch { }
        }

        private void BtnTaoHopDong_Click(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedIndex < 0) { MessageBox.Show("Vui lòng chọn nhân viên!"); return; }
            if (nudLuongCoBan.Value <= 0) { MessageBox.Show("Lương cơ bản phải lớn hơn 0!"); return; }
            if (dtpNgayKetThuc.Value <= dtpNgayBatDau.Value && cmbLoaiHopDong.Text != "Không xác định thời hạn")
            { MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!"); return; }

            string text = cmbNhanVien.SelectedItem.ToString();
            string nvId = text.Contains(" - ") ? text.Split(new[] { " - " }, StringSplitOptions.None)[1] : "";

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    // Đánh hợp đồng cũ hết hạn
                    var cmdOld = new SqlCommand("UPDATE HopDongLaoDong SET TrangThai=N'Hết hạn' WHERE NhanVienId=@NVId AND TrangThai=N'Hiệu lực'", conn);
                    cmdOld.Parameters.AddWithValue("@NVId", nvId);
                    cmdOld.ExecuteNonQuery();

                    // Tạo mới
                    var cmd = new SqlCommand(@"
                        INSERT INTO HopDongLaoDong (SoHopDong,NhanVienId,LoaiHopDong,NgayBatDau,NgayKetThuc,LuongCoBan,PhuCap,HinhThucTraLuong,TrangThai,GhiChu)
                        VALUES (@SoHD,@NVId,@Loai,@BatDau,@KetThuc,@Luong,@PhuCap,@HinhThuc,N'Hiệu lực',@GhiChu)", conn);
                    cmd.Parameters.AddWithValue("@SoHD", txtSoHopDong.Text);
                    cmd.Parameters.AddWithValue("@NVId", nvId);
                    cmd.Parameters.AddWithValue("@Loai", cmbLoaiHopDong.Text);
                    cmd.Parameters.AddWithValue("@BatDau", dtpNgayBatDau.Value.Date);
                    cmd.Parameters.AddWithValue("@KetThuc", cmbLoaiHopDong.Text == "Không xác định thời hạn" ? (object)DBNull.Value : dtpNgayKetThuc.Value.Date);
                    cmd.Parameters.AddWithValue("@Luong", nudLuongCoBan.Value);
                    cmd.Parameters.AddWithValue("@PhuCap", nudPhuCap.Value);
                    cmd.Parameters.AddWithValue("@HinhThuc", cmbHinhThucTraLuong.Text);
                    cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"✅ Tạo hợp đồng {txtSoHopDong.Text} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnLamMoi_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            cmbNhanVien.SelectedIndex = -1;
            lblThongTinNV.Text = "";
            txtSoHopDong.Text = $"HD-{DateTime.Now:yyyy}-{new Random().Next(100, 999):D3}";
            cmbLoaiHopDong.SelectedIndex = 0;
            cmbHinhThucTraLuong.SelectedIndex = 0;
            dtpNgayBatDau.Value = DateTime.Today;
            dtpNgayKetThuc.Value = DateTime.Today.AddYears(1);
            nudLuongCoBan.Value = 0;
            nudPhuCap.Value = 0;
            txtGhiChu.Text = "";
        }

        private void AddLabel(Panel panel, string text, int y)
        {
            panel.Controls.Add(new Label
            {
                Text = text,
                Location = new Point(10, y + 3),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(60, 60, 60)
            });
        }
    }
}