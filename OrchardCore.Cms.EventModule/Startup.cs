using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Cms.EventModule.Models;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.Modules;

namespace OrchardCore.Cms.EventModule;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddContentPart<EventsPart>();

        // 注册 Shape 表提供器
        services.AddScoped<IShapeTableProvider, EventShapeTableProvider>();
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "EventList",
            areaName: "OrchardCore.Cms.EventModule",
            pattern: "events",
            defaults: new { controller = "Event", action = "Index" }
        );
    }
}