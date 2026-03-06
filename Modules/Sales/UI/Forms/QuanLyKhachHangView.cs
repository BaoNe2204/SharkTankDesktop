using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class QuanLyKhachHangView : UserControl
    {
        public QuanLyKhachHangView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            Panel panelTitle = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(0, 120, 215)
            };
            
            Label lblTitle = new Label
            {
                Text = "👥 Quản lý khách hàng",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true
            };
            panelTitle.Controls.Add(lblTitle);
            this.Controls.Add(panelTitle);

            Panel panelContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            Label lblPlaceholder = new Label
            {
                Text = "📝 Hướng dẫn:\n\n" +
                       "1. Tạo DataGridView hiển thị danh sách khách hàng\n" +
                       "2. Thêm TextBox cho: Tên khách, Địa chỉ, Điện thoại, Email...\n" +
                       "3. Thêm Button: Thêm, Sửa, Xóa, In báo cáo\n" +
                       "4. Viết code xử lý sự kiện Click cho các Button\n\n" +
                       "Xem ví dụ trong: Modules/Admin/UI/Forms/QuanLyNguoiDungForm.cs",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                Location = new Point(20, 20),
                AutoSize = true
            };
            panelContent.Controls.Add(lblPlaceholder);

            this.Controls.Add(panelContent);
            this.ResumeLayout(false);
        }
    }
}
