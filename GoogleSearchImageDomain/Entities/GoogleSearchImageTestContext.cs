using System;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using GoogleSearchImageDomain.Abstract;

namespace GoogleSearchImageDomain.Entities
{
    public class GoogleSearchImageTestContext : DbContext
    {
        public GoogleSearchImageTestContext() : base("name=GoogleSearchImageTestContext")
        {
        }

        public DbSet<SearchResult> SearchResults { get; set; }
        public DbSet<SearchResultItem> SearchResultItems { get; set; }
    }
}
