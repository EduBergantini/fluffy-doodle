using Microsoft.Extensions.DependencyInjection;

using Blog.Application.Users.UseCases;
using Blog.Application.Users.Protocols;
using Blog.Domain.Users.UseCases;
using Blog.Infrastructure.SqlServer.Users;

namespace Blog.Server.Api.DependenciesContainer
{
    public static class UserContainer
    {
        public static IServiceCollection AddUserDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateUseCase, UserUseCase>();
            services.AddScoped<IGetUserByEmailRepository, UserRepository>();

            return services;
        }
    }
}
