namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class LichSuHoatDongForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnXoaLoc;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.ComboBox cboAction;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ComboBox cboEntityType;
        private System.Windows.Forms.Label lblEntityType;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DataGridView dgvAuditLogs;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Button btnXoaLog;

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
            this.btnXoaLog = new System.Windows.Forms.Button();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnXoaLoc = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.cboAction = new System.Windows.Forms.ComboBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.cboEntityType = new System.Windows.Forms.ComboBox();
            this.lblEntityType = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.dgvAuditLogs = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1217, 48);
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
            this.lblTitle.Text = "LỊCH SỬ THAO TÁC";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelFilter.Controls.Add(this.btnXoaLog);
            this.panelFilter.Controls.Add(this.btnXemChiTiet);
            this.panelFilter.Controls.Add(this.btnXuatExcel);
            this.panelFilter.Controls.Add(this.btnXoaLoc);
            this.panelFilter.Controls.Add(this.btnTimKiem);
            this.panelFilter.Controls.Add(this.cboAction);
            this.panelFilter.Controls.Add(this.lblAction);
            this.panelFilter.Controls.Add(this.cboEntityType);
            this.panelFilter.Controls.Add(this.lblEntityType);
            this.panelFilter.Controls.Add(this.txtUsername);
            this.panelFilter.Controls.Add(this.lblUsername);
            this.panelFilter.Controls.Add(this.dtpToDate);
            this.panelFilter.Controls.Add(this.lblDenNgay);
            this.panelFilter.Controls.Add(this.dtpFromDate);
            this.panelFilter.Controls.Add(this.lblTuNgay);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 48);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1217, 71);
            this.panelFilter.TabIndex = 1;
            // 
            // btnXoaLog
            // 
            this.btnXoaLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoaLog.Location = new System.Drawing.Point(62, 41);
            this.btnXoaLog.Name = "btnXoaLog";
            this.btnXoaLog.Size = new System.Drawing.Size(111, 24);
            this.btnXoaLog.TabIndex = 14;
            this.btnXoaLog.Text = "🗑️ Xóa log cũ (>90 ngày)";
            this.btnXoaLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXoaLog.Click += new System.EventHandler(this.btnXoaLog_Click);
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemChiTiet.Location = new System.Drawing.Point(1020, 13);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(88, 24);
            this.btnXemChiTiet.TabIndex = 13;
            this.btnXemChiTiet.Text = "Xem chi tiết";
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Location = new System.Drawing.Point(1114, 13);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(64, 24);
            this.btnXuatExcel.TabIndex = 12;
            this.btnXuatExcel.Text = "📥 Excel";
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnXoaLoc
            // 
            this.btnXoaLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaLoc.Location = new System.Drawing.Point(947, 13);
            this.btnXoaLoc.Name = "btnXoaLoc";
            this.btnXoaLoc.Size = new System.Drawing.Size(64, 24);
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
            this.btnTimKiem.Location = new System.Drawing.Point(870, 13);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(69, 24);
            this.btnTimKiem.TabIndex = 10;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cboAction
            // 
            this.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAction.Items.AddRange(new object[] {
            "Tất cả",
            "CREATE",
            "UPDATE",
            "DELETE",
            "LOGIN",
            "LOGOUT",
            "VIEW"});
            this.cboAction.Location = new System.Drawing.Point(758, 16);
            this.cboAction.Name = "cboAction";
            this.cboAction.Size = new System.Drawing.Size(95, 21);
            this.cboAction.TabIndex = 9;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAction.Location = new System.Drawing.Point(694, 19);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(67, 15);
            this.lblAction.TabIndex = 8;
            this.lblAction.Text = "Hành động";
            // 
            // cboEntityType
            // 
            this.cboEntityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEntityType.Items.AddRange(new object[] {
            "Tất cả",
            "System",
            "Admin",
            "HR",
            "Accounting",
            "Sales",
            "Inventory",
            "Users",
            "Employees",
            "Departments",
            "Roles",
            "Permissions",
            "SystemConfigs",
            "SystemNotifications",
            "LoginHistory",
            "AuditLogs",
            "DataChangeLogs"});
            this.cboEntityType.Location = new System.Drawing.Point(586, 15);
            this.cboEntityType.Name = "cboEntityType";
            this.cboEntityType.Size = new System.Drawing.Size(102, 21);
            this.cboEntityType.TabIndex = 7;
            // 
            // lblEntityType
            // 
            this.lblEntityType.AutoSize = true;
            this.lblEntityType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEntityType.Location = new System.Drawing.Point(520, 19);
            this.lblEntityType.Name = "lblEntityType";
            this.lblEntityType.Size = new System.Drawing.Size(60, 15);
            this.lblEntityType.TabIndex = 6;
            this.lblEntityType.Text = "Đối tượng";
            this.lblEntityType.Click += new System.EventHandler(this.lblEntityType_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(421, 14);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(93, 20);
            this.txtUsername.TabIndex = 5;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsername.Location = new System.Drawing.Point(360, 19);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 15);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Tài khoản";
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
            this.dtpFromDate.Location = new System.Drawing.Point(62, 16);
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
            this.lblTuNgay.Click += new System.EventHandler(this.lblTuNgay_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelBottom.Controls.Add(this.lblTongCong);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 572);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1217, 30);
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
            // dgvAuditLogs
            // 
            this.dgvAuditLogs.AllowUserToAddRows = false;
            this.dgvAuditLogs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.dgvAuditLogs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAuditLogs.BackgroundColor = System.Drawing.Color.White;
            this.dgvAuditLogs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAuditLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAuditLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuditLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAuditLogs.EnableHeadersVisualStyles = false;
            this.dgvAuditLogs.Location = new System.Drawing.Point(0, 119);
            this.dgvAuditLogs.Name = "dgvAuditLogs";
            this.dgvAuditLogs.ReadOnly = true;
            this.dgvAuditLogs.RowHeadersVisible = false;
            this.dgvAuditLogs.RowHeadersWidth = 51;
            this.dgvAuditLogs.RowTemplate.Height = 28;
            this.dgvAuditLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAuditLogs.Size = new System.Drawing.Size(1217, 453);
            this.dgvAuditLogs.TabIndex = 2;
            // 
            // LichSuHoatDongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvAuditLogs);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelTop);
            this.Name = "LichSuHoatDongForm";
            this.Size = new System.Drawing.Size(1217, 602);
            this.Load += new System.EventHandler(this.LichSuHoatDongForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLogs)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
