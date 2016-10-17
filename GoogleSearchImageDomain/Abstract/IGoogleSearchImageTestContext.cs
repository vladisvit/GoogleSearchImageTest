using System;
using System.Data.Entity;
using System.Net.Http;
using GoogleSearchImageDomain.Entities;

namespace GoogleSearchImageDomain.Abstract
{
    public interface IGoogleSearchImageTestContext: IDisposable
    {
        DbSet<SearchResult> SearchResults { get; }
        DbSet<SearchResultItem> SearchResultItems { get; }
        int SaveChanges();

        int SaveUpdate(SearchResult searchResult);

        int Delete(SearchResult searchResult);
    }
}
