
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace amazBlog.Data;

public class Posts
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public List<Comments> Comments { get; set; }
}