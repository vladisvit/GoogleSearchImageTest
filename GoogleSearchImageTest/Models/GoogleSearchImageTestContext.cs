using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GoogleSearchImageTest.Models
{
    public class GoogleSearchImageTestContext : DbContext
    {
        public GoogleSearchImageTestContext() : base("name=GoogleSearchImageTestContext")
        {
        }

        public System.Data.Entity.DbSet<GoogleSearchImageTest.Models.SearchResult> SearchResults { get; set; }
    }
}
