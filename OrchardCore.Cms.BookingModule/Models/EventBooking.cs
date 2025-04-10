using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using OrchardCore.Media.Fields;

namespace OrchardCore.Cms.BookingModule.Models;

public class EventBooking : ContentPart
{
    public TextField Location { get; set; }
    public DateTimeField StartDateTime { get; set; }
    public DateTimeField EndDateTime { get; set; }
    public NumericField Capacity { get; set; }
    public NumericField BookedCount { get; set; }
    public BooleanField RequiresLogin { get; set; }
    public MediaField Image { get; set; }
}