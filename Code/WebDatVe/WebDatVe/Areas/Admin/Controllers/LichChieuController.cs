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
    [PemisitonAttribute("Lịch chiếu")]
    public class LichChieuController : BaseController
    {
        // GET: Admin/LichChieu
        public ActionResult Index(DateTime? ngayChieu = null, int maRap = 0, int maPhim = 0)
        {
            if (ngayChieu != null)
            {
                ngayChieu = new DateTime(ngayChieu.Value.Year, ngayChieu.Value.Month, ngayChieu.Value.Day);
            }

            var list = Db.LichChieux.Where(x => (ngayChieu == null || x.NgayChieu == ngayChieu) && (maRap == 0 || x.MaRap == maRap) && (maPhim == 0 || x.MaPhim == maPhim)).OrderByDescending(x => x.NgayChieu).ToList();

            ViewBag.NgayChieu = ngayChieu == null ? "" : ngayChieu.Value.ToString("yyyy-MM-dd");
            ViewBag.MaRap = maRap;
            ViewBag.MaPhim = maPhim;

            return View(list);
        }

        public ActionResult Add()
        {
            return View(new LichChieu() { NgayChieu = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Add(LichChieu model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = Db.LichChieux.FirstOrDefault(x => x.NgayChieu == model.NgayChieu && x.MaPhim == model.MaPhim && x.MaRap == model.MaRap);
                    if (obj == null)
                    {
                        Db.LichChieux.Add(model);
                        Db.SaveChanges();
                        TempData["notice"] = "Thêm thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Lịch chiếu phim đã tồn tại! Vui lòng chọn ngày khác khác!";
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
                var model = Db.LichChieux.FirstOrDefault(x => x.MaLichChieu == id);
                if (model == null)
                {
                    model.MaLichChieu = 0;
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
        public ActionResult Edit(LichChieu model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objCheck = Db.LichChieux.FirstOrDefault(x => x.NgayChieu == model.NgayChieu && x.MaPhim == model.MaPhim && x.MaRap == model.MaRap && x.MaLichChieu != model.MaLichChieu);
                    if (objCheck == null)
                    {
                        var obj = Db.LichChieux.FirstOrDefault(x => x.MaLichChieu == model.MaLichChieu);

                        obj.MaRap = model.MaRap;
                        obj.MaPhim = model.MaPhim;
                        obj.NgayChieu = model.NgayChieu;

                        Db.LichChieux.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();
                        TempData["notice"] = "Sửa thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Lịch chiếu đã tồn tại! Vui lòng chọn ngày khác!";
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
                var model = Db.LichChieux.FirstOrDefault(x => x.MaLichChieu == id);
                Db.LichChieux.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.LichChieux.Remove(model);
                Db.SaveChanges();
                TempData["notice"] = "Xóa thành công!";
            }
            catch
            {
                TempData["notice"] = "Xóa không thành công! Nguyên nhân: Có ràng buộc cơ sở dữ liệu!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult AddXuatChieu(int id)
        {
            try
            {
                var lcPhim = Db.LichChieux.FirstOrDefault(x => x.MaLichChieu == id);
                if (lcPhim == null)
                {
                    lcPhim.MaLichChieu = 0;
                }

                return View(new XuatChieu() { MaLichChieu = id, LichChieu = lcPhim });
            }
            catch
            {
                TempData["notice"] = "Dữ liệu không tồn tại!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AddXuatChieu(XuatChieu model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var lcPhim = Db.LichChieux.FirstOrDefault(x => x.MaLichChieu == model.MaLichChieu);
                    model.LichChieu = lcPhim;

                    var objCheck = Db.XuatChieux.FirstOrDefault(x => x.MaLichChieu == model.MaLichChieu && x.MaPhong == model.MaPhong && x.GioChieu == model.GioChieu && x.PhutChieu == model.PhutChieu);
                    if (objCheck == null)
                    {
                        var lichChieu = Db.LichChieux.FirstOrDefault(x => x.MaLichChieu == model.MaLichChieu);
                        var ngayChieu = lichChieu.NgayChieu;
                        var gioChieu = new DateTime(ngayChieu.Year, ngayChieu.Month, ngayChieu.Day, model.GioChieu, model.PhutChieu, 00);
                        var gioKetThuc = new DateTime(ngayChieu.Year, ngayChieu.Month, ngayChieu.Day, model.GioChieu, model.PhutChieu, 00).AddMinutes(lichChieu.Phim.ThoiLuong);
                        //Check phòng đó có xuất chiếu chưa
                        var list = Db.XuatChieux.Where(x => x.MaPhong == model.MaPhong && x.LichChieu.NgayChieu == ngayChieu).ToList();
                        foreach (var item in list)
                        {
                            var gBd = new DateTime(ngayChieu.Year, ngayChieu.Month, ngayChieu.Day, item.GioChieu, item.PhutChieu, 00);
                            var gKt = new DateTime(ngayChieu.Year, ngayChieu.Month, ngayChieu.Day, item.GioChieu, item.PhutChieu, 00).AddMinutes(item.LichChieu.Phim.ThoiLuong);

                            if ((gBd <= gioChieu && gioChieu <= gKt) || (gBd <= gioKetThuc && gioKetThuc <= gKt))
                            {
                                TempData["notice"] = "Thời gian chiếu phim đã có xuất chiếu khác! Vui lòng chọn giờ khác!";
                                return View(model);
                            }
                        }

                        Db.XuatChieux.Add(model);
                        Db.SaveChanges();
                        TempData["notice"] = "Thêm thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Xuất chiếu phim đã tồn tại! Vui lòng chọn giờ khác!";
                    }
                }
                catch
                {
                    TempData["notice"] = "Thêm không thành công!";
                }
            }

            return View(model);
        }

        public ActionResult EditXuatChieu(int id)
        {
            try
            {
                var model = Db.XuatChieux.FirstOrDefault(x => x.MaXuatChieu == id);
                if (model == null)
                {
                    model.MaXuatChieu = 0;
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
        public ActionResult EditXuatChieu(XuatChieu model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var lcPhim = Db.LichChieux.FirstOrDefault(x => x.MaLichChieu == model.MaLichChieu);
                    model.LichChieu = lcPhim;

                    var objCheck = Db.XuatChieux.FirstOrDefault(x => x.MaLichChieu == model.MaLichChieu && x.MaPhong == model.MaPhong && x.GioChieu == model.GioChieu && x.PhutChieu == model.PhutChieu && x.MaXuatChieu != model.MaXuatChieu);
                    if (objCheck == null)
                    {
                        var lichChieu = Db.LichChieux.FirstOrDefault(x => x.MaLichChieu == model.MaLichChieu);
                        var ngayChieu = lichChieu.NgayChieu;
                        var gioChieu = new DateTime(ngayChieu.Year, ngayChieu.Month, ngayChieu.Day, model.GioChieu, model.PhutChieu, 00);
                        var gioKetThuc = new DateTime(ngayChieu.Year, ngayChieu.Month, ngayChieu.Day, model.GioChieu, model.PhutChieu, 00).AddMinutes(lichChieu.Phim.ThoiLuong);
                        //Check phòng đó có xuất chiếu chưa
                        var list = Db.XuatChieux.Where(x => x.MaPhong == model.MaPhong && x.LichChieu.NgayChieu == ngayChieu && x.MaXuatChieu != model.MaXuatChieu).ToList();
                        foreach (var item in list)
                        {
                            var gBd = new DateTime(ngayChieu.Year, ngayChieu.Month, ngayChieu.Day, item.GioChieu, item.PhutChieu, 00);
                            var gKt = new DateTime(ngayChieu.Year, ngayChieu.Month, ngayChieu.Day, item.GioChieu, item.PhutChieu, 00).AddMinutes(item.LichChieu.Phim.ThoiLuong);

                            if ((gBd <= gioChieu && gioChieu <= gKt) || (gBd <= gioKetThuc && gioKetThuc <= gKt) || (gioChieu <= gBd && gBd <= gioKetThuc) || (gioChieu <= gKt && gKt <= gioKetThuc))
                            {
                                TempData["notice"] = "Thời gian chiếu phim đã có xuất chiếu khác! Vui lòng chọn giờ khác!";
                                return View(model);
                            }
                        }

                        var obj = Db.XuatChieux.FirstOrDefault(x => x.MaXuatChieu == model.MaXuatChieu);

                        obj.MaPhong = model.MaPhong;
                        obj.GioChieu = model.GioChieu;
                        obj.PhutChieu = model.PhutChieu;
                        obj.LoaiChieuPhim = model.LoaiChieuPhim;

                        Db.XuatChieux.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();
                        TempData["notice"] = "Sửa thành công!";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Xuất chiếu phim đã tồn tại! Vui lòng chọn giờ khác!";
                    }
                }
                catch
                {
                    TempData["notice"] = "Sửa không thành công!";
                }
            }
            return View(model);
        }

        public ActionResult DeleteXuatChieu(int id)
        {
            try
            {
                var model = Db.XuatChieux.FirstOrDefault(x => x.MaXuatChieu == id);
                Db.XuatChieux.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.XuatChieux.Remove(model);
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