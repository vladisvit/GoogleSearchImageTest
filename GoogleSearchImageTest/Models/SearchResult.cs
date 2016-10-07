using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoogleSearchImageTest.Models
{
    public class SearchResult
    {
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "The Name value cannot exceed 255 characters. ")]
        public string  Name { get; set; }
        public DateTime SearchDate { get; set; }
        public List<SearchResultItem> Items { get; set; } 
    }
}