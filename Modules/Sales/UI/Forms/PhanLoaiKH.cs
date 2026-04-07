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
            this.Load += (s, e) => {
                RefreshDashboard();
                Application.DoEvents(); 
            };
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
            txtSearch.TextChanged += txtSearch_TextChanged;
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
                txtSearch.ForeColor = Color.Black;
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
            Panel card = new Panel
            {
                Size = new Size(200, 100),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 20, 0)
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 25, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(15, 10),
                AutoSize = true
            };

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Location = new Point(15, 60),
                AutoSize = true
            };

            Panel pnlLine = new Panel
            {
                Height = 4,
                BackColor = color,
                Dock = DockStyle.Bottom
            };

            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            card.Controls.Add(pnlLine);

            return card;
        }

       
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            
            if (searchText == "🔍 Tìm kiếm khách hàng...")
            {
                LoadData("");
            }
            else
            {
                LoadData(searchText);
            }
        }


        private void LoadData(string search = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    
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