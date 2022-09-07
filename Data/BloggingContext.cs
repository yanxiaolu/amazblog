using Microsoft.EntityFrameworkCore;
using amazBlog.Data;
using System.Diagnostics;


public class BloggingContext : DbContext
{
    public DbSet<Blog> Blog { get; set; }
    public DbSet<Posts> Posts { get; set; }
    public DbSet<Comments> Comments { get; set; }
    public BloggingContext(DbContextOptions<BloggingContext> options)
    : base(options)
    {
        Debug.WriteLine($"{ContextId} context created.");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=blogging.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
        .HasData(
            new Blog { Id = 1, Name = "amazBlog", ShortName = "AMZ", Description = "Made amazing" }
        );

        modelBuilder.Entity<Comments>()
        .HasOne(p => p.Posts)
        .WithMany(c => c.Comments);

        modelBuilder.Entity<Comments>()
        .HasKey(p => p.CommentId);
    }
}