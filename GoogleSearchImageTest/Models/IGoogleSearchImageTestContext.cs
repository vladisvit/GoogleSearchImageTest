using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
