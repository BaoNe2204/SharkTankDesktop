using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SharkTank.Modules.HR.Data;

namespace SharkTank.Modules.HR.UI.Forms
{
    public class SoDoToChucView : UserControl
    {
        private TreeView tvToChuc;
        private Panel panelDetail;
        private Label lblTenPB, lblSoNV, lblMoTa;
        private DataGridView dgvNhanVien;
        private Button btnLamMoi;

        public SoDoToChucView()
        {
            InitializeComponent();
            LoadSoDo();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            var lblTitle = new Label { Text = "🏗️ Sơ đồ tổ chức", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) };

            var panelBar = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(10) };
            btnLamMoi = new Button { Text = "🔄 Làm mới", Location = new Point(10, 8), Size = new Size(110, 28), Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.Click += (s, e) => LoadSoDo();
            panelBar.Controls.Add(btnLamMoi);
            panelBar.Controls.Add(new Label { Text = "👆 Click vào phòng ban để xem chi tiết", Location = new Point(130, 12), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray });

            // ── SPLIT ──
            var split = new SplitContainer { Dock = DockStyle.Fill, SplitterDistance = 320, BorderStyle = BorderStyle.None };

            tvToChuc = new TreeView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(248, 250, 255),
                ItemHeight = 28,
                ShowLines = true,
                ShowPlusMinus = true,
                FullRowSelect = true
            };
            tvToChuc.AfterSelect += TvToChuc_AfterSelect;
            split.Panel1.Controls.Add(tvToChuc);
            split.Panel1.BackColor = Color.FromArgb(248, 250, 255);

            // Detail
            panelDetail = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(15), Visible = false };

            lblTenPB = new Label { Location = new Point(15, 10), AutoSize = true, Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) };
            lblSoNV = new Label { Location = new Point(15, 45), AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(16, 137, 62) };
            lblMoTa = new Label { Location = new Point(15, 68), Size = new Size(500, 35), Font = new Font("Segoe UI", 9), ForeColor = Color.Gray };
            var lblDS = new Label { Location = new Point(15, 110), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160), Text = "👥 Danh sách nhân viên:" };

            dgvNhanVien = new DataGridView
            {
                Location = new Point(15, 135),
                Size = new Size(650, 350),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 160);
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvNhanVien.EnableHeadersVisualStyles = false;
            dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255);
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã NV", FillWeight = 80 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ tên", FillWeight = 160 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Chức vụ", FillWeight = 120 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày vào làm", FillWeight = 110 });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng thái", FillWeight = 90 });

            panelDetail.Controls.AddRange(new Control[] { lblTenPB, lblSoNV, lblMoTa, lblDS, dgvNhanVien });

            var lblHint = new Label { Text = "← Chọn phòng ban từ danh sách bên trái", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 12), ForeColor = Color.LightGray };
            split.Panel2.Controls.Add(panelDetail);
            split.Panel2.Controls.Add(lblHint);

            this.Controls.Add(split);
            this.Controls.Add(panelBar);
            this.Controls.Add(lblTitle);
        }

        private void LoadSoDo()
        {
            tvToChuc.Nodes.Clear();
            try
            {
                var root = new TreeNode("🏢 Công ty") { NodeFont = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(0, 80, 160) };

                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    // Load tất cả phòng ban
                    var listPB = new List<(int id, string ten, string moTa)>();
                    var rdrPB = new SqlCommand("SELECT PhongBanId, TenPhongBan, ISNULL(MoTa,'') AS MoTa FROM PhongBan ORDER BY TenPhongBan", conn).ExecuteReader();
                    while (rdrPB.Read())
                        listPB.Add(((int)rdrPB["PhongBanId"], rdrPB["TenPhongBan"].ToString(), rdrPB["MoTa"].ToString()));
                    rdrPB.Close();

                    foreach (var pb in listPB)
                    {
                        // Load NV từng phòng ban
                        var listNV = new List<(string maNV, string hoTen, string chucVu)>();
                        var cmdNV = new SqlCommand(@"
                            SELECT nv.NhanVienId, nv.HoTen, ISNULL(cv.TenChucVu,'--') AS TenChucVu
                            FROM NhanVien nv LEFT JOIN ChucVu cv ON nv.ChucVuId = cv.ChucVuId
                            WHERE nv.PhongBanId = @PBId ORDER BY nv.HoTen", conn);
                        cmdNV.Parameters.AddWithValue("@PBId", pb.id);
                        var rdrNV = cmdNV.ExecuteReader();
                        while (rdrNV.Read())
                            listNV.Add((rdrNV["NhanVienId"].ToString(), rdrNV["HoTen"].ToString(), rdrNV["TenChucVu"].ToString()));
                        rdrNV.Close();

                        // Tạo node phòng ban
                        var pbNode = new TreeNode($"🏬 {pb.ten}  ({listNV.Count} người)")
                        {
                            Tag = pb.id,
                            NodeFont = new Font("Segoe UI", 10, FontStyle.Bold),
                            ForeColor = Color.FromArgb(0, 80, 160)
                        };

                        // Thêm nhân viên làm node con
                        foreach (var nv in listNV)
                        {
                            pbNode.Nodes.Add(new TreeNode($"👤 {nv.hoTen}  —  {nv.chucVu}")
                            {
                                ForeColor = Color.FromArgb(50, 50, 80),
                                NodeFont = new Font("Segoe UI", 9)
                            });
                        }

                        root.Nodes.Add(pbNode);
                    }
                }

                tvToChuc.Nodes.Add(root);
                root.Expand();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải sơ đồ: " + ex.Message); }
        }

        private void TvToChuc_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null || !(e.Node.Tag is int)) return;

            int pbId = (int)e.Node.Tag;
            panelDetail.Visible = true;
            dgvNhanVien.Rows.Clear();

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    var cmdPB = new SqlCommand("SELECT TenPhongBan, ISNULL(MoTa,'') AS MoTa FROM PhongBan WHERE PhongBanId=@Id", conn);
                    cmdPB.Parameters.AddWithValue("@Id", pbId);
                    var rdrPB = cmdPB.ExecuteReader();
                    if (rdrPB.Read())
                    {
                        lblTenPB.Text = $"🏬 {rdrPB["TenPhongBan"]}";
                        lblMoTa.Text = rdrPB["MoTa"].ToString();
                    }
                    rdrPB.Close();

                    var cmdNV = new SqlCommand(@"
                        SELECT nv.NhanVienId, nv.HoTen,
                               ISNULL(cv.TenChucVu,'--') AS TenChucVu,
                               nv.NgayVaoLam,
                               ISNULL(nv.TrangThai,'Đang làm') AS TrangThai
                        FROM NhanVien nv
                        LEFT JOIN ChucVu cv ON nv.ChucVuId = cv.ChucVuId
                        WHERE nv.PhongBanId = @Id ORDER BY nv.HoTen", conn);
                    cmdNV.Parameters.AddWithValue("@Id", pbId);
                    var rdrNV = cmdNV.ExecuteReader();
                    int count = 0;
                    while (rdrNV.Read())
                    {
                        int row = dgvNhanVien.Rows.Add(
                            rdrNV["NhanVienId"],
                            rdrNV["HoTen"],
                            rdrNV["TenChucVu"],
                            rdrNV["NgayVaoLam"] != DBNull.Value ? ((DateTime)rdrNV["NgayVaoLam"]).ToString("dd/MM/yyyy") : "--",
                            rdrNV["TrangThai"]
                        );
                        string tt = rdrNV["TrangThai"].ToString();
                        dgvNhanVien.Rows[row].DefaultCellStyle.ForeColor = tt == "Đang làm" ? Color.FromArgb(16, 137, 62) : Color.OrangeRed;
                        count++;
                    }
                    lblSoNV.Text = $"👥 {count} nhân viên";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}