using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace OrchardCore.BookingTheme
{
    // 资源管理配置类
    public sealed class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static readonly ResourceManifest _manifest;

        // 静态构造函数，用于初始化资源清单
        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            // --- 更新 Apple 样式定义 URL ---
            _manifest
                // 定义一个名为 "bookingtheme-apple-style" 的样式资源
                .DefineStyle("bookingtheme-apple-style")
                // 使用新的绝对路径，以 "/style" 开头
                .SetUrl("/style/css/apple-style.css", "/style/css/apple-style.css")
                // 设置版本号为 "1.1.0"，如果文件内容更改，需要递增版本号
                .SetVersion("1.1.0") 
                .SetDependencies("bootstrap"); // 依赖于 "bootstrap" 样式
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
