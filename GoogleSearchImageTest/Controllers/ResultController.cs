using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoogleSearchImageTest.Models;

namespace GoogleSearchImageTest.Controllers
{
    public class ResultController : ApiController
    {
        private readonly IGoogleSearchImageTestContext db = new GoogleSearchImageTestContext();

        public ResultController()
        {

        }

        public ResultController(IGoogleSearchImageTestContext context)
        {
            db = context;
        }

        public HttpResponseMessage Get(int id)
        {
            var searchResult = db.SearchResults.Include("Items").FirstOrDefault(s => s.Id == id);
            if (searchResult == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(searchResult);
        }

        public HttpResponseMessage Post(SearchResult searchResult)
        {
            var isUpdate = db.SearchResults.Any(s => s.Id == searchResult.Id);
            searchResult.SearchDate = DateTime.Now;

            try
            {
                if (isUpdate)
                {
                    db.SearchResults.Attach(searchResult);
                    var itemResult = db.SearchResults.Include(s => s.Items).FirstOrDefault(s => s.Id == searchResult.Id);
                    if (itemResult != null)
                    {
                        var deletedItems = itemResult?.Items.Where(i => i.Deleted).ToList();
                        db.SearchResultItems.RemoveRange(deletedItems);
                    }
                }
                else
                {
                    searchResult.Items.RemoveAll(i => i.Deleted);
                    searchResult = db.SearchResults.Add(searchResult);
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }


            if (isUpdate)
            {
                var response = Request.CreateResponse(HttpStatusCode.OK);
                string uri = Url.Link("DefaultApi", new { searchResult.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                var response = Request.CreateResponse<SearchResult>(HttpStatusCode.Created, searchResult);
                string uri = Url.Link("DefaultApi", new { searchResult.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
        }
    }
}
