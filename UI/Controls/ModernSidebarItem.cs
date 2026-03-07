using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.UI.Controls
{
    public class ModernSidebarItem : Panel
    {
        private readonly Label _lbl;

        public string ItemText
        {
            get => _lbl.Text;
            set => _lbl.Text = value;
        }

        public ModernSidebarItem(string text)
        {
            Height = 48;
            Dock = DockStyle.Top;
            Cursor = Cursors.Hand;
            BackColor = Color.Transparent;

            _lbl = new Label
            {
                Text = text,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            Controls.Add(_lbl);

            MouseEnter += (s, e) => BackColor = Color.FromArgb(235, 240, 250);
            MouseLeave += (s, e) => BackColor = Color.Transparent;

            _lbl.MouseEnter += (s, e) => BackColor = Color.FromArgb(235, 240, 250);
            _lbl.MouseLeave += (s, e) => BackColor = Color.Transparent;
        }
    }
}

