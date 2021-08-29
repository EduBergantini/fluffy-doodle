using Blog.Application.Common.Protocols;
using Blog.Domain.Users.Entities;
using Blog.Domain.Roles.Entities;
using Blog.Domain.Status.Entities;
using Blog.Infrastructure.SqlServer.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog.Server.Api.Services
{
    public static class DatabaseMigrationService
    {
        public static void InitializeDatabaseMigration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ContentDataContext>();
            var createHash = serviceScope.ServiceProvider.GetService<ICreateHash>();
             
            context.Database.Migrate();
            context.Database.EnsureCreated();

            var user = new User
            {
                Email = "admin@email.com",
                FullName = "Administrador",
                Password = createHash.CreateHash("123Mudar!", 1000).GetAwaiter().GetResult(),
                PublicId = Guid.NewGuid().ToString(),
                RoleId = 1,
                StatusId = 1
            };
            context.Roles.Add(new ApplicationRole { Description = "Administrativo" });
            context.Roles.Add(new ApplicationRole { Description = "Autor" });
            context.Roles.Add(new ApplicationRole { Description = "Usuário" });

            context.Status.Add(new ApplicationStatus { Description = "Ativo" });
            context.Status.Add(new ApplicationStatus { Description = "Inativo" });
            context.Status.Add(new ApplicationStatus { Description = "Bloqueado" });
            context.Status.Add(new ApplicationStatus { Description = "Removido" });
            context.SaveChanges();

            context.Users.Add(user);
            context.SaveChanges();


        }
    }
}
