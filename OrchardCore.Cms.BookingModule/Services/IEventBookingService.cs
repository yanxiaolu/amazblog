namespace OrchardCore.Cms.BookingModule.Services;

public interface IEventBookingService
{
    Task CreateBooking(string eventBookingContentItemId, string userId);
    Task<bool> HasUserBooked(string eventBookingContentItemId, string userId);
}