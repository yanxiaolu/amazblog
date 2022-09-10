using AmazBlog.Core.Interfaces;
using AmazBlog.Shared.Models;
using AmazBlog.EF;

namespace AmazBlog.Core.Services;

public class PostProvider : IPostServices
{
    private BloggingContext _db;
    public PostProvider(BloggingContext db)
    {
        _db = db;
    }
    public List<Post> PostsList()
    {
        return _db.Posts.ToList();
    }
}