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

        private void XuatKho_Load(object sender, EventArgs e)
        {
            cboLoaiXuat.Items.Add("Bán hàng");
            cboLoaiXuat.Items.Add("Nội bộ");

            LoadData();
        }

        // Load dữ liệu
        void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = "SELECT * FROM XuatKho";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Thêm
        private void btnThem_Click(object sender, EventArgs e)
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
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = "DELETE FROM XuatKho WHERE PhieuXuat=@PhieuXuat";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@PhieuXuat", txtPhieuXuat.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
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
        }
    }
}