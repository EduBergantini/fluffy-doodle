using System.Net.Http;

using Xunit;

namespace Blog.IntegrationTests.Common
{
    public class IntegrationTest<TConcreteFixture> : IClassFixture<TConcreteFixture> 
        where TConcreteFixture : ApiWebApplicationFactory
    {
        protected readonly ApiWebApplicationFactory factory;
        protected readonly HttpClient httpClient;

        public IntegrationTest(ApiWebApplicationFactory apiWebApplicationFactory)
        {
            this.factory = apiWebApplicationFactory;
            this.httpClient = apiWebApplicationFactory.CreateClient();
        }
    }
}
