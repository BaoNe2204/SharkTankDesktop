namespace SharkTank.Modules.Sales.UI.Forms
{
    partial class PhieuXuatKho
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.txtGiaXuat = new System.Windows.Forms.TextBox();
            this.lblGiaXuat = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.lblMaSP = new System.Windows.Forms.Label();
            this.txtMaKho = new System.Windows.Forms.TextBox();
            this.lblMaKho = new System.Windows.Forms.Label();
            this.txtPhieuXuat = new System.Windows.Forms.TextBox();
            this.lblPhieuXuat = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnLuuPhieu = new System.Windows.Forms.Button();
            this.btnXoaSP = new System.Windows.Forms.Button();
            this.dgvChiTietXuat = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietXuat)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.btnThemSP);
            this.panelTop.Controls.Add(this.txtGiaXuat);
            this.panelTop.Controls.Add(this.lblGiaXuat);
            this.panelTop.Controls.Add(this.nudSoLuong);
            this.panelTop.Controls.Add(this.lblSoLuong);
            this.panelTop.Controls.Add(this.txtMaSP);
            this.panelTop.Controls.Add(this.lblMaSP);
            this.panelTop.Controls.Add(this.txtMaKho);
            this.panelTop.Controls.Add(this.lblMaKho);
            this.panelTop.Controls.Add(this.txtPhieuXuat);
            this.panelTop.Controls.Add(this.lblPhieuXuat);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 100);
            this.panelTop.TabIndex = 0;
            // 
            // lblPhieuXuat
            // 
            this.lblPhieuXuat.AutoSize = true;
            this.lblPhieuXuat.Location = new System.Drawing.Point(20, 20);
            this.lblPhieuXuat.Name = "lblPhieuXuat";
            this.lblPhieuXuat.Size = new System.Drawing.Size(95, 16);
            this.lblPhieuXuat.TabIndex = 0;
            this.lblPhieuXuat.Text = "Mã Phiếu Xuất:";
            // 
            // txtPhieuXuat
            // 
            this.txtPhieuXuat.Location = new System.Drawing.Point(120, 17);
            this.txtPhieuXuat.Name = "txtPhieuXuat";
            this.txtPhieuXuat.Size = new System.Drawing.Size(150, 22);
            this.txtPhieuXuat.TabIndex = 1;
            // 
            // lblMaKho
            // 
            this.lblMaKho.AutoSize = true;
            this.lblMaKho.Location = new System.Drawing.Point(300, 20);
            this.lblMaKho.Name = "lblMaKho";
            this.lblMaKho.Size = new System.Drawing.Size(55, 16);
            this.lblMaKho.TabIndex = 2;
            this.lblMaKho.Text = "Mã Kho:";
            // 
            // txtMaKho
            // 
            this.txtMaKho.Location = new System.Drawing.Point(360, 17);
            this.txtMaKho.Name = "txtMaKho";
            this.txtMaKho.Size = new System.Drawing.Size(150, 22);
            this.txtMaKho.TabIndex = 3;
            this.txtMaKho.Text = "KHO_CHINH";
            // 
            // lblMaSP
            // 
            this.lblMaSP.AutoSize = true;
            this.lblMaSP.Location = new System.Drawing.Point(20, 60);
            this.lblMaSP.Name = "lblMaSP";
            this.lblMaSP.Size = new System.Drawing.Size(91, 16);
            this.lblMaSP.TabIndex = 4;
            this.lblMaSP.Text = "Mã Sản Phẩm:";
            // 
            // txtMaSP
            // 
            this.txtMaSP.Location = new System.Drawing.Point(120, 57);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.Size = new System.Drawing.Size(150, 22);
            this.txtMaSP.TabIndex = 5;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(300, 60);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(67, 16);
            this.lblSoLuong.TabIndex = 6;
            this.lblSoLuong.Text = "Số Lượng:";
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Location = new System.Drawing.Point(380, 57);
            this.nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(80, 22);
            this.nudSoLuong.TabIndex = 7;
            this.nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblGiaXuat
            // 
            this.lblGiaXuat.AutoSize = true;
            this.lblGiaXuat.Location = new System.Drawing.Point(480, 60);
            this.lblGiaXuat.Name = "lblGiaXuat";
            this.lblGiaXuat.Size = new System.Drawing.Size(61, 16);
            this.lblGiaXuat.TabIndex = 8;
            this.lblGiaXuat.Text = "Giá Xuất:";
            // 
            // txtGiaXuat
            // 
            this.txtGiaXuat.Location = new System.Drawing.Point(550, 57);
            this.txtGiaXuat.Name = "txtGiaXuat";
            this.txtGiaXuat.Size = new System.Drawing.Size(120, 22);
            this.txtGiaXuat.TabIndex = 9;
            // 
            // btnThemSP
            // 
            this.btnThemSP.Location = new System.Drawing.Point(690, 55);
            this.btnThemSP.Name = "btnThemSP";
            this.btnThemSP.Size = new System.Drawing.Size(90, 26);
            this.btnThemSP.TabIndex = 10;
            this.btnThemSP.Text = "Thêm >>";
            this.btnThemSP.UseVisualStyleBackColor = true;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBottom.Controls.Add(this.btnLuuPhieu);
            this.panelBottom.Controls.Add(this.btnXoaSP);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 440);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(800, 60);
            this.panelBottom.TabIndex = 1;
            // 
            // btnXoaSP
            // 
            this.btnXoaSP.Location = new System.Drawing.Point(20, 15);
            this.btnXoaSP.Name = "btnXoaSP";
            this.btnXoaSP.Size = new System.Drawing.Size(100, 30);
            this.btnXoaSP.TabIndex = 0;
            this.btnXoaSP.Text = "Xóa dòng";
            this.btnXoaSP.UseVisualStyleBackColor = true;
            // 
            // btnLuuPhieu
            // 
            this.btnLuuPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuuPhieu.BackColor = System.Drawing.Color.SeaGreen;
            this.btnLuuPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieu.Location = new System.Drawing.Point(630, 10);
            this.btnLuuPhieu.Name = "btnLuuPhieu";
            this.btnLuuPhieu.Size = new System.Drawing.Size(150, 40);
            this.btnLuuPhieu.TabIndex = 1;
            this.btnLuuPhieu.Text = "Lưu Yêu Cầu Xuất";
            this.btnLuuPhieu.UseVisualStyleBackColor = false;
            // 
            // dgvChiTietXuat
            // 
            this.dgvChiTietXuat.AllowUserToAddRows = false;
            this.dgvChiTietXuat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietXuat.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietXuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietXuat.Location = new System.Drawing.Point(0, 100);
            this.dgvChiTietXuat.Name = "dgvChiTietXuat";
            this.dgvChiTietXuat.RowHeadersWidth = 51;
            this.dgvChiTietXuat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietXuat.Size = new System.Drawing.Size(800, 340);
            this.dgvChiTietXuat.TabIndex = 2;
            // 
            // PhieuXuatKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvChiTietXuat);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "PhieuXuatKho";
            this.Size = new System.Drawing.Size(800, 500);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietXuat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnThemSP;
        private System.Windows.Forms.TextBox txtGiaXuat;
        private System.Windows.Forms.Label lblGiaXuat;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.TextBox txtMaKho;
        private System.Windows.Forms.Label lblMaKho;
        private System.Windows.Forms.TextBox txtPhieuXuat;
        private System.Windows.Forms.Label lblPhieuXuat;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnLuuPhieu;
        private System.Windows.Forms.Button btnXoaSP;
        private System.Windows.Forms.DataGridView dgvChiTietXuat;
    }
}