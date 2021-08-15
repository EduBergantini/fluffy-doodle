using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Blog.IntegrationTests.Common;
using Blog.Domain.Contents.Entities;
using Blog.IntegrationTests.Contents.Factories;
using Blog.IntegrationTests.Contents.Stubs;
using Blog.Domain.Contents.UseCases.Contents;

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

        [Fact]
        public async Task GET_ShouldReturnNoContentWhenListIsEmpty()
        {
            var client = base.factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IGetContentListUseCase, EmptyContentListUseCaseStub>();
                });
            }).CreateClient();

            var response = await client.GetAsync(this.url);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task GET_ShouldReturnServerErrorWhenExceptionHappens()
        {
            var client = base.factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IGetContentListUseCase, ExceptionContentListUseCaseStub>();
                });
            }).CreateClient();

            var response = await client.GetAsync(this.url);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

    }
}
