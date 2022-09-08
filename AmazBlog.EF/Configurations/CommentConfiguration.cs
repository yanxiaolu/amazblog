using System;
using AmazBlog.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazBlog.EF.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(p => p.CommentDate).IsRequired();

            builder.Property(p => p.CommentContent).IsRequired()
                .HasColumnType("text");
        }
    }
}

