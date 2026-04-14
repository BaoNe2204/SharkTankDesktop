using System;
using System.Data;
using System.Windows.Forms;
using SharkTank.Modules.Sales.DAL;

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class PhieuXuatKho : UserControl
    {
        private DataTable dtGioHang;

         private XuatKhoDAL xuatKhoDAL = new XuatKhoDAL();

        public PhieuXuatKho()
        {
            InitializeComponent();

            // Thiết lập bảng giỏ hàng tạm
            dtGioHang = new DataTable();
            dtGioHang.Columns.Add("Mã SP", typeof(string));
            dtGioHang.Columns.Add("Số Lượng", typeof(int));
            dtGioHang.Columns.Add("Giá Xuất", typeof(decimal));
            dtGioHang.Columns.Add("Thành Tiền", typeof(decimal), "[Số Lượng] * [Giá Xuất]"); // Tự động tính

            dgvChiTietXuat.DataSource = dtGioHang;

            // Gắn sự kiện
            this.Load += PhieuXuatKho_Load;
            btnThemSP.Click += BtnThemSP_Click;
            btnXoaSP.Click += BtnXoaSP_Click;
            btnLuuPhieu.Click += BtnLuuPhieu_Click;
        }

        private void PhieuXuatKho_Load(object sender, EventArgs e)
        {
            // Tự sinh mã phiếu ngẫu nhiên theo thời gian
            txtPhieuXuat.Text = "PX-" + DateTime.Now.ToString("yyyyMMdd-HHmm");

            // Format DataGridView cho đẹp
            if (dgvChiTietXuat.Columns.Count > 0)
            {
                dgvChiTietXuat.Columns["Giá Xuất"].DefaultCellStyle.Format = "N0";
                dgvChiTietXuat.Columns["Thành Tiền"].DefaultCellStyle.Format = "N0";
            }
        }

        private void BtnThemSP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) || string.IsNullOrWhiteSpace(txtGiaXuat.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã SP và Giá xuất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Thêm dữ liệu vào lưới
                dtGioHang.Rows.Add(txtMaSP.Text.Trim(), nudSoLuong.Value, Convert.ToDecimal(txtGiaXuat.Text.Trim()));

                txtMaSP.Clear();
                nudSoLuong.Value = 1;
                txtGiaXuat.Clear();
                txtMaSP.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Giá xuất phải là số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoaSP_Click(object sender, EventArgs e)
        {
            if (dgvChiTietXuat.CurrentRow != null)
            {
                dgvChiTietXuat.Rows.RemoveAt(dgvChiTietXuat.CurrentRow.Index);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhieuXuat.Text) || string.IsNullOrWhiteSpace(txtMaKho.Text))
            {
                MessageBox.Show("Mã phiếu xuất và Mã kho không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách xuất kho đang trống. Vui lòng thêm sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                bool ketQua = xuatKhoDAL.LuuPhieuXuatKho(txtPhieuXuat.Text.Trim(), txtMaKho.Text.Trim(), dtGioHang);
                if (ketQua)
                {
                    MessageBox.Show("Lưu Phiếu Xuất Kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtGioHang.Clear();
                    txtPhieuXuat.Text = "PX-" + DateTime.Now.ToString("yyyyMMdd-HHmm");
                }
                

                MessageBox.Show("Đã lưu thành công! ", "Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}