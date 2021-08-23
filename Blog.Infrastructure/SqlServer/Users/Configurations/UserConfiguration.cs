using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Blog.Domain.Users.Entities;

namespace Blog.Infrastructure.SqlServer.Users.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TBL_USERS", "admin");
            builder.HasKey(user => user.Id).HasName("PK_USERS");
            builder.Property(user => user.Id).HasColumnName("USER_ID").IsRequired();
            builder.Property(user => user.PublicId).HasColumnName("USER_PUBID").IsRequired().HasMaxLength(128);
            builder.Property(user => user.Email).HasColumnName("USER_EMAIL").IsRequired().HasMaxLength(250);
            builder.Property(user => user.FullName).HasColumnName("USER_FNAME").IsRequired().HasMaxLength(200);
            builder.Property(user => user.Password).HasColumnName("USER_PASSWD").IsRequired().HasMaxLength(256);
            builder.Property(user => user.RegisterDate).HasColumnName("USER_REGDATE").IsRequired().HasDefaultValueSql("GETDATE()");

            builder.Property(user => user.StatusId).HasColumnName("STTS_ID").IsRequired();
            builder.HasOne(user => user.Status)
                .WithMany(status => status.Users)
                .HasForeignKey(user => user.StatusId)
                .HasConstraintName("FK_USERS_STATUS");

            builder.Property(user => user.RoleId).HasColumnName("ROLE_ID").IsRequired();
            builder.HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .HasConstraintName("FK_USERS_ROLES");
            
        }
    }
}
