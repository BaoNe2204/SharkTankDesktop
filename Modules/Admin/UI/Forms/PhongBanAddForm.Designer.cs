namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class PhongBanAddForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtTenPhongBan;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label lblMoTa;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtTenPhongBan = new System.Windows.Forms.TextBox();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.lblTen = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // label tên
            this.lblTen.Text = "Tên phòng ban";
            this.lblTen.Location = new System.Drawing.Point(30, 40);

            // textbox tên
            this.txtTenPhongBan.Location = new System.Drawing.Point(150, 35);
            this.txtTenPhongBan.Width = 200;

            // label mô tả
            this.lblMoTa.Text = "Mô tả";
            this.lblMoTa.Location = new System.Drawing.Point(30, 90);

            // textbox mô tả
            this.txtMoTa.Location = new System.Drawing.Point(150, 85);
            this.txtMoTa.Width = 200;
            this.txtMoTa.Height = 60;
            this.txtMoTa.Multiline = true;

            // button thêm
            this.btnThem.Text = "Thêm";
            this.btnThem.Location = new System.Drawing.Point(150, 170);

            // button hủy
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Location = new System.Drawing.Point(250, 170);
            this.btnHuy.Click += (s, e) => this.Close();

            // form
            this.ClientSize = new System.Drawing.Size(400, 230);
            this.Controls.Add(this.txtTenPhongBan);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.lblMoTa);

            this.Text = "Thêm Phòng Ban";

            this.ResumeLayout(false);
        }
    }
}