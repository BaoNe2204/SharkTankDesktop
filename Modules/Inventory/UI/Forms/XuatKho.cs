using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.BLL;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class XuatKho : UserControl
    {
        public XuatKho()
        {
            InitializeComponent();
            this.Load += XuatKho_Load;
        }

        private void XuatKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM XuatKho", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;
                    dataGridView1.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        void TimKiem()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT * FROM XuatKho
                                   WHERE PhieuXuat LIKE @key OR MaSP LIKE @key OR MaKho LIKE @key";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@key", "%" + txtSearch.Text + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
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

        // THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmXuatKho f = new FrmXuatKho();
            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();
                        string sql = @"INSERT INTO XuatKho
                        (PhieuXuat, MaKho, MaSP, LoaiXuat, SoLuong)
                        VALUES (@PhieuXuat, @MaKho, @MaSP, @LoaiXuat, @SoLuong)";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@PhieuXuat", f.PhieuXuat);
                        cmd.Parameters.AddWithValue("@MaKho", f.MaKho);
                        cmd.Parameters.AddWithValue("@MaSP", f.MaSP);
                        cmd.Parameters.AddWithValue("@LoaiXuat", f.LoaiXuat);
                        cmd.Parameters.AddWithValue("@SoLuong", f.SoLuong);
                        cmd.ExecuteNonQuery();

                        // Ghi DataChangeLogs + AuditLogs
                        AuditHelper.Insert("XuatKho", f.PhieuXuat, f.PhieuXuat,
                            new XuatKhoSnapshot
                            {
                                PhieuXuat = f.PhieuXuat,
                                MaKho = f.MaKho,
                                MaSP = f.MaSP,
                                LoaiXuat = f.LoaiXuat,
                                SoLuong = f.SoLuong.ToString()
                            });
                    }
                    LoadData();
                    MessageBox.Show("Thêm thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm: " + ex.Message);
                }
            }
        }

        // SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chọn phiếu cần sửa!");
                return;
            }

            DataGridViewRow row = dataGridView1.CurrentRow;
            string phieu = row.Cells["PhieuXuat"].Value.ToString();
            string makho = row.Cells["MaKho"].Value.ToString();
            string masp = row.Cells["MaSP"].Value.ToString();
            string loai = row.Cells["LoaiXuat"].Value.ToString();
            int sl = int.Parse(row.Cells["SoLuong"].Value.ToString());

            FrmXuatKho f = new FrmXuatKho();
            f.SetData(phieu, masp, makho, sl, loai);

            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Đọc dữ liệu cũ
                    var oldSnap = XuatKhoSnapshot.FromDb(phieu);
                    var newSnap = new XuatKhoSnapshot
                    {
                        PhieuXuat = phieu,
                        MaKho = f.MaKho,
                        MaSP = f.MaSP,
                        LoaiXuat = f.LoaiXuat,
                        SoLuong = f.SoLuong.ToString()
                    };

                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();
                        string sql = @"UPDATE XuatKho
                        SET MaKho=@MaKho, MaSP=@MaSP, LoaiXuat=@LoaiXuat, SoLuong=@SoLuong
                        WHERE PhieuXuat=@PhieuXuat";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@PhieuXuat", phieu);
                        cmd.Parameters.AddWithValue("@MaKho", f.MaKho);
                        cmd.Parameters.AddWithValue("@MaSP", f.MaSP);
                        cmd.Parameters.AddWithValue("@LoaiXuat", f.LoaiXuat);
                        cmd.Parameters.AddWithValue("@SoLuong", f.SoLuong);
                        cmd.ExecuteNonQuery();

                        // Ghi DataChangeLogs + AuditLogs
                        AuditHelper.Update("XuatKho", phieu, f.PhieuXuat, oldSnap, newSnap);
                    }
                    LoadData();
                    MessageBox.Show("Sửa thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi sửa: " + ex.Message);
                }
            }
        }

        // XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chọn phiếu cần xóa!");
                return;
            }

            string phieu = dataGridView1.CurrentRow.Cells["PhieuXuat"].Value.ToString();

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM XuatKho WHERE PhieuXuat=@PhieuXuat", conn);
                    cmd.Parameters.AddWithValue("@PhieuXuat", phieu);
                    cmd.ExecuteNonQuery();

                    // Ghi DataChangeLogs + AuditLogs
                    AuditHelper.Delete("XuatKho", phieu, phieu, "PhieuXuat");
                }
                LoadData();
                MessageBox.Show("Xóa thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }
    }
}
