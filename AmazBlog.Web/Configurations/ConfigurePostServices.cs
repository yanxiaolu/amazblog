using AmazBlog.Core.Interfaces;
using AmazBlog.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace AmazBlog.Web.Configuraions;

public static class ConfigurePostServices
{
    public static IServiceCollection AddPostServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IPostServices), typeof(PostServices));

        return services;
    }
}