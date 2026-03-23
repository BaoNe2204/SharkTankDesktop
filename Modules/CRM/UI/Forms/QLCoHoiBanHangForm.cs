using SharkTank.Core.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SharkTank.Modules.CRM.UI.Forms
{
    public partial class QLCoHoiBanHangForm : UserControl
    {
        int selectedId = -1;

        public QLCoHoiBanHangForm()
        {
            InitializeComponent();

            LoadLeads();
            LoadData();
            LoadAutoComplete();

            dgv.CellClick += dgv_CellClick;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnSearch.Click += btnSearch_Click;
            txtGiaTri.TextChanged += txtGiaTri_TextChanged;
        }

        // LOAD DATA
        void LoadData()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"SELECT o.QLCoHoiBanHangID,
                                        o.TenCoHoi,
                                        l.Ten,
                                        l.SoDienThoai,
                                        o.GiaTriDuKien,
                                        o.XacSuat,
                                        o.TrangThai
                                 FROM QLCoHoiBanHang o
                                 LEFT JOIN Leads l
                                 ON o.LeadID = l.LeadID";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);
                dgv.DataSource = dt;
                dgv.Columns["QLCoHoiBanHangID"].Visible = false;
                dgv.Columns["QLCoHoiBanHangID"].HeaderText = "ID";
                dgv.Columns["TenCoHoi"].HeaderText = "Tên cơ hội";
                dgv.Columns["Ten"].HeaderText = "Khách hàng";
                dgv.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
                dgv.Columns["GiaTriDuKien"].HeaderText = "Giá trị dự kiến";
                dgv.Columns["XacSuat"].HeaderText = "Xác suất (%)";
                dgv.Columns["TrangThai"].HeaderText = "Trạng thái";
            }

            selectedId = -1;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Columns["GiaTriDuKien"].DefaultCellStyle.Format = "N0";
        }

        // LOAD LEADS
        void LoadLeads()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"SELECT LeadID,
                         CAST(LeadID AS NVARCHAR) + ' - ' + Ten AS TenHienThi
                         FROM Leads";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);

                cboLead.DataSource = dt;
                cboLead.DisplayMember = "TenHienThi"; // hiển thị ID + Tên
                cboLead.ValueMember = "LeadID";       // giá trị thật

                cboLead.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cboLead.AutoCompleteSource = AutoCompleteSource.ListItems;
                cboLead.DropDownStyle = ComboBoxStyle.DropDown;

                cboLead.SelectedIndex = -1;
            }
        }

        // THÊM
        void Them()
        {
            if (txtTenCoHoi.Text.Trim() == "")
            {
                MessageBox.Show("Nhập tên cơ hội");
                return;
            }

            if (!decimal.TryParse(txtGiaTri.Text, out decimal giaTri))
            {
                MessageBox.Show("Giá trị phải là số");
                return;
            }

            if (!int.TryParse(txtXacSuat.Text, out int xacSuat))
            {
                MessageBox.Show("Xác suất phải là số");
                return;
            }

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO QLCoHoiBanHang
                        (TenCoHoi,LeadID,GiaTriDuKien,XacSuat,TrangThai)
                        VALUES
                        (@ten,@lead,@gia,@xs,@tt)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ten", txtTenCoHoi.Text);
                cmd.Parameters.AddWithValue("@lead", cboLead.SelectedValue);
                decimal GiaTri = Convert.ToDecimal(txtGiaTri.Text.Replace(".", ""));
                cmd.Parameters.AddWithValue("@gia", giaTri);
                cmd.Parameters.AddWithValue("@xs", xacSuat);
                cmd.Parameters.AddWithValue("@tt", cboTrangThai.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm thành công");

            LoadData();
            ClearForm();
        }

        // SỬA
        void Sua()
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Chọn cơ hội cần sửa");
                return;
            }

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE QLCoHoiBanHang
                                SET TenCoHoi=@ten,
                                    LeadID=@lead,
                                    GiaTriDuKien=@gia,
                                    XacSuat=@xs,
                                    TrangThai=@tt
                                WHERE QLCoHoiBanHangID=@id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", selectedId);
                cmd.Parameters.AddWithValue("@ten", txtTenCoHoi.Text);
                cmd.Parameters.AddWithValue("@lead", cboLead.SelectedValue);
                cmd.Parameters.AddWithValue("@gia", Convert.ToDecimal(txtGiaTri.Text));
                cmd.Parameters.AddWithValue("@xs", Convert.ToInt32(txtXacSuat.Text));
                cmd.Parameters.AddWithValue("@tt", cboTrangThai.Text);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cập nhật thành công");

            LoadData();
            ClearForm();
        }

        // XÓA
        void Xoa()
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Chọn cơ hội cần xóa");
                return;
            }

            if (MessageBox.Show("Bạn chắc chắn muốn xóa?",
                "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM QLCoHoiBanHang WHERE QLCoHoiBanHangID=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", selectedId);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Xóa thành công");

            LoadData();
            ClearForm();
        }

        // CLICK ROW
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgv.Rows[e.RowIndex];

            selectedId = Convert.ToInt32(row.Cells["QLCoHoiBanHangID"].Value);

            txtTenCoHoi.Text = row.Cells["TenCoHoi"].Value.ToString();
            txtGiaTri.Text = row.Cells["GiaTriDuKien"].Value.ToString();
            txtXacSuat.Text = row.Cells["XacSuat"].Value.ToString();
            cboTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
            cboLead.Text = row.Cells["Ten"].Value.ToString();
        }

        // CLEAR
        void ClearForm()
        {
            txtTenCoHoi.Clear();
            txtGiaTri.Clear();
            txtXacSuat.Clear();

            cboTrangThai.SelectedIndex = -1;
            selectedId = -1;
        }

        // BUTTON
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                LoadData();
                return;
            }

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"SELECT o.QLCoHoiBanHangID,
                        o.TenCoHoi,
                        l.Ten,
                        l.SoDienThoai,
                        o.GiaTriDuKien,
                        o.XacSuat,
                        o.TrangThai
                 FROM QLCoHoiBanHang o
                 LEFT JOIN Leads l
                 ON o.LeadID = l.LeadID
                 WHERE o.TenCoHoi LIKE @key
                 OR l.Ten LIKE @key";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                da.SelectCommand.Parameters.AddWithValue("@key", "%" + txtSearch.Text + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgv.DataSource = dt;
            }
        }
        private void txtGiaTri_TextChanged(object sender, EventArgs e)
        {
            if (txtGiaTri.Text == "") return;

            string text = txtGiaTri.Text.Replace(".", "");

            if (decimal.TryParse(text, out decimal value))
            {
                txtGiaTri.TextChanged -= txtGiaTri_TextChanged;

                txtGiaTri.Text = string.Format("{0:N0}", value);
                txtGiaTri.SelectionStart = txtGiaTri.Text.Length;

                txtGiaTri.TextChanged += txtGiaTri_TextChanged;
            }
        }
        void LoadAutoComplete()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = "SELECT LeadID, Ten FROM Leads";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                AutoCompleteStringCollection auto = new AutoCompleteStringCollection();

                while (reader.Read())
                {
                    auto.Add(reader["LeadID"].ToString());
                    auto.Add(reader["Ten"].ToString());
                }

                txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSearch.AutoCompleteCustomSource = auto;
            }
        }
    }
}