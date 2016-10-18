using System.Linq;
using System.Net;
using System.Web.Mvc;
using GoogleSearchImageDomain.Abstract;
using GoogleSearchImageDomain.Entities;
using GoogleSearchImageTest.Models;

namespace GoogleSearchImageTest.Controllers
{
    public class HomeController : Controller
    {
        private  IGoogleSearchImageTestContext _db;

        public HomeController()
        {
            
        }

        public HomeController(IGoogleSearchImageTestContext context)
        {
            _db = context;
        }

        public ActionResult Index()
        {
            //var searchResultViewModel = new SearchResultViewModel() { SearchResults = _db.SearchResults.Include("Items").ToList() };
            //return View(searchResultViewModel);

            return View();
        }

        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //SearchResult searchResult = _db.SearchResults.Find(id);
            //if (searchResult == null)
            //{
            //    return HttpNotFound();
            //}

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //SearchResult searchResult = _db.SearchResults.Find(id);
            //_db.Delete(searchResult);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
