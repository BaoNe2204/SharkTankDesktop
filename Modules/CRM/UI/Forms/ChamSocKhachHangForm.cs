using SharkTank.Core.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SharkTank.Modules.CRM.UI.Forms
{
    public partial class ChamSocKhachHangForm : UserControl
    {
        int selectedId = -1;

        public ChamSocKhachHangForm()
        {
            InitializeComponent();
            LoadData();
        }

        // LOAD DATA
        void LoadData()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM ChamSocKhachHang";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dgv.DataSource = dt;

                if (dgv.Columns["Id"] != null)
                    dgv.Columns["Id"].Visible = false;
            }
        }

        // THÊM
        void Them()
        {
            if (txtKhachHang.Text.Trim() == "")
            {
                MessageBox.Show("Nhập tên khách hàng");
                return;
            }

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO ChamSocKhachHang
                                (KhachHang,Ngay,Loai,NoiDung)
                                VALUES
                                (@kh,@ngay,@loai,@nd)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@kh", txtKhachHang.Text);
                cmd.Parameters.AddWithValue("@ngay", dtNgay.Value);
                cmd.Parameters.AddWithValue("@loai", cboLoai.Text);
                cmd.Parameters.AddWithValue("@nd", txtNoiDung.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm thành công");

            ClearForm();
            LoadData();
        }

        // SỬA
        void Sua()
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Chọn dòng cần sửa");
                return;
            }

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE ChamSocKhachHang
                                SET KhachHang=@kh,
                                    Ngay=@ngay,
                                    Loai=@loai,
                                    NoiDung=@nd
                                WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", selectedId);
                cmd.Parameters.AddWithValue("@kh", txtKhachHang.Text);
                cmd.Parameters.AddWithValue("@ngay", dtNgay.Value);
                cmd.Parameters.AddWithValue("@loai", cboLoai.Text);
                cmd.Parameters.AddWithValue("@nd", txtNoiDung.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cập nhật thành công");

            ClearForm();
            LoadData();
        }

        // XÓA
        void Xoa()
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Chọn dòng cần xóa");
                return;
            }

            if (MessageBox.Show("Bạn chắc chắn muốn xóa?",
                "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM ChamSocKhachHang WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", selectedId);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Xóa thành công");

            ClearForm();
            LoadData();
        }

        // CLICK ROW
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgv.Rows[e.RowIndex].Cells["Id"].Value == null)
                return;

            DataGridViewRow row = dgv.Rows[e.RowIndex];

            selectedId = Convert.ToInt32(row.Cells["Id"].Value);

            txtKhachHang.Text = row.Cells["KhachHang"].Value.ToString();
            dtNgay.Value = Convert.ToDateTime(row.Cells["Ngay"].Value);
            cboLoai.Text = row.Cells["Loai"].Value.ToString();
            txtNoiDung.Text = row.Cells["NoiDung"].Value.ToString();
        }

        // CLEAR FORM
        void ClearForm()
        {
            txtKhachHang.Clear();
            txtNoiDung.Clear();
            cboLoai.SelectedIndex = -1;

            selectedId = -1;
        }

        // BUTTON EVENTS
        private void btnThem_Click(object sender, EventArgs e)
        {
            Them();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Sua();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        

        // SEARCH
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                LoadData();
                return;
            }

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"SELECT * FROM ChamSocKhachHang
                                WHERE KhachHang LIKE @key";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@key", "%" + txtSearch.Text.Trim() + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgv.DataSource = dt;
            }
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                LoadData();
                return;
            }

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"SELECT * FROM ChamSocKhachHang
                         WHERE KhachHang LIKE @key
                         OR NoiDung LIKE @key
                         OR Loai LIKE @key";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@key", "%" + txtSearch.Text.Trim() + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgv.DataSource = dt;
            }
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTim_Click(sender, e);
            }
        }

        private void formPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblNoiDung_Click(object sender, EventArgs e)
        {

        }
    }
}