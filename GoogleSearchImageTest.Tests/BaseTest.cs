using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Mvc;
using GoogleSearchImageDomain.Abstract;
using GoogleSearchImageDomain.Entities;
using GoogleSearchImageTest.Controllers;
using GoogleSearchImageTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GoogleSearchImageTest.Tests
{
    [TestClass]
    public class BaseTest
    {
        [TestClass]
        public class ControllerTests
        {
            private const string Url = @"http://localhost:28463/api/result";
            private Mock<IGoogleSearchImageTestContext> _context;
            private ResultController controller;

            [TestInitialize()]
            public void Setup()
            {
                _context = new Mock<IGoogleSearchImageTestContext>();
                _context.Setup(m => m.SearchResults).Returns(new List<SearchResult> { GetSearchResult() });

                controller = new ResultController(_context.Object)
                {
                    Request =
                    new HttpRequestMessage
                    {
                        RequestUri = new Uri(Url)
                    },
                    Configuration = new HttpConfiguration()
                };

                controller.Configuration.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional });

                controller.RequestContext.RouteData = new HttpRouteData(
                    route: new HttpRoute(),
                    values: new HttpRouteValueDictionary { { "controller", "result" } });
            }

            [TestMethod]
            public void GetReturnsSearchResults()
            {
                _context.Setup(m => m.SearchResults).Returns(new List<SearchResult> { GetSearchResult(), GetSearchResult2() });
                _context.Setup(m => m.GetSearchResults())
                    .Returns(new List<SearchResult> { GetSearchResult(), GetSearchResult2() });

                var response = controller.Get();
                Assert.IsFalse(response.StatusCode == HttpStatusCode.NotFound);
                IEnumerable<SearchResult> searchResults;
                Assert.IsTrue(response.TryGetContentValue<IEnumerable<SearchResult>>(out searchResults));
                Assert.AreEqual(2, searchResults.Count());
            }

            [TestMethod]
            public void GetReturnsSearchResult()
            {
                _context.Setup(m => m.GetSearchResult(1)).Returns(GetSearchResult);

                var response = controller.Get(1);

                _context.Verify(m => m.GetSearchResult(1));

                Assert.IsFalse(response.StatusCode == HttpStatusCode.NotFound);

                SearchResult searchResult;
                Assert.IsTrue(response.TryGetContentValue<SearchResult>(out searchResult));
                Assert.AreEqual(1, searchResult.Id);
            }

            [TestMethod]
            public void PostSearchResult()
            {
                SearchResult result = GetSearchResult2();
                _context.Setup(m => m.SaveUpdate(result)).Returns(result);

                var response = controller.Post(result);
                _context.Verify(m => m.SaveUpdate(result));

                Assert.IsTrue(response.IsSuccessStatusCode);
            }

            [TestMethod]
            public void DeleteReturnsOk()
            {
                _context.Setup(s => s.Delete(1));

                var response = controller.Delete(1);

                _context.Verify(m => m.Delete(1));

                Assert.IsTrue(response.IsSuccessStatusCode);
            }

            private SearchResult GetSearchResult()
            {
                return new SearchResult()
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
                };
            }

            private SearchResult GetSearchResult2()
            {
                return new SearchResult()
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
                };
            }
        }
    }
}
