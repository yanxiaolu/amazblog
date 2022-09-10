using System.Diagnostics;
using System.Xml.Linq;
using AmazBlog.Shared;
using AmazBlog.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazBlog.EF;
public class BloggingContext : DbContext
{
    public DbSet<Blog> Blog { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public BloggingContext(DbContextOptions<BloggingContext> options)
    : base(options)
    {
        Debug.WriteLine($"{ContextId} context created.");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlite("Data Source=blogging.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
        .HasData(
            new Blog { Id = 1, Name = "amazBlog", ShortName = "AMZ", Description = "Made amazing" }
        );
        modelBuilder.Entity<Post>()
        .HasData(
            new Post { Id = 1, Title = "Welcome AmazBlog", Content = "My First Post", PublishTime = DateTime.Now, UpdateTime = DateTime.Now }
        );

        modelBuilder.Entity<Comment>()
        .HasOne(p => p.Posts)
        .WithMany(c => c.Comments);

        modelBuilder.Entity<Comment>()
        .HasKey(p => p.CommentId);
    }
}

