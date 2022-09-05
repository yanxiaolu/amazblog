
namespace amazBlog.Data;
public class Comments
{
    public int CommentId { get; set; }
    public int CommentPostId { get; set; }
    public DateTime CommentDate { get; set; }
    public string CommentContent { get; set; }
    public Posts Posts { get; set; }
}