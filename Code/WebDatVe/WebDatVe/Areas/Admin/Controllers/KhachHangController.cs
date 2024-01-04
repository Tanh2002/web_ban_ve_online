using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Attributes;
using WebDatVe.Models;

namespace WebDatVe.Areas.Admin.Controllers
{
    [AdminAuthorize]
    [PemisitonAttribute("Khách hàng")]
    public class KhachHangController : BaseController
    {
        // GET: Admin/KhachHang
        public ActionResult Index(string keyword = "")
        {
            var list = Db.KhachHangs.Where(x => x.TenDangNhap.Contains(keyword) || x.HoTen.Contains(keyword) || x.SoDienThoai.Contains(keyword) || x.Email.Contains(keyword) || x.CMND.Contains(keyword)).OrderByDescending(x => x.MaKhachHang).ToList();
            ViewBag.TextSearch = keyword;
            return View(list);
        }

        public ActionResult Add()
        {
            return View(new KhachHang());
        }

        [HttpPost]
        public ActionResult Add(KhachHang model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = Db.KhachHangs.FirstOrDefault(x => x.SoDienThoai == model.SoDienThoai || x.TenDangNhap == model.TenDangNhap);
                    if (obj == null)
                    {
                        Db.KhachHangs.Add(model);
                        Db.SaveChanges();
                        TempData["notice"] = "Thêm thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Số điện thoại đã tồn tại! Vui lòng chọn số điện thoại khác!";
                    }
                }
                catch
                {
                    TempData["notice"] = "Thêm không thành công!";
                }
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var model = Db.KhachHangs.FirstOrDefault(x => x.MaKhachHang == id);
                if (model == null)
                {
                    model.MaKhachHang = 0;
                }
                return View(model);
            }
            catch
            {
                TempData["notice"] = "Dữ liệu không tồn tại!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(KhachHang model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objCheck = Db.KhachHangs.FirstOrDefault(x => x.SoDienThoai == model.SoDienThoai && x.MaKhachHang != model.MaKhachHang);
                    if (objCheck == null)
                    {
                        var obj = Db.KhachHangs.FirstOrDefault(x => x.MaKhachHang == model.MaKhachHang);
                        obj.HoTen = model.HoTen;
                        obj.Email = model.Email;
                        obj.SoDienThoai = model.SoDienThoai;
                        obj.CMND = model.CMND;

                        Db.KhachHangs.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();
                        TempData["notice"] = "Sửa thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Số điện thoại đã tồn tại! Vui lòng chọn số điện thoại khác!";
                    }
                }
                catch
                {
                    TempData["notice"] = "Sửa không thành công!";
                }
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var model = Db.KhachHangs.FirstOrDefault(x => x.MaKhachHang == id);
                Db.KhachHangs.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.KhachHangs.Remove(model);
                Db.SaveChanges();
                TempData["notice"] = "Xóa thành công!";
            }
            catch
            {
                TempData["notice"] = "Xóa không thành công! Nguyên nhân: Có ràng buộc cơ sở dữ liệu!";
            }

            return RedirectToAction("Index");
        }
    }
}