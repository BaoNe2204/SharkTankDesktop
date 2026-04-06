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
            txtMaSP.KeyDown += txtMaSP_KeyDown;
        }

        private void KiemKeDinhKyView_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // ================= LOAD DATA =================
        void LoadData(string maSP = "")
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string sql = @"
SELECT 
    sp.MaSP,
    ISNULL(SUM(nk.SoLuong),0) - ISNULL(SUM(xk.SoLuong),0) AS TonHeThong,
    ISNULL(SUM(bb.TonThucTe),0) AS TonThucTe,
    ISNULL(SUM(bb.TonThucTe),0) 
        - (ISNULL(SUM(nk.SoLuong),0) - ISNULL(SUM(xk.SoLuong),0)) AS ChenhLech
FROM SanPham sp
LEFT JOIN NhapKho nk ON sp.MaSP = nk.MaSP
LEFT JOIN XuatKho xk ON sp.MaSP = xk.MaSP
LEFT JOIN BienBanKiemKe bb 
    ON sp.MaSP = bb.MaSP 
    AND CAST(bb.NgayKiemKe AS DATE) = CAST(GETDATE() AS DATE)
WHERE sp.MaSP LIKE @MaSP
GROUP BY sp.MaSP";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSP", "%" + maSP + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // 👉 Format màu chênh lệch
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int chenhLech = Convert.ToInt32(row.Cells["ChenhLech"].Value);

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
            LoadData(); // reload lại luôn
        }

        // ================= REALTIME =================
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TonThucTe")
            {
                var row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["TonHeThong"].Value == null) return;

                int tonHeThong = Convert.ToInt32(row.Cells["TonHeThong"].Value);

                // ❗ FIX: không ép = 0 nữa
                if (row.Cells["TonThucTe"].Value == null ||
                    string.IsNullOrWhiteSpace(row.Cells["TonThucTe"].Value.ToString()))
                {
                    row.Cells["ChenhLech"].Value = "";
                    return;
                }

                int tonThucTe;
                if (!int.TryParse(row.Cells["TonThucTe"].Value.ToString(), out tonThucTe))
                {
                    MessageBox.Show("Vui lòng nhập số hợp lệ!");
                    row.Cells["TonThucTe"].Value = "";
                    row.Cells["ChenhLech"].Value = "";
                    return;
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
        // ================= PUBLIC RELOAD =================
        public void ReloadData()
        {
            LoadData();
        }
        // ================= LÀM MỚI =================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSP.Clear();
            LoadData();
        }

        private void txtMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData(txtMaSP.Text); // 🔥 gọi trực tiếp
                e.SuppressKeyPress = true;
            }
        }
    }
}