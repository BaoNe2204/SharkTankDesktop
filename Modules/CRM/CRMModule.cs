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
                    new NavBarItem { ID = _menuId++, Text = "Nhân viên phụ trách", ParentID = leadsId }
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

            int pipelineId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = pipelineId,
                Text = "Pipeline bán hàng",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Khách tiềm năng", ParentID = pipelineId },
                    new NavBarItem { ID = _menuId++, Text = "Đang tư vấn", ParentID = pipelineId },
                    new NavBarItem { ID = _menuId++, Text = "Đang báo giá", ParentID = pipelineId },
                    new NavBarItem { ID = _menuId++, Text = "Đang đàm phán", ParentID = pipelineId },
                    new NavBarItem { ID = _menuId++, Text = "Đã chốt / thất bại", ParentID = pipelineId }
                }
            });

            int activityId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = activityId,
                Text = "Hoạt động & lịch làm việc",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Nhắc lịch chăm sóc", ParentID = activityId },
                    new NavBarItem { ID = _menuId++, Text = "Nhắc gọi khách", ParentID = activityId },
                    new NavBarItem { ID = _menuId++, Text = "Nhắc gửi báo giá", ParentID = activityId },
                    new NavBarItem { ID = _menuId++, Text = "Quản lý công việc sales", ParentID = activityId }
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
                    new NavBarItem { ID = _menuId++, Text = "Hiệu quả nhân viên sales", ParentID = crmReportId },
                    new NavBarItem { ID = _menuId++, Text = "Nguồn khách hàng hiệu quả", ParentID = crmReportId },
                    new NavBarItem { ID = _menuId++, Text = "Số lượng lead theo thời gian", ParentID = crmReportId }
                }
            });

            return menuItems;
        }
    }
}
