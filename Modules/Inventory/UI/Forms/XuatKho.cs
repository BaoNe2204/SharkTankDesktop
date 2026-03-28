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
        }

        // ================= LOAD =================
        private void XuatKho_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            LoadData();
        }

        // ================= LOAD DATA =================
=======
            cboLoaiXuat.Items.Add("Bán hàng");
            cboLoaiXuat.Items.Add("Nội bộ");

            LoadData();
        }

        // Load dữ liệu
>>>>>>> main
        void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
<<<<<<< HEAD
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

        // ================= TÌM KIẾM =================
        void TimKiem()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT * FROM XuatKho
                                   WHERE PhieuXuat LIKE @key
                                   OR MaSP LIKE @key
                                   OR MaKho LIKE @key";
=======
                    conn.Open();

                    string sql = "SELECT * FROM XuatKho";
>>>>>>> main

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@key", "%" + txtSearch.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // ================= ENTER SEARCH =================
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKiem();
                e.SuppressKeyPress = true;
            }
        }

        // ================= THÊM =================
=======
                MessageBox.Show(ex.Message);
            }
        }

        // Thêm
>>>>>>> main
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
                    (PhieuXuat, MaKho, MaSP, SoLuong, LoaiXuat)
                    VALUES(@PhieuXuat,@MaKho,@MaSP,@SoLuong,@LoaiXuat)";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@PhieuXuat", txtPhieuXuat.Text);
                    cmd.Parameters.AddWithValue("@MaKho", txtMaKho.Text);
                    cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                    cmd.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));
                    cmd.Parameters.AddWithValue("@LoaiXuat", cboLoaiXuat.Text);

                        cmd.ExecuteNonQuery();
                }

                        cmd.ExecuteNonQuery();
                    }

                    LoadData();
                MessageBox.Show("Thêm thành công");
                }
                catch (Exception ex)
                {
                MessageBox.Show(ex.Message);
            }
        }

        // Sửa
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
                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();

                        string sql = @"UPDATE XuatKho
                    SET MaKho=@MaKho,
                        MaSP=@MaSP,
                        SoLuong=@SoLuong,
                        LoaiXuat=@LoaiXuat
                        WHERE PhieuXuat=@PhieuXuat";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@PhieuXuat", phieu);
                        cmd.Parameters.AddWithValue("@MaKho", f.MaKho);
                        cmd.Parameters.AddWithValue("@MaSP", f.MaSP);
                        cmd.Parameters.AddWithValue("@LoaiXuat", f.LoaiXuat);
                        cmd.Parameters.AddWithValue("@SoLuong", f.SoLuong);

                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.AddWithValue("@PhieuXuat", txtPhieuXuat.Text);
                    cmd.Parameters.AddWithValue("@MaKho", txtMaKho.Text);
                    cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                    cmd.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));
                    cmd.Parameters.AddWithValue("@LoaiXuat", cboLoaiXuat.Text);

                    cmd.ExecuteNonQuery();
                    }

                    LoadData();
                MessageBox.Show("Sửa thành công");
                }
                catch (Exception ex)
                {
                MessageBox.Show(ex.Message);
            }
        }

        // Xóa
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

<<<<<<< HEAD
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM XuatKho WHERE PhieuXuat=@PhieuXuat", conn);

                    cmd.Parameters.AddWithValue("@PhieuXuat", phieu);
=======
                    string sql = "DELETE FROM XuatKho WHERE PhieuXuat=@PhieuXuat";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@PhieuXuat", txtPhieuXuat.Text);

>>>>>>> main
                    cmd.ExecuteNonQuery();
                }

                LoadData();
<<<<<<< HEAD
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
=======
                MessageBox.Show("Xóa thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Click DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
        {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtPhieuXuat.Text = row.Cells["PhieuXuat"].Value.ToString();
                txtMaKho.Text = row.Cells["MaKho"].Value.ToString();
                txtMaSP.Text = row.Cells["MaSP"].Value.ToString();
                txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString();
                cboLoaiXuat.Text = row.Cells["LoaiXuat"].Value.ToString();
            }
>>>>>>> main
        }
    }
}