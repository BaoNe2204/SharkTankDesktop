namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class SaoLuuDuLieuForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpCauHinh = new System.Windows.Forms.GroupBox();
            this.txtThuMuc = new System.Windows.Forms.TextBox();
            this.lblThuMuc = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.lblLoaiDuLieu = new System.Windows.Forms.Label();
            this.cboLoaiDuLieu = new System.Windows.Forms.ComboBox();
            this.lblTenFile = new System.Windows.Forms.Label();
            this.txtTenFile = new System.Windows.Forms.TextBox();
            this.btnSaoLuu = new System.Windows.Forms.Button();
            this.grpLichSu = new System.Windows.Forms.GroupBox();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.colTenFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKichThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnXoaBackup = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.grpCauHinh.SuspendLayout();
            this.grpLichSu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(1065, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "💾 Sao Lưu Dữ Liệu";
            // 
            // grpCauHinh
            // 
            this.grpCauHinh.Controls.Add(this.txtThuMuc);
            this.grpCauHinh.Controls.Add(this.lblThuMuc);
            this.grpCauHinh.Controls.Add(this.btnBrowser);
            this.grpCauHinh.Controls.Add(this.lblLoaiDuLieu);
            this.grpCauHinh.Controls.Add(this.cboLoaiDuLieu);
            this.grpCauHinh.Controls.Add(this.lblTenFile);
            this.grpCauHinh.Controls.Add(this.txtTenFile);
            this.grpCauHinh.Controls.Add(this.btnSaoLuu);
            this.grpCauHinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpCauHinh.Location = new System.Drawing.Point(10, 55);
            this.grpCauHinh.Name = "grpCauHinh";
            this.grpCauHinh.Padding = new System.Windows.Forms.Padding(10);
            this.grpCauHinh.Size = new System.Drawing.Size(1043, 130);
            this.grpCauHinh.TabIndex = 2;
            this.grpCauHinh.TabStop = false;
            this.grpCauHinh.Text = "Cấu Hình Sao Lưu";
            // 
            // txtThuMuc
            // 
            this.txtThuMuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtThuMuc.Location = new System.Drawing.Point(13, 45);
            this.txtThuMuc.Name = "txtThuMuc";
            this.txtThuMuc.ReadOnly = true;
            this.txtThuMuc.Size = new System.Drawing.Size(720, 23);
            this.txtThuMuc.TabIndex = 1;
            // 
            // lblThuMuc
            // 
            this.lblThuMuc.AutoSize = true;
            this.lblThuMuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThuMuc.Location = new System.Drawing.Point(13, 28);
            this.lblThuMuc.Name = "lblThuMuc";
            this.lblThuMuc.Size = new System.Drawing.Size(78, 15);
            this.lblThuMuc.TabIndex = 0;
            this.lblThuMuc.Text = "Thư mục lưu:";
            // 
            // btnBrowser
            // 
            this.btnBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowser.Location = new System.Drawing.Point(815, 42);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(124, 26);
            this.btnBrowser.TabIndex = 2;
            this.btnBrowser.Text = "Chọn thư mục...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // lblLoaiDuLieu
            // 
            this.lblLoaiDuLieu.AutoSize = true;
            this.lblLoaiDuLieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoaiDuLieu.Location = new System.Drawing.Point(13, 75);
            this.lblLoaiDuLieu.Name = "lblLoaiDuLieu";
            this.lblLoaiDuLieu.Size = new System.Drawing.Size(71, 15);
            this.lblLoaiDuLieu.TabIndex = 3;
            this.lblLoaiDuLieu.Text = "Loại dữ liệu:";
            // 
            // cboLoaiDuLieu
            // 
            this.cboLoaiDuLieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiDuLieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLoaiDuLieu.Items.AddRange(new object[] {
            "Toàn bộ dữ liệu",
            "Dữ liệu CRM",
            "Dữ liệu Kho hàng",
            "Dữ liệu Nhân sự",
            "Dữ liệu Tài chính"});
            this.cboLoaiDuLieu.Location = new System.Drawing.Point(13, 92);
            this.cboLoaiDuLieu.Name = "cboLoaiDuLieu";
            this.cboLoaiDuLieu.Size = new System.Drawing.Size(220, 23);
            this.cboLoaiDuLieu.TabIndex = 4;
            // 
            // lblTenFile
            // 
            this.lblTenFile.AutoSize = true;
            this.lblTenFile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenFile.Location = new System.Drawing.Point(260, 75);
            this.lblTenFile.Name = "lblTenFile";
            this.lblTenFile.Size = new System.Drawing.Size(48, 15);
            this.lblTenFile.TabIndex = 5;
            this.lblTenFile.Text = "Tên file:";
            // 
            // txtTenFile
            // 
            this.txtTenFile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenFile.Location = new System.Drawing.Point(260, 92);
            this.txtTenFile.Name = "txtTenFile";
            this.txtTenFile.Size = new System.Drawing.Size(453, 23);
            this.txtTenFile.TabIndex = 6;
            // 
            // btnSaoLuu
            // 
            this.btnSaoLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSaoLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaoLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaoLuu.ForeColor = System.Drawing.Color.White;
            this.btnSaoLuu.Location = new System.Drawing.Point(815, 84);
            this.btnSaoLuu.Name = "btnSaoLuu";
            this.btnSaoLuu.Size = new System.Drawing.Size(124, 35);
            this.btnSaoLuu.TabIndex = 7;
            this.btnSaoLuu.Text = "Sao Lưu";
            this.btnSaoLuu.UseVisualStyleBackColor = false;
            this.btnSaoLuu.Click += new System.EventHandler(this.btnSaoLuu_Click);
            // 
            // grpLichSu
            // 
            this.grpLichSu.Controls.Add(this.dgvLichSu);
            this.grpLichSu.Controls.Add(this.btnXoaBackup);
            this.grpLichSu.Controls.Add(this.btnLamMoi);
            this.grpLichSu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpLichSu.Location = new System.Drawing.Point(10, 240);
            this.grpLichSu.Name = "grpLichSu";
            this.grpLichSu.Padding = new System.Windows.Forms.Padding(10);
            this.grpLichSu.Size = new System.Drawing.Size(1043, 327);
            this.grpLichSu.TabIndex = 5;
            this.grpLichSu.TabStop = false;
            this.grpLichSu.Text = "Lịch Sử Sao Lưu";
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTenFile,
            this.colNgayTao,
            this.colKichThuoc});
            this.dgvLichSu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvLichSu.Location = new System.Drawing.Point(3, 19);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.ReadOnly = true;
            this.dgvLichSu.RowHeadersVisible = false;
            this.dgvLichSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSu.Size = new System.Drawing.Size(1020, 242);
            this.dgvLichSu.TabIndex = 0;
            // 
            // colTenFile
            // 
            this.colTenFile.HeaderText = "Tên File";
            this.colTenFile.Name = "colTenFile";
            this.colTenFile.ReadOnly = true;
            this.colTenFile.Width = 450;
            // 
            // colNgayTao
            // 
            this.colNgayTao.HeaderText = "Ngày Tạo";
            this.colNgayTao.Name = "colNgayTao";
            this.colNgayTao.ReadOnly = true;
            this.colNgayTao.Width = 200;
            // 
            // colKichThuoc
            // 
            this.colKichThuoc.HeaderText = "Kích Thước";
            this.colKichThuoc.Name = "colKichThuoc";
            this.colKichThuoc.ReadOnly = true;
            this.colKichThuoc.Width = 120;
            // 
            // btnXoaBackup
            // 
            this.btnXoaBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaBackup.ForeColor = System.Drawing.Color.Red;
            this.btnXoaBackup.Location = new System.Drawing.Point(773, 274);
            this.btnXoaBackup.Name = "btnXoaBackup";
            this.btnXoaBackup.Size = new System.Drawing.Size(120, 28);
            this.btnXoaBackup.TabIndex = 2;
            this.btnXoaBackup.Text = "Xóa Backup";
            this.btnXoaBackup.UseVisualStyleBackColor = true;
            this.btnXoaBackup.Click += new System.EventHandler(this.btnXoaBackup_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Location = new System.Drawing.Point(899, 274);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(124, 28);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 192);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1043, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 3;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrangThai.ForeColor = System.Drawing.Color.Gray;
            this.lblTrangThai.Location = new System.Drawing.Point(10, 215);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(500, 18);
            this.lblTrangThai.TabIndex = 4;
            this.lblTrangThai.Text = "Sẵn sàng sao lưu dữ liệu...";
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(852, 5);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(40, 40);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIcon.TabIndex = 1;
            this.picIcon.TabStop = false;
            // 
            // SaoLuuDuLieuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.grpLichSu);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.grpCauHinh);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.lblTitle);
            this.Name = "SaoLuuDuLieuForm";
            this.Size = new System.Drawing.Size(1065, 592);
            this.Load += new System.EventHandler(this.SaoLuuForm_Load);
            this.grpCauHinh.ResumeLayout(false);
            this.grpCauHinh.PerformLayout();
            this.grpLichSu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.GroupBox grpCauHinh;
        private System.Windows.Forms.Label lblThuMuc;
        private System.Windows.Forms.TextBox txtThuMuc;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.Label lblLoaiDuLieu;
        private System.Windows.Forms.ComboBox cboLoaiDuLieu;
        private System.Windows.Forms.Label lblTenFile;
        private System.Windows.Forms.TextBox txtTenFile;
        private System.Windows.Forms.Button btnSaoLuu;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.GroupBox grpLichSu;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayTao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKichThuoc;
        private System.Windows.Forms.Button btnXoaBackup;
        private System.Windows.Forms.Button btnLamMoi;
        #endregion
    }
}
