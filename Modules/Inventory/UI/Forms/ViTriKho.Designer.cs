namespace SharkTank.Modules.Inventory.UI.Forms
{
    partial class ViTriKho
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblMaViTri = new System.Windows.Forms.Label();
            this.lblTenViTri = new System.Windows.Forms.Label();
            this.lblKho = new System.Windows.Forms.Label();
            this.txtMaViTri = new System.Windows.Forms.TextBox();
            this.txtTenViTri = new System.Windows.Forms.TextBox();
            this.cboKho = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMaViTri
            // 
            this.lblMaViTri.AutoSize = true;
            this.lblMaViTri.Location = new System.Drawing.Point(24, 24);
            this.lblMaViTri.Name = "lblMaViTri";
            this.lblMaViTri.Size = new System.Drawing.Size(58, 15);
            this.lblMaViTri.TabIndex = 0;
            this.lblMaViTri.Text = "Mã vị trí";
            // 
            // lblTenViTri
            // 
            this.lblTenViTri.AutoSize = true;
            this.lblTenViTri.Location = new System.Drawing.Point(24, 64);
            this.lblTenViTri.Name = "lblTenViTri";
            this.lblTenViTri.Size = new System.Drawing.Size(59, 15);
            this.lblTenViTri.TabIndex = 1;
            this.lblTenViTri.Text = "Tên vị trí";
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Location = new System.Drawing.Point(24, 104);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(29, 15);
            this.lblKho.TabIndex = 2;
            this.lblKho.Text = "Kho";
            // 
            // txtMaViTri
            // 
            this.txtMaViTri.Location = new System.Drawing.Point(120, 21);
            this.txtMaViTri.Name = "txtMaViTri";
            this.txtMaViTri.Size = new System.Drawing.Size(280, 23);
            this.txtMaViTri.TabIndex = 3;
            // 
            // txtTenViTri
            // 
            this.txtTenViTri.Location = new System.Drawing.Point(120, 61);
            this.txtTenViTri.Name = "txtTenViTri";
            this.txtTenViTri.Size = new System.Drawing.Size(280, 23);
            this.txtTenViTri.TabIndex = 4;
            // 
            // cboKho
            // 
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.FormattingEnabled = true;
            this.cboKho.Location = new System.Drawing.Point(120, 101);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(280, 23);
            this.cboKho.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 200);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(720, 280);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(120, 150);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(85, 28);
            this.btnThem.TabIndex = 7;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(220, 150);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(85, 28);
            this.btnSua.TabIndex = 8;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(320, 150);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(85, 28);
            this.btnXoa.TabIndex = 9;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // ViTriKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cboKho);
            this.Controls.Add(this.txtTenViTri);
            this.Controls.Add(this.txtMaViTri);
            this.Controls.Add(this.lblKho);
            this.Controls.Add(this.lblTenViTri);
            this.Controls.Add(this.lblMaViTri);
            this.Name = "ViTriKho";
            this.Size = new System.Drawing.Size(800, 500);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblMaViTri;
        private System.Windows.Forms.Label lblTenViTri;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.TextBox txtMaViTri;
        private System.Windows.Forms.TextBox txtTenViTri;
        private System.Windows.Forms.ComboBox cboKho;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
    }
}
