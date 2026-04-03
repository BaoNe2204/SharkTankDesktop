using SharkTank.Core.Data;
using SharkTank.DAL.Sql;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class KiemKeDinhKy : UserControl
    {
        public KiemKeDinhKy()
        {
            InitializeComponent();
            this.Load += KiemKeDinhKyView_Load;
            this.dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
        }

        private void KiemKeDinhKyView_Load(object sender, EventArgs e)
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
                    string sql = @"
SELECT 
    sp.MaSP,
    ISNULL(SUM(nk.SoLuong),0) - ISNULL(SUM(xk.SoLuong),0) AS TonHeThong,
    ISNULL(SUM(bb.TonThucTe),0) AS TonThucTe
FROM SanPham sp
LEFT JOIN NhapKho nk ON sp.MaSP = nk.MaSP
LEFT JOIN XuatKho xk ON sp.MaSP = xk.MaSP
LEFT JOIN BienBanKiemKe bb 
    ON sp.MaSP = bb.MaSP 
    AND CAST(bb.NgayKiemKe AS DATE) = CAST(GETDATE() AS DATE)
GROUP BY sp.MaSP";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // 👉 THÊM CỘT CHÊNH LỆCH NẾU CHƯA CÓ
                    if (!dataGridView1.Columns.Contains("ChenhLech"))
                    {
                        dataGridView1.Columns.Add("ChenhLech", "Chênh lệch");
                    }

                    // rồi mới tính lại

                    // 👉 TÍNH CHÊNH LỆCH
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int tonHeThong = Convert.ToInt32(row.Cells["TonHeThong"].Value);
                        int tonThucTe = Convert.ToInt32(row.Cells["TonThucTe"].Value);

                        int chenhLech = tonThucTe - tonHeThong;

                        row.Cells["ChenhLech"].Value = chenhLech;

                        // màu (optional)
                        if (chenhLech < 0)
                            row.Cells["ChenhLech"].Style.ForeColor = Color.Red;
                        else
                            row.Cells["ChenhLech"].Style.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= TÌM =================
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"
SELECT 
    sp.MaSP,
    ISNULL(SUM(nk.SoLuong),0) - ISNULL(SUM(xk.SoLuong),0) AS TonHeThong,
    ISNULL(SUM(bb.TonThucTe),0) AS TonThucTe
FROM SanPham sp
LEFT JOIN NhapKho nk ON sp.MaSP = nk.MaSP
LEFT JOIN XuatKho xk ON sp.MaSP = xk.MaSP
LEFT JOIN BienBanKiemKe bb 
    ON sp.MaSP = bb.MaSP 
    AND CAST(bb.NgayKiemKe AS DATE) = CAST(GETDATE() AS DATE)
WHERE sp.MaSP LIKE @MaSP
GROUP BY sp.MaSP";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSP", "%" + txtMaSP.Text + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // thêm cột nếu thiếu
                    if (!dataGridView1.Columns.Contains("ChenhLech"))
                    {
                        dataGridView1.Columns.Add("ChenhLech", "Chênh lệch");
                    }

                    // tính lại
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int tonHeThong = Convert.ToInt32(row.Cells["TonHeThong"].Value);
                        int tonThucTe = Convert.ToInt32(row.Cells["TonThucTe"].Value);

                        row.Cells["ChenhLech"].Value = tonThucTe - tonHeThong;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= KIỂM KÊ =================
        private void btnKiemKe_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns["TonThucTe"].ReadOnly = false;
            MessageBox.Show("Đã bật chế độ kiểm kê!");
        }

        // ================= LƯU =================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();

            using (var conn = SqlConnectionFactory.Create())
            {
                conn.Open();

                // 👉 XÓA DỮ LIỆU HÔM NAY (TRÁNH LẶP)
                string deleteSql = "DELETE FROM BienBanKiemKe WHERE CAST(NgayKiemKe AS DATE) = CAST(GETDATE() AS DATE)";
                new SqlCommand(deleteSql, conn).ExecuteNonQuery();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string maSP = row.Cells["MaSP"].Value.ToString();
                    int tonHeThong = Convert.ToInt32(row.Cells["TonHeThong"].Value);

                    int tonThucTe = 0;
                    if (row.Cells["TonThucTe"].Value != null)
                        int.TryParse(row.Cells["TonThucTe"].Value.ToString(), out tonThucTe);

                    int chenhLech = tonThucTe - tonHeThong;

                    string sql = @"
INSERT INTO BienBanKiemKe (MaSP, TonHeThong, TonThucTe, ChenhLech, NgayKiemKe)
VALUES (@MaSP, @TonHeThong, @TonThucTe, @ChenhLech, GETDATE())";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        cmd.Parameters.AddWithValue("@TonHeThong", tonHeThong);
                        cmd.Parameters.AddWithValue("@TonThucTe", tonThucTe);
                        cmd.Parameters.AddWithValue("@ChenhLech", chenhLech);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Lưu kiểm kê thành công!");
            LoadData(); // reload lại
        }

        // ================= TÍNH REALTIME =================
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TonThucTe")
            {
                var row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["TonHeThong"].Value == null) return;

                int tonHeThong = Convert.ToInt32(row.Cells["TonHeThong"].Value);

                int tonThucTe = 0;
                if (row.Cells["TonThucTe"].Value != null && row.Cells["TonThucTe"].Value.ToString() != "")
                {
                    tonThucTe = Convert.ToInt32(row.Cells["TonThucTe"].Value);
                }

                int chenhLech = tonThucTe - tonHeThong;

                row.Cells["ChenhLech"].Value = chenhLech;

                // màu
                if (chenhLech < 0)
                    row.Cells["ChenhLech"].Style.ForeColor = Color.Red;
                else
                    row.Cells["ChenhLech"].Style.ForeColor = Color.Green;
            }
        }
    }
}