using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private readonly GoogleSearchImageTestContext db = new GoogleSearchImageTestContext();

        public DbSet<SearchResult> SearchResults { get { return db.SearchResults; } }
        public DbSet<SearchResultItem> SearchResultItems { get { return db.SearchResultItems; } }

        public GoogleSearchImageRepository()
        {
            
        }

        public GoogleSearchImageRepository(GoogleSearchImageTestContext dbContext)
        {
            db = dbContext;
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public int SaveUpdate(SearchResult searchResult)
        {
            var isUpdate = db.SearchResults.Any(s => s.Id == searchResult.Id);
            searchResult.SearchDate = DateTime.Now;


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

            return db.SaveChanges();
        }

        public int Delete(SearchResult searchResult)
        {
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
