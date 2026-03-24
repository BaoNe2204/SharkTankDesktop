using System;
using System.Drawing;
using System.Windows.Forms;

// ============================================
// MODULE: Debt (Công Nợ)
// VIEW: CongNoView (Quản lý Công Nợ Tổng Hợp)
// ============================================

namespace SharkTank.Modules.Debt.Views
{
    /// <summary>
    /// View quản lý Công Nợ gồm 3 Tab: Khách hàng, Nhà cung cấp, Lịch sử thanh toán
    /// </summary>
    public class CongNoView : UserControl
    {
        // ============================================
        // KHAI BÁO CONTROLS
        // ============================================
        private Label lblTitle;
        private Label lblDescription;
        private TextBox txtSearch;
        private Button btnThanhToan;

        // TabControl và các TabPages
        private TabControl tabMain;
        private TabPage tabKhachHang;
        private TabPage tabNhaCungCap;
        private TabPage tabLichSu;

        // DataGridViews cho từng Tab
        private DataGridView dgvKhachHang;
        private DataGridView dgvNhaCungCap;
        private DataGridView dgvLichSu;

        // ============================================
        // CONSTRUCTOR
        // ============================================
        public CongNoView()
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
                Text = "QUẢN LÝ CÔNG NỢ",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 215),
                Location = new Point(30, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // ========== DESCRIPTION ==========
            lblDescription = new Label
            {
                Text = "Theo dõi công nợ Khách hàng, Nhà cung cấp và Lịch sử thanh toán",
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
                Size = new Size(300, 27),
                Font = new Font("Segoe UI", 10)
            };

            // ========== TAB CONTROL ==========
            tabMain = new TabControl
            {
                Location = new Point(30, 150),
                Size = new Size(900, 450),
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(tabMain);

            // TẠO CÁC TAB PAGES
            tabKhachHang = new TabPage("👤 Công nợ Khách hàng") { BackColor = Color.White };
            tabNhaCungCap = new TabPage("🏢 Công nợ Nhà cung cấp") { BackColor = Color.White };
            tabLichSu = new TabPage("🕒 Lịch sử thanh toán") { BackColor = Color.White };

            tabMain.TabPages.Add(tabKhachHang);
            tabMain.TabPages.Add(tabNhaCungCap);
            tabMain.TabPages.Add(tabLichSu);

            // ========== DATA GRID VIEWS ==========
            dgvKhachHang = CreateStandardGrid();
            tabKhachHang.Controls.Add(dgvKhachHang);

            dgvNhaCungCap = CreateStandardGrid();
            tabNhaCungCap.Controls.Add(dgvNhaCungCap);

            dgvLichSu = CreateStandardGrid();
            tabLichSu.Controls.Add(dgvLichSu);
            
            // Bắt sự kiện chuyển tab để cập nhật UI nếu cần
            tabMain.SelectedIndexChanged += TabMain_SelectedIndexChanged;
        }

        // Tạo DataGridView chuẩn để dùng chung
        private DataGridView CreateStandardGrid()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10)
            };
        }

        // ============================================
        // LOAD DATA
        // ============================================
        private void LoadData()
        {
            try
            {
                SetupColumns();
                LoadSampleData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupColumns()
        {
            // Cột Tab Khách Hàng
            dgvKhachHang.Columns.Clear();
            dgvKhachHang.Columns.Add("ID", "Mã KH");
            dgvKhachHang.Columns.Add("Name", "Tên Khách Hàng");
            dgvKhachHang.Columns.Add("Phone", "SĐT");
            dgvKhachHang.Columns.Add("TotalDebt", "Tổng Nợ");
            dgvKhachHang.Columns.Add("Paid", "Đã Trả");
            dgvKhachHang.Columns.Add("Remain", "Còn Lại");

            // Cột Tab Nhà Cung Cấp
            dgvNhaCungCap.Columns.Clear();
            dgvNhaCungCap.Columns.Add("ID", "Mã NCC");
            dgvNhaCungCap.Columns.Add("Name", "Tên Nhà Cung Cấp");
            dgvNhaCungCap.Columns.Add("Phone", "SĐT");
            dgvNhaCungCap.Columns.Add("TotalDebt", "Tổng Nợ");
            dgvNhaCungCap.Columns.Add("Paid", "Đã Trả");
            dgvNhaCungCap.Columns.Add("Remain", "Còn Lại");

            // Cột Tab Lịch Sử Thanh Toán
            dgvLichSu.Columns.Clear();
            dgvLichSu.Columns.Add("ID", "Mã GD");
            dgvLichSu.Columns.Add("Date", "Ngày");
            dgvLichSu.Columns.Add("Type", "Đối Tượng"); // KH hay NCC
            dgvLichSu.Columns.Add("Name", "Tên Đối Tượng");
            dgvLichSu.Columns.Add("Amount", "Số Tiền");
            dgvLichSu.Columns.Add("Note", "Ghi Chú");
        }

        private void LoadSampleData()
        {
            // Dữ liệu mẫu Khách hàng
            dgvKhachHang.Rows.Add("KH001", "Nguyễn Văn A", "0901234567", "10,000,000", "5,000,000", "5,000,000");
            dgvKhachHang.Rows.Add("KH002", "Trần Thị B", "0912345678", "2,000,000", "0", "2,000,000");

            // Dữ liệu mẫu Nhà cung cấp
            dgvNhaCungCap.Rows.Add("NCC001", "Công ty CP Giao Hàng", "0283333444", "15,000,000", "10,000,000", "5,000,000");
            dgvNhaCungCap.Rows.Add("NCC002", "Xưởng In Ấn Bao Bì", "0287777888", "8,000,000", "8,000,000", "0");

            // Dữ liệu mẫu Lịch sử
            dgvLichSu.Rows.Add("PT001", "24/03/2026", "Khách hàng", "Nguyễn Văn A", "5,000,000", "Thanh toán đợt 1");
            dgvLichSu.Rows.Add("PC001", "22/03/2026", "Nhà cung cấp", "Công ty CP Giao Hàng", "10,000,000", "Chuyển khoản thanh toán");
        }

        // ============================================
        // EVENT HANDLERS
        // ============================================
        
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            // TODO: Lọc dữ liệu trên DataGridView của Tab đang được chọn (tabMain.SelectedTab)
        }

        private void TabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ẩn hiện nút "Ghi nhận thanh toán" tùy theo Tab
            // Ví dụ: Ở tab Lịch sử thanh toán thì không cần nút Ghi nhận thanh toán
            if (tabMain.SelectedIndex == 2) // Tab Lịch sử
            {
                btnThanhToan.Visible = false;
            }
            else
            {
                btnThanhToan.Visible = true;
            }
            
            // Xóa text tìm kiếm khi chuyển tab
            txtSearch.Clear();
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            string doiTuong = tabMain.SelectedIndex == 0 ? "Khách hàng" : "Nhà cung cấp";
            
            // TODO: Mở form popup ghi nhận thanh toán cho Đối tượng tương ứng
            MessageBox.Show($"Mở form thanh toán cho: {doiTuong}", "Tính năng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void RefreshData()
        {
            LoadData();
        }
    }
}