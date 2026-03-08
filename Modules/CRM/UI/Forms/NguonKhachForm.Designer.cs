using System.Windows.Forms;

namespace CRMSharkTank.Modules.CRM.UI.Forms
{
    partial class NguonKhachForm : UserControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnThemNguon;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvNguonKhach;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnThemNguon = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvNguonKhach = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sua = new System.Windows.Forms.DataGridViewButtonColumn();
            this.xoa = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguonKhach)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(263, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "NGUỒN KHÁCH";
            // 
            // btnThemNguon
            // 
            this.btnThemNguon.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnThemNguon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemNguon.ForeColor = System.Drawing.Color.White;
            this.btnThemNguon.Location = new System.Drawing.Point(94, 105);
            this.btnThemNguon.Name = "btnThemNguon";
            this.btnThemNguon.Size = new System.Drawing.Size(199, 48);
            this.btnThemNguon.TabIndex = 1;
            this.btnThemNguon.Text = "+ Thêm nguồn";
            this.btnThemNguon.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(650, 105);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(442, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Tìm kiếm...";
            // 
            // dgvNguonKhach
            // 
            this.dgvNguonKhach.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.sua,
            this.xoa});
            this.dgvNguonKhach.Location = new System.Drawing.Point(3, 170);
            this.dgvNguonKhach.Name = "dgvNguonKhach";
            this.dgvNguonKhach.Size = new System.Drawing.Size(1110, 424);
            this.dgvNguonKhach.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Tên nguồn";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Mô tả";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // sua
            // 


            this.sua.Text = "Sửa";
            this.sua.UseColumnTextForButtonValue = true;
            // 
            // xoa
            // 


            this.xoa.Text = "Xóa";
            this.xoa.UseColumnTextForButtonValue = true;
            // 
            // NguonKhachForm
            // 
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnThemNguon);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvNguonKhach);
            this.Name = "NguonKhachForm";
            this.Size = new System.Drawing.Size(1303, 597);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguonKhach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewButtonColumn sua;
        private DataGridViewButtonColumn xoa;
    }
}