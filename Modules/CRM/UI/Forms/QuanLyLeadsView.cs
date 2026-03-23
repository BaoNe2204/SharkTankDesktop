using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using SharkTank.Core.Data;

namespace SharkTank.Modules.CRM.UI.Forms
{
    public partial class QuanLyLeadsView : UserControl
    {
        private int _editingLeadId = -1;

        public QuanLyLeadsView()
        {
            InitializeComponent();
            LoadLeads();
            dgvLeads.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLeads.MultiSelect = false;
        }

        private void LoadLeads()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string query = "SELECT LeadID, Ten, SoDienThoai, Email, Nguon, TrangThai FROM Leads";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvLeads.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void dgvLeads_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLeads.Rows[e.RowIndex];
                txtTen.Text = row.Cells["Ten"].Value?.ToString();
                txtPhone.Text = row.Cells["SoDienThoai"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                cbNguon.Text = row.Cells["Nguon"].Value?.ToString();
                cbTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _editingLeadId = -1;
            panelAddLead.Visible = true;
            panelAddLead.BringToFront();
            panelAddLead.Left = (this.Width - panelAddLead.Width) / 2;
            panelAddLead.Top = (this.Height - panelAddLead.Height) / 2;

            txtTen.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            cbNguon.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập tên và số điện thoại!");
                return;
            }

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    if (_editingLeadId == -1)
                    {
                        string query = @"INSERT INTO Leads (Ten, SoDienThoai, Email, Nguon, TrangThai) 
                                         VALUES(@Ten, @Phone, @Email, @Nguon, @TrangThai)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Nguon", cbNguon.Text ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TrangThai", cbTrangThai.Text ?? (object)DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string query = @"UPDATE Leads 
                                         SET Ten = @Ten, SoDienThoai = @Phone, Email = @Email, 
                                             Nguon = @Nguon, TrangThai = @TrangThai
                                         WHERE LeadID = @Id";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", _editingLeadId);
                            cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Nguon", cbNguon.Text ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TrangThai", cbTrangThai.Text ?? (object)DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                _editingLeadId = -1;
                panelAddLead.Visible = false;
                LoadLeads();
                MessageBox.Show("Lưu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _editingLeadId = -1;
            panelAddLead.Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow == null)
            {
                MessageBox.Show("Chọn Lead cần sửa!");
                return;
            }

            _editingLeadId = Convert.ToInt32(dgvLeads.CurrentRow.Cells["LeadID"].Value);
            txtTen.Text = dgvLeads.CurrentRow.Cells["Ten"].Value.ToString();
            txtPhone.Text = dgvLeads.CurrentRow.Cells["SoDienThoai"].Value.ToString();
            txtEmail.Text = dgvLeads.CurrentRow.Cells["Email"].Value?.ToString();
            cbNguon.Text = dgvLeads.CurrentRow.Cells["Nguon"].Value?.ToString();
            cbTrangThai.Text = dgvLeads.CurrentRow.Cells["TrangThai"].Value?.ToString();

            panelAddLead.Visible = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow == null)
            {
                MessageBox.Show("Chọn Lead cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa Lead này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvLeads.CurrentRow.Cells["LeadID"].Value);

                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        string query = "DELETE FROM Leads WHERE LeadID = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadLeads();
                    MessageBox.Show("Xóa thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách cần gọi!");
                return;
            }

            string phone = dgvLeads.CurrentRow.Cells["SoDienThoai"].Value?.ToString();

            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Không có số điện thoại!");
                return;
            }

            try
            {
                System.Diagnostics.Process.Start("zalo://conversation?phone=" + phone);
            }
            catch
            {
                MessageBox.Show("Không mở được ứng dụng Zalo!");
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow == null) return;

            string email = dgvLeads.CurrentRow.Cells["Email"].Value?.ToString();

            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    string url = "https://mail.google.com/mail/?view=cm&to=" + email;
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lead chưa có email!");
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow == null)
            {
                MessageBox.Show("Chọn Lead cần chuyển!");
                return;
            }

            int id = Convert.ToInt32(dgvLeads.CurrentRow.Cells["LeadID"].Value);

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string query = "UPDATE Leads SET TrangThai = 'KhachHang' WHERE LeadID = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadLeads();
                MessageBox.Show("Chuyển thành khách hàng thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (dgvLeads.DataSource is DataTable dt)
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    dt.DefaultView.RowFilter =
                        $"Ten LIKE '%{keyword}%' OR SoDienThoai LIKE '%{keyword}%'";
                }
            }
        }
    }
}
    
