using System.Windows.Forms;

namespace SharkTank.Modules.Sales.UI.Forms
{
    partial class QLCongNoKH
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();

            // Khởi tạo các thành phần
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnLapPhieuThu = new System.Windows.Forms.Button();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvCongNo = new System.Windows.Forms.DataGridView();

            this.pnlHeader.SuspendLayout();
            this.pnlActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongNo)).BeginInit();
            this.SuspendLayout();

            // --- 1. pnlHeader ---
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 100;
            this.pnlHeader.Name = "pnlHeader";

            // lblTitle:
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 45);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Quản Lý Công Nợ Khách Hàng";

            // --- 2. flowCards (Thẻ thống kê) ---
            this.flowCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCards.Height = 130;
            this.flowCards.Name = "flowCards";
            this.flowCards.Padding = new System.Windows.Forms.Padding(20, 15, 0, 0);

            // --- 3. pnlActions (Thanh công cụ) ---
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Height = 60;
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Controls.Add(this.txtSearch);
            this.pnlActions.Controls.Add(this.btnLapPhieuThu);
            this.pnlActions.Controls.Add(this.btnXacNhan);
            this.pnlActions.Controls.Add(this.btnRefresh);

            this.txtSearch.Location = new System.Drawing.Point(25, 15);
            this.txtSearch.Size = new System.Drawing.Size(400, 27);

            this.btnLapPhieuThu.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnLapPhieuThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapPhieuThu.ForeColor = System.Drawing.Color.White;
            this.btnLapPhieuThu.Location = new System.Drawing.Point(440, 14);
            this.btnLapPhieuThu.Size = new System.Drawing.Size(150, 32);
            this.btnLapPhieuThu.Text = "+ Lập Phiếu Thu";

            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(0, 112, 192);
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(600, 14);
            this.btnXacNhan.Size = new System.Drawing.Size(180, 32);
            this.btnXacNhan.Text = "✔️ Xác Nhận Thanh Toán";

            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Size = new System.Drawing.Size(120, 32);
            this.btnRefresh.Location = new System.Drawing.Point(790, 14);

            // --- 4. dgvCongNo (Bảng dữ liệu - Chiếm phần còn lại) ---
            this.dgvCongNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCongNo.BackgroundColor = System.Drawing.Color.White;
            this.dgvCongNo.RowHeadersVisible = false;
            this.dgvCongNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCongNo.EnableHeadersVisualStyles = false;

            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvCongNo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCongNo.ColumnHeadersHeight = 45;

            // --- THỨ TỰ ADD QUYẾT ĐỊNH HIỂN THỊ (QUAN TRỌNG) ---
            this.Controls.Add(this.dgvCongNo);
            this.Controls.Add(this.pnlActions); 
            this.Controls.Add(this.flowCards);
            this.Controls.Add(this.pnlHeader);

            this.Size = new System.Drawing.Size(1200, 800);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongNo)).EndInit();
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flowCards;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnLapPhieuThu;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.DataGridView dgvCongNo;
    }
}