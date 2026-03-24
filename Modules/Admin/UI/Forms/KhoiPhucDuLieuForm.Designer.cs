namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class KhoiPhucDuLieuForm
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
            this.grpCanhBao = new System.Windows.Forms.GroupBox();
            this.lblCanhBao = new System.Windows.Forms.Label();
            this.grpChonFile = new System.Windows.Forms.GroupBox();
            this.lblDuongDan = new System.Windows.Forms.Label();
            this.txtDuongDan = new System.Windows.Forms.TextBox();
            this.btnChonFile = new System.Windows.Forms.Button();
            this.btnKhoiPhuc = new System.Windows.Forms.Button();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.txtNgayTao = new System.Windows.Forms.TextBox();
            this.lblKichThuoc = new System.Windows.Forms.Label();
            this.txtKichThuoc = new System.Windows.Forms.TextBox();
            this.lblLoaiDuLieu = new System.Windows.Forms.Label();
            this.txtLoaiDuLieu = new System.Windows.Forms.TextBox();
            this.chkGhiDe = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvBackupFiles = new System.Windows.Forms.DataGridView();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnXoaFile = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.grpCanhBao.SuspendLayout();
            this.grpChonFile.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackupFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(1065, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🔄 Khôi Phục Dữ Liệu";
            // 
            // grpCanhBao
            // 
            this.grpCanhBao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.grpCanhBao.Controls.Add(this.lblCanhBao);
            this.grpCanhBao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpCanhBao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpCanhBao.Location = new System.Drawing.Point(10, 55);
            this.grpCanhBao.Name = "grpCanhBao";
            this.grpCanhBao.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.grpCanhBao.Size = new System.Drawing.Size(1039, 50);
            this.grpCanhBao.TabIndex = 2;
            this.grpCanhBao.TabStop = false;
            this.grpCanhBao.Text = "⚠ Cảnh Báo";
            // 
            // lblCanhBao
            // 
            this.lblCanhBao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCanhBao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(100)))), ((int)(((byte)(4)))));
            this.lblCanhBao.Location = new System.Drawing.Point(16, 21);
            this.lblCanhBao.Name = "lblCanhBao";
            this.lblCanhBao.Size = new System.Drawing.Size(854, 20);
            this.lblCanhBao.TabIndex = 0;
            this.lblCanhBao.Text = "Việc khôi phục dữ liệu sẽ thay thế toàn bộ dữ liệu hiện tại. Hãy đảm bảo bạn đã s" +
    "ao lưu!";
            // 
            // grpChonFile
            // 
            this.grpChonFile.Controls.Add(this.lblDuongDan);
            this.grpChonFile.Controls.Add(this.txtDuongDan);
            this.grpChonFile.Controls.Add(this.btnChonFile);
            this.grpChonFile.Controls.Add(this.btnKhoiPhuc);
            this.grpChonFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpChonFile.Location = new System.Drawing.Point(10, 115);
            this.grpChonFile.Name = "grpChonFile";
            this.grpChonFile.Padding = new System.Windows.Forms.Padding(10);
            this.grpChonFile.Size = new System.Drawing.Size(1039, 99);
            this.grpChonFile.TabIndex = 3;
            this.grpChonFile.TabStop = false;
            this.grpChonFile.Text = "Chọn File Backup";
            // 
            // lblDuongDan
            // 
            this.lblDuongDan.AutoSize = true;
            this.lblDuongDan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDuongDan.Location = new System.Drawing.Point(17, 14);
            this.lblDuongDan.Name = "lblDuongDan";
            this.lblDuongDan.Size = new System.Drawing.Size(69, 15);
            this.lblDuongDan.TabIndex = 0;
            this.lblDuongDan.Text = "Đường dẫn:";
            // 
            // txtDuongDan
            // 
            this.txtDuongDan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDuongDan.Location = new System.Drawing.Point(20, 32);
            this.txtDuongDan.Name = "txtDuongDan";
            this.txtDuongDan.ReadOnly = true;
            this.txtDuongDan.Size = new System.Drawing.Size(720, 23);
            this.txtDuongDan.TabIndex = 1;
            // 
            // btnChonFile
            // 
            this.btnChonFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChonFile.Location = new System.Drawing.Point(746, 29);
            this.btnChonFile.Name = "btnChonFile";
            this.btnChonFile.Size = new System.Drawing.Size(124, 26);
            this.btnChonFile.TabIndex = 2;
            this.btnChonFile.Text = "Chọn file...";
            this.btnChonFile.UseVisualStyleBackColor = true;
            this.btnChonFile.Click += new System.EventHandler(this.btnChonFile_Click);
            // 
            // btnKhoiPhuc
            // 
            this.btnKhoiPhuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnKhoiPhuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhoiPhuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKhoiPhuc.ForeColor = System.Drawing.Color.White;
            this.btnKhoiPhuc.Location = new System.Drawing.Point(16, 61);
            this.btnKhoiPhuc.Name = "btnKhoiPhuc";
            this.btnKhoiPhuc.Size = new System.Drawing.Size(854, 30);
            this.btnKhoiPhuc.TabIndex = 3;
            this.btnKhoiPhuc.Text = "🔄 Khôi Phục Dữ Liệu";
            this.btnKhoiPhuc.UseVisualStyleBackColor = false;
            this.btnKhoiPhuc.Click += new System.EventHandler(this.btnKhoiPhuc_Click);
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.lblNgayTao);
            this.grpThongTin.Controls.Add(this.txtNgayTao);
            this.grpThongTin.Controls.Add(this.lblKichThuoc);
            this.grpThongTin.Controls.Add(this.txtKichThuoc);
            this.grpThongTin.Controls.Add(this.lblLoaiDuLieu);
            this.grpThongTin.Controls.Add(this.txtLoaiDuLieu);
            this.grpThongTin.Controls.Add(this.chkGhiDe);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpThongTin.Location = new System.Drawing.Point(10, 210);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Padding = new System.Windows.Forms.Padding(10);
            this.grpThongTin.Size = new System.Drawing.Size(1039, 80);
            this.grpThongTin.TabIndex = 4;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông Tin File";
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayTao.Location = new System.Drawing.Point(13, 22);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(58, 15);
            this.lblNgayTao.TabIndex = 0;
            this.lblNgayTao.Text = "Ngày tạo:";
            // 
            // txtNgayTao
            // 
            this.txtNgayTao.BackColor = System.Drawing.Color.White;
            this.txtNgayTao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNgayTao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNgayTao.ForeColor = System.Drawing.Color.Gray;
            this.txtNgayTao.Location = new System.Drawing.Point(13, 38);
            this.txtNgayTao.Name = "txtNgayTao";
            this.txtNgayTao.ReadOnly = true;
            this.txtNgayTao.Size = new System.Drawing.Size(180, 16);
            this.txtNgayTao.TabIndex = 1;
            this.txtNgayTao.Text = "(Chưa chọn file)";
            // 
            // lblKichThuoc
            // 
            this.lblKichThuoc.AutoSize = true;
            this.lblKichThuoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKichThuoc.Location = new System.Drawing.Point(220, 22);
            this.lblKichThuoc.Name = "lblKichThuoc";
            this.lblKichThuoc.Size = new System.Drawing.Size(67, 15);
            this.lblKichThuoc.TabIndex = 2;
            this.lblKichThuoc.Text = "Kích thước:";
            // 
            // txtKichThuoc
            // 
            this.txtKichThuoc.BackColor = System.Drawing.Color.White;
            this.txtKichThuoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKichThuoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKichThuoc.ForeColor = System.Drawing.Color.Gray;
            this.txtKichThuoc.Location = new System.Drawing.Point(220, 38);
            this.txtKichThuoc.Name = "txtKichThuoc";
            this.txtKichThuoc.ReadOnly = true;
            this.txtKichThuoc.Size = new System.Drawing.Size(120, 16);
            this.txtKichThuoc.TabIndex = 3;
            this.txtKichThuoc.Text = "-";
            // 
            // lblLoaiDuLieu
            // 
            this.lblLoaiDuLieu.AutoSize = true;
            this.lblLoaiDuLieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoaiDuLieu.Location = new System.Drawing.Point(370, 22);
            this.lblLoaiDuLieu.Name = "lblLoaiDuLieu";
            this.lblLoaiDuLieu.Size = new System.Drawing.Size(71, 15);
            this.lblLoaiDuLieu.TabIndex = 4;
            this.lblLoaiDuLieu.Text = "Loại dữ liệu:";
            // 
            // txtLoaiDuLieu
            // 
            this.txtLoaiDuLieu.BackColor = System.Drawing.Color.White;
            this.txtLoaiDuLieu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLoaiDuLieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLoaiDuLieu.ForeColor = System.Drawing.Color.Gray;
            this.txtLoaiDuLieu.Location = new System.Drawing.Point(370, 38);
            this.txtLoaiDuLieu.Name = "txtLoaiDuLieu";
            this.txtLoaiDuLieu.ReadOnly = true;
            this.txtLoaiDuLieu.Size = new System.Drawing.Size(140, 16);
            this.txtLoaiDuLieu.TabIndex = 5;
            this.txtLoaiDuLieu.Text = "-";
            // 
            // chkGhiDe
            // 
            this.chkGhiDe.AutoSize = true;
            this.chkGhiDe.Checked = true;
            this.chkGhiDe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGhiDe.Location = new System.Drawing.Point(13, 55);
            this.chkGhiDe.Name = "chkGhiDe";
            this.chkGhiDe.Size = new System.Drawing.Size(148, 19);
            this.chkGhiDe.TabIndex = 6;
            this.chkGhiDe.Text = "Ghi đè dữ liệu hiện tại";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 300);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1039, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 5;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrangThai.ForeColor = System.Drawing.Color.Gray;
            this.lblTrangThai.Location = new System.Drawing.Point(10, 323);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(500, 18);
            this.lblTrangThai.TabIndex = 6;
            this.lblTrangThai.Text = "Sẵn sàng khôi phục dữ liệu...";
            // 
            // grpDanhSach
            // 
            this.grpDanhSach.Controls.Add(this.dgvBackupFiles);
            this.grpDanhSach.Controls.Add(this.btnXoaFile);
            this.grpDanhSach.Controls.Add(this.btnLamMoi);
            this.grpDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpDanhSach.Location = new System.Drawing.Point(10, 348);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Padding = new System.Windows.Forms.Padding(10);
            this.grpDanhSach.Size = new System.Drawing.Size(1039, 212);
            this.grpDanhSach.TabIndex = 7;
            this.grpDanhSach.TabStop = false;
            this.grpDanhSach.Text = "Danh Sách File Backup Có Sẵn";
            // 
            // dgvBackupFiles
            // 
            this.dgvBackupFiles.AllowUserToAddRows = false;
            this.dgvBackupFiles.AllowUserToDeleteRows = false;
            this.dgvBackupFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBackupFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileName,
            this.colCreateDate,
            this.colSize});
            this.dgvBackupFiles.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvBackupFiles.Location = new System.Drawing.Point(10, 29);
            this.dgvBackupFiles.Name = "dgvBackupFiles";
            this.dgvBackupFiles.ReadOnly = true;
            this.dgvBackupFiles.RowHeadersVisible = false;
            this.dgvBackupFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBackupFiles.Size = new System.Drawing.Size(1016, 118);
            this.dgvBackupFiles.TabIndex = 0;
            this.dgvBackupFiles.SelectionChanged += new System.EventHandler(this.dgvBackupFiles_SelectionChanged);
            this.dgvBackupFiles.DoubleClick += new System.EventHandler(this.dgvBackupFiles_DoubleClick);
            // 
            // colFileName
            // 
            this.colFileName.HeaderText = "Tên File";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            this.colFileName.Width = 450;
            // 
            // colCreateDate
            // 
            this.colCreateDate.HeaderText = "Ngày Tạo";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.ReadOnly = true;
            this.colCreateDate.Width = 200;
            // 
            // colSize
            // 
            this.colSize.HeaderText = "Kích Thước";
            this.colSize.Name = "colSize";
            this.colSize.ReadOnly = true;
            this.colSize.Width = 120;
            // 
            // btnXoaFile
            // 
            this.btnXoaFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaFile.ForeColor = System.Drawing.Color.Red;
            this.btnXoaFile.Location = new System.Drawing.Point(776, 171);
            this.btnXoaFile.Name = "btnXoaFile";
            this.btnXoaFile.Size = new System.Drawing.Size(120, 28);
            this.btnXoaFile.TabIndex = 2;
            this.btnXoaFile.Text = "Xóa File";
            this.btnXoaFile.UseVisualStyleBackColor = true;
            this.btnXoaFile.Click += new System.EventHandler(this.btnXoaFile_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Location = new System.Drawing.Point(902, 171);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(124, 28);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
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
            // KhoiPhucDuLieuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.grpDanhSach);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.grpChonFile);
            this.Controls.Add(this.grpCanhBao);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.lblTitle);
            this.Name = "KhoiPhucDuLieuForm";
            this.Size = new System.Drawing.Size(1065, 592);
            this.Load += new System.EventHandler(this.KhoiPhucForm_Load);
            this.grpCanhBao.ResumeLayout(false);
            this.grpChonFile.ResumeLayout(false);
            this.grpChonFile.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.grpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackupFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.GroupBox grpCanhBao;
        private System.Windows.Forms.Label lblCanhBao;
        private System.Windows.Forms.GroupBox grpChonFile;
        private System.Windows.Forms.Label lblDuongDan;
        private System.Windows.Forms.TextBox txtDuongDan;
        private System.Windows.Forms.Button btnChonFile;
        private System.Windows.Forms.Button btnKhoiPhuc;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.TextBox txtNgayTao;
        private System.Windows.Forms.Label lblKichThuoc;
        private System.Windows.Forms.TextBox txtKichThuoc;
        private System.Windows.Forms.Label lblLoaiDuLieu;
        private System.Windows.Forms.TextBox txtLoaiDuLieu;
        private System.Windows.Forms.CheckBox chkGhiDe;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.DataGridView dgvBackupFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
        private System.Windows.Forms.Button btnXoaFile;
        private System.Windows.Forms.Button btnLamMoi;
        #endregion
    }
}
