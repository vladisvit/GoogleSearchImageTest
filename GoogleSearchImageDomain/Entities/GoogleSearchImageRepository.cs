using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using GoogleSearchImageDomain.Abstract;
using GoogleSearchImageDomain.Entities;

namespace GoogleSearchImageDomain.Entities
{
    public class GoogleSearchImageRepository : IGoogleSearchImageTestContext
    {
        private bool _disposed = false;
        private GoogleSearchImageTestContext db = new GoogleSearchImageTestContext();

        public IEnumerable<SearchResult> SearchResults { get { return db.SearchResults; } }
        public IEnumerable<SearchResultItem> SearchResultItems { get { return db.SearchResultItems; } }

        public IEnumerable<SearchResult> GetSearchResults()
        {
            return db.SearchResults.Include("Items");
        } 
        public SearchResult GetSearchResult(int id)
        {
            return db.SearchResults.Include("Items").FirstOrDefault(s => s.Id == id);
        }
        public SearchResult SaveUpdate(SearchResult searchResult)
        {
            var isUpdate = searchResult.Id != 0;
            searchResult.SearchDate = DateTime.Now;

            if (isUpdate)
            {
                db.SearchResults.Attach(searchResult);
                var itemResult = db.SearchResults.Include(s => s.Items).FirstOrDefault(s => s.Id == searchResult.Id);
                if (itemResult != null)
                {
                    var deletedItems = itemResult.Items.Where(i => i.Deleted).ToList();
                    db.SearchResultItems.RemoveRange(deletedItems);
                }
            }
            else
            {
                searchResult.Items.RemoveAll(i => i.Deleted);
                searchResult = db.SearchResults.Add(searchResult);
            }

            db.SaveChanges();

            return searchResult;
        }

        public int Delete(int id)
        {
            SearchResult searchResult = db.SearchResults.Find(id);
            db.SearchResults.Remove(searchResult);
            return db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                db?.Dispose();

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
