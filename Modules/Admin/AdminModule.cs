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

            // 🔐 Người dùng & phân quyền
            int securityId = _menuId++;
            var securityMenu = new NavBarItem
            {
                ID = securityId,
                Text = "Người dùng & phân quyền",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Quản lý tài khoản", ParentID = securityId },
                    new NavBarItem { ID = _menuId++, Text = "Phân quyền Role", ParentID = securityId },
                    new NavBarItem { ID = _menuId++, Text = "Phân quyền chi tiết", ParentID = securityId },
                    new NavBarItem { ID = _menuId++, Text = "Quản lý phiên đăng nhập", ParentID = securityId }
                }
            };

            // 🏢 Cơ cấu tổ chức
            int orgId = _menuId++;
            var orgMenu = new NavBarItem
            {
                ID = orgId,
                Text = "Cơ cấu tổ chức",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Phòng ban", ParentID = orgId },
                    new NavBarItem { ID = _menuId++, Text = "Chức vụ", ParentID = orgId }
                }
            };

            // ⚙️ Cấu hình hệ thống
            int configId = _menuId++;
            var configMenu = new NavBarItem
            {
                ID = configId,
                Text = "Cấu hình hệ thống",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Thông tin công ty", ParentID = configId },
                    new NavBarItem { ID = _menuId++, Text = "Cấu hình hệ thống", ParentID = configId },
                    new NavBarItem { ID = _menuId++, Text = "Cấu hình email", ParentID = configId },
                    new NavBarItem { ID = _menuId++, Text = "Tiền tệ / định dạng", ParentID = configId }
                }
            };

            // 🔔 Thông báo hệ thống
            int notifyId = _menuId++;
            var notifyMenu = new NavBarItem
            {
                ID = notifyId,
                Text = "Thông báo hệ thống",
                ParentID = null
            };

            // 📊 Dashboard
            int dashboardId = _menuId++;
            var dashboardMenu = new NavBarItem
            {
                ID = dashboardId,
                Text = "Dashboard hệ thống",
                ParentID = null
            };

            // 📝 Nhật ký hệ thống
            int auditId = _menuId++;
            var auditMenu = new NavBarItem
            {
                ID = auditId,
                Text = "Nhật ký hệ thống",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Lịch sử đăng nhập", ParentID = auditId },
                    new NavBarItem { ID = _menuId++, Text = "Lịch sử thao tác", ParentID = auditId },
                    new NavBarItem { ID = _menuId++, Text = "Theo dõi thay đổi dữ liệu", ParentID = auditId }
                }
            };

            // 💾 Backup & Restore
            int backupId = _menuId++;
            var backupMenu = new NavBarItem
            {
                ID = backupId,
                Text = "Backup & Restore",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Sao lưu dữ liệu", ParentID = backupId },
                    new NavBarItem { ID = _menuId++, Text = "Khôi phục dữ liệu", ParentID = backupId }
                }
            };


            menuItems.Add(securityMenu);
            menuItems.Add(orgMenu);
            menuItems.Add(configMenu);
            menuItems.Add(notifyMenu);
            menuItems.Add(dashboardMenu);
            menuItems.Add(auditMenu);
            menuItems.Add(backupMenu);

            return menuItems;
        }
    }
}