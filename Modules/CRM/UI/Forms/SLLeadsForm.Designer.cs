using System.Windows.Forms;

namespace SharkTank.Modules.CRM.UI.Forms
{
    partial class SLLeadsForm : UserControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Label lblGrowth;
        private System.Windows.Forms.DataGridView dgvLead;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLead;
        private System.Windows.Forms.Button btnReload;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblGrowth = new System.Windows.Forms.Label();
            this.dgvLead = new System.Windows.Forms.DataGridView();
            this.chartLead = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnReload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLead)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(345, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 Số lượng lead theo thời gian";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(20, 100);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 13);
            this.lblTotal.TabIndex = 2;
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.ForeColor = System.Drawing.Color.White;
            this.lblToday.Location = new System.Drawing.Point(20, 130);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(0, 13);
            this.lblToday.TabIndex = 3;
            // 
            // lblGrowth
            // 
            this.lblGrowth.AutoSize = true;
            this.lblGrowth.ForeColor = System.Drawing.Color.White;
            this.lblGrowth.Location = new System.Drawing.Point(20, 160);
            this.lblGrowth.Name = "lblGrowth";
            this.lblGrowth.Size = new System.Drawing.Size(0, 13);
            this.lblGrowth.TabIndex = 4;
            // 
            // dgvLead
            // 
            this.dgvLead.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLead.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvLead.Location = new System.Drawing.Point(640, 200);
            this.dgvLead.Name = "dgvLead";
            this.dgvLead.Size = new System.Drawing.Size(400, 300);
            this.dgvLead.TabIndex = 6;
            // 
            // chartLead
            // 
            chartArea3.Name = "ChartArea1";
            this.chartLead.ChartAreas.Add(chartArea3);
            this.chartLead.Location = new System.Drawing.Point(20, 200);
            this.chartLead.Name = "chartLead";
            this.chartLead.Size = new System.Drawing.Size(600, 300);
            this.chartLead.TabIndex = 5;
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.White;
            this.btnReload.Location = new System.Drawing.Point(20, 91);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(120, 30);
            this.btnReload.TabIndex = 1;
            this.btnReload.Text = "Làm mới";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // SLLeads
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblToday);
            this.Controls.Add(this.lblGrowth);
            this.Controls.Add(this.chartLead);
            this.Controls.Add(this.dgvLead);
            this.Name = "SLLeadsForm";
            this.Size = new System.Drawing.Size(1160, 630);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLead)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}