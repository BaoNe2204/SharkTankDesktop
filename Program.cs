using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharkTank.UI.Admin;
using SharkTank.UI;

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

                    using (var mainForm = new MainForm(loginForm.SessionService, loginForm.PermissionService))
                    {
                        Application.Run(mainForm);
                        // Nếu bấm Đăng xuất thì lặp lại vòng while để hiện LoginForm ngay
                        keepRunning = mainForm.RequestLogout;
                    }
                }
            }
        }
    }
}
