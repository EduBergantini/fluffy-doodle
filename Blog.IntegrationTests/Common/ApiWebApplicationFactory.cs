using Microsoft.AspNetCore.Mvc.Testing;

using Blog.Server.Api;

namespace Blog.IntegrationTests.Common
{
    public abstract class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
    }
}
