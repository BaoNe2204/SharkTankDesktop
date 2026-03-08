using SharkTank.Modules.Inventory.UI.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharkTank.Modules.Inventory
{
    public static class InventoryViewRouter
    {
        private static Dictionary<string, Func<UserControl>> _routes =
            new Dictionary<string, Func<UserControl>>()
        {
            { "Thêm / sửa / xóa sản phẩm", () => new DanhMucSanPhamView() },
            { "Mã sản phẩm", () => CreateDefaultView("Mã sản phẩm") },
            { "Nhóm hàng", () => CreateDefaultView("Nhóm hàng") },
            { "Đơn vị tính", () => CreateDefaultView("Đơn vị tính") },
            { "Giá nhập / bán", () => CreateDefaultView("Giá nhập / bán") },
            { "Phiếu nhập", () => CreateDefaultView("Phiếu nhập") },
            { "Nhà cung cấp", () => CreateDefaultView("Nhà cung cấp") },
            { "Giá nhập", () => CreateDefaultView("Giá nhập") },
            { "Số lượng", () => CreateDefaultView("Số lượng") },
            { "Phiếu xuất", () => CreateDefaultView("Phiếu xuất") },
            { "Xuất cho bán hàng", () => CreateDefaultView("Xuất cho bán hàng") },
            { "Xuất nội bộ", () => CreateDefaultView("Xuất nội bộ") },
            { "Tồn theo sản phẩm", () => CreateDefaultView("Tồn theo sản phẩm") },
            { "Tồn theo kho", () => CreateDefaultView("Tồn theo kho") },
            { "Cảnh báo hết hàng", () => CreateDefaultView("Cảnh báo hết hàng") },
            { "Kiểm kê định kỳ", () => CreateDefaultView("Kiểm kê định kỳ") },
            { "Điều chỉnh tồn", () => CreateDefaultView("Điều chỉnh tồn") },
            { "Biên bản kiểm kê", () => CreateDefaultView("Biên bản kiểm kê") },
            { "Nhiều kho", () => CreateDefaultView("Nhiều kho") },
            { "Vị trí lưu trữ", () => CreateDefaultView("Vị trí lưu trữ") }
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
                Text = $"Inventory - {name}\nĐang phát triển...",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 16)
            };
            view.Controls.Add(lbl);
            return view;
        }
    }
}
