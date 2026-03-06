using System.Collections.Generic;
using Z80NavBar;
using SharkTank.BLL;
using SharkTank.Modules;
using SharkTank.Modules.Admin;
using SharkTank.Modules.HR;
using SharkTank.Modules.Sales;
using SharkTank.Modules.Inventory;
using SharkTank.Modules.Accounting;
using SharkTank.Modules.CRM;

namespace SharkTank
{
    /// <summary>
    /// Cấu hình menu cho toàn bộ ứng dụng
    /// Mỗi module tự quản lý menu của mình qua GetMenuItems()
    /// </summary>
    public static class MenuConfig
    {
        private static int _currentId = 1000;

        private static AdminModule _adminModule;
        private static HRModule _hrModule;
        private static SalesModule _salesModule;
        private static InventoryModule _inventoryModule;
        private static AccountingModule _accountingModule;
        private static CRMModule _crmModule;

        public static List<NavBarItem> GetAllMenuItems(PermissionService permissionService = null)
        {
            List<NavBarItem> allItems = new List<NavBarItem>();

            _adminModule = _adminModule ?? new AdminModule();
            _hrModule = _hrModule ?? new HRModule();
            _salesModule = _salesModule ?? new SalesModule();
            _inventoryModule = _inventoryModule ?? new InventoryModule();
            _accountingModule = _accountingModule ?? new AccountingModule();
            _crmModule = _crmModule ?? new CRMModule();

            if (permissionService == null || permissionService.Has("ADMIN.VIEW"))
            {
                _currentId = 1000;
                allItems.AddRange(_adminModule.GetMenuItems());
            }

            if (permissionService == null || permissionService.Has("HR.VIEW"))
            {
                _currentId = 2000;
                allItems.AddRange(_hrModule.GetMenuItems());
            }

            if (permissionService == null || permissionService.Has("SALES.VIEW"))
            {
                _currentId = 4000;
                allItems.AddRange(_salesModule.GetMenuItems());
            }

            if (permissionService == null || permissionService.Has("INVENTORY.VIEW"))
            {
                _currentId = 5000;
                allItems.AddRange(_inventoryModule.GetMenuItems());
            }

            if (permissionService == null || permissionService.Has("ACCOUNTING.VIEW"))
            {
                _currentId = 3000;
                allItems.AddRange(_accountingModule.GetMenuItems());
            }

            if (permissionService == null || permissionService.Has("CRM.VIEW"))
            {
                _currentId = 6000;
                allItems.AddRange(_crmModule.GetMenuItems());
            }

            _currentId = 9999;
            allItems.Add(GetLogoutMenu());

            return allItems;
        }

        public static IModule GetModule(string moduleName)
        {
            switch (moduleName)
            {
                case "Admin": return _adminModule ?? (_adminModule = new AdminModule());
                case "HR": return _hrModule ?? (_hrModule = new HRModule());
                case "Sales": return _salesModule ?? (_salesModule = new SalesModule());
                case "Inventory": return _inventoryModule ?? (_inventoryModule = new InventoryModule());
                case "Accounting": return _accountingModule ?? (_accountingModule = new AccountingModule());
                case "CRM": return _crmModule ?? (_crmModule = new CRMModule());
                default: return null;
            }
        }

        private static NavBarItem GetLogoutMenu()
        {
            return new NavBarItem
            {
                ID = _currentId++,
                Text = "🚪 Đăng xuất",
                ParentID = null
            };
        }
    }
}
