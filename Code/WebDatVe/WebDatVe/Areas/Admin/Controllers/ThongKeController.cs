using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Attributes;

namespace WebDatVe.Areas.Admin.Controllers
{

    [AdminAuthorize]
    [PemisitonAttribute("Thống kê")]
    public class ThongKeController : BaseController
    {
        // GET: Admin/ThongKe
        public ActionResult Index()
        {
            return View();
        }
    }
}