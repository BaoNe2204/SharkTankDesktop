using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SharkTank.Modules.Accounting.UI.Forms
{
    /// <summary>
    /// View: Phiếu thu / Phiếu chi
    /// Quản lý thu chi tiền mặt
    /// </summary>
    public partial class PhieuThuChiView : UserControl
    {
        // Khai báo các Controls
        private ComboBox cbLoaiPhieu;
        private TextBox txtSoPhieu;
        private DateTimePicker dtpNgay;
        private NumericUpDown nudSoTien;
        private TextBox txtNoiDung;

        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnPrint;
        private Button btnClear;

        private DataGridView dgvTransactions;

        // DataTable giả lập để test dữ liệu
        private DataTable dtTransactions;

        public PhieuThuChiView()
        {
            InitializeComponent();
            SetupMockData();
            LoadDataToGrid();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Size = new Size(900, 600);

            // 1. PANEL TIÊU ĐỀ (Giữ nguyên của bạn)
            Panel panelTitle = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(0, 120, 215)
            };
            Label lblTitle = new Label
            {
                Text = "💸 Phiếu thu / Phiếu chi",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true
            };
            panelTitle.Controls.Add(lblTitle);
            this.Controls.Add(panelTitle);

            // 2. PANEL NHẬP LIỆU (Input Form)
            GroupBox gbInput = new GroupBox
            {
                Text = "Thông tin phiếu",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 150,
                Padding = new Padding(10)
            };

            // Các Labels & Controls bên trong GroupBox
            Font normalFont = new Font("Segoe UI", 9, FontStyle.Regular);

            gbInput.Controls.Add(new Label { Text = "Loại phiếu:", Location = new Point(20, 30), AutoSize = true, Font = normalFont });
            cbLoaiPhieu = new ComboBox { Location = new Point(100, 27), Width = 150, Font = normalFont, DropDownStyle = ComboBoxStyle.DropDownList };
            cbLoaiPhieu.Items.AddRange(new object[] { "Phiếu Thu", "Phiếu Chi" });
            cbLoaiPhieu.SelectedIndex = 0;
            gbInput.Controls.Add(cbLoaiPhieu);

            gbInput.Controls.Add(new Label { Text = "Số phiếu:", Location = new Point(280, 30), AutoSize = true, Font = normalFont });
            txtSoPhieu = new TextBox { Location = new Point(350, 27), Width = 150, Font = normalFont };
            gbInput.Controls.Add(txtSoPhieu);

            gbInput.Controls.Add(new Label { Text = "Ngày lập:", Location = new Point(530, 30), AutoSize = true, Font = normalFont });
            dtpNgay = new DateTimePicker { Location = new Point(600, 27), Width = 150, Format = DateTimePickerFormat.Short, Font = normalFont };
            gbInput.Controls.Add(dtpNgay);

            gbInput.Controls.Add(new Label { Text = "Số tiền:", Location = new Point(20, 75), AutoSize = true, Font = normalFont });
            nudSoTien = new NumericUpDown { Location = new Point(100, 72), Width = 150, Font = normalFont, Maximum = 100000000000, Increment = 10000, DecimalPlaces = 0 };
            gbInput.Controls.Add(nudSoTien);

            gbInput.Controls.Add(new Label { Text = "Nội dung:", Location = new Point(280, 75), AutoSize = true, Font = normalFont });
            txtNoiDung = new TextBox { Location = new Point(350, 72), Width = 400, Height = 50, Font = normalFont, Multiline = true, ScrollBars = ScrollBars.Vertical };
            gbInput.Controls.Add(txtNoiDung);

            this.Controls.Add(gbInput);
            gbInput.BringToFront();

            // 3. PANEL NÚT BẤM (Buttons)
            FlowLayoutPanel panelButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(15, 10, 0, 0),
                BackColor = Color.WhiteSmoke
            };

            btnAdd = CreateButton("Thêm phiếu", Color.SeaGreen);
            btnEdit = CreateButton("Cập nhật", Color.DarkOrange);
            btnDelete = CreateButton("Xóa", Color.Crimson);
            btnPrint = CreateButton("In phiếu", Color.SteelBlue);
            btnClear = CreateButton("Làm mới", Color.Gray);

            // Gán sự kiện cho các nút
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += BtnClear_Click;
            btnPrint.Click += (s, e) => MessageBox.Show("Chức năng in đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            panelButtons.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnPrint, btnClear });
            this.Controls.Add(panelButtons);
            panelButtons.BringToFront();

            // 4. PANEL DATA GRID VIEW (Danh sách)
            Panel panelGrid = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            dgvTransactions = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                RowHeadersVisible = false,
                Font = normalFont
            };
            dgvTransactions.SelectionChanged += DgvTransactions_SelectionChanged;

            panelGrid.Controls.Add(dgvTransactions);
            this.Controls.Add(panelGrid);
            panelGrid.BringToFront();

            this.ResumeLayout(false);
        }

        // --- Helper Methods ---
        private Button CreateButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                Size = new Size(100, 35),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 10, 0)
            };
        }

        private void SetupMockData()
        {
            dtTransactions = new DataTable();
            dtTransactions.Columns.Add("SoPhieu", typeof(string));
            dtTransactions.Columns.Add("LoaiPhieu", typeof(string));
            dtTransactions.Columns.Add("NgayLap", typeof(DateTime));
            dtTransactions.Columns.Add("SoTien", typeof(decimal));
            dtTransactions.Columns.Add("NoiDung", typeof(string));

            // Thêm dữ liệu mẫu
            dtTransactions.Rows.Add("PT-001", "Phiếu Thu", DateTime.Now.Date, 5000000, "Thu tiền khách hàng A");
            dtTransactions.Rows.Add("PC-001", "Phiếu Chi", DateTime.Now.Date.AddDays(-1), 1500000, "Chi trả tiền điện tháng này");
        }

        private void LoadDataToGrid()
        {
            dgvTransactions.DataSource = dtTransactions;

            // Tùy chỉnh tiêu đề cột
            dgvTransactions.Columns["SoPhieu"].HeaderText = "Số phiếu";
            dgvTransactions.Columns["LoaiPhieu"].HeaderText = "Loại phiếu";
            dgvTransactions.Columns["NgayLap"].HeaderText = "Ngày lập";
            dgvTransactions.Columns["SoTien"].HeaderText = "Số tiền (VNĐ)";
            dgvTransactions.Columns["NoiDung"].HeaderText = "Nội dung";

            // Format cột số tiền
            dgvTransactions.Columns["SoTien"].DefaultCellStyle.Format = "N0";
        }

        // --- Event Handlers ---
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSoPhieu.Clear();
            cbLoaiPhieu.SelectedIndex = 0;
            dtpNgay.Value = DateTime.Now;
            nudSoTien.Value = 0;
            txtNoiDung.Clear();
            txtSoPhieu.Focus();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoPhieu.Text))
            {
                MessageBox.Show("Vui lòng nhập số phiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm vào DataTable
            dtTransactions.Rows.Add(
                txtSoPhieu.Text,
                cbLoaiPhieu.SelectedItem.ToString(),
                dtpNgay.Value.Date,
                nudSoTien.Value,
                txtNoiDung.Text
            );

            BtnClear_Click(null, null);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTransactions.SelectedRows.Count > 0)
            {
                var row = dgvTransactions.SelectedRows[0];
                int rowIndex = row.Index;

                dtTransactions.Rows[rowIndex]["SoPhieu"] = txtSoPhieu.Text;
                dtTransactions.Rows[rowIndex]["LoaiPhieu"] = cbLoaiPhieu.SelectedItem.ToString();
                dtTransactions.Rows[rowIndex]["NgayLap"] = dtpNgay.Value.Date;
                dtTransactions.Rows[rowIndex]["SoTien"] = nudSoTien.Value;
                dtTransactions.Rows[rowIndex]["NoiDung"] = txtNoiDung.Text;

                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTransactions.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int rowIndex = dgvTransactions.SelectedRows[0].Index;
                    dtTransactions.Rows.RemoveAt(rowIndex);
                    BtnClear_Click(null, null);
                }
            }
        }

        private void DgvTransactions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTransactions.SelectedRows.Count > 0)
            {
                var row = dgvTransactions.SelectedRows[0];
                txtSoPhieu.Text = row.Cells["SoPhieu"].Value.ToString();
                cbLoaiPhieu.SelectedItem = row.Cells["LoaiPhieu"].Value.ToString();
                dtpNgay.Value = Convert.ToDateTime(row.Cells["NgayLap"].Value);
                nudSoTien.Value = Convert.ToDecimal(row.Cells["SoTien"].Value);
                txtNoiDung.Text = row.Cells["NoiDung"].Value.ToString();
            }
        }
    }
}