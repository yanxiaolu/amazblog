using AmazBlog.Shared.Models;

namespace AmazBlog.Core.Interfaces;

public interface IPostServices
{

    Task<List<Post>> PostsList();
    Task<bool> Add(Post post);
    Task Update(Post post);
    Task Delete(Post post);
    Task<Post> GetAllPost(int id);
}