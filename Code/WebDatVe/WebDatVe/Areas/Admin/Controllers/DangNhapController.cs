using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Models.Meta;

namespace WebDatVe.Areas.Admin.Controllers
{
    public class DangNhapController : BaseController
    {
        // GET: Admin/DangNhap
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            HttpCookie cookie = Request.Cookies.Get("HoTenAdmin");
            if (cookie != null)
            {
                return Redirect("/Admin/BangDieuKhien");
            }
            return View(new DangNhapMeta());
        }

        [HttpPost]
        public ActionResult Index(DangNhapMeta model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var taiKhoan = Db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == model.TaiKhoan && x.MatKhau == model.MatKhau);

                    if (taiKhoan != null)
                    {
                        HttpCookie cookie = new HttpCookie("HoTenAdmin", Server.UrlEncode(taiKhoan.HoTen));
                        cookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(cookie);

                        HttpCookie maTaiKhoan = new HttpCookie("MaAdmin", taiKhoan.MaTaiKhoan.ToString());
                        maTaiKhoan.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(maTaiKhoan);

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return Redirect("/Admin/DangNhap");
                    }
                    else
                    {
                        ModelState.AddModelError("MatKhau", "Sai tên đăng nhập hoặc mật khẩu");
                        return View(model);
                    }
                }
                catch
                {
                    ModelState.AddModelError("MatKhau", "Đăng nhập không thành công");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}