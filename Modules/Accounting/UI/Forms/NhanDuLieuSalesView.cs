using SharkTank.Core.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Accounting.UI.Forms
{
    public partial class NhanDuLieuSalesView : UserControl
    {
        private DataTable dtDonHangSales;

        public NhanDuLieuSalesView()
        {
            InitializeComponent();
            this.Load += NhanDuLieuSalesView_Load;
            cboTrangThaiSales.SelectedIndex = 0;
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.Value = DateTime.Now;
        }

        private void NhanDuLieuSalesView_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string trangThaiFilter = "";
                    string selectedStatus = cboTrangThaiSales.SelectedItem?.ToString() ?? "Tất cả";

                    if (selectedStatus == "Chưa nhập")
                        trangThaiFilter = "AND (TrangThaiKetoan IS NULL OR TrangThaiKetoan = 'Chưa nhập')";
                    else if (selectedStatus == "Đã nhập")
                        trangThaiFilter = "AND TrangThaiKetoan = 'Đã nhập'";
                    else if (selectedStatus == "Từ chối")
                        trangThaiFilter = "AND TrangThaiKetoan = 'Từ chối'";

                    string sql = $@"SELECT MaDonHang, NgayDatHang, TenKhachHang, SanPham, SoLuong, DonGia, ThanhTien, 
                                    TrangThai, TrangThaiKetoan, NgayTao
                                   FROM DonHangSales 
                                   WHERE NgayDatHang BETWEEN @TuNgay AND @DenNgay
                                   {trangThaiFilter}
                                   ORDER BY NgayDatHang DESC, NgayTao DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@TuNgay", dtpFromDate.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@DenNgay", dtpToDate.Value.Date);

                    dtDonHangSales = new DataTable();
                    da.Fill(dtDonHangSales);

                    dgvDonHangSales.DataSource = dtDonHangSales;
                    FormatDataGridView();
                    UpdateSummary();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvDonHangSales.Columns.Count == 0) return;

            dgvDonHangSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDonHangSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonHangSales.MultiSelect = true;
            dgvDonHangSales.ReadOnly = false;
            dgvDonHangSales.RowHeadersVisible = false;
            dgvDonHangSales.AllowUserToAddRows = false;
            dgvDonHangSales.BackgroundColor = Color.White;
            dgvDonHangSales.BorderStyle = BorderStyle.None;
            dgvDonHangSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDonHangSales.RowTemplate.Height = 30;

            dgvDonHangSales.Columns["MaDonHang"].HeaderText = "Mã đơn hàng";
            dgvDonHangSales.Columns["NgayDatHang"].HeaderText = "Ngày đặt";
            dgvDonHangSales.Columns["TenKhachHang"].HeaderText = "Khách hàng";
            dgvDonHangSales.Columns["SanPham"].HeaderText = "Sản phẩm";
            dgvDonHangSales.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvDonHangSales.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvDonHangSales.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvDonHangSales.Columns["TrangThai"].HeaderText = "Trạng thái Sales";
            dgvDonHangSales.Columns["TrangThaiKetoan"].HeaderText = "Trạng thái KT";
            dgvDonHangSales.Columns["NgayTao"].HeaderText = "Ngày tạo";

            dgvDonHangSales.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDonHangSales.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvDonHangSales.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            dgvDonHangSales.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDonHangSales.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDonHangSales.Columns["NgayDatHang"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvDonHangSales.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            dgvDonHangSales.Columns["colChon"].DisplayIndex = 0;
        }

        private void UpdateSummary()
        {
            int count = 0;
            decimal tongTien = 0;

            foreach (DataRow row in dtDonHangSales.Rows)
            {
                if (row["TrangThaiKetoan"] == DBNull.Value || row["TrangThaiKetoan"].ToString() == "Chưa nhập")
                {
                    count++;
                    if (row["ThanhTien"] != DBNull.Value)
                    {
                        tongTien += Convert.ToDecimal(row["ThanhTien"]);
                    }
                }
            }

            txtSoDonHang.Text = count.ToString();
            txtTongTien.Text = tongTien.ToString("N0") + " VNĐ";
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboTrangThaiSales.SelectedIndex = 0;
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.Value = DateTime.Now;
            LoadData();
        }

        private void btnNhapKetoan_Click(object sender, EventArgs e)
        {
            int selectedCount = 0;
            foreach (DataGridViewRow row in dgvDonHangSales.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colChon"].Value))
                {
                    selectedCount++;
                }
            }

            if (selectedCount == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một đơn hàng để nhập vào kế toán!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn nhập {selectedCount} đơn hàng vào kế toán?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    int successCount = 0;
                    decimal totalAmount = 0;

                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();

                        foreach (DataGridViewRow gridRow in dgvDonHangSales.Rows)
                        {
                            if (Convert.ToBoolean(gridRow.Cells["colChon"].Value))
                            {
                                string maDonHang = gridRow.Cells["MaDonHang"].Value.ToString();
                                string tenKhachHang = gridRow.Cells["TenKhachHang"].Value?.ToString() ?? "";
                                decimal thanhTien = Convert.ToDecimal(gridRow.Cells["ThanhTien"].Value);
                                DateTime ngayDatHang = Convert.ToDateTime(gridRow.Cells["NgayDatHang"].Value);
                                string sanPham = gridRow.Cells["SanPham"].Value?.ToString() ?? "";

                                string insertSql = @"INSERT INTO DoanhThu (MaDoanhThu, LoaiDoanhThu, NgayThu, NoiDung, SoTien, TrangThai, GhiChu, NgayTao)
                                                     VALUES (@MaDoanhThu, @LoaiDoanhThu, @NgayThu, @NoiDung, @SoTien, @TrangThai, @GhiChu, @NgayTao)";

                                SqlCommand cmd = new SqlCommand(insertSql, conn);
                                cmd.Parameters.AddWithValue("@MaDoanhThu", "DT" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(100, 999));
                                cmd.Parameters.AddWithValue("@LoaiDoanhThu", "Doanh thu bán hàng");
                                cmd.Parameters.AddWithValue("@NgayThu", ngayDatHang);
                                cmd.Parameters.AddWithValue("@NoiDung", $"Đơn hàng [{maDonHang}] - {sanPham} - Khách: {tenKhachHang}");
                                cmd.Parameters.AddWithValue("@SoTien", thanhTien);
                                cmd.Parameters.AddWithValue("@TrangThai", "Đã thu");
                                cmd.Parameters.AddWithValue("@GhiChu", $"Nhập từ Sales - Đơn hàng {maDonHang}");
                                cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                                cmd.ExecuteNonQuery();

                                string updateSql = "UPDATE DonHangSales SET TrangThaiKetoan = 'Đã nhập' WHERE MaDonHang = @MaDonHang";
                                SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                                updateCmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                                updateCmd.ExecuteNonQuery();

                                successCount++;
                                totalAmount += thanhTien;
                            }
                        }
                    }

                    MessageBox.Show(
                        $"Nhập thành công {successCount} đơn hàng!\nTổng tiền: {totalAmount.ToString("N0")} VNĐ",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nhập dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            int selectedCount = 0;
            foreach (DataGridViewRow row in dgvDonHangSales.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colChon"].Value))
                {
                    selectedCount++;
                }
            }

            if (selectedCount == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một đơn hàng để từ chối!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn từ chối {selectedCount} đơn hàng này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    int successCount = 0;

                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();

                        foreach (DataGridViewRow gridRow in dgvDonHangSales.Rows)
                        {
                            if (Convert.ToBoolean(gridRow.Cells["colChon"].Value))
                            {
                                string maDonHang = gridRow.Cells["MaDonHang"].Value.ToString();

                                string sql = "UPDATE DonHangSales SET TrangThaiKetoan = 'Từ chối' WHERE MaDonHang = @MaDonHang";
                                SqlCommand cmd = new SqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                                cmd.ExecuteNonQuery();

                                successCount++;
                            }
                        }
                    }

                    MessageBox.Show($"Từ chối thành công {successCount} đơn hàng!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi từ chối dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvDonHangSales.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx|Excel 97-2003|*.xls|CSV Files|*.csv";
                saveFileDialog.FileName = "DonHangSales_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Xuất file thành công!\nĐường dẫn: " + saveFileDialog.FileName,
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
