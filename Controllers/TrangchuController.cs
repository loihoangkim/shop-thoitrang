using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopThoiTrang.Models;

namespace ShopThoiTrang.Controllers
{
    public class TrangchuController : Controller
    {
        private ShopThoiTrangDBContext db = new ShopThoiTrangDBContext();
        // GET: Trangchu
        public ActionResult Index()
        {
            ViewBag.SoMauTin = db.Products.Count();
            return View();
        }
    }
}