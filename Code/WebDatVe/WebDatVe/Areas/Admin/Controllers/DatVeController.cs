using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Attributes;
using WebDatVe.Hepper;

namespace WebDatVe.Areas.Admin.Controllers
{
    [AdminAuthorize]
    [PemisitonAttribute("Đặt vé")]
    public class DatVeController : BaseController
    {
        // GET: Admin/DatVe
        public ActionResult Index(string keyword = "", int madonhang = 0, int makhachhang = 0)
        {
            var list = Db.DonHangs.Where(x => (x.KhachHang.HoTen.Contains(keyword) || x.KhachHang.SoDienThoai.Contains(keyword) || x.KhachHang.Email.Contains(keyword)) && (madonhang <= 0 || x.MaDonHang == madonhang) && (makhachhang <= 0 || x.MaKhachHang == makhachhang)).OrderByDescending(x => x.NgayDat).ToList();

            ViewBag.TextSearch = keyword;
            ViewBag.MaDonHang = madonhang == 0 ? "" : madonhang.ToString();

            return View(list);
        }

        public ActionResult Receive(int id)
        {
            try
            {
                var model = Db.DonHangs.FirstOrDefault(x => x.MaDonHang == id);

                model.TrangThai = (int)TrangThaiDonHang.DaNhanVe;

                Db.DonHangs.Attach(model);
                Db.Entry(model).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["notice"] = "Nhân vé thành công!";
            }
            catch
            {
                TempData["notice"] = "Nhận vé không thành công! Nguyên nhân: Có ràng buộc cơ sở dữ liệu!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Cancel(int id)
        {
            try
            {
                var model = Db.DonHangs.FirstOrDefault(x => x.MaDonHang == id);

                model.TrangThai = (int)TrangThaiDonHang.Huy;

                Db.DonHangs.Attach(model);
                Db.Entry(model).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["notice"] = "Hủy đơn hàng thành công!";
            }
            catch
            {
                TempData["notice"] = "Hủy đơn hàng không thành công! Nguyên nhân: Có ràng buộc cơ sở dữ liệu!";
            }

            return RedirectToAction("Index");
        }
    }
}