namespace OrchardCore.Cms.EventModule.ViewModels;

public class EventListViewModel
{
    public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();
}

public class EventViewModel
{
    public string? Title { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Place { get; set; }
    public string? BannerUrl { get; set; }
    public string? DetailUrl { get; set; } // URL to the event's own page (from AutoroutePart)
    public string? ContentItemId { get; set; } // Good to have for keys or diagnostics
}
