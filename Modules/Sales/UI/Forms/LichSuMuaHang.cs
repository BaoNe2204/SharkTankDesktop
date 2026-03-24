using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class LichSuMuaHang : UserControl
    {
        private string strCon = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True";

        public LichSuMuaHang()
        {
            InitializeComponent();
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            LoadData();

            txtSearch.TextChanged += (s, e) => LoadData();
            btnFilter.Click += (s, e) => LoadData();
        }

        public void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    string query = @"
                        SELECT hd.MaHD AS [Mã HĐ], kh.HoTen AS [Khách Hàng], 
                               hd.NgayLap AS [Ngày Lập], FORMAT(hd.TongTien, 'N0') + ' đ' AS [Tổng Tiền]
                        FROM HoaDon hd
                        JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                        WHERE (kh.HoTen LIKE @s OR hd.MaHD LIKE @s)
                        AND hd.NgayLap BETWEEN @f AND @t
                        ORDER BY hd.NgayLap DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@s", "%" + txtSearch.Text.Trim() + "%");
                    cmd.Parameters.AddWithValue("@f", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@t", dtpTo.Value.Date.AddDays(1).AddSeconds(-1));

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    dgvLichSu.DataSource = dt;

                    dgvLichSu.ClearSelection(); // Xóa màu xanh dòng đầu
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        
    }
}