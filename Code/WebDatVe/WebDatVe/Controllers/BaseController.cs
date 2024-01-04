using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Models;

namespace WebDatVe.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected static WebDatVePhimEntities Db;

        public BaseController()
        {
            Db = new WebDatVePhimEntities();
        }
    }
}