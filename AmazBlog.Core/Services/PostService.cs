using AmazBlog.Core.Interfaces;
using AmazBlog.Shared.Models;
using AmazBlog.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;



namespace AmazBlog.Core.Services;

public class PostServices : IPostServices
{
    private readonly DbContextOptions _options;
    private BloggingContext _db;
    private NavigationManager _NavigationManager;
    private Post post = new();

    public PostServices(BloggingContext db, NavigationManager NavigationManager)
    {
        _db = db;
        _NavigationManager = NavigationManager;
    }
    //public async Task<List<Post>> PostsList()
    //{
    //    return await _db.Posts.ToListAsync();
    //}
    public List<Post> PostsList()
    {
        //_db = new BloggingContext();
        return _db.Posts.ToList();
    }

    public async Task<bool> Add(Post post)
    {
        //var existing = await _db.Posts.Where(p => p.Title == post.Title).FirstOrDefaultAsync();
        //if (existing != null)
        //    return false;
        await _db.AddAsync(post);
        await _db.SaveChangesAsync();
        _NavigationManager.NavigateTo("/");
        return true;
    }

    public async Task Update(Post post)
    {
        post.UpdateTime = DateTime.Now;
        _db.Entry(post).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }
    public async Task Delete(Post post)
    {
        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();
        _NavigationManager.NavigateTo("/");
    }
    public async Task<Post> GetAllPost(int id)
    {
        return await _db.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
    }
}