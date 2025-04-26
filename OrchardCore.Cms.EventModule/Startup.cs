using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Cms.EventModule.Migrations;
using OrchardCore.Cms.EventModule.Models;
using OrchardCore.ContentManagement;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.Modules;

namespace OrchardCore.Cms.EventModule;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddContentPart<EventSeriesPart>();
        services.AddContentPart<BookingPart>();
        // 注册 Shape 表提供器
        services.AddScoped<IShapeTableProvider, EventShapeTableProvider>();
        services.AddScoped<IDataMigration, Migration>();
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes,
        IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "EventList",
            areaName: "OrchardCore.Cms.EventModule",
            pattern: "events",
            defaults: new { controller = "Event", action = "Index" }
        );
        routes.MapAreaControllerRoute(
            name: "Booking",
            areaName: "OrchardCore.Cms.EventModule",
            pattern: "booking/{eventSeriesId?}",
            defaults: new { controller = "Booking", action = "Index" }
        );
    }
}