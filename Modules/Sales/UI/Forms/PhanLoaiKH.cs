using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class PhanLoaiKH : UserControl
    {
        private string strCon = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True;TrustServerCertificate=True";

        public PhanLoaiKH()
        {
            InitializeComponent();
            RefreshDashboard();
            RegisterEvents();
        }

        public void RefreshDashboard()
        {
            LoadSummaryCards();
            LoadData();
        }

        private void RegisterEvents()
        {
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            btnExport.Click += BtnExport_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnUpdateRule.Click += BtnUpdateRule_Click;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng xuất dữ liệu đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDashboard();
        }

        private void BtnUpdateRule_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng cập nhật quy tắc đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 2. Viết hàm xử lý: Khi click vào thì xóa chữ gợi ý
        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Tìm kiếm khách hàng...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black; // Chuyển sang màu đen để gõ cho rõ
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Tìm kiếm khách hàng...";
                txtSearch.ForeColor = Color.Gray; 
            }
        }

        // 1. Hàm nạp số liệu thật lên các thẻ Card
        private void LoadSummaryCards()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    // Truy vấn đếm số lượng khách hàng theo từng phân loại
                    string query = @"
                        SELECT 
                            COUNT(CASE WHEN Total >= 100000000 THEN 1 END) AS VIP,
                            COUNT(CASE WHEN Total >= 50000000 AND Total < 100000000 THEN 1 END) AS Loyal,
                            COUNT(CASE WHEN Total >= 10000000 AND Total < 50000000 THEN 1 END) AS Potential,
                            COUNT(CASE WHEN Total < 10000000 OR Total IS NULL THEN 1 END) AS New
                        FROM (
                            SELECT kh.MaKH, SUM(hd.TongTien) AS Total
                            FROM KhachHang kh
                            LEFT JOIN HoaDon hd ON kh.MaKH = hd.MaKH
                            GROUP BY kh.MaKH
                        ) AS SpendingSummary";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        flowCards.Controls.Clear();

                        // Đổ dữ liệu thật vào từng Card
                        flowCards.Controls.Add(CreateCard("💎 KH VIP", reader["VIP"].ToString(), Color.FromArgb(41, 128, 185)));
                        flowCards.Controls.Add(CreateCard("⭐ Thân Thiết", reader["Loyal"].ToString(), Color.FromArgb(39, 174, 96)));
                        flowCards.Controls.Add(CreateCard("🟢 Tiềm Năng", reader["Potential"].ToString(), Color.FromArgb(243, 156, 18)));
                        flowCards.Controls.Add(CreateCard("⚪ Khách Mới", reader["New"].ToString(), Color.FromArgb(149, 165, 166)));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp thẻ thống kê: " + ex.Message);
            }
        }

        private Panel CreateCard(string title, string value, Color color)
        {
            Panel card = new Panel { Size = new Size(225, 110), BackColor = Color.White, Margin = new Padding(0, 0, 25, 0) };

            // Vạch màu ở dưới đáy (Bottom)
            Panel line = new Panel { Size = new Size(225, 4), BackColor = color, Dock = DockStyle.Bottom };

            // Con số tổng (To, đậm)
            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(15, 10),
                AutoSize = true
            };

            // Tiêu đề (VIP/Thân thiết...)
            Label lblTitle = new Label
            {
                Text = title.ToUpper(),
                Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(18, 65),
                AutoSize = true
            };

            card.Controls.Add(line);
            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            return card;
        }

        // Khi gõ chữ vào ô tìm kiếm
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            // Nếu đang là chữ gợi ý thì không tìm
            if (searchText == "🔍 Tìm kiếm khách hàng...")
            {
                LoadData(""); // Load tất cả
            }
            else
            {
                LoadData(searchText); // Tìm theo chữ vừa gõ
            }
        }


        private void LoadData(string search = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    // Câu Query có thêm điều kiện WHERE để lọc theo Mã hoặc Tên
                    string query = @"
                SELECT 
                    kh.MaKH AS [Mã], 
                    kh.HoTen AS [Tên Khách], 
                    ISNULL((SELECT TOP 1 ch.Icon + ' ' + ch.TenLoai 
                            FROM CauHinhPhanLoai ch 
                            WHERE ISNULL(SUM(hd.TongTien), 0) >= ch.ChiTieuToiThieu 
                            ORDER BY ch.ChiTieuToiThieu DESC), N'⚪ Khách Mới') AS [Xếp hạng],
                    FORMAT(ISNULL(SUM(hd.TongTien), 0), 'N0') + ' đ' AS [Tổng chi tiêu]
                FROM KhachHang kh
                LEFT JOIN HoaDon hd ON kh.MaKH = hd.MaKH
                WHERE kh.HoTen LIKE @search OR kh.MaKH LIKE @search
                GROUP BY kh.MaKH, kh.HoTen";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    // Truyền giá trị tìm kiếm vào (thêm dấu % để tìm kiếm chứa chuỗi đó)
                    adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + search + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPhanLoai.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
    }
}