using System;
using System.Windows.Forms;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class FrmNhapKho : Form
    {
        public FrmNhapKho()
        {
            InitializeComponent();
            this.Load += FrmNhapKho_Load;
        }

        // ================= TRẢ DỮ LIỆU =================
        public string PhieuNhap => txtPhieuNhap.Text;
        public string MaKho => txtMaKho.Text;
        public string MaSP => txtMaSP.Text;
        public string NhaCungCap => txtNhaCungCap.Text;

        public float GiaNhap
        {
            get
            {
                float.TryParse(txtGiaNhap.Text, out float value);
                return value;
            }
        }

        public int SoLuong
        {
            get
            {
                int.TryParse(txtSoLuong.Text, out int value);
                return value;
            }
        }

        // ================= LOAD =================
        private void FrmNhapKho_Load(object sender, EventArgs e)
        {
        }

        // ================= SET DATA (CHO SỬA) =================
        public void SetData(string phieu, string makho, string masp, string ncc, float gia, int sl)
        {
            txtPhieuNhap.Text = phieu;
            txtMaKho.Text = makho;
            txtMaSP.Text = masp;
            txtNhaCungCap.Text = ncc;
            txtGiaNhap.Text = gia.ToString();
            txtSoLuong.Text = sl.ToString();
        }

        // ================= LƯU =================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhieuNhap.Text))
            {
                MessageBox.Show("Nhập phiếu nhập!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaKho.Text))
            {
                MessageBox.Show("Nhập mã kho!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                MessageBox.Show("Nhập mã sản phẩm!");
                return;
            }

            if (!float.TryParse(txtGiaNhap.Text, out _))
            {
                MessageBox.Show("Giá nhập không hợp lệ!");
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out _))
            {
                MessageBox.Show("Số lượng không hợp lệ!");
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