using SharkTank.BLL;
using SharkTank.DAL;
using SharkTank.DAL.InMemory;
using SharkTank.DAL.Sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SharkTank.UI.Admin
{
    public partial class LoginForm : Form
    {
        private static bool _repoChoiceInitialized;
        private static bool _useSqlRepositories;

        private readonly AuthService _authService;
        private readonly PermissionService _permissionService;
        private readonly SessionService _sessionService;
        private readonly IRoleRepository _roleRepository;

        public SessionService SessionService => _sessionService;
        public PermissionService PermissionService => _permissionService;

        public LoginForm()
        {
            // Khi mở trong Designer, bỏ qua khởi tạo service / kết nối DB
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                InitializeComponent();
                return;
            }

            IUserRepository userRepository;
            IPermissionRepository permissionRepository;
            IRoleRepository roleRepository;

            CreateRepositories(out userRepository, out permissionRepository, out roleRepository);

            _authService = new AuthService(userRepository);
            _permissionService = new PermissionService(permissionRepository);
            _sessionService = new SessionService();
            _roleRepository = roleRepository;

            InitializeComponent();
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

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            try
            {
                var result = _authService.Login(txtUsername.Text, txtPassword.Text);
                if (!result.Success)
                {
                    lblMessage.Text = result.ErrorMessage;
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
                lblMessage.Text = "Có lỗi khi đăng nhập: " + ex.Message;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ApplyRoundedCorners();
        }

        private void ApplyRoundedCorners()
        {
            if (panel1 == null) return;

            GraphicsPath path = new GraphicsPath();
            int radius = 20;

            Rectangle bounds = panel1.ClientRectangle;

            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);

            path.CloseAllFigures();
            panel1.Region = new Region(path);
        }
    }
}

