using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AmazBlog.Share;

namespace AmazBlog.Web.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Posts> Posts { get; set; }
    public DbSet<Options> Options { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

