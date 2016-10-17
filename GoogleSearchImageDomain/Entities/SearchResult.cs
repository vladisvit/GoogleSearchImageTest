using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoogleSearchImageDomain.Entities
{
    public class SearchResult
    {
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "The Name value cannot exceed 255 characters. ")]
        public string  Name { get; set; }

        [Display(Name = "Search Date")]
        public DateTime SearchDate { get; set; }
        public List<SearchResultItem> Items { get; set; } 
    }
}