using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class CCCDHoChieuView : UserControl
    {
        private ComboBox cmbNhanVien;
        private Button btnTim, btnLuu, btnXemLichSu;
        private Panel panelForm, panelLichSu;
        private ComboBox cmbLoaiGiayTo;
        private TextBox txtSoGiayTo, txtNoiCap;
        private DateTimePicker dtpNgayCap, dtpNgayHetHan;
        private Label lblConHan, lblHoTen;
        private DataGridView dgvLichSu;
        private string _nhanVienId = "";

        public CCCDHoChieuView()
        {
            InitializeComponent();
            LoadNhanVien();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label { Text = "🪪 CCCD / Hộ chiếu", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) };

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

            AddLbl(panelForm, "Loại giấy tờ *", y);
            cmbLoaiGiayTo = new ComboBox { Location = new Point(170, y), Width = 180, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLoaiGiayTo.Items.AddRange(new[] { "CCCD", "Hộ chiếu", "CMND" });
            panelForm.Controls.Add(cmbLoaiGiayTo);
            y += 35;

            txtSoGiayTo = AddField(panelForm, "Số giấy tờ *", ref y);
            txtNoiCap = AddField(panelForm, "Nơi cấp *", ref y);

            AddLbl(panelForm, "Ngày cấp *", y);
            dtpNgayCap = new DateTimePicker { Location = new Point(170, y), Width = 180, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            panelForm.Controls.Add(dtpNgayCap);
            y += 35;

            AddLbl(panelForm, "Ngày hết hạn *", y);
            dtpNgayHetHan = new DateTimePicker { Location = new Point(170, y), Width = 180, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };
            dtpNgayHetHan.ValueChanged += (s, e) => CapNhatConHan();
            panelForm.Controls.Add(dtpNgayHetHan);
            y += 35;

            lblConHan = new Label { Location = new Point(170, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            panelForm.Controls.Add(lblConHan);
            y += 35;

            btnLuu = MakeBtn("💾 Lưu thay đổi", new Point(170, y), Color.FromArgb(16, 137, 62));
            btnLuu.Click += BtnLuu_Click;
            btnXemLichSu = MakeBtn("🕓 Lịch sử", new Point(320, y), Color.FromArgb(100, 100, 100), 120);
            btnXemLichSu.Click += (s, e) => { panelLichSu.Visible = !panelLichSu.Visible; if (panelLichSu.Visible) LoadLichSu(); };
            panelForm.Controls.AddRange(new Control[] { btnLuu, btnXemLichSu });
            y += 45;

            panelLichSu = new Panel { Location = new Point(10, y), Size = new Size(700, 180), Visible = false, BorderStyle = BorderStyle.FixedSingle };
            panelLichSu.Controls.Add(new Label { Text = "🕓 Lịch sử thay đổi giấy tờ", Dock = DockStyle.Top, Height = 25, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(5, 0, 0, 0) });
            dgvLichSu = BuildDgv();
            panelLichSu.Controls.Add(dgvLichSu);
            panelForm.Controls.Add(panelLichSu);

            this.Controls.Add(panelForm);
            this.Controls.Add(panelTim);
            this.Controls.Add(lblTitle);
        }

        private void CapNhatConHan()
        {
            int soNgay = (int)(dtpNgayHetHan.Value.Date - DateTime.Today).TotalDays;
            if (soNgay < 0) { lblConHan.Text = $"⚠️ Đã hết hạn {Math.Abs(soNgay)} ngày trước!"; lblConHan.ForeColor = Color.Red; }
            else if (soNgay < 90) { lblConHan.Text = $"⚠️ Còn {soNgay} ngày hết hạn!"; lblConHan.ForeColor = Color.OrangeRed; }
            else { lblConHan.Text = $"✅ Còn {soNgay} ngày"; lblConHan.ForeColor = Color.FromArgb(16, 137, 62); }
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
            LoadGiayTo();
            panelForm.Visible = true;
        }

        private void LoadGiayTo()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmdNV = new SqlCommand("SELECT HoTen FROM NhanVien WHERE NhanVienId=@Id", conn);
                    cmdNV.Parameters.AddWithValue("@Id", _nhanVienId);
                    lblHoTen.Text = $"👤 {cmdNV.ExecuteScalar()} - {_nhanVienId}";

                    var cmd = new SqlCommand("SELECT TOP 1 * FROM GiayToNhanVien WHERE NhanVienId=@Id ORDER BY Id DESC", conn);
                    cmd.Parameters.AddWithValue("@Id", _nhanVienId);
                    var rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        cmbLoaiGiayTo.Text = rdr["LoaiGiayTo"].ToString();
                        txtSoGiayTo.Text = rdr["SoGiayTo"].ToString();
                        txtNoiCap.Text = rdr["NoiCap"].ToString();
                        if (rdr["NgayCap"] != DBNull.Value) dtpNgayCap.Value = (DateTime)rdr["NgayCap"];
                        if (rdr["NgayHetHan"] != DBNull.Value) dtpNgayHetHan.Value = (DateTime)rdr["NgayHetHan"];
                        CapNhatConHan();
                    }
                    else
                    {
                        cmbLoaiGiayTo.SelectedIndex = 0;
                        txtSoGiayTo.Text = txtNoiCap.Text = "";
                        lblConHan.Text = "";
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoGiayTo.Text)) { MessageBox.Show("Vui lòng nhập số giấy tờ!"); return; }
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    // Xóa cũ, insert mới
                    new SqlCommand($"DELETE FROM GiayToNhanVien WHERE NhanVienId='{_nhanVienId}'", conn).ExecuteNonQuery();
                    var cmd = new SqlCommand(@"
                        INSERT INTO GiayToNhanVien (NhanVienId,LoaiGiayTo,SoGiayTo,NgayCap,NoiCap,NgayHetHan)
                        VALUES (@NVId,@Loai,@So,@NgayCap,@NoiCap,@HetHan)", conn);
                    cmd.Parameters.AddWithValue("@NVId", _nhanVienId);
                    cmd.Parameters.AddWithValue("@Loai", cmbLoaiGiayTo.Text);
                    cmd.Parameters.AddWithValue("@So", txtSoGiayTo.Text);
                    cmd.Parameters.AddWithValue("@NgayCap", dtpNgayCap.Value.Date);
                    cmd.Parameters.AddWithValue("@NoiCap", txtNoiCap.Text);
                    cmd.Parameters.AddWithValue("@HetHan", dtpNgayHetHan.Value.Date);
                    cmd.ExecuteNonQuery();

                    // Ghi lịch sử
                    var cmdLS = new SqlCommand(@"
                        INSERT INTO LichSuThayDoiHoSo (NhanVienId,TruongThayDoi,GiaTriMoi,NguoiThayDoi)
                        VALUES (@NVId,@Truong,@Moi,@Nguoi)", conn);
                    cmdLS.Parameters.AddWithValue("@NVId", _nhanVienId);
                    cmdLS.Parameters.AddWithValue("@Truong", "Giấy tờ");
                    cmdLS.Parameters.AddWithValue("@Moi", $"{cmbLoaiGiayTo.Text}: {txtSoGiayTo.Text}");
                    cmdLS.Parameters.AddWithValue("@Nguoi", "HR User");
                    cmdLS.ExecuteNonQuery();
                }
                MessageBox.Show("✅ Lưu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CapNhatConHan();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void LoadLichSu()
        {
            try
            {
                dgvLichSu.Rows.Clear();
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT TOP 30 * FROM LichSuThayDoiHoSo WHERE NhanVienId=@Id AND TruongThayDoi='Giấy tờ' ORDER BY NgayThayDoi DESC", conn);
                    cmd.Parameters.AddWithValue("@Id", _nhanVienId);
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                        dgvLichSu.Rows.Add(((DateTime)rdr["NgayThayDoi"]).ToString("dd/MM/yyyy HH:mm"), rdr["GiaTriMoi"], rdr["NguoiThayDoi"]);
                }
            }
            catch { }
        }

        private DataGridView BuildDgv()
        {
            var dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, RowHeadersVisible = false, Font = new Font("Segoe UI", 8), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White, BorderStyle = BorderStyle.None };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 70, 70);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thời gian", FillWeight = 130 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thông tin", FillWeight = 250 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Người sửa", FillWeight = 100 });
            return dgv;
        }

        private TextBox AddField(Panel p, string label, ref int y)
        {
            AddLbl(p, label, y);
            var txt = new TextBox { Location = new Point(170, y), Width = 280, Font = new Font("Segoe UI", 10) };
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