using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SharkTank.Modules.CRM.UI.Forms
{
    public partial class NguonKhachHangForm : UserControl
    {
        public NguonKhachHangForm()
        {
            InitializeComponent();
            LoadChartFromDB();
        }
    
        string connStr = @"Server=(localdb)\MSSQLLocalDB;Database=SharkTankERP;Trusted_Connection=True;";

      

        private void NguonKhachHangForm_Load(object sender, EventArgs e)
        {
            LoadChartFromDB();
        }

        private DataTable GetNguonKhachHang()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT Nguon, COUNT(*) AS SoLuong
                    FROM dbo.Leads
                    WHERE Nguon IS NOT NULL AND LTRIM(RTRIM(Nguon)) <> ''
                    GROUP BY Nguon
                ";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        private void LoadChartFromDB()
        {
            chartNguon.Series["Nguon"].Points.AddXY("Facebook", 120);
            chartNguon.Series["Nguon"].Points.AddXY("Website", 80);
            chartNguon.Series["Nguon"].Points.AddXY("Google Ads", 150);
            chartNguon.Series["Nguon"].Points.AddXY("Zalo", 60);

            DataTable dt = GetNguonKhachHang();

            chartNguon.Series.Clear();
            chartNguon.ChartAreas.Clear();

            ChartArea area = new ChartArea();
            chartNguon.ChartAreas.Add(area);

            Series s = new Series("Nguon");
            s.ChartType = SeriesChartType.Doughnut;
            chartNguon.Series.Add(s);


            Legend legend = new Legend();
            legend.Docking = Docking.Right;
            legend.ForeColor = Color.White;
            legend.BackColor = Color.FromArgb(32, 32, 36);

            chartNguon.Legends.Add(legend);

            foreach (DataRow row in dt.Rows)
            {
                string nguon = row["Nguon"].ToString();

                int value = 0;
                int.TryParse(row["SoLuong"].ToString(), out value);

                chartNguon.Series["Nguon"].Points.AddXY(nguon, value);
            }

            chartNguon.Series["Nguon"].Label = "#PERCENT{P0}";
            chartNguon.Series["Nguon"].Label = "#VALX: #PERCENT{P0}";
        }
    }
}