using System.Data;
using SharkTank.Modules.Sales.DAL;

namespace SharkTank.Modules.Sales.BLL
{
    public class DoanhThu_BLL
    {
        private DoanhThu_DAL dal = new DoanhThu_DAL();

        public DataTable LayDoanhThuTheoNgay()
        {
            return dal.GetDoanhThuTheoNgay();
        }

        public DataTable LayDoanhThuTheoNhanVien()
        {
            return dal.GetDoanhThuTheoNhanVien();
        }

        public DataTable LayDoanhThuTheoSanPham()
        {
            return dal.GetDoanhThuTheoSanPham();
        }
    }
}