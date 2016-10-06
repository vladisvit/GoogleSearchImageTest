namespace GoogleSearchImageTest.Models
{
    public class SearchResultItem
    {
        public int Id { get; set; }
        public int SearchResultId { get; set; }
        public string Title { get; set; }
        public string HtmlTitle { get; set; }
        public string FileName { get; set; }
        public string Src { get; set; }
        public bool Deleted { get; set; }
    }
}