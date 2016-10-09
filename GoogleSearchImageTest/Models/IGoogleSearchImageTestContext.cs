using System;
using System.Data.Entity;

namespace GoogleSearchImageTest.Models
{
    public interface IGoogleSearchImageTestContext: IDisposable
    {
        DbSet<SearchResult> SearchResults { get; }
        DbSet<SearchResultItem> SearchResultItems { get; }
        int SaveChanges();
        void MarkAsModified(SearchResult item);
    }
}
