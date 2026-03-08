using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRMSharkTank.Modules.CRM.UI.Forms
{
    public partial class NguonKhachForm : UserControl
    {
        public NguonKhachForm()
        {
            InitializeComponent();

            StyleGrid();
            LoadData();

            btnThemNguon.Click += btnThemNguon_Click;
            dgvNguonKhach.CellContentClick += dgvNguonKhach_CellContentClick;
        }

        void LoadData()
        {
            dgvNguonKhach.Rows.Add(1, "Website", "Form đăng ký");
            dgvNguonKhach.Rows.Add(2, "Facebook", "Quảng cáo");
            dgvNguonKhach.Rows.Add(3, "Google Ads", "Ads search");
            dgvNguonKhach.Rows.Add(4, "Giới thiệu", "Khách cũ giới thiệu");
        }

        void StyleGrid()
        {
            dgvNguonKhach.AllowUserToAddRows = false;
            dgvNguonKhach.RowHeadersVisible = false;

            dgvNguonKhach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNguonKhach.RowTemplate.Height = 40;
        }

        private void btnThemNguon_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thêm nguồn khách");
        }

        private void dgvNguonKhach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}