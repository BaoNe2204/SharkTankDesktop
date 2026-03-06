using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    /// <summary>
    /// Form phân quyền theo Role
    /// </summary>
    public partial class PhanQuyenTheoRoleForm : UserControl
    {
        private ComboBox cboRole;
        private CheckedListBox chkModules;
        private Button btnSave, btnCancel, btnSelectAll, btnDeselectAll;

        public PhanQuyenTheoRoleForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            // Title
            Label lblTitle = new Label
            {
                Text = "Phân quyền theo Role",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 215),
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // Description
            Label lblDesc = new Label
            {
                Text = "Quản lý phân quyền truy cập module theo vai trò (Role)",
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
            cboRole = new ComboBox
            {
                Location = new Point(20, 50),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboRole.Items.AddRange(new object[] 
            { 
                "Admin - Quản trị viên",
                "HR - Nhân sự",
                "Sales - Bán hàng",
                "Inventory - Kho",
                "Accounting - Kế toán",
                "CRM - Quản lý khách hàng"
            });
            cboRole.SelectedIndex = 0;
            cboRole.SelectedIndexChanged += CboRole_SelectedIndexChanged;
            panelForm.Controls.Add(cboRole);

            // Info Panel
            Panel panelInfo = new Panel
            {
                Location = new Point(450, 20),
                Size = new Size(330, 60),
                BackColor = Color.FromArgb(230, 244, 255),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelForm.Controls.Add(panelInfo);

            Label lblInfo = new Label
            {
                Text = "ℹ️ Thông tin Role",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 5),
                AutoSize = true
            };
            panelInfo.Controls.Add(lblInfo);

            Label lblInfoDesc = new Label
            {
                Text = "Admin có toàn quyền truy cập\ntất cả các module trong hệ thống",
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, 28),
                AutoSize = true
            };
            panelInfo.Controls.Add(lblInfoDesc);

            // Label: Modules
            Label lblModules = new Label
            {
                Text = "Chọn các Module được phép truy cập:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(20, 100),
                AutoSize = true
            };
            panelForm.Controls.Add(lblModules);

            // CheckedListBox: Modules
            chkModules = new CheckedListBox
            {
                Location = new Point(20, 130),
                Size = new Size(760, 280),
                Font = new Font("Segoe UI", 10),
                CheckOnClick = true
            };
            chkModules.Items.AddRange(new object[] 
            {
                "👑 Admin - Quản trị hệ thống",
                "💰 Accounting - Kế toán",
                "👥 HR - Quản lý nhân sự",
                "📦 Inventory - Quản lý kho",
                "💼 Sales - Bán hàng",
                "📞 CRM - Quản lý khách hàng"
            });
            panelForm.Controls.Add(chkModules);

            // Button: Chọn tất cả
            btnSelectAll = new Button
            {
                Text = "✓ Chọn tất cả",
                Location = new Point(20, 425),
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
                for (int i = 0; i < chkModules.Items.Count; i++)
                    chkModules.SetItemChecked(i, true);
            };
            panelForm.Controls.Add(btnSelectAll);

            // Button: Bỏ chọn tất cả
            btnDeselectAll = new Button
            {
                Text = "✗ Bỏ chọn tất cả",
                Location = new Point(150, 425),
                Size = new Size(130, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDeselectAll.FlatAppearance.BorderSize = 0;
            btnDeselectAll.Click += (s, e) => 
            {
                for (int i = 0; i < chkModules.Items.Count; i++)
                    chkModules.SetItemChecked(i, false);
            };
            panelForm.Controls.Add(btnDeselectAll);

            // Button: Lưu
            btnSave = new Button
            {
                Text = "💾 Lưu phân quyền",
                Location = new Point(540, 425),
                Size = new Size(140, 35),
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
            btnCancel = new Button
            {
                Text = "❌ Hủy",
                Location = new Point(690, 425),
                Size = new Size(90, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            panelForm.Controls.Add(btnCancel);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadData()
        {
            // Load permissions for selected role
            LoadPermissionsForRole(cboRole.SelectedIndex);
        }

        private void LoadPermissionsForRole(int roleIndex)
        {
            // Clear all
            for (int i = 0; i < chkModules.Items.Count; i++)
                chkModules.SetItemChecked(i, false);

            // Set permissions based on role
            switch (roleIndex)
            {
                case 0: // Admin - All modules
                    for (int i = 0; i < chkModules.Items.Count; i++)
                        chkModules.SetItemChecked(i, true);
                    break;
                
                case 1: // HR - Only HR module
                    chkModules.SetItemChecked(2, true); // HR
                    break;
                
                case 2: // Sales - Only Sales module
                    chkModules.SetItemChecked(4, true); // Sales
                    chkModules.SetItemChecked(5, true); // CRM
                    break;
                
                case 3: // Inventory - Only Inventory module
                    chkModules.SetItemChecked(3, true); // Inventory
                    break;
                
                case 4: // Accounting - Only Accounting module
                    chkModules.SetItemChecked(1, true); // Accounting
                    break;
                
                case 5: // CRM - Only CRM module
                    chkModules.SetItemChecked(5, true); // CRM
                    break;
            }
        }

        private void CboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPermissionsForRole(cboRole.SelectedIndex);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Get selected modules
            string selectedModules = "";
            for (int i = 0; i < chkModules.Items.Count; i++)
            {
                if (chkModules.GetItemChecked(i))
                {
                    selectedModules += chkModules.Items[i].ToString() + "\n";
                }
            }

            if (string.IsNullOrEmpty(selectedModules))
            {
                MessageBox.Show(
                    "Vui lòng chọn ít nhất một module!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var result = MessageBox.Show(
                $"Lưu phân quyền cho Role: {cboRole.Text}\n\nCác module được chọn:\n{selectedModules}\nBạn có chắc chắn?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // TODO: Save to database
                MessageBox.Show(
                    "Đã lưu phân quyền thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            LoadData(); // Reset to original state
            MessageBox.Show(
                "Đã hủy thao tác",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
