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
    [PemisitonAttribute("Phim")]
    public class PhimController : BaseController
    {
        // GET: Admin/Phim
        public ActionResult Index(string keyword = "", int maTheLoai = 0, int maTrangThai = 0)
        {
            var list = Db.Phims.Where(x => x.TenPhim.Contains(keyword) && (maTheLoai <= 0 || x.TheLoais.Count(y => y.MaTheLoai == maTheLoai) > 0) && (maTrangThai <= 0 || x.TrangThai == maTrangThai)).OrderByDescending(x => x.MaPhim).ToList();

            ViewBag.TextSearch = keyword;
            ViewBag.MaTheLoai = maTheLoai;
            ViewBag.MaTrangThai = maTrangThai;

            return View(list);
        }

        public ActionResult Add()
        {
            return View(new Phim());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Phim model, int[] MaTheLoais)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = Db.Phims.FirstOrDefault(x => x.TenPhim == model.TenPhim);
                    if (obj == null)
                    {
                        model.NgayTao = DateTime.Now;

                        var list = Db.TheLoais.Where(x => MaTheLoais.Contains(x.MaTheLoai)).ToList();
                        model.TheLoais = list;


                        Db.Phims.Add(model);
                        Db.SaveChanges();
                        TempData["notice"] = "Thêm thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tên phim đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.Phims.FirstOrDefault(x => x.MaPhim == id);
                if (model == null)
                {
                    model.MaPhim = 0;
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
        public ActionResult Edit(Phim model, int[] MaTheLoais)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objCheck = Db.Phims.FirstOrDefault(x => x.TenPhim == model.TenPhim && x.MaPhim != model.MaPhim);
                    if (objCheck == null)
                    {
                        var obj = Db.Phims.FirstOrDefault(x => x.MaPhim == model.MaPhim);

                        obj.TheLoais.Clear();

                        obj.TenPhim = model.TenPhim;
                        obj.GioiThieu = model.GioiThieu;
                        obj.NoiDung = model.NoiDung;
                        obj.HinhAnh = model.HinhAnh;
                        obj.VideoGioiThieu = model.VideoGioiThieu;
                        obj.ThoiLuong = model.ThoiLuong;
                        obj.TrangThai = model.TrangThai;
                        obj.NgayKhoiChieu = model.NgayKhoiChieu;

                        var list = Db.TheLoais.Where(x => MaTheLoais.Contains(x.MaTheLoai)).ToList();
                        obj.TheLoais = list;

                        Db.Phims.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();

                        TempData["notice"] = "Sửa thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Tên phim đã tồn tại! Vui lòng chọn tên khác!";
                    }
                }
                catch (Exception ex)
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
                var model = Db.Phims.FirstOrDefault(x => x.MaPhim == id);
                Db.Phims.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.Phims.Remove(model);
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