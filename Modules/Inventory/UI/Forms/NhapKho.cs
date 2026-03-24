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

        // ================= THÊM =================
        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmNhapKho f = new FrmNhapKho();

            if (f.ShowDialog() == DialogResult.OK) // ✅ FIX: bỏ Form trung gian
            {
                try
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
                MessageBox.Show("Chọn phiếu cần sửa!");
                return;
            }

            DataGridViewRow row = dataGridView1.CurrentRow;

            string phieu = row.Cells["PhieuNhap"].Value.ToString();
            string makho = row.Cells["MaKho"].Value.ToString();
            string masp = row.Cells["MaSP"].Value.ToString();
            string ncc = row.Cells["NhaCungCap"].Value.ToString();
            float gia = float.Parse(row.Cells["GiaNhap"].Value.ToString());
            int sl = int.Parse(row.Cells["SoLuong"].Value.ToString());

            FrmNhapKho f = new FrmNhapKho();
            f.SetData(phieu, makho, masp, ncc, gia, sl);

            if (f.ShowDialog() == DialogResult.OK) // ✅ FIX
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

                        cmd.Parameters.AddWithValue("@PhieuNhap", phieu);
                        cmd.Parameters.AddWithValue("@MaKho", f.MaKho);
                        cmd.Parameters.AddWithValue("@MaSP", f.MaSP);
                        cmd.Parameters.AddWithValue("@NhaCungCap", f.NhaCungCap);
                        cmd.Parameters.AddWithValue("@GiaNhap", f.GiaNhap);
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
                MessageBox.Show("Chọn phiếu cần xóa!");
                return;
            }

            string phieu = dataGridView1.CurrentRow.Cells["PhieuNhap"].Value.ToString();

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM NhapKho WHERE PhieuNhap=@PhieuNhap", conn);

                    cmd.Parameters.AddWithValue("@PhieuNhap", phieu);
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
            LoadData();
        }
    }
}