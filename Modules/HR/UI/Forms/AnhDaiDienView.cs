using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class AnhDaiDienView : UserControl
    {
        private ComboBox cmbNhanVien;
        private Button btnTim, btnChonAnh, btnLuu;
        private PictureBox picAnh;
        private Label lblHoTen, lblMaNV, lblDuongDan;
        private Panel panelForm;
        private string _nhanVienId = "";
        private string _anhMoi = "";

        public AnhDaiDienView()
        {
            InitializeComponent();
            LoadNhanVien();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label { Text = "🖼️ Ảnh đại diện", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) };

            var panelTim = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(10) };
            panelTim.Controls.Add(new Label { Text = "Chọn nhân viên:", Location = new Point(10, 17), AutoSize = true, Font = new Font("Segoe UI", 10) });
            cmbNhanVien = new ComboBox { Location = new Point(140, 14), Width = 300, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            btnTim = MakeBtn("🔍 Xem", new Point(450, 12), Color.FromArgb(0, 120, 215), 80);
            btnTim.Click += BtnTim_Click;
            panelTim.Controls.AddRange(new Control[] { cmbNhanVien, btnTim });

            panelForm = new Panel { Dock = DockStyle.Fill, Padding = new Padding(30), Visible = false };

            // Ảnh to ở giữa
            picAnh = new PictureBox
            {
                Size = new Size(200, 200),
                Location = new Point(50, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(230, 235, 240)
            };

            lblHoTen = new Label { Location = new Point(280, 20), AutoSize = true, Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) };
            lblMaNV = new Label { Location = new Point(280, 55), AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = Color.Gray };
            lblDuongDan = new Label { Location = new Point(280, 80), Size = new Size(400, 40), Font = new Font("Segoe UI", 8), ForeColor = Color.Gray, AutoSize = false };

            btnChonAnh = MakeBtn("📷 Chọn ảnh mới", new Point(280, 130), Color.FromArgb(100, 100, 200), 160);
            btnChonAnh.Click += BtnChonAnh_Click;

            btnLuu = MakeBtn("💾 Lưu ảnh", new Point(280, 170), Color.FromArgb(16, 137, 62), 130);
            btnLuu.Click += BtnLuu_Click;

            panelForm.Controls.AddRange(new Control[] { picAnh, lblHoTen, lblMaNV, lblDuongDan, btnChonAnh, btnLuu });

            this.Controls.Add(panelForm);
            this.Controls.Add(panelTim);
            this.Controls.Add(lblTitle);
        }

        private void LoadNhanVien()
        {
            try
            {
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

        private void BtnTim_Click(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedIndex < 0) { MessageBox.Show("Vui lòng chọn nhân viên!"); return; }
            string text = cmbNhanVien.SelectedItem.ToString();
            _nhanVienId = text.Contains("[") ? text.Substring(text.LastIndexOf('[') + 1).TrimEnd(']') : "";
            _anhMoi = "";
            LoadAnh();
            panelForm.Visible = true;
        }

        private void LoadAnh()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT HoTen, AnhDaiDien FROM NhanVien WHERE NhanVienId=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", _nhanVienId);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        lblHoTen.Text = rdr["HoTen"].ToString();
                        lblMaNV.Text = $"Mã NV: {_nhanVienId}";
                        string duongDan = rdr["AnhDaiDien"]?.ToString() ?? "";
                        lblDuongDan.Text = string.IsNullOrEmpty(duongDan) ? "Chưa có ảnh đại diện" : duongDan;

                        picAnh.Image = null;
                        if (!string.IsNullOrEmpty(duongDan) && File.Exists(duongDan))
                            picAnh.Image = Image.FromFile(duongDan);
                        else
                            picAnh.BackColor = Color.FromArgb(230, 235, 240);
                    }
                }
            }
            catch { }
        }

        private void BtnChonAnh_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp", Title = "Chọn ảnh đại diện" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _anhMoi = dlg.FileName;
                    picAnh.Image = Image.FromFile(_anhMoi);
                    lblDuongDan.Text = _anhMoi;
                }
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_anhMoi)) { MessageBox.Show("Vui lòng chọn ảnh mới trước!"); return; }
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("UPDATE NhanVien SET AnhDaiDien=@Anh WHERE NhanVienId=@Id", conn);
                    cmd.Parameters.AddWithValue("@Anh", _anhMoi);
                    cmd.Parameters.AddWithValue("@Id", _nhanVienId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("✅ Cập nhật ảnh thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _anhMoi = "";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private Button MakeBtn(string text, Point loc, Color color, int width = 140)
        {
            var btn = new Button { Text = text, Location = loc, Size = new Size(width, 32), Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}