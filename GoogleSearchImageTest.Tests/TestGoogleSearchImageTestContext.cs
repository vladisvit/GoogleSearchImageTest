using System.Data.Entity;
using GoogleSearchImageTest.Models;

namespace GoogleSearchImageTest.Tests
{
    public class TestGoogleSearchImageTestContext: IGoogleSearchImageTestContext
    {
        public TestGoogleSearchImageTestContext()
        {
            this.SearchResults = new TestSearchResultDbSet();
            this.SearchResultItems = new TestDbSet<SearchResultItem>();
        }

        public DbSet<SearchResult> SearchResults { get; }
        public DbSet<SearchResultItem> SearchResultItems { get; }
        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(SearchResult item)
        {
           
        }

        public void Dispose()
        {
           
        }
    }
}
