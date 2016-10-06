using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleSearchImageTest.Models
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public DateTime SearchDate { get; set; }
        public List<SearchResultItem> Items { get; set; } 
    }
}