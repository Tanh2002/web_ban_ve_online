using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Attributes;

namespace WebDatVe.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class BangDieuKhienController : BaseController
    {
        // GET: Admin/BangDieuKhien
        public ActionResult Index()
        {
            return View();
        }
    }
}