using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharkTank.Modules;
using SharkTank.Modules.Accounting;
using SharkTank.Modules.Admin;
using SharkTank.Modules.HR;
using SharkTank.Modules.Inventory;
using SharkTank.Modules.Sales;
using SharkTank.Modules.CRM;

namespace SharkTank
{
    /// <summary>
    /// Quản lý việc hiển thị các view trong panelContent
    /// </summary>
    public class ViewManager
    {
        private Panel _contentPanel;
        private Dictionary<string, IModule> _modules;
        private string _currentModule = "";

        public ViewManager(Panel contentPanel)
        {
            _contentPanel = contentPanel;
            InitializeModules();
        }

        private void InitializeModules()
        {
            _modules = new Dictionary<string, IModule>
            {
                { "Accounting", new AccountingModule() },
                { "Admin", new AdminModule() },
                { "HR", new HRModule() },
                { "Inventory", new InventoryModule() },
                { "Sales", new SalesModule() },
                { "CRM", new CRMModule() }
            };
        }

        /// <summary>
        /// Hiển thị view tương ứng với menu item được chọn
        /// </summary>
        public void ShowView(string menuText)
        {
            ShowView(menuText, _currentModule);
        }

        /// <summary>
        /// Hiển thị view với module cụ thể
        /// </summary>
        public void ShowView(string viewName, string moduleName)
        {
            // Xóa view hiện tại
            _contentPanel.Controls.Clear();

            // Debug: Show what we're trying to load
            System.Diagnostics.Debug.WriteLine($"ViewManager.ShowView: viewName='{viewName}', moduleName='{moduleName}'");

            // Kiểm tra nếu click vào module cha
            if (_modules.ContainsKey(viewName))
            {
                _currentModule = viewName;
                ShowModuleHome(viewName);
                return;
            }

            // Hiển thị view của module
            if (!string.IsNullOrEmpty(moduleName) && _modules.ContainsKey(moduleName))
            {
                var module = _modules[moduleName];

                // Cho module xử lý menu item trước
                if (module.OnMenuItemClick(viewName))
                {
                    // Module đã xử lý riêng, không cần load view mặc định
                    return;
                }

                try
                {
                    UserControl view = module.GetView(viewName);
                    
                    if (view != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"ViewManager: Successfully loaded view for '{viewName}'");
                        view.Dock = DockStyle.Fill;
                        _contentPanel.Controls.Add(view);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"ViewManager: GetView returned null for '{viewName}'");
                        ShowDefaultView($"{moduleName} - {viewName}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ViewManager Error: {ex.Message}");
                    MessageBox.Show($"Lỗi khi load view: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShowDefaultView($"{moduleName} - {viewName}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"ViewManager: Module '{moduleName}' not found or empty");
                ShowDefaultView(viewName);
            }
        }

        private void ShowModuleHome(string moduleName)
        {
            Label lblTitle = new Label
            {
                Text = $"{moduleName} Module",
                Font = new System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(0, 120, 215),
                Location = new System.Drawing.Point(50, 50),
                AutoSize = true
            };

            Label lblDesc = new Label
            {
                Text = $"Chọn một mục từ menu bên trái",
                Font = new System.Drawing.Font("Segoe UI", 14),
                Location = new System.Drawing.Point(50, 100),
                AutoSize = true
            };

            _contentPanel.Controls.Add(lblTitle);
            _contentPanel.Controls.Add(lblDesc);
        }

        private void ShowDefaultView(string menuText)
        {
            Label lblDefault = new Label
            {
                Text = $"View for '{menuText}' is under construction...",
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.Gray,
                AutoSize = true,
                Location = new System.Drawing.Point(50, 50)
            };

            _contentPanel.Controls.Add(lblDefault);
        }

        public void SetCurrentModule(string moduleName)
        {
            _currentModule = moduleName;
        }
    }
}
