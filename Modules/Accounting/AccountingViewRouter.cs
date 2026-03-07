using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharkTank.Modules.Accounting
{
    public static class AccountingViewRouter
    {
        private static Dictionary<string, Func<UserControl>> _routes =
            new Dictionary<string, Func<UserControl>>()
        {
            { "Phiếu thu", () => CreateDefaultView("Phiếu thu") },
            { "Phiếu chi", () => CreateDefaultView("Phiếu chi") },
            { "Nguồn tiền", () => CreateDefaultView("Nguồn tiền") },
            { "Tiền mặt", () => CreateDefaultView("Sổ quỹ tiền mặt") },
            { "Ngân hàng", () => CreateDefaultView("Sổ quỹ ngân hàng") },
            { "Số dư", () => CreateDefaultView("Số dư") },
            { "Công nợ khách hàng", () => CreateDefaultView("Công nợ khách hàng") },
            { "Công nợ nhà cung cấp", () => CreateDefaultView("Công nợ nhà cung cấp") },
            { "Lịch sử thanh toán", () => CreateDefaultView("Lịch sử thanh toán") },
            { "Nhận dữ liệu từ Sales", () => CreateDefaultView("Nhận dữ liệu từ Sales") },
            { "Ghi nhận doanh thu", () => CreateDefaultView("Ghi nhận doanh thu") },
            { "Ghi nhận chi phí", () => CreateDefaultView("Ghi nhận chi phí") },
            { "Báo cáo thu chi", () => CreateDefaultView("Báo cáo thu chi") },
            { "Lãi lỗ", () => CreateDefaultView("Báo cáo lãi lỗ") },
            { "Sổ cái đơn giản", () => CreateDefaultView("Sổ cái") },
            { "Tổng hợp tài chính", () => CreateDefaultView("Tổng hợp tài chính") }
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
