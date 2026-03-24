using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.BLL;
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

        void LoadKho()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
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

        // Thêm kho
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"INSERT INTO Kho VALUES (@MaKho,@TenKho,@DiaChi)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKho", txtMaKho.Text);
                    cmd.Parameters.AddWithValue("@TenKho", txtTenKho.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.ExecuteNonQuery();

                    // Ghi DataChangeLogs + AuditLogs
                    AuditHelper.Insert("Kho", txtMaKho.Text, txtTenKho.Text,
                        new KhoSnapshot { MaKho = txtMaKho.Text, TenKho = txtTenKho.Text, DiaChi = txtDiaChi.Text });

                    MessageBox.Show("Thêm kho thành công");
                    LoadKho();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Sửa kho
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string maKho = txtMaKho.Text;
                var oldSnap = KhoSnapshot.FromDb(maKho);
                var newSnap = new KhoSnapshot { MaKho = maKho, TenKho = txtTenKho.Text, DiaChi = txtDiaChi.Text };

                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"UPDATE Kho SET TenKho=@TenKho, DiaChi=@DiaChi WHERE MaKho=@MaKho";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKho", maKho);
                    cmd.Parameters.AddWithValue("@TenKho", txtTenKho.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.ExecuteNonQuery();

                    // Ghi DataChangeLogs + AuditLogs (so sánh tự động)
                    AuditHelper.Update("Kho", maKho, txtTenKho.Text, oldSnap, newSnap);

                    MessageBox.Show("Sửa kho thành công");
                    LoadKho();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Xóa kho
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maKho = txtMaKho.Text;
                string tenKho = txtTenKho.Text;

                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM Kho WHERE MaKho=@MaKho";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKho", maKho);
                    cmd.ExecuteNonQuery();

                    // Ghi DataChangeLogs + AuditLogs
                    AuditHelper.Delete("Kho", maKho, tenKho, "MaKho");

                    MessageBox.Show("Xóa kho thành công");
                    LoadKho();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtMaKho.Text = dataGridView1.CurrentRow.Cells["MaKho"].Value.ToString();
                txtTenKho.Text = dataGridView1.CurrentRow.Cells["TenKho"].Value.ToString();
                txtDiaChi.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
            }
        }
    }
}
