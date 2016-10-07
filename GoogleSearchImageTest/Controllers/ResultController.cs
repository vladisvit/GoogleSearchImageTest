using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoogleSearchImageTest.Models;

namespace GoogleSearchImageTest.Controllers
{
    public class ResultController : ApiController
    {
        private GoogleSearchImageTestContext db = new GoogleSearchImageTestContext();

        public SearchResult Get(int id)
        {
            var searchResult = db.SearchResults.Include("Items").FirstOrDefault(s => s.Id == id);

            return searchResult;
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
                }
                else
                {
                    searchResult = db.SearchResults.Add(searchResult);
                }

                var itemResult = db.SearchResults.Include(s => s.Items).First(s => s.Id == searchResult.Id);
                var deletedItems = itemResult.Items.Where(i => i.Deleted).ToList();
                db.SearchResultItems.RemoveRange(deletedItems);

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
