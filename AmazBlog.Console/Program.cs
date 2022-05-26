//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using AmazBlog.Infrastructure;
using AmazBlog.Share;
using System.Linq;

namespace AmazBlog;

internal class Program
{
    private static void Main()
    {
        using var db = new AmazBlogContext();
        Console.WriteLine($"Database path: {db.DbPath}.");

        // Create
        Console.WriteLine("向Options表插入新的配置");
        db.Add(new Options { BlogId = 2, OptionName = "blogname", OPtionValue = "AmazBlog" });
        db.SaveChanges();
        // Read
        var options = db.Options.OrderBy(b => b.Id).First();
        Console.WriteLine($"{options.Id}-{options.BlogId}");

    }
}