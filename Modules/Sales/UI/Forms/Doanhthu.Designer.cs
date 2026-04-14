using System.Windows.Forms;

namespace SharkTank.Modules.Sales.UI.Forms
{
    partial class Doanhthu
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnTheoSanPham = new System.Windows.Forms.Button();
            this.btnTheoNhanVien = new System.Windows.Forms.Button();
            this.btnTheoNgay = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvHienThi = new System.Windows.Forms.DataGridView();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHienThi)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlMenu.Controls.Add(this.btnTheoSanPham);
            this.pnlMenu.Controls.Add(this.btnTheoNhanVien);
            this.pnlMenu.Controls.Add(this.btnTheoNgay);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(150, 366);
            this.pnlMenu.TabIndex = 0;
            // 
            // btnTheoSanPham
            // 
            this.btnTheoSanPham.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTheoSanPham.Location = new System.Drawing.Point(0, 82);
            this.btnTheoSanPham.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTheoSanPham.Name = "btnTheoSanPham";
            this.btnTheoSanPham.Size = new System.Drawing.Size(150, 41);
            this.btnTheoSanPham.TabIndex = 2;
            this.btnTheoSanPham.Text = "Theo sản phẩm";
            this.btnTheoSanPham.UseVisualStyleBackColor = true;
            this.btnTheoSanPham.Click += new System.EventHandler(this.BtnTheoSanPham_Click);
            // 
            // btnTheoNhanVien
            // 
            this.btnTheoNhanVien.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTheoNhanVien.Location = new System.Drawing.Point(0, 41);
            this.btnTheoNhanVien.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTheoNhanVien.Name = "btnTheoNhanVien";
            this.btnTheoNhanVien.Size = new System.Drawing.Size(150, 41);
            this.btnTheoNhanVien.TabIndex = 1;
            this.btnTheoNhanVien.Text = "Theo nhân viên";
            this.btnTheoNhanVien.UseVisualStyleBackColor = true;
            this.btnTheoNhanVien.Click += new System.EventHandler(this.BtnTheoNhanVien_Click);
            // 
            // btnTheoNgay
            // 
            this.btnTheoNgay.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTheoNgay.Location = new System.Drawing.Point(0, 0);
            this.btnTheoNgay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTheoNgay.Name = "btnTheoNgay";
            this.btnTheoNgay.Size = new System.Drawing.Size(150, 41);
            this.btnTheoNgay.TabIndex = 0;
            this.btnTheoNgay.Text = "Theo ngày/tháng";
            this.btnTheoNgay.UseVisualStyleBackColor = true;
            this.btnTheoNgay.Click += new System.EventHandler(this.BtnTheoNgay_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgvHienThi);
            this.pnlMain.Controls.Add(this.lblTieuDe);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(150, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.pnlMain.Size = new System.Drawing.Size(450, 366);
            this.pnlMain.TabIndex = 1;
            // 
            // dgvHienThi
            // 
            this.dgvHienThi.AllowUserToAddRows = false;
            this.dgvHienThi.AllowUserToDeleteRows = false;
            this.dgvHienThi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHienThi.BackgroundColor = System.Drawing.Color.White;
            this.dgvHienThi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHienThi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHienThi.Location = new System.Drawing.Point(8, 32);
            this.dgvHienThi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvHienThi.Name = "dgvHienThi";
            this.dgvHienThi.ReadOnly = true;
            this.dgvHienThi.RowHeadersWidth = 51;
            this.dgvHienThi.RowTemplate.Height = 24;
            this.dgvHienThi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHienThi.Size = new System.Drawing.Size(434, 326);
            this.dgvHienThi.TabIndex = 1;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(8, 8);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(434, 24);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Hiển thị doanh thu";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Doanhthu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlMenu);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Doanhthu";
            this.Text = "Doanh thu SharkTank";
            this.pnlMenu.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHienThi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnTheoSanPham;
        private System.Windows.Forms.Button btnTheoNhanVien;
        private System.Windows.Forms.Button btnTheoNgay;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dgvHienThi;
        private System.Windows.Forms.Label lblTieuDe;
    }
}