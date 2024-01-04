using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Attributes;
using WebDatVe.Models;

namespace WebDatVe.Controllers
{
    public class AccountController : BaseController
    {
        [CustomerAuthorize]
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies.Get("MaKhach");
            var ma = int.Parse(cookie.Value);
            var data = Db.KhachHangs.FirstOrDefault(x => x.MaKhachHang == ma);
            return View(data);
        }
        [CustomerAuthorize]
        [HttpPost]
        public ActionResult Index(KhachHang model)
        {
            try
            {
                HttpCookie cookied = Request.Cookies.Get("MaKhach");
                var ma = int.Parse(cookied.Value);
                var obj = Db.KhachHangs.FirstOrDefault(x => x.MaKhachHang == ma);
                obj.HoTen = model.HoTen;
                obj.Email = model.Email;
                obj.SoDienThoai = model.SoDienThoai;
                obj.CMND = model.CMND;

                Db.KhachHangs.Attach(obj);
                Db.Entry(obj).State = EntityState.Modified;
                Db.SaveChanges();
                TempData["notice"] = "Cập nhật thành công!";

                HttpCookie cookie = new HttpCookie("HoTenKhach", Server.UrlEncode(obj.HoTen));
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);

                HttpCookie maKhach = new HttpCookie("MaKhach", obj.MaKhachHang.ToString());
                maKhach.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(maKhach);
            }
            catch
            {
                TempData["notice"] = "Cập nhật không thành công!";
            }

            return RedirectToAction("Index");
        }

        [CustomerAuthorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [CustomerAuthorize]
        [HttpPost]
        public ActionResult ChangePassword(string passcu, string passmoi, string nhaplaipassmoi)
        {
            try
            {
                HttpCookie cookied = Request.Cookies.Get("MaKhach");
                var ma = int.Parse(cookied.Value);
                var obj = Db.KhachHangs.FirstOrDefault(x => x.MaKhachHang == ma && x.MatKhau == passcu);
                if (obj == null || obj.MaKhachHang == 0)
                {
                    TempData["notice"] = "Mật khẩu cũ không đúng!";
                    return View();
                }
                if (passmoi != nhaplaipassmoi)
                {
                    TempData["notice"] = "Xác nhận mật khẩu không khớp!";
                    return View();
                }
                obj.MatKhau = passmoi;

                Db.KhachHangs.Attach(obj);
                Db.Entry(obj).State = EntityState.Modified;
                Db.SaveChanges();
                TempData["notice"] = "Đổi pass thành công!";
            }
            catch
            {
                TempData["notice"] = "Đổi pass không thành công!";
            }

            return RedirectToAction("changepassword");
        }

        public ActionResult Login()
        {
            HttpCookie cookie = Request.Cookies.Get("HoTenKhach");
            if (cookie != null)
            {
                return Redirect("/account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachhang = Db.KhachHangs.FirstOrDefault(x => x.TenDangNhap == username && x.MatKhau == password);

                    if (khachhang != null)
                    {
                        HttpCookie cookie = new HttpCookie("HoTenKhach", Server.UrlEncode(khachhang.HoTen));
                        cookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(cookie);

                        HttpCookie maKhach = new HttpCookie("MaKhach", khachhang.MaKhachHang.ToString());
                        maKhach.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(maKhach);

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return Redirect("/account/login");
                    }
                    else
                    {
                        TempData["notice"] = "Tài khoản hoặc mật khẩu không đúng!";
                        return View();
                    }
                }
                catch
                {
                    TempData["notice"] = "Đăng nhập không thành công!";
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            HttpCookie cookie = Request.Cookies.Get("HoTenKhach");
            if (cookie != null)
            {
                return Redirect("/account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(KhachHang model)
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

                        return Redirect("/account");
                    }
                    else
                    {
                        TempData["notice"] = "Số điện thoại hoặc tên đăng nhập đã tồn tại! Vui lòng chọn thông tin khác!";
                    }
                }
                catch
                {
                    TempData["notice"] = "Đăng ký không thành công!";
                }
            }
            return View(model);
        }

        [CustomerAuthorize]
        public ActionResult LogOut()
        {
            try
            {
                HttpCookie cookie = Request.Cookies.Get("HoTenKhach");
                cookie.Expires = DateTime.Now;
                Response.Cookies.Add(cookie);

                HttpCookie makhach = Request.Cookies.Get("MaKhach");
                makhach.Expires = DateTime.Now;
                Response.Cookies.Add(makhach);
            }
            catch
            {

            }
            return Redirect("/");
        }

        [CustomerAuthorize]
        public ActionResult Order()
        {
            try
            {
                HttpCookie cookie = Request.Cookies.Get("MaKhach");
                var ma = int.Parse(cookie.Value);
                var data = Db.DonHangs.Where(x => x.MaKhachHang == ma).OrderByDescending(x => x.NgayDat).ToList();
                return View(data);
            }
            catch
            {
                return Redirect("/");
            }
        }

        [CustomerAuthorize]
        public ActionResult OrderDetail(int id)
        {
            try
            {
                HttpCookie cookie = Request.Cookies.Get("MaKhach");
                var ma = int.Parse(cookie.Value);
                var data = Db.DonHangs.FirstOrDefault(x => x.MaKhachHang == ma && x.MaDonHang == id);
                if (data == null && data.MaDonHang == 0)
                {
                    return Redirect("/account/order");
                }
                return View(data);
            }catch
            {
                return Redirect("/account/order");
            }
        }
    }
}