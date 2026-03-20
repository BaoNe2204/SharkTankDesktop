using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace SharkTank.Modules.CRM.UI.Forms
{
    public partial class QuanLyLeadsForm : UserControl
    {
        List<Lead> leads = new List<Lead>();
        int currentId = 1;

        public QuanLyLeadsForm()
        {
            InitializeComponent();
        }

        private void QuanLyLeadsForm_Load(object sender, EventArgs e)
        {
            btnAdd.Click += BtnAdd_Click;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Lead lead = new Lead()
            {
                Id = currentId++,
                TenKhach = txtTen.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Nguon = cbNguon.Text,
                TrangThai = cbTrangThai.Text
            };

            leads.Add(lead);

            dgvLeads.Rows.Add(
                lead.Id,
                lead.TenKhach,
                lead.Phone,
                lead.Email,
                lead.Nguon,
                lead.TrangThai,
                ""
            );

            ClearInput();
        }

        private void ClearInput()
        {
            txtTen.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            cbNguon.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;
        }

        private void PanelTitle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow != null)
            {
                if (!string.IsNullOrWhiteSpace(txtTen.Text))
                    dgvLeads.CurrentRow.Cells[1].Value = txtTen.Text;

                if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                    dgvLeads.CurrentRow.Cells[2].Value = txtPhone.Text;

                if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                    dgvLeads.CurrentRow.Cells[3].Value = txtEmail.Text;

                if (!string.IsNullOrWhiteSpace(cbNguon.Text))
                    dgvLeads.CurrentRow.Cells[4].Value = cbNguon.Text;

                if (!string.IsNullOrWhiteSpace(cbTrangThai.Text))
                    dgvLeads.CurrentRow.Cells[5].Value = cbTrangThai.Text;

                MessageBox.Show("Đã cập nhật Lead!");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow != null)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa Lead này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    dgvLeads.Rows.Remove(dgvLeads.CurrentRow);
                    MessageBox.Show("Đã xóa Lead!");
                }
            }
        }
        private void BtnCall_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow != null)
            {
                string phone = dgvLeads.CurrentRow.Cells[2].Value.ToString();
                MessageBox.Show("Hãy gọi số: " + phone);
            }
        }
        private void btnCall_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow != null)
            {
                string phone = dgvLeads.CurrentRow.Cells[2].Value?.ToString();

                if (!string.IsNullOrEmpty(phone))
                {
                    string zaloLink = "zalo://conversation?phone=" + phone;

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = zaloLink,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Lead này chưa có số điện thoại!");
                }
            }
        }
        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (dgvLeads.CurrentRow != null)
            {
                string email = dgvLeads.CurrentRow.Cells[3].Value?.ToString();

                if (!string.IsNullOrEmpty(email))
                {
                    string gmailUrl = "https://mail.google.com/mail/?view=cm&to=" + email;

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = gmailUrl,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Lead này chưa có email!");
                }
            }
        }
        // Model Lead
        public class Lead
        {
            public int Id { get; set; }
            public string TenKhach { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Nguon { get; set; }
            public string TrangThai { get; set; }
        }
    }
}
