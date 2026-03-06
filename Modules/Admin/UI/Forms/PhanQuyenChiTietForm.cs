using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    /// <summary>
    /// Form quản lý phân quyền chi tiết theo chức năng
    /// </summary>
    public partial class PhanQuyenChiTietForm : UserControl
    {
        public PhanQuyenChiTietForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Set properties
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            // Title
            Label lblTitle = new Label
            {
                Text = "Phân quyền chi tiết theo chức năng",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 215),
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // Description
            Label lblDesc = new Label
            {
                Text = "Quản lý phân quyền chi tiết cho từng chức năng cụ thể",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Location = new Point(30, 70),
                AutoSize = true
            };
            this.Controls.Add(lblDesc);

            // Panel chứa form
            Panel panelForm = new Panel
            {
                Location = new Point(30, 120),
                Size = new Size(800, 500),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250)
            };
            this.Controls.Add(panelForm);

            // Label: Chọn Role
            Label lblRole = new Label
            {
                Text = "Chọn Role:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            panelForm.Controls.Add(lblRole);

            // ComboBox: Role
            ComboBox cboRole = new ComboBox
            {
                Location = new Point(20, 50),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboRole.Items.AddRange(new object[] { "Admin", "HR", "Sales", "Inventory", "Accounting", "CRM" });
            cboRole.SelectedIndex = 0;
            panelForm.Controls.Add(cboRole);

            // Label: Chọn Module
            Label lblModule = new Label
            {
                Text = "Chọn Module:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(350, 20),
                AutoSize = true
            };
            panelForm.Controls.Add(lblModule);

            // ComboBox: Module
            ComboBox cboModule = new ComboBox
            {
                Location = new Point(350, 50),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboModule.Items.AddRange(new object[] { "Admin", "HR", "Sales", "Inventory", "Accounting", "CRM" });
            cboModule.SelectedIndex = 0;
            panelForm.Controls.Add(cboModule);

            // CheckedListBox: Danh sách quyền
            Label lblPermissions = new Label
            {
                Text = "Danh sách quyền:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(20, 100),
                AutoSize = true
            };
            panelForm.Controls.Add(lblPermissions);

            CheckedListBox chkPermissions = new CheckedListBox
            {
                Location = new Point(20, 130),
                Size = new Size(630, 300),
                Font = new Font("Segoe UI", 10),
                CheckOnClick = true
            };
            chkPermissions.Items.AddRange(new object[] 
            {
                "VIEW - Xem dữ liệu",
                "CREATE - Tạo mới",
                "UPDATE - Cập nhật",
                "DELETE - Xóa",
                "EXPORT - Xuất dữ liệu",
                "IMPORT - Nhập dữ liệu",
                "APPROVE - Phê duyệt",
                "PRINT - In ấn"
            });
            panelForm.Controls.Add(chkPermissions);

            // Button: Lưu
            Button btnSave = new Button
            {
                Text = "💾 Lưu",
                Location = new Point(20, 450),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            panelForm.Controls.Add(btnSave);

            // Button: Hủy
            Button btnCancel = new Button
            {
                Text = "❌ Hủy",
                Location = new Point(150, 450),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            panelForm.Controls.Add(btnCancel);

            // Button: Chọn tất cả
            Button btnSelectAll = new Button
            {
                Text = "✓ Chọn tất cả",
                Location = new Point(530, 450),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSelectAll.FlatAppearance.BorderSize = 0;
            btnSelectAll.Click += (s, e) => 
            {
                for (int i = 0; i < chkPermissions.Items.Count; i++)
                    chkPermissions.SetItemChecked(i, true);
            };
            panelForm.Controls.Add(btnSelectAll);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Lưu phân quyền thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Đã hủy thao tác",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
