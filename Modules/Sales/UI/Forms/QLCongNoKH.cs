using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Đừng quên Add Reference thư viện này nhé An!

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class QLCongNoKH : UserControl
    {
        private string strCon = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True";

        public QLCongNoKH()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadDashboard();
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text.Trim());
            btnXacNhan.Click += BtnXacNhan_Click;
            btnLapPhieuThu.Click += btnLapPhieuThu_Click;
            btnRefresh.Click += (s, e) => {
                LoadDashboard();
                MessageBox.Show("Đã cập nhật dữ liệu mới nhất từ hệ thống!", "Thông báo");
            };
        }

        public void LoadDashboard() { LoadSummaryCards(); LoadData(); }

        private void LoadSummaryCards()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    string q = @"SELECT SUM(NoHienTai) as ThucNo, COUNT(CASE WHEN NoHienTai > HanMucNo THEN 1 END) as SoKhachVuot
                                 FROM (SELECT kh.HanMucNo, 
                                       ISNULL((SELECT SUM(TongTien-DaThanhToan) FROM HoaDon WHERE MaKH=kh.MaKH),0) - 
                                       ISNULL((SELECT SUM(SoTienThu) FROM PhieuThuNo WHERE MaKH=kh.MaKH AND TrangThai=N'Đã xác nhận'),0) as NoHienTai
                                       FROM KhachHang kh) as T";
                    SqlCommand cmd = new SqlCommand(q, conn);
                    conn.Open();
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        flowCards.Controls.Clear();
                        decimal thucNo = r["ThucNo"] != DBNull.Value ? Convert.ToDecimal(r["ThucNo"]) : 0;
                        flowCards.Controls.Add(CreateCard("💰 TỔNG TIỀN ĐANG NỢ", string.Format("{0:N0} đ", thucNo), Color.SeaGreen));
                        flowCards.Controls.Add(CreateCard("⚠️ SỐ KHÁCH NỢ XẤU", r["SoKhachVuot"].ToString(), Color.OrangeRed));
                    }
                }
            }
            catch { }
        }

        private void LoadData(string s = "")
        {
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string q = @"SELECT kh.MaKH AS [Mã], kh.HoTen AS [Tên Khách], 
                            FORMAT(ISNULL((SELECT SUM(TongTien-DaThanhToan) FROM HoaDon WHERE MaKH=kh.MaKH),0) - 
                                   ISNULL((SELECT SUM(SoTienThu) FROM PhieuThuNo WHERE MaKH=kh.MaKH AND TrangThai=N'Đã xác nhận'),0), 'N0') + ' đ' AS [Số Nợ Hiện Tại],
                            FORMAT(kh.HanMucNo, 'N0') + ' đ' AS [Hạn Mức],
                            ISNULL((SELECT COUNT(*) FROM PhieuThuNo WHERE MaKH=kh.MaKH AND TrangThai=N'Chờ xác nhận'),0) AS [Phiếu Chờ]
                            FROM KhachHang kh WHERE kh.HoTen LIKE @s OR kh.MaKH LIKE @s";
                SqlDataAdapter adp = new SqlDataAdapter(q, conn);
                adp.SelectCommand.Parameters.AddWithValue("@s", "%" + s + "%");
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dgvCongNo.DataSource = dt;
            }
        }

        private void btnLapPhieuThu_Click(object sender, EventArgs e)
        {
            if (dgvCongNo.CurrentRow == null) return;
            string maKH = dgvCongNo.CurrentRow.Cells["Mã"].Value.ToString();
            string input = Interaction.InputBox("Nhập số tiền khách thanh toán:", "Lập Phiếu Thu", "0");
            if (decimal.TryParse(input, out decimal st) && st > 0)
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    string sql = "INSERT INTO PhieuThuNo(MaKH, SoTienThu, NgayHenTra, TrangThai) VALUES(@m, @s, GETDATE(), N'Chờ xác nhận')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@m", maKH); cmd.Parameters.AddWithValue("@s", st);
                    conn.Open(); cmd.ExecuteNonQuery();
                    LoadDashboard();
                }
            }
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if (dgvCongNo.CurrentRow == null) return;
            string maKH = dgvCongNo.CurrentRow.Cells["Mã"].Value.ToString();
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                string sql = "UPDATE TOP(1) PhieuThuNo SET TrangThai=N'Đã xác nhận' WHERE MaKH=@m AND TrangThai=N'Chờ xác nhận'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@m", maKH);
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0) { MessageBox.Show("Xác nhận thành công!"); LoadDashboard(); }
            }
        }

        private Panel CreateCard(string t, string v, Color c)
        {
            Panel p = new Panel { Size = new Size(300, 100), BackColor = Color.White, Margin = new Padding(0, 0, 20, 0) };
            p.Controls.Add(new Label { Text = t, Location = new Point(20, 15), AutoSize = true, ForeColor = Color.Gray });
            p.Controls.Add(new Label { Text = v, Location = new Point(20, 40), AutoSize = true, Font = new Font("Segoe UI", 18, FontStyle.Bold) });
            p.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 5, BackColor = c });
            return p;
        }
    }
}