using System.Windows.Forms;

namespace SharkTank.Modules.Sales.UI.Forms
{
    partial class PhanLoaiKH
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTopActions = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnUpdateRule = new System.Windows.Forms.Button();
            this.dgvPhanLoai = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlTopActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanLoai)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 65);
            this.pnlHeader.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(303, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Phân Loại Khách Hàng";
            // 
            // flowCards
            // 
            this.flowCards.AutoScroll = true;
            this.flowCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCards.Location = new System.Drawing.Point(0, 65);
            this.flowCards.Name = "flowCards";
            this.flowCards.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);
            this.flowCards.Size = new System.Drawing.Size(1200, 150);
            this.flowCards.TabIndex = 2;
            this.flowCards.WrapContents = false;
            // 
            // pnlTopActions
            // 
            this.pnlTopActions.Controls.Add(this.txtSearch);
            this.pnlTopActions.Controls.Add(this.btnRefresh);
            this.pnlTopActions.Controls.Add(this.btnExport);
            this.pnlTopActions.Controls.Add(this.btnUpdateRule);
            this.pnlTopActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopActions.Location = new System.Drawing.Point(0, 215);
            this.pnlTopActions.Name = "pnlTopActions";
            this.pnlTopActions.Size = new System.Drawing.Size(1200, 55);
            this.pnlTopActions.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(25, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(450, 29);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "🔍 Tìm kiếm khách hàng...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(850, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(95, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(740, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "📥 Xuất Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // btnUpdateRule
            // 
            this.btnUpdateRule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnUpdateRule.FlatAppearance.BorderSize = 0;
            this.btnUpdateRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateRule.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUpdateRule.ForeColor = System.Drawing.Color.White;
            this.btnUpdateRule.Location = new System.Drawing.Point(620, 12);
            this.btnUpdateRule.Name = "btnUpdateRule";
            this.btnUpdateRule.Size = new System.Drawing.Size(110, 30);
            this.btnUpdateRule.TabIndex = 3;
            this.btnUpdateRule.Text = "Cập nhật quy tắc";
            this.btnUpdateRule.UseVisualStyleBackColor = false;
            this.btnUpdateRule.Click += new System.EventHandler(this.BtnUpdateRule_Click);
            // 
            // dgvPhanLoai
            // 
            this.dgvPhanLoai.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.dgvPhanLoai.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPhanLoai.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhanLoai.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhanLoai.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhanLoai.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPhanLoai.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhanLoai.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPhanLoai.ColumnHeadersHeight = 50;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(89)))), ((int)(((byte)(126)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(241)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhanLoai.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPhanLoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhanLoai.EnableHeadersVisualStyles = false;
            this.dgvPhanLoai.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvPhanLoai.Location = new System.Drawing.Point(0, 270);
            this.dgvPhanLoai.MultiSelect = false;
            this.dgvPhanLoai.Name = "dgvPhanLoai";
            this.dgvPhanLoai.RowHeadersVisible = false;
            this.dgvPhanLoai.RowTemplate.Height = 45;
            this.dgvPhanLoai.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhanLoai.Size = new System.Drawing.Size(1200, 530);
            this.dgvPhanLoai.TabIndex = 0;
            // 
            // PhanLoaiKH
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.dgvPhanLoai);
            this.Controls.Add(this.pnlTopActions);
            this.Controls.Add(this.flowCards);
            this.Controls.Add(this.pnlHeader);
            this.Name = "PhanLoaiKH";
            this.Size = new System.Drawing.Size(1200, 800);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTopActions.ResumeLayout(false);
            this.pnlTopActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanLoai)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flowCards;
        private System.Windows.Forms.Panel pnlTopActions;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnUpdateRule;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvPhanLoai;
    }
}