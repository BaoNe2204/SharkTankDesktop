using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    /// <summary>
    /// Form quản lý người dùng (CRUD)
    /// </summary>
    public partial class QuanLyNguoiDungForm : UserControl
    {
        private DataGridView dgvUsers;
        private TextBox txtSearch;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh;

        public QuanLyNguoiDungForm()
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
                Text = "Quản lý người dùng",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 215),
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // Toolbar Panel
            Panel panelToolbar = new Panel
            {
                Location = new Point(30, 80),
                Size = new Size(900, 50),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(panelToolbar);

            // Search TextBox
            Label lblSearch = new Label
            {
                Text = "🔍",
                Font = new Font("Segoe UI", 14),
                Location = new Point(10, 12),
                AutoSize = true
            };
            panelToolbar.Controls.Add(lblSearch);

            txtSearch = new TextBox
            {
                Location = new Point(40, 12),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10)
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            panelToolbar.Controls.Add(txtSearch);

            // Button Add
            btnAdd = new Button
            {
                Text = "➕ Thêm mới",
                Location = new Point(360, 8),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;
            panelToolbar.Controls.Add(btnAdd);

            // Button Edit
            btnEdit = new Button
            {
                Text = "✏️ Sửa",
                Location = new Point(490, 8),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 193, 7),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += BtnEdit_Click;
            panelToolbar.Controls.Add(btnEdit);

            // Button Delete
            btnDelete = new Button
            {
                Text = "🗑️ Xóa",
                Location = new Point(600, 8),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;
            panelToolbar.Controls.Add(btnDelete);

            // Button Refresh
            btnRefresh = new Button
            {
                Text = "🔄 Làm mới",
                Location = new Point(710, 8),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;
            panelToolbar.Controls.Add(btnRefresh);

            // DataGridView
            dgvUsers = new DataGridView
            {
                Location = new Point(30, 150),
                Size = new Size(900, 400),
                Font = new Font("Segoe UI", 10),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            
            // Add columns
            dgvUsers.Columns.Add("UserId", "ID");
            dgvUsers.Columns.Add("Username", "Tên đăng nhập");
            dgvUsers.Columns.Add("FullName", "Họ tên");
            dgvUsers.Columns.Add("Email", "Email");
            dgvUsers.Columns.Add("Role", "Vai trò");
            dgvUsers.Columns.Add("Status", "Trạng thái");
            dgvUsers.Columns.Add("CreatedDate", "Ngày tạo");

            dgvUsers.Columns["UserId"].Width = 50;
            dgvUsers.Columns["Username"].Width = 120;
            dgvUsers.Columns["Status"].Width = 100;
            dgvUsers.Columns["CreatedDate"].Width = 120;

            this.Controls.Add(dgvUsers);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadData()
        {
            // Sample data
            dgvUsers.Rows.Clear();
            dgvUsers.Rows.Add("1", "admin", "Administrator", "admin@sharktank.com", "Admin", "Hoạt động", "01/01/2026");
            dgvUsers.Rows.Add("2", "hr_user", "Nguyễn Văn A", "hr@sharktank.com", "HR", "Hoạt động", "05/01/2026");
            dgvUsers.Rows.Add("3", "sales_user", "Trần Thị B", "sales@sharktank.com", "Sales", "Hoạt động", "10/01/2026");
            dgvUsers.Rows.Add("4", "inventory_user", "Lê Văn C", "inventory@sharktank.com", "Inventory", "Khóa", "15/01/2026");
            dgvUsers.Rows.Add("5", "accounting_user", "Phạm Thị D", "accounting@sharktank.com", "Accounting", "Hoạt động", "20/01/2026");
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // TODO: Implement search logic
            string searchText = txtSearch.Text.ToLower();
            // Filter data based on search text
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng thêm người dùng mới\n\nTODO: Mở form thêm mới",
                "Thêm người dùng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var selectedRow = dgvUsers.SelectedRows[0];
                string username = selectedRow.Cells["Username"].Value.ToString();
                
                MessageBox.Show(
                    $"Chức năng sửa người dùng: {username}\n\nTODO: Mở form chỉnh sửa",
                    "Sửa người dùng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    "Vui lòng chọn người dùng cần sửa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var selectedRow = dgvUsers.SelectedRows[0];
                string username = selectedRow.Cells["Username"].Value.ToString();
                
                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa người dùng: {username}?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dgvUsers.Rows.Remove(selectedRow);
                    MessageBox.Show("Đã xóa người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(
                    "Vui lòng chọn người dùng cần xóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            txtSearch.Clear();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
