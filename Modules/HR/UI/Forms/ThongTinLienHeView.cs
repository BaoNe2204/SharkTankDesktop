using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class ThongTinLienHeView : UserControl
    {
        private ComboBox cmbNhanVien;
        private Button btnTim, btnLuu;
        private Panel panelForm;
        private TextBox txtSoDT, txtEmail, txtDiaChi, txtNguoiLienHe, txtSDTNguoiLienHe, txtQuanHe;
        private Label lblHoTen;
        private string _nhanVienId = "";

        public ThongTinLienHeView()
        {
            InitializeComponent();
            LoadNhanVien();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label { Text = "📞 Thông tin liên hệ", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) };

            var panelTim = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(10) };
            panelTim.Controls.Add(new Label { Text = "Chọn nhân viên:", Location = new Point(10, 17), AutoSize = true, Font = new Font("Segoe UI", 10) });
            cmbNhanVien = new ComboBox { Location = new Point(140, 14), Width = 300, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            btnTim = MakeBtn("🔍 Xem hồ sơ", new Point(450, 12), Color.FromArgb(0, 120, 215));
            btnTim.Click += BtnTim_Click;
            panelTim.Controls.AddRange(new Control[] { cmbNhanVien, btnTim });

            panelForm = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20), AutoScroll = true, Visible = false };

            int y = 10;
            lblHoTen = new Label { Location = new Point(10, y), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) };
            panelForm.Controls.Add(lblHoTen);
            y += 40;

            // Liên hệ trực tiếp
            panelForm.Controls.Add(MakeSection("📱 Liên hệ trực tiếp", new Point(10, y), 680, 130));
            y += 10;
            txtSoDT = AddField(panelForm, "Số điện thoại *", ref y);
            txtEmail = AddField(panelForm, "Email *", ref y);

            AddLbl(panelForm, "Địa chỉ", y);
            txtDiaChi = new TextBox { Location = new Point(180, y), Width = 420, Height = 55, Font = new Font("Segoe UI", 10), Multiline = true };
            panelForm.Controls.Add(txtDiaChi);
            y += 65;

            // Liên hệ khẩn cấp
            panelForm.Controls.Add(MakeSection("🆘 Liên hệ khẩn cấp", new Point(10, y), 680, 120));
            y += 10;
            txtNguoiLienHe = AddField(panelForm, "Họ tên người liên hệ", ref y);
            txtSDTNguoiLienHe = AddField(panelForm, "Số điện thoại", ref y);
            txtQuanHe = AddField(panelForm, "Quan hệ (VD: Vợ/Chồng)", ref y);

            btnLuu = MakeBtn("💾 Lưu thay đổi", new Point(180, y), Color.FromArgb(16, 137, 62));
            btnLuu.Click += BtnLuu_Click;
            panelForm.Controls.Add(btnLuu);

            this.Controls.Add(panelForm);
            this.Controls.Add(panelTim);
            this.Controls.Add(lblTitle);
        }

        private Panel MakeSection(string title, Point loc, int width, int height)
        {
            var p = new Panel { Location = loc, Size = new Size(width, height), BackColor = Color.FromArgb(245, 247, 250), BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(5) };
            p.Controls.Add(new Label { Text = title, Location = new Point(8, 5), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) });
            return p;
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
            LoadLienHe();
            panelForm.Visible = true;
        }

        private void LoadLienHe()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT * FROM NhanVien WHERE NhanVienId=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", _nhanVienId);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        lblHoTen.Text = $"👤 {rdr["HoTen"]} - {_nhanVienId}";
                        txtSoDT.Text = rdr["SoDienThoai"].ToString();
                        txtEmail.Text = rdr["Email"].ToString();
                        txtDiaChi.Text = rdr["DiaChi"].ToString();
                        // Tạm dùng GhiChu để lưu thông tin liên hệ khẩn cấp (JSON đơn giản)
                        string ghiChu = rdr["GhiChu"]?.ToString() ?? "";
                        ParseKhanCap(ghiChu);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void ParseKhanCap(string ghiChu)
        {
            // Format: KC|TenNguoi|SDT|QuanHe
            if (ghiChu.StartsWith("KC|"))
            {
                string[] parts = ghiChu.Split('|');
                if (parts.Length >= 4)
                {
                    txtNguoiLienHe.Text = parts[1];
                    txtSDTNguoiLienHe.Text = parts[2];
                    txtQuanHe.Text = parts[3];
                }
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoDT.Text)) { MessageBox.Show("Số điện thoại không được để trống!"); return; }

            string khanCap = $"KC|{txtNguoiLienHe.Text}|{txtSDTNguoiLienHe.Text}|{txtQuanHe.Text}";

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        UPDATE NhanVien SET SoDienThoai=@SDT, Email=@Email, DiaChi=@DiaChi, GhiChu=@GhiChu
                        WHERE NhanVienId=@Id", conn);
                    cmd.Parameters.AddWithValue("@SDT", txtSoDT.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@GhiChu", khanCap);
                    cmd.Parameters.AddWithValue("@Id", _nhanVienId);
                    cmd.ExecuteNonQuery();

                    // Ghi lịch sử
                    var cmdLS = new SqlCommand(@"INSERT INTO LichSuThayDoiHoSo (NhanVienId,TruongThayDoi,GiaTriMoi,NguoiThayDoi) VALUES (@NVId,'Thông tin liên hệ',@Moi,'HR User')", conn);
                    cmdLS.Parameters.AddWithValue("@NVId", _nhanVienId);
                    cmdLS.Parameters.AddWithValue("@Moi", $"SDT: {txtSoDT.Text}, Email: {txtEmail.Text}");
                    cmdLS.ExecuteNonQuery();
                }
                MessageBox.Show("✅ Lưu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private TextBox AddField(Panel p, string label, ref int y)
        {
            AddLbl(p, label, y);
            var txt = new TextBox { Location = new Point(180, y), Width = 320, Font = new Font("Segoe UI", 10) };
            p.Controls.Add(txt); y += 35;
            return txt;
        }

        private void AddLbl(Panel p, string text, int y) =>
            p.Controls.Add(new Label { Text = text, Location = new Point(10, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(60, 60, 60) });

        private Button MakeBtn(string text, Point loc, Color color, int width = 140)
        {
            var btn = new Button { Text = text, Location = loc, Size = new Size(width, 32), Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}