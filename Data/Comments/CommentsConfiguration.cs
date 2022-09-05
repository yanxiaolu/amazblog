using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazBlog.Data;
public class CommentsConfiguration : IEntityTypeConfiguration<Comments>
{
    public void Configure(EntityTypeBuilder<Comments> builder)
    {
        builder.Property(p => p.CommentDate)
        .HasColumnType("text")
        .IsRequired();


    }
}