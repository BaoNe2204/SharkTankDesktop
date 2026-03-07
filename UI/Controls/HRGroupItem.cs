using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.UI.Controls
{
    public class HRGroupItem : Panel
    {
        private readonly Label _lblTitle;
        private readonly Panel _subPanel;
        private bool _isExpanded;

        public event EventHandler Expanded;

        public HRGroupItem(string title)
        {
            Dock = DockStyle.Top;
            AutoSize = true;

            _lblTitle = new Label
            {
                Text = "   " + title,
                Height = 45,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(240, 244, 250),
                Cursor = Cursors.Hand
            };
            _lblTitle.Click += Toggle;

            _subPanel = new Panel
            {
                Dock = DockStyle.Top,
                Visible = false,
                AutoSize = true
            };

            Controls.Add(_subPanel);
            Controls.Add(_lblTitle);
        }

        private void Toggle(object sender, EventArgs e)
        {
            _isExpanded = !_isExpanded;
            _subPanel.Visible = _isExpanded;
            if (_isExpanded)
            {
                Expanded?.Invoke(this, EventArgs.Empty);
            }
        }

        public void AddSubItem(string text)
        {
            var lbl = new Label
            {
                Text = "      - " + text,
                Height = 35,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(80, 80, 80),
                Cursor = Cursors.Hand
            };

            lbl.MouseEnter += (s, e) => lbl.BackColor = Color.FromArgb(230, 235, 245);
            lbl.MouseLeave += (s, e) => lbl.BackColor = Color.Transparent;

            _subPanel.Controls.Add(lbl);
            _subPanel.Controls.SetChildIndex(lbl, 0);
        }

        public void Collapse()
        {
            _isExpanded = false;
            _subPanel.Visible = false;
        }
    }
}

