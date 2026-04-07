namespace SharkTank.Modules.Sales.UI.Forms
{
    partial class HoaDon
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlTop;
        public System.Windows.Forms.Button btnXuatHoaDon;
        public System.Windows.Forms.Button btnInPDF;
        public System.Windows.Forms.Button btnGuiKhach;
        public System.Windows.Forms.DataGridView dgvHoaDon;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnGuiKhach = new System.Windows.Forms.Button();
            this.btnInPDF = new System.Windows.Forms.Button();
            this.btnXuatHoaDon = new System.Windows.Forms.Button();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.btnGuiKhach);
            this.pnlTop.Controls.Add(this.btnInPDF);
            this.pnlTop.Controls.Add(this.btnXuatHoaDon);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(800, 60);
            this.pnlTop.TabIndex = 1;
            // 
            // btnGuiKhach
            // 
            this.btnGuiKhach.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGuiKhach.ForeColor = System.Drawing.Color.White;
            this.btnGuiKhach.Location = new System.Drawing.Point(260, 15);
            this.btnGuiKhach.Name = "btnGuiKhach";
            this.btnGuiKhach.Size = new System.Drawing.Size(120, 30);
            this.btnGuiKhach.TabIndex = 0;
            this.btnGuiKhach.Text = "📧 Gửi Khách Hàng";
            this.btnGuiKhach.UseVisualStyleBackColor = false;
            this.btnGuiKhach.Click += new System.EventHandler(this.BtnGuiKhach_Click);
            // 
            // btnInPDF
            // 
            this.btnInPDF.BackColor = System.Drawing.Color.OrangeRed;
            this.btnInPDF.ForeColor = System.Drawing.Color.White;
            this.btnInPDF.Location = new System.Drawing.Point(150, 15);
            this.btnInPDF.Name = "btnInPDF";
            this.btnInPDF.Size = new System.Drawing.Size(100, 30);
            this.btnInPDF.TabIndex = 1;
            this.btnInPDF.Text = "🖨️ In / PDF";
            this.btnInPDF.UseVisualStyleBackColor = false;
            this.btnInPDF.Click += new System.EventHandler(this.BtnInPDF_Click);
            // 
            // btnXuatHoaDon
            // 
            this.btnXuatHoaDon.BackColor = System.Drawing.Color.SeaGreen;
            this.btnXuatHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnXuatHoaDon.Location = new System.Drawing.Point(20, 15);
            this.btnXuatHoaDon.Name = "btnXuatHoaDon";
            this.btnXuatHoaDon.Size = new System.Drawing.Size(120, 30);
            this.btnXuatHoaDon.TabIndex = 2;
            this.btnXuatHoaDon.Text = "+ Xuất Hóa Đơn";
            this.btnXuatHoaDon.UseVisualStyleBackColor = false;
            this.btnXuatHoaDon.Click += new System.EventHandler(this.BtnXuatHoaDon_Click);
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHoaDon.Location = new System.Drawing.Point(0, 60);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.ReadOnly = true;
            this.dgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.Size = new System.Drawing.Size(800, 440);
            this.dgvHoaDon.TabIndex = 0;
            // 
            // HoaDon
            // 
            this.Controls.Add(this.dgvHoaDon);
            this.Controls.Add(this.pnlTop);
            this.Name = "HoaDon";
            this.Size = new System.Drawing.Size(800, 500);
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);

        }
    }
}