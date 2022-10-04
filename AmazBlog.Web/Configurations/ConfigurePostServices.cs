using AmazBlog.Core.Interfaces;
using AmazBlog.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AmazBlog.Web.Configuraions;

public static class ConfigurePostServices
{
    public static IServiceCollection AddPostServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IPostServices), typeof(PostServices));
        services.AddAuthentication();

        return services;
    }
}