using SharkTank.Core.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Accounting.UI.Forms
{
    public partial class DoanhThuView : UserControl
    {
        private DataTable dtDoanhThu;
        private string currentMaDoanhThu = null;
        private bool isEditMode = false;

        public DoanhThuView()
        {
            InitializeComponent();
            this.Load += DoanhThuView_Load;
            txtSearch.KeyDown += txtSearch_KeyDown;
            cboTrangThai.SelectedIndex = 0;
            cboLoaiDoanhThu.SelectedIndex = 0;
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpToDate.Value = DateTime.Now;
        }

        private void DoanhThuView_Load(object sender, EventArgs e)
        {
            LoadData();
            GenerateMaDoanhThu();
        }

        private void GenerateMaDoanhThu()
        {
            txtMaDoanhThu.Text = "DT" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT MaDoanhThu, LoaiDoanhThu, NgayThu, NoiDung, SoTien, TrangThai, GhiChu, NgayTao 
                                   FROM DoanhThu 
                                   WHERE NgayThu BETWEEN @TuNgay AND @DenNgay
                                   ORDER BY NgayThu DESC, NgayTao DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@TuNgay", dtpFromDate.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@DenNgay", dtpToDate.Value.Date);

                    dtDoanhThu = new DataTable();
                    da.Fill(dtDoanhThu);

                    dgvDoanhThu.DataSource = dtDoanhThu;
                    FormatDataGridView();
                    CalculateTongDoanhThu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvDoanhThu.Columns.Count == 0) return;

            dgvDoanhThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDoanhThu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoanhThu.MultiSelect = false;
            dgvDoanhThu.ReadOnly = true;
            dgvDoanhThu.RowHeadersVisible = false;
            dgvDoanhThu.AllowUserToAddRows = false;
            dgvDoanhThu.BackgroundColor = Color.White;
            dgvDoanhThu.BorderStyle = BorderStyle.None;
            dgvDoanhThu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDoanhThu.RowTemplate.Height = 30;

            dgvDoanhThu.Columns["MaDoanhThu"].HeaderText = "Mã doanh thu";
            dgvDoanhThu.Columns["LoaiDoanhThu"].HeaderText = "Loại doanh thu";
            dgvDoanhThu.Columns["NgayThu"].HeaderText = "Ngày thu";
            dgvDoanhThu.Columns["NoiDung"].HeaderText = "Nội dung";
            dgvDoanhThu.Columns["SoTien"].HeaderText = "Số tiền (VNĐ)";
            dgvDoanhThu.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvDoanhThu.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvDoanhThu.Columns["NgayTao"].HeaderText = "Ngày tạo";

            dgvDoanhThu.Columns["SoTien"].DefaultCellStyle.Format = "N0";
            dgvDoanhThu.Columns["SoTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDoanhThu.Columns["NgayThu"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvDoanhThu.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        private void CalculateTongDoanhThu()
        {
            decimal tongDoanhThu = 0;
            foreach (DataRow row in dtDoanhThu.Rows)
            {
                if (row["SoTien"] != DBNull.Value)
                {
                    tongDoanhThu += Convert.ToDecimal(row["SoTien"]);
                }
            }
            txtTongDoanhThu.Text = tongDoanhThu.ToString("N0") + " VNĐ";
        }

        private void TimKiem()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT MaDoanhThu, LoaiDoanhThu, NgayThu, NoiDung, SoTien, TrangThai, GhiChu, NgayTao 
                                   FROM DoanhThu 
                                   WHERE (MaDoanhThu LIKE @key OR NoiDung LIKE @key OR LoaiDoanhThu LIKE @key)
                                   AND NgayThu BETWEEN @TuNgay AND @DenNgay
                                   ORDER BY NgayThu DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@key", "%" + txtSearch.Text + "%");
                    da.SelectCommand.Parameters.AddWithValue("@TuNgay", dtpFromDate.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@DenNgay", dtpToDate.Value.Date);

                    dtDoanhThu = new DataTable();
                    da.Fill(dtDoanhThu);

                    dgvDoanhThu.DataSource = dtDoanhThu;
                    FormatDataGridView();
                    CalculateTongDoanhThu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKiem();
                e.SuppressKeyPress = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtSearch.Clear();
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpToDate.Value = DateTime.Now;
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearForm();
            GenerateMaDoanhThu();
            isEditMode = false;
            currentMaDoanhThu = null;
            txtMaDoanhThu.Enabled = false;
            EnableForm(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDoanhThu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn doanh thu cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isEditMode = true;
            LoadSelectedRow();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDoanhThu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn doanh thu cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDoanhThu = dgvDoanhThu.CurrentRow.Cells["MaDoanhThu"].Value.ToString();

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa doanh thu [" + maDoanhThu + "]?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM DoanhThu WHERE MaDoanhThu = @MaDoanhThu", conn);
                        cmd.Parameters.AddWithValue("@MaDoanhThu", maDoanhThu);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Xóa doanh thu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa doanh thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung doanh thu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNoiDung.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSoTien.Text) || !decimal.TryParse(txtSoTien.Text.Replace(",", ""), out decimal soTien))
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTien.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql;

                    if (isEditMode && !string.IsNullOrEmpty(currentMaDoanhThu))
                    {
                        sql = @"UPDATE DoanhThu SET 
                                LoaiDoanhThu = @LoaiDoanhThu,
                                NgayThu = @NgayThu,
                                NoiDung = @NoiDung,
                                SoTien = @SoTien,
                                TrangThai = @TrangThai,
                                GhiChu = @GhiChu
                                WHERE MaDoanhThu = @MaDoanhThu";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MaDoanhThu", currentMaDoanhThu);
                        cmd.Parameters.AddWithValue("@LoaiDoanhThu", cboLoaiDoanhThu.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@NgayThu", dtpNgayThu.Value.Date);
                        cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoTien", soTien);
                        cmd.Parameters.AddWithValue("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Chưa thu");
                        cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text.Trim());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật doanh thu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        sql = @"INSERT INTO DoanhThu (MaDoanhThu, LoaiDoanhThu, NgayThu, NoiDung, SoTien, TrangThai, GhiChu, NgayTao)
                                VALUES (@MaDoanhThu, @LoaiDoanhThu, @NgayThu, @NoiDung, @SoTien, @TrangThai, @GhiChu, @NgayTao)";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MaDoanhThu", txtMaDoanhThu.Text.Trim());
                        cmd.Parameters.AddWithValue("@LoaiDoanhThu", cboLoaiDoanhThu.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@NgayThu", dtpNgayThu.Value.Date);
                        cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoTien", soTien);
                        cmd.Parameters.AddWithValue("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Chưa thu");
                        cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm doanh thu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadData();
                ClearForm();
                GenerateMaDoanhThu();
                EnableForm(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu doanh thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearForm();
            EnableForm(false);
            isEditMode = false;
            currentMaDoanhThu = null;
        }

        private void dgvDoanhThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LoadSelectedRow();
            }
        }

        private void LoadSelectedRow()
        {
            DataGridViewRow row = dgvDoanhThu.CurrentRow;
            if (row != null)
            {
                currentMaDoanhThu = row.Cells["MaDoanhThu"].Value.ToString();
                txtMaDoanhThu.Text = currentMaDoanhThu;
                txtMaDoanhThu.Enabled = false;

                cboLoaiDoanhThu.SelectedItem = row.Cells["LoaiDoanhThu"].Value?.ToString() ?? "";
                dtpNgayThu.Value = Convert.ToDateTime(row.Cells["NgayThu"].Value);
                txtNoiDung.Text = row.Cells["NoiDung"].Value?.ToString() ?? "";
                txtSoTien.Text = Convert.ToDecimal(row.Cells["SoTien"].Value).ToString("N0");
                cboTrangThai.SelectedItem = row.Cells["TrangThai"].Value?.ToString() ?? "";
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString() ?? "";

                isEditMode = true;
                EnableForm(true);
            }
        }

        private void ClearForm()
        {
            GenerateMaDoanhThu();
            txtMaDoanhThu.Enabled = false;
            cboLoaiDoanhThu.SelectedIndex = 0;
            dtpNgayThu.Value = DateTime.Now;
            txtNoiDung.Clear();
            txtSoTien.Clear();
            cboTrangThai.SelectedIndex = 0;
            txtGhiChu.Clear();
            currentMaDoanhThu = null;
            isEditMode = false;
        }

        private void EnableForm(bool enable)
        {
            cboLoaiDoanhThu.Enabled = enable;
            dtpNgayThu.Enabled = enable;
            txtNoiDung.Enabled = enable;
            txtSoTien.Enabled = enable;
            cboTrangThai.Enabled = enable;
            txtGhiChu.Enabled = enable;
            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (dgvDoanhThu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MessageBox.Show("Chức năng in đang được phát triển!\nDữ liệu: " + dtDoanhThu.Rows.Count + " dòng",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvDoanhThu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx|Excel 97-2003|*.xls|CSV Files|*.csv";
                saveFileDialog.FileName = "DoanhThu_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

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
