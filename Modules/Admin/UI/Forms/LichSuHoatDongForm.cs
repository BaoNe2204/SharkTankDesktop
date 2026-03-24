using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.Core.Models;
namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class LichSuHoatDongForm : UserControl
    {
        private readonly AuditService _auditService;

        public LichSuHoatDongForm()
        {
            InitializeComponent();
            _auditService = AuditService.CreateDefault();

            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
        }

        private void LichSuHoatDongForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var username = txtUsername.Text.Trim();
                string action = null;
                if (cboAction.SelectedIndex == 1) action = "CREATE";
                else if (cboAction.SelectedIndex == 2) action = "UPDATE";
                else if (cboAction.SelectedIndex == 3) action = "DELETE";
                else if (cboAction.SelectedIndex == 4) action = "LOGIN";
                else if (cboAction.SelectedIndex == 5) action = "LOGOUT";
                else if (cboAction.SelectedIndex == 6) action = "VIEW";

                string entityType = cboEntityType.SelectedIndex > 0
                    ? cboEntityType.Text.Trim() : null;

                IEnumerable<AuditLog> data = _auditService.SearchAuditLogs(
                    string.IsNullOrEmpty(username) ? null : username,
                    action,
                    entityType,
                    dtpFromDate.Checked ? (DateTime?)dtpFromDate.Value : null,
                    dtpToDate.Checked ? (DateTime?)dtpToDate.Value : null);

                var list = data.ToList();
                dgvAuditLogs.DataSource = null;
                dgvAuditLogs.DataSource = list;
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
            cboAction.SelectedIndex = 0;
            cboEntityType.SelectedIndex = 0;
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
                    FileName = "LichSuThaoTac_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv"
                };
                if (sfd.ShowDialog() != DialogResult.OK) return;
                ExportToCsv(dgvAuditLogs, sfd.FileName);
                MessageBox.Show("Xuất CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvAuditLogs.CurrentRow == null) return;
            var idVal = dgvAuditLogs.CurrentRow.Cells["AuditLogId"]?.Value;
            if (idVal == null || !int.TryParse(idVal.ToString(), out int id)) return;

            var item = _auditService.GetAuditLogById(id);
            if (item == null) return;

            var changes = _auditService.SearchDataChangeLogs(auditLogId: id).ToList();
            string changesText = "";
            if (changes.Count > 0)
            {
                var lines = new System.Collections.Generic.List<string>();
                foreach (var c in changes)
                {
                    var oldStr = c.OldValue ?? "(null)";
                    var newStr = c.NewValue ?? "(null)";
                    lines.Add("  [" + c.FieldName + "]  " + oldStr + "  ->  " + newStr);
                }
                changesText = "\n\nChi tiết thay đổi:\n" + string.Join("\n", lines);
            }

            var msg =
                "Tài khoản : " + item.Username + "\n" +
                "Họ tên    : " + item.FullName + "\n" +
                "Hành động : " + item.ActionDisplay + "\n" +
                "Đối tượng : " + item.EntityType + "\n" +
                "ID bản ghi: " + (item.EntityId ?? "-") + "\n" +
                "Tên đối tượng: " + (item.EntityName ?? "-") + "\n" +
                "Mô tả     : " + (item.Description ?? "-") + "\n" +
                "IP        : " + item.IpAddress + "\n" +
                "Thiết bị  : " + item.DeviceInfo + "\n" +
                "Thời gian  : " + item.Timestamp.ToString("dd/MM/yyyy HH:mm:ss") + "\n" +
                "Giá trị cũ: " + (item.OldValues ?? "-") + "\n" +
                "Giá trị mới: " + (item.NewValues ?? "-") + changesText;

            MessageBox.Show(msg, "Chi tiết thao tác", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoaLog_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa các log cũ hơn 90 ngày?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                _auditService.DeleteOldAuditLogs(90);
                LoadData();
                MessageBox.Show("Đã xóa log cũ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa log: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void lblEntityType_Click(object sender, EventArgs e)
        {

        }

        private void lblTuNgay_Click(object sender, EventArgs e)
        {

        }
    }
}
