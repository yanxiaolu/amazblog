using Microsoft.Extensions.FileProviders; // Required for PhysicalFileProvider
using OrchardCore.Logging;
using System.IO; // Required for Path

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNLogHost();

builder.Services
    .AddOrchardCms()
    // ... other services ...
    ;

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// --- Add Custom Static File Mapping for BookingTheme ---
// Define the desired URL path prefix
var customThemeRequestPath = "/style"; // Your desired URL prefix

// Define the physical path to the theme's wwwroot directory
// Adjust the relative path ".." if your project structure is different
var themePhysicalPath = Path.Combine(app.Environment.ContentRootPath, "..", "OrchardCore.BookingTheme", "wwwroot");

// Ensure the directory exists before adding the provider
if (Directory.Exists(themePhysicalPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(themePhysicalPath),
        RequestPath = customThemeRequestPath // Map the physical path to "/style" URL
    });
}
// ------------------------------------------------------

// Keep the default UseStaticFiles to serve files from the main app's wwwroot
app.UseStaticFiles();

app.UseOrchardCore();

app.Run();