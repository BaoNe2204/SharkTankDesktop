using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class ViTriKho : UserControl
    {
        public ViTriKho()
        {
            InitializeComponent();
            this.Load += ViTriKho_Load;
        }

        private void ViTriKho_Load(object sender, EventArgs e)
        {
            LoadKho();
            LoadData();
        }

        // Load danh sách Kho vào Combobox
        void LoadKho()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = "SELECT MaKho, TenKho FROM Kho";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboKho.DataSource = dt;
                    cboKho.DisplayMember = "TenKho";
                    cboKho.ValueMember = "MaKho";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Load dữ liệu vị trí
        void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT * FROM ViTriKho";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
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

        // Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"INSERT INTO ViTriKho
                    (MaViTri, TenViTri, MaKho)
                    VALUES(@MaViTri,@TenViTri,@MaKho)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@MaViTri", txtMaViTri.Text);
                    cmd.Parameters.AddWithValue("@TenViTri", txtTenViTri.Text);
                    cmd.Parameters.AddWithValue("@MaKho", cboKho.SelectedValue);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
                MessageBox.Show("Thêm thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"UPDATE ViTriKho
                    SET TenViTri=@TenViTri,
                        MaKho=@MaKho
                    WHERE MaViTri=@MaViTri";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@MaViTri", txtMaViTri.Text);
                    cmd.Parameters.AddWithValue("@TenViTri", txtTenViTri.Text);
                    cmd.Parameters.AddWithValue("@MaKho", cboKho.SelectedValue);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
                MessageBox.Show("Sửa thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = "DELETE FROM ViTriKho WHERE MaViTri=@MaViTri";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaViTri", txtMaViTri.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
                MessageBox.Show("Xóa thành công");
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

                txtMaViTri.Text = row.Cells["MaViTri"].Value.ToString();
                txtTenViTri.Text = row.Cells["TenViTri"].Value.ToString();
                cboKho.SelectedValue = row.Cells["MaKho"].Value.ToString();
            }
        }
    }
}