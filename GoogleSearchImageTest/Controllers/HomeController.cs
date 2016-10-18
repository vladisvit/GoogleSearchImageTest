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
            return View();
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
