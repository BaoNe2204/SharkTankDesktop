using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
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

        private void DanhMucSanPhamView_Load(object sender, EventArgs e)
        {
            LoadData();
        }

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Thêm sản phẩm
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = "INSERT INTO SanPham VALUES(@MaSP,@NhomHang,@DonViTinh,@GiaNhap,@GiaBan)";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                    cmd.Parameters.AddWithValue("@NhomHang", txtNhomHang.Text);
                    cmd.Parameters.AddWithValue("@DonViTinh", txtDonViTinh.Text);
                    cmd.Parameters.AddWithValue("@GiaNhap", txtGiaNhap.Text);
                    cmd.Parameters.AddWithValue("@GiaBan", txtGiaBan.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Sửa sản phẩm
        private void btnSua_Click(object sender, EventArgs e)
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

                    cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                    cmd.Parameters.AddWithValue("@NhomHang", txtNhomHang.Text);
                    cmd.Parameters.AddWithValue("@DonViTinh", txtDonViTinh.Text);
                    cmd.Parameters.AddWithValue("@GiaNhap", txtGiaNhap.Text);
                    cmd.Parameters.AddWithValue("@GiaBan", txtGiaBan.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Xóa sản phẩm
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = "DELETE FROM SanPham WHERE MaSP=@MaSP";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Click DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtMaSP.Text = row.Cells[0].Value.ToString();
                txtNhomHang.Text = row.Cells[1].Value.ToString();
                txtDonViTinh.Text = row.Cells[2].Value.ToString();
                txtGiaNhap.Text = row.Cells[3].Value.ToString();
                txtGiaBan.Text = row.Cells[4].Value.ToString();
            }
        }
    }
}