using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Mvc;
using GoogleSearchImageDomain.Entities;
using GoogleSearchImageTest.Controllers;
using GoogleSearchImageTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogleSearchImageTest.Tests
{
    [TestClass]
    public class BaseTest
    {
        [TestClass]
        public class ControllerTests
        {
            private readonly TestGoogleSearchImageTestContext _context;

            public ControllerTests()
            {
                _context = new TestGoogleSearchImageTestContext();
                _context.SearchResults.Add(GetSearchResult());
            }

            [TestMethod]
            public void GetReturnsSearchResult()
            {
                var controller = new ResultController(_context);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                var response = controller.Get(1);

                SearchResult searchResult;
                Assert.IsTrue(response.TryGetContentValue<SearchResult>(out searchResult));
                Assert.AreEqual(1, searchResult.Id);
            }

            [TestMethod]
            public void PostSearchResult()
            {
                const string Url = @"http://localhost:28463/api/result";
                var controller = new ResultController(_context);
                controller.Request = new HttpRequestMessage
                {
                    RequestUri = new Uri(Url)
                };
                controller.Configuration = new HttpConfiguration();
                controller.Configuration.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional });

                controller.RequestContext.RouteData = new HttpRouteData(
                    route: new HttpRoute(),
                    values: new HttpRouteValueDictionary { { "controller", "result" } });

                SearchResult result = GetSearchResult2();
                var response = controller.Post(result);

                Assert.AreEqual(Url+@"/2", response.Headers.Location.AbsoluteUri);
            }

            [TestMethod]
            public void ReturnIndex()
            {
                var controller = new HomeController(_context);
                _context.SearchResults.Add(new SearchResult()
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
                });

                var viewResult = controller.Index() as ViewResult;

                Assert.IsInstanceOfType(viewResult?.Model, typeof(SearchResultViewModel));
            }

            [TestMethod]
            public void DeleteReturnsOk()
            {
                var controller = new HomeController(_context);

                var result = controller.DeleteConfirmed(1);

                Assert.IsFalse(_context.SearchResults.Any());
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
