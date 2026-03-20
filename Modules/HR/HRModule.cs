using System.Collections.Generic;
using System.Windows.Forms;
using Z80NavBar;

namespace SharkTank.Modules.HR
{
    public class HRModule : IModule
    {
        private int _menuId = 2000;

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

            // ── QUẢN LÝ NHÂN VIÊN ──
            int qlnvId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = qlnvId,
                Text = "Quản lý nhân viên",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Danh sách nhân viên", ParentID = qlnvId },
                    new NavBarItem { ID = _menuId++, Text = "Thêm nhân viên mới",  ParentID = qlnvId },
                }
            });

            // ── HỒ SƠ NHÂN VIÊN ──
            int profileId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = profileId,
                Text = "Hồ sơ nhân viên",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Thông tin cá nhân",   ParentID = profileId },
                    new NavBarItem { ID = _menuId++, Text = "Ảnh đại diện",        ParentID = profileId },
                    new NavBarItem { ID = _menuId++, Text = "CCCD / hộ chiếu",     ParentID = profileId },
                    new NavBarItem { ID = _menuId++, Text = "Thông tin liên hệ",   ParentID = profileId }
                }
            });





            // ── PHÒNG BAN & CHỨC VỤ ──
            int deptId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = deptId,
                Text = "Phòng ban & chức vụ",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Danh sách phòng ban", ParentID = deptId },
                    new NavBarItem { ID = _menuId++, Text = "Sơ đồ tổ chức",       ParentID = deptId },
                    new NavBarItem { ID = _menuId++, Text = "Chức danh",           ParentID = deptId },
                    new NavBarItem { ID = _menuId++, Text = "Điều chuyển nhân sự", ParentID = deptId }
                }
            });

            // ── HỢP ĐỒNG LAO ĐỘNG ──
            int contractId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = contractId,
                Text = "Hợp đồng lao động",
                ParentID = null,
                Childs = new List<NavBarItem>
                {
                    new NavBarItem { ID = _menuId++, Text = "Tạo hợp đồng",        ParentID = contractId },
                    new NavBarItem { ID = _menuId++, Text = "Lịch sử hợp đồng",    ParentID = contractId }
                }
            });

            // ── CHẤM CÔNG ──

            int attendId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = attendId,
                Text = "⏰ Chấm công",
                ParentID = null,
                Childs = new List<NavBarItem> {
                    new NavBarItem { ID = _menuId++, Text = "Check-in / Check-out",  ParentID = attendId },
                    new NavBarItem { ID = _menuId++, Text = "Bảng công theo tháng",  ParentID = attendId },
                    new NavBarItem { ID = _menuId++, Text = "Nghỉ phép",             ParentID = attendId },
                    new NavBarItem { ID = _menuId++, Text = "Làm thêm giờ",          ParentID = attendId }
                }
            });

            // ── TÍNH LƯƠNG ──

            int salaryId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = salaryId,
                Text = "💵 Tính lương",
                ParentID = null,
                Childs = new List<NavBarItem> {
                    new NavBarItem { ID = _menuId++, Text = "Lương cơ bản", ParentID = salaryId },
                    new NavBarItem { ID = _menuId++, Text = "Phụ cấp",      ParentID = salaryId },
                    new NavBarItem { ID = _menuId++, Text = "Khấu trừ",     ParentID = salaryId },
                    new NavBarItem { ID = _menuId++, Text = "Thưởng",       ParentID = salaryId },
                    new NavBarItem { ID = _menuId++, Text = "Bảng lương",   ParentID = salaryId },
                    new NavBarItem { ID = _menuId++, Text = "Phiếu lương",  ParentID = salaryId }
                }
            });

            // ── KHEN THƯỞNG VÀ KỈ LUẬT ──

            int rewardId = _menuId++;
            menuItems.Add(new NavBarItem
            {
                ID = rewardId,
                Text = "🎖️ Khen thưởng & kỷ luật",
                ParentID = null,
                Childs = new List<NavBarItem> {
                    new NavBarItem { ID = _menuId++, Text = "Quyết định thưởng",  ParentID = rewardId },
                    new NavBarItem { ID = _menuId++, Text = "Vi phạm / cảnh cáo", ParentID = rewardId },
                    new NavBarItem { ID = _menuId++, Text = "Lịch sử",            ParentID = rewardId }
                }
            });

            return menuItems;
        }
    }
}