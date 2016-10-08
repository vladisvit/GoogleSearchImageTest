using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GoogleSearchImageTest.Models
{
    public class GoogleSearchImageTestContext : DbContext, IGoogleSearchImageTestContext
    {
        public GoogleSearchImageTestContext() : base("name=GoogleSearchImageTestContext")
        {
        }

        public System.Data.Entity.DbSet<GoogleSearchImageTest.Models.SearchResult> SearchResults { get; set; }
        public System.Data.Entity.DbSet<GoogleSearchImageTest.Models.SearchResultItem> SearchResultItems { get; set; }

        public void MarkAsModified(SearchResult item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
