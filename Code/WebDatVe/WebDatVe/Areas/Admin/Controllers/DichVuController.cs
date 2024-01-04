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
    [PemisitonAttribute("Dịch vụ")]
    public class DichVuController : BaseController
    {
        // GET: Admin/TheLoai
        public ActionResult Index(string keyword = "")
        {
            var list = Db.DichVus.Where(x => x.TenDichVu.Contains(keyword)).OrderByDescending(x => x.MaDichVu).ToList();
            ViewBag.TextSearch = keyword;
            return View(list);
        }

        public ActionResult Add()
        {
            return View(new DichVu());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(DichVu model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = Db.DichVus.FirstOrDefault(x => x.TenDichVu == model.TenDichVu);
                    if (obj == null)
                    {
                        model.NgayTao = DateTime.Now;

                        Db.DichVus.Add(model);
                        Db.SaveChanges();
                        TempData["notice"] = "Thêm thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tên dịch vụ đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.DichVus.FirstOrDefault(x => x.MaDichVu == id);
                if (model == null)
                {
                    model.MaDichVu = 0;
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
        public ActionResult Edit(DichVu model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objCheck = Db.DichVus.FirstOrDefault(x => x.TenDichVu == model.TenDichVu && x.MaDichVu != model.MaDichVu);
                    if (objCheck == null)
                    {
                        var obj = Db.DichVus.FirstOrDefault(x => x.MaDichVu == model.MaDichVu);
                        obj.TenDichVu = model.TenDichVu;
                        obj.NoiDung = model.NoiDung;
                        obj.GiaTien = model.GiaTien;
                        obj.HinhAnh = model.HinhAnh;
                        obj.TrangThai = model.TrangThai;

                        Db.DichVus.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();
                        TempData["notice"] = "Sửa thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tên dịch vụ đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.DichVus.FirstOrDefault(x => x.MaDichVu == id);
                Db.DichVus.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.DichVus.Remove(model);
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