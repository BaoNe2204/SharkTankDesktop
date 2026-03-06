using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharkTank.Modules.Sales.UI.Forms;

namespace SharkTank.Modules.Sales
{
    public static class SalesViewRouter
    {
        private static Dictionary<string, Func<UserControl>> _routes =
            new Dictionary<string, Func<UserControl>>()
        {
            { "Thêm / sửa / xóa khách hàng", () => new QuanLyKhachHangView() },
            { "Phân loại khách hàng", () => new QuanLyKhachHangView() },
            { "Lịch sử mua hàng", () => new QuanLyKhachHangView() },
            { "Công nợ khách", () => new QuanLyKhachHangView() },
            { "Tạo báo giá", () => new QuanLyKhachHangView() },
            { "Sửa / duyệt báo giá", () => new QuanLyKhachHangView() },
            { "Gửi cho khách", () => new QuanLyKhachHangView() },
            { "Tạo đơn hàng", () => new QuanLyKhachHangView() },
            { "Trạng thái đơn", () => new QuanLyKhachHangView() },
            { "Chi tiết sản phẩm", () => new QuanLyKhachHangView() },
            { "Chiết khấu", () => new QuanLyKhachHangView() },
            { "Thuế", () => new QuanLyKhachHangView() },
            { "Xuất hóa đơn", () => new QuanLyKhachHangView() },
            { "In / PDF", () => new QuanLyKhachHangView() },
            { "Gửi khách hàng", () => new QuanLyKhachHangView() },
            { "Doanh thu theo ngày/tháng", () => new QuanLyKhachHangView() },
            { "Theo nhân viên", () => new QuanLyKhachHangView() },
            { "Theo sản phẩm", () => new QuanLyKhachHangView() },
            { "Kiểm tra tồn trước khi bán", () => new QuanLyKhachHangView() },
            { "Tạo yêu cầu xuất kho", () => new QuanLyKhachHangView() }
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
                Text = $"Sales - {name}\nĐang phát triển...",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 16)
            };

            view.Controls.Add(lbl);

            return view;
        }
    }
}
