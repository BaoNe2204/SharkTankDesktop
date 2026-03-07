using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Z80NavBar;
using SharkTank.Modules.Sales.UI.Forms;

namespace SharkTank.Modules.Sales
{
    public class SalesModule : IModule
    {
        private static int _menuId = 4000;

        public string ModuleName => "Sales";

        public UserControl GetView(string viewName)
        {
            return SalesViewRouter.GetView(viewName);
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

            int customerId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = customerId,
                Text = "👥 Quản lý khách hàng",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Thêm / sửa / xóa khách hàng", ParentID = customerId },
                    new NavBarItem { ID = _menuId++, Text = "Phân loại khách hàng", ParentID = customerId },
                    new NavBarItem { ID = _menuId++, Text = "Lịch sử mua hàng", ParentID = customerId },
                    new NavBarItem { ID = _menuId++, Text = "Công nợ khách", ParentID = customerId }
                }
            });

            int quoteId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = quoteId,
                Text = "📑 Báo giá",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Tạo báo giá", ParentID = quoteId },
                    new NavBarItem { ID = _menuId++, Text = "Sửa / duyệt báo giá", ParentID = quoteId },
                    new NavBarItem { ID = _menuId++, Text = "Gửi cho khách", ParentID = quoteId }
                }
            });

            int orderId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = orderId,
                Text = "🛒 Đơn bán hàng",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Tạo đơn hàng", ParentID = orderId },
                    new NavBarItem { ID = _menuId++, Text = "Trạng thái đơn", ParentID = orderId },
                    new NavBarItem { ID = _menuId++, Text = "Chi tiết sản phẩm", ParentID = orderId },
                    new NavBarItem { ID = _menuId++, Text = "Chiết khấu", ParentID = orderId },
                    new NavBarItem { ID = _menuId++, Text = "Thuế", ParentID = orderId }
                }
            });

            int invoiceId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = invoiceId,
                Text = "🧾 Hóa đơn bán",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Xuất hóa đơn", ParentID = invoiceId },
                    new NavBarItem { ID = _menuId++, Text = "In / PDF", ParentID = invoiceId },
                    new NavBarItem { ID = _menuId++, Text = "Gửi khách hàng", ParentID = invoiceId }
                }
            });

            int revenueId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = revenueId,
                Text = "💰 Theo dõi doanh thu",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Doanh thu theo ngày/tháng", ParentID = revenueId },
                    new NavBarItem { ID = _menuId++, Text = "Theo nhân viên", ParentID = revenueId },
                    new NavBarItem { ID = _menuId++, Text = "Theo sản phẩm", ParentID = revenueId }
                }
            });

            int stockLinkId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = stockLinkId,
                Text = "📦 Liên kết kho",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Kiểm tra tồn trước khi bán", ParentID = stockLinkId },
                    new NavBarItem { ID = _menuId++, Text = "Tạo yêu cầu xuất kho", ParentID = stockLinkId }
                }
            });

            return menuItems;
        }
    }
}
