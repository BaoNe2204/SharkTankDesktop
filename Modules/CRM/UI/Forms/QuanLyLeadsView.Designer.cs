using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.CRM.UI.Forms
{
    partial class QuanLyLeadsForm
    {
        private Panel panelTitle;
        private Label lblTitle;
        private DataGridView dgvLeads;

        private TextBox txtSearch;
        private Button btnSearch;

        private Button btnAdd;
        private Button btnConvert;
        private Button btnCall;
        private Button btnEmail;
        private Button btnEdit;
        private Button btnDelete;
        // PANEL EDIT LEAD
        private Panel panelEditLead;
        private Label lblEditTen;
        private Label lblEditPhone;
        private Label lblEditEmail;
        private Label lblEditTrangThai;

        private TextBox txtEditTen;
        private TextBox txtEditPhone;
        private TextBox txtEditEmail;
        private ComboBox cbEditTrangThai;

        private Button btnUpdate;
        private Button btnEditCancel;

        // PANEL ADD LEAD
        private Panel panelAddLead;
        private Label lblTen;
        private Label lblPhone;
        private Label lblEmail;
        private Label lblNguon;
        private Label lblTrangThai;

        private TextBox txtTen;
        private TextBox txtPhone;
        private TextBox txtEmail;

        private ComboBox cbNguon;
        private ComboBox cbTrangThai;

        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvLeads = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnCall = new System.Windows.Forms.Button();
            this.btnEmail = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelAddLead = new System.Windows.Forms.Panel();
            this.panelEditLead = new System.Windows.Forms.Panel();
            this.lblEditTen = new System.Windows.Forms.Label();
            this.lblEditPhone = new System.Windows.Forms.Label();
            this.lblEditEmail = new System.Windows.Forms.Label();
            this.lblEditTrangThai = new System.Windows.Forms.Label();

            this.txtEditTen = new System.Windows.Forms.TextBox();
            this.txtEditPhone = new System.Windows.Forms.TextBox();
            this.txtEditEmail = new System.Windows.Forms.TextBox();
            this.cbEditTrangThai = new System.Windows.Forms.ComboBox();

            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnEditCancel = new System.Windows.Forms.Button();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblNguon = new System.Windows.Forms.Label();
            this.cbNguon = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cbTrangThai = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeads)).BeginInit();
            this.panelAddLead.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.panelTitle.Controls.Add(this.lblTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(1100, 60);
            this.panelTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(24, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(415, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🎯 Quản lý khách hàng tiềm năng";
            // 
            // dgvLeads
            // 
            this.dgvLeads.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLeads.BackgroundColor = System.Drawing.Color.White;
            this.dgvLeads.Location = new System.Drawing.Point(20, 100);
            this.dgvLeads.Name = "dgvLeads";
            this.dgvLeads.ReadOnly = true;
            this.dgvLeads.RowHeadersVisible = false;
            this.dgvLeads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeads.Size = new System.Drawing.Size(1060, 300);
            this.dgvLeads.TabIndex = 3;
            this.dgvLeads.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLeads_CellClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(20, 70);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 22);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(280, 68);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(96, 26);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "🔍 Tìm Kiếm";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Green;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(100, 430);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(140, 40);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "➕ Thêm Lead";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(260, 430);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(140, 40);
            this.btnConvert.TabIndex = 5;
            this.btnConvert.Text = "🔄 Chuyển đổi";
            // 
            // btnCall
            // 
            this.btnCall.Location = new System.Drawing.Point(420, 430);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(140, 40);
            this.btnCall.TabIndex = 6;
            this.btnCall.Text = "📞 Gọi điện";
            // 
            // btnEmail
            // 
            this.btnEmail.Location = new System.Drawing.Point(580, 430);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(140, 40);
            this.btnEmail.TabIndex = 7;
            this.btnEmail.Text = "✉ Email";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(740, 430);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(140, 40);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "✏ Sửa";

            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Red;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(900, 430);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 40);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "🗑 Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            //
            //bntClick
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panelAddLead
            // 
            this.panelAddLead.BackColor = System.Drawing.Color.White;
            this.panelAddLead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAddLead.Controls.Add(this.lblTen);
            this.panelAddLead.Controls.Add(this.txtTen);
            this.panelAddLead.Controls.Add(this.lblPhone);
            this.panelAddLead.Controls.Add(this.txtPhone);
            this.panelAddLead.Controls.Add(this.lblEmail);
            this.panelAddLead.Controls.Add(this.txtEmail);
            this.panelAddLead.Controls.Add(this.lblNguon);
            this.panelAddLead.Controls.Add(this.cbNguon);
            this.panelAddLead.Controls.Add(this.lblTrangThai);
            this.panelAddLead.Controls.Add(this.cbTrangThai);
            this.panelAddLead.Controls.Add(this.btnSave);
            this.panelAddLead.Controls.Add(this.btnCancel);
            this.panelAddLead.Location = new System.Drawing.Point(334, 193);
            this.panelAddLead.Name = "panelAddLead";
            this.panelAddLead.Size = new System.Drawing.Size(400, 300);
            this.panelAddLead.TabIndex = 10;
            this.panelAddLead.Visible = false;
            // panelEditLead
            this.panelEditLead.BackColor = System.Drawing.Color.White;
            this.panelEditLead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEditLead.Location = new System.Drawing.Point(334, 193);
            this.panelEditLead.Size = new System.Drawing.Size(400, 260);
            this.panelEditLead.Visible = false;


            // lblEditTen
            this.lblEditTen.Text = "Tên:";
            this.lblEditTen.Location = new System.Drawing.Point(20, 30);

            // txtEditTen
            this.txtEditTen.Location = new System.Drawing.Point(120, 30);
            this.txtEditTen.Size = new System.Drawing.Size(200, 22);


            // lblEditPhone
            this.lblEditPhone.Text = "Phone:";
            this.lblEditPhone.Location = new System.Drawing.Point(20, 70);

            // txtEditPhone
            this.txtEditPhone.Location = new System.Drawing.Point(120, 70);
            this.txtEditPhone.Size = new System.Drawing.Size(200, 22);


            // lblEditEmail
            this.lblEditEmail.Text = "Email:";
            this.lblEditEmail.Location = new System.Drawing.Point(20, 110);

            // txtEditEmail
            this.txtEditEmail.Location = new System.Drawing.Point(120, 110);
            this.txtEditEmail.Size = new System.Drawing.Size(200, 22);


            // lblEditTrangThai
            this.lblEditTrangThai.Text = "Trạng thái:";
            this.lblEditTrangThai.Location = new System.Drawing.Point(20, 150);

            // cbEditTrangThai
            this.cbEditTrangThai.Items.AddRange(new object[] {
"Mới",
"Đang tư vấn",
"Quan tâm"});
            this.cbEditTrangThai.Location = new System.Drawing.Point(120, 150);
            this.cbEditTrangThai.Size = new System.Drawing.Size(200, 24);


            // btnUpdate
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.BackColor = System.Drawing.Color.Green;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(150, 200);
            this.btnUpdate.Size = new System.Drawing.Size(80, 30);


            // btnEditCancel
            this.btnEditCancel.Text = "Hủy";
            this.btnEditCancel.Location = new System.Drawing.Point(240, 200);
            this.btnEditCancel.Size = new System.Drawing.Size(80, 30);


            // add controls
            this.panelEditLead.Controls.Add(this.lblEditTen);
            this.panelEditLead.Controls.Add(this.txtEditTen);
            this.panelEditLead.Controls.Add(this.lblEditPhone);
            this.panelEditLead.Controls.Add(this.txtEditPhone);
            this.panelEditLead.Controls.Add(this.lblEditEmail);
            this.panelEditLead.Controls.Add(this.txtEditEmail);
            this.panelEditLead.Controls.Add(this.lblEditTrangThai);
            this.panelEditLead.Controls.Add(this.cbEditTrangThai);
            this.panelEditLead.Controls.Add(this.btnUpdate);
            this.panelEditLead.Controls.Add(this.btnEditCancel);
            // 
            // lblTen
            // 
            this.lblTen.Location = new System.Drawing.Point(20, 30);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(100, 23);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Tên:";
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(120, 30);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(200, 22);
            this.txtTen.TabIndex = 1;
            // 
            // lblPhone
            // 
            this.lblPhone.Location = new System.Drawing.Point(20, 70);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(100, 23);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "Phone:";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(120, 70);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 22);
            this.txtPhone.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(20, 110);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(100, 23);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(120, 110);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 22);
            this.txtEmail.TabIndex = 5;
            // 
            // lblNguon
            // 
            this.lblNguon.Location = new System.Drawing.Point(20, 150);
            this.lblNguon.Name = "lblNguon";
            this.lblNguon.Size = new System.Drawing.Size(100, 23);
            this.lblNguon.TabIndex = 6;
            this.lblNguon.Text = "Nguồn:";
            // 
            // cbNguon
            // 
            this.cbNguon.Items.AddRange(new object[] {
            "Facebook",
            "Website",
            "Google Ads",
            "Zalo"});
            this.cbNguon.Location = new System.Drawing.Point(120, 150);
            this.cbNguon.Name = "cbNguon";
            this.cbNguon.Size = new System.Drawing.Size(200, 24);
            this.cbNguon.TabIndex = 7;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Location = new System.Drawing.Point(20, 190);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(100, 23);
            this.lblTrangThai.TabIndex = 8;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // cbTrangThai
            // 
            this.cbTrangThai.Items.AddRange(new object[] {
            "Mới",
            "Đang tư vấn",
            "Quan tâm",
            "Không chốt"
            });
            this.cbTrangThai.Location = new System.Drawing.Point(120, 190);
            this.cbTrangThai.Name = "cbTrangThai";
            this.cbTrangThai.Size = new System.Drawing.Size(200, 24);
            this.cbTrangThai.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(150, 227);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(245, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // QuanLyLeadsForm
            // 
            this.Controls.Add(this.panelTitle);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvLeads);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.btnEmail);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.panelAddLead);
            this.Name = "QuanLyLeadsForm";
            this.Size = new System.Drawing.Size(1100, 600);
            this.panelTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeads)).EndInit();
            this.panelAddLead.ResumeLayout(false);
            this.panelAddLead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       
    }
}