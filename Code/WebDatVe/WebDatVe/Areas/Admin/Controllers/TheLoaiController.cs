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
    [PemisitonAttribute("Thể loại")]
    public class TheLoaiController : BaseController
    {
        // GET: Admin/TheLoai
        public ActionResult Index(string keyword = "")
        {
            var list = Db.TheLoais.Where(x => x.TenTheLoai.Contains(keyword)).OrderByDescending(x => x.MaTheLoai).ToList();
            ViewBag.TextSearch = keyword;
            return View(list);
        }

        public ActionResult Add()
        {
            return View(new TheLoai());
        }

        [HttpPost]
        public ActionResult Add(TheLoai model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = Db.TheLoais.FirstOrDefault(x => x.TenTheLoai == model.TenTheLoai);
                    if (obj == null)
                    {
                        Db.TheLoais.Add(model);
                        Db.SaveChanges();
                        TempData["notice"] = "Thêm thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tên thể loại đã tồn tại! Vui lòng chọn tên thể loại khác!";
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
                var model = Db.TheLoais.FirstOrDefault(x => x.MaTheLoai == id);
                if (model == null)
                {
                    model.MaTheLoai = 0;
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
        public ActionResult Edit(TheLoai model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objCheck = Db.TheLoais.FirstOrDefault(x => x.TenTheLoai == model.TenTheLoai && x.MaTheLoai != model.MaTheLoai);
                    if (objCheck == null)
                    {
                        var obj = Db.TheLoais.FirstOrDefault(x => x.MaTheLoai == model.MaTheLoai);
                        obj.TenTheLoai = model.TenTheLoai;

                        Db.TheLoais.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();
                        TempData["notice"] = "Sửa thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tên thể loại đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.TheLoais.FirstOrDefault(x => x.MaTheLoai == id);
                Db.TheLoais.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.TheLoais.Remove(model);
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