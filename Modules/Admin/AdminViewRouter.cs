using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharkTank.Modules.Admin.UI.Forms;

namespace SharkTank.Modules.Admin
{
    public static class AdminViewRouter
    {
        private static Dictionary<string, Func<UserControl>> _routes =
            new Dictionary<string, Func<UserControl>>()
        {
            { "Phân quyền theo role", () => new PhanQuyenTheoRoleForm() },
            { "Phân quyền chi tiết theo chức năng", () => new PhanQuyenChiTietForm() },
            { "Tạo / sửa / xóa người dùng", () => new QuanLyNguoiDungForm() }
        };

        public static UserControl GetView(string menuText)
        {
            if (_routes.ContainsKey(menuText))
                return _routes[menuText]();

            return CreateDefaultView(menuText);
        }

        private static UserControl CreateDefaultView(string name)
        {
            UserControl view = new UserControl
            {
                Dock = DockStyle.Fill
            };

            Label lbl = new Label
            {
                Text = $"Chức năng: {name}\nĐang phát triển...",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 16)
            };

            view.Controls.Add(lbl);

            return view;
        }
    }
}
