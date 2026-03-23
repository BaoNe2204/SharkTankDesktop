using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharkTank.Modules.Admin.UI.Forms;

namespace SharkTank.Modules.Admin
{
    public static class AdminViewRouter
    {
        private static Dictionary<string, Func<UserControl>> _routes =
            new Dictionary<string, Func<UserControl>>(StringComparer.OrdinalIgnoreCase)
            {
                // =========================
                // 🔐 Người dùng & phân quyền
                // =========================
                { "Quản lý tài khoản", () => new QuanLyNguoiDungForm() },
                { "Phân quyền Role", () => new PhanQuyenRoleForm() },
                { "Phân quyền chi tiết", () => new PhanQuyenChiTietForm() },
                { "Quản lý phiên đăng nhập", () => new QuanLyPhienDangNhapForm() },

                // =========================
                // 🏢 Cơ cấu tổ chức
                // =========================
                 { "Phòng ban", () => new PhongBanForm() },
                 { "Chức vụ", () => new ChucVuForm() },

                // =========================
                // ⚙️ Cấu hình hệ thống
                // =========================
                { "Thông tin công ty", () => new ThongTinCongTyForm() },
                { "Cấu hình hệ thống", () => new CauHinhHeThongForm() },
                { "Cấu hình email", () => new CauHinhEmailForm() },
                { "Tiền tệ / định dạng", () => new DinhDangTienTeForm() },

                // =========================
                // 🔔 Thông báo hệ thống
                // =========================
                 { "Thông báo hệ thống", () => new ThongBaoHeThongForm() },

                // =========================
                // 📊 Dashboard hệ thống
                // =========================
                // { "Dashboard hệ thống", () => new DashboardTongForm() },

                // =========================
                // 📝 Nhật ký hệ thống
                // =========================
                { "Lịch sử đăng nhập", () => new LichSuDangNhapForm() },
                { "Lịch sử thao tác", () => new LichSuHoatDongForm() },
                { "Theo dõi thay đổi dữ liệu", () => new NhatKyHeThongForm() },

                // =========================
                // 💾 Backup & Restore
                // =========================
                // { "Sao lưu dữ liệu", () => new BackupRestoreForm() },
                // { "Khôi phục dữ liệu", () => new BackupRestoreForm() },

                // =========================
                // 📦 Quản lý module
                // =========================
                // { "Bật / tắt module ERP", () => new ModuleManagerForm() }
            };

        public static UserControl GetView(string menuText)
        {
            if (_routes.ContainsKey(menuText))
                return _routes[menuText]();

            return CreateDefaultView(menuText);
        }

        private static UserControl CreateDefaultView(string name)
        {
            var view = new UserControl { Dock = DockStyle.Fill };
            var lbl = new Label
            {
                Text = $"Admin - {name}\nĐang phát triển...",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 16)
            };
            view.Controls.Add(lbl);
            return view;
        }
    }
}
