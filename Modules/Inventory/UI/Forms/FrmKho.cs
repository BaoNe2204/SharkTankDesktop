using System;
using System.Windows.Forms;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class FrmKho : Form
    {
        public string MaKho { get; set; }
        public string TenKho { get; set; }
        public string DiaChi { get; set; }

        public FrmKho()
        {
            InitializeComponent();
        }

        // Đổ dữ liệu khi sửa
        public void SetData(string makho, string tenkho, string diachi)
        {
            txtMaKho.Text = makho;
            txtTenKho.Text = tenkho;
            txtDiaChi.Text = diachi;
        }

        // Nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaKho.Text == "" || txtTenKho.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu");
                return;
            }

            MaKho = txtMaKho.Text;
            TenKho = txtTenKho.Text;
            DiaChi = txtDiaChi.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Nút Hủy
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}