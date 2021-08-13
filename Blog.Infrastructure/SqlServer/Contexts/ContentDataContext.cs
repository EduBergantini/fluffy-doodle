using Microsoft.EntityFrameworkCore;

using Blog.Domain.Contents.Entities;
using Blog.Infrastructure.SqlServer.Contents.Configurations;

namespace Blog.Infrastructure.SqlServer.Contexts
{
    public class ContentDataContext : DbContext
    {
        public ContentDataContext(DbContextOptions options) 
            : base(options)
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
