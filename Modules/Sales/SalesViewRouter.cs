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
            { "Báo giá", () => new BaoGia() },
            { "Hóa đơn", () => new HoaDon() },
            { "Theo dõi doanh thu", () => new Doanhthu() },

            { "Kiểm tra tồn trước khi bán", () => new KiemtraTonkho() },
            { "Tạo yêu cầu xuất kho", () => new PhieuXuatKho() }
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
