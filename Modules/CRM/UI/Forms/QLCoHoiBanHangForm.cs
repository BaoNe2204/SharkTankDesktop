using SharkTank.Core.Data;
using SharkTank.BLL;
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
                cboLead.DisplayMember = "TenHienThi";
                cboLead.ValueMember = "LeadID";
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
                decimal giaTriValue = Convert.ToDecimal(txtGiaTri.Text.Replace(".", ""));
                cmd.Parameters.AddWithValue("@gia", giaTri);
                cmd.Parameters.AddWithValue("@xs", xacSuat);
                cmd.Parameters.AddWithValue("@tt", cboTrangThai.Text);
                cmd.ExecuteNonQuery();

                // Ghi DataChangeLogs + AuditLogs
                AuditHelper.Insert("QLCoHoiBanHang", txtTenCoHoi.Text, txtTenCoHoi.Text,
                    new QLCoHoiBanHangSnapshot
                    {
                        TenCoHoi = txtTenCoHoi.Text,
                        GiaTriDuKien = giaTri.ToString(),
                        XacSuat = xacSuat.ToString(),
                        TrangThai = cboTrangThai.Text
                    });
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

            // Đọc dữ liệu cũ
            var oldSnap = QLCoHoiBanHangSnapshot.FromDb(selectedId.ToString());
            var newSnap = new QLCoHoiBanHangSnapshot
            {
                TenCoHoi = txtTenCoHoi.Text,
                GiaTriDuKien = txtGiaTri.Text,
                XacSuat = txtXacSuat.Text,
                TrangThai = cboTrangThai.Text
            };

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

                // Ghi DataChangeLogs + AuditLogs
                AuditHelper.Update("QLCoHoiBanHang", selectedId.ToString(), txtTenCoHoi.Text, oldSnap, newSnap);
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

                // Ghi DataChangeLogs + AuditLogs
                AuditHelper.Delete("QLCoHoiBanHang", selectedId.ToString(), txtTenCoHoi.Text, "QLCoHoiBanHangID");
            }

            MessageBox.Show("Xóa thành công");
            LoadData();
            ClearForm();
        }

        void ClearForm()
        {
            txtTenCoHoi.Clear();
            txtGiaTri.Clear();
            txtXacSuat.Clear();
            cboLead.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = -1;
            selectedId = -1;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgv.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["QLCoHoiBanHangID"].Value);
            txtTenCoHoi.Text = row.Cells["TenCoHoi"].Value.ToString();
            cboLead.Text = row.Cells["Ten"].Value.ToString();
            txtGiaTri.Text = row.Cells["GiaTriDuKien"].Value.ToString();
            txtXacSuat.Text = row.Cells["XacSuat"].Value.ToString();
            cboTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
        }

        void LoadAutoComplete()
        {
            txtTenCoHoi.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTenCoHoi.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
            acsc.Add("Gọi điện xác nhận");
            acsc.Add("Gửi email");
            acsc.Add("Họp trực tiếp");
            acsc.Add("Gửi báo giá");
            txtTenCoHoi.AutoCompleteCustomSource = acsc;
        }

        private void txtGiaTri_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnThem_Click(object sender, EventArgs e) { Them(); }
        private void btnSua_Click(object sender, EventArgs e) { Sua(); }
        private void btnXoa_Click(object sender, EventArgs e) { Xoa(); }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
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
                                 LEFT JOIN Leads l ON o.LeadID = l.LeadID
                                 WHERE o.TenCoHoi LIKE @key OR l.Ten LIKE @key";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@key", "%" + keyword + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }
    }
}
