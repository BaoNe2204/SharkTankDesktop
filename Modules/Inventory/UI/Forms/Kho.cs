using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class Kho : UserControl
    {
        public Kho()
        {
            InitializeComponent();
            this.Load += Kho_Load;
        }

        private void Kho_Load(object sender, EventArgs e)
        {
            LoadKho();
        }

        // ================= LOAD =================
        void LoadKho()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = "SELECT * FROM Kho";

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

        // ================= THÊM =================
        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmKho f = new FrmKho();

            if (f.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = "INSERT INTO Kho VALUES (@MaKho,@TenKho,@DiaChi)";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@MaKho", f.MaKho);
                    cmd.Parameters.AddWithValue("@TenKho", f.TenKho);
                    cmd.Parameters.AddWithValue("@DiaChi", f.DiaChi);

                    cmd.ExecuteNonQuery();
                }

                LoadKho();
            }
        }

        // ================= SỬA =================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var row = dataGridView1.CurrentRow;

            FrmKho f = new FrmKho();

            f.SetData(
                row.Cells["MaKho"].Value.ToString(),
                row.Cells["TenKho"].Value.ToString(),
                row.Cells["DiaChi"].Value.ToString()
            );

            if (f.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"UPDATE Kho 
                                   SET TenKho=@TenKho, DiaChi=@DiaChi 
                                   WHERE MaKho=@MaKho";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@MaKho", f.MaKho);
                    cmd.Parameters.AddWithValue("@TenKho", f.TenKho);
                    cmd.Parameters.AddWithValue("@DiaChi", f.DiaChi);

                    cmd.ExecuteNonQuery();
                }

                LoadKho();
            }
        }

        // ================= XÓA =================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            string makho = dataGridView1.CurrentRow.Cells["MaKho"].Value.ToString();

            if (MessageBox.Show("Xóa kho này?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No) return;

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Kho WHERE MaKho=@MaKho", conn);

                cmd.Parameters.AddWithValue("@MaKho", makho);
                cmd.ExecuteNonQuery();
            }

            LoadKho();
        }

        // ================= TÌM =================

        void TimKiem()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT * FROM Kho
                           WHERE MaKho LIKE '%' + @key + '%'
                           OR TenKho LIKE '%' + @key + '%'";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@key", txtSearch.Text);

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
        private void txtMaKho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TimKiem();
            }
        }

        // ================= LÀM MỚI =================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadKho();
        }
    }
}