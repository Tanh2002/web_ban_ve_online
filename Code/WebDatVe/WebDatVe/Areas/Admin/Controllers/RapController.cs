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
    [PemisitonAttribute("Rạp")]
    public class RapController : BaseController
    {
        // GET: Admin/Rap
        public ActionResult Index(string keyword = "")
        {
            var list = Db.Raps.Where(x => x.TenRap.Contains(keyword)).OrderByDescending(x => x.MaRap).ToList();
            ViewBag.TextSearch = keyword;
            return View(list);
        }

        public ActionResult Add()
        {
            return View(new Rap());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Rap model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = Db.Raps.FirstOrDefault(x => x.TenRap == model.TenRap);
                    if (obj == null)
                    {
                        Db.Raps.Add(model);
                        Db.SaveChanges();
                        TempData["notice"] = "Thêm thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tên rạp đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.Raps.FirstOrDefault(x => x.MaRap == id);
                if (model == null)
                {
                    model.MaRap = 0;
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
        [ValidateInput(false)]
        public ActionResult Edit(Rap model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objCheck = Db.Raps.FirstOrDefault(x => x.TenRap == model.TenRap && x.MaRap != model.MaRap);
                    if (objCheck == null)
                    {
                        var obj = Db.Raps.FirstOrDefault(x => x.MaRap == model.MaRap);
                        obj.TenRap = model.TenRap;
                        obj.HinhAnh = model.HinhAnh;
                        obj.DiaChi = model.DiaChi;
                        obj.NoiDung = model.NoiDung;

                        Db.Raps.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();
                        TempData["notice"] = "Sửa thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tên rạp đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.Raps.FirstOrDefault(x => x.MaRap == id);
                Db.Raps.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.Raps.Remove(model);
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