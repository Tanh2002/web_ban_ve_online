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
    [PemisitonAttribute("Tin tức")]
    public class TinTucController : BaseController
    {
        // GET: Admin/TinTuc
        public ActionResult Index(string keyword = "")
        {
            var list = Db.TinTucs.Where(x => x.TieuDe.Contains(keyword)).OrderByDescending(x => x.MaTinTuc).ToList();
            ViewBag.TextSearch = keyword;
            return View(list);
        }

        public ActionResult Add()
        {
            return View(new TinTuc());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(TinTuc model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = Db.TinTucs.FirstOrDefault(x => x.TieuDe == model.TieuDe);
                    if (obj == null)
                    {
                        model.NgayTao = DateTime.Now;
                        Db.TinTucs.Add(model);
                        Db.SaveChanges();
                        TempData["notice"] = "Thêm thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tiêu đề đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.TinTucs.FirstOrDefault(x => x.MaTinTuc == id);
                if (model == null)
                {
                    model.MaTinTuc = 0;
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
        public ActionResult Edit(TinTuc model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objCheck = Db.TinTucs.FirstOrDefault(x => x.TieuDe == model.TieuDe && x.MaTinTuc != model.MaTinTuc);
                    if (objCheck == null)
                    {
                        var obj = Db.TinTucs.FirstOrDefault(x => x.MaTinTuc == model.MaTinTuc);
                        obj.TieuDe = model.TieuDe;
                        obj.HinhAnh = model.HinhAnh;
                        obj.GioiThieu = model.GioiThieu;
                        obj.NoiDung = model.NoiDung;

                        Db.TinTucs.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();
                        TempData["notice"] = "Sửa thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tiêu đề đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.TinTucs.FirstOrDefault(x => x.MaTinTuc == id);
                Db.TinTucs.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.TinTucs.Remove(model);
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