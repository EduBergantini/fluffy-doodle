using Blog.Infrastructure.SqlServer.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Server.Api.Services
{
    public static class DatabaseMigrationService
    {
        public static void InitializeDatabaseMigration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ContentDataContext>();
            context.Database.Migrate();
            context.Database.EnsureCreated();
        }
    }
}
