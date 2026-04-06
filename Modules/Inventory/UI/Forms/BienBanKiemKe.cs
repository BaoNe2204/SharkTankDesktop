using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class BienBanKiemKe : UserControl
    {
        public BienBanKiemKe()
        {
            InitializeComponent();
            this.Load += BienBanKiemKe_Load;
        }

        private void BienBanKiemKe_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Load tồn kho hệ thống
        void LoadData()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"
                    SELECT 
                        sp.MaSP,
                        ISNULL(SUM(nk.SoLuong),0) 
                        - ISNULL(SUM(xk.SoLuong),0) AS TonHeThong,
                        0 AS TonThucTe,
                        0 AS ChenhLech
                    FROM SanPham sp
                    LEFT JOIN NhapKho nk ON sp.MaSP = nk.MaSP
                    LEFT JOIN XuatKho xk ON sp.MaSP = xk.MaSP
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
                        ISNULL(SUM(nk.SoLuong),0) 
                        - ISNULL(SUM(xk.SoLuong),0) AS TonHeThong,
                        0 AS TonThucTe,
                        0 AS ChenhLech
                    FROM SanPham sp
                    LEFT JOIN NhapKho nk ON sp.MaSP = nk.MaSP
                    LEFT JOIN XuatKho xk ON sp.MaSP = xk.MaSP
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

        // Tự tính chênh lệch khi nhập tồn thực tế
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int tonHeThong = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["TonHeThong"].Value);
                int tonThucTe = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["TonThucTe"].Value);

                int chenhLech = tonThucTe - tonHeThong;

                dataGridView1.Rows[e.RowIndex].Cells["ChenhLech"].Value = chenhLech;
            }
            catch
            {

            }
        }

        // Lưu biên bản kiểm kê
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string masp = row.Cells["MaSP"].Value.ToString();
                        int tonHeThong = Convert.ToInt32(row.Cells["TonHeThong"].Value);
                        int tonThucTe = Convert.ToInt32(row.Cells["TonThucTe"].Value);
                        int chenhLech = Convert.ToInt32(row.Cells["ChenhLech"].Value);

                        string sql = @"INSERT INTO BienBanKiemKe
                                      (MaSP, TonHeThong, TonThucTe, ChenhLech)
                                      VALUES
                                      (@MaSP, @TonHeThong, @TonThucTe, @ChenhLech)";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@MaSP", masp);
                        cmd.Parameters.AddWithValue("@TonHeThong", tonHeThong);
                        cmd.Parameters.AddWithValue("@TonThucTe", tonThucTe);
                        cmd.Parameters.AddWithValue("@ChenhLech", chenhLech);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Lưu biên bản kiểm kê thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}