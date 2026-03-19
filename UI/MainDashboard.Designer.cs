using SharkTank.BLL;
using SharkTank.DAL.Sql;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank
{
    partial class MainDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        class MyColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(220, 235, 250); }
            }

            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(0, 120, 215); }
            }
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _notificationService = new NotificationService(new SqlNotificationRepository());
            this.components = new System.ComponentModel.Container();
            this.panelContent = new System.Windows.Forms.Panel();
            this.PanelTop = new System.Windows.Forms.Panel();
            this.panelUserRight = new System.Windows.Forms.Panel();
            this.contextMenuUser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.picChevron = new System.Windows.Forms.PictureBox();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.picBell = new System.Windows.Forms.PictureBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.z80Navigation1 = new SharkTank.Z80_NavBar.Z80_Navigation();
            this.PanelTop.SuspendLayout();
            this.panelUserRight.SuspendLayout();
            this.contextMenuUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChevron)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBell)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Location = new System.Drawing.Point(260, 60);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1118, 606);
            this.panelContent.TabIndex = 0;
            this.panelContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContent_Paint);
            // 
            // PanelTop
            // 
            this.PanelTop.BackColor = System.Drawing.Color.White;
            this.PanelTop.Controls.Add(this.panelUserRight);
            this.PanelTop.Controls.Add(this.lblDate);
            this.PanelTop.Controls.Add(this.lblWelcome);
            this.PanelTop.Location = new System.Drawing.Point(260, 0);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(1118, 60);
            this.PanelTop.TabIndex = 0;
            this.PanelTop.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelTop_Paint);
            // 
            // panelUserRight
            // 
            this.panelUserRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUserRight.ContextMenuStrip = this.contextMenuUser;
            this.panelUserRight.Controls.Add(this.picChevron);
            this.panelUserRight.Controls.Add(this.lblUserRole);
            this.panelUserRight.Controls.Add(this.lblUserName);
            this.panelUserRight.Controls.Add(this.picAvatar);
            this.panelUserRight.Controls.Add(this.picBell);
            this.panelUserRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelUserRight.Location = new System.Drawing.Point(817, 8);
            this.panelUserRight.Name = "panelUserRight";
            this.panelUserRight.Size = new System.Drawing.Size(288, 44);
            this.panelUserRight.TabIndex = 3;
            this.panelUserRight.Click += new System.EventHandler(this.panelUserRight_Click);
            // 
            // contextMenuUser
            // 
            this.contextMenuUser.BackColor = System.Drawing.Color.White;
            this.contextMenuUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.contextMenuUser.ForeColor = System.Drawing.Color.Black;
            this.contextMenuUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProfile,
            this.mnuSettings,
            this.mnuLogout});
            this.contextMenuUser.Name = "contextMenuUser";
            this.contextMenuUser.Size = new System.Drawing.Size(128, 84);
            // 
            // mnuProfile
            // 
            this.mnuProfile.AutoSize = false;
            this.mnuProfile.Name = "mnuProfile";
            this.mnuProfile.Size = new System.Drawing.Size(116, 32);
            this.mnuProfile.Text = "Profile";
            this.mnuProfile.Click += new System.EventHandler(this.mnuProfile_Click);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(127, 24);
            this.mnuSettings.Text = "Settings";
            this.mnuSettings.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // mnuLogout
            // 
            this.mnuLogout.Name = "mnuLogout";
            this.mnuLogout.Size = new System.Drawing.Size(127, 24);
            this.mnuLogout.Text = "Logout";
            this.mnuLogout.Click += new System.EventHandler(this.mnuLogout_Click);
            // 
            // picChevron
            // 
            this.picChevron.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picChevron.BackColor = System.Drawing.Color.Transparent;
            this.picChevron.Location = new System.Drawing.Point(261, 14);
            this.picChevron.Name = "picChevron";
            this.picChevron.Size = new System.Drawing.Size(20, 20);
            this.picChevron.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picChevron.TabIndex = 4;
            this.picChevron.TabStop = false;
            this.picChevron.Click += new System.EventHandler(this.panelUserRight_Click);
            this.picChevron.Paint += new System.Windows.Forms.PaintEventHandler(this.picChevron_Paint);
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblUserRole.ForeColor = System.Drawing.Color.Gray;
            this.lblUserRole.Location = new System.Drawing.Point(80, 23);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(56, 13);
            this.lblUserRole.TabIndex = 3;
            this.lblUserRole.Text = "HR Office";
            this.lblUserRole.Click += new System.EventHandler(this.panelUserRight_Click);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoEllipsis = true;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.Black;
            this.lblUserName.Location = new System.Drawing.Point(80, 6);
            this.lblUserName.MaximumSize = new System.Drawing.Size(180, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(70, 17);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Otor John";
            this.lblUserName.Click += new System.EventHandler(this.panelUserRight_Click);
            // 
            // picAvatar
            // 
            this.picAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.picAvatar.ContextMenuStrip = this.contextMenuUser;
            this.picAvatar.Location = new System.Drawing.Point(38, 5);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(36, 36);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 1;
            this.picAvatar.TabStop = false;
            this.picAvatar.Click += new System.EventHandler(this.panelUserRight_Click);
            // 
            // picBell
            // 
            this.picBell.BackColor = System.Drawing.Color.Transparent;
            this.picBell.Image = global::SharkTank.Properties.Resources.bell_solid;
            this.picBell.Location = new System.Drawing.Point(3, 6);
            this.picBell.Name = "picBell";
            this.picBell.Size = new System.Drawing.Size(29, 35);
            this.picBell.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBell.TabIndex = 0;
            this.picBell.TabStop = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDate.ForeColor = System.Drawing.Color.Gray;
            this.lblDate.Location = new System.Drawing.Point(3, 37);
            this.lblDate.MaximumSize = new System.Drawing.Size(500, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(214, 15);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Today is Saturday, 12th November 2002";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.Black;
            this.lblWelcome.Location = new System.Drawing.Point(3, 10);
            this.lblWelcome.MaximumSize = new System.Drawing.Size(600, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(184, 21);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome, Mr. [Tên] 👋";
            this.lblWelcome.Click += new System.EventHandler(this.lblWelcome_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.panelLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLogo.Location = new System.Drawing.Point(-1, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(261, 74);
            this.panelLogo.TabIndex = 4;
            // 
            // z80Navigation1
            // 
            this.z80Navigation1.Location = new System.Drawing.Point(0, 66);
            this.z80Navigation1.Name = "z80Navigation1";
            this.z80Navigation1.Size = new System.Drawing.Size(260, 600);
            this.z80Navigation1.TabIndex = 3;
            // 
            // MainDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 667);
            this.Controls.Add(this.panelLogo);
            this.Controls.Add(this.z80Navigation1);
            this.Controls.Add(this.PanelTop);
            this.Controls.Add(this.panelContent);
            this.Name = "MainDashboard";
            this.Text = "SharkTank Dashboard";
            this.Load += new System.EventHandler(this.MainDashboard_Load);
            this.PanelTop.ResumeLayout(false);
            this.PanelTop.PerformLayout();
            this.panelUserRight.ResumeLayout(false);
            this.panelUserRight.PerformLayout();
            this.contextMenuUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picChevron)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBell)).EndInit();
            this.panelUserRight.Controls.Add(this.lblNotificationCount);

            this.ResumeLayout(false);
            this.contextMenuNotifications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblNotificationCount = new System.Windows.Forms.Label();

            this.lblNotificationCount.AutoSize = false;
            this.lblNotificationCount.BackColor = System.Drawing.Color.Red;
            this.lblNotificationCount.ForeColor = System.Drawing.Color.White;
            this.lblNotificationCount.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblNotificationCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNotificationCount.Location = new System.Drawing.Point(18, 2);
            this.lblNotificationCount.Name = "lblNotificationCount";
            this.lblNotificationCount.Size = new System.Drawing.Size(16, 16);
            this.lblNotificationCount.TabIndex = 5;
            this.lblNotificationCount.Text = "0";
            this.lblNotificationCount.Visible = false;

            this.picBell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBell.Click += new System.EventHandler(this.picBell_Click);


        }

        #endregion
        private System.Windows.Forms.Label lblNotificationCount;
        private System.Windows.Forms.ContextMenuStrip contextMenuNotifications;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel PanelTop;
        private System.Windows.Forms.Panel panelUserRight;
        private System.Windows.Forms.PictureBox picBell;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.PictureBox picChevron;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.ContextMenuStrip contextMenuUser;
        private System.Windows.Forms.ToolStripMenuItem mnuProfile;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuLogout;
        private System.Windows.Forms.Panel panelLogo;
        private SharkTank.Z80_NavBar.Z80_Navigation z80Navigation1;
    }
}
