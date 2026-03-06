using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Z80NavBar;

namespace SharkTank.Modules
{
    /// <summary>
    /// Delegate xử lý khi click vào menu item
    /// </summary>
    /// <param name="menuText">Tên menu item</param>
    /// <returns>True nếu đã xử lý, False để dùng default</returns>
    public delegate bool MenuItemHandler(string menuText);

    /// <summary>
    /// Helper class để đăng ký handler cho menu items nhanh gọn
    /// </summary>
    public class MenuHandlers
    {
        private readonly Dictionary<string, MenuItemHandler> _handlers = new Dictionary<string, MenuItemHandler>(StringComparer.OrdinalIgnoreCase);
        private readonly MenuItemHandler _defaultHandler;

        public MenuHandlers(MenuItemHandler defaultHandler = null)
        {
            _defaultHandler = defaultHandler;
        }

        /// <summary>
        /// Đăng ký handler cho 1 menu item
        /// </summary>
        public MenuHandlers Register(string menuText, MenuItemHandler handler)
        {
            _handlers[menuText] = handler;
            return this; // cho phép chain: Register("A",...).Register("B",...)
        }

        /// <summary>
        /// Đăng ký handler cho nhiều menu items cùng xử lý
        /// </summary>
        public MenuHandlers RegisterMultiple(string[] menuTexts, MenuItemHandler handler)
        {
            foreach (var text in menuTexts)
            {
                _handlers[text] = handler;
            }
            return this;
        }

        /// <summary>
        /// Xử lý khi click menu - gọi handler tương ứng
        /// </summary>
        public bool Handle(string menuText)
        {
            if (_handlers.TryGetValue(menuText, out var handler) && handler != null)
            {
                return handler(menuText);
            }
            return _defaultHandler?.Invoke(menuText) ?? false;
        }

        /// <summary>
        /// Kiểm tra có handler cho menu này không
        /// </summary>
        public bool HasHandler(string menuText)
        {
            return _handlers.ContainsKey(menuText);
        }
    }

    /// <summary>
    /// Base class cho Module để có sẵn hệ thống xử lý menu
    /// </summary>
    public abstract class ModuleBase : IModule
    {
        protected MenuHandlers Handlers { get; private set; }

        protected virtual void InitMenuHandlers()
        {
            // Override trong module con để đăng ký handlers
        }

        public List<NavBarItem> GetMenuItems()
        {
            Handlers = new MenuHandlers();
            InitMenuHandlers();
            return GetModuleMenuItems();
        }

        /// <summary>
        /// Trả về menu items - override trong module con
        /// </summary>
        protected abstract List<NavBarItem> GetModuleMenuItems();

        /// <summary>
        /// Xử lý khi click menu item - gọi handler hoặc trả về view mặc định
        /// </summary>
        public virtual bool OnMenuItemClick(string menuText)
        {
            return Handlers?.Handle(menuText) ?? false;
        }

        // IModule members
        public abstract string ModuleName { get; }
        public abstract UserControl GetView(string viewName);
        public abstract List<string> GetAvailableViews();
    }
}
