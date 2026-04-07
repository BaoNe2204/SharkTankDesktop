using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class TonKho : UserControl
    {
        public TonKho()
        {
            InitializeComponent();
            this.Load += TonKho_Load;
        }

        private void TonKho_Load(object sender, EventArgs e)
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
    SanPham.MaSP,

    ISNULL(SUM(NhapKho.SoLuong),0) -
    ISNULL(SUM(XuatKho.SoLuong),0) AS TonTheoSanPham,

    ISNULL(SUM(NhapKho.SoLuong),0) -
    ISNULL(SUM(XuatKho.SoLuong),0) AS TonTheoKho,

    CASE 
        WHEN (ISNULL(SUM(NhapKho.SoLuong),0) -
              ISNULL(SUM(XuatKho.SoLuong),0)) = 0
            THEN N'Hết hàng'

        WHEN (ISNULL(SUM(NhapKho.SoLuong),0) -
              ISNULL(SUM(XuatKho.SoLuong),0)) <= 5
            THEN N'Sắp hết'

        ELSE N'Còn hàng'
    END AS CanhBao

FROM SanPham
LEFT JOIN NhapKho ON SanPham.MaSP = NhapKho.MaSP
LEFT JOIN XuatKho ON SanPham.MaSP = XuatKho.MaSP
GROUP BY SanPham.MaSP";

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
    SanPham.MaSP,

    ISNULL(SUM(NhapKho.SoLuong),0) -
    ISNULL(SUM(XuatKho.SoLuong),0) AS TonTheoSanPham,

    ISNULL(SUM(NhapKho.SoLuong),0) -
    ISNULL(SUM(XuatKho.SoLuong),0) AS TonTheoKho,

    CASE 
        WHEN (ISNULL(SUM(NhapKho.SoLuong),0) -
              ISNULL(SUM(XuatKho.SoLuong),0)) = 0
            THEN N'Hết hàng'

        WHEN (ISNULL(SUM(NhapKho.SoLuong),0) -
              ISNULL(SUM(XuatKho.SoLuong),0)) <= 5
            THEN N'Sắp hết'

        ELSE N'Còn hàng'
    END AS CanhBao

FROM SanPham
LEFT JOIN NhapKho ON SanPham.MaSP = NhapKho.MaSP
LEFT JOIN XuatKho ON SanPham.MaSP = XuatKho.MaSP  
WHERE SanPham.MaSP LIKE '%' + @MaSP + '%'
GROUP BY SanPham.MaSP";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text.Trim());

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}