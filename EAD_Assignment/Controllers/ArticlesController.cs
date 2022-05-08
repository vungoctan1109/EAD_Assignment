using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EAD_Assignment.Data;
using EAD_Assignment.Models;
using EAD_Assignment.Service;
using HtmlAgilityPack;
using Nest;
using PagedList;

namespace EAD_Assignment.Controllers
{
    public class ArticlesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Articles
        public ActionResult Index(string sort, string keyword, string currentKeyword, int? categoryId, int? page)
        {
            int pageNumber = (page ?? 1);
            keyword = keyword ?? currentKeyword;
            int pageSize = 10;
            ViewBag.ListCategory = db.Categories.ToList();
            ViewBag.Sort = sort;
            ViewBag.CurrentKeyword = keyword;
            //ElasticSearch h
            List<Article> list = new List<Article>();
            var searchRequest = new SearchRequest<Article>()
            {
                From = pageSize * (pageNumber - 1),
                QueryOnQueryString = keyword
            };
            if (categoryId != null)
            {
                searchRequest.Query = new TermQuery
                {
                    Field = "categoryId",
                    Value = categoryId
                };                         
            }
            if (sort == "createdAt_asc")
            {
                searchRequest.Sort = new List<ISort>
                {
                    new FieldSort { Field = "createdAt", Order = SortOrder.Ascending }
                };
            }
            searchRequest.Sort = new List<ISort>
            {
                new FieldSort { Field = "createdAt", Order = SortOrder.Descending }
            };

            var searchResult =
                ElasticSearchService.GetInstance().Search<Article>(searchRequest);
            list = searchResult.Documents.ToList();
            
            ViewBag.CategoryId = categoryId;
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Articles/Details/5
        public ActionResult Details(string Url)
        {
            if (Url == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(Url);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public ActionResult Preview(string Url)
        {
            if (Url == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(Url);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Url,Title,ImageUrl,Detail,Description,Author,CreatedAt,UpdatedAt,CategoryId,Status")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(string Url)
        {
            if (Url == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(Url);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Url,Title,ImageUrl,Detail,Description,Author,CreatedAt,UpdatedAt,CategoryId,Status")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}