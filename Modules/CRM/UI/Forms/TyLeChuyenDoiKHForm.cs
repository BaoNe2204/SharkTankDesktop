using SharkTank.Modules.CRM.BLL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.CRM.UI.Forms
{
    public partial class TyLeChuyenDoiKHForm : UserControl
    {
        private TyLeChuyenDoiService service = new TyLeChuyenDoiService();

        private int percent = 0; // 🔥 dùng thật

        public TyLeChuyenDoiKHForm()
        {
            InitializeComponent();

        }

        private void TyLeChuyenDoiKHForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // ================= LOAD DATA =================
        private void LoadData()
        {
            var data = service.GetTyLeChuyenDoi();

            percent = (int)Math.Min(data.tyLe, 100);

            lblPercent.Text = percent + "%";
            lblDetail.Text = $"{data.daChot} / {data.tongLead} khách";

            progressBar1.Value = percent;

            // đổi màu
            if (percent < 30)
                lblPercent.ForeColor = Color.Red;
            else if (percent < 60)
                lblPercent.ForeColor = Color.Orange;
            else
                lblPercent.ForeColor = Color.Green;

            this.Invalidate(); // 🔥 vẽ lại vòng tròn
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        // ================= VẼ VÒNG TRÒN =================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(550, 180, 120, 120);

            // nền
            using (Pen gray = new Pen(Color.LightGray, 12))
                e.Graphics.DrawArc(gray, rect, 0, 360);

            // phần %
            using (Pen green = new Pen(lblPercent.ForeColor, 12))
                e.Graphics.DrawArc(green, rect, -90, (int)(360 * percent / 100.0));
        }

        private void lblDetail_Click(object sender, EventArgs e)
        {

        }
    }
}