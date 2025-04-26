using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace OrchardCore.Cms.EventModule.Models;

public class BookingPart:ContentPart
{
    public ContentPickerField EventSeries { get; set; } = new ContentPickerField();
    public DateField BookedDate { get; set; } = new DateField();
    public TimeField BookedTime { get; set; } = new TimeField();
    public TextField User { get; set; } = new TextField();
}