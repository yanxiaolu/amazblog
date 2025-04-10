using Microsoft.AspNetCore.Http;
using OrchardCore.Cms.BookingModule.Models;
using OrchardCore.Cms.BookingModule.ViewModels;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
// 添加此命名空间
using OrchardCore.DisplayManagement.Views;

// 如果需要 BuildDisplayContext

namespace OrchardCore.Cms.BookingModule.Drivers;

public class EventBookingContentPartDisplayDriver : ContentPartDisplayDriver<EventBooking>
{
    private readonly IContentManager _contentManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EventBookingContentPartDisplayDriver(IContentManager contentManager, IHttpContextAccessor httpContextAccessor)
    {
        _contentManager = contentManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public override IDisplayResult Display(EventBooking part, BuildPartDisplayContext context)
    {
        return Initialize<EventBookingViewModel>("EventBooking", model =>
            {
                model.Title = part.Title;
                model.Description = part.Description;
                model.StartDateTime = part.StartDateTime;
                model.EndDateTime = part.EndDateTime;
                model.Location = part.Location;
                model.ContentItemId = part.ContentItem.ContentItemId;
                // 这里可以根据用户登录状态和预约情况设置 CanBook 和 BookingMessage
            })
            .Location("Content");
    }
}