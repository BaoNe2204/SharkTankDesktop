using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Z80NavBar;
using SharkTank.Modules.HR.UI.Forms;

namespace SharkTank.Modules.HR
{
    public class HRModule : IModule
    {
        private static int _menuId = 2000;

        public string ModuleName => "HR";

        public UserControl GetView(string viewName)
        {
            return HRViewRouter.GetView(viewName);
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

            int profileId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = profileId,
                Text = "🧑‍💼 Hồ sơ nhân viên",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Thêm / sửa / xóa nhân viên", ParentID = profileId },
                    new NavBarItem { ID = _menuId++, Text = "Thông tin cá nhân", ParentID = profileId },
                    new NavBarItem { ID = _menuId++, Text = "Ảnh đại diện", ParentID = profileId },
                    new NavBarItem { ID = _menuId++, Text = "CCCD / hộ chiếu", ParentID = profileId },
                    new NavBarItem { ID = _menuId++, Text = "Thông tin liên hệ", ParentID = profileId }
                }
            });

            int deptId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = deptId,
                Text = "🏢 Phòng ban & chức vụ",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Danh sách phòng ban", ParentID = deptId },
                    new NavBarItem { ID = _menuId++, Text = "Sơ đồ tổ chức", ParentID = deptId },
                    new NavBarItem { ID = _menuId++, Text = "Chức danh", ParentID = deptId },
                    new NavBarItem { ID = _menuId++, Text = "Điều chuyển nhân sự", ParentID = deptId }
                }
            });

            int contractId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = contractId,
                Text = "📄 Hợp đồng lao động",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Tạo hợp đồng", ParentID = contractId },
                    new NavBarItem { ID = _menuId++, Text = "Gia hạn / chấm dứt", ParentID = contractId },
                    new NavBarItem { ID = _menuId++, Text = "Lịch sử hợp đồng", ParentID = contractId }
                }
            });

            int attendanceId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = attendanceId,
                Text = "⏰ Chấm công",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Check-in / Check-out", ParentID = attendanceId },
                    new NavBarItem { ID = _menuId++, Text = "Bảng công theo tháng", ParentID = attendanceId },
                    new NavBarItem { ID = _menuId++, Text = "Nghỉ phép", ParentID = attendanceId },
                    new NavBarItem { ID = _menuId++, Text = "Làm thêm giờ", ParentID = attendanceId }
                }
            });

            int salaryId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = salaryId,
                Text = "💵 Tính lương",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Lương cơ bản", ParentID = salaryId },
                    new NavBarItem { ID = _menuId++, Text = "Phụ cấp", ParentID = salaryId },
                    new NavBarItem { ID = _menuId++, Text = "Khấu trừ", ParentID = salaryId },
                    new NavBarItem { ID = _menuId++, Text = "Thưởng", ParentID = salaryId },
                    new NavBarItem { ID = _menuId++, Text = "Bảng lương", ParentID = salaryId },
                    new NavBarItem { ID = _menuId++, Text = "Phiếu lương", ParentID = salaryId }
                }
            });

            int rewardId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = rewardId,
                Text = "🎖️ Khen thưởng & kỷ luật",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Quyết định thưởng", ParentID = rewardId },
                    new NavBarItem { ID = _menuId++, Text = "Vi phạm / cảnh cáo", ParentID = rewardId },
                    new NavBarItem { ID = _menuId++, Text = "Lịch sử", ParentID = rewardId }
                }
            });

            return menuItems;
        }
    }
}
