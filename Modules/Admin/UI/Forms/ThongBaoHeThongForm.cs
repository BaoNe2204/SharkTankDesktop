using SharkTank.BLL;
using SharkTank.Core.Models;
using SharkTank.DAL.Sql;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class ThongBaoHeThongForm : UserControl
    {
        private readonly NotificationService _notificationService;

        public ThongBaoHeThongForm()
        {
            InitializeComponent();
            _notificationService = new NotificationService(new SqlNotificationRepository());
        }

        private void ThongBaoHeThongForm_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadData();
        }

        private void ConfigureGrid()
        {
            dgvNotifications.AutoGenerateColumns = false;
            dgvNotifications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNotifications.MultiSelect = false;
            dgvNotifications.ReadOnly = true;
        }

        private void LoadData()
        {
            try
            {
                var data = _notificationService.GetAll().ToList();

                dgvNotifications.DataSource = null;
                dgvNotifications.AutoGenerateColumns = true;
                dgvNotifications.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var f = new ThongBaoHeThongEditForm())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var item = GetSelectedItem();
            if (item == null) return;

            using (var f = new ThongBaoHeThongEditForm(item.NotificationId))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var item = GetSelectedItem();
            if (item == null) return;

            var confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa thông báo này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _notificationService.Delete(item.NotificationId);
                LoadData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private SystemNotification GetSelectedItem()
        {
            if (dgvNotifications.CurrentRow == null) return null;

            var bound = dgvNotifications.CurrentRow.DataBoundItem;
            if (bound is SystemNotification sn) return sn;

            // Fallback: nếu DataBoundItem không phải SystemNotification, lấy NotificationId từ cột.
            var col = dgvNotifications.Columns
                .Cast<DataGridViewColumn>()
                .FirstOrDefault(c =>
                    string.Equals(c.Name, "NotificationId", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.DataPropertyName, "NotificationId", StringComparison.OrdinalIgnoreCase));

            if (col != null)
            {
                var val = dgvNotifications.CurrentRow.Cells[col.Index]?.Value;
                if (val != null && int.TryParse(val.ToString(), out var id))
                    return new SystemNotification { NotificationId = id };
            }

            return null;
        }
    }
}
