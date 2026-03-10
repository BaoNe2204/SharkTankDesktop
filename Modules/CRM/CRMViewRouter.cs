
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
            { "Nhân viên phụ trách", () => new NhanVienPhuTrachForm() },
            { "Lịch gọi điện", () => CreateDefaultView("Lịch gọi điện") },
            { "Lịch hẹn gặp khách", () => CreateDefaultView("Lịch hẹn gặp khách") },
            { "Ghi chú trao đổi", () => CreateDefaultView("Ghi chú trao đổi") },
            { "Lịch sử chăm sóc khách hàng", () => CreateDefaultView("Lịch sử chăm sóc") },
            { "Chuyển Lead → Khách hàng chính thức", () => CreateDefaultView("Chuyển đổi khách hàng") },
            { "Tạo khách hàng cho module Sales", () => CreateDefaultView("Tạo khách hàng cho Sales") },
            { "Lưu lịch sử chuyển đổi", () => CreateDefaultView("Lịch sử chuyển đổi") },
            { "Tên cơ hội bán", () => CreateDefaultView("Cơ hội bán hàng") },
            { "Khách hàng liên quan", () => CreateDefaultView("Khách hàng liên quan") },
            { "Giá trị dự kiến", () => CreateDefaultView("Giá trị dự kiến") },
            { "Xác suất thành công", () => CreateDefaultView("Xác suất thành công") },
            { "Trạng thái cơ hội", () => CreateDefaultView("Trạng thái cơ hội") },
            { "Khách tiềm năng", () => CreateDefaultView("Pipeline - Khách tiềm năng") },
            { "Đang tư vấn", () => CreateDefaultView("Pipeline - Đang tư vấn") },
            { "Đang báo giá", () => CreateDefaultView("Pipeline - Đang báo giá") },
            { "Đang đàm phán", () => CreateDefaultView("Pipeline - Đang đàm phán") },
            { "Đã chốt / thất bại", () => CreateDefaultView("Pipeline - Đã chốt") },
            { "Nhắc lịch chăm sóc", () => CreateDefaultView("Nhắc lịch chăm sóc") },
            { "Nhắc gọi khách", () => CreateDefaultView("Nhắc gọi khách") },
            { "Nhắc gửi báo giá", () => CreateDefaultView("Nhắc gửi báo giá") },
            { "Quản lý công việc sales", () => CreateDefaultView("Quản lý công việc") },
            { "Tỷ lệ chuyển đổi khách hàng", () => CreateDefaultView("Báo cáo tỷ lệ chuyển đổi") },
            { "Hiệu quả nhân viên sales", () => CreateDefaultView("Báo cáo hiệu quả NV") },
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
