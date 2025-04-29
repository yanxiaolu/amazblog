using OrchardCore.Logging;

// 创建 WebApplicationBuilder 实例
var builder = WebApplication.CreateBuilder(args);

// 使用 NLog 作为日志记录系统
builder.Host.UseNLogHost();

// 配置服务并添加 Orchard Core CMS
builder.Services
    .AddOrchardCms()
// // Orchard Core 特有的中间件管道配置，可以在这里自定义服务
// .ConfigureServices( services => {
// })
// .Configure( (app, routes, services) => {
// })
;

// 构建 WebApplication 实例
var app = builder.Build();

// 配置中间件，根据环境区分开发和生产模式
if (!app.Environment.IsDevelopment())
{
    // 非开发环境使用异常处理页面
    app.UseExceptionHandler("/Error");
    // 使用 HSTS，提高 HTTPS 安全性（默认 30 天，可根据需要调整）
    app.UseHsts();
}

// 强制使用 HTTPS 重定向
app.UseHttpsRedirection();

// 允许访问静态文件，如 CSS、JS、图片等
app.UseStaticFiles();

// 启用 Orchard Core 中间件，处理 CMS 功能
app.UseOrchardCore();

// 运行应用程序
app.Run();
