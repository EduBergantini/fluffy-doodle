using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Blog.IntegrationTests.Common;
using Blog.Domain.Contents.UseCases;
using Blog.IntegrationTests.Contents.Stubs;

namespace Blog.IntegrationTests.Contents
{
    public class ContentsIntegrationTest : ApiWebApplicationFactory
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<IGetContentListUseCase, ContentListUseCaseStub>();
            });
            base.ConfigureWebHost(builder);
        }
    }
}
