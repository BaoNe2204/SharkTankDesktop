using System;
using System.Windows.Forms;
using SharkTank.UI.Admin;

namespace SharkTank
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var keepRunning = true;
            while (keepRunning)
            {
                using (var loginForm = new LoginForm())
                {
                    var result = loginForm.ShowDialog();
                    if (result != DialogResult.OK)
                    {
                        // Người dùng không đăng nhập (Cancel/Close) -> thoát app
                        break;
                    }

                    using (var mainDashboard = new MainDashboard(loginForm.SessionService, loginForm.PermissionService))
                    {
                        Application.Run(mainDashboard);
                        // Nếu bấm Đăng xuất thì lặp lại vòng while để hiện LoginForm ngay
                        keepRunning = mainDashboard.RequestLogout;
                    }
                }
            }
        }
    }
}
