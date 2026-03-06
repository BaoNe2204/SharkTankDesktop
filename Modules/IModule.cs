using System.Collections.Generic;
using System.Windows.Forms;
using Z80NavBar;

namespace SharkTank.Modules
{
    /// <summary>
    /// Interface cho các module
    /// Mỗi bộ phận implement interface này
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Tên module
        /// </summary>
        string ModuleName { get; }

        /// <summary>
        /// Lấy view tương ứng với tên menu
        /// </summary>
        UserControl GetView(string viewName);

        /// <summary>
        /// Danh sách các view có trong module
        /// </summary>
        List<string> GetAvailableViews();

        /// <summary>
        /// Lấy menu items của module này
        /// Mỗi module tự định nghĩa menu của mình
        /// </summary>
        List<NavBarItem> GetMenuItems();

        /// <summary>
        /// Xử lý khi click menu item
        /// Trả về True nếu đã xử lý, False để dùng view mặc định
        /// </summary>
        bool OnMenuItemClick(string menuText);
    }
}
