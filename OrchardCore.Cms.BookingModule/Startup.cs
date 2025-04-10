using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Cms.BookingModule.Drivers;
using OrchardCore.Cms.BookingModule.Migrations;
using OrchardCore.Cms.BookingModule.Services;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Security.Permissions;

namespace OrchardCore.Cms.BookingModule;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IEventBookingService, EventBookingService>();
        services.AddContentPart<Models.EventBooking>();
        services.AddScoped<IContentPartDisplayDriver, EventBookingContentPartDisplayDriver>();
        services.AddScoped<IDataMigration, EventBookingMigrations>();
        services.AddScoped<IPermissionProvider, Permissions>();
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes,
        IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "EventBooking.Details",
            areaName: "OrchardCore.Cms.BookingModule",  // 修改这里
            pattern: "event-booking/{contentItemId}",
            defaults: new { controller = "EventBooking", action = "Details" }
        );
    }
}