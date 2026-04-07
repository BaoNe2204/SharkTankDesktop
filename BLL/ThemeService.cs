using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SharkTank.DAL.Sql;

namespace SharkTank.BLL
{
    /// <summary>
    /// Dịch vụ đọc SystemConfigs từ SQL và cung cấp giá trị cho toàn app.
    /// Khởi tạo 1 lần trong Program.cs / MainDashboard, dùng chung everywhere.
    /// </summary>
    public class ThemeService
    {
        private static ThemeService _instance;
        private readonly Dictionary<string, string> _configs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public string AppName { get; private set; } = "SharkTank ERP";
        public string AppVersion { get; private set; } = "1.0.0";
        public string DefaultLanguage { get; private set; } = "vi-VN";
        public bool AutoBackup { get; private set; } = true;
        public int BackupInterval { get; private set; } = 7;

        // Security
        public int PasswordMinLength { get; private set; } = 8;
        public bool PasswordRequireUppercase { get; private set; } = true;
        public bool PasswordRequireLowercase { get; private set; } = true;
        public bool PasswordRequireDigit { get; private set; } = true;
        public bool PasswordRequireSpecial { get; private set; } = false;
        public int PasswordExpiryDays { get; private set; } = 90;
        public bool LockAccountOnFail { get; private set; } = true;
        public int MaxLoginAttempts { get; private set; } = 5;

        // Display / Theme
        public string ThemeColor { get; private set; } = "Blue";
        public string ThemeStyle { get; private set; } = "Modern";
        public bool ShowUsername { get; private set; } = true;
        public bool ShowAvatar { get; private set; } = true;
        public int FontSize { get; private set; } = 10;

        // Email
        public string SmtpServer { get; private set; } = "smtp.gmail.com";
        public int SmtpPort { get; private set; } = 587;
        public string SmtpEncryption { get; private set; } = "TLS";
        public string EmailFrom { get; private set; } = "";
        public string EmailUsername { get; private set; } = "";
        public string EmailPassword { get; private set; } = "";
        public bool UseDefaultCredentials { get; private set; } = true;

        // Format
        public string DefaultCurrency { get; private set; } = "VND";
        public int DecimalPlaces { get; private set; } = 2;
        public string ThousandSeparator { get; private set; } = ",";
        public string DecimalSeparator { get; private set; } = ".";
        public string DateFormat { get; private set; } = "dd/MM/yyyy";
        public string TimeFormat { get; private set; } = "HH:mm:ss";
        public string WeekStartDay { get; private set; } = "Monday";
        public string TimeZone { get; private set; } = "UTC+07:00";
        public string NumberDecimalSeparator { get; private set; } = ".";
        public string NumberThousandSeparator { get; private set; } = ",";
        public bool AutoRoundNumbers { get; private set; } = true;
        public bool ShowCurrencySymbol { get; private set; } = true;
        public bool UseTimeZone { get; private set; } = true;

        public static ThemeService Instance => _instance ?? (_instance = new ThemeService());

        /// <summary>Sự kiện bắn khi theme/font thay đổi → MainDashboard subscribe để ApplyTheme() ngay.</summary>
        public event Action OnThemeChanged;

        private ThemeService()
        {
            LoadFromDatabase();
        }

        /// <summary>Gọi sau khi Lưu display config → bắn OnThemeChanged → MainDashboard ApplyTheme().</summary>
        public void Reload()
        {
            LoadFromDatabase();
            OnThemeChanged?.Invoke();
        }

        public void LoadFromDatabase()
        {
            if (!SqlConnectionFactory.HasConnectionString())
                return;

            try
            {
                var repo = new SqlSystemConfigRepository(SqlConnectionFactory.Create());
                var all = repo.GetAll().ToList();
                _configs.Clear();
                foreach (var c in all)
                    _configs[c.ConfigKey] = c.ConfigValue ?? "";

                // General
                AppName = Get("AppName", "SharkTank ERP");
                AppVersion = Get("AppVersion", "1.0.0");
                DefaultLanguage = Get("DefaultLanguage", "vi-VN");
                AutoBackup = GetBool("AutoBackup", true);
                BackupInterval = GetInt("BackupInterval", 7);

                // Security
                PasswordMinLength = GetInt("PasswordMinLength", 8);
                PasswordRequireUppercase = GetBool("PasswordRequireUppercase", true);
                PasswordRequireLowercase = GetBool("PasswordRequireLowercase", true);
                PasswordRequireDigit = GetBool("PasswordRequireDigit", true);
                PasswordRequireSpecial = GetBool("PasswordRequireSpecial", false);
                PasswordExpiryDays = GetInt("PasswordExpiryDays", 90);
                LockAccountOnFail = GetBool("LockAccountOnFail", true);
                MaxLoginAttempts = GetInt("MaxLoginAttempts", 5);

                // Display
                ThemeColor = Get("ThemeColor", "Blue");
                ThemeStyle = Get("ThemeStyle", "Modern");
                ShowUsername = GetBool("ShowUsername", true);
                ShowAvatar = GetBool("ShowAvatar", true);
                FontSize = Math.Max(8, Math.Min(24, GetInt("FontSize", 10)));

                // Email
                SmtpServer = Get("SmtpServer", "smtp.gmail.com");
                SmtpPort = GetInt("SmtpPort", 587);
                SmtpEncryption = Get("SmtpEncryption", "TLS");
                EmailFrom = Get("EmailFrom", "");
                EmailUsername = Get("EmailUsername", "");
                EmailPassword = Get("EmailPassword", "");
                UseDefaultCredentials = GetBool("UseDefaultCredentials", true);

                // Format
                DefaultCurrency = Get("DefaultCurrency", "VND");
                DecimalPlaces = GetInt("DecimalPlaces", 2);
                ThousandSeparator = Get("ThousandSeparator", ",");
                DecimalSeparator = Get("DecimalSeparator", ".");
                DateFormat = Get("DateFormat", "dd/MM/yyyy");
                TimeFormat = Get("TimeFormat", "HH:mm:ss");
                WeekStartDay = Get("WeekStartDay", "Monday");
                TimeZone = Get("TimeZone", "UTC+07:00");
                NumberDecimalSeparator = Get("NumberDecimalSeparator", ".");
                NumberThousandSeparator = Get("NumberThousandSeparator", ",");
                AutoRoundNumbers = GetBool("AutoRoundNumbers", true);
                ShowCurrencySymbol = GetBool("ShowCurrencySymbol", true);
                UseTimeZone = GetBool("UseTimeZone", true);
            }
            catch
            {
                // Nếu DB chưa có bảng/config → dùng mặc định
            }
        }

        public string Get(string key, string defaultValue = "")
            => _configs.TryGetValue(key, out var v) && !string.IsNullOrEmpty(v) ? v : defaultValue;

        public int GetInt(string key, int defaultValue = 0)
            => int.TryParse(Get(key), out var v) ? v : defaultValue;

        public bool GetBool(string key, bool defaultValue = false)
        {
            if (!_configs.TryGetValue(key, out var v) || string.IsNullOrWhiteSpace(v))
                return defaultValue;

            if (v == "1" || v.Equals("true", StringComparison.OrdinalIgnoreCase) || v.Equals("yes", StringComparison.OrdinalIgnoreCase))
                return true;

            if (v == "0" || v.Equals("false", StringComparison.OrdinalIgnoreCase) || v.Equals("no", StringComparison.OrdinalIgnoreCase))
                return false;

            return defaultValue;
        }

        /// <summary>Định dạng số tiền theo cấu hình hiện tại.</summary>
        public string FormatCurrency(decimal amount)
        {
            string symbol = "";
            switch (DefaultCurrency)
            {
                case "VND": symbol = "₫"; break;
                case "USD": symbol = "$"; break;
                case "EUR": symbol = "€"; break;
                case "JPY": symbol = "¥"; break;
            }

            string formatted = amount.ToString($"N{DecimalPlaces}",
                new NumberFormatInfo
                {
                    NumberDecimalSeparator = DecimalSeparator,
                    NumberGroupSeparator = ThousandSeparator
                });

            return ShowCurrencySymbol ? $"{symbol} {formatted}" : formatted;
        }

        /// <summary>Định dạng ngày theo cấu hình hiện tại.</summary>
        public string FormatDate(DateTime dt)
        {
            try { return dt.ToString(DateFormat); }
            catch { return dt.ToString("dd/MM/yyyy"); }
        }

        /// <summary>Định dạng ngày-giờ theo cấu hình hiện tại.</summary>
        public string FormatDateTime(DateTime dt)
        {
            try { return dt.ToString($"{DateFormat} {TimeFormat}"); }
            catch { return dt.ToString("dd/MM/yyyy HH:mm:ss"); }
        }

        /// <summary>Font mặc định theo FontSize cấu hình.</summary>
        public Font GetDefaultFont()
        {
            return new Font("Segoe UI", FontSize, FontStyle.Regular);
        }

        /// <summary>Font Bold theo FontSize cấu hình.</summary>
        public Font GetDefaultFontBold()
        {
            return new Font("Segoe UI", FontSize, FontStyle.Bold);
        }

        /// <summary>Màu nền thanh trái (logo + nav). Mặc định #282d33 như thiết kế SharkTank.</summary>
        public Color GetThemeBackColor()
        {
            switch (ThemeColor)
            {
                case "Dark": return Color.FromArgb(30, 30, 30);
                case "Light": return Color.FromArgb(245, 245, 245);
                case "Green": return Color.FromArgb(240, 248, 240);
                default: return Color.FromArgb(40, 45, 51); // Xanh xám đậm #282d33 (mặc định)
            }
        }

        /// <summary>Màu accent theo theme.</summary>
        public Color GetAccentColor()
        {
            switch (ThemeColor)
            {
                case "Dark": return Color.FromArgb(0, 120, 215);
                case "Light": return Color.FromArgb(0, 120, 215);
                case "Green": return Color.FromArgb(40, 167, 69);
                default: return Color.FromArgb(0, 120, 215); // Blue
            }
        }

        /// <summary>Nền vùng nội dung chính (panelContent + module views).</summary>
        public Color GetWorkspaceBackColor()
        {
            switch (ThemeColor)
            {
                case "Dark": return Color.FromArgb(40, 40, 43);
                case "Light": return Color.White;
                case "Green": return Color.FromArgb(245, 252, 245);
                default: return Color.FromArgb(248, 248, 250);
            }
        }

        /// <summary>Màu chữ trong vùng nội dung.</summary>
        public Color GetWorkspaceForeColor()
        {
            switch (ThemeColor)
            {
                case "Dark": return Color.FromArgb(230, 230, 230);
                case "Light": return Color.FromArgb(30, 30, 30);
                case "Green": return Color.FromArgb(20, 70, 30);
                default: return Color.FromArgb(30, 30, 30);
            }
        }

        /// <summary>Thanh header (PanelTop). Mặc định Blue + Light: nền trắng như thiết kế; Dark: nền tối.</summary>
        public Color GetHeaderBackColor()
        {
            switch (ThemeColor)
            {
                case "Dark": return Color.FromArgb(45, 45, 48);
                case "Light": return Color.White;
                default: return Color.White;
            }
        }

        /// <summary>Chữ chính trên header (lời chào, tên user).</summary>
        public Color GetHeaderForeColor()
        {
            switch (ThemeColor)
            {
                case "Dark": return Color.WhiteSmoke;
                case "Light": return Color.FromArgb(40, 40, 40);
                case "Green": return Color.FromArgb(20, 90, 35);
                default: return Color.FromArgb(40, 40, 40);
            }
        }

        /// <summary>Ngày / dòng phụ trên header (xám vừa, nền trắng mặc định).</summary>
        public Color GetHeaderSubtleForeColor()
        {
            switch (ThemeColor)
            {
                case "Dark": return Color.FromArgb(160, 160, 165);
                case "Light": return Color.FromArgb(115, 115, 115);
                case "Green": return Color.FromArgb(80, 120, 90);
                default: return Color.FromArgb(115, 115, 115);
            }
        }

        /// <summary>Màu role (Admin) dưới tên user.</summary>
        public Color GetHeaderRoleForeColor()
        {
            switch (ThemeColor)
            {
                case "Dark": return Color.FromArgb(180, 180, 185);
                case "Green": return Color.FromArgb(70, 110, 80);
                default: return Color.DimGray;
            }
        }

        /// <summary>Viền đáy PanelTop (chia tách vùng nội dung).</summary>
        public Color GetHeaderBottomBorderColor()
        {
            switch (ThemeColor)
            {
                case "Dark": return Color.FromArgb(60, 60, 65);
                case "Green": return Color.FromArgb(210, 225, 215);
                default: return Color.FromArgb(230, 230, 232);
            }
        }

        /// <summary>Mép trên vài pixel — cùng nền header để che vệt hệ thống, không tạo sọc tối trên nền trắng.</summary>
        public Color GetHeaderTopStripColor() => GetHeaderBackColor();

        /// <summary>Áp dụng nền/chữ cho panel nội dung và toàn bộ UserControl con (đệ quy).</summary>
        public void ApplyWorkspaceTheme(Control root)
        {
            if (root == null) return;

            var bg = GetWorkspaceBackColor();
            var fg = GetWorkspaceForeColor();

            void Visit(Control c)
            {
                if (c is DataGridView)
                    return;

                if (c is PictureBox pb && pb.Image != null)
                {
                    // Giữ nền trong suốt cho icon
                    return;
                }

                if (c is Button || c is ComboBox || c is TextBox || c is NumericUpDown || c is DateTimePicker || c is MaskedTextBox)
                    return;

                if (c is CheckBox chk)
                {
                    chk.ForeColor = fg;
                    foreach (Control ch in c.Controls)
                        Visit(ch);
                    return;
                }

                if (c is RadioButton rb)
                {
                    rb.ForeColor = fg;
                    return;
                }

                if (c is Label lbl)
                {
                    // Giữ badge thông báo đỏ
                    if (lbl.Name == "lblNotificationCount")
                        return;
                    lbl.ForeColor = fg;
                    return;
                }

                if (c is Panel || c is FlowLayoutPanel || c is TableLayoutPanel || c is SplitContainer || c is UserControl)
                {
                    c.BackColor = bg;
                }
                else if (c is GroupBox gb)
                {
                    gb.BackColor = bg;
                    gb.ForeColor = fg;
                }
                else if (c is TabControl)
                {
                    c.BackColor = bg;
                }
                else if (c is TabPage tp)
                {
                    tp.BackColor = bg;
                    tp.ForeColor = fg;
                }

                foreach (Control child in c.Controls)
                    Visit(child);
            }

            root.BackColor = bg;
            foreach (Control child in root.Controls)
                Visit(child);
        }
    }
}
