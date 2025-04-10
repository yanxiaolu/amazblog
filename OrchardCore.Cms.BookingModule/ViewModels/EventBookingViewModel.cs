using OrchardCore.ContentFields.Fields;
using OrchardCore.Media.Fields;
using OrchardCore.Title.Models;

namespace OrchardCore.Cms.BookingModule.ViewModels;

public class EventBookingViewModel
{
    public TitlePart Title { get; set; }
    public MediaField Description { get; set; }
    public TextField Location { get; set; }
    public DateTimeField StartDateTime { get; set; }
    public DateTimeField EndDateTime { get; set; }
    public int Capacity { get; set; }
    public int BookedCount { get; set; }
    public bool RequiresLogin { get; set; }
    public string ContentItemId { get; set; } // 活动内容项的 ID
    public bool IsLoggedIn { get; set; } // 用户是否已登录
    public bool CanBook { get; set; } // 是否可以预约
    public string BookingMessage { get; set; } // 预约结果消息
}