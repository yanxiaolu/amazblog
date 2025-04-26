namespace OrchardCore.Cms.EventModule.ViewModels;

public class BookingViewModel
{
    public string EventSeriesId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<TimeSlot> AvailableSlots { get; set; }
}