using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;

namespace OrchardCore.Cms.EventModule.Models;

public class EventSeriesPart:ContentPart
{
    public DateTimeField StartDate { get; set; } = new DateTimeField();
    public DateTimeField EndDate { get; set; } = new DateTimeField();
    public TextField RecurrencePattern { get; set; } = new TextField(); // "Daily", "Weekly", "Monthly"
    public NumericField TimeSlotDuration { get; set; } = new NumericField(); // In minutes
    public NumericField TimeSlotsPerDay { get; set; } = new NumericField();
    public TextField Location { get; set; } = new TextField();
    public MediaField EventBanner { get; set; } = new MediaField();
}