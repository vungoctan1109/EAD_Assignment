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
        public ActionResult Index(string sortType, string keyword, int? categoryId, int? page)
        {
            ViewBag.ListCategory = db.Categories.ToList();
            ViewBag.CategoryID = categoryId;
            ViewBag.SortType = sortType;
            ViewBag.Keyword = keyword;
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            //ElasticSearch h
            List<Article> list = new List<Article>();
            var searchRequest = new SearchRequest<Article>();
            searchRequest.From = 0;
            searchRequest.Size = 10000;
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
            if (categoryId != null)
            {
                listQuery.Add(new TermQuery { Field = "categoryId", Value = categoryId });
            }
            searchRequest.Query = new QueryContainer(new BoolQuery
            {
                Must = listQuery
            });

            if (("createdAt_asc").Equals(sortType))
            {
                searchRequest.Sort = new List<ISort>
                {
                    new FieldSort { Field = "createdAt", Order = SortOrder.Ascending }
                };
            }
            else
            {
                //ES  Sort by field typed non-string a.k.a non-analyzed text field
                searchRequest.Sort = new List<ISort>
                {
                    new FieldSort { Field = "createdAt", Order = SortOrder.Descending }
                };
            }
            //ES Sort by field typed string a.k.a analyzed text field -> them .keyword
            //searchRequest.Sort = new List<ISort>
            //{
            //    new FieldSort { Field = "url.keyword", Order = SortOrder.Descending }
            //};
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
        [ValidateInput(false)]
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
        [ValidateInput(false)]
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