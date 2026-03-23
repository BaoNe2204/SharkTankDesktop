using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class DieuChinhTon : UserControl
    {
        public DieuChinhTon()
        {
            InitializeComponent();
            this.Load += DieuChinhTon_Load;
        }

        // Load form
        private void DieuChinhTon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Hiển thị tồn kho
        void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"
                    SELECT 
                        sp.MaSP,
                        ISNULL(SUM(nk.SoLuong),0) AS TongNhap,
                        ISNULL(SUM(xk.SoLuong),0) AS TongXuat,
                        ISNULL(SUM(dc.SoLuongDieuChinh),0) AS DieuChinh,
                        ISNULL(SUM(nk.SoLuong),0) 
                        - ISNULL(SUM(xk.SoLuong),0) 
                        + ISNULL(SUM(dc.SoLuongDieuChinh),0) AS TonKho
                    FROM SanPham sp
                    LEFT JOIN NhapKho nk ON sp.MaSP = nk.MaSP
                    LEFT JOIN XuatKho xk ON sp.MaSP = xk.MaSP
                    LEFT JOIN DieuChinhTon dc ON sp.MaSP = dc.MaSP
                    GROUP BY sp.MaSP";

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

        // Tìm sản phẩm
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"
                    SELECT 
                        sp.MaSP,
                        ISNULL(SUM(nk.SoLuong),0) AS TongNhap,
                        ISNULL(SUM(xk.SoLuong),0) AS TongXuat,
                        ISNULL(SUM(dc.SoLuongDieuChinh),0) AS DieuChinh,
                        ISNULL(SUM(nk.SoLuong),0) 
                        - ISNULL(SUM(xk.SoLuong),0) 
                        + ISNULL(SUM(dc.SoLuongDieuChinh),0) AS TonKho
                    FROM SanPham sp
                    LEFT JOIN NhapKho nk ON sp.MaSP = nk.MaSP
                    LEFT JOIN XuatKho xk ON sp.MaSP = xk.MaSP
                    LEFT JOIN DieuChinhTon dc ON sp.MaSP = dc.MaSP
                    WHERE sp.MaSP LIKE '%' + @MaSP + '%'
                    GROUP BY sp.MaSP";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
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

        // Điều chỉnh tồn
        private void btnDieuChinhTon_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm");
                return;
            }

            string masp = dataGridView1.CurrentRow.Cells["MaSP"].Value.ToString();

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập số lượng điều chỉnh (+ hoặc -)",
                "Điều chỉnh tồn");

            if (input == "") return;

            int soluong = Convert.ToInt32(input);

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"INSERT INTO DieuChinhTon
                                   (MaSP, SoLuongDieuChinh, LyDo)
                                   VALUES
                                   (@MaSP, @SoLuong, N'Điều chỉnh tồn kho')";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@MaSP", masp);
                    cmd.Parameters.AddWithValue("@SoLuong", soluong);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Điều chỉnh tồn thành công");

                // load lại bảng
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}