using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoogleSearchImageDomain.Abstract;
using GoogleSearchImageDomain.Entities;
using GoogleSearchImageTest.Models;

namespace GoogleSearchImageTest.Controllers
{
    public class ResultController : ApiController
    {
        private readonly IGoogleSearchImageTestContext _db;

        public ResultController()
        {

        }

        public ResultController(IGoogleSearchImageTestContext context)
        {
            _db = context;
        }

        public HttpResponseMessage Get()
        {
            var searchResult = _db.SearchResults.Include("Items");
            if (searchResult == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(searchResult);
        }

        public HttpResponseMessage Get(int id)
        {
            var searchResult = _db.SearchResults.Include("Items").FirstOrDefault(s => s.Id == id);
            if (searchResult == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(searchResult);
        }

        public HttpResponseMessage Post(int id)
        {
            SearchResult searchResult = _db.SearchResults.Find(id);
            _db.Delete(searchResult);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            string uri = Url.Link("DefaultApi", new { searchResult.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Post(SearchResult searchResult)
        {
            var isUpdate = _db.SearchResults.Any(s => s.Id == searchResult.Id);

            try
            {
                _db.SaveUpdate(searchResult);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
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
                var response = Request.CreateResponse(HttpStatusCode.Created, searchResult);
                string uri = Url.Link("DefaultApi", new { searchResult.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
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
