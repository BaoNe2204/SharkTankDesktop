namespace SharkTank.Modules.CRM.UI.Forms
{
    partial class TyLeChuyenDoiKHForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblDetail;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel card;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblDetail = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.card = new System.Windows.Forms.Panel();
            this.card.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPercent
            // 
            this.lblPercent.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblPercent.ForeColor = System.Drawing.Color.Green;
            this.lblPercent.Location = new System.Drawing.Point(285, 18);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(471, 130);
            this.lblPercent.TabIndex = 0;
            this.lblPercent.Text = "0%";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDetail
            // 
            this.lblDetail.Location = new System.Drawing.Point(318, 119);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(400, 74);
            this.lblDetail.TabIndex = 1;
            this.lblDetail.Text = "0 / 0 khách";
            this.lblDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDetail.Click += new System.EventHandler(this.lblDetail_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(266, 231);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(500, 25);
            this.progressBar1.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(467, 349);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 35);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // card
            // 
            this.card.BackColor = System.Drawing.Color.White;
            this.card.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card.Controls.Add(this.lblPercent);
            this.card.Controls.Add(this.progressBar1);
            this.card.Controls.Add(this.lblDetail);
            this.card.Location = new System.Drawing.Point(3, 51);
            this.card.Name = "card";
            this.card.Size = new System.Drawing.Size(1111, 282);
            this.card.TabIndex = 0;
            // 
            // TyLeChuyenDoiKHForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.card);
            this.Controls.Add(this.btnRefresh);
            this.Name = "TyLeChuyenDoiKHForm";
            this.Size = new System.Drawing.Size(1155, 577);
            this.Load += new System.EventHandler(this.TyLeChuyenDoiKHForm_Load);
            this.card.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}