using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDatVe.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Home = "active";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.About = "active";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Contact = "active";
            return View();
        }

        public ActionResult TermsOfUse()
        {
            ViewBag.TermsOfUse = "active";
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            ViewBag.PrivacyPolicy = "active";
            return View();
        }
    }
}