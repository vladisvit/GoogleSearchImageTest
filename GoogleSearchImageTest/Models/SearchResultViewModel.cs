using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoogleSearchImageDomain.Entities;

namespace GoogleSearchImageTest.Models
{
    public class SearchResultViewModel
    {
        public IEnumerable<SearchResult> SearchResults { get; set; }
    }
}