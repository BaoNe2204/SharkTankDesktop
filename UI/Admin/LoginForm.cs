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
                _permissionService.LoadPermissionsForUser(result.User.UserId, result.User.RoleId);

                // Ghi nhật ký đăng nhập (bảng LoginHistory) — màn "Lịch sử đăng nhập" đọc từ đây
                try
                {
                    var audit = AuditService.CreateDefault();
                    var sess = _sessionService.CurrentSession;
                    audit.LogLogin(
                        result.User.UserId,
                        result.User.Username,
                        result.User.FullName,
                        result.User.Role != null ? result.User.Role.RoleName : null,
                        sess != null ? (sess.IpAddress ?? string.Empty) : string.Empty,
                        sess != null ? (sess.DeviceInfo ?? string.Empty) : (Environment.MachineName),
                        "Success");
                    // Lịch sử thao tác (AuditLogs)
                    audit.LogActionForUser(
                        result.User,
                        "LOGIN",
                        "System",
                        result.User.UserId.ToString(),
                        result.User.Username,
                        "Đăng nhập thành công");
                }
                catch (Exception logEx)
                {
                    System.Diagnostics.Debug.WriteLine("LoginHistory insert: " + logEx.Message);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Có lỗi khi đăng nhập: " + ex.Message;
            }
        }
        private void RoundButton(Button btn, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            ApplyRoundedCorners();
            Button btnLogin = this.Controls.Find("btnLogin", true)[0] as Button;
            RoundButton(btnLogin, 40);
            //label3.Parent = pictureBox1;
            //label3.BackColor = Color.Transparent;
        }
        private void linkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "Vui lòng liên hệ quản trị viên để đặt lại mật khẩu.",
                "Quên mật khẩu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "Tài khoản người dùng được tạo bởi quản trị viên hệ thống.\n\nVui lòng liên hệ bộ phận IT hoặc quản trị hệ thống để được cấp tài khoản.",
                "Yêu cầu tạo tài khoản",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


