using AmazBlog.Web.Data;
using AmazBlog.EF;
using AmazBlog.Web.Configuraions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//builder.Services.AddScoped(_ => new BloggingContext(builder.Configuration.GetConnectionString("DefaultConn")));

//使用sqlite
// builder.Services.AddDbContext<BloggingContext>(
//    option => option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConn")));

//使用sqlserver
builder.Services.AddDbContext<BloggingContext>(
    option => option.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConn"), b => b.MigrationsAssembly("AmazBlog.EF")));
//加入service集合
builder.Services.AddPostServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

