using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Blog.Domain.Contents.Entities;
using Blog.Infrastructure.SqlServer.Contents.Configurations;

namespace Blog.Infrastructure.SqlServer.Contexts
{
    public class ContentDataContext : DbContext
    {
        public ContentDataContext(IDbContextOptions options) 
            : base((DbContextOptions)options)
        {
        }

        public virtual DbSet<Content> Contents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
