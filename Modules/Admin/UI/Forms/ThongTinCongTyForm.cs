using System;
using System.Windows.Forms;
using SharkTank.DAL;
using SharkTank.DAL.Sql;
using SharkTank.Core.Models;

namespace SharkTank.Modules.Admin.UI.Forms
{
    public partial class ThongTinCongTyForm : UserControl
    {
        private Company _company;
        private readonly ICompanyRepository _companyRepo;

        public ThongTinCongTyForm()
        {
            InitializeComponent();
            _companyRepo = new SqlCompanyRepository(SqlConnectionFactory.Create());
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _company = _companyRepo.GetCurrent();
                if (_company == null)
                {
                    _company = new Company();
                }

                txtTenCongTy.Text = _company.CompanyName;
                txtDiaChi.Text = _company.Address;
                txtMaSoThue.Text = _company.TaxCode;
                txtDienThoai.Text = _company.Phone;
                txtEmail.Text = _company.Email;
                txtWebsite.Text = _company.Website;
                txtSoDangKy.Text = _company.BusinessLicense;
                txtNguoiDaiDien.Text = _company.RepresentativeName;
                txtChucVu.Text = _company.RepresentativePosition;
                txtHotline.Text = _company.Hotline;

                if (!string.IsNullOrEmpty(_company.LogoPath) && System.IO.File.Exists(_company.LogoPath))
                {
                    try
                    {
                        picLogo.Image = System.Drawing.Image.FromFile(_company.LogoPath);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
                dlg.Title = "Chọn Logo Công Ty";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picLogo.Image = System.Drawing.Image.FromFile(dlg.FileName);
                        _company.LogoPath = dlg.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenCongTy.Text))
            {
                MessageBox.Show("Tên công ty không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCongTy.Focus();
                return;
            }

            try
            {
                _company.CompanyName = txtTenCongTy.Text.Trim();
                _company.Address = txtDiaChi.Text.Trim();
                _company.TaxCode = txtMaSoThue.Text.Trim();
                _company.Phone = txtDienThoai.Text.Trim();
                _company.Email = txtEmail.Text.Trim();
                _company.Website = txtWebsite.Text.Trim();
                _company.BusinessLicense = txtSoDangKy.Text.Trim();
                _company.RepresentativeName = txtNguoiDaiDien.Text.Trim();
                _company.RepresentativePosition = txtChucVu.Text.Trim();
                _company.Hotline = txtHotline.Text.Trim();

                _companyRepo.Save(_company);
                MessageBox.Show(
                    "Đã lưu thông tin công ty lên SQL (" + DateTime.Now.ToString("HH:mm:ss") + ").\n\n" +
                    "Mở lại màn hình này để xác nhận dữ liệu đọc từ database.",
                    "Đã lưu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn hủy thay đổi?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoadData();
            }
        }
    }
}
