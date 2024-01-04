using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDatVe.Controllers
{
    public class CinemaController : BaseController
    {
        // GET: Cinema
        public ActionResult Index()
        {
            ViewBag.Cinema = "active";
            var data = Db.Raps.OrderBy(x => x.TenRap).ToList();
            return View(data);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.Cinema = "active";
            try
            {
                var data = Db.Raps.FirstOrDefault(x => x.MaRap == id);
                return View(data);
            }
            catch
            {
                return Redirect("/cinema");
            }
        }
    }
}