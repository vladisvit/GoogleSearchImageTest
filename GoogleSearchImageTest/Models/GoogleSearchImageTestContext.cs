using System.Data.Entity;

namespace GoogleSearchImageTest.Models
{
    public class GoogleSearchImageTestContext : DbContext, IGoogleSearchImageTestContext
    {
        public GoogleSearchImageTestContext() : base("name=GoogleSearchImageTestContext")
        {
        }

        public DbSet<SearchResult> SearchResults { get; set; }
        public DbSet<SearchResultItem> SearchResultItems { get; set; }

        public void MarkAsModified(SearchResult item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
