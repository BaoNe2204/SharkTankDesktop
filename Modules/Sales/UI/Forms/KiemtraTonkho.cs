using System;
using System.Data;
using System.Windows.Forms;
using SharkTank.Modules.Sales.DAL; // Nhớ check lại đường dẫn thư mục DAL

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class KiemtraTonkho : UserControl
    {
        TonKhoDAL tonKhoDAL = new TonKhoDAL();

        public string MaSanPhamDuocChon { get; private set; }
        public string TenSanPhamDuocChon { get; private set; }
        public decimal GiaBanDuocChon { get; private set; }

        public event Action SanPhamDaDuocChon;
        public event Action YeuCauDongGiaoDien;

        public KiemtraTonkho()
        {
            InitializeComponent();

            this.Load += LienKetKho_Load;
            btnDong.Click += BtnDong_Click;
            btnChon.Click += BtnChon_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            dgvTonKho.CellDoubleClick += DgvTonKho_CellDoubleClick;
        }

        private void LienKetKho_Load(object sender, EventArgs e)
        {
            LoadDanhSachSanPham("");
        }

        private void LoadDanhSachSanPham(string tuKhoa)
        {
            try
            {
                DataTable dt = tonKhoDAL.GetDanhSachTonKho(tuKhoa);
                dgvTonKho.DataSource = dt;

                if (dgvTonKho.Columns.Count > 0)
                {
                    dgvTonKho.Columns["Giá Bán"].DefaultCellStyle.Format = "N0";
                    dgvTonKho.Columns["Số Lượng Tồn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            LoadDanhSachSanPham(tuKhoa);
        }

        private void BtnChon_Click(object sender, EventArgs e)
        {
            if (dgvTonKho.CurrentRow != null)
            {
                int tonKho = Convert.ToInt32(dgvTonKho.CurrentRow.Cells["Số Lượng Tồn"].Value);

                if (tonKho <= 0)
                {
                    MessageBox.Show("Sản phẩm này đã hết hàng trong kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MaSanPhamDuocChon = dgvTonKho.CurrentRow.Cells["Mã SP"].Value.ToString();
                TenSanPhamDuocChon = dgvTonKho.CurrentRow.Cells["Tên Sản Phẩm"].Value.ToString();
                GiaBanDuocChon = Convert.ToDecimal(dgvTonKho.CurrentRow.Cells["Giá Bán"].Value);

                SanPhamDaDuocChon?.Invoke();
            }
            else
            {
                MessageBox.Show("Vui lòng click chọn một sản phẩm trong bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void DgvTonKho_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnChon_Click(sender, e);
            }
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.Hide();
            YeuCauDongGiaoDien?.Invoke();
        }
    }
}