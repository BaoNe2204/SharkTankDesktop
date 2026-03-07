using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class GiaHanHopDongForm : Form
    {
        private string _nhanVienId;
        private string _soHopDongCu;

        private TextBox txtSoHopDongMoi;
        private ComboBox cmbLoaiHopDong, cmbHinhThucTraLuong;
        private DateTimePicker dtpNgayBatDau, dtpNgayKetThuc;
        private NumericUpDown nudLuongCoBan, nudPhuCap;
        private TextBox txtGhiChu;
        private Button btnLuu, btnHuy;

        public GiaHanHopDongForm(string nhanVienId, string soHopDongCu)
        {
            _nhanVienId = nhanVienId;
            _soHopDongCu = soHopDongCu;

            this.Text = $"🔄 Gia hạn hợp đồng - {nhanVienId}";
            this.Size = new Size(520, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            BuildUI();
            LoadHopDongCu();
        }

        private void BuildUI()
        {
            int y = 20;
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20), AutoScroll = true };

            var lblTitle = new Label
            {
                Text = $"Gia hạn hợp đồng (cũ: {_soHopDongCu})",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 80, 160),
                Location = new Point(10, y),
                AutoSize = true
            };
            panel.Controls.Add(lblTitle);
            y += 40;

            AddRow(panel, "Số HĐ mới", ref y, out txtSoHopDongMoi);
            txtSoHopDongMoi.Text = $"HD-{DateTime.Now:yyyy}-{new Random().Next(100, 999):D3}";

            AddLabel(panel, "Loại hợp đồng", y);
            cmbLoaiHopDong = new ComboBox { Location = new Point(160, y), Width = 250, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLoaiHopDong.Items.AddRange(new[] { "Xác định thời hạn", "Không xác định thời hạn", "Thử việc", "Theo mùa vụ" });
            cmbLoaiHopDong.SelectedIndex = 0;
            panel.Controls.Add(cmbLoaiHopDong);
            y += 35;

            AddLabel(panel, "Ngày bắt đầu", y);
            dtpNgayBatDau = new DateTimePicker { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panel.Controls.Add(dtpNgayBatDau);
            y += 35;

            AddLabel(panel, "Ngày kết thúc", y);
            dtpNgayKetThuc = new DateTimePicker { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            dtpNgayKetThuc.Value = DateTime.Now.AddYears(1);
            panel.Controls.Add(dtpNgayKetThuc);
            y += 35;

            AddLabel(panel, "Lương cơ bản", y);
            nudLuongCoBan = new NumericUpDown { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Maximum = 999999999, ThousandsSeparator = true };
            panel.Controls.Add(nudLuongCoBan);
            y += 35;

            AddLabel(panel, "Phụ cấp", y);
            nudPhuCap = new NumericUpDown { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), Maximum = 999999999, ThousandsSeparator = true };
            panel.Controls.Add(nudPhuCap);
            y += 35;

            AddLabel(panel, "Hình thức trả lương", y);
            cmbHinhThucTraLuong = new ComboBox { Location = new Point(160, y), Width = 200, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbHinhThucTraLuong.Items.AddRange(new[] { "Theo tháng", "Theo tuần", "Theo ngày", "Theo giờ" });
            cmbHinhThucTraLuong.SelectedIndex = 0;
            panel.Controls.Add(cmbHinhThucTraLuong);
            y += 35;

            AddLabel(panel, "Ghi chú", y);
            txtGhiChu = new TextBox { Location = new Point(160, y), Width = 280, Height = 50, Font = new Font("Segoe UI", 10), Multiline = true };
            panel.Controls.Add(txtGhiChu);
            y += 60;

            // Buttons
            btnLuu = new Button
            {
                Text = "💾 Gia hạn",
                Location = new Point(160, y),
                Size = new Size(110, 33),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLuu.FlatAppearance.BorderSize = 0;

            btnHuy = new Button
            {
                Text = "Hủy",
                Location = new Point(280, y),
                Size = new Size(90, 33),
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            panel.Controls.AddRange(new Control[] { btnLuu, btnHuy });
            this.Controls.Add(panel);

            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += (s, e) => this.Close();
        }

        private void LoadHopDongCu()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT * FROM HopDongLaoDong WHERE SoHopDong = @SoHD", conn);
                    cmd.Parameters.AddWithValue("@SoHD", _soHopDongCu);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        cmbLoaiHopDong.Text = rdr["LoaiHopDong"].ToString();
                        cmbHinhThucTraLuong.Text = rdr["HinhThucTraLuong"].ToString();
                        nudLuongCoBan.Value = rdr["LuongCoBan"] != DBNull.Value ? (decimal)rdr["LuongCoBan"] : 0;
                        nudPhuCap.Value = rdr["PhuCap"] != DBNull.Value ? (decimal)rdr["PhuCap"] : 0;
                        dtpNgayBatDau.Value = rdr["NgayKetThuc"] != DBNull.Value
                                                        ? ((DateTime)rdr["NgayKetThuc"]).AddDays(1)
                                                        : DateTime.Today;
                    }
                }
            }
            catch { }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var tran = conn.BeginTransaction();
                    try
                    {
                        // Đánh dấu hợp đồng cũ hết hạn
                        var cmdOld = new SqlCommand("UPDATE HopDongLaoDong SET TrangThai=N'Hết hạn' WHERE SoHopDong=@SoHD", conn, tran);
                        cmdOld.Parameters.AddWithValue("@SoHD", _soHopDongCu);
                        cmdOld.ExecuteNonQuery();

                        // Tạo hợp đồng mới
                        var cmdNew = new SqlCommand(@"
                            INSERT INTO HopDongLaoDong (SoHopDong,NhanVienId,LoaiHopDong,NgayBatDau,NgayKetThuc,LuongCoBan,PhuCap,HinhThucTraLuong,TrangThai,GhiChu)
                            VALUES (@SoHD,@NVId,@Loai,@BatDau,@KetThuc,@Luong,@PhuCap,@HinhThuc,N'Hiệu lực',@GhiChu)", conn, tran);
                        cmdNew.Parameters.AddWithValue("@SoHD", txtSoHopDongMoi.Text);
                        cmdNew.Parameters.AddWithValue("@NVId", _nhanVienId);
                        cmdNew.Parameters.AddWithValue("@Loai", cmbLoaiHopDong.Text);
                        cmdNew.Parameters.AddWithValue("@BatDau", dtpNgayBatDau.Value.Date);
                        cmdNew.Parameters.AddWithValue("@KetThuc", dtpNgayKetThuc.Value.Date);
                        cmdNew.Parameters.AddWithValue("@Luong", nudLuongCoBan.Value);
                        cmdNew.Parameters.AddWithValue("@PhuCap", nudPhuCap.Value);
                        cmdNew.Parameters.AddWithValue("@HinhThuc", cmbHinhThucTraLuong.Text);
                        cmdNew.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                        cmdNew.ExecuteNonQuery();

                        tran.Commit();
                        MessageBox.Show("✅ Gia hạn hợp đồng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRow(Panel panel, string label, ref int y, out TextBox txt)
        {
            AddLabel(panel, label, y);
            txt = new TextBox { Location = new Point(160, y), Width = 250, Font = new Font("Segoe UI", 10) };
            panel.Controls.Add(txt);
            y += 35;
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