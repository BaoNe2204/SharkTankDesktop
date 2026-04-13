using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Inventory.UI.Forms
{
    public partial class FrmViTriKho : Form
    {
        public string MaViTri { get; set; }
        public string TenViTri { get; set; }
        public string MaKho { get; set; }

        public FrmViTriKho()
        {
            InitializeComponent();
            LoadKho();
        }

        // ================= LOAD COMBO =================
        void LoadKho()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string sql = "SELECT MaKho, TenKho FROM Kho";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboKho.DataSource = dt;
                cboKho.DisplayMember = "TenKho";
                cboKho.ValueMember = "MaKho";
            }
        }

        // ================= SET DATA =================
        public void SetData(string ma, string ten, string kho)
        {
            txtMaViTri.Text = ma;
            txtTenViTri.Text = ten;
            cboKho.SelectedValue = kho;
        }

        // ================= LƯU =================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaViTri.Text == "" || txtTenViTri.Text == "")
            {
                MessageBox.Show("Nhập đầy đủ dữ liệu");
                return;
            }

            MaViTri = txtMaViTri.Text;
            TenViTri = txtTenViTri.Text;
            MaKho = cboKho.SelectedValue.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ================= HỦY =================
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}