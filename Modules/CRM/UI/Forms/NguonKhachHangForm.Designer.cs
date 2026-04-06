using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SharkTank.Modules.CRM.UI.Forms
{
    public partial class NguonKhachHangForm :UserControl
    {
        private Panel panelMain;
        private Panel panelHeader;
        private Label lblTitle;
        private Chart chartNguon;
        private FormBorderStyle FormBorderStyle;

        private void InitializeComponent()
        {
            this.panelMain = new Panel();
            this.panelHeader = new Panel();
            this.lblTitle = new Label();
            this.chartNguon = new Chart();

            ((System.ComponentModel.ISupportInitialize)(this.chartNguon)).BeginInit();
            this.SuspendLayout();

            // ===== FORM =====
            this.Text = "Nguồn khách hàng";
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.BackColor = Color.FromArgb(24, 24, 28);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            // ===== PANEL MAIN =====
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.BackColor = Color.FromArgb(24, 24, 28);

            // ===== HEADER =====
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 70;
            this.panelHeader.BackColor = Color.FromArgb(0, 122, 204);

            this.lblTitle.Text = "📊 Nguồn khách hàng hiệu quả";
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.AutoSize = true;

            this.panelHeader.Controls.Add(this.lblTitle);

            // ===== CHART =====
            this.chartNguon.Dock = DockStyle.Fill;
            this.chartNguon.BackColor = Color.FromArgb(32, 32, 36);

            ChartArea area = new ChartArea();
            area.BackColor = Color.FromArgb(32, 32, 36);
            area.Area3DStyle.Enable3D = true;

            this.chartNguon.ChartAreas.Add(area);

            Series series = new Series("Nguon");
            series.ChartType = SeriesChartType.Doughnut;
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            series["PieLabelStyle"] = "Outside";

            this.chartNguon.Series.Add(series);

            Legend legend = new Legend();
            legend.ForeColor = Color.White;
            legend.BackColor = Color.FromArgb(32, 32, 36);
            this.chartNguon.Legends.Add(legend);

            // ===== ADD CONTROL =====
            this.panelMain.Controls.Add(this.chartNguon);
            this.panelMain.Controls.Add(this.panelHeader);

            this.Controls.Add(this.panelMain);

            ((System.ComponentModel.ISupportInitialize)(this.chartNguon)).EndInit();
            this.ResumeLayout(false);
        }
    }
}