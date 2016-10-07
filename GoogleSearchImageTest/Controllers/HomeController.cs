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
