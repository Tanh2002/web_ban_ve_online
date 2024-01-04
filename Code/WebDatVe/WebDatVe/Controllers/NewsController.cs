using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDatVe.Controllers
{
    public class NewsController : BaseController
    {
        // GET: News
        public ActionResult Index(int page = 0)
        {
            ViewBag.News = "active";
            if (page > 0)
            {
                page = page - 1;
            }
            ViewBag.Page = page;
            var skip = 5 * page;
            var data = Db.TinTucs.OrderByDescending(x => x.NgayTao).Skip(skip).Take(5).ToList();
            return View(data);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.News = "active";
            try
            {
                var data = Db.TinTucs.FirstOrDefault(x => x.MaTinTuc == id);
                return View(data);
            }
            catch
            {
                return Redirect("/news");
            }
        }
    }
}