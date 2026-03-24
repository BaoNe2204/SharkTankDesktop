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
            { "Thêm / sửa / xóa khách hàng", () => new QuanLyKhachHang() },
            { "Phân loại khách hàng", () => new PhanLoaiKH() },
            { "Lịch sử mua hàng", () => new LichSuMuaHang() },
            { "Công nợ khách", () => new QLCongNoKH() },
            { "Tạo báo giá", () => new QuanLyKhachHang() },
            { "Sửa / duyệt báo giá", () => new QuanLyKhachHang() },
            { "Gửi cho khách", () => new QuanLyKhachHang() },
            { "Tạo đơn hàng", () => new QuanLyKhachHang() },
            { "Trạng thái đơn", () => new QuanLyKhachHang() },
            { "Chi tiết sản phẩm", () => new QuanLyKhachHang() },
            { "Chiết khấu", () => new QuanLyKhachHang() },
            { "Thuế", () => new QuanLyKhachHang() },
            { "Xuất hóa đơn", () => new QuanLyKhachHang() },
            { "In / PDF", () => new QuanLyKhachHang() },
            { "Gửi khách hàng", () => new QuanLyKhachHang() },
            { "Doanh thu theo ngày/tháng", () => new QuanLyKhachHang() },
            { "Theo nhân viên", () => new QuanLyKhachHang() },
            { "Theo sản phẩm", () => new QuanLyKhachHang() },
            { "Kiểm tra tồn trước khi bán", () => new QuanLyKhachHang() },
            { "Tạo yêu cầu xuất kho", () => new QuanLyKhachHang() }
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
