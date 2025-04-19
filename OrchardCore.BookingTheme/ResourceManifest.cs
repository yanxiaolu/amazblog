using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace OrchardCore.BookingTheme
{
    public sealed class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static readonly ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            // --- Update Apple Style Definition URL ---
            _manifest
                .DefineStyle("bookingtheme-apple-style")
                // Use the new absolute path starting with "/style"
                .SetUrl("/style/css/apple-style.css", "/style/css/apple-style.css")
                .SetVersion("1.1.0") // Increment version if file content changed
                .SetDependencies("bootstrap");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
