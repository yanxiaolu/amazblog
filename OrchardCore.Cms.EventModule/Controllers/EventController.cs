using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Navigation;
using YesSql;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using OrchardCore.Media;
using OrchardCore.Cms.EventModule.ViewModels;
using OrchardCore.Cms.EventModule.Models;
using OrchardCore.Title.Models;
using OrchardCore.Autoroute.Models;

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
                var eventsPart = item.As<EventsPart>();     // 注意这里使用正确的类名 EventsPart
                var titlePart = item.As<TitlePart>();       // 标题部件，提供标题功能
                var autoroutePart = item.As<AutoroutePart>(); // 自动路由部件，提供 URL 路径
                
                // 调试：查看内容项的完整 JSON 结构，帮助排查字段问题
                // var json = JsonConvert.SerializeObject(item.Content, Formatting.Indented);
                // System.Diagnostics.Debug.WriteLine(json);
                
                // 获取 DateTimeField 中的日期值的几种方法
                
                // 方法1：直接从强类型对象中获取 (推荐方法)
                DateTime? startDate = eventsPart?.StartTime?.Value; // 正确使用 StartTime 字段
                DateTime? endDate = eventsPart?.EndTime?.Value;     // 正确使用 EndTime 字段
                
                // 方法2：通过动态访问 Content 中的数据
                // var startDate = item.Content.EventsPart?.StartTime?.Value;
                
                // 方法3：如果 JSON 结构中的字段名与模型类中的不一致，可以尝试直接访问原始字段名
                // var startDate = item.Content["EventsPart"]?["StartDate"]?["Value"]?.ToString();
                // if (startDate != null) startDate = DateTime.Parse(startDate);
                
                // 处理媒体路径，转换为可访问的 URL
                string? bannerUrl = null;
                if (eventsPart?.EventBanner?.Paths?.Length > 0) // 正确使用 EventBanner 字段
                {
                    var bannerPath = eventsPart.EventBanner.Paths[0];
                    bannerUrl = _mediaFileStore.MapPathToPublicUrl(bannerPath);
                }

                // 创建视图模型
                model.Events.Add(new EventViewModel
                {
                    ContentItemId = item.ContentItemId,
                    Title = titlePart?.Title ?? item.DisplayText ?? "[No Title]",
                    StartDate = startDate, // 使用上面获取的日期值
                    EndDate = endDate,     // 使用上面获取的日期值
                    Place = eventsPart?.Location?.Text, // 正确使用 Location 字段
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
