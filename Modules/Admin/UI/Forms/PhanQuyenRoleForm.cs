using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class PhanQuyenRoleForm : UserControl
    {
        public PhanQuyenRoleForm()
        {
            InitializeComponent();
            this.Load += PhanQuyenRoleForm_Load;
            cboRole.SelectedIndexChanged += CboRole_SelectedIndexChanged;
            btnSave.Click += BtnSave_Click;
        }

        void PhanQuyenRoleForm_Load(object sender, EventArgs e)
        {
            LoadPermissions();
            LoadRoles();
            if (cboRole.Items.Count > 0)
            {
                CboRole_SelectedIndexChanged(null, null);
            }
        }

        void LoadRoles()
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT RoleId, RoleName FROM Roles", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cboRole.DataSource = dt;
                cboRole.DisplayMember = "RoleName";
                cboRole.ValueMember = "RoleId";
            }
        }

        void LoadPermissions()
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT PermissionId, PermissionCode FROM Permissions", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                clbPermissions.DataSource = dt;
                clbPermissions.DisplayMember = "PermissionCode";
                clbPermissions.ValueMember = "PermissionId";
            }
        }

        void CboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView row = cboRole.SelectedItem as DataRowView;
            if (row == null)
                return;

            int roleId = Convert.ToInt32(row["RoleId"]);

            for (int i = 0; i < clbPermissions.Items.Count; i++)
            {
                clbPermissions.SetItemChecked(i, false);
            }

            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT PermissionId FROM RolePermissions WHERE RoleId=@roleId", conn);

                cmd.Parameters.AddWithValue("@roleId", roleId);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    int permissionId = Convert.ToInt32(rd["PermissionId"]);

                    for (int i = 0; i < clbPermissions.Items.Count; i++)
                    {
                        DataRowView permissionRow = (DataRowView)clbPermissions.Items[i];

                        if ((int)permissionRow["PermissionId"] == permissionId)
                        {
                            clbPermissions.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }
        void BtnSave_Click(object sender, EventArgs e)
        {
            int roleId = Convert.ToInt32(((DataRowView)cboRole.SelectedItem)["RoleId"]);
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlCommand deleteCmd = new SqlCommand(
                    "DELETE FROM RolePermissions WHERE RoleId=@roleId", conn);

                deleteCmd.Parameters.AddWithValue("@roleId", roleId);
                deleteCmd.ExecuteNonQuery();

                foreach (DataRowView item in clbPermissions.CheckedItems)
                {
                    int permissionId = Convert.ToInt32(item["PermissionId"]);

                    SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO RolePermissions(RoleId, PermissionId) VALUES(@roleId,@permissionId)",
                        conn);

                    insertCmd.Parameters.AddWithValue("@roleId", roleId);
                    insertCmd.Parameters.AddWithValue("@permissionId", permissionId);

                    insertCmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Lưu phân quyền thành công!");
        }
    }
}