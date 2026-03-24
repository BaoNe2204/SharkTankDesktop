using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class FrmSanPham : Form
    {
        // ================= TRẢ DỮ LIỆU =================
        public string MaSP => txtMaSP.Text;
        public string NhomHang => txtNhomHang.Text;
        public string DonViTinh => txtDonViTinh.Text;

        public float GiaNhap
        {
            get
            {
                float.TryParse(txtGiaNhap.Text, out float value);
                return value;
            }
        }

        public float GiaBan
        {
            get
            {
                float.TryParse(txtGiaBan.Text, out float value);
                return value;
            }
        }

        public FrmSanPham()
        {
            InitializeComponent();
        }

        // ================= SET DATA (CHO SỬA) =================
        public void SetData(string ma, string nhom, string dvt, float giaNhap, float giaBan)
        {
            txtMaSP.Text = ma;
            txtNhomHang.Text = nhom;
            txtDonViTinh.Text = dvt;
            txtGiaNhap.Text = giaNhap.ToString();
            txtGiaBan.Text = giaBan.ToString();

        }

        // ================= NÚT LƯU =================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu
            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm!");
                return;
            }

            if (string.IsNullOrEmpty(txtGiaNhap.Text) || string.IsNullOrEmpty(txtGiaBan.Text))
            {
                MessageBox.Show("Vui lòng nhập giá!");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ================= NÚT HỦY =================
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}