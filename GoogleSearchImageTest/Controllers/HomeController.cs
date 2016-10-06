using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoogleSearchImageTest.Models;

namespace GoogleSearchImageTest.Controllers
{
    public class HomeController : Controller
    {
        private GoogleSearchImageTestContext db = new GoogleSearchImageTestContext();

        // GET: SearchResults
        public ActionResult Index()
        {
            var results = new SearchResult[]
                        {
             new SearchResult()
             {
                 Id = 1,
                 Name = "res#1",
                 SearchDate = DateTime.Now,
                 Items = new List<SearchResultItem>()
                 {
                     new SearchResultItem()
                     {
                         Id = 1,
                         Title = "yaaahooo",
                         HtmlTitle = @"<span>YH</span>",
                         FileName = "yaay.jpg",
                         Src = @"http://ddd.com/yaay.jpg",
                         Deleted = false,
                         SearchResultId = 1
                     },
                     new SearchResultItem()
                     {
                         Id = 2,
                         Title = "yaaahooo2",
                         HtmlTitle = @"<span>YH2</span>",
                         FileName = "yaay2.jpg",
                         Src = @"http://ddd.com/yaay2.jpg",
                         Deleted = false,
                         SearchResultId = 1
                     }
                 }
             },
             new SearchResult()
             {
                 Id = 2,
                 Name = "res#2",
                 SearchDate = DateTime.Now,
                 Items = new List<SearchResultItem>()
                 {
                     new SearchResultItem()
                     {
                         Id = 2,
                         Title = "yaaahooo2",
                         HtmlTitle = @"<span>YH2</span>",
                         FileName = "yaay2.jpg",
                         Src = @"http://ddd.com/yaay2.jpg",
                         Deleted = false,
                         SearchResultId = 2
                     }
                 }
             }
                        };

            //return View(results);
            return View(db.SearchResults.ToList());
        }

        // GET: SearchResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchResult searchResult = db.SearchResults.Find(id);
            if (searchResult == null)
            {
                return HttpNotFound();
            }
            return View(searchResult);
        }

        // POST: SearchResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SearchResult searchResult = db.SearchResults.Find(id);
            db.SearchResults.Remove(searchResult);
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
