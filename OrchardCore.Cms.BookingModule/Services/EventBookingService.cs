using OrchardCore.Cms.BookingModule.Models;
using OrchardCore.ContentManagement;
using YesSql;
using System.Linq; // 添加LINQ命名空间
using YesSql.Services; // 添加YesSql服务命名空间

namespace OrchardCore.Cms.BookingModule.Services;

public class EventBookingService : IEventBookingService
{
    private readonly ISession _session;
    private readonly IContentManager _contentManager;

    public EventBookingService(ISession session, IContentManager contentManager)
    {
        _session = session;
        _contentManager = contentManager;
    }

    public async Task CreateBooking(string eventBookingContentItemId, string userId)
    {
        var booking = new Booking
        {
            EventBookingContentItemId = int.Parse(eventBookingContentItemId),
            UserId = userId
        };
        _session.Save(booking);
        await _session.SaveChangesAsync();
    }

    public async Task<bool> HasUserBooked(string eventBookingContentItemId, string userId)
    {
        var parsedId = int.Parse(eventBookingContentItemId);

        // 使用YesSql的原生查询方式
        var bookings = await _session.Query<Booking>()
            .ListAsync();

        // 在内存中进行过滤
        return bookings.Any(b => b.EventBookingContentItemId == parsedId && b.UserId == userId);
    }
}