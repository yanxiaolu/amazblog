using AmazBlog.Shared.Models;

namespace AmazBlog.Core.Interfaces;

public interface IPostServices
{

    List<Post> PostsList();
    Task<bool> Add(Post post);
    //Task<bool> Update(Post post);
    // Task<List<Post>> List();
}