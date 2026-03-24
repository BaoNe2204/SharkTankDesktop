namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class NhatKyHeThongForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnXoaLoc;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.ComboBox cboChangeType;
        private System.Windows.Forms.Label lblChangeType;
        private System.Windows.Forms.TextBox txtRecordId;
        private System.Windows.Forms.Label lblRecordId;
        private System.Windows.Forms.ComboBox cboTableName;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DataGridView dgvDataChange;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblTongCong;

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
            this.cboChangeType = new System.Windows.Forms.ComboBox();
            this.lblChangeType = new System.Windows.Forms.Label();
            this.txtRecordId = new System.Windows.Forms.TextBox();
            this.lblRecordId = new System.Windows.Forms.Label();
            this.cboTableName = new System.Windows.Forms.ComboBox();
            this.lblTableName = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.dgvDataChange = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataChange)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1182, 48);
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
            this.lblTitle.Text = "THEO DÕI THAY ĐỔI DỮ LIỆU";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelFilter.Controls.Add(this.btnXemChiTiet);
            this.panelFilter.Controls.Add(this.btnXuatExcel);
            this.panelFilter.Controls.Add(this.btnXoaLoc);
            this.panelFilter.Controls.Add(this.btnTimKiem);
            this.panelFilter.Controls.Add(this.cboChangeType);
            this.panelFilter.Controls.Add(this.lblChangeType);
            this.panelFilter.Controls.Add(this.txtRecordId);
            this.panelFilter.Controls.Add(this.lblRecordId);
            this.panelFilter.Controls.Add(this.cboTableName);
            this.panelFilter.Controls.Add(this.lblTableName);
            this.panelFilter.Controls.Add(this.dtpToDate);
            this.panelFilter.Controls.Add(this.lblDenNgay);
            this.panelFilter.Controls.Add(this.dtpFromDate);
            this.panelFilter.Controls.Add(this.lblTuNgay);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 48);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1182, 72);
            this.panelFilter.TabIndex = 1;
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemChiTiet.Location = new System.Drawing.Point(896, 41);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(78, 25);
            this.btnXemChiTiet.TabIndex = 13;
            this.btnXemChiTiet.Text = "Xem chi tiết";
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Location = new System.Drawing.Point(980, 41);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(65, 25);
            this.btnXuatExcel.TabIndex = 12;
            this.btnXuatExcel.Text = "📥 Excel";
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnXoaLoc
            // 
            this.btnXoaLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaLoc.Location = new System.Drawing.Point(980, 12);
            this.btnXoaLoc.Name = "btnXoaLoc";
            this.btnXoaLoc.Size = new System.Drawing.Size(65, 24);
            this.btnXoaLoc.TabIndex = 11;
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
            this.btnTimKiem.Location = new System.Drawing.Point(896, 13);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(78, 24);
            this.btnTimKiem.TabIndex = 10;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cboChangeType
            // 
            this.cboChangeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChangeType.Items.AddRange(new object[] {
            "Tất cả",
            "INSERT",
            "UPDATE",
            "DELETE"});
            this.cboChangeType.Location = new System.Drawing.Point(806, 16);
            this.cboChangeType.Name = "cboChangeType";
            this.cboChangeType.Size = new System.Drawing.Size(78, 21);
            this.cboChangeType.TabIndex = 9;
            // 
            // lblChangeType
            // 
            this.lblChangeType.AutoSize = true;
            this.lblChangeType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChangeType.Location = new System.Drawing.Point(741, 19);
            this.lblChangeType.Name = "lblChangeType";
            this.lblChangeType.Size = new System.Drawing.Size(75, 15);
            this.lblChangeType.TabIndex = 8;
            this.lblChangeType.Text = "Loại thay đổi";
            // 
            // txtRecordId
            // 
            this.txtRecordId.Location = new System.Drawing.Point(643, 16);
            this.txtRecordId.Name = "txtRecordId";
            this.txtRecordId.Size = new System.Drawing.Size(86, 20);
            this.txtRecordId.TabIndex = 7;
            // 
            // lblRecordId
            // 
            this.lblRecordId.AutoSize = true;
            this.lblRecordId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRecordId.Location = new System.Drawing.Point(587, 19);
            this.lblRecordId.Name = "lblRecordId";
            this.lblRecordId.Size = new System.Drawing.Size(61, 15);
            this.lblRecordId.TabIndex = 6;
            this.lblRecordId.Text = "ID bản ghi";
            // 
            // cboTableName
            // 
            this.cboTableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTableName.Items.AddRange(new object[] {
            "Tất cả",
            "NhanVien",
            "KhachHang",
            "SanPham",
            "Leads",
            "Users",
            "Employees",
            "Departments",
            "PhongBan",
            "ChucVu",
            "Roles",
            "Kho",
            "ViTriKho",
            "NhapKho",
            "XuatKho",
            "ChamSocKhachHang",
            "QLCoHoiBanHang",
            "NghiPhep",
            "Permissions",
            "RolePermissions",
            "SystemNotifications",
            "LoginHistory",
            "AuditLogs"});
            this.cboTableName.Location = new System.Drawing.Point(446, 16);
            this.cboTableName.Name = "cboTableName";
            this.cboTableName.Size = new System.Drawing.Size(129, 21);
            this.cboTableName.TabIndex = 5;
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTableName.Location = new System.Drawing.Point(360, 19);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(56, 15);
            this.lblTableName.TabIndex = 4;
            this.lblTableName.Text = "Tên bảng";
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
            this.lblDenNgay.Location = new System.Drawing.Point(180, 19);
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
            this.panelBottom.Location = new System.Drawing.Point(0, 530);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1182, 30);
            this.panelBottom.TabIndex = 3;
            // 
            // lblTongCong
            // 
            this.lblTongCong.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTongCong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblTongCong.Location = new System.Drawing.Point(0, 0);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.lblTongCong.Size = new System.Drawing.Size(343, 30);
            this.lblTongCong.TabIndex = 0;
            this.lblTongCong.Text = "Tổng cộng: 0 bản ghi";
            this.lblTongCong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvDataChange
            // 
            this.dgvDataChange.AllowUserToAddRows = false;
            this.dgvDataChange.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.dgvDataChange.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDataChange.BackgroundColor = System.Drawing.Color.White;
            this.dgvDataChange.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDataChange.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDataChange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDataChange.EnableHeadersVisualStyles = false;
            this.dgvDataChange.Location = new System.Drawing.Point(0, 120);
            this.dgvDataChange.Name = "dgvDataChange";
            this.dgvDataChange.ReadOnly = true;
            this.dgvDataChange.RowHeadersVisible = false;
            this.dgvDataChange.RowHeadersWidth = 51;
            this.dgvDataChange.RowTemplate.Height = 28;
            this.dgvDataChange.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDataChange.Size = new System.Drawing.Size(1182, 410);
            this.dgvDataChange.TabIndex = 2;
            // 
            // NhatKyHeThongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvDataChange);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelTop);
            this.Name = "NhatKyHeThongForm";
            this.Size = new System.Drawing.Size(1182, 560);
            this.Load += new System.EventHandler(this.NhatKyHeThongForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataChange)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
