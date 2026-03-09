using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class ChucVuForm : UserControl
    {
        int selectedId = -1;

        public ChucVuForm()
        {
            InitializeComponent();

            LoadData();

            dgvChucVu.CellClick += dgvChucVu_CellClick;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
        }

        void LoadData()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = "SELECT ChucVuId, TenChucVu, MoTa FROM ChucVu";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvChucVu.DataSource = dt;
            }
        }

        void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ChucVuId, TenChucVu, MoTa 
                                 FROM ChucVu
                                 WHERE TenChucVu LIKE @Ten";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@Ten", "%" + txtTimKiem.Text + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvChucVu.DataSource = dt;
            }
        }

        private void dgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChucVu.Rows[e.RowIndex];

                selectedId = Convert.ToInt32(row.Cells["ChucVuId"].Value);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ChucVuAddForm f = new ChucVuAddForm();

            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadData(); 
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Chọn chức vụ cần sửa");
                return;
            }

            ChucVuEditForm f = new ChucVuEditForm(selectedId);

            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Chọn chức vụ cần xóa");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Confirm",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM ChucVu WHERE ChucVuId=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", selectedId);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Xóa thành công");

            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            selectedId = -1;

            LoadData();
        }
        private void lblTenChucVu_Click(object sender, EventArgs e)
        {

        }
    }
}