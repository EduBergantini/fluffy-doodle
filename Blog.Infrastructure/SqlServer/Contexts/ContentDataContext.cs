using Microsoft.EntityFrameworkCore;

using Blog.Domain.Contents.Entities;
using Blog.Domain.Roles.Entities;
using Blog.Domain.Status.Entities;
using Blog.Domain.Users.Entities;
using Blog.Infrastructure.SqlServer.Contents.Configurations;
using Blog.Infrastructure.SqlServer.Roles.Configurations;
using Blog.Infrastructure.SqlServer.Status.Configurations;
using Blog.Infrastructure.SqlServer.Users.Configurations;

namespace Blog.Infrastructure.SqlServer.Contexts
{
    public class ContentDataContext : DbContext
    {
        public ContentDataContext(DbContextOptions options) 
            : base(options)
        {
        }

        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<ApplicationRole> Roles { get; set; }
        public virtual DbSet<ApplicationStatus> Status { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContentConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationStatusConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
