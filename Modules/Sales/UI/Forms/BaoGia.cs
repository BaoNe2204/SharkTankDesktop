using System;
using System.Windows.Forms;

using SharkTankDesktop.Modules.Sales.BLL;
namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class BaoGia : UserControl
    {
        private BaoGiaBLL _bll = new BaoGiaBLL();

        public BaoGia()
        {
            InitializeComponent();

            btnTaoBaoGia.Click += BtnTaoBaoGia_Click;
            btnDuyet.Click += BtnDuyet_Click;

            LoadData();
        }

        public void LoadData()
        {
            try
            {
                dgvBaoGia.DataSource = _bll.GetDanhSachBaoGia();
            }
            catch { /* Có thể log lỗi ở đây */ }
        }

        private void BtnTaoBaoGia_Click(object sender, EventArgs e)
        {
            TaoBaoGia frmTaoMoi = new TaoBaoGia();
            frmTaoMoi.ShowDialog();
            LoadData();
        }

        private void BtnDuyet_Click(object sender, EventArgs e)
        {
            if (dgvBaoGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một báo giá bên dưới để duyệt!", "Lưu ý");
                return;
            }

            string maBG = dgvBaoGia.CurrentRow.Cells["Mã BG"].Value.ToString();
            string trangThai = dgvBaoGia.CurrentRow.Cells["Trạng Thái"].Value.ToString();

            if (trangThai != "Mới lập")
            {
                MessageBox.Show("Chỉ có thể duyệt báo giá ở trạng thái 'Mới lập'!", "Lưu ý");
                return;
            }

            if (_bll.DuyetBaoGia(maBG))
            {
                MessageBox.Show($"Báo giá {maBG} đã được duyệt và chốt!", "Thành công");
                LoadData();
            }
        }
    }
}