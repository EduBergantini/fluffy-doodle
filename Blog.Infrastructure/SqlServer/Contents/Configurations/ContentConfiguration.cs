using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Blog.Domain.Contents.Entities;

namespace Blog.Infrastructure.SqlServer.Contents.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.ToTable("TBL_CONTENTS", "contents");
            builder.HasKey(content => content.Id).HasName("PK_CONTENTS");
            builder.Property(content => content.Id).HasColumnName("CTNT_ID").IsRequired();
            builder.Property(content => content.Title).HasColumnName("CTNT_TITLE").IsRequired().HasMaxLength(120);
            builder.Property(content => content.PublicId).HasColumnName("CTNT_PUBID").IsRequired().HasMaxLength(150);
            builder.Property(content => content.Subtitle).HasColumnName("CTNT_SUBTITLE").IsRequired().HasMaxLength(270);
            builder.Property(content => content.FeaturedImage).HasColumnName("CTNT_FEATIMG").IsRequired().HasMaxLength(200);
        }
    }
}