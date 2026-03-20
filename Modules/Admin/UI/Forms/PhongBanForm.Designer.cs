using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class PhongBanForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvPhongBan;
        private Panel panelTop;
        private Panel panelButton;

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
            this.lblTitle = new Label();
            this.lblTimKiem = new Label();
            this.txtTimKiem = new TextBox();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.btnLamMoi = new Button();
            this.dgvPhongBan = new DataGridView();
            this.panelTop = new Panel();
            this.panelButton = new Panel();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongBan)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.SuspendLayout();

            // ===== TITLE =====
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.Text = "QUẢN LÝ PHÒNG BAN";

            // ===== LABEL SEARCH =====
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new Font("Segoe UI", 10F);
            this.lblTimKiem.Location = new Point(22, 91);
            this.lblTimKiem.Text = "Tìm Kiếm";

            // ===== TEXTBOX SEARCH =====
            this.txtTimKiem.Font = new Font("Segoe UI", 10F);
            this.txtTimKiem.Location = new Point(93, 85);
            this.txtTimKiem.Size = new Size(250, 25);

            // ===== BUTTON THÊM =====
            this.btnThem.BackColor = Color.FromArgb(46, 204, 113);
            this.btnThem.FlatStyle = FlatStyle.Flat;
            this.btnThem.ForeColor = Color.White;
            this.btnThem.Location = new Point(3, 10);
            this.btnThem.Size = new Size(120, 35);
            this.btnThem.Text = "Thêm";

            // ===== BUTTON SỬA =====
            this.btnSua.BackColor = Color.FromArgb(52, 152, 219);
            this.btnSua.FlatStyle = FlatStyle.Flat;
            this.btnSua.ForeColor = Color.White;
            this.btnSua.Location = new Point(148, 10);
            this.btnSua.Size = new Size(120, 35);
            this.btnSua.Text = "Sửa";

            // ===== BUTTON XÓA =====
            this.btnXoa.BackColor = Color.FromArgb(231, 76, 60);
            this.btnXoa.FlatStyle = FlatStyle.Flat;
            this.btnXoa.ForeColor = Color.White;
            this.btnXoa.Location = new Point(292, 10);
            this.btnXoa.Size = new Size(120, 35);
            this.btnXoa.Text = "Xóa";

            // ===== BUTTON LÀM MỚI =====
            this.btnLamMoi.BackColor = Color.Gray;
            this.btnLamMoi.FlatStyle = FlatStyle.Flat;
            this.btnLamMoi.ForeColor = Color.White;
            this.btnLamMoi.Location = new Point(440, 10);
            this.btnLamMoi.Size = new Size(120, 35);
            this.btnLamMoi.Text = "Làm mới";

            // ===== DATAGRID =====
            this.dgvPhongBan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhongBan.BackgroundColor = Color.White;
            this.dgvPhongBan.BorderStyle = BorderStyle.None;
            this.dgvPhongBan.Location = new Point(3, 140);
            this.dgvPhongBan.Size = new Size(1174, 469);
            this.dgvPhongBan.RowTemplate.Height = 28;

            // ===== PANEL TOP =====
            this.panelTop.BackColor = Color.White;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Size = new Size(1180, 60);

            // ===== PANEL BUTTON =====
            this.panelButton.Controls.Add(this.btnThem);
            this.panelButton.Controls.Add(this.btnSua);
            this.panelButton.Controls.Add(this.btnXoa);
            this.panelButton.Controls.Add(this.btnLamMoi);
            this.panelButton.Location = new Point(372, 70);
            this.panelButton.Size = new Size(808, 64);

            // ===== FORM =====
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.panelButton);
            this.Controls.Add(this.dgvPhongBan);
            this.Name = "PhongBanForm";
            this.Size = new Size(1180, 622);

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongBan)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}