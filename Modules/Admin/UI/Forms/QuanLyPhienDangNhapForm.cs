using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class QuanLyPhienDangNhapForm : UserControl
    {
        public QuanLyPhienDangNhapForm()
        {
            InitializeComponent();
            WireEvents();
            LoadSessions();
        }

        private void WireEvents()
        {
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnLogout.Click += btnLogout_Click;
            btnLogoutAll.Click += btnLogoutAll_Click;
            btnLockUser.Click += btnLockUser_Click;
            btnDetail.Click += btnDetail_Click;
            cbStatus.SelectedIndexChanged += cbStatus_SelectedIndexChanged;
        }

        // ================= LOAD SESSIONS (1 USER = 1 ROW) =================
        private void LoadSessions()
        {
            LoadSessions(GetUserFilter(), GetIpFilter(), GetStatusFilter());
        }

        private void LoadSessions(string user, string ip, string status)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string sql = @"
;WITH UserSession AS
(
    SELECT
        u.UserId,
        u.Username,
        COALESCE(sa.SessionId, sl.SessionId) AS SessionId,
        COALESCE(sa.IpAddress, sl.IpAddress, N'') AS IpAddress,
        COALESCE(sa.DeviceInfo, sl.DeviceInfo, N'') AS DeviceInfo,
        COALESCE(sa.LoginTime, sl.LoginTime) AS LoginTime,
        COALESCE(sa.LogoutTime, sl.LogoutTime) AS LogoutTime,
        CASE WHEN sa.SessionId IS NOT NULL THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS IsActive
    FROM dbo.Users u
    OUTER APPLY
    (
        SELECT TOP 1 s.SessionId, s.IpAddress, s.DeviceInfo, s.LoginTime, s.LogoutTime
        FROM dbo.LoginSessions s
        WHERE s.UserId = u.UserId AND s.IsActive = 1
        ORDER BY s.LoginTime DESC
    ) sa
    OUTER APPLY
    (
        SELECT TOP 1 s.SessionId, s.IpAddress, s.DeviceInfo, s.LoginTime, s.LogoutTime
        FROM dbo.LoginSessions s
        WHERE s.UserId = u.UserId
        ORDER BY s.LoginTime DESC
    ) sl
)
SELECT
    SessionId,
    UserId,
    Username,
    IpAddress,
    DeviceInfo,
    LoginTime,
    LogoutTime,
    IsActive,
    CASE WHEN IsActive = 1 THEN N'Online' ELSE N'Offline' END AS StatusText
FROM UserSession
WHERE (@User = N'' OR Username LIKE N'%' + @User + N'%')
  AND (@Ip = N'' OR ISNULL(IpAddress, N'') LIKE N'%' + @Ip + N'%')
  AND
  (
        @Status = N'All'
        OR (@Status = N'Online' AND IsActive = 1)
        OR (@Status = N'Offline' AND IsActive = 0)
        OR (@Status = N'Expired' AND IsActive = 0 AND LogoutTime IS NOT NULL)
  )
ORDER BY Username;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@User", user);
                    da.SelectCommand.Parameters.AddWithValue("@Ip", ip);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvSessions.DataSource = dt;
                    ApplyGridFormatting();
                    UpdateStats(dt);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSessions();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSessions();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = GetSelectedRow();
            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần đăng xuất");
                return;
            }

            var userIdValue = selectedRow.Cells["UserId"].Value;
            if (userIdValue == null)
            {
                MessageBox.Show("Không xác định được UserId");
                return;
            }

            bool isActive = selectedRow.Cells["IsActive"].Value != DBNull.Value && Convert.ToBoolean(selectedRow.Cells["IsActive"].Value);
            if (!isActive)
            {
                MessageBox.Show("Tài khoản này đang Offline rồi.");
                return;
            }

            int userId = Convert.ToInt32(userIdValue);

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                const string sql = @"
UPDATE dbo.LoginSessions
SET IsActive = 0,
    LogoutTime = GETDATE()
WHERE UserId = @UserId AND IsActive = 1;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadSessions();
        }

        private void btnLogoutAll_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất tất cả phiên đang online?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                const string sql = @"
UPDATE dbo.LoginSessions
SET IsActive = 0,
    LogoutTime = GETDATE()
WHERE IsActive = 1;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            LoadSessions();
        }

        private void btnLockUser_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = GetSelectedRow();
            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để khóa");
                return;
            }

            var userIdValue = selectedRow.Cells["UserId"].Value;
            if (userIdValue == null) return;
            int userId = Convert.ToInt32(userIdValue);

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE dbo.Users SET IsLocked = 1, IsActive = 0, UpdatedAt = GETDATE() WHERE UserId = @UserId;", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE dbo.LoginSessions SET IsActive = 0, LogoutTime = GETDATE() WHERE UserId = @UserId AND IsActive = 1;", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Đã khóa tài khoản và đăng xuất toàn bộ phiên online của user.");
            LoadSessions();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = GetSelectedRow();
            if (row == null)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xem chi tiết");
                return;
            }

            string detail =
                $"User: {row.Cells["Username"].Value}\n" +
                $"SessionId: {row.Cells["SessionId"].Value}\n" +
                $"IP: {row.Cells["IpAddress"].Value}\n" +
                $"Device: {row.Cells["DeviceInfo"].Value}\n" +
                $"LoginTime: {row.Cells["LoginTime"].Value}\n" +
                $"LogoutTime: {row.Cells["LogoutTime"].Value}\n" +
                $"Status: {row.Cells["StatusText"].Value}";

            MessageBox.Show(detail, "Chi tiết tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSessions();
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "User...")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.Black;
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                txtUser.Text = "User...";
                txtUser.ForeColor = Color.Gray;
            }
        }

        private void txtIP_Enter(object sender, EventArgs e)
        {
            if (txtIP.Text == "IP...")
            {
                txtIP.Text = "";
                txtIP.ForeColor = Color.Black;
            }
        }

        private void txtIP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIP.Text))
            {
                txtIP.Text = "IP...";
                txtIP.ForeColor = Color.Gray;
            }
        }

        private string GetUserFilter()
        {
            return txtUser.Text == "User..." ? "" : txtUser.Text.Trim();
        }

        private string GetIpFilter()
        {
            return txtIP.Text == "IP..." ? "" : txtIP.Text.Trim();
        }

        private string GetStatusFilter()
        {
            return cbStatus.SelectedItem?.ToString() ?? "All";
        }

        private void UpdateStats(DataTable dt)
        {
            int online = 0;
            int offline = 0;

            foreach (DataRow row in dt.Rows)
            {
                bool isActive = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"]);
                if (isActive) online++;
                else offline++;
            }

            lblOnline.Text = $"Online: {online}";
            lblOffline.Text = $"Offline: {offline}";
        }

        private void ApplyGridFormatting()
        {
            if (dgvSessions.Columns.Contains("IsActive"))
                dgvSessions.Columns["IsActive"].Visible = false;

            if (dgvSessions.Columns.Contains("UserId"))
                dgvSessions.Columns["UserId"].Visible = false;

            if (dgvSessions.Columns.Contains("StatusText"))
                dgvSessions.Columns["StatusText"].HeaderText = "Status";

            dgvSessions.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvSessions.MultiSelect = true;
            dgvSessions.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
        }

        private DataGridViewRow GetSelectedRow()
        {
            if (dgvSessions.SelectedRows.Count > 0)
                return dgvSessions.SelectedRows[0];

            if (dgvSessions.CurrentCell != null)
                return dgvSessions.Rows[dgvSessions.CurrentCell.RowIndex];

            return null;
        }
    }
}
