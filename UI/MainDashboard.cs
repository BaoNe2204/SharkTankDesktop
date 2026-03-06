using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Z80NavBar;
using Z80NavBar.Themes;
using SharkTank.BLL;

namespace SharkTank
{
    public partial class MainDashboard : Form
    {
        private ViewManager _viewManager;
        private string _currentModule = "";
        private readonly SessionService _sessionService;
        private readonly PermissionService _permissionService;
        public bool RequestLogout { get; private set; }

        public MainDashboard(SessionService sessionService, PermissionService permissionService)
        {
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            InitializeComponent();
        }

        private void RoundPanel(Panel panel, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            panel.Region = new Region(path);
        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {
            RoundPanel(panelSearch, 20);
            
            // Hiển thị thông tin user
            var user = _sessionService.CurrentUser;
            var fullName = string.IsNullOrWhiteSpace(user?.FullName) ? user?.Username : user.FullName;
            lblWelcome.Text = $"Welcome to SharkTank ERP";
            lblDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            
            // Cập nhật title với tên user
            this.Text = $"SharkTank ERP - {fullName}";
            
            // Thêm logo và nút logout vào panelLogo
            SetupLogoPanel();
            
            // Khởi tạo ViewManager
            _viewManager = new ViewManager(panelContent);
            
            // Initialize Z80 Navigation
            InitializeNavigation();
        }

        private void SetupLogoPanel()
        {
            // Logo text
            Label lblLogo = new Label
            {
                Text = "🦈 SharkTank",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 5),
                AutoSize = true
            };
            panelLogo.Controls.Add(lblLogo);

            // User info
            var user = _sessionService.CurrentUser;
            var fullName = string.IsNullOrWhiteSpace(user?.FullName) ? user?.Username : user.FullName;
            
            Label lblUser = new Label
            {
                Text = fullName,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 200, 255),
                Location = new Point(15, 35),
                AutoSize = true
            };
            panelLogo.Controls.Add(lblUser);

            // Role
            Label lblRole = new Label
            {
                Text = user?.Role?.RoleName ?? "User",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.LightGray,
                Location = new Point(15, 52),
                AutoSize = true
            };
            panelLogo.Controls.Add(lblRole);
        }

        private void InitializeNavigation()
        {
            // Lấy tất cả menu items từ MenuConfig với permission filtering
            List<NavBarItem> navItems = MenuConfig.GetAllMenuItems(_permissionService);

            // Chọn theme (Dark hoặc Blue)
            ThemeSelector themeSelector = new ThemeSelector(Theme.Dark);

            // Initialize navigation control
            z80Navigation1.Initialize(navItems, themeSelector.CurrentTheme);

            // Subscribe to SelectedItem event
            z80Navigation1.SelectedItem += Z80Navigation1_SelectedItem;
        }

        private void Z80Navigation1_SelectedItem(NavBarItem item)
        {
            // Xử lý logout
            if (item.Text == "🚪 Đăng xuất")
            {
                var result = MessageBox.Show(
                    "Bạn có chắc chắn muốn đăng xuất?",
                    "Xác nhận đăng xuất",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    _sessionService.EndSession();
                    RequestLogout = true;
                    this.Close();
                }
                return;
            }

            // Xác định module dựa trên menu item
            string moduleName = GetModuleNameFromMenuItem(item);
            
            if (!string.IsNullOrEmpty(moduleName))
            {
                _currentModule = moduleName;
                _viewManager.SetCurrentModule(_currentModule);
            }

            // Hiển thị view tương ứng trong panelContent
            _viewManager.ShowView(item.Text, _currentModule);
            
            // Cập nhật title của form
            var user = _sessionService.CurrentUser;
            var fullName = string.IsNullOrWhiteSpace(user?.FullName) ? user?.Username : user.FullName;
            this.Text = $"SharkTank ERP - {fullName} - {_currentModule} - {item.Text}";
        }

        /// <summary>
        /// Xác định module name dựa trên menu item ID range
        /// </summary>
        private string GetModuleNameFromMenuItem(NavBarItem item)
        {
            // Mỗi module có ID range riêng:
            // Admin: 1000-1999
            // HR: 2000-2999
            // Accounting: 3000-3999
            // Sales: 4000-4999
            // Inventory: 5000-5999
            // CRM: 6000-6999
            
            if (item.ID >= 1000 && item.ID < 2000)
                return "Admin";
            else if (item.ID >= 2000 && item.ID < 3000)
                return "HR";
            else if (item.ID >= 3000 && item.ID < 4000)
                return "Accounting";
            else if (item.ID >= 4000 && item.ID < 5000)
                return "Sales";
            else if (item.ID >= 5000 && item.ID < 6000)
                return "Inventory";
            else if (item.ID >= 6000 && item.ID < 7000)
                return "CRM";
            
            return _currentModule; // Giữ nguyên module hiện tại nếu không xác định được
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {
            // Vẽ border hoặc custom paint cho panelContent nếu cần
        }
    }
}
