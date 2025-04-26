using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Navigation;
using YesSql;
using OrchardCore.Media;
using OrchardCore.Cms.EventModule.ViewModels;
using OrchardCore.Title.Models;
using OrchardCore.Autoroute.Models;
using OrchardCore.Cms.EventModule.Models;

namespace OrchardCore.Cms.EventModule.Controllers;

public class EventController : Controller
{
    private readonly ISession _session;
    private readonly IContentManager _contentManager;
    private readonly IMediaFileStore _mediaFileStore;

    public EventController(
        ISession session,
        IContentManager contentManager,
        IMediaFileStore mediaFileStore
    )
    {
        _session = session;
        _contentManager = contentManager;
        _mediaFileStore = mediaFileStore;
    }

    // Action to display the list page (e.g., at /events)
    public async Task<IActionResult> Index(PagerParameters pagerParameters)
    {
        // 初始化模型
        var model = new EventListViewModel();

        try
        {
            // 查询所有已发布的 events 类型内容项
            // 注意：这里使用 "events" 作为内容类型名称，这应该与您在 Admin UI 中创建的内容类型名称一致
            var allEventContentItems = await _session.Query<ContentItem, ContentItemIndex>(index =>
                index.ContentType == "EventsPart" && index.Published)
                .OrderByDescending(index => index.PublishedUtc)
                .ListAsync();

            foreach (var item in allEventContentItems)
            {
                /*
                 * ContentItem 的结构解析：
                 *
                 * ContentItem 是一个复合对象，包含多个内容部件 (ContentParts)
                 * Content 属性是一个 JsonDynamicObject，包含了所有部件的数据
                 * As<T>() 方法从 Content 中提取并创建指定类型的部件实例
                 */

                // 从 ContentItem 中提取各种部件
                var eventsPart = item.As<EventSeriesPart>(); // 正确的类名 EventSeriesPart
                var titlePart = item.As<TitlePart>();
                var autoroutePart = item.As<AutoroutePart>();

                // 正确获取日期字段
                DateTime? startDate = eventsPart?.StartDate?.Value;
                DateTime? endDate = eventsPart?.EndDate?.Value;

                // 处理媒体路径，转换为可访问的 URL
                string? bannerUrl = null;
                if (eventsPart?.EventBanner?.Paths?.Length > 0)
                {
                    var bannerPath = eventsPart.EventBanner.Paths[0];
                    bannerUrl = _mediaFileStore.MapPathToPublicUrl(bannerPath);
                }

                // 创建视图模型
                model.Events.Add(new EventViewModel
                {
                    ContentItemId = item.ContentItemId,
                    Title = titlePart?.Title ?? item.DisplayText ?? "[No Title]",
                    StartDate = startDate,
                    EndDate = endDate,
                    Place = eventsPart?.Location?.Text,
                    BannerUrl = bannerUrl,
                    DetailUrl = autoroutePart?.Path
                });
            }

            // 返回包含事件列表的视图
            return View(model);
        }
        catch (Exception ex)
        {
            // 记录异常
            System.Diagnostics.Debug.WriteLine($"Error retrieving events: {ex}");
            // Consider using a proper logging framework instead of Debug.WriteLine
            return StatusCode(500, "An error occurred while retrieving event data.");
        }
    }
}
