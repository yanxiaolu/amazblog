using OrchardCore.Security.Permissions;

namespace OrchardCore.Cms.BookingModule;

public class Permissions : IPermissionProvider
{
    public static readonly Permission ManageEventBookings = new("ManageEventBookings", "管理活动预约");
    public static readonly Permission BookEvents = new("BookEvents", "预约活动");

    public Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
        return Task.FromResult(new[]
        {
            ManageEventBookings,
            BookEvents
        }.AsEnumerable());
    }

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
    {
        return new[]
        {
            new PermissionStereotype
            {
                Name = "Administrator",
                Permissions = new[] { ManageEventBookings }
            },
            new PermissionStereotype
            {
                Name = "Editor",
                Permissions = new[] { ManageEventBookings }
            },
            new PermissionStereotype
            {
                Name = "Author",
                Permissions = new[] { BookEvents }
            },
            new PermissionStereotype
            {
                Name = "Authenticated",
                Permissions = new[] { BookEvents }
            }
        };
    }
}