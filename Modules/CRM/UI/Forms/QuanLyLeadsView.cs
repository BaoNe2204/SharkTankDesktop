using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.CRM.UI.Forms
{
    /// <summary>
    /// View: Quản lý khách hàng tiềm năng (Leads)
    /// Thêm/Sửa/Xóa leads, phân loại, theo dõi
    /// </summary>
    public partial class QuanLyLeadsView : UserControl
    {
        public QuanLyLeadsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Panel tiêu đề
            Panel panelTitle = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(0, 120, 215)
            };
            
            Label lblTitle = new Label
            {
                Text = "🎯 Quản lý khách hàng tiềm năng (Leads)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true
            };
            panelTitle.Controls.Add(lblTitle);
            this.Controls.Add(panelTitle);

            // Panel nội dung chính
            Panel panelContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            Label lblPlaceholder = new Label
            {
                Text = "📝 Hướng dẫn:\n\n" +
                       "1. Tạo DataGridView hiển thị danh sách Leads\n" +
                       "2. Thêm TextBox cho: Tên khách, Số điện thoại, Nguồn, Trạng thái...\n" +
                       "3. Thêm Button: Thêm lead, Chuyển đổi, Gọi điện, Gửi email\n" +
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
