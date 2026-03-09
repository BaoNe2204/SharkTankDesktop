using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class ChucVuEditForm : Form
    {
        int id;

        public ChucVuEditForm(int chucVuId)
        {
            InitializeComponent();

            id = chucVuId;

            LoadData();

            btnLuu.Click += btnLuu_Click;
        }

        void LoadData()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = "SELECT TenChucVu, MoTa FROM ChucVu WHERE ChucVuId=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtTenChucVu.Text = reader["TenChucVu"].ToString();
                    txtMoTa.Text = reader["MoTa"].ToString();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE ChucVu 
                                 SET TenChucVu=@Ten, MoTa=@MoTa 
                                 WHERE ChucVuId=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Ten", txtTenChucVu.Text);
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Sửa thành công");

            DialogResult = DialogResult.OK;
            Close();
        }
        
    }
}