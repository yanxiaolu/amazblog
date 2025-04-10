using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.Cms.BookingModule.Models;
using OrchardCore.Cms.BookingModule.ViewModels;
using OrchardCore.Users.Services;

namespace OrchardCore.Cms.BookingModule.Controllers;

public class EventBookingController : Controller
{
    private readonly IContentManager _contentManager;
    private readonly IUserService _userService;

    public EventBookingController(IContentManager contentManager, IUserService userService)
    {
        _contentManager = contentManager;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Details(string contentItemId)
    {
        var contentItem = await _contentManager.GetAsync(contentItemId);
        if (contentItem == null)
        {
            return NotFound();
        }

        var eventBooking = contentItem.As<EventBooking>();
        if (eventBooking == null)
        {
            return NotFound();
        }

        var user = await _userService.GetAuthenticatedUserAsync(User);
        var isLoggedIn = user != null;

        var viewModel = new EventBookingViewModel
        {
            Title = contentItem.DisplayText,
            Location = eventBooking.Location?.Text,
            StartDateTime = eventBooking.StartDateTime?.Value,
            EndDateTime = eventBooking.EndDateTime?.Value,
            Capacity = eventBooking.Capacity?.Value ?? 0,
            BookedCount = eventBooking.BookedCount?.Value ?? 0,
            RequiresLogin = eventBooking.RequiresLogin?.Value ?? false,
            IsLoggedIn = isLoggedIn,
            CanBook = (!eventBooking.RequiresLogin?.Value ?? true) || isLoggedIn,
            ContentItemId = contentItemId
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Book(BookingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "预约信息无效，请检查后重试。";
            return RedirectToAction("Details", new { contentItemId = model.EventBookingContentItemId });
        }

        var contentItem = await _contentManager.GetAsync(model.EventBookingContentItemId);
        if (contentItem == null)
        {
            return NotFound();
        }

        var eventBooking = contentItem.As<EventBooking>();
        if (eventBooking == null || eventBooking.BookedCount?.Value >= eventBooking.Capacity?.Value)
        {
            TempData["Error"] = "活动已满，无法预约。";
            return RedirectToAction("Details", new { contentItemId = model.EventBookingContentItemId });
        }

        eventBooking.BookedCount.Value++;
        await _contentManager.UpdateAsync(contentItem);

        TempData["Success"] = "预约成功！";
        return RedirectToAction("Details", new { contentItemId = model.EventBookingContentItemId });
    }
}