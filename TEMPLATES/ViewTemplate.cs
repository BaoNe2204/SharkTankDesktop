using System;
using System.Drawing;
using System.Windows.Forms;

// ============================================
// TEMPLATE - Copy file này để tạo View mới
// ============================================
// 1. Đổi namespace: SharkTank.Modules.[TenModule].Views
// 2. Đổi class name: [TenView]
// 3. Implement logic nghiệp vụ
// ============================================

namespace SharkTank.Modules.Templates
{
    /// <summary>
    /// Template để tạo View mới
    /// Copy file này và thay thế [TenModule] và [TenView] bằng tên thực tế
    /// </summary>
    public class BaoCaoTaiChinhView : UserControl
    {
        // ============================================
        // KHAI BÁO CONTROLS
        // ============================================
        private Label lblTitle;
        private Label lblDescription;
        private Button btnSave;
        private Button btnCancel;
        private DataGridView dgvData;
        private TextBox txtSearch;
        
        // ============================================
        // CONSTRUCTOR
        // ============================================
        public BaoCaoTaiChinhView()
        {
            InitializeComponent();
            LoadData();
        }

        // ============================================
        // INITIALIZE COMPONENTS
        // ============================================
        private void InitializeComponent()
        {
            // Thiết lập UserControl
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(20);

            // ========== TITLE ==========
            lblTitle = new Label
            {
                Text = "[Module] - [View Name]",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 215),
                Location = new Point(30, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // ========== DESCRIPTION ==========
            lblDescription = new Label
            {
                Text = "Mô tả chức năng của view",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                Location = new Point(30, 60),
                AutoSize = true
            };
            this.Controls.Add(lblDescription);

            // ========== SEARCH BOX ==========
            txtSearch = new TextBox
            {
                Location = new Point(30, 100),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10)
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            this.Controls.Add(txtSearch);

            // ========== BUTTONS ==========
            btnSave = new Button
            {
                Text = "💾 Lưu",
                Location = new Point(30, 140),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button
            {
                Text = "❌ Hủy",
                Location = new Point(140, 140),
                Size = new Size(100, 35),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(btnCancel);

            // ========== DATA GRID VIEW ==========
            dgvData = new DataGridView
            {
                Location = new Point(30, 190),
                Size = new Size(900, 400),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(dgvData);
        }

        // ============================================
        // LOAD DATA
        // ============================================
        private void LoadData()
        {
            try
            {
                // Setup columns
                dgvData.Columns.Clear();
                dgvData.Columns.Add("ID", "ID");
                dgvData.Columns.Add("Name", "Tên");
                dgvData.Columns.Add("Description", "Mô tả");
                dgvData.Columns.Add("Status", "Trạng thái");
                dgvData.Columns.Add("CreatedDate", "Ngày tạo");

                // Set column widths
                dgvData.Columns["ID"].Width = 50;
                dgvData.Columns["Status"].Width = 100;
                dgvData.Columns["CreatedDate"].Width = 120;

                // TODO: Load data from database
                // Sample data
                dgvData.Rows.Add("1", "Item 1", "Description 1", "Active", "01/01/2026");
                dgvData.Rows.Add("2", "Item 2", "Description 2", "Active", "02/01/2026");
                dgvData.Rows.Add("3", "Item 3", "Description 3", "Inactive", "03/01/2026");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================
        // EVENT HANDLERS
        // ============================================
        
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // TODO: Implement search logic
            string searchText = txtSearch.Text.ToLower();
            
            // Example: Filter DataGridView
            // foreach (DataGridViewRow row in dgvData.Rows)
            // {
            //     row.Visible = row.Cells["Name"].Value.ToString().ToLower().Contains(searchText);
            // }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (!ValidateInput())
                {
                    MessageBox.Show("Vui lòng kiểm tra lại thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // TODO: Implement save logic
                // Example: Save to database
                
                MessageBox.Show("Đã lưu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Reload data
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn hủy?", 
                "Xác nhận", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question
            );
            
            if (result == DialogResult.Yes)
            {
                ClearForm();
            }
        }

        // ============================================
        // HELPER METHODS
        // ============================================
        
        private void ClearForm()
        {
            txtSearch.Clear();
            // Clear other controls...
        }

        private bool ValidateInput()
        {
            // TODO: Implement validation
            // Example:
            // if (string.IsNullOrWhiteSpace(txtName.Text))
            // {
            //     MessageBox.Show("Vui lòng nhập tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     return false;
            // }
            
            return true;
        }

        private void RefreshData()
        {
            LoadData();
        }
    }
}
