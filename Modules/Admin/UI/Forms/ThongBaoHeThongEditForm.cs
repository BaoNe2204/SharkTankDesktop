using SharkTank.BLL;
using SharkTank.Core.Models;
using SharkTank.DAL.Sql;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class ThongBaoHeThongEditForm : Form
    {
        private readonly NotificationService _notificationService;
        private SystemNotification _editingItem;
        private readonly int _notificationId = 0;

        public ThongBaoHeThongEditForm()
        {
            InitializeComponent();
            _notificationService = new NotificationService(new SqlNotificationRepository());
        }

        public ThongBaoHeThongEditForm(int notificationId)
        {
            InitializeComponent();
            _notificationService = new NotificationService(new SqlNotificationRepository());
            _notificationId = notificationId;
        }

        private void ThongBaoHeThongEditForm_Load(object sender, EventArgs e)
        {
            LoadComboboxData();

            if (_notificationId > 0)
            {
                LoadNotification();
            }
            else
            {
                dtpStartAt.Value = DateTime.Now;
                chkNoEndDate.Checked = true;
                dtpEndAt.Value = DateTime.Now.AddDays(7);
                dtpEndAt.Enabled = false;
                chkIsActive.Checked = true;
            }
        }

        private void LoadComboboxData()
        {
            cboType.Items.Clear();
            cboType.Items.Add("Thông tin");
            cboType.Items.Add("Cảnh báo");
            cboType.Items.Add("Lỗi");
            cboType.SelectedIndex = 0;

            cboTargetType.Items.Clear();
            cboTargetType.Items.Add("ALL");
            cboTargetType.Items.Add("ROLE");
            cboTargetType.Items.Add("USER");
            cboTargetType.SelectedIndex = 0;
        }

        private void LoadNotification()
        {
            _editingItem = _notificationService.GetAll()
                .FirstOrDefault(x => x.NotificationId == _notificationId);

            if (_editingItem == null) return;

            txtTitle.Text = _editingItem.Title;
            rtbContent.Text = _editingItem.Content;
            SetComboBoxValue(cboType, _editingItem.Type);
            SetComboBoxValue(cboTargetType, _editingItem.TargetType);
            txtTargetValue.Text = _editingItem.TargetValue;
            dtpStartAt.Value = _editingItem.StartAt;
            dtpEndAt.Value = _editingItem.EndAt ?? DateTime.Now;
            chkNoEndDate.Checked = !_editingItem.EndAt.HasValue;
            chkIsActive.Checked = _editingItem.IsActive;

            dtpEndAt.Enabled = !chkNoEndDate.Checked;
        }

        private static void SetComboBoxValue(ComboBox comboBox, string value)
        {
            if (comboBox == null) return;
            if (value == null) return;

            // With DropDownList, setting Text can fail if value không tồn tại trong Items.
            // Ở đây ta chọn đúng item (case-insensitive) nếu tìm thấy.
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                var itemText = comboBox.Items[i]?.ToString();
                if (string.Equals(itemText, value, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
        }

        private void chkNoEndDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpEndAt.Enabled = !chkNoEndDate.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!chkNoEndDate.Checked && dtpEndAt.Value <= dtpStartAt.Value)
                {
                    MessageBox.Show(
                        "Ngày kết thúc phải lớn hơn ngày bắt đầu (hoặc chọn 'Không có ngày kết thúc').",
                        "Dữ liệu không hợp lệ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Nếu _editingItem null (ví dụ không tìm thấy theo id), vẫn giữ NotificationId
                // để Save chạy đường Update thay vì Insert tạo bản ghi mới.
                var model = _editingItem ?? new SystemNotification { NotificationId = _notificationId };

                model.Title = txtTitle.Text.Trim();
                model.Content = rtbContent.Text.Trim();
                model.Type = cboType.Text;
                model.TargetType = cboTargetType.Text;
                model.TargetValue = string.IsNullOrWhiteSpace(txtTargetValue.Text)
                    ? null
                    : txtTargetValue.Text.Trim();
                model.StartAt = dtpStartAt.Value;
                model.EndAt = chkNoEndDate.Checked ? (DateTime?)null : dtpEndAt.Value;
                model.IsActive = chkIsActive.Checked;
                model.CreatedBy = null;

                _notificationService.Save(model);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lưu thông báo: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
