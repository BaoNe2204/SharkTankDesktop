namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class ChucVuEditForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtTenChucVu;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;

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
            this.lblTen = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtTenChucVu = new System.Windows.Forms.TextBox();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTen
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(40, 50);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(76, 15);
            this.lblTen.Text = "Tên chức vụ";

            // txtTenChucVu
            this.txtTenChucVu.Location = new System.Drawing.Point(150, 45);
            this.txtTenChucVu.Name = "txtTenChucVu";
            this.txtTenChucVu.Size = new System.Drawing.Size(250, 23);

            // lblMoTa
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(40, 110);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(39, 15);
            this.lblMoTa.Text = "Mô tả";

            // txtMoTa
            this.txtMoTa.Location = new System.Drawing.Point(150, 105);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Size = new System.Drawing.Size(250, 80);
            this.txtMoTa.Name = "txtMoTa";

            // btnLuu
            this.btnLuu.Location = new System.Drawing.Point(150, 220);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(90, 35);
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;

            // btnHuy
            this.btnHuy.Location = new System.Drawing.Point(310, 220);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 35);
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += (s, e) => this.Close();

            // Form
            this.ClientSize = new System.Drawing.Size(460, 300);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.txtTenChucVu);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Name = "ChucVuEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa Chức Vụ";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}