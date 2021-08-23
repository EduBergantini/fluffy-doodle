using Microsoft.EntityFrameworkCore;

using Blog.Domain.Contents.Entities;
using Blog.Infrastructure.SqlServer.Contents.Configurations;
using Blog.Infrastructure.SqlServer.Roles.Configurations;
using Blog.Domain.Roles.Entities;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContentConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
