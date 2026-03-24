using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class QuanLyKhachHang : UserControl
    {
        private string strCon = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True;TrustServerCertificate=True";

        public QuanLyKhachHang()
        {
            InitializeComponent();
            RegisterEvents();
            LoadData();
        }

        private void RegisterEvents()
        {
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnInBaoCao.Click += (s, e) => MessageBox.Show("Chức năng in báo cáo đang được phát triển!", "Thông báo");
            dgvKhachHang.CellClick += DgvKhachHang_CellClick;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    string query = "SELECT MaKH AS [Mã], HoTen AS [Tên Khách], DienThoai AS [Điện Thoại], DiaChi AS [Địa Chỉ], Email FROM KhachHang";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvKhachHang.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // THÊM MỚI
        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhach.Text) || string.IsNullOrWhiteSpace(txtTenKhach.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Nhắc nhở");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    string sql = "INSERT INTO KhachHang (MaKH, HoTen, DienThoai, DiaChi, Email) VALUES (@ma, @name, @phone, @address, @email)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ma", txtMaKhach.Text.Trim());
                    cmd.Parameters.AddWithValue("@name", txtTenKhach.Text.Trim());
                    cmd.Parameters.AddWithValue("@phone", txtDienThoai.Text.Trim());
                    cmd.Parameters.AddWithValue("@address", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Ghi DataChangeLogs + AuditLogs
                    AuditHelper.Insert("KhachHang", txtMaKhach.Text.Trim(), txtTenKhach.Text.Trim(),
                        new KhachHangSnapshot
                        {
                            MaKH = txtMaKhach.Text.Trim(),
                            HoTen = txtTenKhach.Text.Trim(),
                            DienThoai = txtDienThoai.Text.Trim(),
                            DiaChi = txtDiaChi.Text.Trim(),
                            Email = txtEmail.Text.Trim()
                        });

                    MessageBox.Show("Thêm khách hàng mới thành công!", "Thành công");
                    LoadData();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                    MessageBox.Show("Mã khách hàng này đã tồn tại!", "Lỗi trùng mã");
                else
                    MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        // SỬA
        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhach.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng từ bảng để sửa!", "Thông báo");
                return;
            }

            try
            {
                string maKH = txtMaKhach.Text.Trim();

                // Đọc dữ liệu cũ trước khi sửa
                var oldSnap = KhachHangSnapshot.FromDb(maKH);
                var newSnap = new KhachHangSnapshot
                {
                    MaKH = maKH,
                    HoTen = txtTenKhach.Text.Trim(),
                    DienThoai = txtDienThoai.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    conn.Open();
                    string sql = "UPDATE KhachHang SET HoTen=@name, DienThoai=@phone, DiaChi=@address, Email=@email WHERE MaKH=@ma";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", txtTenKhach.Text.Trim());
                    cmd.Parameters.AddWithValue("@phone", txtDienThoai.Text.Trim());
                    cmd.Parameters.AddWithValue("@address", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@ma", maKH);
                    cmd.ExecuteNonQuery();

                    // Ghi DataChangeLogs + AuditLogs (so sánh tự động)
                    AuditHelper.Update("KhachHang", maKH, txtTenKhach.Text.Trim(), oldSnap, newSnap);

                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        // XÓA
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhach.Text)) return;

            var result = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng {txtMaKhach.Text}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string maKH = txtMaKhach.Text.Trim();
                    string tenKH = txtTenKhach.Text.Trim();

                    using (SqlConnection conn = new SqlConnection(strCon))
                    {
                        conn.Open();
                        string sql = "DELETE FROM KhachHang WHERE MaKH=@ma";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@ma", maKH);
                        cmd.ExecuteNonQuery();

                        // Ghi DataChangeLogs + AuditLogs
                        AuditHelper.Delete("KhachHang", maKH, tenKH, "MaKH");

                        MessageBox.Show("Đã xóa khách hàng!");
                        LoadData();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void DgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKhach.Text = row.Cells["Mã"].Value.ToString();
                txtTenKhach.Text = row.Cells["Tên Khách"].Value.ToString();
                txtDienThoai.Text = row.Cells["Điện Thoại"].Value.ToString();
                txtDiaChi.Text = row.Cells["Địa Chỉ"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtMaKhach.Focus();
            }
        }

        private void dgvKhachHang_MouseDown(object sender, MouseEventArgs e)
        {
            var hit = dgvKhachHang.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.None)
                ClearInputs();
        }

        private void ClearInputs()
        {
            txtMaKhach.Clear();
            txtTenKhach.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtMaKhach.ReadOnly = false;
            txtMaKhach.Focus();
        }
    }
}
