using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatVe.Attributes;
using WebDatVe.Hepper;
using WebDatVe.Models;

namespace WebDatVe.Controllers
{
    [CustomerAuthorize]
    public class BookingController : BaseController
    {
        // GET: Booking
        public ActionResult Index(int movies = 0, int cinema = 0)
        {
            try
            {
                if (cinema == 0)
                {
                    cinema = Db.Raps.FirstOrDefault().MaRap;
                }

                ViewBag.movies = movies;
                ViewBag.cinema = cinema;

                var data = Db.Phims.FirstOrDefault(x => x.MaPhim == movies);
                if (data == null || data.MaPhim == 0)
                {
                    return Redirect("/movies");
                }
                return View(data);
            }
            catch
            {
                return Redirect("/movies");
            }
        }

        public ActionResult SeatPlan(int movies = 0, int cinema = 0, int screen = 0)
        {
            try
            {
                if (movies <= 0 || cinema <= 0 || screen <= 0)
                {
                    return Redirect("/movies");
                }

                ViewBag.movies = movies;
                ViewBag.cinema = cinema;

                var data = Db.XuatChieux.FirstOrDefault(x => x.MaXuatChieu == screen);
                if (data == null || data.MaXuatChieu == 0)
                {
                    return Redirect("/booking?movies=" + movies + "&cinema=" + cinema);
                }

                return View(data);
            }
            catch
            {
                return Redirect("/movies");
            }
        }

        public ActionResult Popcorn(int movies = 0, int cinema = 0, int screen = 0, string place = "", string service = "")
        {
            try
            {
                if (movies <= 0 || cinema <= 0 || screen <= 0 || string.IsNullOrEmpty(place))
                {
                    return Redirect("/movies");
                }

                var dsGhe = place.Split(',').Select(int.Parse).ToList();
                var dsDichVu = new List<int>();


                if (!string.IsNullOrEmpty(service))
                    dsDichVu = service.Split(',').Select(int.Parse).ToList();

                var tienve = 0;
                var tiendichvu = 0;
                var tongtien = 0;

                ViewBag.movies = movies;
                ViewBag.cinema = cinema;
                ViewBag.place = place;
                ViewBag.service = service;
                ViewBag.count = dsGhe.Count.ToString("00");


                var ghes = Db.Ghes.Where(x => dsGhe.Contains(x.MaGhe)).ToList();
                foreach (var ghe in ghes)
                {
                    if (ghe.LoaiGhe == (int)LoaiGhe.GheThuong)
                    {
                        tienve += ConfigData.VeGheThuong;
                    }

                    if (ghe.LoaiGhe == (int)LoaiGhe.GheVip)
                    {
                        tienve += ConfigData.VeGheVip;
                    }

                    if (ghe.LoaiGhe == (int)LoaiGhe.GheDoi)
                    {
                        tienve += ConfigData.VeGheDoi;
                    }
                }

                var tenDVs = new List<string>();
                foreach (var item in dsDichVu)
                {
                    var dv = Db.DichVus.FirstOrDefault(x => x.MaDichVu == item);
                    if (dv != null && dv.MaDichVu > 0)
                    {
                        tiendichvu += dv.GiaTien;
                        tenDVs.Add(dv.TenDichVu);
                    }
                }
                ViewBag.tenDVs = tenDVs;

                tongtien = tienve + tiendichvu;

                ViewBag.tienve = tienve.ToString("N0");
                ViewBag.tiendichvu = tiendichvu.ToString("N0");
                ViewBag.tongtien = tongtien.ToString("N0");


                var data = Db.XuatChieux.FirstOrDefault(x => x.MaXuatChieu == screen);
                if (data == null || data.MaXuatChieu == 0)
                {
                    return Redirect("/booking?movies=" + movies + "&cinema=" + cinema);
                }

                return View(data);
            }
            catch
            {
                return Redirect("/movies");
            }
        }

        public ActionResult Checkout(int movies = 0, int cinema = 0, int screen = 0, string place = "", string service = "")
        {
            try
            {
                if (movies <= 0 || cinema <= 0 || screen <= 0 || string.IsNullOrEmpty(place))
                {
                    return Redirect("/movies");
                }

                var dsGhe = place.Split(',').Select(int.Parse).ToList();
                var dsDichVu = new List<int>();


                if (!string.IsNullOrEmpty(service))
                    dsDichVu = service.Split(',').Select(int.Parse).ToList();

                var tienve = 0;
                var tiendichvu = 0;
                var tongtien = 0;

                ViewBag.movies = movies;
                ViewBag.cinema = cinema;
                ViewBag.place = place;
                ViewBag.service = service;
                ViewBag.count = dsGhe.Count.ToString("00");

                var ghes = Db.Ghes.Where(x => dsGhe.Contains(x.MaGhe)).ToList();
                foreach (var ghe in ghes)
                {
                    if (ghe.LoaiGhe == (int)LoaiGhe.GheThuong)
                    {
                        tienve += ConfigData.VeGheThuong;
                    }

                    if (ghe.LoaiGhe == (int)LoaiGhe.GheVip)
                    {
                        tienve += ConfigData.VeGheVip;
                    }

                    if (ghe.LoaiGhe == (int)LoaiGhe.GheDoi)
                    {
                        tienve += ConfigData.VeGheDoi;
                    }
                }

                var tenDVs = new List<string>();
                foreach (var item in dsDichVu)
                {
                    var dv = Db.DichVus.FirstOrDefault(x => x.MaDichVu == item);
                    if (dv != null && dv.MaDichVu > 0)
                    {
                        tiendichvu += dv.GiaTien;
                        tenDVs.Add(dv.TenDichVu);
                    }
                }
                ViewBag.tenDVs = tenDVs;

                tongtien = tienve + tiendichvu;

                ViewBag.tienve = tienve.ToString("N0");
                ViewBag.tiendichvu = tiendichvu.ToString("N0");
                ViewBag.tongtien = tongtien.ToString("N0");

                var data = Db.XuatChieux.FirstOrDefault(x => x.MaXuatChieu == screen);
                if (data == null || data.MaXuatChieu == 0)
                {
                    return Redirect("/booking?movies=" + movies + "&cinema=" + cinema);
                }

                return View(data);
            }
            catch
            {
                return Redirect("/movies");
            }
        }

        [HttpPost]
        public ActionResult Checkout(int movies = 0, int cinema = 0, int screen = 0, string place = "", string service = "", string card = "")
        {
            try
            {
                if (movies <= 0 || cinema <= 0 || screen <= 0 || string.IsNullOrEmpty(place))
                {
                    return Redirect("/movies");
                }

                var dsGhe = place.Split(',').Select(int.Parse).ToList();
                var dsDichVu = new List<int>();


                if (!string.IsNullOrEmpty(service))
                    dsDichVu = service.Split(',').Select(int.Parse).ToList();

                var tienve = 0;
                var tiendichvu = 0;
                var tongtien = 0;

                ViewBag.movies = movies;
                ViewBag.cinema = cinema;
                ViewBag.place = place;
                ViewBag.service = service;
                ViewBag.count = dsGhe.Count.ToString("00");

                var ghes = Db.Ghes.Where(x => dsGhe.Contains(x.MaGhe)).ToList();
                foreach (var ghe in ghes)
                {
                    if (ghe.LoaiGhe == (int)LoaiGhe.GheThuong)
                    {
                        tienve += ConfigData.VeGheThuong;
                    }

                    if (ghe.LoaiGhe == (int)LoaiGhe.GheVip)
                    {
                        tienve += ConfigData.VeGheVip;
                    }

                    if (ghe.LoaiGhe == (int)LoaiGhe.GheDoi)
                    {
                        tienve += ConfigData.VeGheDoi;
                    }
                }

                var tenDVs = new List<string>();
                foreach (var item in dsDichVu)
                {
                    var dv = Db.DichVus.FirstOrDefault(x => x.MaDichVu == item);
                    if (dv != null && dv.MaDichVu > 0)
                    {
                        tiendichvu += dv.GiaTien;
                        tenDVs.Add(dv.TenDichVu);
                    }
                }
                ViewBag.tenDVs = tenDVs;

                tongtien = tienve + tiendichvu;

                ViewBag.tienve = tienve.ToString("N0");
                ViewBag.tiendichvu = tiendichvu.ToString("N0");
                ViewBag.tongtien = tongtien.ToString("N0");


                var data = Db.XuatChieux.FirstOrDefault(x => x.MaXuatChieu == screen);
                if (data == null || data.MaXuatChieu == 0)
                {
                    return Redirect("/booking?movies=" + movies + "&cinema=" + cinema);
                }

                HttpCookie cookie = Request.Cookies.Get("MaKhach");
                var ma = int.Parse(cookie.Value);

                var donhang = new DonHang()
                {
                    MaKhachHang = ma,
                    NgayDat = DateTime.Now,
                    TrangThai = (int)TrangThaiDonHang.Moi,
                    ThanhTien = tongtien
                };

                Db.DonHangs.Add(donhang);
                Db.SaveChanges();

                foreach (var ghe in ghes)
                {
                    var chitietdonhang = new ChiTietDonHang()
                    {
                        MaDonHang = donhang.MaDonHang,
                        MaGhe = ghe.MaGhe,
                        MaXuatChieu = screen
                    };
                    if (ghe.LoaiGhe == (int)LoaiGhe.GheThuong)
                    {
                        chitietdonhang.GiaVe = ConfigData.VeGheThuong;
                    }

                    if (ghe.LoaiGhe == (int)LoaiGhe.GheVip)
                    {
                        chitietdonhang.GiaVe = ConfigData.VeGheVip;
                    }

                    if (ghe.LoaiGhe == (int)LoaiGhe.GheDoi)
                    {
                        chitietdonhang.GiaVe = ConfigData.VeGheDoi;
                    }

                    Db.ChiTietDonHangs.Add(chitietdonhang);
                }

                Db.SaveChanges();

                foreach (var item in dsDichVu.GroupBy(x => x))
                {
                    var dv = Db.DichVus.FirstOrDefault(x => x.MaDichVu == item.Key);
                    if (dv != null && dv.MaDichVu > 0)
                    {
                        var donhangdichvu = new DonHangDichVu()
                        {
                            MaDonHang = donhang.MaDonHang,
                            MaDichVu = dv.MaDichVu,
                            SoLuong = item.Count(),
                            DonGia = dv.GiaTien
                        };

                        Db.DonHangDichVus.Add(donhangdichvu);
                    }
                }
                Db.SaveChanges();

                return Redirect("/account/orderdetail/" + donhang.MaDonHang);
            }
            catch
            {
                return Redirect("/movies");
            }
        }
    }
}