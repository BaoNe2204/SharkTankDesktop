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

            // 🔥 ENTER để tìm
            txtSearch.KeyDown += txtMaSP_KeyDown;
        }

        private void NhapKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // ================= LOAD + TÌM =================
        void LoadData(string keyword = "")
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string sql = @"
SELECT * FROM NhapKho
WHERE 
    PhieuNhap LIKE @kw OR
    MaKho LIKE @kw OR
    MaSP LIKE @kw OR
    NhaCungCap LIKE @kw";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
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
            FrmNhapKho f = new FrmNhapKho();

            if (f.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"INSERT INTO NhapKho
                    (PhieuNhap, MaKho, MaSP, NhaCungCap, GiaNhap, SoLuong)
                    VALUES (@PhieuNhap, @MaKho, @MaSP, @NhaCungCap, @GiaNhap, @SoLuong)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@PhieuNhap", f.PhieuNhap);
                    cmd.Parameters.AddWithValue("@MaKho", f.MaKho);
                    cmd.Parameters.AddWithValue("@MaSP", f.MaSP);
                    cmd.Parameters.AddWithValue("@NhaCungCap", f.NhaCungCap);
                    cmd.Parameters.AddWithValue("@GiaNhap", f.GiaNhap);
                    cmd.Parameters.AddWithValue("@SoLuong", f.SoLuong);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
            }
        }

        // ================= SỬA =================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var row = dataGridView1.CurrentRow;

            string phieu = row.Cells["PhieuNhap"].Value.ToString();

            FrmNhapKho f = new FrmNhapKho();
            f.SetData(
                phieu,
                row.Cells["MaKho"].Value.ToString(),
                row.Cells["MaSP"].Value.ToString(),
                row.Cells["NhaCungCap"].Value.ToString(),
                float.Parse(row.Cells["GiaNhap"].Value.ToString()),
                int.Parse(row.Cells["SoLuong"].Value.ToString())
            );

            if (f.ShowDialog() == DialogResult.OK)
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

                    cmd.Parameters.AddWithValue("@PhieuNhap", phieu);
                    cmd.Parameters.AddWithValue("@MaKho", f.MaKho);
                    cmd.Parameters.AddWithValue("@MaSP", f.MaSP);
                    cmd.Parameters.AddWithValue("@NhaCungCap", f.NhaCungCap);
                    cmd.Parameters.AddWithValue("@GiaNhap", f.GiaNhap);
                    cmd.Parameters.AddWithValue("@SoLuong", f.SoLuong);

                    cmd.ExecuteNonQuery();
                }

                LoadData();
            }
        }

        // ================= XÓA =================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            string phieu = dataGridView1.CurrentRow.Cells["PhieuNhap"].Value.ToString();

            if (MessageBox.Show("Xóa?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No) return;

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM NhapKho WHERE PhieuNhap=@PhieuNhap", conn);

                cmd.Parameters.AddWithValue("@PhieuNhap", phieu);
                cmd.ExecuteNonQuery();
            }

            LoadData();
        }

        // ================= LÀM MỚI =================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }
    }
}