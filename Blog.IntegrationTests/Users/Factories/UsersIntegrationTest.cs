using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Blog.IntegrationTests.Common;
using Blog.Domain.Users.UseCases;
using Blog.IntegrationTests.Users.Stubs;

namespace Blog.IntegrationTests.Users.Factories
{
    public class UsersIntegrationTest : ApiWebApplicationFactory
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
                services.AddScoped<IAuthenticateUseCase, AuthenticateUseCaseStub>();
            });
            base.ConfigureWebHost(builder);
        }
    }
}
