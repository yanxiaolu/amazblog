using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AmazBlog.Shared.Models;

namespace AmazBlog.Shared.Configurations
{
    public class PostConfigration : IEntityTypeConfiguration<Post>
    {
        public PostConfigration()
        {
        }
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title).IsRequired()
        .HasColumnType("varchar(50)");

            builder.Property(p => p.Content).HasColumnType("text");

            builder.Property(p => p.PublishTime).IsRequired();

            builder.Property(p => p.UpdateTime).IsRequired();
        }
    }
}

