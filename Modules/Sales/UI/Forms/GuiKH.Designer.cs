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
            // 
            // lblEmailTo
            // 
            this.lblEmailTo.AutoSize = true;
            this.lblEmailTo.Location = new System.Drawing.Point(20, 25);
            this.lblEmailTo.Name = "lblEmailTo";
            this.lblEmailTo.Size = new System.Drawing.Size(95, 13);
            this.lblEmailTo.TabIndex = 9;
            this.lblEmailTo.Text = "Email khách hàng:";
            // 
            // txtEmailTo
            // 
            this.txtEmailTo.Location = new System.Drawing.Point(120, 22);
            this.txtEmailTo.Name = "txtEmailTo";
            this.txtEmailTo.Size = new System.Drawing.Size(440, 20);
            this.txtEmailTo.TabIndex = 8;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 65);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(47, 13);
            this.lblTieuDe.TabIndex = 7;
            this.lblTieuDe.Text = "Tiêu đề:";
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Location = new System.Drawing.Point(120, 62);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(440, 20);
            this.txtTieuDe.TabIndex = 6;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(20, 105);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(75, 13);
            this.lblFile.TabIndex = 5;
            this.lblFile.Text = "File đính kèm:";
            // 
            // txtFileDinhKem
            // 
            this.txtFileDinhKem.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtFileDinhKem.Location = new System.Drawing.Point(120, 102);
            this.txtFileDinhKem.Name = "txtFileDinhKem";
            this.txtFileDinhKem.ReadOnly = true;
            this.txtFileDinhKem.Size = new System.Drawing.Size(440, 20);
            this.txtFileDinhKem.TabIndex = 4;
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Location = new System.Drawing.Point(20, 145);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(93, 13);
            this.lblNoiDung.TabIndex = 3;
            this.lblNoiDung.Text = "Nội dung lời nhắn:";
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Location = new System.Drawing.Point(20, 165);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNoiDung.Size = new System.Drawing.Size(540, 130);
            this.txtNoiDung.TabIndex = 2;
            // 
            // btnGui
            // 
            this.btnGui.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGui.ForeColor = System.Drawing.Color.White;
            this.btnGui.Location = new System.Drawing.Point(440, 310);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(120, 35);
            this.btnGui.TabIndex = 1;
            this.btnGui.Text = "📤 Gửi Email";
            this.btnGui.UseVisualStyleBackColor = false;
            this.btnGui.Click += new System.EventHandler(this.BtnGui_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.IndianRed;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(340, 310);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 35);
            this.btnHuy.TabIndex = 0;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // GuiKH
            // 
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
            this.Name = "GuiKH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gửi Tài Liệu Cho Khách Hàng";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}