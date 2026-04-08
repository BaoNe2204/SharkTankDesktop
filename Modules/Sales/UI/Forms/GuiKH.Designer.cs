namespace SharkTank.Modules.Sales.UI.Forms
{
    partial class GuiKH
    {
        private System.ComponentModel.IContainer components = null;

        public System.Windows.Forms.Label lblEmailTo;
        public System.Windows.Forms.TextBox txtEmailTo;
        public System.Windows.Forms.Label lblTieuDe;
        public System.Windows.Forms.TextBox txtTieuDe;
        public System.Windows.Forms.Label lblFile;
        public System.Windows.Forms.TextBox txtFileDinhKem;
        public System.Windows.Forms.Label lblNoiDung;
        public System.Windows.Forms.TextBox txtNoiDung;
        public System.Windows.Forms.Button btnGui;
        public System.Windows.Forms.Button btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblEmailTo = new System.Windows.Forms.Label();
            this.txtEmailTo = new System.Windows.Forms.TextBox();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtTieuDe = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtFileDinhKem = new System.Windows.Forms.TextBox();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.btnGui = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblEmailTo
            this.lblEmailTo.AutoSize = true;
            this.lblEmailTo.Location = new System.Drawing.Point(20, 25);
            this.lblEmailTo.Text = "Email khách hàng:";

            // txtEmailTo
            this.txtEmailTo.Location = new System.Drawing.Point(120, 22);
            this.txtEmailTo.Size = new System.Drawing.Size(440, 20);

            // lblTieuDe
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 65);
            this.lblTieuDe.Text = "Tiêu đề:";

            // txtTieuDe
            this.txtTieuDe.Location = new System.Drawing.Point(120, 62);
            this.txtTieuDe.Size = new System.Drawing.Size(440, 20);

            // lblFile
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(20, 105);
            this.lblFile.Text = "File đính kèm:";

            // txtFileDinhKem
            this.txtFileDinhKem.Location = new System.Drawing.Point(120, 102);
            this.txtFileDinhKem.Size = new System.Drawing.Size(440, 20);
            this.txtFileDinhKem.ReadOnly = true;
            this.txtFileDinhKem.BackColor = System.Drawing.Color.WhiteSmoke;

            // lblNoiDung
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Location = new System.Drawing.Point(20, 145);
            this.lblNoiDung.Text = "Nội dung lời nhắn:";

            // txtNoiDung
            this.txtNoiDung.Location = new System.Drawing.Point(20, 165);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Size = new System.Drawing.Size(540, 130);
            this.txtNoiDung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // btnGui
            this.btnGui.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGui.ForeColor = System.Drawing.Color.White;
            this.btnGui.Location = new System.Drawing.Point(440, 310);
            this.btnGui.Size = new System.Drawing.Size(120, 35);
            this.btnGui.Text = "📤 Gửi Email";

            // btnHuy
            this.btnHuy.BackColor = System.Drawing.Color.IndianRed;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(340, 310);
            this.btnHuy.Size = new System.Drawing.Size(90, 35);
            this.btnHuy.Text = "Hủy";

            // GuiKH
            this.ClientSize = new System.Drawing.Size(580, 360);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.lblNoiDung);
            this.Controls.Add(this.txtFileDinhKem);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.txtTieuDe);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.txtEmailTo);
            this.Controls.Add(this.lblEmailTo);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gửi Tài Liệu Cho Khách Hàng";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}