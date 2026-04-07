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

            txtMaSP.KeyDown += txtMaSP_KeyDown;

            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;

            // tránh lỗi popup
            dataGridView1.DataError += (s, e) => { e.Cancel = true; };
        }

        private void BienBanKiemKe_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // ================= LOAD =================
        void LoadData(string keyword = "")
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

WHERE sp.MaSP LIKE @kw

GROUP BY sp.MaSP";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                dataGridView1.ReadOnly = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;

                dataGridView1.Columns["TonThucTe"].ReadOnly = false;
                dataGridView1.Columns["TonThucTe"].ValueType = typeof(int);

                dataGridView1.Columns["ChenhLech"].ReadOnly = true;
            }
        }

        // ================= ENTER =================
        private void txtMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData(txtMaSP.Text); // ✅ luôn cho tìm
                e.SuppressKeyPress = true;
            }
        }

        // ================= COMMIT =================
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        // ================= TÍNH CHÊNH LỆCH =================
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex].Name != "TonThucTe") return;

            var row = dataGridView1.Rows[e.RowIndex];

            int tonHeThong = 0;
            int.TryParse(row.Cells["TonHeThong"].Value?.ToString(), out tonHeThong);

            int tonThucTe = 0;
            int.TryParse(row.Cells["TonThucTe"].Value?.ToString(), out tonThucTe);

            int chenhLech = tonThucTe - tonHeThong;

            row.Cells["ChenhLech"].Value = chenhLech;
        }

        // ================= LƯU =================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                // 🔥 XÓA dữ liệu hôm nay trước (tránh trùng)
                string deleteSql = @"DELETE FROM BienBanKiemKe 
                                     WHERE CAST(NgayKiemKe AS DATE) = CAST(GETDATE() AS DATE)";
                new SqlCommand(deleteSql, conn).ExecuteNonQuery();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string masp = row.Cells["MaSP"].Value.ToString();

                    int tonHeThong = 0;
                    int.TryParse(row.Cells["TonHeThong"].Value?.ToString(), out tonHeThong);

                    int tonThucTe = 0;
                    int.TryParse(row.Cells["TonThucTe"].Value?.ToString(), out tonThucTe);

                    int chenhLech = tonThucTe - tonHeThong;

                    string sql = @"INSERT INTO BienBanKiemKe
                                  (MaSP, TonHeThong, TonThucTe, ChenhLech, NgayKiemKe)
                                  VALUES
                                  (@MaSP, @TonHeThong, @TonThucTe, @ChenhLech, GETDATE())";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@MaSP", masp);
                    cmd.Parameters.AddWithValue("@TonHeThong", tonHeThong);
                    cmd.Parameters.AddWithValue("@TonThucTe", tonThucTe);
                    cmd.Parameters.AddWithValue("@ChenhLech", chenhLech);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Lưu biên bản kiểm kê thành công");

            // ✅ load lại nhưng KHÔNG mất dữ liệu (vì đã lưu DB)
            LoadData();
        }

        // ================= LÀM MỚI =================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSP.Clear();
            LoadData(); // giờ dùng được vì đã lưu DB
        }
    }
}