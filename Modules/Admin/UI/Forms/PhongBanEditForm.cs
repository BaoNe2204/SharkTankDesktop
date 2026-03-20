using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class PhongBanEditForm : Form
    {
        int id;

        public PhongBanEditForm(int phongBanId)
        {
            InitializeComponent();
            id = phongBanId;
            this.StartPosition = FormStartPosition.CenterScreen;
            btnLuu.Click += btnLuu_Click;
            LoadData();
        }

        void LoadData()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = "SELECT TenPhongBan, MoTa FROM PhongBan WHERE PhongBanId=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtTenPhongBan.Text = reader["TenPhongBan"].ToString();
                    txtMoTa.Text = reader["MoTa"].ToString();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE PhongBan 
                                 SET TenPhongBan=@Ten, MoTa=@MoTa
                                 WHERE PhongBanId=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Ten", txtTenPhongBan.Text);
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