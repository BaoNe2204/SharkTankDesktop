using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class PhanQuyenChiTietForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.GroupBox grpPermissions;
        private System.Windows.Forms.CheckedListBox clbPermissions;
        private System.Windows.Forms.Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.cboUser = new System.Windows.Forms.ComboBox();
            this.grpPermissions = new System.Windows.Forms.GroupBox();
            this.clbPermissions = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpPermissions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(213, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Phân quyền chi tiết";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUser.Location = new System.Drawing.Point(22, 65);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(74, 19);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "Chọn User";
            // 
            // cboUser
            // 
            this.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboUser.Location = new System.Drawing.Point(26, 90);
            this.cboUser.Name = "cboUser";
            this.cboUser.Size = new System.Drawing.Size(300, 25);
            this.cboUser.TabIndex = 2;
            this.cboUser.DropDownStyle = ComboBoxStyle.DropDown;
            // 
            // grpPermissions
            // 
            this.grpPermissions.Controls.Add(this.clbPermissions);
            this.grpPermissions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpPermissions.Location = new System.Drawing.Point(26, 121);
            this.grpPermissions.Name = "grpPermissions";
            this.grpPermissions.Size = new System.Drawing.Size(1020, 411);
            this.grpPermissions.TabIndex = 3;
            this.grpPermissions.TabStop = false;
            this.grpPermissions.Text = "Danh sách quyền";
            // 
            // clbPermissions
            // 
            this.clbPermissions.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.clbPermissions.Location = new System.Drawing.Point(15, 30);
            this.clbPermissions.Name = "clbPermissions";
            this.clbPermissions.Size = new System.Drawing.Size(985, 364);
            this.clbPermissions.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(30, 538);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 40);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "💾 Lưu phân quyền";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PhanQuyenChiTietForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.cboUser);
            this.Controls.Add(this.grpPermissions);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "PhanQuyenChiTietForm";
            this.Size = new System.Drawing.Size(1087, 602);
            this.Load += new System.EventHandler(this.PhanQuyenChiTietForm_Load);
            this.grpPermissions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}