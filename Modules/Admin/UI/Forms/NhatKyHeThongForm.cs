using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.Core.Models;
namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class NhatKyHeThongForm : UserControl
    {
        private readonly AuditService _auditService;

        public NhatKyHeThongForm()
        {
            InitializeComponent();
            _auditService = AuditService.CreateDefault();

            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
        }

        private void NhatKyHeThongForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string tableName = cboTableName.SelectedIndex > 0
                    ? cboTableName.Text.Trim() : null;
                var recordId = txtRecordId.Text.Trim();
                string changeType = null;
                if (cboChangeType.SelectedIndex == 1) changeType = "INSERT";
                else if (cboChangeType.SelectedIndex == 2) changeType = "UPDATE";
                else if (cboChangeType.SelectedIndex == 3) changeType = "DELETE";

                IEnumerable<DataChangeLog> data = _auditService.SearchDataChangeLogs(
                    tableName: string.IsNullOrEmpty(tableName) ? null : tableName,
                    recordId: string.IsNullOrEmpty(recordId) ? null : recordId,
                    fromDate: dtpFromDate.Checked ? (DateTime?)dtpFromDate.Value : null,
                    toDate: dtpToDate.Checked ? (DateTime?)dtpToDate.Value : null);

                IEnumerable<DataChangeLog> filtered = data;
                if (!string.IsNullOrEmpty(changeType))
                    filtered = data.Where(x => x.ChangeType == changeType);

                var list = filtered.ToList();
                dgvDataChange.DataSource = null;
                dgvDataChange.DataSource = list;
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
            txtRecordId.Clear();
            cboTableName.SelectedIndex = 0;
            cboChangeType.SelectedIndex = 0;
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
                    FileName = "TheoDoiDuLieu_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv"
                };
                if (sfd.ShowDialog() != DialogResult.OK) return;
                ExportToCsv(dgvDataChange, sfd.FileName);
                MessageBox.Show("Xuất CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvDataChange.CurrentRow == null) return;
            var idVal = dgvDataChange.CurrentRow.Cells["ChangeLogId"]?.Value;
            if (idVal == null || !int.TryParse(idVal.ToString(), out int id)) return;

            var item = _auditService.SearchDataChangeLogs().FirstOrDefault(x => x.ChangeLogId == id);
            if (item == null) return;

            AuditLog auditInfo = null;
            if (item.AuditLogId.HasValue)
                auditInfo = _auditService.GetAuditLogById(item.AuditLogId.Value);

            var msg =
                "ID          : " + item.ChangeLogId + "\n" +
                "Audit Log ID: " + (item.AuditLogId.HasValue ? item.AuditLogId.Value.ToString() : "-") + "\n" +
                "Bảng        : " + item.TableName + "\n" +
                "ID bản ghi : " + item.RecordId + "\n" +
                "Trường      : " + item.FieldName + "\n" +
                "Giá trị cũ : " + (item.OldValue ?? "(null)") + "\n" +
                "Giá trị mới : " + (item.NewValue ?? "(null)") + "\n" +
                "Loại thay đổi: " + item.ChangeTypeDisplay + "\n" +
                "Thời gian   : " + item.ChangeTime.ToString("dd/MM/yyyy HH:mm:ss");

            if (auditInfo != null)
            {
                msg +=
                    "\n\n-- Thông tin Audit --\n" +
                    "Người thực hiện: " + auditInfo.Username + " (" + auditInfo.FullName + ")\n" +
                    "Hành động      : " + auditInfo.ActionDisplay + "\n" +
                    "Mô tả          : " + (auditInfo.Description ?? "-");
            }

            MessageBox.Show(msg, "Chi tiết thay đổi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
