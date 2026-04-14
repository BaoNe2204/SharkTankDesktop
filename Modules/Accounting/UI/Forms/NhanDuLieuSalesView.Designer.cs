namespace SharkTank.Modules.Accounting.UI.Forms
{
    partial class NhanDuLieuSalesView
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.grpHuongDan = new System.Windows.Forms.GroupBox();
            this.txtHuongDan = new System.Windows.Forms.TextBox();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.cboTrangThaiSales = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.btnLoc = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.btnTuChoi = new System.Windows.Forms.Button();
            this.btnNhapKetoan = new System.Windows.Forms.Button();
            this.panelGridSales = new System.Windows.Forms.Panel();
            this.dgvDonHangSales = new System.Windows.Forms.DataGridView();
            this.colChon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.txtSoDonHang = new System.Windows.Forms.TextBox();
            this.lblSoDonHang = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.grpHuongDan.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelGridSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonHangSales)).BeginInit();
            this.panelSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.ForeColor = System.Drawing.Color.White;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1500, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(275, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Nhận dữ liệu từ Sales";
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.panelInfo.Controls.Add(this.grpHuongDan);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Location = new System.Drawing.Point(0, 60);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Padding = new System.Windows.Forms.Padding(10);
            this.panelInfo.Size = new System.Drawing.Size(1500, 70);
            this.panelInfo.TabIndex = 1;
            // 
            // grpHuongDan
            // 
            this.grpHuongDan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.grpHuongDan.Controls.Add(this.txtHuongDan);
            this.grpHuongDan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHuongDan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpHuongDan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpHuongDan.Location = new System.Drawing.Point(10, 10);
            this.grpHuongDan.Name = "grpHuongDan";
            this.grpHuongDan.Size = new System.Drawing.Size(1480, 50);
            this.grpHuongDan.TabIndex = 0;
            this.grpHuongDan.TabStop = false;
            this.grpHuongDan.Text = "Hướng dẫn";
            // 
            // txtHuongDan
            // 
            this.txtHuongDan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.txtHuongDan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHuongDan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHuongDan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtHuongDan.Location = new System.Drawing.Point(3, 19);
            this.txtHuongDan.Multiline = true;
            this.txtHuongDan.Name = "txtHuongDan";
            this.txtHuongDan.ReadOnly = true;
            this.txtHuongDan.Size = new System.Drawing.Size(1474, 28);
            this.txtHuongDan.TabIndex = 0;
            this.txtHuongDan.Text = "Chọn các đơn hàng từ Sales để nhập vào hệ thống kế toán. Tick chọn các đơn hàng cần nhập, sau đó nhấn \"Nhập vào kế toán\".";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.Controls.Add(this.dtpToDate);
            this.panelFilter.Controls.Add(this.lblToDate);
            this.panelFilter.Controls.Add(this.dtpFromDate);
            this.panelFilter.Controls.Add(this.lblFromDate);
            this.panelFilter.Controls.Add(this.cboTrangThaiSales);
            this.panelFilter.Controls.Add(this.lblTrangThai);
            this.panelFilter.Controls.Add(this.btnLoc);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 130);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1500, 50);
            this.panelFilter.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(800, 10);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 25);
            this.dtpToDate.TabIndex = 4;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblToDate.Location = new System.Drawing.Point(770, 13);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(24, 19);
            this.lblToDate.TabIndex = 5;
            this.lblToDate.Text = "Đến:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(620, 10);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(120, 25);
            this.dtpFromDate.TabIndex = 2;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFromDate.Location = new System.Drawing.Point(560, 13);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(56, 19);
            this.lblFromDate.TabIndex = 3;
            this.lblFromDate.Text = "Từ ngày:";
            // 
            // cboTrangThaiSales
            // 
            this.cboTrangThaiSales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThaiSales.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTrangThaiSales.FormattingEnabled = true;
            this.cboTrangThaiSales.Items.AddRange(new object[] { "Tất cả", "Chưa nhập", "Đã nhập", "Từ chối" });
            this.cboTrangThaiSales.Location = new System.Drawing.Point(130, 8);
            this.cboTrangThaiSales.Name = "cboTrangThaiSales";
            this.cboTrangThaiSales.Size = new System.Drawing.Size(150, 25);
            this.cboTrangThaiSales.TabIndex = 0;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrangThai.Location = new System.Drawing.Point(20, 13);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(104, 19);
            this.lblTrangThai.TabIndex = 1;
            this.lblTrangThai.Text = "Trạng thái Sales:";
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.btnLoc.FlatAppearance.BorderSize = 0;
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(940, 8);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(90, 30);
            this.btnLoc.TabIndex = 6;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelButtons.Controls.Add(this.btnLamMoi);
            this.panelButtons.Controls.Add(this.btnXuatFile);
            this.panelButtons.Controls.Add(this.btnTuChoi);
            this.panelButtons.Controls.Add(this.btnNhapKetoan);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 180);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1500, 50);
            this.panelButtons.TabIndex = 3;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.Gray;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(1310, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoi.TabIndex = 0;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnXuatFile.FlatAppearance.BorderSize = 0;
            this.btnXuatFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatFile.ForeColor = System.Drawing.Color.White;
            this.btnXuatFile.Location = new System.Drawing.Point(1190, 10);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(110, 35);
            this.btnXuatFile.TabIndex = 1;
            this.btnXuatFile.Text = "Xuất File";
            this.btnXuatFile.UseVisualStyleBackColor = false;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnTuChoi
            // 
            this.btnTuChoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnTuChoi.FlatAppearance.BorderSize = 0;
            this.btnTuChoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTuChoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTuChoi.ForeColor = System.Drawing.Color.White;
            this.btnTuChoi.Location = new System.Drawing.Point(1050, 10);
            this.btnTuChoi.Name = "btnTuChoi";
            this.btnTuChoi.Size = new System.Drawing.Size(130, 35);
            this.btnTuChoi.TabIndex = 2;
            this.btnTuChoi.Text = "Từ chối";
            this.btnTuChoi.UseVisualStyleBackColor = false;
            this.btnTuChoi.Click += new System.EventHandler(this.btnTuChoi_Click);
            // 
            // btnNhapKetoan
            // 
            this.btnNhapKetoan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnNhapKetoan.FlatAppearance.BorderSize = 0;
            this.btnNhapKetoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapKetoan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNhapKetoan.ForeColor = System.Drawing.Color.White;
            this.btnNhapKetoan.Location = new System.Drawing.Point(890, 10);
            this.btnNhapKetoan.Name = "btnNhapKetoan";
            this.btnNhapKetoan.Size = new System.Drawing.Size(150, 35);
            this.btnNhapKetoan.TabIndex = 3;
            this.btnNhapKetoan.Text = "Nhập vào kế toán";
            this.btnNhapKetoan.UseVisualStyleBackColor = false;
            this.btnNhapKetoan.Click += new System.EventHandler(this.btnNhapKetoan_Click);
            // 
            // panelGridSales
            // 
            this.panelGridSales.Controls.Add(this.dgvDonHangSales);
            this.panelGridSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridSales.Location = new System.Drawing.Point(0, 230);
            this.panelGridSales.Name = "panelGridSales";
            this.panelGridSales.Padding = new System.Windows.Forms.Padding(10);
            this.panelGridSales.Size = new System.Drawing.Size(1500, 410);
            this.panelGridSales.TabIndex = 4;
            // 
            // dgvDonHangSales
            // 
            this.dgvDonHangSales.AllowUserToAddRows = false;
            this.dgvDonHangSales.AllowUserToDeleteRows = false;
            this.dgvDonHangSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonHangSales.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonHangSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonHangSales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colChon });
            this.dgvDonHangSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonHangSales.Location = new System.Drawing.Point(10, 10);
            this.dgvDonHangSales.Name = "dgvDonHangSales";
            this.dgvDonHangSales.RowHeadersVisible = false;
            this.dgvDonHangSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDonHangSales.Size = new System.Drawing.Size(1480, 390);
            this.dgvDonHangSales.TabIndex = 0;
            // 
            // colChon
            // 
            this.colChon.FalseValue = "0";
            this.colChon.HeaderText = "Chọn";
            this.colChon.Name = "colChon";
            this.colChon.TrueValue = "1";
            this.colChon.Width = 50;
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelSummary.Controls.Add(this.txtTongTien);
            this.panelSummary.Controls.Add(this.lblTongTien);
            this.panelSummary.Controls.Add(this.txtSoDonHang);
            this.panelSummary.Controls.Add(this.lblSoDonHang);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSummary.Location = new System.Drawing.Point(0, 640);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(1500, 40);
            this.panelSummary.TabIndex = 5;
            // 
            // txtTongTien
            // 
            this.txtTongTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtTongTien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.txtTongTien.Location = new System.Drawing.Point(1200, 8);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(200, 19);
            this.txtTongTien.TabIndex = 3;
            this.txtTongTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.lblTongTien.Location = new System.Drawing.Point(1100, 10);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(87, 19);
            this.lblTongTien.TabIndex = 2;
            this.lblTongTien.Text = "Tổng tiền:";
            // 
            // txtSoDonHang
            // 
            this.txtSoDonHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtSoDonHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSoDonHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtSoDonHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.txtSoDonHang.Location = new System.Drawing.Point(200, 8);
            this.txtSoDonHang.Name = "txtSoDonHang";
            this.txtSoDonHang.ReadOnly = true;
            this.txtSoDonHang.Size = new System.Drawing.Size(60, 19);
            this.txtSoDonHang.TabIndex = 1;
            // 
            // lblSoDonHang
            // 
            this.lblSoDonHang.AutoSize = true;
            this.lblSoDonHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSoDonHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.lblSoDonHang.Location = new System.Drawing.Point(20, 10);
            this.lblSoDonHang.Name = "lblSoDonHang";
            this.lblSoDonHang.Size = new System.Drawing.Size(174, 19);
            this.lblSoDonHang.TabIndex = 0;
            this.lblSoDonHang.Text = "Số đơn hàng chờ nhập:";
            // 
            // NhanDuLieuSalesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelGridSales);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.panelHeader);
            this.Name = "NhanDuLieuSalesView";
            this.Size = new System.Drawing.Size(1500, 680);
            this.Load += new System.EventHandler(this.NhanDuLieuSalesView_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelInfo.ResumeLayout(false);
            this.grpHuongDan.ResumeLayout(false);
            this.grpHuongDan.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelGridSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonHangSales)).EndInit();
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.GroupBox grpHuongDan;
        private System.Windows.Forms.TextBox txtHuongDan;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.ComboBox cboTrangThaiSales;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnTuChoi;
        private System.Windows.Forms.Button btnNhapKetoan;
        private System.Windows.Forms.Panel panelGridSales;
        private System.Windows.Forms.DataGridView dgvDonHangSales;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChon;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.TextBox txtSoDonHang;
        private System.Windows.Forms.Label lblSoDonHang;
    }
}
