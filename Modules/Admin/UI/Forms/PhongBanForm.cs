using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class PhongBanForm : UserControl
    {
        int selectedId = -1;

        public PhongBanForm()
        {
            InitializeComponent();

            LoadData();

            dgvPhongBan.CellClick += dgvPhongBan_CellClick;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
        }

        void LoadData(string keyword = "")
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"SELECT PhongBanId, TenPhongBan, MoTa
                                 FROM PhongBan
                                 WHERE TenPhongBan LIKE @Ten";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@Ten", "%" + keyword + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPhongBan.DataSource = dt;
            }
        }

        void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text);
        }

        private void dgvPhongBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhongBan.Rows[e.RowIndex];
                selectedId = Convert.ToInt32(row.Cells["PhongBanId"].Value);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            PhongBanAddForm f = new PhongBanAddForm();

            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadData(txtTimKiem.Text);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Chọn phòng ban cần sửa");
                return;
            }

            PhongBanEditForm f = new PhongBanEditForm(selectedId);

            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadData(txtTimKiem.Text);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Chọn phòng ban cần xóa");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Confirm",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string tenPB = "";
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (var c = new SqlCommand("SELECT TenPhongBan FROM PhongBan WHERE PhongBanId=@Id", conn)) { c.Parameters.AddWithValue("@Id", selectedId); var r = c.ExecuteScalar(); if (r != null) tenPB = r.ToString(); }
                using (var c = new SqlCommand("DELETE FROM PhongBan WHERE PhongBanId=@Id", conn)) { c.Parameters.AddWithValue("@Id", selectedId); c.ExecuteNonQuery(); }

                // Ghi DataChangeLogs + AuditLogs
                AuditHelper.Delete("PhongBan", selectedId.ToString(), tenPB, "PhongBanId");
            }

            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            selectedId = -1;
            LoadData();
        }
    }
}