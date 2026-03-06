using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Z80NavBar;
using SharkTank.Modules.Admin.UI.Forms;

namespace SharkTank.Modules.Admin
{
    public class AdminModule : IModule
    {
        private static int _menuId = 1000;

        public string ModuleName => "Admin";

        public UserControl GetView(string viewName)
        {
            return AdminViewRouter.GetView(viewName);
        }

        public List<string> GetAvailableViews()
        {
            return new List<string>();
        }

        public bool OnMenuItemClick(string menuText)
        {
            return false;
        }

        public List<NavBarItem> GetMenuItems()
        {
            var menuItems = new List<NavBarItem>();

            int securityId = _menuId++;
            var securityMenu = new NavBarItem
            {
                ID = securityId,
                Text = "🔐 Quản lý người dùng & bảo mật",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Đăng nhập / đăng xuất", ParentID = securityId },
                    new NavBarItem { ID = _menuId++, Text = "Đổi mật khẩu / quên mật khẩu", ParentID = securityId },
                    new NavBarItem { ID = _menuId++, Text = "Xác thực tài khoản", ParentID = securityId },
                    new NavBarItem { ID = _menuId++, Text = "Khóa / mở khóa tài khoản", ParentID = securityId },
                    new NavBarItem { ID = _menuId++, Text = "Phân quyền theo role", ParentID = securityId },
                    new NavBarItem { ID = _menuId++, Text = "Phân quyền chi tiết theo chức năng", ParentID = securityId },
                    new NavBarItem { ID = _menuId++, Text = "Quản lý phiên đăng nhập", ParentID = securityId }
                }
            };

            int accountId = _menuId++;
            var accountMenu = new NavBarItem
            {
                ID = accountId,
                Text = "👥 Quản lý tài khoản",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Tạo / sửa / xóa người dùng", ParentID = accountId },
                    new NavBarItem { ID = _menuId++, Text = "Gán phòng ban", ParentID = accountId },
                    new NavBarItem { ID = _menuId++, Text = "Gán vai trò (HR, Sales, Kho, Kế toán…)", ParentID = accountId },
                    new NavBarItem { ID = _menuId++, Text = "Reset mật khẩu", ParentID = accountId },
                    new NavBarItem { ID = _menuId++, Text = "Kích hoạt / vô hiệu hóa", ParentID = accountId }
                }
            };

            int configId = _menuId++;
            var configMenu = new NavBarItem
            {
                ID = configId,
                Text = "⚙️ Cấu hình hệ thống",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Thông tin công ty", ParentID = configId },
                    new NavBarItem { ID = _menuId++, Text = "Logo, tên hệ thống", ParentID = configId },
                    new NavBarItem { ID = _menuId++, Text = "Cấu hình tiền tệ", ParentID = configId },
                    new NavBarItem { ID = _menuId++, Text = "Định dạng ngày giờ", ParentID = configId },
                    new NavBarItem { ID = _menuId++, Text = "Cấu hình email (nếu có)", ParentID = configId },
                    new NavBarItem { ID = _menuId++, Text = "Thiết lập tham số hệ thống", ParentID = configId }
                }
            };

            int dashboardId = _menuId++;
            var dashboardMenu = new NavBarItem
            {
                ID = dashboardId,
                Text = "Dashboard tổng",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Tổng doanh thu", ParentID = dashboardId },
                    new NavBarItem { ID = _menuId++, Text = "Số nhân viên", ParentID = dashboardId },
                    new NavBarItem { ID = _menuId++, Text = "Tồn kho", ParentID = dashboardId },
                    new NavBarItem { ID = _menuId++, Text = "Công nợ", ParentID = dashboardId },
                    new NavBarItem { ID = _menuId++, Text = "Hoạt động gần đây", ParentID = dashboardId }
                }
            };

            int auditId = _menuId++;
            var auditMenu = new NavBarItem
            {
                ID = auditId,
                Text = "Nhật ký hệ thống (Audit Log)",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Lịch sử đăng nhập", ParentID = auditId },
                    new NavBarItem { ID = _menuId++, Text = "Lịch sử thao tác", ParentID = auditId },
                    new NavBarItem { ID = _menuId++, Text = "Theo dõi thay đổi dữ liệu", ParentID = auditId },
                    new NavBarItem { ID = _menuId++, Text = "Phát hiện truy cập bất thường", ParentID = auditId }
                }
            };

            int backupId = _menuId++;
            var backupMenu = new NavBarItem
            {
                ID = backupId,
                Text = "Backup & Restore",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Sao lưu dữ liệu", ParentID = backupId },
                    new NavBarItem { ID = _menuId++, Text = "Khôi phục dữ liệu", ParentID = backupId },
                    new NavBarItem { ID = _menuId++, Text = "Xuất / nhập database", ParentID = backupId }
                }
            };

            menuItems.Add(securityMenu);
            menuItems.Add(accountMenu);
            menuItems.Add(configMenu);
            menuItems.Add(dashboardMenu);
            menuItems.Add(auditMenu);
            menuItems.Add(backupMenu);

            return menuItems;
        }
    }
}
