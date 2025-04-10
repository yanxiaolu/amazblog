using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models; // 添加此命名空间
using OrchardCore.Media.Fields; // 添加此命名空间

namespace OrchardCore.Cms.BookingModule.Models;

public class EventBooking : ContentPart
{
    public TitlePart Title { get; set; }
    public MediaField Description { get; set; }
    public TextField Location { get; set; }
    public DateTimeField StartDateTime { get; set; }
    public DateTimeField EndDateTime { get; set; }
    public int Capacity { get; set; } = 100;
    public int BookedCount { get; set; }
    public bool RequiresLogin { get; set; } = true;
}