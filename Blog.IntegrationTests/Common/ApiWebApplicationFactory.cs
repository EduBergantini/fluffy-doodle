using System;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

using Blog.Server.Api;


namespace Blog.IntegrationTests.Common
{
    public abstract class ApiWebApplicationFactory : WebApplicationFactory<Startup>, IDisposable
    {
        protected IConfiguration Configuration { get; set; }

    }
}
