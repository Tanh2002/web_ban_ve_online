using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Attributes;
using WebDatVe.Models;

namespace WebDatVe.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected static WebDatVePhimEntities Db;

        public BaseController()
        {
            Db = new WebDatVePhimEntities();
        }

        [AdminAuthorize]
        public ActionResult Logout()
        {
            HttpCookie cookie = Request.Cookies.Get("HoTenAdmin");
            cookie.Expires = DateTime.Now;
            Response.Cookies.Add(cookie);

            HttpCookie maTaiKhoan = Request.Cookies.Get("MaAdmin");
            maTaiKhoan.Expires = DateTime.Now;
            Response.Cookies.Add(maTaiKhoan);
            return Redirect("/Admin/DangNhap");
        }
    }
}