using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Z80NavBar;
using SharkTank.Modules.Accounting.UI.Forms;

namespace SharkTank.Modules.Accounting
{
    public class AccountingModule : IModule
    {
        private static int _menuId = 3000;

        public string ModuleName => "Accounting";

        public UserControl GetView(string viewName)
        {
            return AccountingViewRouter.GetView(viewName);
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

            int paymentId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = paymentId,
                Text = "Thu / chi",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Phiếu thu", ParentID = paymentId },
                    new NavBarItem { ID = _menuId++, Text = "Phiếu chi", ParentID = paymentId },
                    new NavBarItem { ID = _menuId++, Text = "Nguồn tiền", ParentID = paymentId }
                }
            });

            int cashbookId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = cashbookId,
                Text = "Sổ quỹ",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Tiền mặt", ParentID = cashbookId },
                    new NavBarItem { ID = _menuId++, Text = "Ngân hàng", ParentID = cashbookId },
                    new NavBarItem { ID = _menuId++, Text = "Số dư", ParentID = cashbookId }
                }
            });

            int debtId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = debtId,
                Text = "Công nợ",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Công nợ khách hàng", ParentID = debtId },
                    new NavBarItem { ID = _menuId++, Text = "Công nợ nhà cung cấp", ParentID = debtId },
                    new NavBarItem { ID = _menuId++, Text = "Lịch sử thanh toán", ParentID = debtId }
                }
            });

            int invoiceAccId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = invoiceAccId,
                Text = "Hạch toán hóa đơn",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Nhận dữ liệu từ Sales", ParentID = invoiceAccId },
                    new NavBarItem { ID = _menuId++, Text = "Ghi nhận doanh thu", ParentID = invoiceAccId },
                    new NavBarItem { ID = _menuId++, Text = "Ghi nhận chi phí", ParentID = invoiceAccId }
                }
            });

            int reportId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = reportId,
                Text = "Báo cáo tài chính",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Báo cáo thu chi", ParentID = reportId },
                    new NavBarItem { ID = _menuId++, Text = "Lãi lỗ", ParentID = reportId },
                    new NavBarItem { ID = _menuId++, Text = "Sổ cái đơn giản", ParentID = reportId },
                    new NavBarItem { ID = _menuId++, Text = "Tổng hợp tài chính", ParentID = reportId }
                }
            });

            return menuItems;
        }
    }
}
