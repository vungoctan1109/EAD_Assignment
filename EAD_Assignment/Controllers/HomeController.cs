using EAD_Assignment.Data;
using EAD_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;

namespace EAD_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult Index()
        {
            var articles = from s in db.Articles select s;
            articles = articles.OrderBy(s => s.CreatedAt);
            ViewBag.CategoryList = from s in db.Categories select s;
            return View(articles.ToList());
        }

        [HttpPost]
        public ActionResult Index(Article article)
        {
            var data = db.Articles.ToList();
            ViewBag.CategoryList = from s in db.Categories select s;
            return View(data);
        }

        public ActionResult SearchByCategory(int category, int? page)
        {
            var articles = from s in db.Articles where (s.CategoryId == category) orderby (s.CreatedAt) select s;
            ViewBag.CategoryList = from s in db.Categories select s;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(articles.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(string url)
        {
            if (url == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(url);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryList = from s in db.Categories select s;
            ViewBag.RelatedPost = from s in db.Articles where (s.CategoryId == article.CategoryId) select s;

            return View(article);
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