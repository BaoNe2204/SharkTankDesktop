using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class PhongBanAddForm : Form
    {
        public PhongBanAddForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            btnThem.Click += btnThem_Click;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO PhongBan(TenPhongBan, MoTa) VALUES(@Ten,@MoTa)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Ten", txtTenPhongBan.Text);
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);

                cmd.ExecuteNonQuery();

                // Ghi DataChangeLogs + AuditLogs
                AuditHelper.Insert("PhongBan", txtTenPhongBan.Text.Trim(), txtTenPhongBan.Text.Trim(),
                    new PhongBanSnapshot
                    {
                        TenPhongBan = txtTenPhongBan.Text.Trim(),
                        MoTa = txtMoTa.Text.Trim()
                    });
            }

            MessageBox.Show("Thêm phòng ban thành công");

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}