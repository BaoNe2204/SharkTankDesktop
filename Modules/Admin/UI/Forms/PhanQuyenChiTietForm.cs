using SharkTank.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class PhanQuyenChiTietForm : UserControl
    {
        public PhanQuyenChiTietForm()
        {
            InitializeComponent();
        }

        private void PhanQuyenChiTietForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadPermissions();

            if (cboUser.Items.Count > 0)
            {
                cboUser.SelectedIndex = 0;
                cboUser_SelectedIndexChanged(null, null);
            }

            cboUser.SelectedIndexChanged += cboUser_SelectedIndexChanged;
        }
        private void LoadUsers()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT UserId, Username FROM dbo.Users ORDER BY Username", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cboUser.DataSource = dt;
                cboUser.DisplayMember = "Username";
                cboUser.ValueMember = "UserId";
            }
        }

        private void LoadPermissions()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT PermissionId, PermissionName FROM dbo.Permissions ORDER BY PermissionName",
                    conn);

                SqlDataReader reader = cmd.ExecuteReader();

                clbPermissions.Items.Clear();

                while (reader.Read())
                {
                    clbPermissions.Items.Add(new PermissionItem
                    {
                        Id = (int)reader["PermissionId"],
                        Name = reader["PermissionName"].ToString()
                    });
                }
            }
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUser.SelectedValue == null) return;

            int userId = Convert.ToInt32(cboUser.SelectedValue);
            List<int> permissionIds = GetEffectivePermissionIds(userId);

            for (int i = 0; i < clbPermissions.Items.Count; i++)
            {
                PermissionItem item = (PermissionItem)clbPermissions.Items[i];
                clbPermissions.SetItemChecked(i, permissionIds.Contains(item.Id));
            }
        }

        private List<int> GetEffectivePermissionIds(int userId)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                EnsureUserPermissionsTable(conn);

                SqlCommand userPermCmd = new SqlCommand(
                    "SELECT PermissionId FROM dbo.UserPermissions WHERE UserId = @UserId", conn);
                userPermCmd.Parameters.AddWithValue("@UserId", userId);

                List<int> userPermissionIds = new List<int>();
                using (SqlDataReader reader = userPermCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userPermissionIds.Add((int)reader["PermissionId"]);
                    }
                }

                if (userPermissionIds.Count > 0)
                {
                    return userPermissionIds;
                }

                SqlCommand rolePermCmd = new SqlCommand(@"
SELECT rp.PermissionId
FROM dbo.Users u
JOIN dbo.RolePermissions rp ON u.RoleId = rp.RoleId
WHERE u.UserId = @UserId", conn);
                rolePermCmd.Parameters.AddWithValue("@UserId", userId);

                List<int> rolePermissionIds = new List<int>();
                using (SqlDataReader reader = rolePermCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rolePermissionIds.Add((int)reader["PermissionId"]);
                    }
                }

                return rolePermissionIds;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboUser.SelectedValue == null) return;

            int userId = Convert.ToInt32(cboUser.SelectedValue);
            List<int> selectedPermissionIds = clbPermissions.CheckedItems
                .Cast<PermissionItem>()
                .Select(p => p.Id)
                .ToList();

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                EnsureUserPermissionsTable(conn);

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        SqlCommand deleteCmd = new SqlCommand(
                            "DELETE FROM dbo.UserPermissions WHERE UserId = @UserId", conn, tran);
                        deleteCmd.Parameters.AddWithValue("@UserId", userId);
                        deleteCmd.ExecuteNonQuery();

                        foreach (int permissionId in selectedPermissionIds)
                        {
                            SqlCommand insertCmd = new SqlCommand(
                                "INSERT INTO dbo.UserPermissions(UserId, PermissionId) VALUES(@UserId, @PermissionId)",
                                conn,
                                tran);

                            insertCmd.Parameters.AddWithValue("@UserId", userId);
                            insertCmd.Parameters.AddWithValue("@PermissionId", permissionId);
                            insertCmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }

            MessageBox.Show("Lưu phân quyền chi tiết theo tài khoản thành công!");
        }

        private static void EnsureUserPermissionsTable(SqlConnection conn)
        {
            string sql = @"
IF OBJECT_ID('dbo.UserPermissions', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.UserPermissions
    (
        UserId INT NOT NULL,
        PermissionId INT NOT NULL,
        CONSTRAINT PK_UserPermissions PRIMARY KEY (UserId, PermissionId),
        CONSTRAINT FK_UserPermissions_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId),
        CONSTRAINT FK_UserPermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES dbo.Permissions(PermissionId)
    );
END";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class PermissionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
