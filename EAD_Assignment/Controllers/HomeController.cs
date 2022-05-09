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
using Nest;
using EAD_Assignment.Service;
using System.Diagnostics;

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
        public ActionResult Index(string keyword)
        {
            var data = db.Articles.ToList();
            ViewBag.CategoryList = from s in db.Categories select s;
            return Redirect("/Home/Search?keyword=" + keyword);
        }
        public ActionResult Search(string keyword, int? page)
        {
            ViewBag.Keyword = keyword;
            ViewBag.CategoryList = from s in db.Categories select s;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //ElasticSearch h
            List<Article> list = new List<Article>();
            var searchRequest = new SearchRequest<Article>();
            searchRequest.From = 0;
            //Must
            //{
            //  Category,
            //  Should
            //        {
            //          Url=keyword,
            //          Detail=keyword
            //        }
            //}
            var listQuery = new List<QueryContainer>();
            if (!string.IsNullOrEmpty(keyword))
            {
                var query = new BoolQuery
                {
                    Should = new List<QueryContainer>
                        {
                             new MatchQuery{ Field = "url", Query = keyword},
                             new MatchQuery{ Field = "detail", Query = keyword}
                        }
                };
                listQuery.Add(query);
            }
            listQuery.Add(new TermQuery { Field = "categoryId", Value = 1 });
            searchRequest.Query = new QueryContainer(new BoolQuery
            {
                Must = listQuery
            });
            var searchResult =
                ElasticSearchService.GetInstance().Search<Article>(searchRequest);
            list = searchResult.Documents.ToList();
            Debug.WriteLine("Elastic search: " + searchResult.Total + " Result" + list.Count());
            return View(list.ToPagedList(pageNumber, pageSize));
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