using System;
namespace AmazBlog.Shared.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public List<Post> Posts { get; set; }

        public Blog()
        {
        }
    }
}

