using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class KiemKeDinhKy : UserControl
    {
        public KiemKeDinhKy()
        {
            InitializeComponent();
            this.Load += KiemKeDinhKyView_Load;
        }

        private void KiemKeDinhKyView_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT 
                                   sp.MaSP,
                                   ISNULL(SUM(nk.SoLuong),0) - ISNULL(SUM(xk.SoLuong),0) AS TonHeThong,
                                   0 AS TonThucTe,
                                   0 AS ChenhLech
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

        // Tìm sản phẩm
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"SELECT 
                                   sp.MaSP,
                                   ISNULL(SUM(nk.SoLuong),0) - ISNULL(SUM(xk.SoLuong),0) AS TonHeThong,
                                   0 AS TonThucTe,
                                   0 AS ChenhLech
                                   FROM SanPham sp
                                   LEFT JOIN NhapKho nk ON sp.MaSP = nk.MaSP
                                   LEFT JOIN XuatKho xk ON sp.MaSP = xk.MaSP
                                   WHERE sp.MaSP LIKE '%' + @MaSP + '%'
                                   GROUP BY sp.MaSP, sp.TenSP";

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

        // Cho nhập tồn thực tế
        private void btnKiemKe_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns["TonThucTe"].ReadOnly = false;
        }

        // Lưu kiểm kê
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["MaSP"].Value == null) continue;

                        string masp = row.Cells["MaSP"].Value.ToString();
                        int tonHeThong = Convert.ToInt32(row.Cells["TonHeThong"].Value);
                        int tonThucTe = Convert.ToInt32(row.Cells["TonThucTe"].Value);

                        int chenhLech = tonThucTe - tonHeThong;

                        string sql = @"INSERT INTO BienBanKiemKe
                                       (MaSP,TonHeThong,TonThucTe,ChenhLech,Ngay)
                                       VALUES
                                       (@MaSP,@TonHeThong,@TonThucTe,@ChenhLech,GETDATE())";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@MaSP", masp);
                        cmd.Parameters.AddWithValue("@TonHeThong", tonHeThong);
                        cmd.Parameters.AddWithValue("@TonThucTe", tonThucTe);
                        cmd.Parameters.AddWithValue("@ChenhLech", chenhLech);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Kiểm kê thành công");

                LoadData();
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