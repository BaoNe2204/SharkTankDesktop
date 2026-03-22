using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharkTank.Modules.HR.UI.Forms;

namespace SharkTank.Modules.HR
{
    public static class HRViewRouter
    {
        private static Dictionary<string, Func<UserControl>> _routes =
            new Dictionary<string, Func<UserControl>>()
        {
            // ── QUẢN LÝ NHÂN VIÊN ──
            { "Danh sách nhân viên",        () => new DanhSachNhanVienView() },
            { "Thêm nhân viên mới",         () => new ThemNhanVienView() },

            // ── HỒ SƠ ──
            { "Thông tin cá nhân",          () => new ThongTinCaNhanView() },
            { "Ảnh đại diện",               () => new AnhDaiDienView() },
            { "CCCD / hộ chiếu",            () => CreateDefaultView("CCCD") },
            { "Thông tin liên hệ",          () => CreateDefaultView("TTLH") },

            // ── PHÒNG BAN ──
            { "Danh sách phòng ban",        () => CreateDefaultView("Danh sách phòng ban") },
            { "Sơ đồ tổ chức",              () => CreateDefaultView("Sơ đồ tổ chức") },
            { "Chức danh",                  () => CreateDefaultView("Chức danh") },
            { "Điều chuyển nhân sự",        () => CreateDefaultView("Điều chuyển nhân sự") },

            // ── HỢP ĐỒNG ──
            { "Tạo hợp đồng",               () => new TaoHopDongView() },
            { "Lịch sử hợp đồng",           () => new LichSuHopDongView() },

            // ── CHẤM CÔNG ──
            { "Check-in / Check-out",       () => new CheckInOutView() },
            { "Bảng công theo tháng",       () => new BangCongView() },
            { "Nghỉ phép",                  () => new NghiPhepView() },
            { "Làm thêm giờ",               () => new LamThemGioView() },

            // ── LƯƠNG ──
            { "Lương cơ bản",               () => CreateDefaultView("Lương cơ bản") },
            { "Phụ cấp",                    () => CreateDefaultView("Phụ cấp") },
            { "Khấu trừ",                   () => CreateDefaultView("Khấu trừ") },
            { "Thưởng",                     () => CreateDefaultView("Thưởng") },
            { "Bảng lương",                 () => CreateDefaultView("Bảng lương") },
            { "Phiếu lương",                () => CreateDefaultView("Phiếu lương") },

            // ── KHEN THƯỞNG ──
            { "Quyết định thưởng",          () => CreateDefaultView("Quyết định thưởng") },
            { "Vi phạm / cảnh cáo",         () => CreateDefaultView("Vi phạm / cảnh cáo") },
            { "Lịch sử",                    () => CreateDefaultView("Lịch sử") }
        };

        public static UserControl GetView(string menuText)
        {
            string key = menuText?.Trim() ?? "";
            if (_routes.ContainsKey(key))
                return _routes[key]();
            return CreateDefaultView(menuText);
        }

        private static UserControl CreateDefaultView(string name)
        {
            UserControl view = new UserControl { Dock = DockStyle.Fill };
            view.Controls.Add(new Label
            {
                Text = $"HR - {name}\nĐang phát triển...",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 16)
            });
            return view;
        }
    }
}