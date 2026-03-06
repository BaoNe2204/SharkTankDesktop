using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    /// <summary>
    /// View: Danh mục sản phẩm
    /// Thêm/Sửa/Xóa sản phẩm, mã, nhóm hàng, đơn vị tính, giá
    /// </summary>
    public partial class DanhMucSanPhamView : UserControl
    {
        public DanhMucSanPhamView()
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
                Text = "📋 Danh mục sản phẩm",
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
                       "1. Tạo DataGridView hiển thị danh sách sản phẩm\n" +
                       "2. Thêm TextBox cho: Mã SP, Tên SP, Nhóm hàng, Đơn vị tính, Giá nhập, Giá bán...\n" +
                       "3. Thêm Button: Thêm, Sửa, Xóa, Nhập Excel\n" +
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
