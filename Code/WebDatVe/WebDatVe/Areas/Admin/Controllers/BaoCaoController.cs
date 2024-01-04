using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Attributes;

namespace WebDatVe.Areas.Admin.Controllers
{
    [AdminAuthorize]
    [PemisitonAttribute("Báo cáo")]
    public class BaoCaoController : BaseController
    {
        // GET: Admin/BaoCao
        public ActionResult Index()
        {
            return View();
        }
    }
}