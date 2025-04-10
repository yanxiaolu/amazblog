using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Autoroute.Models;
using OrchardCore.Cms.EventModule.Models;
using OrchardCore.Cms.EventModule.ViewModels;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Media;
using OrchardCore.Navigation;
using OrchardCore.Title.Models;
using YesSql;

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
        // Ensure model is initialized
        var model = new EventListViewModel();

        try
        {
            // Query content items using ISession and ContentItemIndex
            var allEventContentItems = await _session.Query<ContentItem, ContentItemIndex>(index =>
                index.ContentType == "event" && index.Published)
                .OrderByDescending(index => index.PublishedUtc)
                .ListAsync();

            foreach (var item in allEventContentItems)
            {
                var eventPart = item.ContentItem.As<EventPart>();
                var titlePart = item.ContentItem.As<TitlePart>();
                var autoroutePart = item.ContentItem.As<AutoroutePart>();

                string? bannerUrl = null;
                if (eventPart?.MediaBanner?.Paths?.Length > 0)
                {
                    var bannerPath = eventPart.MediaBanner.Paths[0];
                    bannerUrl = _mediaFileStore.MapPathToPublicUrl(bannerPath);
                }

                model.Events.Add(new EventViewModel
                {
                    ContentItemId = item.ContentItemId,
                    Title = titlePart?.Title ?? "[No Title Part]",
                    StartDate = eventPart?.StartDate?.Value,
                    EndDate = eventPart?.EndDate?.Value,
                    Place = eventPart?.EventPlace?.Text,
                    BannerUrl = bannerUrl,
                    DetailUrl = autoroutePart?.Path
                });
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "An unexpected error occurred retrieving event data.");
        }

        // Explicitly specify the view name to ensure correct binding
        return View("Index", model);
    }
}
