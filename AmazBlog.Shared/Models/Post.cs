using System.Xml.Linq;
using AmazBlog.Shared.Models;

namespace AmazBlog.Shared.Models;
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public List<Comment>? Comments { get; set; }
    public Blog Blog { get; set; }
}

