using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.BLL;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class DanhMucSanPhamView : UserControl
    {
        public DanhMucSanPhamView()
        {
            InitializeComponent();
            this.Load += DanhMucSanPhamView_Load;
        }

        // ================= LOAD =================
        private void DanhMucSanPhamView_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // ================= LOAD DATA =================
        void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SanPham", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;
                    dataGridView1.ReadOnly = true;

                    if (dataGridView1.Columns.Contains("TenSP"))
                        dataGridView1.Columns["TenSP"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        // ================= TÌM KIẾM =================
        void TimKiem()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT * FROM SanPham 
                                   WHERE MaSP LIKE @key 
                                   OR NhomHang LIKE @key 
                                   OR DonViTinh LIKE @key";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@key", "%" + txtSearch.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // ================= ENTER TÌM KIẾM =================
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKiem();
                e.SuppressKeyPress = true;
            }
        }

        // ================= THÊM =================
        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmSanPham f = new FrmSanPham();

            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();

                        string sql = @"INSERT INTO SanPham 
                                       (MaSP, NhomHang, DonViTinh, GiaNhap, GiaBan)
                                       VALUES (@MaSP, @NhomHang, @DonViTinh, @GiaNhap, @GiaBan)";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MaSP", f.MaSP);
                        cmd.Parameters.AddWithValue("@NhomHang", f.NhomHang);
                        cmd.Parameters.AddWithValue("@DonViTinh", f.DonViTinh);
                        cmd.Parameters.AddWithValue("@GiaNhap", f.GiaNhap);
                        cmd.Parameters.AddWithValue("@GiaBan", f.GiaBan);

                        cmd.ExecuteNonQuery();
                    }

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm: " + ex.Message);
                }
            }
        }

        // ================= SỬA =================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chọn sản phẩm cần sửa!");
                return;
            }

            // Lấy dữ liệu từ dòng đang chọn
            DataGridViewRow row = dataGridView1.CurrentRow;

            string ma = row.Cells["MaSP"].Value.ToString();
            string nhom = row.Cells["NhomHang"].Value.ToString();
            string dvt = row.Cells["DonViTinh"].Value.ToString();
            float giaNhap = float.Parse(row.Cells["GiaNhap"].Value.ToString());
            float giaBan = float.Parse(row.Cells["GiaBan"].Value.ToString());

            // Mở form
            FrmSanPham f = new FrmSanPham();

            // GÁN DỮ LIỆU VÀO FORM
            f.SetData(ma, nhom, dvt, giaNhap, giaBan);

            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();

                        string sql = @"UPDATE SanPham 
                               SET NhomHang=@NhomHang,
                                   DonViTinh=@DonViTinh,
                                   GiaNhap=@GiaNhap,
                                   GiaBan=@GiaBan
                               WHERE MaSP=@MaSP";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@MaSP", ma);
                        cmd.Parameters.AddWithValue("@NhomHang", f.NhomHang);
                        cmd.Parameters.AddWithValue("@DonViTinh", f.DonViTinh);
                        cmd.Parameters.AddWithValue("@GiaNhap", f.GiaNhap);
                        cmd.Parameters.AddWithValue("@GiaBan", f.GiaBan);

                        cmd.ExecuteNonQuery();
                    }

                    try { AuditService.CreateDefault().LogUpdate("SanPham", ma, nhom); } catch { }

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // ================= XÓA =================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chọn sản phẩm cần xóa!");
                return;
            }

            string ma = dataGridView1.CurrentRow.Cells["MaSP"].Value.ToString();

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa?",
                "Xác nhận",
                MessageBoxButtons.YesNo
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(
                            "DELETE FROM SanPham WHERE MaSP=@MaSP", conn);

                        cmd.Parameters.AddWithValue("@MaSP", ma);
                        cmd.ExecuteNonQuery();
                    }

                    try { AuditService.CreateDefault().LogDelete("SanPham", ma); } catch { }

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message);
                }
            }
        }

        // ================= LÀM MỚI =================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }
    }
}