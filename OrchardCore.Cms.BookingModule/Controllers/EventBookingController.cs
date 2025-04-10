using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.Cms.BookingModule.Services;
using OrchardCore.Cms.BookingModule.ViewModels;
using OrchardCore.Users.Services;
using YesSql;

namespace OrchardCore.Cms.BookingModule.Controllers;

public class EventBookingController : Controller // 添加继承Controller基类
{
    private readonly IContentManager _contentManager;
    private readonly IEventBookingService _eventBookingService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserService _userService;
    private readonly ISession _session;

    public EventBookingController(IContentManager contentManager,
        IEventBookingService eventBookingService,
        IAuthorizationService authorizationService,
        IUserService userService,
        ISession session)
    {
        _contentManager = contentManager;
        _eventBookingService = eventBookingService;
        _authorizationService = authorizationService;
        _userService = userService;
        _session = session;
    }

    [HttpGet]
    public async Task<IActionResult> Details(string contentItemId)
    {
        Console.WriteLine("debug: EventBookingController: Details: contentItemId is " + contentItemId);
        var eventBookingContentItem = await _contentManager.GetAsync(contentItemId);
        if (eventBookingContentItem == null)
        {
            //打印日志
            Console.WriteLine("debug: EventBookingController: Details: contentItemId is null");
            return NotFound();
        }

        foreach (var part in eventBookingContentItem.Content.Node)
        {
            Console.WriteLine("debug: EventBookingController: Details: part is " + part);
        }

        var eventBookingPart = eventBookingContentItem.As<Models.EventBooking>();
        if (eventBookingPart == null)
        {
            //打印日志
            Console.WriteLine("debug: EventBookingController: Details: eventBookingPart is null");
            return NotFound();
        }

        var user = await _userService.GetAuthenticatedUserAsync(User);
        var isLoggedIn = user != null;
        var canBook = true;
        string bookingMessage = null;

        // 这里需要根据实际情况添加逻辑判断活动是否可以预约
        // 例如：检查活动容量、是否需要登录、用户是否已预约等
        if (isLoggedIn)
        {
            // 检查用户是否已经预约过该活动
            var hasBooked = await _eventBookingService.HasUserBooked(contentItemId, user.UserName);
            if (hasBooked)
            {
                canBook = false;
                bookingMessage = "您已经预约过该活动。";
            }
        }

        var viewModel = new EventBookingViewModel
        {
            Title = eventBookingPart.Title,
            Description = eventBookingPart.Description,
            StartDateTime = eventBookingPart.StartDateTime,
            EndDateTime = eventBookingPart.EndDateTime,
            Location = eventBookingPart.Location,
            Capacity = 100, // 这里需要根据实际情况设置容量
            BookedCount = 0, // 从数据库获取实际预约人数
            RequiresLogin = true, // 根据需求设置是否需要登录
            ContentItemId = contentItemId,
            IsLoggedIn = isLoggedIn,
            CanBook = canBook,
            BookingMessage = bookingMessage
        };

        return View(viewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Book(BookingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Details", new { contentItemId = model.EventBookingContentItemId });
        }

        var eventBookingContentItem = await _contentManager.GetAsync(model.EventBookingContentItemId);
        if (eventBookingContentItem == null)
        {
            return NotFound();
        }

        var eventBookingPart = eventBookingContentItem.As<Models.EventBooking>();
        if (eventBookingPart == null)
        {
            return NotFound();
        }

        var user = await _userService.GetAuthenticatedUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        // if (eventBookingPart.RequiresLogin && user == null)
        // {
        //     return RedirectToAction("Details", new { contentItemId = model.EventBookingContentItemId });
        // }

        // if (eventBookingPart.BookedCount >= eventBookingPart.Capacity)
        // {
        //     TempData["Error"] = "活动已满，无法预约。";
        //     return RedirectToAction("Details", new { contentItemId = model.EventBookingContentItemId });
        // }

        // 检查用户是否已经预约过该活动
        var hasBooked = await _eventBookingService.HasUserBooked(model.EventBookingContentItemId, user.UserName);
        if (hasBooked)
        {
            TempData["Error"] = "您已经预约过该活动。";
            return RedirectToAction("Details", new { contentItemId = model.EventBookingContentItemId });
        }

        // 创建预约记录
        await _eventBookingService.CreateBooking(model.EventBookingContentItemId, user.UserName);

        // 更新活动已预约人数
        // eventBookingPart.BookedCount++;
        await _contentManager.UpdateAsync(eventBookingContentItem);

        TempData["Success"] = "预约成功！";
        return RedirectToAction("Details", new { contentItemId = model.EventBookingContentItemId });
    }
}