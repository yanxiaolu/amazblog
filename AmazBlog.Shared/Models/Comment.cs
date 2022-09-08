using System;
namespace AmazBlog.Shared.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int CommentPostId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public Post Posts { get; set; }

        public Comment()
        {
        }
    }
}

