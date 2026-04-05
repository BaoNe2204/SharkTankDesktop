using SharkTank.Core.Models;
using SharkTank.Modules.Accounting.UI.Forms;
using SharkTank.Modules.Debt.Views;
using SharkTank.Modules.Finance.Views;
using SharkTank.Modules.Reports.Views;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SharkTank.Modules.Accounting
{
    public static class AccountingViewRouter
    {
        private static Dictionary<string, Func<UserControl>> _routes =
            new Dictionary<string, Func<UserControl>>()
        {
            { "Phiếu thu chi", () => new PhieuThuChiView() },
            { "Quản lý sổ quỹ", () => new SoQuyView() },
            { "Quản lý Công nợ", () => new CongNoView() },
            { "Nhận dữ liệu từ Sales", () => CreateDefaultView("Nhận dữ liệu từ Sales") },
            { "Ghi nhận doanh thu", () => CreateDefaultView("Ghi nhận doanh thu") },
            { "Ghi nhận chi phí", () => CreateDefaultView("Ghi nhận chi phí") },
            { "Báo cáo tài chính", () => new BaoCaoTaiChinhView() },

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
                Text = $"Accounting - {name}\nĐang phát triển...",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 16)
            };
            view.Controls.Add(lbl);
            return view;
        }
    }
}
