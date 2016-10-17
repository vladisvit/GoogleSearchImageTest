using System;
using System.Data.Entity;
using System.Linq;
using GoogleSearchImageDomain.Abstract;
using GoogleSearchImageDomain.Entities;
using GoogleSearchImageTest.Models;

namespace GoogleSearchImageTest.Tests
{
    public class TestGoogleSearchImageTestContext: IGoogleSearchImageTestContext
    {
        public TestGoogleSearchImageTestContext()
        {
            this.SearchResults = new TestSearchResultDbSet();
            this.SearchResultItems = new TestSearchResultItemDbSet();
        }

        public DbSet<SearchResult> SearchResults { get; }
        public DbSet<SearchResultItem> SearchResultItems { get; }

        public int SaveChanges()
        {
            return 0;
        }

        public int SaveUpdate(SearchResult searchResult)
        {
            var isUpdate = SearchResults.Any(s => s.Id == searchResult.Id);
            searchResult.SearchDate = DateTime.Now;

            if (isUpdate)
            {
                SearchResults.Attach(searchResult);
                var itemResult = SearchResults.Include(s => s.Items).FirstOrDefault(s => s.Id == searchResult.Id);
                if (itemResult != null)
                {
                    var deletedItems = itemResult?.Items.Where(i => i.Deleted).ToList();
                    SearchResultItems.RemoveRange(deletedItems);
                }
            }
            else
            {
                searchResult.Items.RemoveAll(i => i.Deleted);
                searchResult = SearchResults.Add(searchResult);
            }

            return SaveChanges();
        }

        public int Delete(SearchResult searchResult)
        {
            SearchResults.Remove(searchResult);
            return SaveChanges();
        }

        public void Dispose()
        {
           
        }
    }
}
