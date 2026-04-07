using System;
using System.Windows.Forms;
using SharkTank.Modules.Sales.BLL;

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class HoaDon : UserControl
    {
        private HoaDonBLL _bll = new HoaDonBLL();

        public HoaDon()
        {
            InitializeComponent();

            btnXuatHoaDon.Click += BtnXuatHoaDon_Click;
            btnInPDF.Click += BtnInPDF_Click;
            btnGuiKhach.Click += BtnGuiKhach_Click;

            LoadData();
        }
        public void LoadData()
        {
            try
            {
                dgvHoaDon.DataSource = _bll.GetDanhSachHoaDon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXuatHoaDon_Click(object sender, EventArgs e)
        {
            XuatHD frmXuat = new XuatHD();
            frmXuat.ShowDialog();


            LoadData();
        }

        private void BtnInPDF_Click(object sender, EventArgs e)
        {
            PDF frmPDF = new PDF(dgvHoaDon, "BÁO CÁO DANH SÁCH HÓA ĐƠN BÁN HÀNG");
            frmPDF.ShowDialog();
        }

        private void BtnGuiKhach_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng click chọn một hóa đơn ở bảng bên dưới để gửi!", "Lưu ý");
                return;
            }

            string maHD = dgvHoaDon.CurrentRow.Cells["Mã Hóa Đơn"].Value.ToString();
            string tenKhach = dgvHoaDon.CurrentRow.Cells["Tên Khách Hàng"].Value.ToString();

            GuiKH frmMail = new GuiKH("", tenKhach, maHD, "");
            frmMail.ShowDialog();
        }


    }
}