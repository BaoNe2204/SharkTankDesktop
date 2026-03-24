namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class LichSuDangNhapForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnXoaLoc;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.DataGridView dgvLoginHistory;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Button btnXemChiTiet;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnXoaLoc = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.dgvLoginHistory = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoginHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1164, 48);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(514, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "LỊCH SỬ ĐĂNG NHẬP";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelFilter.Controls.Add(this.btnXemChiTiet);
            this.panelFilter.Controls.Add(this.btnXuatExcel);
            this.panelFilter.Controls.Add(this.btnXoaLoc);
            this.panelFilter.Controls.Add(this.btnTimKiem);
            this.panelFilter.Controls.Add(this.cboStatus);
            this.panelFilter.Controls.Add(this.lblStatus);
            this.panelFilter.Controls.Add(this.txtUsername);
            this.panelFilter.Controls.Add(this.lblUsername);
            this.panelFilter.Controls.Add(this.dtpToDate);
            this.panelFilter.Controls.Add(this.lblDenNgay);
            this.panelFilter.Controls.Add(this.dtpFromDate);
            this.panelFilter.Controls.Add(this.lblTuNgay);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 48);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1164, 66);
            this.panelFilter.TabIndex = 1;
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemChiTiet.Location = new System.Drawing.Point(1009, 13);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(73, 25);
            this.btnXemChiTiet.TabIndex = 11;
            this.btnXemChiTiet.Text = "Xem chi tiết";
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Location = new System.Drawing.Point(930, 13);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(73, 24);
            this.btnXuatExcel.TabIndex = 10;
            this.btnXuatExcel.Text = "📥 Excel";
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnXoaLoc
            // 
            this.btnXoaLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaLoc.Location = new System.Drawing.Point(849, 13);
            this.btnXoaLoc.Name = "btnXoaLoc";
            this.btnXoaLoc.Size = new System.Drawing.Size(73, 24);
            this.btnXoaLoc.TabIndex = 9;
            this.btnXoaLoc.Text = "Xóa lọc";
            this.btnXoaLoc.Click += new System.EventHandler(this.btnXoaLoc_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(767, 13);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(73, 24);
            this.btnTimKiem.TabIndex = 8;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Items.AddRange(new object[] {
            "Tất cả",
            "Thành công",
            "Thất bại",
            "Bị khóa"});
            this.cboStatus.Location = new System.Drawing.Point(604, 16);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(103, 21);
            this.cboStatus.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(538, 19);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 15);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Trạng thái";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(411, 16);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(129, 20);
            this.txtUsername.TabIndex = 5;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsername.Location = new System.Drawing.Point(347, 19);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 15);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Tài khoản";
            this.lblUsername.Click += new System.EventHandler(this.lblUsername_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(236, 16);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(112, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDenNgay.Location = new System.Drawing.Point(178, 19);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(57, 15);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến ngày";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(60, 16);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(112, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTuNgay.Location = new System.Drawing.Point(13, 19);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(50, 15);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ ngày";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelBottom.Controls.Add(this.lblTongCong);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 541);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1164, 37);
            this.panelBottom.TabIndex = 3;
            // 
            // lblTongCong
            // 
            this.lblTongCong.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTongCong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblTongCong.Location = new System.Drawing.Point(0, 0);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.lblTongCong.Size = new System.Drawing.Size(343, 37);
            this.lblTongCong.TabIndex = 0;
            this.lblTongCong.Text = "Tổng cộng: 0 bản ghi";
            this.lblTongCong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvLoginHistory
            // 
            this.dgvLoginHistory.AllowUserToAddRows = false;
            this.dgvLoginHistory.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.dgvLoginHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLoginHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvLoginHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLoginHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLoginHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoginHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLoginHistory.EnableHeadersVisualStyles = false;
            this.dgvLoginHistory.Location = new System.Drawing.Point(0, 114);
            this.dgvLoginHistory.Name = "dgvLoginHistory";
            this.dgvLoginHistory.ReadOnly = true;
            this.dgvLoginHistory.RowHeadersVisible = false;
            this.dgvLoginHistory.RowHeadersWidth = 51;
            this.dgvLoginHistory.RowTemplate.Height = 28;
            this.dgvLoginHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoginHistory.Size = new System.Drawing.Size(1164, 427);
            this.dgvLoginHistory.TabIndex = 2;
            // 
            // LichSuDangNhapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvLoginHistory);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelTop);
            this.Name = "LichSuDangNhapForm";
            this.Size = new System.Drawing.Size(1164, 578);
            this.Load += new System.EventHandler(this.LichSuDangNhapForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoginHistory)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
