using AmazBlog.Core.Interfaces;
using AmazBlog.Shared.Models;
using AmazBlog.EF;
using Microsoft.EntityFrameworkCore;


namespace AmazBlog.Core.Services;

public class PostServices : IPostServices
{
    private BloggingContext _db;

    public PostServices(BloggingContext db)
    {
        _db = db;
    }
    public List<Post> PostsList()
    {
        return _db.Posts.ToList();
    }
    public async Task<bool> Add(Post post)
    {
        var existing = await _db.Posts.Where(p => p.Title == post.Title).FirstOrDefaultAsync();
        if (existing != null)
            return false;
        await _db.AddAsync(post);
        return await _db.SaveChangesAsync() > 0;
    }
}