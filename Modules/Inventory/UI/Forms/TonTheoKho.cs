using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class TonTheoKho : UserControl
    {
        public TonTheoKho()
        {
            InitializeComponent();
            this.Load += TonTheoKho_Load;
        }

        private void TonTheoKho_Load(object sender, EventArgs e)
        {
            LoadTonKho();
        }

        // Load dữ liệu tồn kho
        void LoadTonKho()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT 
                        sp.MaSP,
                        sp.TenSP,

                        ISNULL(SUM(nk.SoLuong),0) -
                        ISNULL(SUM(xk.SoLuong),0) AS TonKho

                        FROM SanPham sp
                        LEFT JOIN NhapKho nk ON sp.MaSP = nk.MaSP
                        LEFT JOIN XuatKho xk ON sp.MaSP = xk.MaSP

                        GROUP BY sp.MaSP, sp.TenSP";

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

        // Nút tìm sản phẩm
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT 
                        sp.MaSP,
                        sp.TenSP,

                        ISNULL(SUM(nk.SoLuong),0) -
                        ISNULL(SUM(xk.SoLuong),0) AS TonKho

                        FROM SanPham sp
                        LEFT JOIN NhapKho nk ON sp.MaSP = nk.MaSP
                        LEFT JOIN XuatKho xk ON sp.MaSP = xk.MaSP

                        WHERE sp.MaSP LIKE '%' + @MaSP + '%'

                        GROUP BY sp.MaSP, sp.TenSP";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSP", txtMaKho.Text);

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
    }
}