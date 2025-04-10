using OrchardCore.ContentFields.Fields;
using OrchardCore.Media.Fields;
using OrchardCore.Title.Models;

namespace OrchardCore.Cms.BookingModule.ViewModels;

public class EventBookingViewModel
{
    public string Title { get; set; }
    public string Location { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public int Capacity { get; set; }
    public int BookedCount { get; set; }
    public bool RequiresLogin { get; set; }
    public bool IsLoggedIn { get; set; }
    public bool CanBook { get; set; }
    public string ContentItemId { get; set; }
}