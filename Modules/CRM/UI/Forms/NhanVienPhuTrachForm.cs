using SharkTank.Core.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SharkTank.Modules.CRM.UI.Forms
{
    public partial class NhanVienPhuTrachForm : UserControl
    {
        int maNV = 0;

        public NhanVienPhuTrachForm()
        {
            InitializeComponent();
            CreateFormPanel();

            if (pnlForm != null)
                pnlForm.Visible = false;

            if (dgvNhanVien != null)
                dgvNhanVien.CellClick += dgvNhanVien_CellClick;

            if (btnThem != null)
                btnThem.Click += btnThem_Click;

            if (btnSua != null)
                btnSua.Click += btnSua_Click;

            if (btnXoa != null)
                btnXoa.Click += btnXoa_Click;

            if (btnLuu != null)
                btnLuu.Click += btnLuu_Click;

            if (btnHuy != null)
                btnHuy.Click += btnHuy_Click;

            if (btnSearch != null)
                btnSearch.Click += btnSearch_Click;

            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.MultiSelect = false;

            LoadNhanVien();
        }

        // LOAD DATA
        void LoadNhanVien()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT * FROM NhanVienPhuTrach";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvNhanVien.AutoGenerateColumns = false;
                dgvNhanVien.DataSource = dt;
            }
        }

        // NÚT THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            maNV = 0;

            ClearForm();

            pnlForm.Visible = true;
            pnlForm.BringToFront();

        }

        // NÚT SỬA
         private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null) return;

            maNV = Convert.ToInt32(dgvNhanVien.CurrentRow.Cells["MaNV"].Value);

            txtTenNV.Text = dgvNhanVien.CurrentRow.Cells["TenNV"].Value.ToString();
            txtEmail.Text = dgvNhanVien.CurrentRow.Cells["Email"].Value.ToString();
            txtPhone.Text = dgvNhanVien.CurrentRow.Cells["DienThoai"].Value.ToString();

            pnlForm.Visible = true;
            pnlForm.BringToFront();
        }

        // NÚT XÓA
        void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvNhanVien.CurrentRow.Cells["MaNV"].Value);

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa?",
                "Xác nhận",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string query = "DELETE FROM NhanVienPhuTrach WHERE MaNV=@id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadNhanVien();
            }
        }

        // NÚT LƯU
        void btnLuu_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlCommand cmd;

                if (maNV == 0)
                {
                    string query = "INSERT INTO NhanVienPhuTrach(TenNV,Email,DienThoai) VALUES(@ten,@email,@dt)";
                    cmd = new SqlCommand(query, conn);
                }
                else
                {
                    string query = "UPDATE NhanVienPhuTrach SET TenNV=@ten,Email=@email,DienThoai=@dt WHERE MaNV=@id";
                    cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@id", maNV);
                }

                cmd.Parameters.AddWithValue("@ten", txtTenNV.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@dt", txtPhone.Text);

                cmd.ExecuteNonQuery();
            }

            LoadNhanVien();

            pnlForm.Visible = false;

            ClearForm();
        }

        // NÚT HỦY
        void btnHuy_Click(object sender, EventArgs e)
        {
            pnlForm.Visible = false;
        }

        // CLICK DATAGRIDVIEW
        void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

            txtTenNV.Text = row.Cells["TenNV"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtPhone.Text = row.Cells["DienThoai"].Value.ToString();
            dgvNhanVien.ReadOnly = true;
        }

        // CLEAR FORM
        void ClearForm()
        {
            if (txtTenNV != null)
                txtTenNV.Text = "";

            if (txtEmail != null)
                txtEmail.Text = "";

            if (txtPhone != null)
                txtPhone.Text = "";
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string keyword = txtSearch.Text.Trim();

                string query = "SELECT * FROM NhanVienPhuTrach WHERE TenNV LIKE @kw OR Email LIKE @kw";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvNhanVien.DataSource = dt;
            }
        }
    }
}