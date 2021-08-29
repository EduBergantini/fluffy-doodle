using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Blog.Infrastructure.SqlServer.Contexts;
using Blog.Infrastructure.Common.Adapters.Configurations;
using Blog.Infrastructure.Common.Adapters;
using Blog.Application.Common.Protocols;
using SimpleHashing.Net;

namespace Blog.Server.Api.DependenciesContainer
{
    public static class CommonDependencies
    {
        public static IServiceCollection AddCommonDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContentDataContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSingleton(service =>
            {
                var section = configuration.GetSection("Blog:Web");
                return new AuthenticationTokenConfiguration(secretKey: section.GetValue<string>("AuthTokenSecretkey"), totalDaysToExpire: section.GetValue<int>("DaysToExpire"));
            });

            services.AddSingleton<ICreateEncryption, IdentityJwtAdapter>();
            services.AddSingleton<ICompareHash, SimpleHashingAdapter>();
            services.AddSingleton<ICreateHash, SimpleHashingAdapter>();
            services.AddSingleton<ISimpleHash, SimpleHash>();

            return services;
        }
    }
}
