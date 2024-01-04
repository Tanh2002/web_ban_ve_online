using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Attributes;
using WebDatVe.Hepper;
using WebDatVe.Models;

namespace WebDatVe.Areas.Admin.Controllers
{
    [AdminAuthorize]
    [PemisitonAttribute("Ghế")]
    public class GheController : BaseController
    {
        // GET: Admin/TheLoai
        public ActionResult Index(string keyword = "", int maRap = 0)
        {
            var list = Db.Phongs.Where(x => x.TenPhong.Contains(keyword) && (maRap <= 0 || x.MaRap == maRap)).OrderByDescending(x => x.MaPhong).ToList();
            ViewBag.TextSearch = keyword;
            ViewBag.MaRap = maRap;
            return View(list);
        }

        public ActionResult Add(int id)
        {
            try
            {
                var phong = Db.Phongs.FirstOrDefault(x => x.MaPhong == id);
                if (phong == null)
                {
                    phong.MaPhong = 0;
                }

                var model = new Ghe();
                model.MaPhong = id;

                return View(model);
            }
            catch
            {
                TempData["notice"] = "Dữ liệu không tồn tại!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Add(Ghe model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objCheck = Db.Ghes.FirstOrDefault(x => x.TenGhe == model.TenGhe && x.MaPhong == model.MaPhong);
                    if (objCheck == null)
                    {
                        if (model.LoaiGhe != (int)LoaiGhe.GheDoi)
                        {
                            objCheck = Db.Ghes.FirstOrDefault(x => x.SoHang == model.SoHang && x.SoO == model.SoO && x.MaPhong == model.MaPhong);
                            if (objCheck == null)
                            {
                                Db.Ghes.Add(model);
                                Db.SaveChanges();
                                TempData["notice"] = "Thêm thành công!";

                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["notice"] = "Vị trí ghế đã tồn tại! Vui lòng chọn vị trí khác!";
                            }
                        }
                        else
                        {
                            if (model.SoO >= ConfigData.OGheMax)
                            {
                                TempData["notice"] = "Ghế đôi không được chọn ô cuối! Vui lòng chọn vị trí khác!";
                            }
                            else
                            {
                                objCheck = Db.Ghes.FirstOrDefault(x => x.SoHang == model.SoHang && x.SoO == model.SoO + 1 && x.MaPhong == model.MaPhong);
                                if (objCheck == null)
                                {
                                    Db.Ghes.Add(model);
                                    Db.SaveChanges();
                                    TempData["notice"] = "Thêm thành công!";

                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    TempData["notice"] = "Vị trí ghế đã tồn tại! Vui lòng chọn vị trí khác!";
                                }
                            }
                        }
                    }
                    else
                    {
                        TempData["notice"] = "Tên ghế đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.Ghes.FirstOrDefault(x => x.MaGhe == id);
                if (model == null)
                {
                    model.MaGhe = 0;
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
        public ActionResult Edit(Ghe model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objCheck = Db.Ghes.FirstOrDefault(x => x.TenGhe == model.TenGhe && x.MaPhong == model.MaPhong && x.MaGhe != model.MaGhe);
                    if (objCheck == null)
                    {
                        if (model.LoaiGhe != (int)LoaiGhe.GheDoi)
                        {
                            objCheck = Db.Ghes.FirstOrDefault(x => x.SoHang == model.SoHang && x.SoO == model.SoO && x.MaPhong == model.MaPhong && x.MaGhe != model.MaGhe);
                            if (objCheck == null)
                            {
                                var obj = Db.Ghes.FirstOrDefault(x => x.MaGhe == model.MaGhe);
                                obj.MaPhong = model.MaPhong;
                                obj.TenGhe = model.TenGhe;
                                obj.LoaiGhe = model.LoaiGhe;
                                obj.SoHang = model.SoHang;
                                obj.SoO = model.SoO;

                                Db.Ghes.Attach(obj);
                                Db.Entry(obj).State = EntityState.Modified;
                                Db.SaveChanges();
                                TempData["notice"] = "Sửa thành công!";

                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["notice"] = "Vị trí ghế đã tồn tại! Vui lòng chọn vị trí khác!";
                            }
                        }
                        else
                        {
                            if (model.SoO >= ConfigData.OGheMax)
                            {
                                TempData["notice"] = "Ghế đôi không được chọn ô cuối! Vui lòng chọn vị trí khác!";
                            }
                            else
                            {
                                objCheck = Db.Ghes.FirstOrDefault(x => x.SoHang == model.SoHang && x.SoO == model.SoO + 1 && x.MaPhong == model.MaPhong && x.MaGhe != model.MaGhe);
                                if (objCheck == null)
                                {
                                    var obj = Db.Ghes.FirstOrDefault(x => x.MaGhe == model.MaGhe);
                                    obj.MaPhong = model.MaPhong;
                                    obj.TenGhe = model.TenGhe;
                                    obj.LoaiGhe = model.LoaiGhe;
                                    obj.SoHang = model.SoHang;
                                    obj.SoO = model.SoO;

                                    Db.Ghes.Attach(obj);
                                    Db.Entry(obj).State = EntityState.Modified;
                                    Db.SaveChanges();
                                    TempData["notice"] = "Sửa thành công!";

                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    TempData["notice"] = "Vị trí ghế đã tồn tại! Vui lòng chọn vị trí khác!";
                                }
                            }
                        }
                    }
                    else
                    {
                        TempData["notice"] = "Tên ghế đã tồn tại! Vui lòng chọn tên khác!";
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
                var model = Db.Ghes.FirstOrDefault(x => x.MaPhong == id);
                Db.Ghes.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.Ghes.Remove(model);
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