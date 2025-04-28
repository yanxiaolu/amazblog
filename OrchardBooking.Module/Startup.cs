using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardBooking.Module.Migrations;
using OrchardBooking.Module.Modules;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace OrchardBooking.Module;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddContentPart<EventPart>();
        services.AddScoped<IContentDefinitionManager, ContentDefinitionManager>();
        services.AddScoped<IDataMigration, EventTypeMigrations>();
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "Home",
            areaName: "OrchardBooking.Module",
            pattern: "",
            defaults: new { controller = "Home", action = "Index" }
        );
    }
}

