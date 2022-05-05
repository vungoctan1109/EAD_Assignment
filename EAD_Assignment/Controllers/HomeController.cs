using EAD_Assignment.Data;
using EAD_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EAD_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult Index()
        {
            var Articles = (from article in db.Articles select article).ToList();
            return View(Articles);
        }

        [HttpPost]
        public ActionResult Index(Article article)
        {
            var data = db.Articles.ToList();
            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}