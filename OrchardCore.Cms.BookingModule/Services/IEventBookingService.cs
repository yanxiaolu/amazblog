namespace OrchardCore.Cms.BookingModule.Services;

public interface IEventBookingService
{
    Task<bool> HasUserBooked(string eventBookingContentItemId, string userName);
    Task CreateBooking(string eventBookingContentItemId, string userName);
}