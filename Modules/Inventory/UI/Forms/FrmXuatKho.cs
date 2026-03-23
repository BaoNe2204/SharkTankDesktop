using System;
using System.Windows.Forms;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class FrmXuatKho : Form
    {
        public FrmXuatKho()
        {
            InitializeComponent();
        }

        // ================= TRẢ DỮ LIỆU =================
        public string PhieuXuat => txtPhieuXuat.Text;
        public string MaSP => txtMaSP.Text;
        public string MaKho => txtMaKho.Text;

        public int SoLuong
        {
            get
            {
                int.TryParse(txtSoLuong.Text, out int value);
                return value;
            }
        }

        public string LoaiXuat => cbLoaiXuat.Text;

        // ================= SET DATA (CHO SỬA) =================
        public void SetData(string phieu, string masp, string makho, int soluong, string loai)
        {
            txtPhieuXuat.Text = phieu;
            txtMaSP.Text = masp;
            txtMaKho.Text = makho;
            txtSoLuong.Text = soluong.ToString();
            cbLoaiXuat.Text = loai;
        }

        // ================= LOAD =================
        private void FrmXuatKho_Load(object sender, EventArgs e)
        {
            // Load loại xuất
            cbLoaiXuat.Items.Clear();
            cbLoaiXuat.Items.Add("Bán hàng");
            cbLoaiXuat.Items.Add("Nội bộ");
            cbLoaiXuat.SelectedIndex = 0;
        }

        // ================= LƯU =================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhieuXuat.Text))
            {
                MessageBox.Show("Nhập phiếu xuất!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                MessageBox.Show("Nhập mã sản phẩm!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaKho.Text))
            {
                MessageBox.Show("Nhập mã kho!");
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out _))
            {
                MessageBox.Show("Số lượng không hợp lệ!");
                return;
            }

            if (string.IsNullOrWhiteSpace(cbLoaiXuat.Text))
            {
                MessageBox.Show("Chọn loại xuất!");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ================= HỦY =================
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}