using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using amazBlog.Data;

namespace amazBlog.Data;

public class PostsConfiguration : IEntityTypeConfiguration<Posts>
{
    public void Configure(EntityTypeBuilder<Posts> builder)
    {
        builder
        .Property(p => p.Title)
        .IsRequired()
        .HasColumnType("varchar(50)");

        builder
        .Property(p => p.Content)
        .HasColumnType("text");
    }
}