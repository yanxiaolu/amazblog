using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Cms.BookingModule.Models;
using OrchardCore.Cms.BookingModule.Services;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace OrchardCore.Cms.BookingModule
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // 注册内容部件
            services.AddContentPart<EventBooking>()
                   .UseDisplayDriver<EventBookingDisplayDriver>();

            // 注册Migration用于内容类型自动创建
            services.AddScoped<IDataMigration, Migrations>();

            // 注册服务
            services.AddScoped<IEventBookingService, EventBookingService>();
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            // 配置路由
            routes.MapAreaControllerRoute(
                name: "EventBooking",
                areaName: "OrchardCore.Cms.BookingModule",
                pattern: "EventBooking/{action}/{contentItemId?}",
                defaults: new { controller = "EventBooking", action = "Details" }
            );
        }
    }
}