using OrchardCore.Entities;

namespace OrchardCore.Cms.BookingModule.Models;

public class Booking : Entity
{
    public int EventBookingContentItemId { get; set; } // 关联的活动内容项 ID
    public string UserId { get; set; } // 预约用户的 ID
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
}