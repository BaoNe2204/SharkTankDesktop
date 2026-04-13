using SharkTank.Modules.Sales.BLL;
using System;
using System.Windows.Forms;

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class Doanhthu : UserControl
    {
        // Gọi lớp BUS
        private DoanhThu_BLL bus = new DoanhThu_BLL();

        public Doanhthu()
        {
            InitializeComponent();

            btnTheoNgay.Click += BtnTheoNgay_Click;
            btnTheoNhanVien.Click += BtnTheoNhanVien_Click;
            btnTheoSanPham.Click += BtnTheoSanPham_Click;

            this.Load += Doanhthu_Load;
        }

        private void Doanhthu_Load(object sender, EventArgs e)
        {
            btnTheoNgay.PerformClick();
        }

        private void BtnTheoNgay_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "DOANH THU THEO NGÀY/THÁNG";
            dgvHienThi.DataSource = bus.LayDoanhThuTheoNgay();
            FormatTienTe();
        }

        private void BtnTheoNhanVien_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "DOANH THU THEO NHÂN VIÊN";
            dgvHienThi.DataSource = bus.LayDoanhThuTheoNhanVien();
            FormatTienTe();
        }

        private void BtnTheoSanPham_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "TOP SẢN PHẨM BÁN CHẠY";
            dgvHienThi.DataSource = bus.LayDoanhThuTheoSanPham();
            FormatTienTe();
        }

        private void FormatTienTe()
        {
            if (dgvHienThi.Columns.Contains("Doanh Thu"))
                dgvHienThi.Columns["Doanh Thu"].DefaultCellStyle.Format = "N0";

            if (dgvHienThi.Columns.Contains("Tổng Doanh Thu"))
                dgvHienThi.Columns["Tổng Doanh Thu"].DefaultCellStyle.Format = "N0";

            if (dgvHienThi.Columns.Contains("Tổng Thu"))
                dgvHienThi.Columns["Tổng Thu"].DefaultCellStyle.Format = "N0";
        }


    }
}