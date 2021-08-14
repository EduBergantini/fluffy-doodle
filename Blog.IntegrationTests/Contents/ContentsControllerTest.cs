using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Xunit;

using Blog.IntegrationTests.Common;
using Blog.Domain.Contents.Entities;


namespace Blog.IntegrationTests.Contents
{
    public class ContentsControllerTest : IntegrationTest<ContentsIntegrationTest>
    {
        private readonly string url = "/api/contents";

        public ContentsControllerTest(ContentsIntegrationTest apiWebApplicationFactory) 
            : base(apiWebApplicationFactory)
        {
        }

        [Fact]
        public async Task GET_ShouldReturnListOfContentsOnSuccess()
        {
            var response = await base.httpClient.GetAsync(this.url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var body = await response.Content.ReadAsStringAsync();
            var contents = JsonSerializer.Deserialize<IEnumerable<Content>>(body);
            Assert.Equal(3, contents.Count());

        }

    }
}
