using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Z80NavBar;
using SharkTank.Modules.Inventory.UI.Forms;

namespace SharkTank.Modules.Inventory
{
    public class InventoryModule : IModule
    {
        private static int _menuId = 5000;

        public string ModuleName => "Inventory";

        public UserControl GetView(string viewName)
        {
            return InventoryViewRouter.GetView(viewName);
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

            int catalogId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = catalogId,
                Text = "📋 Danh mục sản phẩm",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Thêm / sửa / xóa sản phẩm", ParentID = catalogId },
                    new NavBarItem { ID = _menuId++, Text = "Mã sản phẩm", ParentID = catalogId },
                    new NavBarItem { ID = _menuId++, Text = "Nhóm hàng", ParentID = catalogId },
                    new NavBarItem { ID = _menuId++, Text = "Đơn vị tính", ParentID = catalogId },
                    new NavBarItem { ID = _menuId++, Text = "Giá nhập / bán", ParentID = catalogId }
                }
            });

            int importId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = importId,
                Text = "📥 Nhập kho",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Phiếu nhập", ParentID = importId },
                    new NavBarItem { ID = _menuId++, Text = "Nhà cung cấp", ParentID = importId },
                    new NavBarItem { ID = _menuId++, Text = "Giá nhập", ParentID = importId },
                    new NavBarItem { ID = _menuId++, Text = "Số lượng", ParentID = importId }
                }
            });

            int exportId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = exportId,
                Text = "📤 Xuất kho",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Phiếu xuất", ParentID = exportId },
                    new NavBarItem { ID = _menuId++, Text = "Xuất cho bán hàng", ParentID = exportId },
                    new NavBarItem { ID = _menuId++, Text = "Xuất nội bộ", ParentID = exportId }
                }
            });

            int stockId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = stockId,
                Text = "📊 Tồn kho",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Tồn theo sản phẩm", ParentID = stockId },
                    new NavBarItem { ID = _menuId++, Text = "Tồn theo kho", ParentID = stockId },
                    new NavBarItem { ID = _menuId++, Text = "Cảnh báo hết hàng", ParentID = stockId }
                }
            });

            int checkId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = checkId,
                Text = "🔍 Kiểm kê",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Kiểm kê định kỳ", ParentID = checkId },
                    new NavBarItem { ID = _menuId++, Text = "Điều chỉnh tồn", ParentID = checkId },
                    new NavBarItem { ID = _menuId++, Text = "Biên bản kiểm kê", ParentID = checkId }
                }
            });

            int warehouseId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = warehouseId,
                Text = "🏬 Quản lý kho",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Nhiều kho", ParentID = warehouseId },
                    new NavBarItem { ID = _menuId++, Text = "Vị trí lưu trữ", ParentID = warehouseId }
                }
            });

            return menuItems;
        }
    }
}
