using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class NhapKho : UserControl
    {
        public NhapKho()
        {
            InitializeComponent();
            this.Load += NhapKho_Load;
        }

        // ================= LOAD =================
        private void NhapKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // ================= LOAD DATA =================
        void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM NhapKho", conn);
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

                    string sql = @"INSERT INTO NhapKho
                    (PhieuNhap,MaKho,MaSP,NhaCungCap,GiaNhap,SoLuong)
                    VALUES
                    (@PhieuNhap,@MaKho,@MaSP,@NhaCungCap,@GiaNhap,@SoLuong)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    }

                    cmd.Parameters.AddWithValue("@PhieuNhap", txtPhieuNhap.Text);
                    cmd.Parameters.AddWithValue("@MaKho", txtMaKho.Text);
                    cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                    cmd.Parameters.AddWithValue("@NhaCungCap", txtNhaCungCap.Text);
                    cmd.Parameters.AddWithValue("@GiaNhap", float.Parse(txtGiaNhap.Text));
                    cmd.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));

                    cmd.ExecuteNonQuery();
                }

                LoadData();
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

                    string sql = @"UPDATE NhapKho 
                           SET MaKho=@MaKho,
                               MaSP=@MaSP,
                               NhaCungCap=@NhaCungCap,
                               GiaNhap=@GiaNhap,
                               SoLuong=@SoLuong
                           WHERE PhieuNhap=@PhieuNhap";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@PhieuNhap", txtPhieuNhap.Text);
                    cmd.Parameters.AddWithValue("@MaKho", txtMaKho.Text);
                    cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                    cmd.Parameters.AddWithValue("@NhaCungCap", txtNhaCungCap.Text);
                    cmd.Parameters.AddWithValue("@GiaNhap", float.Parse(txtGiaNhap.Text));
                    cmd.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));

                    cmd.ExecuteNonQuery();
                }
                    }

                LoadData();
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

                    string sql = "DELETE FROM NhapKho WHERE PhieuNhap=@PhieuNhap";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@PhieuNhap", txtPhieuNhap.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Click DataGridView
        private void dataGridViewNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtPhieuNhap.Text = row.Cells["PhieuNhap"].Value.ToString();
                txtMaKho.Text = row.Cells["MaKho"].Value.ToString();
                txtMaSP.Text = row.Cells["MaSP"].Value.ToString();
                txtNhaCungCap.Text = row.Cells["NhaCungCap"].Value.ToString();
                txtGiaNhap.Text = row.Cells["GiaNhap"].Value.ToString();
                txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString();
            }
        }
    }
}