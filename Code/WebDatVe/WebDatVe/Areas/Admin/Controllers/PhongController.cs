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
    [PemisitonAttribute("Phòng")]
    public class PhongController : BaseController
    {
        // GET: Admin/TheLoai
        public ActionResult Index(string keyword = "", int maRap = 0)
        {
            var list = Db.Phongs.Where(x => x.TenPhong.Contains(keyword) && (maRap <= 0 || x.MaRap == maRap)).OrderByDescending(x => x.MaPhong).ToList();

            ViewBag.TextSearch = keyword;
            ViewBag.MaRap = maRap;

            return View(list);
        }

        public ActionResult Add()
        {
            return View(new Phong());
        }

        [HttpPost]
        public ActionResult Add(Phong model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = Db.Phongs.FirstOrDefault(x => x.TenPhong == model.TenPhong && x.MaRap == model.MaRap);
                    if (obj == null)
                    {
                        Db.Phongs.Add(model);
                        Db.SaveChanges();
                        TempData["notice"] = "Thêm thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tên phòng đã tồn tại! Vui lòng chọn tên khác!";
                    }
                }
                catch(Exception ex)
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
                var model = Db.Phongs.FirstOrDefault(x => x.MaPhong == id);
                if (model == null)
                {
                    model.MaPhong = 0;
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
        public ActionResult Edit(Phong model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objCheck = Db.Phongs.FirstOrDefault(x => x.TenPhong == model.TenPhong && x.MaRap == model.MaRap && x.MaPhong != model.MaPhong);
                    if (objCheck == null)
                    {
                        var obj = Db.Phongs.FirstOrDefault(x => x.MaPhong == model.MaPhong);
                        obj.TenPhong = model.TenPhong;
                        obj.MaRap = model.MaRap;
                        obj.MoTa = model.MoTa;

                        Db.Phongs.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();
                        TempData["notice"] = "Sửa thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tên phòng đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.Phongs.FirstOrDefault(x => x.MaPhong == id);
                Db.Phongs.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.Phongs.Remove(model);
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