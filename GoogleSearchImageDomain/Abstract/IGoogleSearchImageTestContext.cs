using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using GoogleSearchImageDomain.Entities;

namespace GoogleSearchImageDomain.Abstract
{
    public interface IGoogleSearchImageTestContext : IDisposable
    {
        IEnumerable<SearchResult> SearchResults { get; }
        IEnumerable<SearchResultItem> SearchResultItems { get; }

        IEnumerable<SearchResult> GetSearchResults();
        SearchResult GetSearchResult(int id);
        SearchResult SaveUpdate(SearchResult searchResult);

        int Delete(int id);
    }
}
