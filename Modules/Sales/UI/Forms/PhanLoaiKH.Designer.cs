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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTopActions = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvPhanLoai = new System.Windows.Forms.DataGridView();


            this.pnlHeader.SuspendLayout();
            this.pnlTopActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanLoai)).BeginInit();
            this.SuspendLayout();

            // --- Nền UserControl ---
            this.BackColor = System.Drawing.Color.FromArgb(242, 245, 250);

            // --- pnlHeader ---
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 65;

            this.lblTitle.Text = "Phân Loại Khách Hàng";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 112, 192);
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.AutoSize = true;

            // --- flowCards ---
            this.flowCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCards.Height = 150;
            this.flowCards.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);

            // --- pnlTopActions ---
            this.pnlTopActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopActions.Height = 55;
            this.pnlTopActions.Controls.Add(this.txtSearch);

            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSearch.Location = new System.Drawing.Point(25, 12);
            this.txtSearch.Size = new System.Drawing.Size(450, 30);
            this.txtSearch.Text = "🔍 Tìm kiếm khách hàng...";
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;

            // --- dgvPhanLoai (Màn lột xác thực sự ở đây) ---
            this.dgvPhanLoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhanLoai.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhanLoai.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhanLoai.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPhanLoai.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvPhanLoai.AllowUserToAddRows = false;
            this.dgvPhanLoai.RowHeadersVisible = false; // Ẩn cái cột trống xấu xí bên trái
            this.dgvPhanLoai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhanLoai.MultiSelect = false;

            // Tự động giãn đều các cột
            this.dgvPhanLoai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Header bảng (Tông Navy Slate)
            this.dgvPhanLoai.EnableHeadersVisualStyles = false;
            this.dgvPhanLoai.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.dgvPhanLoai.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPhanLoai.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.dgvPhanLoai.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            this.dgvPhanLoai.ColumnHeadersHeight = 50;

            // Style cho các dòng dữ liệu
            this.dgvPhanLoai.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvPhanLoai.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(71, 89, 126);
            this.dgvPhanLoai.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(232, 241, 250);
            this.dgvPhanLoai.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(0, 112, 192);
            this.dgvPhanLoai.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            this.dgvPhanLoai.RowTemplate.Height = 45;
            this.dgvPhanLoai.GridColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // Màu xen kẽ giữa các dòng 
            this.dgvPhanLoai.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 251, 252);

            // Thêm các thành phần vào Form theo thứ tự
            this.Controls.Add(this.dgvPhanLoai);
            this.Controls.Add(this.pnlTopActions);
            this.Controls.Add(this.flowCards);
            this.Controls.Add(this.pnlHeader);

            this.Size = new System.Drawing.Size(1200, 800);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTopActions.ResumeLayout(false);
            this.pnlTopActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanLoai)).EndInit();
            this.ResumeLayout(false);

            this.flowCards.WrapContents = false; 
            this.flowCards.AutoScroll = true;    
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