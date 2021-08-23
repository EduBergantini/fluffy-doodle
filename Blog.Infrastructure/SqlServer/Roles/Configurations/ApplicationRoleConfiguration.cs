using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Blog.Domain.Roles.Entities;


namespace Blog.Infrastructure.SqlServer.Roles.Configurations
{
    class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("TBL_ROLES", "admin");
            builder.HasKey(x => x.Id).HasName("PK_ROLES");
            builder.Property(role => role.Id).HasColumnName("ROLE_ID").IsRequired();
            builder.Property(role => role.Description).HasColumnName("ROLE_DESC").IsRequired().HasMaxLength(150);
        }
    }
}
