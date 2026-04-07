namespace SharkTank.Modules.Sales.UI.Forms
{
    partial class BaoGia
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlTop;
        public System.Windows.Forms.Button btnTaoBaoGia;
        public System.Windows.Forms.Button btnDuyet;
        public System.Windows.Forms.DataGridView dgvBaoGia;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnDuyet = new System.Windows.Forms.Button();
            this.btnTaoBaoGia = new System.Windows.Forms.Button();
            this.dgvBaoGia = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoGia)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.btnDuyet);
            this.pnlTop.Controls.Add(this.btnTaoBaoGia);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(800, 60);
            this.pnlTop.TabIndex = 1;
            // 
            // btnDuyet
            // 
            this.btnDuyet.BackColor = System.Drawing.Color.SteelBlue;
            this.btnDuyet.ForeColor = System.Drawing.Color.White;
            this.btnDuyet.Location = new System.Drawing.Point(150, 15);
            this.btnDuyet.Name = "btnDuyet";
            this.btnDuyet.Size = new System.Drawing.Size(100, 30);
            this.btnDuyet.TabIndex = 0;
            this.btnDuyet.Text = "✓ Duyệt (Chốt)";
            this.btnDuyet.UseVisualStyleBackColor = false;
            // 
            // btnTaoBaoGia
            // 
            this.btnTaoBaoGia.BackColor = System.Drawing.Color.SeaGreen;
            this.btnTaoBaoGia.ForeColor = System.Drawing.Color.White;
            this.btnTaoBaoGia.Location = new System.Drawing.Point(20, 15);
            this.btnTaoBaoGia.Name = "btnTaoBaoGia";
            this.btnTaoBaoGia.Size = new System.Drawing.Size(120, 30);
            this.btnTaoBaoGia.TabIndex = 1;
            this.btnTaoBaoGia.Text = "+ Tạo Báo Giá Mới";
            this.btnTaoBaoGia.UseVisualStyleBackColor = false;
            this.btnTaoBaoGia.Click += new System.EventHandler(this.BtnTaoBaoGia_Click);
            // 
            // dgvBaoGia
            // 
            this.dgvBaoGia.AllowUserToAddRows = false;
            this.dgvBaoGia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBaoGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoGia.Location = new System.Drawing.Point(0, 60);
            this.dgvBaoGia.Name = "dgvBaoGia";
            this.dgvBaoGia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBaoGia.Size = new System.Drawing.Size(800, 440);
            this.dgvBaoGia.TabIndex = 0;
            // 
            // BaoGia
            // 
            this.Controls.Add(this.dgvBaoGia);
            this.Controls.Add(this.pnlTop);
            this.Name = "BaoGia";
            this.Size = new System.Drawing.Size(800, 500);
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoGia)).EndInit();
            this.ResumeLayout(false);

        }
    }
}