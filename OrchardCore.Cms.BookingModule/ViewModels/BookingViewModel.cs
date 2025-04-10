using System.ComponentModel.DataAnnotations;

namespace OrchardCore.Cms.BookingModule.ViewModels;

public class BookingViewModel
{
    [Required]
    public string EventBookingContentItemId { get; set; }
}