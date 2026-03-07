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
            mnuProfile.AutoSize = false;
            mnuSettings.AutoSize = false;
            mnuLogout.AutoSize = false;

            mnuProfile.Width = panelUserRight.Width;
            mnuSettings.Width = panelUserRight.Width;
            mnuLogout.Width = panelUserRight.Width;
            CreateNavigationControl();
        }

        private void CreateNavigationControl()
        {
            // z80Navigation1 đã được tạo trong Designer, chỉ cần initialize
            // z80Navigation1 = new Z80_NavBar.Z80_Navigation
            // {
            //     BackColor = Color.FromArgb(35, 40, 45),
            //     BorderStyle = BorderStyle.FixedSingle,
            //     Location = new Point(4, 80),
            //     Name = "z80Navigation1",
            //     Size = new Size(250, 574),
            //     TabIndex = 3
            // };
            // Controls.Add(z80Navigation1);
            // Controls.SetChildIndex(z80Navigation1, 3);

            // Đã được Designer thêm vào, chỉ cần set properties nếu cần
            z80Navigation1.BackColor = Color.FromArgb(35, 40, 45);
            z80Navigation1.BorderStyle = BorderStyle.FixedSingle;
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
            // Hiển thị thông tin user trên PanelTop
            var user = _sessionService.CurrentUser;
            var fullName = string.IsNullOrWhiteSpace(user?.FullName) ? user?.Username : user.FullName;
            lblDate.Text = "Today is " + GetOrdinalDateString(DateTime.Now);
            lblWelcome.Text = $"Welcome, Mr. {fullName} 👋";
            lblUserName.Text = fullName;
            lblUserRole.Text = user?.Role?.RoleName ?? "User";

            // Avatar tròn
            MakePictureBoxRound(picAvatar);

            // Cập nhật title với tên user
            this.Text = $"SharkTank ERP - {fullName}";

            // Thêm logo và nút logout vào panelLogo
            SetupLogoPanel();

            // Khởi tạo ViewManager
            _viewManager = new ViewManager(panelContent);

            // Initialize Z80 Navigation
            InitializeNavigation();
        }

        private static string GetOrdinalDateString(DateTime d)
        {
            int day = d.Day;
            string suffix = (day % 10 == 1 && day != 11) ? "st" : (day % 10 == 2 && day != 12) ? "nd" : (day % 10 == 3 && day != 13) ? "rd" : "th";
            return d.ToString("dddd") + ", " + day + suffix + " " + d.ToString("MMMM yyyy");
        }

        private void MakePictureBoxRound(PictureBox pic)
        {
            if (pic.Width <= 0 || pic.Height <= 0) return;
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, pic.Width, pic.Height);
                pic.Region = new Region(path);
            }
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

        private void panelUserRight_Click(object sender, EventArgs e)
        {
            contextMenuUser.AutoSize = false;
            contextMenuUser.Width = panelUserRight.Width;

            contextMenuUser.Show(panelUserRight, new Point(0, panelUserRight.Height));
        }

        private void mnuProfile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Profile - Coming soon.", "Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings - Coming soon.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuLogout_Click(object sender, EventArgs e)
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
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {
            // Vẽ border hoặc custom paint cho panelContent nếu cần
        }

        private void picBell_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = picBell.ClientRectangle;
            if (r.Width <= 0 || r.Height <= 0) return;
            using (var pen = new Pen(Color.Black, 1.5f))
            using (var brush = new SolidBrush(Color.Black))
            {
                int w = r.Width;
                int h = r.Height;
                int cx = w / 2;
                int top = 2;
                int bottom = h - 4;
                // Bell body: arc + two lines
                g.DrawArc(pen, 2, top, w - 4, (h - 4) * 3 / 4, 0, 180);
                g.DrawLine(pen, 4, top + (h - 4) * 3 / 8, 4, bottom - 2);
                g.DrawLine(pen, w - 4, top + (h - 4) * 3 / 8, w - 4, bottom - 2);
                g.DrawEllipse(pen, cx - 2, bottom - 3, 4, 4);
            }
        }

        private void picChevron_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = picChevron.ClientRectangle;
            if (r.Width <= 0 || r.Height <= 0) return;
            int cx = r.Width / 2;
            int top = 4;
            int bottom = r.Height - 4;
            int left = 4;
            int right = r.Width - 4;
            using (var brush = new SolidBrush(Color.Gray))
            {
                var pts = new[] { new Point(cx, bottom), new Point(left, top), new Point(right, top) };
                g.FillPolygon(brush, pts);
            }
        }

        private void PanelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
