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
using HtmlAgilityPack;

namespace EAD_Assignment.Controllers
{
    public class SourcesController : Controller
    {
        public static string previewLink;
        private DBContext db = new DBContext();

        // GET: Sources
        public ActionResult Index()
        {
            return View(db.Sources.ToList());
        }

        // GET: Sources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // GET: Sources/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Link,LinkSelector,TitleDetailSelector,ContentDetailSelector,ImageDetailSelector,DescriptionDetailSelector,CreatedAt,UpdatedAt,CategoryId,Status")] Source source)
        {
            if (ModelState.IsValid)
            {
                db.Sources.Add(source);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(source);
        }

        // GET: Sources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Link,LinkSelector,TitleDetailSelector,ContentDetailSelector,ImageDetailSelector,DescriptionDetailSelector,CreatedAt,UpdatedAt,CategoryId,Status")] Source source)
        {
            if (ModelState.IsValid)
            {
                db.Entry(source).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(source);
        }

        // GET: Sources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Source source = db.Sources.Find(id);
            db.Sources.Remove(source);
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

        public ActionResult PreviewSource(string url, string linkSelector)
        {
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var nodeList = doc.QuerySelectorAll(linkSelector);
            HashSet<string> setLink = new HashSet<string>();
            foreach (var node in nodeList)
            {
                var link = node.Attributes["href"].Value;
                if (string.IsNullOrEmpty(link))
                {
                    continue;
                }
                setLink.Add(link);
            }
            previewLink = setLink.First();
            return View(setLink);
        }

        public ActionResult PreviewArticle(string titleSelector, string imgSelector, string descriptionSelector, string detailSelector)
        {
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(previewLink);
            string title = doc.QuerySelector(titleSelector)?.InnerText;
            string description = doc.QuerySelector(descriptionSelector)?.InnerText;
            var image = doc.QuerySelector(imgSelector)?.GetAttributeValue("data-src", string.Empty);
            var contentNode = doc.QuerySelectorAll(detailSelector);
            StringBuilder contentArticle = new StringBuilder();
            foreach (var content in contentNode)
            {
                contentArticle.Append(content.InnerHtml);
            }

            Article article = new Article()
            {
                Url = previewLink,
                Title = title,
                ImageUrl = image,
                Description = description,
                Detail = contentArticle.ToString()
            };
            return View(article);
        }
    }
}