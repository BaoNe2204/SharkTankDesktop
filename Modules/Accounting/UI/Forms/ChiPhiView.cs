using SharkTank.Core.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Accounting.UI.Forms
{
    public partial class ChiPhiView : UserControl
    {
        private DataTable dtChiPhi;
        private string currentMaChiPhi = null;
        private bool isEditMode = false;

        public ChiPhiView()
        {
            InitializeComponent();
            this.Load += ChiPhiView_Load;
            txtSearch.KeyDown += txtSearch_KeyDown;
            cboTrangThai.SelectedIndex = 0;
            cboLoaiChiPhi.SelectedIndex = 0;
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpToDate.Value = DateTime.Now;
        }

        private void ChiPhiView_Load(object sender, EventArgs e)
        {
            LoadData();
            GenerateMaChiPhi();
        }

        private void GenerateMaChiPhi()
        {
            txtMaChiPhi.Text = "CP" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT MaChiPhi, LoaiChiPhi, NgayChi, NoiDung, SoTien, TrangThai, GhiChu, NgayTao 
                                   FROM ChiPhi 
                                   WHERE NgayChi BETWEEN @TuNgay AND @DenNgay
                                   ORDER BY NgayChi DESC, NgayTao DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@TuNgay", dtpFromDate.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@DenNgay", dtpToDate.Value.Date);

                    dtChiPhi = new DataTable();
                    da.Fill(dtChiPhi);

                    dgvChiPhi.DataSource = dtChiPhi;
                    FormatDataGridView();
                    CalculateTongChiPhi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvChiPhi.Columns.Count == 0) return;

            dgvChiPhi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiPhi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiPhi.MultiSelect = false;
            dgvChiPhi.ReadOnly = true;
            dgvChiPhi.RowHeadersVisible = false;
            dgvChiPhi.AllowUserToAddRows = false;
            dgvChiPhi.BackgroundColor = Color.White;
            dgvChiPhi.BorderStyle = BorderStyle.None;
            dgvChiPhi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChiPhi.RowTemplate.Height = 30;

            dgvChiPhi.Columns["MaChiPhi"].HeaderText = "Mã chi phí";
            dgvChiPhi.Columns["LoaiChiPhi"].HeaderText = "Loại chi phí";
            dgvChiPhi.Columns["NgayChi"].HeaderText = "Ngày chi";
            dgvChiPhi.Columns["NoiDung"].HeaderText = "Nội dung";
            dgvChiPhi.Columns["SoTien"].HeaderText = "Số tiền (VNĐ)";
            dgvChiPhi.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvChiPhi.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvChiPhi.Columns["NgayTao"].HeaderText = "Ngày tạo";

            dgvChiPhi.Columns["SoTien"].DefaultCellStyle.Format = "N0";
            dgvChiPhi.Columns["SoTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChiPhi.Columns["NgayChi"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvChiPhi.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        private void CalculateTongChiPhi()
        {
            decimal tongChiPhi = 0;
            foreach (DataRow row in dtChiPhi.Rows)
            {
                if (row["SoTien"] != DBNull.Value)
                {
                    tongChiPhi += Convert.ToDecimal(row["SoTien"]);
                }
            }
            txtTongChiPhi.Text = tongChiPhi.ToString("N0") + " VNĐ";
        }

        private void TimKiem()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT MaChiPhi, LoaiChiPhi, NgayChi, NoiDung, SoTien, TrangThai, GhiChu, NgayTao 
                                   FROM ChiPhi 
                                   WHERE (MaChiPhi LIKE @key OR NoiDung LIKE @key OR LoaiChiPhi LIKE @key)
                                   AND NgayChi BETWEEN @TuNgay AND @DenNgay
                                   ORDER BY NgayChi DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@key", "%" + txtSearch.Text + "%");
                    da.SelectCommand.Parameters.AddWithValue("@TuNgay", dtpFromDate.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@DenNgay", dtpToDate.Value.Date);

                    dtChiPhi = new DataTable();
                    da.Fill(dtChiPhi);

                    dgvChiPhi.DataSource = dtChiPhi;
                    FormatDataGridView();
                    CalculateTongChiPhi();
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
            GenerateMaChiPhi();
            isEditMode = false;
            currentMaChiPhi = null;
            txtMaChiPhi.Enabled = false;
            EnableForm(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvChiPhi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn chi phí cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isEditMode = true;
            LoadSelectedRow();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChiPhi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn chi phí cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maChiPhi = dgvChiPhi.CurrentRow.Cells["MaChiPhi"].Value.ToString();

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa chi phí [" + maChiPhi + "]?",
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
                        SqlCommand cmd = new SqlCommand("DELETE FROM ChiPhi WHERE MaChiPhi = @MaChiPhi", conn);
                        cmd.Parameters.AddWithValue("@MaChiPhi", maChiPhi);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Xóa chi phí thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa chi phí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung chi phí!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    if (isEditMode && !string.IsNullOrEmpty(currentMaChiPhi))
                    {
                        sql = @"UPDATE ChiPhi SET 
                                LoaiChiPhi = @LoaiChiPhi,
                                NgayChi = @NgayChi,
                                NoiDung = @NoiDung,
                                SoTien = @SoTien,
                                TrangThai = @TrangThai,
                                GhiChu = @GhiChu
                                WHERE MaChiPhi = @MaChiPhi";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MaChiPhi", currentMaChiPhi);
                        cmd.Parameters.AddWithValue("@LoaiChiPhi", cboLoaiChiPhi.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@NgayChi", dtpNgayChi.Value.Date);
                        cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoTien", soTien);
                        cmd.Parameters.AddWithValue("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Chưa thanh toán");
                        cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text.Trim());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật chi phí thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        sql = @"INSERT INTO ChiPhi (MaChiPhi, LoaiChiPhi, NgayChi, NoiDung, SoTien, TrangThai, GhiChu, NgayTao)
                                VALUES (@MaChiPhi, @LoaiChiPhi, @NgayChi, @NoiDung, @SoTien, @TrangThai, @GhiChu, @NgayTao)";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MaChiPhi", txtMaChiPhi.Text.Trim());
                        cmd.Parameters.AddWithValue("@LoaiChiPhi", cboLoaiChiPhi.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@NgayChi", dtpNgayChi.Value.Date);
                        cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoTien", soTien);
                        cmd.Parameters.AddWithValue("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Chưa thanh toán");
                        cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm chi phí thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadData();
                ClearForm();
                GenerateMaChiPhi();
                EnableForm(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu chi phí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearForm();
            EnableForm(false);
            isEditMode = false;
            currentMaChiPhi = null;
        }

        private void dgvChiPhi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LoadSelectedRow();
            }
        }

        private void LoadSelectedRow()
        {
            DataGridViewRow row = dgvChiPhi.CurrentRow;
            if (row != null)
            {
                currentMaChiPhi = row.Cells["MaChiPhi"].Value.ToString();
                txtMaChiPhi.Text = currentMaChiPhi;
                txtMaChiPhi.Enabled = false;

                cboLoaiChiPhi.SelectedItem = row.Cells["LoaiChiPhi"].Value?.ToString() ?? "";
                dtpNgayChi.Value = Convert.ToDateTime(row.Cells["NgayChi"].Value);
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
            GenerateMaChiPhi();
            txtMaChiPhi.Enabled = false;
            cboLoaiChiPhi.SelectedIndex = 0;
            dtpNgayChi.Value = DateTime.Now;
            txtNoiDung.Clear();
            txtSoTien.Clear();
            cboTrangThai.SelectedIndex = 0;
            txtGhiChu.Clear();
            currentMaChiPhi = null;
            isEditMode = false;
        }

        private void EnableForm(bool enable)
        {
            cboLoaiChiPhi.Enabled = enable;
            dtpNgayChi.Enabled = enable;
            txtNoiDung.Enabled = enable;
            txtSoTien.Enabled = enable;
            cboTrangThai.Enabled = enable;
            txtGhiChu.Enabled = enable;
            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (dgvChiPhi.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dgvChiPhi.MultiSelect = false;
                dgvChiPhi.SelectAll();
                dgvChiPhi.ClearSelection();

                MessageBox.Show("Chức năng in đang được phát triển!\nDữ liệu: " + dtChiPhi.Rows.Count + " dòng",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvChiPhi.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx|Excel 97-2003|*.xls|CSV Files|*.csv";
                saveFileDialog.FileName = "ChiPhi_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

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
