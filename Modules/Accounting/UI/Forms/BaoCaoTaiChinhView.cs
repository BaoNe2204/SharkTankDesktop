using System;
using System.Drawing;
using System.Windows.Forms;

// ============================================
// MODULE: Reports (Báo Cáo)
// VIEW: BaoCaoTaiChinhView (Trung tâm Báo cáo)
// ============================================

namespace SharkTank.Modules.Reports.Views
{
    public class BaoCaoTaiChinhView : UserControl
    {
        // ============================================
        // KHAI BÁO CONTROLS
        // ============================================
        private Label lblTitle;

        // Bộ lọc (Filter Panel)
        private Panel pnlFilter;
        private Label lblThoiGian;
        private ComboBox cboThoiGian;
        private Label lblTuNgay;
        private DateTimePicker dtpTuNgay;
        private Label lblDenNgay;
        private DateTimePicker dtpDenNgay;
        private Button btnXemBaoCao;
        private Button btnXuatExcel;

        // TabControl & TabPages
        private TabControl tabMain;
        private TabPage tabThuChi;
        private TabPage tabLaiLo;
        private TabPage tabSoCai;
        private TabPage tabTongHop;

        // Controls trong các Tab
        private DataGridView dgvThuChi;
        private DataGridView dgvSoCai;

        // Controls cho Tab Lãi Lỗ
            private Label lblTongThu;
           private Label lblTongChi;
        private Label lblLoiNhuan;

        // ============================================
        // CONSTRUCTOR
        // ============================================
        public BaoCaoTaiChinhView()
        {
            InitializeComponent();
            SetupDefaultDates();
            LoadData();
        }

        // ============================================
        // INITIALIZE COMPONENTS
        // ============================================
        private void InitializeComponent()
        {
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(20);

            // ========== TITLE ==========
            lblTitle = new Label
            {
                Text = "📊 TRUNG TÂM BÁO CÁO TÀI CHÍNH",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 215),
                Location = new Point(30, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // ========== FILTER PANEL ==========
            pnlFilter = new Panel
            {
                Location = new Point(30, 70),
                Size = new Size(900, 60),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblThoiGian = new Label { Text = "Kỳ báo cáo:", Location = new Point(15, 20), AutoSize = true, Font = new Font("Segoe UI", 10) };
            cboThoiGian = new ComboBox { Location = new Point(95, 17), Size = new Size(120, 25), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cboThoiGian.Items.AddRange(new string[] { "Tháng này", "Tháng trước", "Quý này", "Năm nay", "Tùy chọn" });
            cboThoiGian.SelectedIndex = 0;
            cboThoiGian.SelectedIndexChanged += CboThoiGian_SelectedIndexChanged;

            lblTuNgay = new Label { Text = "Từ ngày:", Location = new Point(230, 20), AutoSize = true, Font = new Font("Segoe UI", 10) };
            dtpTuNgay = new DateTimePicker { Location = new Point(295, 17), Size = new Size(110, 25), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10) };

            lblDenNgay = new Label { Text = "Đến ngày:", Location = new Point(420, 20), AutoSize = true, Font = new Font("Segoe UI", 10) };
            dtpDenNgay = new DateTimePicker { Location = new Point(495, 17), Size = new Size(110, 25), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10) };

            btnXemBaoCao = new Button
            {
                Text = "🔍 Xem Báo Cáo",
                Location = new Point(620, 15),
                Size = new Size(130, 30),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnXemBaoCao.FlatAppearance.BorderSize = 0;
            btnXemBaoCao.Click += BtnXemBaoCao_Click;

            btnXuatExcel = new Button
            {
                Text = "📥 Xuất Excel",
                Location = new Point(760, 15),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnXuatExcel.FlatAppearance.BorderSize = 0;
            btnXuatExcel.Click += BtnXuatExcel_Click;

            pnlFilter.Controls.Add(lblThoiGian); pnlFilter.Controls.Add(cboThoiGian);
            pnlFilter.Controls.Add(lblTuNgay); pnlFilter.Controls.Add(dtpTuNgay);
            pnlFilter.Controls.Add(lblDenNgay); pnlFilter.Controls.Add(dtpDenNgay);
            pnlFilter.Controls.Add(btnXemBaoCao); pnlFilter.Controls.Add(btnXuatExcel);
            this.Controls.Add(pnlFilter);

            // ========== TAB CONTROL ==========
            tabMain = new TabControl
            {
                Location = new Point(30, 150),
                Size = new Size(900, 450),
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(tabMain);

            // --- TAB 1: BÁO CÁO THU CHI ---
            tabThuChi = new TabPage("📈 Báo cáo Thu Chi") { BackColor = Color.White };
            dgvThuChi = CreateStandardGrid();
            tabThuChi.Controls.Add(dgvThuChi);
            tabMain.TabPages.Add(tabThuChi);

            // --- TAB 2: LÃI LỖ ---
            tabLaiLo = new TabPage("⚖️ Kết quả Lãi Lỗ") { BackColor = Color.White };
            InitializeLaiLoTab();
            tabMain.TabPages.Add(tabLaiLo);

            // --- TAB 3: SỔ CÁI ĐƠN GIẢN ---
            tabSoCai = new TabPage("📓 Sổ Cái") { BackColor = Color.White };
            dgvSoCai = CreateStandardGrid();
            tabSoCai.Controls.Add(dgvSoCai);
            tabMain.TabPages.Add(tabSoCai);

            // --- TAB 4: TỔNG HỢP TÀI CHÍNH ---
            tabTongHop = new TabPage("🚀 Tổng hợp (Dashboard)") { BackColor = Color.White };
            InitializeDashboardTab();
            tabMain.TabPages.Add(tabTongHop);
        }

        // ============================================
        // SETUP GIAO DIỆN CON
        // ============================================
        private DataGridView CreateStandardGrid()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false
            };
        }

        private void InitializeLaiLoTab()
        {
            // P&L Layout đơn giản
            Label title = new Label { Text = "BÁO CÁO KẾT QUẢ HOẠT ĐỘNG KINH DOANH", Font = new Font("Segoe UI", 16, FontStyle.Bold), Location = new Point(220, 30), AutoSize = true };

            Panel pnlDoanhThu = CreateKPICard("Tổng Doanh Thu (Thu)", "150,000,000 đ", Color.FromArgb(40, 167, 69), new Point(150, 100));
            Panel pnlChiPhi = CreateKPICard("Tổng Chi Phí (Chi)", "90,000,000 đ", Color.FromArgb(220, 53, 69), new Point(500, 100));

            Label line = new Label { Text = "--------------------------------------------------------------------------------", Location = new Point(150, 240), AutoSize = true, ForeColor = Color.LightGray };

            Label lblLoiNhuanTitle = new Label { Text = "LỢI NHUẬN THUẦN:", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(250, 280), AutoSize = true };
            lblLoiNhuan = new Label { Text = "+ 60,000,000 đ", Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = Color.FromArgb(0, 120, 215), Location = new Point(500, 275), AutoSize = true };

            tabLaiLo.Controls.Add(title);
            tabLaiLo.Controls.Add(pnlDoanhThu);
            tabLaiLo.Controls.Add(pnlChiPhi);
            tabLaiLo.Controls.Add(line);
            tabLaiLo.Controls.Add(lblLoiNhuanTitle);
            tabLaiLo.Controls.Add(lblLoiNhuan);
        }

        private void InitializeDashboardTab()
        {
            // Các thẻ chỉ số (Cards)
            tabTongHop.Controls.Add(CreateKPICard("Quỹ Tiền Mặt", "4,500,000 đ", Color.DimGray, new Point(50, 50)));
            tabTongHop.Controls.Add(CreateKPICard("Quỹ Ngân Hàng", "13,000,000 đ", Color.DimGray, new Point(480, 50)));
            tabTongHop.Controls.Add(CreateKPICard("Nợ Phải Thu (Khách hàng)", "25,000,000 đ", Color.FromArgb(0, 120, 215), new Point(50, 220)));
            tabTongHop.Controls.Add(CreateKPICard("Nợ Phải Trả (Nhà cung cấp)", "15,000,000 đ", Color.FromArgb(220, 53, 69), new Point(480, 220)));
        }

        private Panel CreateKPICard(string title, string value, Color valueColor, Point location)
        {
            Panel pnl = new Panel { Location = location, Size = new Size(350, 120), BackColor = Color.FromArgb(248, 249, 250), BorderStyle = BorderStyle.FixedSingle };
            Label lblTitle = new Label { Text = title, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(20, 20), AutoSize = true };
            Label lblValue = new Label { Text = value, Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = valueColor, Location = new Point(20, 60), AutoSize = true };
            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(lblValue);
            return pnl;
        }

        // ============================================
        // LOGIC & EVENTS
        // ============================================
        private void SetupDefaultDates()
        {
            // Mặc định chọn Tháng này
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);
        }

        private void CboThoiGian_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            switch (cboThoiGian.SelectedItem.ToString())
            {
                case "Tháng này":
                    dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
                    dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);
                    break;
                case "Tháng trước":
                    DateTime lastMonth = now.AddMonths(-1);
                    dtpTuNgay.Value = new DateTime(lastMonth.Year, lastMonth.Month, 1);
                    dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);
                    break;
                case "Năm nay":
                    dtpTuNgay.Value = new DateTime(now.Year, 1, 1);
                    dtpDenNgay.Value = new DateTime(now.Year, 12, 31);
                    break;
            }
        }

        private void BtnXemBaoCao_Click(object sender, EventArgs e)
        {
            // Cập nhật lại dữ liệu dựa trên khoảng thời gian mới
            LoadData();
            MessageBox.Show($"Đã tải dữ liệu từ {dtpTuNgay.Value.ToShortDateString()} đến {dtpDenNgay.Value.ToShortDateString()}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            // Gợi ý: Dùng EPPlus hoặc ClosedXML để xuất dgv đang Active
            string currentTab = tabMain.SelectedTab.Text;
            MessageBox.Show($"Chức năng xuất Excel cho [{currentTab}] đang được xây dựng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadData()
        {
            // Thiết lập cột cho dgvThuChi
            dgvThuChi.Columns.Clear();
            dgvThuChi.Columns.Add("Category", "Hạng mục");
            dgvThuChi.Columns.Add("Type", "Loại");
            dgvThuChi.Columns.Add("Amount", "Tổng tiền");
            dgvThuChi.Rows.Add("Bán hàng", "Thu", "120,000,000");
            dgvThuChi.Rows.Add("Dịch vụ", "Thu", "30,000,000");
            dgvThuChi.Rows.Add("Lương nhân viên", "Chi", "50,000,000");

            // Thiết lập cột cho dgvSoCai
            dgvSoCai.Columns.Clear();
            dgvSoCai.Columns.Add("Date", "Ngày");
            dgvSoCai.Columns.Add("RefID", "Số CT");
            dgvSoCai.Columns.Add("Desc", "Diễn giải");
            dgvSoCai.Columns.Add("In", "Thu");
            dgvSoCai.Columns.Add("Out", "Chi");
            dgvSoCai.Columns.Add("Balance", "Tồn Quỹ");
            dgvSoCai.Rows.Add("01/03/2026", "SDDK", "Số dư đầu kỳ", "", "", "10,000,000");
            dgvSoCai.Rows.Add("05/03/2026", "PT01", "Bán hàng", "5,000,000", "", "15,000,000");
        }
    }
}