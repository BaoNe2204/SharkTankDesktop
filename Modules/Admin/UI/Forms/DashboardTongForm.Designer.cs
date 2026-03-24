using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Admin.UI.Forms
{
    partial class DashboardTongForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.card0 = new System.Windows.Forms.Panel();
            this.acc0 = new System.Windows.Forms.Panel();
            this.ico0 = new System.Windows.Forms.Label();
            this.val0 = new System.Windows.Forms.Label();
            this.sub0 = new System.Windows.Forms.Label();
            this.card1 = new System.Windows.Forms.Panel();
            this.acc1 = new System.Windows.Forms.Panel();
            this.ico1 = new System.Windows.Forms.Label();
            this.val1 = new System.Windows.Forms.Label();
            this.sub1 = new System.Windows.Forms.Label();
            this.card2 = new System.Windows.Forms.Panel();
            this.acc2 = new System.Windows.Forms.Panel();
            this.ico2 = new System.Windows.Forms.Label();
            this.val2 = new System.Windows.Forms.Label();
            this.sub2 = new System.Windows.Forms.Label();
            this.card3 = new System.Windows.Forms.Panel();
            this.acc3 = new System.Windows.Forms.Panel();
            this.ico3 = new System.Windows.Forms.Label();
            this.val3 = new System.Windows.Forms.Label();
            this.sub3 = new System.Windows.Forms.Label();
            this.card4 = new System.Windows.Forms.Panel();
            this.acc4 = new System.Windows.Forms.Panel();
            this.ico4 = new System.Windows.Forms.Label();
            this.val4 = new System.Windows.Forms.Label();
            this.sub4 = new System.Windows.Forms.Label();
            this.card5 = new System.Windows.Forms.Panel();
            this.acc5 = new System.Windows.Forms.Panel();
            this.ico5 = new System.Windows.Forms.Label();
            this.val5 = new System.Windows.Forms.Label();
            this.sub5 = new System.Windows.Forms.Label();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.pnlLeftChart = new System.Windows.Forms.Panel();
            this.pnlBarChart = new System.Windows.Forms.Panel();
            this.pnlChartHeader1 = new System.Windows.Forms.Panel();
            this.lblChartTitle1 = new System.Windows.Forms.Label();
            this.pnlRightChart = new System.Windows.Forms.Panel();
            this.pnlPieChart = new System.Windows.Forms.Panel();
            this.pnlChartHeader2 = new System.Windows.Forms.Panel();
            this.lblChartTitle2 = new System.Windows.Forms.Label();
            this.pnlActivity = new System.Windows.Forms.Panel();
            this.pnlActInner = new System.Windows.Forms.Panel();
            this.dgvActivity = new System.Windows.Forms.DataGridView();
            this.actHeader = new System.Windows.Forms.Panel();
            this.lblActTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.flowCards.SuspendLayout();
            this.card0.SuspendLayout();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.card4.SuspendLayout();
            this.card5.SuspendLayout();
            this.pnlCharts.SuspendLayout();
            this.pnlLeftChart.SuspendLayout();
            this.pnlChartHeader1.SuspendLayout();
            this.pnlRightChart.SuspendLayout();
            this.pnlChartHeader2.SuspendLayout();
            this.pnlActivity.SuspendLayout();
            this.pnlActInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivity)).BeginInit();
            this.actHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblClock);
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(25, 0, 25, 0);
            this.pnlHeader.Size = new System.Drawing.Size(1200, 73);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(25, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard Hệ Thống";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.lblSubtitle.Location = new System.Drawing.Point(28, 53);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(400, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Tổng quan hoạt động SharkTank ERP";
            // 
            // lblClock
            // 
            this.lblClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblClock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.lblClock.Location = new System.Drawing.Point(860, 18);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(270, 20);
            this.lblClock.TabIndex = 2;
            this.lblClock.Text = "24/03/2026 14:32";
            this.lblClock.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(1037, 37);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(93, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Cập nhật";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // flowCards
            // 
            this.flowCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.flowCards.Controls.Add(this.card0);
            this.flowCards.Controls.Add(this.card1);
            this.flowCards.Controls.Add(this.card2);
            this.flowCards.Controls.Add(this.card3);
            this.flowCards.Controls.Add(this.card4);
            this.flowCards.Controls.Add(this.card5);
            this.flowCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCards.Location = new System.Drawing.Point(0, 73);
            this.flowCards.Name = "flowCards";
            this.flowCards.Padding = new System.Windows.Forms.Padding(20, 18, 20, 0);
            this.flowCards.Size = new System.Drawing.Size(1200, 124);
            this.flowCards.TabIndex = 1;
            this.flowCards.WrapContents = false;
            // 
            // card0
            // 
            this.card0.BackColor = System.Drawing.Color.White;
            this.card0.Controls.Add(this.acc0);
            this.card0.Controls.Add(this.ico0);
            this.card0.Controls.Add(this.val0);
            this.card0.Controls.Add(this.sub0);
            this.card0.Location = new System.Drawing.Point(20, 18);
            this.card0.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.card0.Name = "card0";
            this.card0.Size = new System.Drawing.Size(154, 90);
            this.card0.TabIndex = 0;
            // 
            // acc0
            // 
            this.acc0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.acc0.Dock = System.Windows.Forms.DockStyle.Left;
            this.acc0.Location = new System.Drawing.Point(0, 0);
            this.acc0.Name = "acc0";
            this.acc0.Size = new System.Drawing.Size(7, 90);
            this.acc0.TabIndex = 0;
            // 
            // ico0
            // 
            this.ico0.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.ico0.Location = new System.Drawing.Point(20, 12);
            this.ico0.Name = "ico0";
            this.ico0.Size = new System.Drawing.Size(35, 30);
            this.ico0.TabIndex = 1;
            this.ico0.Text = "👥";
            // 
            // val0
            // 
            this.val0.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.val0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.val0.Location = new System.Drawing.Point(20, 38);
            this.val0.Name = "val0";
            this.val0.Size = new System.Drawing.Size(131, 32);
            this.val0.TabIndex = 2;
            this.val0.Text = "—";
            // 
            // sub0
            // 
            this.sub0.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.sub0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.sub0.Location = new System.Drawing.Point(20, 72);
            this.sub0.Name = "sub0";
            this.sub0.Size = new System.Drawing.Size(131, 18);
            this.sub0.TabIndex = 3;
            this.sub0.Text = "Tổng nhân viên";
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.White;
            this.card1.Controls.Add(this.acc1);
            this.card1.Controls.Add(this.ico1);
            this.card1.Controls.Add(this.val1);
            this.card1.Controls.Add(this.sub1);
            this.card1.Location = new System.Drawing.Point(189, 18);
            this.card1.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.card1.Name = "card1";
            this.card1.Size = new System.Drawing.Size(154, 90);
            this.card1.TabIndex = 1;
            // 
            // acc1
            // 
            this.acc1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.acc1.Dock = System.Windows.Forms.DockStyle.Left;
            this.acc1.Location = new System.Drawing.Point(0, 0);
            this.acc1.Name = "acc1";
            this.acc1.Size = new System.Drawing.Size(7, 90);
            this.acc1.TabIndex = 0;
            // 
            // ico1
            // 
            this.ico1.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.ico1.Location = new System.Drawing.Point(20, 12);
            this.ico1.Name = "ico1";
            this.ico1.Size = new System.Drawing.Size(35, 30);
            this.ico1.TabIndex = 1;
            this.ico1.Text = "🏢";
            // 
            // val1
            // 
            this.val1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.val1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.val1.Location = new System.Drawing.Point(20, 38);
            this.val1.Name = "val1";
            this.val1.Size = new System.Drawing.Size(131, 32);
            this.val1.TabIndex = 2;
            this.val1.Text = "—";
            // 
            // sub1
            // 
            this.sub1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.sub1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.sub1.Location = new System.Drawing.Point(20, 72);
            this.sub1.Name = "sub1";
            this.sub1.Size = new System.Drawing.Size(134, 18);
            this.sub1.TabIndex = 3;
            this.sub1.Text = "Phòng ban";
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.White;
            this.card2.Controls.Add(this.acc2);
            this.card2.Controls.Add(this.ico2);
            this.card2.Controls.Add(this.val2);
            this.card2.Controls.Add(this.sub2);
            this.card2.Location = new System.Drawing.Point(358, 18);
            this.card2.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.card2.Name = "card2";
            this.card2.Size = new System.Drawing.Size(154, 90);
            this.card2.TabIndex = 2;
            // 
            // acc2
            // 
            this.acc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(130)))), ((int)(((byte)(8)))));
            this.acc2.Dock = System.Windows.Forms.DockStyle.Left;
            this.acc2.Location = new System.Drawing.Point(0, 0);
            this.acc2.Name = "acc2";
            this.acc2.Size = new System.Drawing.Size(7, 90);
            this.acc2.TabIndex = 0;
            // 
            // ico2
            // 
            this.ico2.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.ico2.Location = new System.Drawing.Point(20, 12);
            this.ico2.Name = "ico2";
            this.ico2.Size = new System.Drawing.Size(35, 30);
            this.ico2.TabIndex = 1;
            this.ico2.Text = "💼";
            // 
            // val2
            // 
            this.val2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.val2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.val2.Location = new System.Drawing.Point(20, 38);
            this.val2.Name = "val2";
            this.val2.Size = new System.Drawing.Size(131, 32);
            this.val2.TabIndex = 2;
            this.val2.Text = "—";
            // 
            // sub2
            // 
            this.sub2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.sub2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.sub2.Location = new System.Drawing.Point(20, 72);
            this.sub2.Name = "sub2";
            this.sub2.Size = new System.Drawing.Size(131, 18);
            this.sub2.TabIndex = 3;
            this.sub2.Text = "Chức vụ";
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.White;
            this.card3.Controls.Add(this.acc3);
            this.card3.Controls.Add(this.ico3);
            this.card3.Controls.Add(this.val3);
            this.card3.Controls.Add(this.sub3);
            this.card3.Location = new System.Drawing.Point(527, 18);
            this.card3.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.card3.Name = "card3";
            this.card3.Size = new System.Drawing.Size(154, 90);
            this.card3.TabIndex = 3;
            // 
            // acc3
            // 
            this.acc3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.acc3.Dock = System.Windows.Forms.DockStyle.Left;
            this.acc3.Location = new System.Drawing.Point(0, 0);
            this.acc3.Name = "acc3";
            this.acc3.Size = new System.Drawing.Size(7, 90);
            this.acc3.TabIndex = 0;
            // 
            // ico3
            // 
            this.ico3.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.ico3.Location = new System.Drawing.Point(20, 12);
            this.ico3.Name = "ico3";
            this.ico3.Size = new System.Drawing.Size(35, 30);
            this.ico3.TabIndex = 1;
            this.ico3.Text = "🔑";
            // 
            // val3
            // 
            this.val3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.val3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.val3.Location = new System.Drawing.Point(20, 38);
            this.val3.Name = "val3";
            this.val3.Size = new System.Drawing.Size(131, 32);
            this.val3.TabIndex = 2;
            this.val3.Text = "—";
            // 
            // sub3
            // 
            this.sub3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.sub3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.sub3.Location = new System.Drawing.Point(20, 72);
            this.sub3.Name = "sub3";
            this.sub3.Size = new System.Drawing.Size(131, 18);
            this.sub3.TabIndex = 3;
            this.sub3.Text = "Đăng nhập tháng";
            // 
            // card4
            // 
            this.card4.BackColor = System.Drawing.Color.White;
            this.card4.Controls.Add(this.acc4);
            this.card4.Controls.Add(this.ico4);
            this.card4.Controls.Add(this.val4);
            this.card4.Controls.Add(this.sub4);
            this.card4.Location = new System.Drawing.Point(696, 18);
            this.card4.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.card4.Name = "card4";
            this.card4.Size = new System.Drawing.Size(154, 90);
            this.card4.TabIndex = 4;
            // 
            // acc4
            // 
            this.acc4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.acc4.Dock = System.Windows.Forms.DockStyle.Left;
            this.acc4.Location = new System.Drawing.Point(0, 0);
            this.acc4.Name = "acc4";
            this.acc4.Size = new System.Drawing.Size(7, 90);
            this.acc4.TabIndex = 0;
            // 
            // ico4
            // 
            this.ico4.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.ico4.Location = new System.Drawing.Point(20, 12);
            this.ico4.Name = "ico4";
            this.ico4.Size = new System.Drawing.Size(35, 30);
            this.ico4.TabIndex = 1;
            this.ico4.Text = "🔒";
            // 
            // val4
            // 
            this.val4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.val4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.val4.Location = new System.Drawing.Point(20, 38);
            this.val4.Name = "val4";
            this.val4.Size = new System.Drawing.Size(134, 32);
            this.val4.TabIndex = 2;
            this.val4.Text = "—";
            // 
            // sub4
            // 
            this.sub4.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.sub4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.sub4.Location = new System.Drawing.Point(20, 72);
            this.sub4.Name = "sub4";
            this.sub4.Size = new System.Drawing.Size(134, 18);
            this.sub4.TabIndex = 3;
            this.sub4.Text = "TK bị khóa";
            // 
            // card5
            // 
            this.card5.BackColor = System.Drawing.Color.White;
            this.card5.Controls.Add(this.acc5);
            this.card5.Controls.Add(this.ico5);
            this.card5.Controls.Add(this.val5);
            this.card5.Controls.Add(this.sub5);
            this.card5.Location = new System.Drawing.Point(865, 18);
            this.card5.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.card5.Name = "card5";
            this.card5.Size = new System.Drawing.Size(154, 90);
            this.card5.TabIndex = 5;
            // 
            // acc5
            // 
            this.acc5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.acc5.Dock = System.Windows.Forms.DockStyle.Left;
            this.acc5.Location = new System.Drawing.Point(0, 0);
            this.acc5.Name = "acc5";
            this.acc5.Size = new System.Drawing.Size(7, 90);
            this.acc5.TabIndex = 0;
            this.acc5.Paint += new System.Windows.Forms.PaintEventHandler(this.acc5_Paint);
            // 
            // ico5
            // 
            this.ico5.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.ico5.Location = new System.Drawing.Point(20, 12);
            this.ico5.Name = "ico5";
            this.ico5.Size = new System.Drawing.Size(35, 30);
            this.ico5.TabIndex = 1;
            this.ico5.Text = "📋";
            // 
            // val5
            // 
            this.val5.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.val5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.val5.Location = new System.Drawing.Point(20, 38);
            this.val5.Name = "val5";
            this.val5.Size = new System.Drawing.Size(131, 32);
            this.val5.TabIndex = 2;
            this.val5.Text = "—";
            // 
            // sub5
            // 
            this.sub5.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.sub5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.sub5.Location = new System.Drawing.Point(20, 72);
            this.sub5.Name = "sub5";
            this.sub5.Size = new System.Drawing.Size(131, 18);
            this.sub5.TabIndex = 3;
            this.sub5.Text = "Nhật ký Audit";
            // 
            // pnlCharts
            // 
            this.pnlCharts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.pnlCharts.Controls.Add(this.pnlLeftChart);
            this.pnlCharts.Controls.Add(this.pnlRightChart);
            this.pnlCharts.Controls.Add(this.pnlActivity);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharts.Location = new System.Drawing.Point(0, 197);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.pnlCharts.Size = new System.Drawing.Size(1200, 603);
            this.pnlCharts.TabIndex = 2;
            // 
            // pnlLeftChart
            // 
            this.pnlLeftChart.BackColor = System.Drawing.Color.White;
            this.pnlLeftChart.Controls.Add(this.pnlBarChart);
            this.pnlLeftChart.Controls.Add(this.pnlChartHeader1);
            this.pnlLeftChart.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftChart.Location = new System.Drawing.Point(20, 10);
            this.pnlLeftChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLeftChart.Name = "pnlLeftChart";
            this.pnlLeftChart.Size = new System.Drawing.Size(480, 373);
            this.pnlLeftChart.TabIndex = 0;
            // 
            // pnlBarChart
            // 
            this.pnlBarChart.BackColor = System.Drawing.Color.White;
            this.pnlBarChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBarChart.Location = new System.Drawing.Point(0, 42);
            this.pnlBarChart.Name = "pnlBarChart";
            this.pnlBarChart.Padding = new System.Windows.Forms.Padding(15, 5, 15, 10);
            this.pnlBarChart.Size = new System.Drawing.Size(480, 331);
            this.pnlBarChart.TabIndex = 1;
            // 
            // pnlChartHeader1
            // 
            this.pnlChartHeader1.BackColor = System.Drawing.Color.Transparent;
            this.pnlChartHeader1.Controls.Add(this.lblChartTitle1);
            this.pnlChartHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChartHeader1.Location = new System.Drawing.Point(0, 0);
            this.pnlChartHeader1.Name = "pnlChartHeader1";
            this.pnlChartHeader1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.pnlChartHeader1.Size = new System.Drawing.Size(480, 42);
            this.pnlChartHeader1.TabIndex = 0;
            // 
            // lblChartTitle1
            // 
            this.lblChartTitle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChartTitle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblChartTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblChartTitle1.Location = new System.Drawing.Point(15, 0);
            this.lblChartTitle1.Name = "lblChartTitle1";
            this.lblChartTitle1.Size = new System.Drawing.Size(465, 42);
            this.lblChartTitle1.TabIndex = 0;
            this.lblChartTitle1.Text = "Hoạt động hệ thống (12 tháng gần nhất)";
            this.lblChartTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlRightChart
            // 
            this.pnlRightChart.BackColor = System.Drawing.Color.White;
            this.pnlRightChart.Controls.Add(this.pnlPieChart);
            this.pnlRightChart.Controls.Add(this.pnlChartHeader2);
            this.pnlRightChart.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRightChart.Location = new System.Drawing.Point(860, 10);
            this.pnlRightChart.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.pnlRightChart.Name = "pnlRightChart";
            this.pnlRightChart.Size = new System.Drawing.Size(320, 373);
            this.pnlRightChart.TabIndex = 1;
            // 
            // pnlPieChart
            // 
            this.pnlPieChart.BackColor = System.Drawing.Color.White;
            this.pnlPieChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPieChart.Location = new System.Drawing.Point(0, 42);
            this.pnlPieChart.Name = "pnlPieChart";
            this.pnlPieChart.Padding = new System.Windows.Forms.Padding(15, 5, 15, 10);
            this.pnlPieChart.Size = new System.Drawing.Size(320, 331);
            this.pnlPieChart.TabIndex = 1;
            // 
            // pnlChartHeader2
            // 
            this.pnlChartHeader2.BackColor = System.Drawing.Color.Transparent;
            this.pnlChartHeader2.Controls.Add(this.lblChartTitle2);
            this.pnlChartHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChartHeader2.Location = new System.Drawing.Point(0, 0);
            this.pnlChartHeader2.Name = "pnlChartHeader2";
            this.pnlChartHeader2.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.pnlChartHeader2.Size = new System.Drawing.Size(320, 42);
            this.pnlChartHeader2.TabIndex = 0;
            // 
            // lblChartTitle2
            // 
            this.lblChartTitle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChartTitle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblChartTitle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblChartTitle2.Location = new System.Drawing.Point(15, 0);
            this.lblChartTitle2.Name = "lblChartTitle2";
            this.lblChartTitle2.Size = new System.Drawing.Size(305, 42);
            this.lblChartTitle2.TabIndex = 0;
            this.lblChartTitle2.Text = "Người dùng theo vai trò";
            this.lblChartTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlActivity
            // 
            this.pnlActivity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.pnlActivity.Controls.Add(this.pnlActInner);
            this.pnlActivity.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActivity.Location = new System.Drawing.Point(20, 383);
            this.pnlActivity.Name = "pnlActivity";
            this.pnlActivity.Padding = new System.Windows.Forms.Padding(0, 8, 20, 20);
            this.pnlActivity.Size = new System.Drawing.Size(1160, 200);
            this.pnlActivity.TabIndex = 2;
            // 
            // pnlActInner
            // 
            this.pnlActInner.BackColor = System.Drawing.Color.White;
            this.pnlActInner.Controls.Add(this.dgvActivity);
            this.pnlActInner.Controls.Add(this.actHeader);
            this.pnlActInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActInner.Location = new System.Drawing.Point(0, 8);
            this.pnlActInner.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pnlActInner.Name = "pnlActInner";
            this.pnlActInner.Size = new System.Drawing.Size(1140, 172);
            this.pnlActInner.TabIndex = 0;
            // 
            // dgvActivity
            // 
            this.dgvActivity.AllowUserToAddRows = false;
            this.dgvActivity.AllowUserToDeleteRows = false;
            this.dgvActivity.AllowUserToResizeRows = false;
            this.dgvActivity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActivity.BackgroundColor = System.Drawing.Color.White;
            this.dgvActivity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            this.dgvActivity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvActivity.ColumnHeadersHeight = 40;
            this.dgvActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActivity.EnableHeadersVisualStyles = false;
            this.dgvActivity.Location = new System.Drawing.Point(0, 42);
            this.dgvActivity.MultiSelect = false;
            this.dgvActivity.Name = "dgvActivity";
            this.dgvActivity.ReadOnly = true;
            this.dgvActivity.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.dgvActivity.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvActivity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActivity.Size = new System.Drawing.Size(1140, 130);
            this.dgvActivity.TabIndex = 1;
            // 
            // actHeader
            // 
            this.actHeader.BackColor = System.Drawing.Color.Transparent;
            this.actHeader.Controls.Add(this.lblActTitle);
            this.actHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.actHeader.Location = new System.Drawing.Point(0, 0);
            this.actHeader.Name = "actHeader";
            this.actHeader.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.actHeader.Size = new System.Drawing.Size(1140, 42);
            this.actHeader.TabIndex = 0;
            // 
            // lblActTitle
            // 
            this.lblActTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblActTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblActTitle.Location = new System.Drawing.Point(15, 0);
            this.lblActTitle.Name = "lblActTitle";
            this.lblActTitle.Size = new System.Drawing.Size(1125, 42);
            this.lblActTitle.TabIndex = 0;
            this.lblActTitle.Text = "Hoạt động gần đây";
            this.lblActTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DashboardTongForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.pnlCharts);
            this.Controls.Add(this.flowCards);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "DashboardTongForm";
            this.Size = new System.Drawing.Size(1200, 800);
            this.pnlHeader.ResumeLayout(false);
            this.flowCards.ResumeLayout(false);
            this.card0.ResumeLayout(false);
            this.card1.ResumeLayout(false);
            this.card2.ResumeLayout(false);
            this.card3.ResumeLayout(false);
            this.card4.ResumeLayout(false);
            this.card5.ResumeLayout(false);
            this.pnlCharts.ResumeLayout(false);
            this.pnlLeftChart.ResumeLayout(false);
            this.pnlChartHeader1.ResumeLayout(false);
            this.pnlRightChart.ResumeLayout(false);
            this.pnlChartHeader2.ResumeLayout(false);
            this.pnlActivity.ResumeLayout(false);
            this.pnlActInner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivity)).EndInit();
            this.actHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // Fields
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblClock;
        private Button btnRefresh;
        private FlowLayoutPanel flowCards;
        private Panel pnlCharts;
        private Panel pnlBarChart;
        private Panel pnlPieChart;
        private Panel pnlActivity;
        private DataGridView dgvActivity;
        private Panel card0;
        private Panel acc0;
        private Label ico0;
        private Label val0;
        private Label sub0;
        private Panel card1;
        private Panel acc1;
        private Label ico1;
        private Label val1;
        private Label sub1;
        private Panel card2;
        private Panel acc2;
        private Label ico2;
        private Label val2;
        private Label sub2;
        private Panel card3;
        private Panel acc3;
        private Label ico3;
        private Label val3;
        private Label sub3;
        private Panel card4;
        private Panel acc4;
        private Label ico4;
        private Label val4;
        private Label sub4;
        private Panel card5;
        private Panel acc5;
        private Label ico5;
        private Label val5;
        private Label sub5;
        private Panel pnlLeftChart;
        private Panel pnlChartHeader1;
        private Label lblChartTitle1;
        private Panel pnlRightChart;
        private Panel pnlChartHeader2;
        private Label lblChartTitle2;
        private Panel pnlActInner;
        private Panel actHeader;
        private Label lblActTitle;
    }
}
