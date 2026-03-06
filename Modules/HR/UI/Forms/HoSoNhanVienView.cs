using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.HR.UI.Forms
{
    /// <summary>
    /// View: Hồ sơ nhân viên
    /// Thêm/Sửa/Xóa thông tin nhân viên
    /// </summary>
    public partial class HoSoNhanVienView : UserControl
    {
        public HoSoNhanVienView()
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
                Text = "🧑‍💼 Hồ sơ nhân viên",
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

            // TODO: Thêm controls vào đây
            // Ví dụ: DataGridView hiển thị danh sách nhân viên
            // TextBox/ComboBox để nhập liệu
            // Button để Thêm/Sửa/Xóa

            Label lblPlaceholder = new Label
            {
                Text = "📝 Hướng dẫn:\n\n" +
                       "1. Tạo DataGridView hiển thị danh sách nhân viên\n" +
                       "2. Thêm TextBox cho: Họ tên, Số CCCD, Điện thoại, Email...\n" +
                       "3. Thêm Button: Thêm, Sửa, Xóa, Lưu\n" +
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
