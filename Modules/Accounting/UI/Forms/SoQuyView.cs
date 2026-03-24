using System;
using System.Drawing;
using System.Windows.Forms;

// ============================================
// MODULE: Finance (Tài Chính)
// VIEW: SoQuyView (Sổ Quỹ)
// ============================================

namespace SharkTank.Modules.Finance.Views
{
    /// <summary>
    /// View quản lý Sổ Quỹ (Tiền mặt, Ngân hàng, Số dư)
    /// </summary>
    public class SoQuyView : UserControl
    {
        // ============================================
        // KHAI BÁO CONTROLS
        // ============================================
        private Label lblTitle;
        private Label lblDescription;

        // Controls cho phần Tổng quan
        private Panel pnlSummary;
        private Label lblTienMatTitle;
        private Label lblTienMatValue;
        private Label lblNganHangTitle;
        private Label lblNganHangValue;
        private Label lblSoDuTitle;
        private Label lblSoDuValue;

        private Button btnAddThu;
        private Button btnAddChi;
        private DataGridView dgvData;
        private TextBox txtSearch;

        // ============================================
        // CONSTRUCTOR
        // ============================================
        public SoQuyView()
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
                Text = "TÀI CHÍNH - QUẢN LÝ SỔ QUỸ",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 215),
                Location = new Point(30, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // ========== DESCRIPTION ==========
            lblDescription = new Label
            {
                Text = "Theo dõi dòng tiền mặt, tiền gửi ngân hàng và tổng số dư hiện tại.",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                Location = new Point(30, 60),
                AutoSize = true
            };
            this.Controls.Add(lblDescription);

            // ========== SUMMARY PANEL (Tiền Mặt, Ngân Hàng, Số Dư) ==========
            pnlSummary = new Panel
            {
                Location = new Point(30, 95),
                Size = new Size(900, 80),
                BackColor = Color.FromArgb(240, 244, 248),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Tiền mặt
            lblTienMatTitle = new Label { Text = "💵 TIỀN MẶT", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(20, 15), AutoSize = true };
            lblTienMatValue = new Label { Text = "0 đ", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(40, 167, 69), Location = new Point(20, 40), AutoSize = true };

            // Ngân hàng
            lblNganHangTitle = new Label { Text = "🏦 NGÂN HÀNG", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(320, 15), AutoSize = true };
            lblNganHangValue = new Label { Text = "0 đ", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(0, 120, 215), Location = new Point(320, 40), AutoSize = true };

            // Tổng Số dư
            lblSoDuTitle = new Label { Text = "💰 TỔNG SỐ DƯ", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(620, 15), AutoSize = true };
            lblSoDuValue = new Label { Text = "0 đ", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(220, 53, 69), Location = new Point(620, 40), AutoSize = true };

            pnlSummary.Controls.Add(lblTienMatTitle);
            pnlSummary.Controls.Add(lblTienMatValue);
            pnlSummary.Controls.Add(lblNganHangTitle);
            pnlSummary.Controls.Add(lblNganHangValue);
            pnlSummary.Controls.Add(lblSoDuTitle);
            pnlSummary.Controls.Add(lblSoDuValue);
            this.Controls.Add(pnlSummary);

            // ========== SEARCH BOX ==========
            txtSearch = new TextBox
            {
                Location = new Point(30, 195),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10)
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            this.Controls.Add(txtSearch);

            // ========== BUTTONS ==========
            btnAddThu = new Button
            {
                Text = "➕ Thêm Phiếu Thu",
                Location = new Point(660, 190),
                Size = new Size(130, 35),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAddThu.FlatAppearance.BorderSize = 0;
            btnAddThu.Click += BtnAddThu_Click;
            this.Controls.Add(btnAddThu);

            btnAddChi = new Button
            {
                Text = "➖ Thêm Phiếu Chi",
                Location = new Point(800, 190),
                Size = new Size(130, 35),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAddChi.FlatAppearance.BorderSize = 0;
            btnAddChi.Click += BtnAddChi_Click;
            this.Controls.Add(btnAddChi);

            // ========== DATA GRID VIEW ==========
            dgvData = new DataGridView
            {
                Location = new Point(30, 240),
                Size = new Size(900, 350),
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
                dgvData.Columns.Add("ID", "Mã GD");
                dgvData.Columns.Add("Date", "Ngày");
                dgvData.Columns.Add("Type", "Loại");
                dgvData.Columns.Add("Method", "Hình thức");
                dgvData.Columns.Add("Amount", "Số tiền");
                dgvData.Columns.Add("Description", "Diễn giải");

                // Set column widths
                dgvData.Columns["ID"].Width = 80;
                dgvData.Columns["Date"].Width = 100;
                dgvData.Columns["Type"].Width = 80;
                dgvData.Columns["Method"].Width = 120;
                dgvData.Columns["Amount"].Width = 150;

                // TODO: Load data from database
                // Sample data
                dgvData.Rows.Add("GD001", "24/03/2026", "Thu", "Tiền mặt", "5,000,000", "Khách hàng thanh toán hợp đồng A");
                dgvData.Rows.Add("GD002", "24/03/2026", "Chi", "Ngân hàng", "2,000,000", "Thanh toán tiền điện nước tháng 3");
                dgvData.Rows.Add("GD003", "23/03/2026", "Thu", "Ngân hàng", "15,000,000", "Nhận tiền đầu tư");
                dgvData.Rows.Add("GD004", "22/03/2026", "Chi", "Tiền mặt", "500,000", "Mua văn phòng phẩm");

                // Cập nhật các con số Tổng quan (Sample numbers)
                // Trong thực tế bạn sẽ query SUM() từ Database để fill vào đây
                UpdateSummaryValues(4500000, 13000000);
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
            string searchText = txtSearch.Text.ToLower();

            // Ví dụ Lọc DataGridView (Cần có CurrencyManager hỗ trợ hoặc dùng DataView nếu bind qua DataTable)
            // foreach (DataGridViewRow row in dgvData.Rows)
            // {
            //      if (row.Cells["Description"].Value != null)
            //      {
            //          row.Visible = row.Cells["Description"].Value.ToString().ToLower().Contains(searchText);
            //      }
            // }
        }

        private void BtnAddThu_Click(object sender, EventArgs e)
        {
            // TODO: Mở Form Thêm Phiếu Thu
            MessageBox.Show("Chức năng thêm Phiếu Thu đang được xây dựng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddChi_Click(object sender, EventArgs e)
        {
            // TODO: Mở Form Thêm Phiếu Chi
            MessageBox.Show("Chức năng thêm Phiếu Chi đang được xây dựng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ============================================
        // HELPER METHODS
        // ============================================

        /// <summary>
        /// Cập nhật hiển thị số liệu Tổng quan Sổ quỹ
        /// </summary>
        /// <param name="tienMat">Tổng tiền mặt</param>
        /// <param name="nganHang">Tổng tiền ngân hàng</param>
        private void UpdateSummaryValues(decimal tienMat, decimal nganHang)
        {
            decimal tongSoDu = tienMat + nganHang;

            // Format tiền tệ VNĐ
            lblTienMatValue.Text = tienMat.ToString("N0") + " đ";
            lblNganHangValue.Text = nganHang.ToString("N0") + " đ";
            lblSoDuValue.Text = tongSoDu.ToString("N0") + " đ";
        }

        private void ClearForm()
        {
            txtSearch.Clear();
        }

        public void RefreshData()
        {
            LoadData();
        }
    }
}