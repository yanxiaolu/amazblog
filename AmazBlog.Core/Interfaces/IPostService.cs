using AmazBlog.Shared.Models;

namespace AmazBlog.Core.Interfaces;

public interface IPostServices
{
    // Task<List<Post>> PostsList();
    List<Post> PostsList();
}