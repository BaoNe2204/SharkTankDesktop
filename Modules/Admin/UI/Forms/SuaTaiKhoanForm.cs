using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public class SuaTaiKhoanForm : Form
    {
        int userId;

        TextBox txtUsername;
        ComboBox cboRole;
        ComboBox cboStatus;

        Button btnUpdate;
        Button btnCancel;

        public SuaTaiKhoanForm(int id)
        {
            userId = id;
            InitializeComponent();
            LoadRoles();
            LoadUser();
        }

        private void InitializeComponent()
        {
            Text = "Sửa tài khoản";
            Size = new Size(420, 350);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            Label lblTitle = new Label()
            {
                Text = "✏️ Sửa tài khoản người dùng",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 90, 160),
                Location = new Point(20, 10),
                AutoSize = true
            };

            int y = 60;

            txtUsername = CreateTextBox("Username", ref y);
            cboRole = CreateComboBox("Vai trò (Role)", ref y);
            cboStatus = CreateComboBox("Trạng thái", ref y);

            cboStatus.Items.AddRange(new string[]
            {
                "Active",
                "Locked"
            });

            btnUpdate = new Button()
            {
                Text = "💾 Cập nhật",
                Width = 110,
                Height = 35,
                Location = new Point(80, y + 20),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnCancel = new Button()
            {
                Text = "❌ Hủy",
                Width = 110,
                Height = 35,
                Location = new Point(210, y + 20),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnUpdate.Click += UpdateUser;
            btnCancel.Click += Cancel_Click;

            Controls.Add(lblTitle);
            Controls.Add(btnUpdate);
            Controls.Add(btnCancel);
        }

        TextBox CreateTextBox(string label, ref int y)
        {
            Label lbl = new Label()
            {
                Text = label,
                Location = new Point(30, y),
                AutoSize = true
            };

            TextBox txt = new TextBox()
            {
                Location = new Point(30, y + 20),
                Width = 330
            };

            Controls.Add(lbl);
            Controls.Add(txt);

            y += 55;

            return txt;
        }

        ComboBox CreateComboBox(string label, ref int y)
        {
            Label lbl = new Label()
            {
                Text = label,
                Location = new Point(30, y),
                AutoSize = true
            };

            ComboBox cbo = new ComboBox()
            {
                Location = new Point(30, y + 20),
                Width = 330,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Controls.Add(lbl);
            Controls.Add(cbo);

            y += 55;

            return cbo;
        }

        void LoadRoles()
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT RoleId, RoleName FROM Roles", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboRole.DataSource = dt;
                cboRole.DisplayMember = "RoleName";
                cboRole.ValueMember = "RoleId";
            }
        }

        void LoadUser()
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT Username, RoleId, IsActive
                    FROM Users
                    WHERE UserId=@id", conn);

                cmd.Parameters.AddWithValue("@id", userId);

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    txtUsername.Text = rd["Username"].ToString();
                    cboRole.SelectedValue = rd["RoleId"];
                    cboStatus.Text = (bool)rd["IsActive"] ? "Active" : "Locked";
                }
            }
        }

        void UpdateUser(object sender, EventArgs e)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                bool active = cboStatus.Text == "Active";

                SqlCommand cmd = new SqlCommand(@"
            UPDATE Users
            SET Username=@u,
                RoleId=@r,
                IsActive=@a,
                IsLocked=@l,
                UpdatedAt=GETDATE()
            WHERE UserId=@id", conn);

                cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                cmd.Parameters.AddWithValue("@r", cboRole.SelectedValue);
                cmd.Parameters.AddWithValue("@a", active);
                cmd.Parameters.AddWithValue("@l", !active);
                cmd.Parameters.AddWithValue("@id", userId);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cập nhật tài khoản thành công");
            Close();
        }

        void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}