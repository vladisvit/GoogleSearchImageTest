using System.ComponentModel.DataAnnotations;

namespace GoogleSearchImageDomain.Entities
{
    public class SearchResultItem
    {
        public int Id { get; set; }
        public int SearchResultId { get; set; }

        [StringLength(255, ErrorMessage = "The Title value cannot exceed 255 characters. ")]
        public string Title { get; set; }

        [StringLength(255, ErrorMessage = "The HtmlTitle value cannot exceed 255 characters. ")]
        public string HtmlTitle { get; set; }

        [StringLength(255, ErrorMessage = "The Filename value cannot exceed 255 characters. ")]
        public string FileName { get; set; }

        [StringLength(2048, ErrorMessage = "The Src value cannot exceed 2048 characters. ")]
        public string Src { get; set; }
        public bool Deleted { get; set; }
    }
}