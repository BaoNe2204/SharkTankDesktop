using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class ChucVuAddForm : Form
    {
        public ChucVuAddForm()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenChucVu.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chức vụ!");
                return;
            }

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string query = "INSERT INTO ChucVu (TenChucVu, MoTa) VALUES (@Ten, @MoTa)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Ten", txtTenChucVu.Text);
                    cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Thêm chức vụ thành công!");

                txtTenChucVu.Clear();
                txtMoTa.Clear();
                txtTenChucVu.Focus();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}