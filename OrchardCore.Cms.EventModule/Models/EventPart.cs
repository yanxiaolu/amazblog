using System;
using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;
using OrchardCore.Media.Fields;

namespace OrchardCore.Cms.EventModule.Models;

public class EventPart : ContentPart
{
    public DateTimeField StartDate { get; set; } = new DateTimeField(); // Corresponds to "startdate"
    public DateTimeField EndDate { get; set; } = new DateTimeField();   // Corresponds to "enddate"
    public TextField EventPlace { get; set; } = new TextField();   // Corresponds to "eventplace"
    public MediaField MediaBanner { get; set; } = new MediaField(); // Corresponds to "mediabanner"

}
