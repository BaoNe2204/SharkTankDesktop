namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class ThongBaoHeThongEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.RichTextBox rtbContent;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblTargetType;
        private System.Windows.Forms.ComboBox cboTargetType;
        private System.Windows.Forms.Label lblTargetValue;
        private System.Windows.Forms.TextBox txtTargetValue;
        private System.Windows.Forms.Label lblStartAt;
        private System.Windows.Forms.DateTimePicker dtpStartAt;
        private System.Windows.Forms.Label lblEndAt;
        private System.Windows.Forms.DateTimePicker dtpEndAt;
        private System.Windows.Forms.CheckBox chkNoEndDate;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

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
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblContent = new System.Windows.Forms.Label();
            this.rtbContent = new System.Windows.Forms.RichTextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblTargetType = new System.Windows.Forms.Label();
            this.cboTargetType = new System.Windows.Forms.ComboBox();
            this.lblTargetValue = new System.Windows.Forms.Label();
            this.txtTargetValue = new System.Windows.Forms.TextBox();
            this.lblStartAt = new System.Windows.Forms.Label();
            this.dtpStartAt = new System.Windows.Forms.DateTimePicker();
            this.lblEndAt = new System.Windows.Forms.Label();
            this.dtpEndAt = new System.Windows.Forms.DateTimePicker();
            this.chkNoEndDate = new System.Windows.Forms.CheckBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(25, 20);
            this.lblHeader.Text = "Thong bao he thong";

            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(30, 70);
            this.lblTitle.Text = "Tieu de";

            this.txtTitle.Location = new System.Drawing.Point(150, 67);
            this.txtTitle.Size = new System.Drawing.Size(420, 22);

            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(30, 110);
            this.lblContent.Text = "Noi dung";

            this.rtbContent.Location = new System.Drawing.Point(150, 110);
            this.rtbContent.Size = new System.Drawing.Size(420, 120);

            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(30, 250);
            this.lblType.Text = "Loai";

            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Location = new System.Drawing.Point(150, 247);
            this.cboType.Size = new System.Drawing.Size(180, 24);

            this.lblTargetType.AutoSize = true;
            this.lblTargetType.Location = new System.Drawing.Point(30, 290);
            this.lblTargetType.Text = "Doi tuong";

            this.cboTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTargetType.Location = new System.Drawing.Point(150, 287);
            this.cboTargetType.Size = new System.Drawing.Size(180, 24);

            this.lblTargetValue.AutoSize = true;
            this.lblTargetValue.Location = new System.Drawing.Point(30, 330);
            this.lblTargetValue.Text = "Gia tri doi tuong";

            this.txtTargetValue.Location = new System.Drawing.Point(150, 327);
            this.txtTargetValue.Size = new System.Drawing.Size(420, 22);

            this.lblStartAt.AutoSize = true;
            this.lblStartAt.Location = new System.Drawing.Point(30, 370);
            this.lblStartAt.Text = "Bat dau";

            this.dtpStartAt.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpStartAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartAt.Location = new System.Drawing.Point(150, 367);
            this.dtpStartAt.Size = new System.Drawing.Size(180, 22);

            this.lblEndAt.AutoSize = true;
            this.lblEndAt.Location = new System.Drawing.Point(30, 410);
            this.lblEndAt.Text = "Ket thuc";

            this.dtpEndAt.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpEndAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndAt.Location = new System.Drawing.Point(150, 407);
            this.dtpEndAt.Size = new System.Drawing.Size(180, 22);

            this.chkNoEndDate.AutoSize = true;
            this.chkNoEndDate.Location = new System.Drawing.Point(350, 409);
            this.chkNoEndDate.Text = "Khong co ngay ket thuc";
            this.chkNoEndDate.CheckedChanged += new System.EventHandler(this.chkNoEndDate_CheckedChanged);

            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(150, 445);
            this.chkIsActive.Text = "Dang hoat dong";
            this.chkIsActive.Checked = true;

            this.btnSave.Location = new System.Drawing.Point(310, 490);
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.Text = "Luu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(450, 490);
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.Text = "Dong";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.ClientSize = new System.Drawing.Size(620, 550);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.rtbContent);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.lblTargetType);
            this.Controls.Add(this.cboTargetType);
            this.Controls.Add(this.lblTargetValue);
            this.Controls.Add(this.txtTargetValue);
            this.Controls.Add(this.lblStartAt);
            this.Controls.Add(this.dtpStartAt);
            this.Controls.Add(this.lblEndAt);
            this.Controls.Add(this.dtpEndAt);
            this.Controls.Add(this.chkNoEndDate);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "ThongBaoHeThongEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thong bao he thong";
            this.Load += new System.EventHandler(this.ThongBaoHeThongEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
