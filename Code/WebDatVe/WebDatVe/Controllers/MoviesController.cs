using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Models;

namespace WebDatVe.Controllers
{
    public class MoviesController : BaseController
    {
        // GET: Movies
        public ActionResult Index(string keyword = "", int category = 0, int cinema = 0)
        {
            ViewBag.Movies = "active";
            try
            {

                if (cinema == 0)
                {
                    cinema = Db.Raps.FirstOrDefault().MaRap;
                }

                ViewBag.keyword = keyword;
                ViewBag.category = category;
                ViewBag.cinema = cinema;
                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month;
                var day = DateTime.Now.Day;
                var ngay = new DateTime(year, month, day);
                var lichchieu = Db.LichChieux.Where(x => x.NgayChieu >= ngay && x.MaRap == cinema).ToList();
                var data = new List<Phim>();
                foreach (var item in lichchieu)
                {
                    data.Add(item.Phim);
                }

                data = data.Distinct().ToList();

                if (category > 0)
                    data = data.Where(x => x.TenPhim.Contains(keyword) && x.TheLoais.Count(y => y.MaTheLoai == category) > 0).OrderByDescending(x => x.NgayKhoiChieu).ToList();
                else
                    data = data.Where(x => x.TenPhim.Contains(keyword)).OrderByDescending(x => x.NgayKhoiChieu).ToList();
                return View(data);
            }
            catch
            {
                return Redirect("/");
            }
        }

        public ActionResult Detail(int id, int cinema = 0)
        {
            ViewBag.Movies = "active";
            ViewBag.cinema = cinema;
            try
            {
                var data = Db.Phims.FirstOrDefault(x => x.MaPhim == id);
                return View(data);
            }
            catch
            {
                return Redirect("/movies");
            }
        }
    }
}