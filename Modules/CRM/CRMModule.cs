using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Z80NavBar;
using SharkTank.Modules.CRM.UI.Forms;

namespace SharkTank.Modules.CRM
{
    public class CRMModule : IModule
    {
        private static int _menuId = 6000;

        public string ModuleName => "CRM";

        public UserControl GetView(string viewName)
        {
            return CRMViewRouter.GetView(viewName);
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

            int leadsId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = leadsId,
                Text = "Quản lý khách hàng tiềm năng",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Quản lý Leads", ParentID = leadsId },
                    
                }
            });

            int careId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = careId,
                Text = "Chăm sóc khách hàng",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Chăm sóc khách hàng", ParentID = careId },
                    
                }
            });

            int oppId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = oppId,
                Text = "Quản lý cơ hội bán hàng",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Quản lý cơ hội bán hàng", ParentID = oppId },
                   
                }
            });


            int crmReportId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = crmReportId,
                Text = "Báo cáo CRM",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Tỷ lệ chuyển đổi khách hàng", ParentID = crmReportId },
                    new NavBarItem { ID = _menuId++, Text = "Nguồn khách hàng hiệu quả", ParentID = crmReportId },
                    new NavBarItem { ID = _menuId++, Text = "Số lượng lead theo thời gian", ParentID = crmReportId }
                }
            });

            return menuItems;
        }
    }
}
