using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Blog.IntegrationTests.Common;
using Blog.IntegrationTests.Contents.Stubs;
using Blog.Domain.Contents.UseCases.Contents;


namespace Blog.IntegrationTests.Contents.Factories
{
    public class ContentsIntegrationTest : ApiWebApplicationFactory
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config => 
            { 
                Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Integration.json").Build(); 
                config.AddConfiguration(Configuration); 
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<IGetContentListUseCase, ContentListUseCaseStub>();
                services.AddScoped<IGetContentByPublicIdUseCase, ContentByPublicIdUseCaseStub>();
            });
            base.ConfigureWebHost(builder);
        }
    }
}
