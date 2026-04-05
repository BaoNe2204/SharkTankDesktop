namespace SharkTank.Modules.Sales.UI.Forms
{
    partial class TaoBaoGia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Khai báo các control (Public để code logic dễ gọi)
        public System.Windows.Forms.GroupBox gbThongTin;
        public System.Windows.Forms.Label lblKhachHang;
        public System.Windows.Forms.ComboBox cbKhachHang;
        public System.Windows.Forms.Label lblNgayHetHan;
        public System.Windows.Forms.DateTimePicker dtpNgayHetHan;
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
            this.lblNgayHetHan = new System.Windows.Forms.Label();
            this.dtpNgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.gbChiTiet = new System.Windows.Forms.GroupBox();
            this.lblSanPham = new System.Windows.Forms.Label();
            this.cbSanPham = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTongTienText = new System.Windows.Forms.Label();
            this.lblTongTienValue = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();

            this.gbThongTin.SuspendLayout();
            this.gbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // --- GroupBox Thông Tin Chung ---
            this.gbThongTin.Controls.Add(this.txtGhiChu);
            this.gbThongTin.Controls.Add(this.lblGhiChu);
            this.gbThongTin.Controls.Add(this.dtpNgayHetHan);
            this.gbThongTin.Controls.Add(this.lblNgayHetHan);
            this.gbThongTin.Controls.Add(this.cbKhachHang);
            this.gbThongTin.Controls.Add(this.lblKhachHang);
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbThongTin.Location = new System.Drawing.Point(0, 0);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(850, 100);
            this.gbThongTin.TabIndex = 0;
            this.gbThongTin.TabStop = false;
            this.gbThongTin.Text = "Thông tin chung";

            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new System.Drawing.Point(20, 30);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(68, 13);
            this.lblKhachHang.Text = "Khách hàng:";

            this.cbKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhachHang.FormattingEnabled = true;
            this.cbKhachHang.Location = new System.Drawing.Point(95, 27);
            this.cbKhachHang.Name = "cbKhachHang";
            this.cbKhachHang.Size = new System.Drawing.Size(250, 21);

            this.lblNgayHetHan.AutoSize = true;
            this.lblNgayHetHan.Location = new System.Drawing.Point(20, 65);
            this.lblNgayHetHan.Name = "lblNgayHetHan";
            this.lblNgayHetHan.Size = new System.Drawing.Size(74, 13);
            this.lblNgayHetHan.Text = "Ngày hết hạn:";

            this.dtpNgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHetHan.Location = new System.Drawing.Point(95, 62);
            this.dtpNgayHetHan.Name = "dtpNgayHetHan";
            this.dtpNgayHetHan.Size = new System.Drawing.Size(120, 20);

            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(380, 30);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(47, 13);
            this.lblGhiChu.Text = "Ghi chú:";

            this.txtGhiChu.Location = new System.Drawing.Point(435, 27);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(380, 55);

            // --- GroupBox Chi Tiết Hàng Hóa ---
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
            this.gbChiTiet.Size = new System.Drawing.Size(850, 420);
            this.gbChiTiet.TabIndex = 1;
            this.gbChiTiet.TabStop = false;
            this.gbChiTiet.Text = "Chi tiết sản phẩm";

            this.lblSanPham.AutoSize = true;
            this.lblSanPham.Location = new System.Drawing.Point(20, 30);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.Size = new System.Drawing.Size(58, 13);
            this.lblSanPham.Text = "Sản phẩm:";

            this.cbSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSanPham.FormattingEnabled = true;
            this.cbSanPham.Location = new System.Drawing.Point(85, 27);
            this.cbSanPham.Name = "cbSanPham";
            this.cbSanPham.Size = new System.Drawing.Size(260, 21);

            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(380, 30);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(52, 13);
            this.lblSoLuong.Text = "Số lượng:";

            this.numSoLuong.Location = new System.Drawing.Point(440, 28);
            this.numSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(80, 20);
            this.numSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });

            this.btnThemSP.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnThemSP.ForeColor = System.Drawing.Color.White;
            this.btnThemSP.Location = new System.Drawing.Point(550, 24);
            this.btnThemSP.Name = "btnThemSP";
            this.btnThemSP.Size = new System.Drawing.Size(90, 26);
            this.btnThemSP.Text = "Thêm SP";
            this.btnThemSP.UseVisualStyleBackColor = false;

            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Location = new System.Drawing.Point(13, 65);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(824, 342);
            this.dgvChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));

            // --- Panel Chức Năng (Dưới cùng) ---
            this.pnlBottom.Controls.Add(this.lblTongTienValue);
            this.pnlBottom.Controls.Add(this.lblTongTienText);
            this.pnlBottom.Controls.Add(this.btnHuy);
            this.pnlBottom.Controls.Add(this.btnLuu);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 520);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(850, 60);
            this.pnlBottom.TabIndex = 2;
            this.pnlBottom.BackColor = System.Drawing.Color.WhiteSmoke;

            this.lblTongTienText.AutoSize = true;
            this.lblTongTienText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienText.Location = new System.Drawing.Point(20, 20);
            this.lblTongTienText.Name = "lblTongTienText";
            this.lblTongTienText.Size = new System.Drawing.Size(82, 17);
            this.lblTongTienText.Text = "Tổng tiền:";

            this.lblTongTienValue.AutoSize = true;
            this.lblTongTienValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienValue.ForeColor = System.Drawing.Color.Crimson;
            this.lblTongTienValue.Location = new System.Drawing.Point(105, 18);
            this.lblTongTienValue.Name = "lblTongTienValue";
            this.lblTongTienValue.Size = new System.Drawing.Size(34, 20);
            this.lblTongTienValue.Text = "0 đ";

            this.btnLuu.BackColor = System.Drawing.Color.SeaGreen;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(620, 12);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 35);
            this.btnLuu.Text = "Lưu Báo Giá";
            this.btnLuu.UseVisualStyleBackColor = false;

            this.btnHuy.BackColor = System.Drawing.Color.IndianRed;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(730, 12);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 35);
            this.btnHuy.Text = "Hủy Bỏ";
            this.btnHuy.UseVisualStyleBackColor = false;

            // --- Cấu Hình Form TaoBaoGia ---
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 580);
            this.Controls.Add(this.gbChiTiet);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.gbThongTin);
            this.Name = "TaoBaoGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; // Mở form ở giữa màn hình
            this.Text = "Tạo Báo Giá Mới";

            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.gbChiTiet.ResumeLayout(false);
            this.gbChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}