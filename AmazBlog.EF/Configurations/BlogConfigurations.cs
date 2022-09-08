using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AmazBlog.Shared.Models;

namespace AmazBlog.EF.Configurations
{
    public class BlogConfigration : IEntityTypeConfiguration<Blog>
    {
        public BlogConfigration()
        {
        }
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(p => p.Name).IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.ShortName).IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.ShortName).HasColumnType("varchar(200)");

            builder.Property(p => p.Description).HasColumnType("varchar(200)");
        }
    }
}

