using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using SharkTank.Core.Data;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public class ThemTaiKhoanForm : Form
    {
        TextBox txtUsername;
        TextBox txtPassword;
        ComboBox cboRole;
        ComboBox cboStatus;

        Button btnSave;
        Button btnCancel;

        public ThemTaiKhoanForm()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void InitializeComponent()
        {
            Text = "Tạo tài khoản";
            Size = new Size(420, 380);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            Label lblTitle = new Label()
            {
                Text = "👤 Tạo tài khoản người dùng",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 90, 160),
                Location = new Point(20, 10),
                AutoSize = true
            };

            int y = 60;

            txtUsername = CreateTextBox("Username", ref y);
            txtPassword = CreateTextBox("Password", ref y);
            txtPassword.PasswordChar = '*';

            cboRole = CreateComboBox("Vai trò (Role)", ref y);
            cboStatus = CreateComboBox("Trạng thái", ref y);

            cboStatus.Items.AddRange(new string[]
            {
                "Active",
                "Locked"
            });

            btnSave = new Button()
            {
                Text = "💾 Lưu",
                Width = 100,
                Height = 35,
                Location = new Point(90, y + 20),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnCancel = new Button()
            {
                Text = "❌ Hủy",
                Width = 100,
                Height = 35,
                Location = new Point(210, y + 20),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnSave.Click += SaveAccount;
            btnCancel.Click += Cancel_Click;

            Controls.Add(lblTitle);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
        }
        void Cancel_Click(object sender, EventArgs e)
        {
            Close();
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

        void SaveAccount(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Username và Password");
                return;
            }

            string salt = Guid.NewGuid().ToString("N");
            string hash = HashPassword(txtPassword.Text, salt);

            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                INSERT INTO Users
                (Username, PasswordHash, PasswordSalt, RoleId, IsActive, IsLocked, CreatedAt, UpdatedAt)
                VALUES
                (@u,@h,@s,@role,@active,@locked,GETDATE(),GETDATE())", conn);

                cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                cmd.Parameters.AddWithValue("@h", hash);
                cmd.Parameters.AddWithValue("@s", salt);
                cmd.Parameters.AddWithValue("@role", cboRole.SelectedValue);

                bool active = cboStatus.Text == "Active";

                cmd.Parameters.AddWithValue("@active", active);
                cmd.Parameters.AddWithValue("@locked", !active);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Tạo tài khoản thành công");
            Close();
        }

        string HashPassword(string password, string salt)
        {
            using (SHA256 sha = SHA256.Create())
            {
                string combined = password + salt;
                byte[] bytes = Encoding.Unicode.GetBytes(combined);
                byte[] hash = sha.ComputeHash(bytes);

                return BitConverter.ToString(hash)
                    .Replace("-", "")
                    .ToLower();
            }
        }
    }
}