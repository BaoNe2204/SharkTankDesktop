using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class XuatKho : UserControl
    {
        public XuatKho()
        {
            InitializeComponent();
            this.Load += XuatKho_Load;

            // 🔥 ENTER để tìm
            txtSearch.KeyDown += txtMaSP_KeyDown;
        }

        private void XuatKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // ================= LOAD + TÌM =================
        void LoadData(string keyword = "")
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"
SELECT * FROM XuatKho
WHERE 
    PhieuXuat LIKE @kw OR
    MaKho LIKE @kw OR
    MaSP LIKE @kw OR
    LoaiXuat LIKE @kw";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load: " + ex.Message);
            }
        }

        // ================= ENTER TÌM =================
        private void txtMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData(txtSearch.Text);
                e.SuppressKeyPress = true;
            }
        }

        // ================= THÊM =================
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

        // ================= SỬA =================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chọn dòng cần sửa!");
                return;
            }

            var row = dataGridView1.CurrentRow;

            string phieu = row.Cells["PhieuXuat"].Value.ToString();

            FrmXuatKho f = new FrmXuatKho();
            f.SetData(
                phieu,
                row.Cells["MaSP"].Value.ToString(),
                row.Cells["MaKho"].Value.ToString(),
                int.Parse(row.Cells["SoLuong"].Value.ToString()),
                row.Cells["LoaiXuat"].Value.ToString()
            );

            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();

                        string sql = @"UPDATE XuatKho
                        SET MaKho=@MaKho,
                            MaSP=@MaSP,
                            LoaiXuat=@LoaiXuat,
                            SoLuong=@SoLuong
                        WHERE PhieuXuat=@PhieuXuat";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@PhieuXuat", phieu);
                        cmd.Parameters.AddWithValue("@MaKho", f.MaKho);
                        cmd.Parameters.AddWithValue("@MaSP", f.MaSP);
                        cmd.Parameters.AddWithValue("@LoaiXuat", f.LoaiXuat);
                        cmd.Parameters.AddWithValue("@SoLuong", f.SoLuong);

                        cmd.ExecuteNonQuery();
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

        // ================= XÓA =================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Chọn dòng cần xóa!");
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
                }

                LoadData();
                MessageBox.Show("Xóa thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        // ================= LÀM MỚI =================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }
    }
}