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

            // --- Bootstrap 4 ---
            // You might use a CDN or local files. Ensure Popper.js is included if needed for JS components.
            _manifest
                .DefineStyle("bootstrap")
                .SetCdn("https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css", "https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.css")
                .SetCdnIntegrity("sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N", "sha384-..." ) // Add SRI for the debug version if needed
                .SetVersion("4.6.2");

            // --- Add Apple Style Definition (Depends on Bootstrap) ---
            _manifest
                .DefineStyle("bookingtheme-apple-style")
                .SetUrl("~/OrchardCore.BookingTheme/css/apple-style.css", "~/OrchardCore.BookingTheme/css/apple-style.css")
                .SetVersion("1.1.0") // Increment version
                .SetDependencies("bootstrap"); // Make it depend on Bootstrap

            // --- Bootstrap JS (Optional, if needed) ---
            // _manifest
            //     .DefineScript("bootstrap")
            //     .SetCdn("https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js", "https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.js")
            //     .SetCdnIntegrity("sha384-Fy6S3B9q64WdZWQUiU+q4/2Lc9npb8tCaSX9FK7E8HnRr0Jz8D6OP9dO5Vg3Q9ct", "sha384-...") // Add SRI for the debug version if needed
            //     .SetVersion("4.6.2");

        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
