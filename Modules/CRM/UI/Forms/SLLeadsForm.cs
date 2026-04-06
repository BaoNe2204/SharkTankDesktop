using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SharkTank.Modules.CRM.Services;

namespace SharkTank.Modules.CRM.UI.Forms
{
    public partial class SLLeadsForm : UserControl
    {
        private LeadService service = new LeadService();
        private bool showDebug;

        public SLLeadsForm()
        {
            InitializeComponent();
            btnReload.Click += btnReload_Click;
            LoadData();
            
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime from = new DateTime(now.Year, 1, 1);
                DateTime to = now;
                DataTable dt = service.GetLeadByDate(from, to);

                if (showDebug)
                    MessageBox.Show("Rows: " + dt.Rows.Count);
                chartLead.Series.Clear();

                Series s = new Series("Lead")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 3
                };

                foreach (DataRow r in dt.Rows)
                {
                    s.Points.AddXY(r["Ngay"], r["SoLuong"]);
                }

                chartLead.Series.Add(s);

                dgvLead.DataSource = dt;

                lblTotal.Text = "Tổng: " + service.GetTotal(from, to);
                lblToday.Text = "Hôm nay: " + service.GetToday();
                lblGrowth.Text = "Tăng trưởng: " + service.GetGrowth(from, to).ToString("0.0") + "%";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
        }
    }
}