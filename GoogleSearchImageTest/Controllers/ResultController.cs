using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoogleSearchImageTest.Models;

namespace GoogleSearchImageTest.Controllers
{
    public class ResultController : ApiController
    {
        public SearchResult[] Get()
        {
            return new SearchResult[]
            {
             new SearchResult()
             {
                 Id = 1,
                 Name = "res#1",
                 SearchDate = DateTime.Now,
                 Items = new List<SearchResultItem>()
                 {
                    new SearchResultItem()
                            {
                                Id = 1,
                                Title = "yaaahooo",
                                HtmlTitle = @"<span>YH</span>",
                                FileName = "yaay.jpg",
                                Src = @"http://ddd.com/yaay.jpg",
                                Deleted = false,
                                SearchResultId = 1
                            },
                            new SearchResultItem()
                            {
                                Id = 2,
                                Title = "yaaahooo2",
                                HtmlTitle = @"<span>YH2</span>",
                                FileName = "yaay2.jpg",
                                Src = @"http://ddd.com/yaay2.jpg",
                                Deleted = false,
                                SearchResultId = 1
                            }
                 }
             },
             new SearchResult()
             {
                 Id = 2,
                 Name = "res#2",
                 SearchDate = DateTime.Now,
                 Items = new List<SearchResultItem>()
                 {
                     new SearchResultItem()
                            {
                                Id = 2,
                                Title = "yaaahooo2",
                                HtmlTitle = @"<span>YH2</span>",
                                FileName = "yaay2.jpg",
                                Src = @"http://ddd.com/yaay2.jpg",
                                Deleted = false,
                                SearchResultId = 2
                            }
                 }
             }
            };
        }

        public SearchResult[] Get(int id)
        {
            if (id == 1)
            {
                return new SearchResult[]
                {
                    new SearchResult()
                    {
                        Id = 1,
                        Name = "res#1",
                        SearchDate = DateTime.Now,
                        Items = new List<SearchResultItem>()
                        {
                            new SearchResultItem()
                            {
                                Id = 1,
                                Title = "yaaahooo",
                                HtmlTitle = @"<span>YH</span>",
                                FileName = "yaay.jpg",
                                Src = @"http://ddd.com/yaay.jpg",
                                Deleted = false,
                                SearchResultId = 1
                            },
                            new SearchResultItem()
                            {
                                Id = 2,
                                Title = "yaaahooo2",
                                HtmlTitle = @"<span>YH2</span>",
                                FileName = "yaay2.jpg",
                                Src = @"http://ddd.com/yaay2.jpg",
                                Deleted = false,
                                SearchResultId = 1
                            }
                        }
                    }
                };
            }

            if (id == 2)
            {
                return new SearchResult[]
       {
                                 new SearchResult()
             {
                 Id = 2,
                 Name = "res#2",
                 SearchDate = DateTime.Now,
                 Items = new List<SearchResultItem>()
                 {
                     new SearchResultItem()
                     {
                         Id = 2,
                         Title = "yaaahooo2",
                         HtmlTitle = @"<span>YH2</span>",
                         FileName = "yaay2.jpg",
                         Src = @"http://ddd.com/yaay2.jpg",
                         Deleted = false,
                         SearchResultId = 2
                     }
                 }
                }
       };
            }

            return null;
        }
    }
}
