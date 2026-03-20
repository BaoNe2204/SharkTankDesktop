using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Z80NavBar;
using Z80NavBar.Themes;
using SharkTank.BLL;
using System.Linq;
using SharkTank.Core.Models;
using SharkTank.DAL.Sql;

namespace SharkTank
{
    public partial class MainDashboard : Form
    {
        private ViewManager _viewManager;
        private string _currentModule = "";
        private readonly SessionService _sessionService;
        private readonly PermissionService _permissionService;
        private Timer _sessionMonitorTimer;
        private bool _isHandlingForcedLogout;
        private NotificationService _notificationService;
        private List<SystemNotification> _notifications = new List<SystemNotification>();
        private Form _notificationsPopup;

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
            var user = _sessionService.CurrentUser;
            var fullName = string.IsNullOrWhiteSpace(user?.FullName) ? user?.Username : user.FullName;
            lblDate.Text = "Today is " + GetOrdinalDateString(DateTime.Now);
            lblWelcome.Text = $"Welcome, Mr. {fullName} 👋";
            lblUserName.Text = fullName;
            lblUserRole.Text = user?.Role?.RoleName ?? "User";

            MakePictureBoxRound(picAvatar);
            this.Text = $"SharkTank ERP - {fullName}";

            SetupLogoPanel();
            _viewManager = new ViewManager(panelContent);
            InitializeNavigation();
            StartSessionMonitor();
            LoadNotifications();

        }
        private void LoadNotifications()
        {
            var user = _sessionService.CurrentUser;
            if (user == null) return;

            _notifications = _notificationService
                .GetForUser(user.Role?.RoleName, user.Username)
                .ToList();

            UpdateNotificationBell();
        }

        private void UpdateNotificationBell()
        {
            int count = _notifications.Count;
            lblNotificationCount.Text = count > 9 ? "9+" : count.ToString();
            lblNotificationCount.Visible = count > 0;
        }

        private void picBell_Click(object sender, EventArgs e)
        {
            ShowNotificationsMenu();
        }

        private void ShowNotificationsMenu()
        {
            if (IsNotificationsPopupVisible())
            {
                HideNotificationsPopup();
                return;
            }

            LoadNotifications();
            if (_notifications == null) _notifications = new List<SystemNotification>();

            int popupWidth = 340;
            int popupHeight = 420;

            var bellBottomLeft = picBell.PointToScreen(new Point(0, picBell.Height));
            var bellCenter = picBell.PointToScreen(new Point(picBell.Width / 2, picBell.Height / 2));

            int x = bellCenter.X - popupWidth / 2;
            int y = bellBottomLeft.Y + 10; 

            var wa = Screen.FromControl(picBell).WorkingArea;
            if (x < wa.Left) x = wa.Left;
            if (x + popupWidth > wa.Right) x = wa.Right - popupWidth;
            if (y < wa.Top) y = wa.Top;
            if (y + popupHeight > wa.Bottom) y = wa.Bottom - popupHeight;

            _notificationsPopup = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(popupWidth, popupHeight),
                BackColor = Color.White,
                TopMost = true
            };

            _notificationsPopup.Deactivate += (s, e) => HideNotificationsPopup();
            _notificationsPopup.FormClosed += (s, e) =>
            {
                if (ReferenceEquals(_notificationsPopup, s as Form)) _notificationsPopup = null;
            };

            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            var header = new Label
            {
                Text = "Thông báo",
                AutoSize = false,
                Height = 42,
                Width = popupWidth,
                Padding = new Padding(14, 10, 0, 0),
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new System.Drawing.Point(0, 0),
                BackColor = Color.White
            };

            var headerLine = new Panel
            {
                Height = 1,
                Width = popupWidth,
                Location = new System.Drawing.Point(0, header.Height),
                BackColor = Color.FromArgb(230, 230, 230)
            };

            var listPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(10, 8, 10, 10),
                Location = new System.Drawing.Point(0, header.Height + headerLine.Height),
                Size = new System.Drawing.Size(popupWidth, popupHeight - header.Height - headerLine.Height)
            };

            if (_notifications.Count == 0)
            {
                listPanel.Controls.Add(new Label
                {
                    Text = "Không có thông báo nào",
                    Dock = DockStyle.Top,
                    AutoSize = false,
                    Width = popupWidth - 30,
                    Height = 60,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                    ForeColor = Color.Gray,
                    Font = new System.Drawing.Font("Segoe UI", 9.5F)
                });
            }
            else
            {
                foreach (var item in _notifications)
                {
                    listPanel.Controls.Add(CreateNotificationCard(item));
                }
            }

            mainPanel.Controls.Add(header);
            mainPanel.Controls.Add(headerLine);
            mainPanel.Controls.Add(listPanel);
            _notificationsPopup.Controls.Add(mainPanel);

            _notificationsPopup.Show(this);
        }

        private void HideNotificationsPopup()
        {
            try
            {
                if (_notificationsPopup != null && !_notificationsPopup.IsDisposed)
                    _notificationsPopup.Close();
            }
            catch
            {
            }
            finally
            {
                _notificationsPopup = null;
            }
        }

        private bool IsNotificationsPopupVisible()
        {
            return _notificationsPopup != null && !_notificationsPopup.IsDisposed && _notificationsPopup.Visible;
        }

        private Control CreateNotificationCard(SystemNotification item)
        {
            Color typeColor = GetNotificationTypeColor(item?.Type);

            var card = new Panel
            {
                Width = 320,
                Height = 70,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 10),
                Cursor = Cursors.Hand
            };

            var iconCircle = new Panel
            {
                Width = 34,
                Height = 34,
                Left = 12,
                Top = 18,
                BackColor = typeColor
            };
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, iconCircle.Width, iconCircle.Height);
                iconCircle.Region = new Region(path);
            }

            var pic = new PictureBox
            {
                Size = new System.Drawing.Size(16, 16),
                Left = (iconCircle.Width - 16) / 2,
                Top = (iconCircle.Height - 16) / 2,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = global::SharkTank.Properties.Resources.bell_solid,
                BackColor = Color.Transparent
            };

            iconCircle.Controls.Add(pic);

            var lblTitle = new Label
            {
                AutoEllipsis = true,
                Left = 54,
                Top = 15,
                Width = 250,
                Height = 24,
                Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold),
                ForeColor = Color.Black,
                Text = item?.Title ?? ""
            };

            var lblTime = new Label
            {
                Left = 54,
                Top = 40,
                Width = 250,
                Height = 18,
                Font = new System.Drawing.Font("Segoe UI", 8.5F),
                ForeColor = Color.Gray,
                Text = GetRelativeTimeText(item?.CreatedAt ?? DateTime.Now)
            };

            void open()
            {
                if (item == null) return;
                HideNotificationsPopup();
                MessageBox.Show(
                    item.Content ?? "",
                    item.Title ?? "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            card.Click += (s, e) => open();
            iconCircle.Click += (s, e) => open();
            pic.Click += (s, e) => open();
            lblTitle.Click += (s, e) => open();
            lblTime.Click += (s, e) => open();

            card.Controls.Add(iconCircle);
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblTime);

            return card;
        }

        private static Color GetNotificationTypeColor(string type)
        {
            var t = (type ?? "").Trim().ToLowerInvariant();

            if (t == "error") return Color.FromArgb(245, 87, 87);    // đỏ
            if (t == "warning") return Color.FromArgb(255, 177, 50); // cam/vàng
            return Color.FromArgb(70, 136, 255);                     // xanh cho Info/khác
        }

        private static string GetRelativeTimeText(DateTime createdAt)
        {
            var now = DateTime.Now;
            if (createdAt > now) createdAt = now;

            var diff = now - createdAt;
            if (diff.TotalMinutes < 1) return "vừa xong";
            if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} phút trước";
            if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} giờ trước";

            return $"{(int)diff.TotalDays} ngày trước";
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
            Label lblLogo = new Label
            {
                Text = "🦈 SharkTank",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 5),
                AutoSize = true
            };
            panelLogo.Controls.Add(lblLogo);

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
            List<NavBarItem> navItems = MenuConfig.GetAllMenuItems(_permissionService);
            ThemeSelector themeSelector = new ThemeSelector(Theme.Dark);
            z80Navigation1.Initialize(navItems, themeSelector.CurrentTheme);
            z80Navigation1.SelectedItem += Z80Navigation1_SelectedItem;
        }

        private void StartSessionMonitor()
        {
            if (_sessionService.CurrentSession == null) return;

            _sessionMonitorTimer = new Timer { Interval = 5000 };
            _sessionMonitorTimer.Tick += SessionMonitorTimer_Tick;
            _sessionMonitorTimer.Start();
        }

        private void StopSessionMonitor()
        {
            if (_sessionMonitorTimer == null) return;

            _sessionMonitorTimer.Stop();
            _sessionMonitorTimer.Tick -= SessionMonitorTimer_Tick;
            _sessionMonitorTimer.Dispose();
            _sessionMonitorTimer = null;
        }

        private void SessionMonitorTimer_Tick(object sender, EventArgs e)
        {
            if (_isHandlingForcedLogout) return;

            bool isActive = _sessionService.IsCurrentSessionActive();
            if (isActive) return;

            _isHandlingForcedLogout = true;
            StopSessionMonitor();

            MessageBox.Show(
                "Phiên đăng nhập của bạn đã bị quản trị viên đăng xuất.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            _sessionService.EndSession();
            RequestLogout = true;
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            StopSessionMonitor();
            base.OnFormClosed(e);
        }

        private void Z80Navigation1_SelectedItem(NavBarItem item)
        {
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
                    StopSessionMonitor();
                    _sessionService.EndSession();
                    RequestLogout = true;
                    this.Close();
                }
                return;
            }

            string moduleName = GetModuleNameFromMenuItem(item);

            if (!string.IsNullOrEmpty(moduleName))
            {
                _currentModule = moduleName;
                _viewManager.SetCurrentModule(_currentModule);
            }

            _viewManager.ShowView(item.Text, _currentModule);

            var user = _sessionService.CurrentUser;
            var fullName = string.IsNullOrWhiteSpace(user?.FullName) ? user?.Username : user.FullName;
            this.Text = $"SharkTank ERP - {fullName} - {_currentModule} - {item.Text}";
        }

        private string GetModuleNameFromMenuItem(NavBarItem item)
        {
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

            return _currentModule;
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
                StopSessionMonitor();
                _sessionService.EndSession();
                RequestLogout = true;
                this.Close();
            }
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {
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

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }
    }
}
