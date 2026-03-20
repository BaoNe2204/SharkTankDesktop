using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class QuanLyPhienDangNhapForm : UserControl
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private TextBox txtUser;
        private TextBox txtIP;
        private ComboBox cbStatus;

        private Button btnSearch;
        private Button btnRefresh;

        private DataGridView dgvSessions;

        private Button btnLogout;
        private Button btnLogoutAll;
        private Button btnLockUser;
        private Button btnDetail;

        private Label lblOnline;
        private Label lblOffline;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvSessions = new System.Windows.Forms.DataGridView();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnLogoutAll = new System.Windows.Forms.Button();
            this.btnLockUser = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.lblOnline = new System.Windows.Forms.Label();
            this.lblOffline = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSessions)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(319, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ PHIÊN ĐĂNG NHẬP";
            // 
            // txtUser
            // 
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUser.ForeColor = System.Drawing.Color.Gray;
            this.txtUser.Location = new System.Drawing.Point(20, 58);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(170, 25);
            this.txtUser.TabIndex = 3;
            this.txtUser.Text = "User...";
            this.txtUser.Enter += new System.EventHandler(this.txtUser_Enter);
            this.txtUser.Leave += new System.EventHandler(this.txtUser_Leave);
            // 
            // txtIP
            // 
            this.txtIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIP.ForeColor = System.Drawing.Color.Gray;
            this.txtIP.Location = new System.Drawing.Point(200, 58);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(170, 25);
            this.txtIP.TabIndex = 4;
            this.txtIP.Text = "IP...";
            this.txtIP.Enter += new System.EventHandler(this.txtIP_Enter);
            this.txtIP.Leave += new System.EventHandler(this.txtIP_Leave);
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbStatus.Items.AddRange(new object[] {
            "All",
            "Online",
            "Offline",
            "Expired"});
            this.cbStatus.Location = new System.Drawing.Point(380, 58);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(130, 25);
            this.cbStatus.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(530, 58);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 27);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Location = new System.Drawing.Point(602, 58);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72, 27);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // dgvSessions
            // 
            this.dgvSessions.AllowUserToAddRows = false;
            this.dgvSessions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSessions.BackgroundColor = System.Drawing.Color.White;
            this.dgvSessions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSessions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSessions.EnableHeadersVisualStyles = false;
            this.dgvSessions.Location = new System.Drawing.Point(0, 100);
            this.dgvSessions.Name = "dgvSessions";
            this.dgvSessions.RowHeadersVisible = false;
            this.dgvSessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSessions.Size = new System.Drawing.Size(1122, 431);
            this.dgvSessions.TabIndex = 9;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(32, 550);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(120, 30);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "Đăng xuất phiên";
            this.btnLogout.UseVisualStyleBackColor = false;
            // 
            // btnLogoutAll
            // 
            this.btnLogoutAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.btnLogoutAll.FlatAppearance.BorderSize = 0;
            this.btnLogoutAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogoutAll.ForeColor = System.Drawing.Color.White;
            this.btnLogoutAll.Location = new System.Drawing.Point(158, 550);
            this.btnLogoutAll.Name = "btnLogoutAll";
            this.btnLogoutAll.Size = new System.Drawing.Size(130, 30);
            this.btnLogoutAll.TabIndex = 11;
            this.btnLogoutAll.Text = "Đăng xuất tất cả";
            this.btnLogoutAll.UseVisualStyleBackColor = false;
            // 
            // btnLockUser
            // 
            this.btnLockUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnLockUser.FlatAppearance.BorderSize = 0;
            this.btnLockUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLockUser.ForeColor = System.Drawing.Color.White;
            this.btnLockUser.Location = new System.Drawing.Point(294, 550);
            this.btnLockUser.Name = "btnLockUser";
            this.btnLockUser.Size = new System.Drawing.Size(120, 30);
            this.btnLockUser.TabIndex = 12;
            this.btnLockUser.Text = "Khóa tài khoản";
            this.btnLockUser.UseVisualStyleBackColor = false;
            // 
            // btnDetail
            // 
            this.btnDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnDetail.FlatAppearance.BorderSize = 0;
            this.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetail.ForeColor = System.Drawing.Color.White;
            this.btnDetail.Location = new System.Drawing.Point(420, 550);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(90, 30);
            this.btnDetail.TabIndex = 13;
            this.btnDetail.Text = "Chi tiết";
            this.btnDetail.UseVisualStyleBackColor = false;
            // 
            // lblOnline
            // 
            this.lblOnline.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOnline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(137)))), ((int)(((byte)(62)))));
            this.lblOnline.Location = new System.Drawing.Point(760, 61);
            this.lblOnline.Name = "lblOnline";
            this.lblOnline.Size = new System.Drawing.Size(110, 23);
            this.lblOnline.TabIndex = 1;
            this.lblOnline.Text = "Online: 0";
            // 
            // lblOffline
            // 
            this.lblOffline.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOffline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.lblOffline.Location = new System.Drawing.Point(875, 61);
            this.lblOffline.Name = "lblOffline";
            this.lblOffline.Size = new System.Drawing.Size(110, 23);
            this.lblOffline.TabIndex = 2;
            this.lblOffline.Text = "Offline: 0";
            // 
            // QuanLyPhienDangNhapForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblOnline);
            this.Controls.Add(this.lblOffline);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvSessions);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnLogoutAll);
            this.Controls.Add(this.btnLockUser);
            this.Controls.Add(this.btnDetail);
            this.Name = "QuanLyPhienDangNhapForm";
            this.Size = new System.Drawing.Size(1199, 652);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSessions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}


