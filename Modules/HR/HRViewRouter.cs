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
            { "CCCD / hộ chiếu",            () => new CCCDHoChieuView() },
            { "Thông tin liên hệ",          () => new ThongTinLienHeView() },
       

            // ── PHÒNG BAN ──
            { "Danh sách phòng ban",        () => new DanhSachPhongBanView() },
            { "Sơ đồ tổ chức",              () => new SoDoToChucView() },
            { "Chức danh",                  () => new ChucDanhView() },
            { "Điều chuyển nhân sự",        () => new DieuChuyenNhanSuView() },

            // ── HỢP ĐỒNG ──
            { "Tạo hợp đồng",               () => new TaoHopDongView() },
            { "Lịch sử hợp đồng",           () => new LichSuHopDongView() },

            // ── CHẤM CÔNG ──
            { "Check-in / Check-out",       () => new CheckInOutView() },
            { "Bảng công theo tháng",       () => new BangCongView() },
            { "Nghỉ phép",                  () => new NghiPhepView() },
            { "Làm thêm giờ",               () => new LamThemGioView() } };


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