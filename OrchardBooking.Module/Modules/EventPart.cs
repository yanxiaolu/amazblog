using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;
using OrchardCore.Title.Models;

namespace OrchardBooking.Module.Modules;

public class EventPart : ContentPart
{

    public TitlePart EventName { get; set; }
    public DateTime EventDate { get; set; }
    public TextField Location { get; set; }
    public TextField Description { get; set; }
    public MediaField ImageUrl { get; set; }
}
