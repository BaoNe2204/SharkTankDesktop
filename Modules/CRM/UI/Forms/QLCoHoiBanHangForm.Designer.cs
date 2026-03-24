using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.CRM.UI.Forms
{
    partial class QLCoHoiBanHangForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel header;
        private Label lblTitle;

        private TextBox txtSearch;
        private Button btnSearch;

        private Panel formPanel;

        private Label lblTen;
        private Label lblKH;
        private Label lblGiaTri;
        private Label lblXacSuat;
        private Label lblTrangThai;

        private TextBox txtTenCoHoi;
        private ComboBox cboLead;
        private TextBox txtGiaTri;
        private TextBox txtXacSuat;
        private ComboBox cboTrangThai;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;

        private DataGridView dgv;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.header = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.formPanel = new System.Windows.Forms.Panel();
            this.lblTen = new System.Windows.Forms.Label();
            this.lblKH = new System.Windows.Forms.Label();
            this.lblGiaTri = new System.Windows.Forms.Label();
            this.lblXacSuat = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtTenCoHoi = new System.Windows.Forms.TextBox();
            this.cboLead = new System.Windows.Forms.ComboBox();
            this.txtGiaTri = new System.Windows.Forms.TextBox();
            this.txtXacSuat = new System.Windows.Forms.TextBox();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.header.SuspendLayout();
            this.formPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.header.Controls.Add(this.lblTitle);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(1476, 60);
            this.header.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(340, 36);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ CƠ HỘI BÁN HÀNG";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(20, 80);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 25);
            this.txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(280, 80);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "🔍 Tìm";
            // 
            // formPanel
            // 
            this.formPanel.BackColor = System.Drawing.Color.White;
            this.formPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.formPanel.Controls.Add(this.lblTen);
            this.formPanel.Controls.Add(this.lblKH);
            this.formPanel.Controls.Add(this.lblGiaTri);
            this.formPanel.Controls.Add(this.lblXacSuat);
            this.formPanel.Controls.Add(this.lblTrangThai);
            this.formPanel.Controls.Add(this.txtTenCoHoi);
            this.formPanel.Controls.Add(this.cboLead);
            this.formPanel.Controls.Add(this.txtGiaTri);
            this.formPanel.Controls.Add(this.txtXacSuat);
            this.formPanel.Controls.Add(this.cboTrangThai);
            this.formPanel.Controls.Add(this.btnThem);
            this.formPanel.Controls.Add(this.btnSua);
            this.formPanel.Controls.Add(this.btnXoa);
            this.formPanel.Location = new System.Drawing.Point(20, 120);
            this.formPanel.Name = "formPanel";
            this.formPanel.Size = new System.Drawing.Size(900, 160);
            this.formPanel.TabIndex = 3;
            // 
            // lblTen
            // 
            this.lblTen.Location = new System.Drawing.Point(20, 20);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(100, 23);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Tên cơ hội";
            // 
            // lblKH
            // 
            this.lblKH.Location = new System.Drawing.Point(20, 60);
            this.lblKH.Name = "lblKH";
            this.lblKH.Size = new System.Drawing.Size(100, 23);
            this.lblKH.TabIndex = 1;
            this.lblKH.Text = "Khách Hàng\r\n";
            // 
            // lblGiaTri
            // 
            this.lblGiaTri.Location = new System.Drawing.Point(20, 100);
            this.lblGiaTri.Name = "lblGiaTri";
            this.lblGiaTri.Size = new System.Drawing.Size(100, 23);
            this.lblGiaTri.TabIndex = 2;
            this.lblGiaTri.Text = "Giá trị";
            // 
            // lblXacSuat
            // 
            this.lblXacSuat.Location = new System.Drawing.Point(350, 20);
            this.lblXacSuat.Name = "lblXacSuat";
            this.lblXacSuat.Size = new System.Drawing.Size(100, 23);
            this.lblXacSuat.TabIndex = 3;
            this.lblXacSuat.Text = "Xác suất (%)";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Location = new System.Drawing.Point(350, 60);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(100, 23);
            this.lblTrangThai.TabIndex = 4;
            this.lblTrangThai.Text = "Trạng thái";
            // 
            // txtTenCoHoi
            // 
            this.txtTenCoHoi.Location = new System.Drawing.Point(120, 20);
            this.txtTenCoHoi.Name = "txtTenCoHoi";
            this.txtTenCoHoi.Size = new System.Drawing.Size(180, 20);
            this.txtTenCoHoi.TabIndex = 5;
            // 
            // cboLead
            // 
            this.cboLead.Location = new System.Drawing.Point(120, 60);
            this.cboLead.Name = "cboLead";
            this.cboLead.Size = new System.Drawing.Size(180, 21);
            this.cboLead.TabIndex = 6;
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Location = new System.Drawing.Point(120, 100);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.Size = new System.Drawing.Size(180, 20);
            this.txtGiaTri.TabIndex = 7;
            // 
            // txtXacSuat
            // 
            this.txtXacSuat.Location = new System.Drawing.Point(450, 20);
            this.txtXacSuat.Name = "txtXacSuat";
            this.txtXacSuat.Size = new System.Drawing.Size(180, 20);
            this.txtXacSuat.TabIndex = 8;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.Items.AddRange(new object[] {
            "Mới",
            "Đang tư vấn",
            "Đàm phán",
            "Thành công",
            "Thất bại"});
            this.cboTrangThai.Location = new System.Drawing.Point(450, 60);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(180, 21);
            this.cboTrangThai.TabIndex = 9;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(450, 100);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 10;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(540, 100);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 11;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(630, 100);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 12;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            // 
            // dgv
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.Location = new System.Drawing.Point(20, 300);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(1063, 350);
            this.dgv.TabIndex = 4;
            // 
            // QLCoHoiBanHangForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.header);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.formPanel);
            this.Controls.Add(this.dgv);
            this.Name = "QLCoHoiBanHangForm";
            this.Size = new System.Drawing.Size(1476, 683);
            this.header.ResumeLayout(false);
            this.formPanel.ResumeLayout(false);
            this.formPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}