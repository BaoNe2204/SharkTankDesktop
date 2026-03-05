using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.BLL;
using SharkTank.DAL;
using SharkTank.DAL.InMemory;
using SharkTank.DAL.Sql;

namespace SharkTank.UI.Admin
{
    public class LoginForm : Form
    {
        private static bool _repoChoiceInitialized;
        private static bool _useSqlRepositories;

        private readonly AuthService _authService;
        private readonly PermissionService _permissionService;
        private readonly SessionService _sessionService;
        private readonly IRoleRepository _roleRepository;

        private TextBox _txtUsername;
        private TextBox _txtPassword;
        private Button _btnLogin;
        private Label _lblMessage;

        public SessionService SessionService => _sessionService;
        public PermissionService PermissionService => _permissionService;

        public LoginForm()
        {
            // Ưu tiên dùng SQL nếu đã cấu hình & dùng được (kiểm tra 1 lần duy nhất), sau đó cache kết quả.
            IUserRepository userRepository;
            IPermissionRepository permissionRepository;
            IRoleRepository roleRepository;

            CreateRepositories(out userRepository, out permissionRepository, out roleRepository);

            _authService = new AuthService(userRepository);
            _permissionService = new PermissionService(permissionRepository);
            _sessionService = new SessionService();
            _roleRepository = roleRepository;

            InitializeComponents();
        }

        private static void CreateRepositories(out IUserRepository userRepository, out IPermissionRepository permissionRepository, out IRoleRepository roleRepository)
        {
            if (!_repoChoiceInitialized)
            {
                try
                {
                    if (SqlConnectionFactory.HasConnectionString())
                    {
                        using (var conn = SqlConnectionFactory.Create())
                        {
                            conn.Open(); // kiểm tra một lần khi app start
                        }
                        _useSqlRepositories = true;
                    }
                    else
                    {
                        _useSqlRepositories = false;
                    }
                }
                catch
                {
                    _useSqlRepositories = false;
                }

                _repoChoiceInitialized = true;
            }

            if (_useSqlRepositories)
            {
                userRepository = new SqlUserRepository();
                permissionRepository = new SqlPermissionRepository();
                roleRepository = new SqlRoleRepository();
            }
            else
            {
                userRepository = new InMemoryUserRepository();
                permissionRepository = new InMemoryPermissionRepository();
                roleRepository = new InMemoryRoleRepository();
            }
        }

        private void InitializeComponents()
        {
            Text = "Đăng nhập hệ thống";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(360, 220);

            var lblTitle = new Label
            {
                Text = "ĐĂNG NHẬP ERP",
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(110, 20)
            };

            var lblUsername = new Label
            {
                Text = "Username:",
                AutoSize = true,
                Location = new Point(40, 70)
            };

            _txtUsername = new TextBox
            {
                Location = new Point(130, 65),
                Width = 180
            };

            var lblPassword = new Label
            {
                Text = "Password:",
                AutoSize = true,
                Location = new Point(40, 105)
            };

            _txtPassword = new TextBox
            {
                Location = new Point(130, 100),
                Width = 180,
                UseSystemPasswordChar = true
            };

            _btnLogin = new Button
            {
                Text = "Đăng nhập",
                Location = new Point(130, 140),
                Width = 100
            };
            _btnLogin.Click += BtnLogin_Click;

            _lblMessage = new Label
            {
                AutoSize = true,
                ForeColor = Color.Red,
                Location = new Point(40, 180),
                Width = 280
            };

            Controls.Add(lblTitle);
            Controls.Add(lblUsername);
            Controls.Add(_txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(_txtPassword);
            Controls.Add(_btnLogin);
            Controls.Add(_lblMessage);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            _lblMessage.Text = string.Empty;

            try
            {
                var result = _authService.Login(_txtUsername.Text, _txtPassword.Text);
                if (!result.Success)
                {
                    _lblMessage.Text = result.ErrorMessage;
                    return;
                }

                // Load role name (không hardcode) để MainForm hiển thị đúng role
                result.User.Role = _roleRepository.GetById(result.User.RoleId);

                // Khởi tạo session & load quyền
                _sessionService.StartSession(result.User);
                _permissionService.LoadPermissionsForRole(result.User.RoleId);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _lblMessage.Text = "Có lỗi khi đăng nhập: " + ex.Message;
            }
        }
    }
}

