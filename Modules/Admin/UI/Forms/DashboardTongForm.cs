using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class DashboardTongForm : UserControl
    {
        private readonly string _connStr;
        private readonly List<BarData> _barData = new List<BarData>();
        private readonly List<PieData> _pieData = new List<PieData>();

        // ── Cached panel references for painting ──
        private Panel _cachedBarChart;
        private Panel _cachedPieChart;
        private FlowLayoutPanel _cachedFlowCards;

        public DashboardTongForm()
        {
            _connStr = System.Configuration.ConfigurationManager
                .ConnectionStrings["SharkTankDB"]?.ConnectionString
                ?? @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SharkTankERP;Integrated Security=True;TrustServerCertificate=True";

            InitializeComponent();
            _cachedBarChart = pnlBarChart;
            _cachedPieChart = pnlPieChart;
            _cachedFlowCards = flowCards;

            pnlBarChart.Paint += PnlBarChart_Paint;
            pnlPieChart.Paint += PnlPieChart_Paint;

            UpdateClock();
            LoadDashboard();
        }

        public void LoadDashboard()
        {
            LoadKPICards();
            LoadBarChartData();
            LoadPieChartData();
            LoadActivityLog();
            pnlBarChart.Invalidate();
            pnlPieChart.Invalidate();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboard();
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // ══════════════════════════════════════════════════════════
        // 1. KPI CARDS
        // ══════════════════════════════════════════════════════════
        private void LoadKPICards()
        {
            // Nếu trước đó đã bấm "Cập nhật" khi code còn tạo card động
            // thì FlowLayoutPanel có thể đang không chứa card0..card5 (designer).
            // Ép đưa lại đúng card template từ Designer để UI giống design nhất.
            if (!_cachedFlowCards.Controls.Contains(card0))
            {
                _cachedFlowCards.SuspendLayout();
                _cachedFlowCards.Controls.Clear();
                _cachedFlowCards.Controls.Add(card0);
                _cachedFlowCards.Controls.Add(card1);
                _cachedFlowCards.Controls.Add(card2);
                _cachedFlowCards.Controls.Add(card3);
                _cachedFlowCards.Controls.Add(card4);
                _cachedFlowCards.Controls.Add(card5);
                _cachedFlowCards.ResumeLayout();
            }

            var values = GetKPIValues();
            Label[] valueLabels = new[] { val0, val1, val2, val3, val4, val5 };
            for (int i = 0; i < valueLabels.Length; i++)
            {
                valueLabels[i].Text = i < values.Count ? values[i] : "0";
            }
        }

        private List<string> GetKPIValues()
        {
            var r = new List<string>();
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    var q = @"
                        SELECT
                            (SELECT COUNT(*) FROM Users WHERE IsActive = 1) AS NV,
                            (SELECT COUNT(*) FROM PhongBan) AS PB,
                            (SELECT COUNT(*) FROM ChucVu) AS CV,
                            (SELECT COUNT(*) FROM LoginHistory
                              WHERE MONTH(LoginTime) = MONTH(GETDATE())
                              AND YEAR(LoginTime) = YEAR(GETDATE())) AS [LOGIN_THANG],
                            (SELECT COUNT(*) FROM Users WHERE IsLocked = 1) AS [USER_KHOA],
                            (SELECT COUNT(*) FROM AuditLogs) AS AUDIT";
                    using (var cmd = new SqlCommand(q, conn))
                    using (var dr = cmd.ExecuteReader())
                        if (dr.Read())
                        {
                            r.Add(ToStr(dr["NV"]));
                            r.Add(ToStr(dr["PB"]));
                            r.Add(ToStr(dr["CV"]));
                            r.Add(ToStr(dr["LOGIN_THANG"]));
                            r.Add(ToStr(dr["USER_KHOA"]));
                            r.Add(ToStr(dr["AUDIT"]));
                        }
                }
            }
            catch { /* DB offline */ }

            while (r.Count < 6)
                r.Add("0");
            return r;
        }

        private Panel MakeCard(string icon, string value, string sub, Color accent)
        {
            var p = new Panel
            {
                Size = new Size(154, 90),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 15, 0)
            };
            var accentBar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 5,
                BackColor = accent
            };
            var ico = new Label
            {
                Text = icon,
                Location = new Point(20, 12),
                Font = new Font("Segoe UI", 16F),
                AutoSize = false,
                Size = new Size(35, 30)
            };
            var valLbl = new Label
            {
                Text = value,
                Location = new Point(20, 35),
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = false,
                Size = new Size(131, 30)
            };
            var subLbl = new Label
            {
                Text = sub,
                Location = new Point(20, 70),
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = false,
                Size = new Size(131, 18)
            };
                p.Controls.AddRange(new Control[] { accentBar, ico, valLbl, subLbl });
            return p;
        }

        // ══════════════════════════════════════════════════════════
        // 2. BAR CHART  —  Doanh thu theo tháng
        // ══════════════════════════════════════════════════════════
        private void LoadBarChartData()
        {
            _barData.Clear();
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    // Doanh thu = số audit log theo tháng (thay HoaDon vì DB chưa có bảng đó)
                    var q = @"
                        SELECT TOP 12
                            FORMAT(a.Timestamp,'MMM') AS Thang,
                            YEAR(a.Timestamp) AS Nam,
                            COUNT(*) AS SoLuong
                        FROM AuditLogs a
                        GROUP BY YEAR(a.Timestamp), MONTH(a.Timestamp), FORMAT(a.Timestamp,'MMM')
                        ORDER BY YEAR(a.Timestamp), MONTH(a.Timestamp)";
                    using (var cmd = new SqlCommand(q, conn))
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                            _barData.Add(new BarData(
                                $"{dr["Thang"]} {dr["Nam"]}",
                                dr["SoLuong"] != DBNull.Value ? Convert.ToDouble(dr["SoLuong"]) : 0));
                }
            }
            catch
            {
                // Keep empty, UI will show "Không có dữ liệu".
            }
            if (_barData.Count == 0)
                _barData.Add(new BarData("Không có dữ liệu", 0));
        }

        private void PnlBarChart_Paint(object sender, PaintEventArgs e)
        {
            if (_cachedBarChart == null) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int W = _cachedBarChart.Width;
            int H = _cachedBarChart.Height;
            if (W <= 10 || H <= 10) return;

            int padLeft = 60, padRight = 15, padTop = 15, padBot = 30;
            int chartW = W - padLeft - padRight;
            int chartH = H - padTop - padBot;

            double maxVal = 1;
            foreach (var d in _barData)
                if (d.Value > maxVal) maxVal = d.Value;
            if (maxVal == 0) maxVal = 1;

            // Grid lines
            using (var pen = new Pen(Color.FromArgb(230, 232, 235), 1))
            {
                for (int i = 0; i <= 4; i++)
                {
                    int y = padTop + chartH * i / 4;
                    g.DrawLine(pen, padLeft, y, W - padRight, y);
                    double val = maxVal * (4 - i) / 4;
                    var lbl = new Label
                    {
                        Text = val < 1000 ? val.ToString("N0")
                                : (val / 1000000).ToString("0.#") + "M",
                        Font = new Font("Segoe UI", 7F),
                        ForeColor = Color.FromArgb(127, 140, 141)
                    };
                    using (var bg = new Bitmap(60, 16))
                    using (var tg = Graphics.FromImage(bg))
                    {
                        tg.Clear(Color.Transparent);
                        tg.DrawString(lbl.Text, lbl.Font, new SolidBrush(lbl.ForeColor),
                            new PointF(0, 0));
                        g.DrawImage(bg, padLeft - 58, y - 8);
                    }
                }
            }

            // Bars
            int n = _barData.Count;
            if (n == 0) return;
            int gapX = Math.Max(4, chartW / (n * 5));
            int barW = (chartW - gapX * (n + 1)) / n;
            if (barW < 4) barW = 4;

            Color[] barColors = new Color[]
            {
                Color.FromArgb(52, 152, 219),
                Color.FromArgb(40, 167, 69),
                Color.FromArgb(253, 130, 8),
                Color.FromArgb(155, 89, 182),
                Color.FromArgb(220, 53, 69),
                Color.FromArgb(23, 162, 184),
                Color.FromArgb(241, 196, 15),
                Color.FromArgb(52, 73, 94),
                Color.FromArgb(46, 204, 113),
                Color.FromArgb(230, 126, 34),
                Color.FromArgb(155, 89, 182),
                Color.FromArgb(26, 188, 156),
            };

            for (int i = 0; i < n; i++)
            {
                var d = _barData[i];
                int x = padLeft + gapX + i * (barW + gapX);
                int barH = (int)(d.Value / maxVal * chartH);
                if (barH < 2) barH = 2;
                int y = padTop + chartH - barH;

                using (var br = new LinearGradientBrush(
                    new Rectangle(x, y, barW, barH),
                    barColors[i % barColors.Length],
                    Color.FromArgb(180, barColors[i % barColors.Length]),
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(br, x, y, barW, barH);
                }

                // Value label on top of bar
                if (barH > 18)
                {
                    var f = new Font("Segoe UI", 7F, FontStyle.Bold);
                    var txt = d.Value < 1000 ? d.Value.ToString("N0")
                               : (d.Value / 1000000).ToString("0.#") + "M";
                    using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                        g.DrawString(txt, f,
                            new SolidBrush(Color.FromArgb(44, 62, 80)),
                            new RectangleF(x, y - 14, barW, 14), sf);
                }

                // X label
                var xLbl = new Font("Segoe UI", 7F);
                using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                    g.DrawString(d.Label, xLbl,
                        new SolidBrush(Color.FromArgb(100, 100, 100)),
                        new RectangleF(x - gapX / 2, padTop + chartH + 4, barW + gapX, 18), sf);
            }
        }

        // ══════════════════════════════════════════════════════════
        // 3. PIE CHART  —  Sản phẩm theo danh mục
        // ══════════════════════════════════════════════════════════
        private void LoadPieChartData()
        {
            _pieData.Clear();
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    // Pie chart: Users theo vai trò (Roles)
                    var q = @"
                        SELECT TOP 6
                            r.RoleName AS Ten,
                            COUNT(u.UserId) AS SoLuong
                        FROM Users u
                        INNER JOIN Roles r ON u.RoleId = r.RoleId
                        GROUP BY r.RoleName
                        ORDER BY SoLuong DESC";
                    using (var cmd = new SqlCommand(q, conn))
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                            _pieData.Add(new PieData(
                                dr["Ten"].ToString(),
                                dr["SoLuong"] != DBNull.Value ? Convert.ToInt32(dr["SoLuong"]) : 0));
                }
            }
            catch { /* silent */ }

            if (_pieData.Count == 0)
            {
                _pieData.Add(new PieData("Không có dữ liệu", 1));
            }
        }

        private void PnlPieChart_Paint(object sender, PaintEventArgs e)
        {
            if (_cachedPieChart == null) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int W = _cachedPieChart.Width;
            int H = _cachedPieChart.Height;
            if (W <= 10 || H <= 10) return;

            int total = 0;
            foreach (var d in _pieData) total += d.Value;
            if (total == 0) total = 1;

            int legendW = Math.Min(130, W / 3);
            int pieSize = Math.Min(H - 10, W - legendW - 20);
            int cx = (W - legendW - pieSize) / 2 + pieSize / 2;
            int cy = H / 2;
            int r = pieSize / 2 - 10;
            if (r < 10) r = 10;

            Color[] pieColors = new Color[]
            {
                Color.FromArgb(52, 152, 219),
                Color.FromArgb(40, 167, 69),
                Color.FromArgb(253, 130, 8),
                Color.FromArgb(155, 89, 182),
                Color.FromArgb(220, 53, 69),
                Color.FromArgb(23, 162, 184),
            };

            float startAngle = -90;
            for (int i = 0; i < _pieData.Count; i++)
            {
                float sweepAngle = 360f * _pieData[i].Value / total;
                using (var br = new SolidBrush(pieColors[i % pieColors.Length]))
                {
                    g.FillPie(br, cx - r, cy - r, r * 2, r * 2, startAngle, sweepAngle);
                }
                // White separator
                using (var pen = new Pen(Color.White, 2))
                {
                    g.DrawPie(pen, cx - r, cy - r, r * 2, r * 2, startAngle, sweepAngle);
                }
                startAngle += sweepAngle;
            }

            // Donut hole
            int inner = (int)(r * 0.5);
            using (var br = new SolidBrush(Color.White))
            {
                g.FillEllipse(br, cx - inner, cy - inner, inner * 2, inner * 2);
            }
            var centerVal = new Font("Segoe UI", 11F, FontStyle.Bold);
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
            {
                g.DrawString(total.ToString(), centerVal,
                    new SolidBrush(Color.FromArgb(44, 62, 80)),
                    new RectangleF(cx - inner, cy - 12, inner * 2, 16), sf);
            }

            // Legend
            int lx = cx + r + 10;
            if (lx + legendW > W) lx = W - legendW - 5;
            int ly = 10;
            for (int i = 0; i < _pieData.Count; i++)
            {
                var d = _pieData[i];
                using (var br = new SolidBrush(pieColors[i % pieColors.Length]))
                {
                    g.FillRectangle(br, lx, ly + i * 22, 12, 12);
                }
                var pct = total > 0 ? (100 * d.Value / total) : 0;
                var lbl = $"{d.Label}  {pct}%";
                var lblFont = new Font("Segoe UI", 8F);
                using (var sf = new StringFormat())
                    g.DrawString(lbl, lblFont,
                        new SolidBrush(Color.FromArgb(44, 62, 80)),
                        new RectangleF(lx + 16, ly + i * 22 - 2, legendW - 16, 18), sf);
            }
        }

        // ══════════════════════════════════════════════════════════
        // 4. RECENT ACTIVITY TABLE
        // ══════════════════════════════════════════════════════════
        private void LoadActivityLog()
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    // Ghép LoginHistory + AuditLogs
                    var q = @"
                        SELECT TOP 10
                            CASE
                              WHEN t.Kieu = 'LOGIN' THEN N'🔑 Đăng nhập'
                              WHEN t.Kieu = 'AUDIT' THEN N'📋 ' + t.Action
                              ELSE N'📝 ' + t.EntityType
                            END AS [Hành động],
                            ISNULL(t.Username, N'System') AS [Người thực hiện],
                            ISNULL(FORMAT(t.ThoiGian,'dd/MM HH:mm'), '') AS [Thời gian],
                            ISNULL(t.MoTa, N'Thay đổi dữ liệu') AS [Chi tiết]
                        FROM (
                            SELECT 'LOGIN' AS Kieu, lh.LoginTime AS ThoiGian,
                                   lh.Status AS MoTa, lh.Username, NULL AS Action, NULL AS EntityType
                            FROM LoginHistory lh
                            UNION ALL
                            SELECT 'AUDIT', a.Timestamp, a.Description, a.Username, a.Action, a.EntityType
                            FROM AuditLogs a
                        ) t
                        ORDER BY t.ThoiGian DESC";
                    var da = new SqlDataAdapter(q, conn);
                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvActivity.DataSource = dt;
                }
            }
            catch
            {
                var dt = new DataTable();
                dt.Columns.Add("Hành động");
                dt.Columns.Add("Người thực hiện");
                dt.Columns.Add("Thời gian");
                dt.Columns.Add("Chi tiết");
                dt.Rows.Add("Không có dữ liệu", "System", "", "Không đọc được dữ liệu từ CSDL");
                dgvActivity.DataSource = dt;
            }
        }

        // ══════════════════════════════════════════════════════════
        // HELPERS
        // ══════════════════════════════════════════════════════════
        private static string ToStr(object o)
        {
            if (o == null || o == DBNull.Value) return "0";
            if (o is int i) return i.ToString("N0");
            if (o is long l) return l.ToString("N0");
            if (o is decimal d) return d.ToString("N0");
            return o.ToString();
        }

        private struct BarData
        {
            public string Label { get; }
            public double Value { get; }
            public BarData(string label, double value) { Label = label; Value = value; }
        }

        private struct PieData
        {
            public string Label { get; }
            public int Value { get; }
            public PieData(string label, int value) { Label = label; Value = value; }
        }

        private void acc5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
