using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.Core.Models;
namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class LichSuDangNhapForm : UserControl
    {
        private readonly AuditService _auditService;

        public LichSuDangNhapForm()
        {
            InitializeComponent();
            _auditService = AuditService.CreateDefault();

            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
        }

        private void LichSuDangNhapForm_Load(object sender, EventArgs e)
        {
            LoadData();
            dgvLoginHistory.Sorted += DgvLoginHistory_Sorted;
        }

        private void LoadData()
        {
            try
            {
                var username = txtUsername.Text.Trim();
                string status = null;
                if (cboStatus.SelectedIndex == 1) status = "Success";
                else if (cboStatus.SelectedIndex == 2) status = "Failed";
                else if (cboStatus.SelectedIndex == 3) status = "Locked";

                IEnumerable<LoginHistory> data = _auditService.SearchLoginHistory(
                    string.IsNullOrEmpty(username) ? null : username,
                    status,
                    dtpFromDate.Checked ? (DateTime?)dtpFromDate.Value : null,
                    dtpToDate.Checked ? (DateTime?)dtpToDate.Value : null);

                var list = data.ToList();
                dgvLoginHistory.DataSource = null;
                dgvLoginHistory.DataSource = list;
                lblTongCong.Text = "Tổng cộng: " + list.Count + " bản ghi";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnXoaLoc_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            cboStatus.SelectedIndex = 0;
            dtpFromDate.Checked = false;
            dtpToDate.Checked = false;
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            LoadData();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var sfd = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                    FileName = "LichSuDangNhap_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv"
                };
                if (sfd.ShowDialog() != DialogResult.OK) return;

                ExportToCsv(dgvLoginHistory, sfd.FileName);
                MessageBox.Show("Xuất CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvLoginHistory.CurrentRow == null) return;
            var id = dgvLoginHistory.CurrentRow.Cells["LoginHistoryId"]?.Value;
            if (id == null) return;

            if (!int.TryParse(id.ToString(), out int loginHistoryId)) return;

            var item = _auditService.SearchLoginHistory().FirstOrDefault(x => x.LoginHistoryId == loginHistoryId);
            if (item == null) return;

            string logoutStr = item.LogoutTime.HasValue
                ? item.LogoutTime.Value.ToString("dd/MM/yyyy HH:mm:ss")
                : "Chưa đăng xuất";

            var msg =
                "Tài khoản : " + item.Username + "\n" +
                "Họ tên    : " + item.FullName + "\n" +
                "Vai trò   : " + item.RoleName + "\n" +
                "Đăng nhập : " + item.LoginTime.ToString("dd/MM/yyyy HH:mm:ss") + "\n" +
                "Đăng xuất : " + logoutStr + "\n" +
                "IP        : " + item.IpAddress + "\n" +
                "Thiết bị  : " + item.DeviceInfo + "\n" +
                "Trạng thái: " + item.Status + "\n" +
                "Lý do     : " + (item.FailureReason ?? "-");

            MessageBox.Show(msg, "Chi tiết đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DgvLoginHistory_Sorted(object sender, EventArgs e)
        {
            if (dgvLoginHistory.DataSource is List<LoginHistory> list)
                lblTongCong.Text = "Tổng cộng: " + list.Count + " bản ghi";
        }

        private void ExportToCsv(DataGridView dgv, string filePath)
        {
            var lines = new System.Collections.Generic.List<string[]>();
            var headers = new System.Collections.Generic.List<string>();
            foreach (DataGridViewColumn col in dgv.Columns)
                headers.Add(QuoteCsv(col.HeaderText));
            lines.Add(headers.ToArray());

            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cells = new System.Collections.Generic.List<string>();
                foreach (DataGridViewCell cell in row.Cells)
                    cells.Add(QuoteCsv(cell.Value != null ? cell.Value.ToString() : ""));
                lines.Add(cells.ToArray());
            }

            System.IO.File.WriteAllLines(filePath, lines.Select(l => string.Join(",", l)),
                new System.Text.UTF8Encoding(true));
        }

        private static string QuoteCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "\"\"";
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return "\"" + value + "\"";
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }
    }
}
