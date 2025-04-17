using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace OrchardCore.BookingTheme
{
    // Note: While this uses IConfigureOptions, placing it in the Theme project
    // relies on Orchard Core's Startup scanning. A more standard theme approach
    // uses IResourceManifestProvider as discussed previously.
    // However, to modify *this specific file* as requested:
    public sealed class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static readonly ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            // --- Add Apple Style Definition ---
            _manifest
                .DefineStyle("bookingtheme-apple-style") // Unique name for your style
                .SetUrl("~/OrchardCore.BookingTheme/css/apple-style.css", "~/OrchardCore.BookingTheme/css/apple-style.css") // Path to your CSS file
                .SetVersion("1.0.0"); // Optional: Version number

        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }


    }
}
