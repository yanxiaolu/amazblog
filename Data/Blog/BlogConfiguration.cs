using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using amazBlog.Data;

namespace amazBlog.Data;
public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder
        .Property(b => b.Name)
        .IsRequired()
        .HasMaxLength(50)
        .HasColumnType("varchar(50)");

        builder
        .Property(b => b.Description)
        .IsRequired()
        .HasColumnType("varchar(200)");
    }
}