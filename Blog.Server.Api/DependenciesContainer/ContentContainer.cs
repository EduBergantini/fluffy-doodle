using Microsoft.Extensions.DependencyInjection;

using Blog.Application.Contents.Protocols;
using Blog.Application.Contents.UseCases;
using Blog.Domain.Contents.UseCases;
using Blog.Infrastructure.SqlServer.Contents;

namespace Blog.Server.Api.DependenciesContainer
{
    public static class ContentDependencies
    {
        public static IServiceCollection AddContentDependencies(this IServiceCollection services)
        {
            services.AddScoped<IGetContentListUseCase, ContentUseCase>();
            services.AddScoped<IGetContentListRepository, ContentRepository>();

            return services;
        }
    }
}
