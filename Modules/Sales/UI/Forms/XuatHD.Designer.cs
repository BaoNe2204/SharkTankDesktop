namespace SharkTank.Modules.Sales.UI.Forms
{
    partial class XuatHD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Khai báo các control (Public để file code logic dễ gọi)
        public System.Windows.Forms.GroupBox gbThongTin;
        public System.Windows.Forms.Label lblKhachHang;
        public System.Windows.Forms.ComboBox cbKhachHang;
        public System.Windows.Forms.Label lblNgayLap;
        public System.Windows.Forms.DateTimePicker dtpNgayLap;
        public System.Windows.Forms.Label lblGhiChu;
        public System.Windows.Forms.TextBox txtGhiChu;

        public System.Windows.Forms.GroupBox gbChiTiet;
        public System.Windows.Forms.Label lblSanPham;
        public System.Windows.Forms.ComboBox cbSanPham;
        public System.Windows.Forms.Label lblSoLuong;
        public System.Windows.Forms.NumericUpDown numSoLuong;
        public System.Windows.Forms.Button btnThemSP;
        public System.Windows.Forms.DataGridView dgvChiTiet;

        public System.Windows.Forms.Panel pnlBottom;
        public System.Windows.Forms.Label lblTongTienText;
        public System.Windows.Forms.Label lblTongTienValue;
        public System.Windows.Forms.Label lblDaThanhToan;
        public System.Windows.Forms.NumericUpDown numDaThanhToan;
        public System.Windows.Forms.Button btnLuu;
        public System.Windows.Forms.Button btnHuy;

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
            this.gbThongTin = new System.Windows.Forms.GroupBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.cbKhachHang = new System.Windows.Forms.ComboBox();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.gbChiTiet = new System.Windows.Forms.GroupBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.cbSanPham = new System.Windows.Forms.ComboBox();
            this.lblSanPham = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.numDaThanhToan = new System.Windows.Forms.NumericUpDown();
            this.lblDaThanhToan = new System.Windows.Forms.Label();
            this.lblTongTienValue = new System.Windows.Forms.Label();
            this.lblTongTienText = new System.Windows.Forms.Label();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();

            this.gbThongTin.SuspendLayout();
            this.gbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDaThanhToan)).BeginInit();
            this.SuspendLayout();

            // --- GroupBox Thông Tin Chung ---
            this.gbThongTin.Controls.Add(this.txtGhiChu);
            this.gbThongTin.Controls.Add(this.lblGhiChu);
            this.gbThongTin.Controls.Add(this.dtpNgayLap);
            this.gbThongTin.Controls.Add(this.lblNgayLap);
            this.gbThongTin.Controls.Add(this.cbKhachHang);
            this.gbThongTin.Controls.Add(this.lblKhachHang);
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbThongTin.Location = new System.Drawing.Point(0, 0);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(850, 100);
            this.gbThongTin.TabIndex = 0;
            this.gbThongTin.TabStop = false;
            this.gbThongTin.Text = "Thông tin hóa đơn";

            // lblKhachHang
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new System.Drawing.Point(20, 30);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(68, 13);
            this.lblKhachHang.Text = "Khách hàng:";

            // cbKhachHang
            this.cbKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhachHang.FormattingEnabled = true;
            this.cbKhachHang.Location = new System.Drawing.Point(95, 27);
            this.cbKhachHang.Name = "cbKhachHang";
            this.cbKhachHang.Size = new System.Drawing.Size(250, 21);

            // lblNgayLap
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(20, 65);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(52, 13);
            this.lblNgayLap.Text = "Ngày lập:";

            // dtpNgayLap
            this.dtpNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayLap.Location = new System.Drawing.Point(95, 62);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(120, 20);

            // lblGhiChu
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(380, 30);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(47, 13);
            this.lblGhiChu.Text = "Ghi chú:";

            // txtGhiChu
            this.txtGhiChu.Location = new System.Drawing.Point(435, 27);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(380, 55);

            // --- GroupBox Chi Tiết Mua Hàng ---
            this.gbChiTiet.Controls.Add(this.dgvChiTiet);
            this.gbChiTiet.Controls.Add(this.btnThemSP);
            this.gbChiTiet.Controls.Add(this.numSoLuong);
            this.gbChiTiet.Controls.Add(this.lblSoLuong);
            this.gbChiTiet.Controls.Add(this.cbSanPham);
            this.gbChiTiet.Controls.Add(this.lblSanPham);
            this.gbChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbChiTiet.Location = new System.Drawing.Point(0, 100);
            this.gbChiTiet.Name = "gbChiTiet";
            this.gbChiTiet.Padding = new System.Windows.Forms.Padding(10);
            this.gbChiTiet.Size = new System.Drawing.Size(850, 410);
            this.gbChiTiet.TabIndex = 1;
            this.gbChiTiet.TabStop = false;
            this.gbChiTiet.Text = "Chi tiết sản phẩm mua";

            // lblSanPham
            this.lblSanPham.AutoSize = true;
            this.lblSanPham.Location = new System.Drawing.Point(20, 30);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.Size = new System.Drawing.Size(58, 13);
            this.lblSanPham.Text = "Sản phẩm:";

            // cbSanPham
            this.cbSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSanPham.FormattingEnabled = true;
            this.cbSanPham.Location = new System.Drawing.Point(85, 27);
            this.cbSanPham.Name = "cbSanPham";
            this.cbSanPham.Size = new System.Drawing.Size(260, 21);

            // lblSoLuong
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(380, 30);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(52, 13);
            this.lblSoLuong.Text = "Số lượng:";

            // numSoLuong
            this.numSoLuong.Location = new System.Drawing.Point(440, 28);
            this.numSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(80, 20);
            this.numSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // btnThemSP
            this.btnThemSP.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnThemSP.ForeColor = System.Drawing.Color.White;
            this.btnThemSP.Location = new System.Drawing.Point(550, 24);
            this.btnThemSP.Name = "btnThemSP";
            this.btnThemSP.Size = new System.Drawing.Size(90, 26);
            this.btnThemSP.Text = "Thêm SP";
            this.btnThemSP.UseVisualStyleBackColor = false;

            // dgvChiTiet
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Location = new System.Drawing.Point(13, 65);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(824, 332);
            this.dgvChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));

            // --- Panel Chức Năng (Dưới cùng) ---
            this.pnlBottom.Controls.Add(this.numDaThanhToan);
            this.pnlBottom.Controls.Add(this.lblDaThanhToan);
            this.pnlBottom.Controls.Add(this.lblTongTienValue);
            this.pnlBottom.Controls.Add(this.lblTongTienText);
            this.pnlBottom.Controls.Add(this.btnHuy);
            this.pnlBottom.Controls.Add(this.btnLuu);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 510);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(850, 70);
            this.pnlBottom.TabIndex = 2;
            this.pnlBottom.BackColor = System.Drawing.Color.WhiteSmoke;

            // lblTongTienText
            this.lblTongTienText.AutoSize = true;
            this.lblTongTienText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTienText.Location = new System.Drawing.Point(20, 10);
            this.lblTongTienText.Name = "lblTongTienText";
            this.lblTongTienText.Size = new System.Drawing.Size(82, 17);
            this.lblTongTienText.Text = "Tổng tiền:";

            // lblTongTienValue
            this.lblTongTienValue.AutoSize = true;
            this.lblTongTienValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTienValue.ForeColor = System.Drawing.Color.Crimson;
            this.lblTongTienValue.Location = new System.Drawing.Point(105, 8);
            this.lblTongTienValue.Name = "lblTongTienValue";
            this.lblTongTienValue.Size = new System.Drawing.Size(34, 20);
            this.lblTongTienValue.Text = "0 đ";

            // lblDaThanhToan
            this.lblDaThanhToan.AutoSize = true;
            this.lblDaThanhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular);
            this.lblDaThanhToan.Location = new System.Drawing.Point(20, 40);
            this.lblDaThanhToan.Name = "lblDaThanhToan";
            this.lblDaThanhToan.Size = new System.Drawing.Size(125, 15);
            this.lblDaThanhToan.Text = "Khách đã thanh toán:";

            // numDaThanhToan
            this.numDaThanhToan.Location = new System.Drawing.Point(150, 38);
            this.numDaThanhToan.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 }); 
            this.numDaThanhToan.Name = "numDaThanhToan";
            this.numDaThanhToan.Size = new System.Drawing.Size(150, 20);
            this.numDaThanhToan.ThousandsSeparator = true;

            // btnLuu
            this.btnLuu.BackColor = System.Drawing.Color.SeaGreen;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(600, 15);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 40);
            this.btnLuu.Text = "Xuất Hóa Đơn";
            this.btnLuu.UseVisualStyleBackColor = false;

            // btnHuy
            this.btnHuy.BackColor = System.Drawing.Color.IndianRed;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(730, 15);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 40);
            this.btnHuy.Text = "Hủy Bỏ";
            this.btnHuy.UseVisualStyleBackColor = false;

            // --- Cấu Hình Form XuatHD ---
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 580);
            this.Controls.Add(this.gbChiTiet);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.gbThongTin);
            this.Name = "XuatHD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xuất Hóa Đơn Mới";

            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.gbChiTiet.ResumeLayout(false);
            this.gbChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDaThanhToan)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}