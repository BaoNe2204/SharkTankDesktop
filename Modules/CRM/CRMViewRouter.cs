
using SharkTank.Core.Data;
using SharkTank.Modules.CRM.UI.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharkTank.Modules.CRM
{
    public static class CRMViewRouter
    {
        private static Dictionary<string, Func<UserControl>> _routes =
            new Dictionary<string, Func<UserControl>>()
        {
            { "Quản lý Leads", () => new QuanLyLeadsForm() },
            { "Chăm sóc khách hàng", () => new ChamSocKhachHangForm() },
            { "Quản lý cơ hội bán hàng", () => new QLCoHoiBanHangForm()},
            { "Tỷ lệ chuyển đổi khách hàng", () => new TyLeChuyenDoiKHForm()},
            { "Nguồn khách hàng hiệu quả", () => CreateDefaultView("Báo cáo nguồn khách") },
            { "Số lượng lead theo thời gian", () => CreateDefaultView("Báo cáo số lượng lead") }
        };

        public static UserControl GetView(string menuText)
        {
            if (_routes.ContainsKey(menuText))
                return _routes[menuText]();

            return CreateDefaultView(menuText);
        }

        private static UserControl CreateDefaultView(string name)
        {
            UserControl view = new UserControl { Dock = DockStyle.Fill };
            Label lbl = new Label
            {
                Text = $"CRM - {name}\nĐang phát triển...",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 16)
            };
            view.Controls.Add(lbl);
            return view;
        }
    }
}
