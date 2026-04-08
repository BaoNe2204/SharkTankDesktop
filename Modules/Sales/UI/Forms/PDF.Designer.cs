namespace SharkTank.Modules.Sales.UI.Forms
{
    partial class PDF
    {
        private System.ComponentModel.IContainer components = null;

        public System.Windows.Forms.Label lblTieuDe;
        public System.Windows.Forms.TextBox txtTieuDe;
        public System.Windows.Forms.Label lblDuongDan;
        public System.Windows.Forms.TextBox txtDuongDan;
        public System.Windows.Forms.Button btnChon;
        public System.Windows.Forms.Button btnXuat;
        public System.Windows.Forms.Button btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtTieuDe = new System.Windows.Forms.TextBox();
            this.lblDuongDan = new System.Windows.Forms.Label();
            this.txtDuongDan = new System.Windows.Forms.TextBox();
            this.btnChon = new System.Windows.Forms.Button();
            this.btnXuat = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 30);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(104, 13);
            this.lblTieuDe.TabIndex = 6;
            this.lblTieuDe.Text = "Tiêu đề tài liệu PDF:";
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Location = new System.Drawing.Point(20, 50);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(440, 20);
            this.txtTieuDe.TabIndex = 5;
            // 
            // lblDuongDan
            // 
            this.lblDuongDan.AutoSize = true;
            this.lblDuongDan.Location = new System.Drawing.Point(20, 90);
            this.lblDuongDan.Name = "lblDuongDan";
            this.lblDuongDan.Size = new System.Drawing.Size(59, 13);
            this.lblDuongDan.TabIndex = 4;
            this.lblDuongDan.Text = "Nơi lưu file:";
            // 
            // txtDuongDan
            // 
            this.txtDuongDan.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDuongDan.Location = new System.Drawing.Point(20, 110);
            this.txtDuongDan.Name = "txtDuongDan";
            this.txtDuongDan.ReadOnly = true;
            this.txtDuongDan.Size = new System.Drawing.Size(350, 20);
            this.txtDuongDan.TabIndex = 3;
            // 
            // btnChon
            // 
            this.btnChon.Location = new System.Drawing.Point(380, 108);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(80, 24);
            this.btnChon.TabIndex = 2;
            this.btnChon.Text = "Chọn...";
            this.btnChon.Click += new System.EventHandler(this.BtnChon_Click);
            // 
            // btnXuat
            // 
            this.btnXuat.BackColor = System.Drawing.Color.OrangeRed;
            this.btnXuat.ForeColor = System.Drawing.Color.White;
            this.btnXuat.Location = new System.Drawing.Point(240, 160);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(120, 35);
            this.btnXuat.TabIndex = 1;
            this.btnXuat.Text = "Xuất File PDF";
            this.btnXuat.UseVisualStyleBackColor = false;
            this.btnXuat.Click += new System.EventHandler(this.BtnXuat_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(370, 160);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 35);
            this.btnHuy.TabIndex = 0;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            //this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // PDF
            // 
            this.ClientSize = new System.Drawing.Size(480, 220);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXuat);
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.txtDuongDan);
            this.Controls.Add(this.lblDuongDan);
            this.Controls.Add(this.txtTieuDe);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "PDF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trình Xuất Dữ Liệu PDF";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}