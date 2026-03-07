using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SharkTank.Modules.HR
{
    public static class HRViewRouter
    {
        private static Dictionary<string, Func<UserControl>> _routes =
            new Dictionary<string, Func<UserControl>>()
        {
            { "Thêm / sửa / xóa nhân viên", () => CreateDefaultView("Hồ sơ nhân viên") },
            { "Thông tin cá nhân", () => CreateDefaultView("Thông tin cá nhân") },
            { "Ảnh đại diện", () => CreateDefaultView("Ảnh đại diện") },
            { "CCCD / hộ chiếu", () => CreateDefaultView("CCCD / hộ chiếu") },
            { "Thông tin liên hệ", () => CreateDefaultView("Thông tin liên hệ") },
            { "Danh sách phòng ban", () => CreateDefaultView("Danh sách phòng ban") },
            { "Sơ đồ tổ chức", () => CreateDefaultView("Sơ đồ tổ chức") },
            { "Chức danh", () => CreateDefaultView("Chức danh") },
            { "Điều chuyển nhân sự", () => CreateDefaultView("Điều chuyển nhân sự") },
            { "Tạo hợp đồng", () => CreateDefaultView("Tạo hợp đồng") },
            { "Gia hạn / chấm dứt", () => CreateDefaultView("Gia hạn / chấm dứt") },
            { "Lịch sử hợp đồng", () => CreateDefaultView("Lịch sử hợp đồng") },
            { "Check-in / Check-out", () => CreateDefaultView("Check-in / Check-out") },
            { "Bảng công theo tháng", () => CreateDefaultView("Bảng công theo tháng") },
            { "Nghỉ phép", () => CreateDefaultView("Nghỉ phép") },
            { "Làm thêm giờ", () => CreateDefaultView("Làm thêm giờ") },
            { "Lương cơ bản", () => CreateDefaultView("Lương cơ bản") },
            { "Phụ cấp", () => CreateDefaultView("Phụ cấp") },
            { "Khấu trừ", () => CreateDefaultView("Khấu trừ") },
            { "Thưởng", () => CreateDefaultView("Thưởng") },
            { "Bảng lương", () => CreateDefaultView("Bảng lương") },
            { "Phiếu lương", () => CreateDefaultView("Phiếu lương") },
            { "Quyết định thưởng", () => CreateDefaultView("Quyết định thưởng") },
            { "Vi phạm / cảnh cáo", () => CreateDefaultView("Vi phạm / cảnh cáo") },
            { "Lịch sử", () => CreateDefaultView("Lịch sử") }
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
                Text = $"HR - {name}\nĐang phát triển...",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 16)
            };
            view.Controls.Add(lbl);
            return view;
        }
    }
}
