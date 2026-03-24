using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using SharkTank.BLL;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class QuanLyNguoiDungForm : UserControl
    {
        string connStr = ConfigurationManager.ConnectionStrings["SharkTankDB"].ConnectionString;
        public QuanLyNguoiDungForm()
        {
            InitializeComponent();

            LoadUsers();

            btnRefresh.Click += btnRefresh_Click;
            btnDelete.Click += btnDelete_Click;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ThemTaiKhoanForm form = new ThemTaiKhoanForm();

            form.ShowDialog(); 

            LoadUsers(); 
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa");
                return;
            }

            int userId = Convert.ToInt32(
                dgvUsers.SelectedRows[0].Cells[0].Value
            );

            SuaTaiKhoanForm form = new SuaTaiKhoanForm(userId);

            form.ShowDialog();

            LoadUsers();
        }
        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT 
                                u.UserId,
                                u.Username,
                                u.FullName,
                                u.Email,
                                r.RoleName,
                                '' AS Department,
                                CASE 
                                    WHEN u.IsActive = 1 THEN 'Active'
                                    ELSE 'Inactive'
                                END AS Status,
                                u.CreatedAt
                                FROM Users u
                                JOIN Roles r ON u.RoleId = r.RoleId";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvUsers.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvUsers.Rows.Add(
                        row["UserId"],
                        row["Username"],
                        row["FullName"],
                        row["Email"],
                        row["RoleName"],
                        row["Department"],
                        row["Status"],
                        row["CreatedAt"]
                    );
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa");
                return;
            }

            int userId = Convert.ToInt32(
                dgvUsers.SelectedRows[0].Cells[0].Value
            );

            string username = dgvUsers.SelectedRows[0].Cells[1].Value.ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Users WHERE UserId=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                conn.Open();
                cmd.ExecuteNonQuery();

                // Ghi DataChangeLogs + AuditLogs
                AuditHelper.Delete("Users", userId.ToString(), username, "UserId");
            }

            MessageBox.Show("Xóa thành công");

            LoadUsers();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT 
                                u.UserId,
                                u.Username,
                                u.FullName,
                                u.Email,
                                r.RoleName,
                                '' AS Department,
                                CASE 
                                    WHEN u.IsActive = 1 THEN 'Active'
                                    ELSE 'Inactive'
                                END AS Status,
                                u.CreatedAt
                                FROM Users u
                                JOIN Roles r ON u.RoleId = r.RoleId
                                WHERE u.Username LIKE @key
                                OR u.FullName LIKE @key";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@key", "%" + txtSearch.Text + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvUsers.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvUsers.Rows.Add(
                        row["UserId"],
                        row["Username"],
                        row["FullName"],
                        row["Email"],
                        row["RoleName"],
                        row["Department"],
                        row["Status"],
                        row["CreatedAt"]
                    );
                }
            }
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}