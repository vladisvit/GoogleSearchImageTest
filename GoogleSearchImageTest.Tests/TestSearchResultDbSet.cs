using System.Linq;
using GoogleSearchImageTest.Models;

namespace GoogleSearchImageTest.Tests
{
    public class TestSearchResultDbSet : TestDbSet<SearchResult>
    {
        public override SearchResult Find(params object[] keyValues)
        {
            return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
        }
    }
}
