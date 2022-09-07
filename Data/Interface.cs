namespace amazBlog.Data
{
    public interface IBlogSetting
    {
        IEnumerable<Blog> GetAllSetting();
    }

    public class BlogSetting:IBlogSetting
    {
        private BloggingContext _db;
        public BlogSetting(BloggingContext db)
        {
            _db = db;
        }

        public IEnumerable<Blog> GetAllSetting()
        {
            return _db.Blog.ToList();
        }
    }
}
