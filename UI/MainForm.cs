using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.UI.Controls;

namespace SharkTank.UI
{
    public class MainForm : Form
    {
        private readonly SessionService _sessionService;
        private readonly PermissionService _permissionService;

        private Label _lblUser;
        private Label _lblRole;

        private Button _btnLogout;
        private Panel _sidebar;

        // Sidebar items theo module để ẩn/hiện theo quyền
        private Control _itemHR;
        private Control _itemSales;
        private Control _itemInventory;
        private Control _itemAccounting;
        private Control _itemAdmin;

        private readonly List<HRGroupItem> _hrGroups = new List<HRGroupItem>();

        public bool RequestLogout { get; private set; }

        public MainForm(SessionService sessionService, PermissionService permissionService)
        {
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));

            InitializeComponents();
            Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var user = _sessionService.CurrentUser;
            var fullName = string.IsNullOrWhiteSpace(user?.FullName) ? user?.Username : user.FullName;
            _lblUser.Text = $"User: {fullName}";
            _lblRole.Text = $"Role: {user?.Role?.RoleName ?? user?.RoleId.ToString() ?? "Unknown"}";

            // Nếu là HR thì dùng sidebar chi tiết HR, ngược lại dùng sidebar module chung
            if (string.Equals(user?.Role?.RoleName, "HR", StringComparison.OrdinalIgnoreCase))
            {
                BuildHrSidebar();
            }
            else
            {
                BuildModuleSidebarItems();
                ApplyPermissions();
            }
        }

        private void ApplyPermissions()
        {
            if (_itemHR != null) _itemHR.Visible = _permissionService.Has("HR.VIEW");
            if (_itemSales != null) _itemSales.Visible = _permissionService.Has("SALES.VIEW");
            if (_itemInventory != null) _itemInventory.Visible = _permissionService.Has("INVENTORY.VIEW");
            if (_itemAccounting != null) _itemAccounting.Visible = _permissionService.Has("ACCOUNTING.VIEW");
            if (_itemAdmin != null) _itemAdmin.Visible = _permissionService.Has("ADMIN.VIEW");
        }

        private void InitializeComponents()
        {
            Text = "SharkTank ERP - Main";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1100, 650);

            CreateSidebarShell();

            var panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White
            };

            _lblUser = new Label { AutoSize = true, Location = new Point(20, 15), Font = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold) };
            _lblRole = new Label { AutoSize = true, Location = new Point(20, 40), ForeColor = Color.DimGray };
            panelTop.Controls.Add(_lblUser);
            panelTop.Controls.Add(_lblRole);

            _btnLogout = new Button
            {
                Text = "Đăng xuất",
                Height = 36,
                Dock = DockStyle.Bottom,
                Width = 150,
                Margin = new Padding(15, 10, 15, 10)
            };
            _btnLogout.Click += (s, e) =>
            {
                _sessionService.EndSession();
                RequestLogout = true;
                Close();
            };

            // Logout sẽ được thêm vào sidebar sau cùng trong BuildModuleSidebarItems/BuildHrSidebar

            // Main dashboard area: table layout 3 columns x 2 rows như mẫu
            var panelMain = new Panel { Dock = DockStyle.Fill, Padding = new Padding(16), BackColor = Color.FromArgb(250, 250, 250) };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 2
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150)); // hàng trên: thẻ thống kê
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  // hàng dưới: bảng / chart

            layout.Controls.Add(CreateStatCard("Nhân sự", "250 staff"), 0, 0);
            layout.Controls.Add(CreateStatCard("Đơn bán", "100 orders"), 1, 0);
            layout.Controls.Add(CreateStatCard("Kho", "10 kho"), 2, 0);

            layout.Controls.Add(CreateSectionBox("Memo / Staff list"), 0, 1);
            layout.Controls.Add(CreateSectionBox("Payment vouchers"), 1, 1);
            layout.Controls.Add(CreateSectionBox("Applications / Chart"), 2, 1);

            panelMain.Controls.Add(layout);

            Controls.Add(panelMain);
            Controls.Add(panelTop);
            Controls.Add(_sidebar);
        }

        private Control CreateStatCard(string title, string value)
        {
            var panel = new Panel
            {
                Margin = new Padding(8),
                BackColor = Color.White,
                Dock = DockStyle.Fill
            };

            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(Pens.Gainsboro, rect);
            };

            var lblTitle = new Label
            {
                Text = title,
                AutoSize = true,
                Location = new Point(12, 12),
                ForeColor = Color.DimGray
            };
            var lblValue = new Label
            {
                Text = value,
                AutoSize = true,
                Location = new Point(12, 45),
                Font = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Bold)
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblValue);
            return panel;
        }

        private Control CreateSectionBox(string title)
        {
            var gb = new GroupBox
            {
                Text = title,
                Dock = DockStyle.Fill,
                Margin = new Padding(8)
            };
            return gb;
        }

        private void CreateSidebarShell()
        {
            _sidebar = new Panel
            {
                Width = 190,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(250, 251, 253),
                Padding = new Padding(0, 10, 0, 0)
            };

            Controls.Add(_sidebar);
        }

        private void BuildModuleSidebarItems()
        {
            _sidebar.Controls.Clear();

            // Tạo item theo thứ tự từ dưới lên vì DockStyle.Top stack từ trên xuống
            AddSidebarItem("Admin", () => MessageBox.Show("Admin module", "Module"));
            AddSidebarItem("Accounting", () => MessageBox.Show("Accounting module", "Module"));
            AddSidebarItem("Inventory", () => MessageBox.Show("Inventory module", "Module"));
            AddSidebarItem("Sales", () => MessageBox.Show("Sales module", "Module"));
            AddSidebarItem("HR", () => MessageBox.Show("HR module", "Module"));

            _sidebar.Controls.Add(_btnLogout);
            _btnLogout.BringToFront();
        }

        private void AddSidebarItem(string text, Action onClick)
        {
            var item = new ModernSidebarItem(text);
            item.Click += (_, __) => onClick();
            foreach (Control c in item.Controls)
            {
                c.Click += (_, __) => onClick();
            }
            _sidebar.Controls.Add(item);

            // mapping cho RBAC ẩn/hiện
            if (text == "HR") _itemHR = item;
            if (text == "Sales") _itemSales = item;
            if (text == "Inventory") _itemInventory = item;
            if (text == "Accounting") _itemAccounting = item;
            if (text == "Admin") _itemAdmin = item;
        }

        private void BuildHrSidebar()
        {
            _sidebar.Controls.Clear();
            _hrGroups.Clear();

            var header = new Label
            {
                Text = "👤 HR — NHÂN SỰ",
                Height = 50,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0)
            };
            _sidebar.Controls.Add(header);

            // Tạo group HR theo đúng cấu trúc
            var hoSo = new HRGroupItem("🧾 Hồ sơ nhân viên");
            hoSo.AddSubItem("Thêm / sửa / xóa nhân viên");
            hoSo.AddSubItem("Thông tin cá nhân");
            hoSo.AddSubItem("Ảnh đại diện");
            hoSo.AddSubItem("CCCD / hộ chiếu");
            hoSo.AddSubItem("Thông tin liên hệ");

            var phongBan = new HRGroupItem("🏢 Phòng ban & chức vụ");
            phongBan.AddSubItem("Danh sách phòng ban");
            phongBan.AddSubItem("Sơ đồ tổ chức");
            phongBan.AddSubItem("Chức danh");
            phongBan.AddSubItem("Điều chuyển nhân sự");

            var hopDong = new HRGroupItem("📄 Hợp đồng lao động");
            hopDong.AddSubItem("Tạo hợp đồng");
            hopDong.AddSubItem("Gia hạn / chấm dứt");
            hopDong.AddSubItem("Lịch sử hợp đồng");

            var chamCong = new HRGroupItem("⏰ Chấm công");
            chamCong.AddSubItem("Check-in / Check-out");
            chamCong.AddSubItem("Bảng công theo tháng");
            chamCong.AddSubItem("Nghỉ phép");
            chamCong.AddSubItem("Làm thêm giờ");

            var tinhLuong = new HRGroupItem("💵 Tính lương");
            tinhLuong.AddSubItem("Lương cơ bản");
            tinhLuong.AddSubItem("Phụ cấp");
            tinhLuong.AddSubItem("Khấu trừ");
            tinhLuong.AddSubItem("Thưởng");
            tinhLuong.AddSubItem("Bảng lương");
            tinhLuong.AddSubItem("Phiếu lương");

            var khenThuong = new HRGroupItem("🎖️ Khen thưởng & kỷ luật");
            khenThuong.AddSubItem("Quyết định thưởng");
            khenThuong.AddSubItem("Vi phạm / cảnh cáo");
            khenThuong.AddSubItem("Lịch sử");

            AddHrGroup(khenThuong);
            AddHrGroup(tinhLuong);
            AddHrGroup(chamCong);
            AddHrGroup(hopDong);
            AddHrGroup(phongBan);
            AddHrGroup(hoSo);

            _sidebar.Controls.Add(_btnLogout);
            _btnLogout.BringToFront();
        }

        private void AddHrGroup(HRGroupItem group)
        {
            group.Expanded += HrGroup_Expanded;
            _hrGroups.Add(group);
            _sidebar.Controls.Add(group);
        }

        private void HrGroup_Expanded(object sender, EventArgs e)
        {
            foreach (var g in _hrGroups)
            {
                if (!ReferenceEquals(g, sender))
                {
                    g.Collapse();
                }
            }
        }
    }
}

