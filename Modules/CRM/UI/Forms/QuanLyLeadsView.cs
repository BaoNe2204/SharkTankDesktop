using SharkTank.Core.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace SharkTank.Modules.CRM.UI.Forms
{
    public partial class QuanLyLeadsForm : UserControl
    {
        public QuanLyLeadsForm()
        {
            InitializeComponent();
            LoadLeads(); // load dữ liệu ngay khi mở
            dgvLeads.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLeads.MultiSelect = false;
        }

        private void QuanLyLeadsForm_Load(object sender, EventArgs e)
        {
            LoadLeads();
        }

        // LOAD DATA
        private void LoadLeads()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT * FROM Leads";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvLeads.DataSource = dt;
            }
        }

        // CLICK ROW -> SHOW DATA
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

        // ADD LEAD
        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            panelAddLead.Visible = true;
            panelAddLead.BringToFront(); // đưa panel lên trên cùng
            panelAddLead.Left = (this.Width - panelAddLead.Width) / 2;
            panelAddLead.Top = (this.Height - panelAddLead.Height) / 2;
            editingLeadId = -1;

            txtTen.Clear();
            txtPhone.Clear();
            txtEmail.Clear();

            cbNguon.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;

        }
        // SAVE NEW LEAD
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtTen.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin!");
                return;
            }

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                SqlCommand cmd;

                conn.Open();

                if (editingLeadId == -1)
                {
                    // INSERT
                    string query = @"INSERT INTO Leads 
                    (Ten,SoDienThoai,Email,Nguon,TrangThai) 
                    VALUES(@Ten,@Phone,@Email,@Nguon,@TrangThai)";

                    cmd = new SqlCommand(query, conn);
                }
                else
                {
                    // UPDATE
                    string query = @"UPDATE Leads 
                    SET Ten=@Ten,
                        SoDienThoai=@Phone,
                        Email=@Email,
                        Nguon=@Nguon,
                        TrangThai=@TrangThai
                    WHERE LeadID=@Id";

                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", editingLeadId);
                }

                cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Nguon", cbNguon.Text);
                cmd.Parameters.AddWithValue("@TrangThai", cbTrangThai.Text);

                cmd.ExecuteNonQuery();
            }

            editingLeadId = -1;

            panelAddLead.Visible = false;

            LoadLeads();

            MessageBox.Show("Lưu thành công!");
        }
        // CANCEL ADD
        private void btnCancel_Click(object sender, EventArgs e)
        {
            editingLeadId = -1;
            panelAddLead.Visible = false;
        }

        // EDIT LEAD
        // biến lưu ID đang sửa
        int editingLeadId = -1;


        // =======================
        // NÚT SỬA
        // =======================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow == null)
            {
                MessageBox.Show("Chọn Lead cần sửa");
                return;
            }

            // lấy ID
            editingLeadId = Convert.ToInt32(dgvLeads.CurrentRow.Cells["LeadID"].Value);

            // đổ dữ liệu lên panel
            txtTen.Text = dgvLeads.CurrentRow.Cells["Ten"].Value.ToString();
            txtPhone.Text = dgvLeads.CurrentRow.Cells["SoDienThoai"].Value.ToString();
            txtEmail.Text = dgvLeads.CurrentRow.Cells["Email"].Value.ToString();
            cbNguon.Text = dgvLeads.CurrentRow.Cells["Nguon"].Value.ToString();
            cbTrangThai.Text = dgvLeads.CurrentRow.Cells["TrangThai"].Value.ToString();

            // mở panel
            panelAddLead.Visible = true;
        }


        // =======================
        // NÚT LƯU (UPDATE SQL)
        // =======================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTen.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin!");
                return;
            }

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                SqlCommand cmd;

                conn.Open();

                if (editingLeadId == -1)
                {
                    // INSERT
                    string query = @"INSERT INTO Leads 
                    (Ten,SoDienThoai,Email,Nguon,TrangThai) 
                    VALUES(@Ten,@Phone,@Email,@Nguon,@TrangThai)";

                    cmd = new SqlCommand(query, conn);
                }
                else
                {
                    // UPDATE
                    string query = @"UPDATE Leads 
                    SET Ten=@Ten,
                        SoDienThoai=@Phone,
                        Email=@Email,
                        Nguon=@Nguon,
                        TrangThai=@TrangThai
                    WHERE LeadID=@Id";

                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", editingLeadId);
                }

                cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Nguon", cbNguon.Text);
                cmd.Parameters.AddWithValue("@TrangThai", cbTrangThai.Text);

                cmd.ExecuteNonQuery();
            }

            editingLeadId = -1;

            panelAddLead.Visible = false;

            LoadLeads();

            MessageBox.Show("Lưu thành công!");
        }

        // =======================
        // NÚT HỦY
        // =======================
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            editingLeadId = -1;
            panelAddLead.Visible = false;
        }
        // DELETE LEAD
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow == null)
            {
                MessageBox.Show("Chọn Lead cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa Lead này?",
                "Xác nhận",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvLeads.CurrentRow.Cells["LeadID"].Value);

                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    string query = "DELETE FROM Leads WHERE LeadID=@Id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadLeads();
            }
        }

        // CALL ZALO
        private void btnCall_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách cần gọi!");
                return;
            }

            string phone = dgvLeads.CurrentRow.Cells["SoDienThoai"].Value.ToString();

            try
            {
                System.Diagnostics.Process.Start("zalo://conversation?phone=" + phone);
            }
            catch
            {
                MessageBox.Show("Không mở được ứng dụng Zalo!");
            }
        }

        // EMAIL
        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow == null) return;

            string email = dgvLeads.CurrentRow.Cells["Email"].Value?.ToString();

            if (!string.IsNullOrEmpty(email))
            {
                string url = "https://mail.google.com/mail/?view=cm&to=" + email;

                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Lead chưa có email!");
            }
        }

        // CONVERT LEAD
        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow == null)
            {
                MessageBox.Show("Chọn Lead cần chuyển!");
                return;
            }

            int id = Convert.ToInt32(dgvLeads.CurrentRow.Cells["LeadID"].Value);

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "UPDATE Leads SET TrangThai='KhachHang' WHERE LeadID=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadLeads();
            MessageBox.Show("Chuyển thành khách hàng thành công!");
        }

        // PHONE ONLY NUMBER
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        // SEARCH
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            DataTable dt = (DataTable)dgvLeads.DataSource;

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
    
