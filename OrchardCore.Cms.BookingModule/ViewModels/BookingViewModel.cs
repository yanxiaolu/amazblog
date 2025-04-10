using System.ComponentModel.DataAnnotations;

namespace OrchardCore.Cms.BookingModule.ViewModels;

public class BookingViewModel
{
    [Required]
    public string EventBookingContentItemId { get; set; }

    // 可以添加更多预约字段，如联系人信息、预约人数等
    public string ContactName { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public int NumberOfAttendees { get; set; } = 1;
    public string Remarks { get; set; }
}