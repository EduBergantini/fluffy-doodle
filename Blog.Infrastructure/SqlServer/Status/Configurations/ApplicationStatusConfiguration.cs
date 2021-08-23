using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Blog.Domain.Status.Entities;

namespace Blog.Infrastructure.SqlServer.Status.Configurations
{
    class ApplicationStatusConfiguration : IEntityTypeConfiguration<ApplicationStatus>
    {
        public void Configure(EntityTypeBuilder<ApplicationStatus> builder)
        {
            builder.ToTable("TBL_STATUS", "admin");
            builder.HasKey(status => status.Id).HasName("PK_STATUS");
            builder.Property(status => status.Id).HasColumnName("STTS_ID").IsRequired();
            builder.Property(status => status.Description).HasColumnName("STTS_DESC").IsRequired().HasMaxLength(150);
        }
    }
}
