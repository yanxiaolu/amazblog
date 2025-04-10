namespace OrchardCore.Cms.BookingModule.ViewModels;

public class EventBookingViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string Location { get; set; }
    public int Capacity { get; set; }
    public int BookedCount { get; set; }
    public bool RequiresLogin { get; set; }
    public string ContentItemId { get; set; } // 活动内容项的 ID
    public bool IsLoggedIn { get; set; } // 用户是否已登录
    public bool CanBook { get; set; } // 是否可以预约
    public string BookingMessage { get; set; } // 预约结果消息
}