using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;
using OrchardCore.Media.Fields;

namespace OrchardCore.Cms.EventModule.Models;

public class EventsPart : ContentPart
{
    public DateTimeField StartTime { get; set; } = new DateTimeField(); // Corresponds to "startdate"
    public DateTimeField EndTime { get; set; } = new DateTimeField();   // Corresponds to "enddate"
    public TextField Location { get; set; } = new TextField();   // Corresponds to "eventplace"
    public MediaField EventBanner { get; set; } = new MediaField(); // Corresponds to "mediabanner"

}
