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

            // ENTER để tìm
            txtSearch.KeyDown += txtTimKiem_KeyDown;
        }

        private void ViTriKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // ================= LOAD =================
        void LoadData(string keyword = "")
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT * FROM ViTriKho
                                   WHERE MaViTri LIKE @kw 
                                   OR TenViTri LIKE @kw 
                                   OR MaKho LIKE @kw";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
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

        // ================= ENTER SEARCH =================
        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData(txtSearch.Text);
                e.SuppressKeyPress = true;
            }
        }

        // ================= THÊM =================
        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmViTriKho f = new FrmViTriKho();

            if (f.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"INSERT INTO ViTriKho
                                   (MaViTri, TenViTri, MaKho)
                                   VALUES (@Ma, @Ten, @Kho)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@Ma", f.MaViTri);
                    cmd.Parameters.AddWithValue("@Ten", f.TenViTri);
                    cmd.Parameters.AddWithValue("@Kho", f.MaKho);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
            }
        }

        // ================= SỬA =================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var row = dataGridView1.CurrentRow;

            FrmViTriKho f = new FrmViTriKho();

            f.SetData(
                row.Cells["MaViTri"].Value.ToString(),
                row.Cells["TenViTri"].Value.ToString(),
                row.Cells["MaKho"].Value.ToString()
            );

            if (f.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"UPDATE ViTriKho
                                   SET TenViTri=@Ten,
                                       MaKho=@Kho
                                   WHERE MaViTri=@Ma";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@Ma", f.MaViTri);
                    cmd.Parameters.AddWithValue("@Ten", f.TenViTri);
                    cmd.Parameters.AddWithValue("@Kho", f.MaKho);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
            }
        }

        // ================= XÓA =================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            string ma = dataGridView1.CurrentRow.Cells["MaViTri"].Value.ToString();

            if (MessageBox.Show("Xóa vị trí này?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No) return;

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string sql = "DELETE FROM ViTriKho WHERE MaViTri=@Ma";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", ma);

                cmd.ExecuteNonQuery();
            }

            LoadData();
        }

        // ================= LÀM MỚI =================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }
    }
}