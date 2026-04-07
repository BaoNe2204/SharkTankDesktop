using iTextSharp.text;       
using iTextSharp.text.pdf;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SharkTank.Modules.Sales.UI.Forms
{
    public partial class PDF : Form
    {
        private DataGridView _dgvData;

        // Constructor nhận vào cái Bảng và Tiêu đề mặc định
        public PDF(DataGridView dgv, string tieuDeMacDinh)
        {
            InitializeComponent();
            _dgvData = dgv;
            txtTieuDe.Text = tieuDeMacDinh;

            btnChon.Click += BtnChon_Click;
            btnHuy.Click += (s, e) => this.Close();
            btnXuat.Click += BtnXuat_Click;
        }

        private void BtnChon_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF Documents (*.pdf)|*.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    txtDuongDan.Text = sfd.FileName;
                }
            }
        }

        private void BtnXuat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDuongDan.Text))
            {
                MessageBox.Show("Vui lòng chọn nơi lưu file!", "Lưu ý");
                return;
            }

            try
            {
                string fontPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + "\\arial.ttf";
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontHeader = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font fontCell = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

                using (FileStream stream = new FileStream(txtDuongDan.Text, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 20f, 20f, 30f, 30f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    Paragraph title = new Paragraph(txtTieuDe.Text.ToUpper(), fontTitle);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 20f;
                    pdfDoc.Add(title);

                    PdfPTable pdfTable = new PdfPTable(_dgvData.Columns.Count);
                    pdfTable.WidthPercentage = 100;

                    foreach (DataGridViewColumn column in _dgvData.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, fontHeader));
                        cell.BackgroundColor = new BaseColor(41, 128, 185);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.Padding = 8f;
                        pdfTable.AddCell(cell);
                    }

                    foreach (DataGridViewRow row in _dgvData.Rows)
                    {
                        if (row.IsNewRow) continue;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            string cellValue = cell.Value != null ? cell.Value.ToString() : "";
                            PdfPCell pdfCell = new PdfPCell(new Phrase(cellValue, fontCell));
                            pdfCell.Padding = 6f;
                            pdfTable.AddCell(pdfCell);
                        }
                    }

                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }

                MessageBox.Show("Xuất file PDF thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}