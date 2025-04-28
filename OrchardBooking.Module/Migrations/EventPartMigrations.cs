using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Data.Migration;
using OrchardCore.Autoroute.Models;
using OrchardCore.Media.Fields;

namespace OrchardBooking.Module.Migrations
{
    /// <summary>
    /// 为 EventPart 定义数据迁移步骤。
    ///此类会创建 EventPart 内容部件及其关联的字段。
    ///（可选）将此部件附加到一个内容类型（例如 "Event"）上。
    /// </summary>
    public class EventTypeMigrations : DataMigration
    {
        // 依赖注入内容定义管理器服务
        private readonly IContentDefinitionManager _contentDefinitionManager;

        // 构造函数，用于注入 IContentDefinitionManager 服务
        public EventTypeMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        /// <summary>
        /// 当模块首次启用时，此方法会被执行。
        /// 它负责创建 Event 内容类型并附加内置部件和字段。
        /// </summary>
        /// <returns>返回此迁移的版本号（通常从 1 开始）。</returns>
        public async Task<int> Create()
        {
            // 使用 AlterTypeDefinitionAsync 定义 Event 内容类型
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Event", type => type
                    .Creatable() // 允许从管理界面创建此类型的内容项
                    .Listable()  // 允许在内容列表中显示此类型的内容项
                    .Draftable() // 支持草稿功能
                    .Versionable() // 支持版本控制
                    .Securable() // 支持权限设置
                    .WithDescription("代表一个可以安排的事件，使用内置部件和字段。")

                    //
                    // 附加系统内置的 TitlePart
                    //
                    .WithPart("TitlePart", part => part
                        .WithDisplayName("活动名称")
                        .WithSettings(new TitlePartSettings
                        {
                            Options = TitlePartOptions.EditableRequired // 设置标题为必填项
                        })
                    )
                    //
                    // 附加自定义的 EventDateTimePart
                    //
                    .WithPart("EventDateTimePart", part => part
                        .WithDisplayName("事件日期和时间")
                    )
                    //
                    //附加内置的 HTMLBody
                    //
                    .WithPart("HtmlBodyPart", part => part
                    .WithDisplayName("活动描述")
                    .WithDescription("活动内容概述")
                    .WithEditor("Wysiwyg") // 使用富文本编辑器
                    )
                    ///
                    /// 附加自定EventBannerPicPart
                    /// 
                    .WithPart("EventCoverPicPart", part => part
                        .WithDisplayName("活动封面")
                        .WithDescription("活动封面")
                    )
                    //
                    // 附加系统内置的 AutoroutePart
                    //
                    .WithPart("AutoroutePart", part => part
                        .WithSettings(new AutoroutePartSettings
                        {
                            AllowCustomPath = false, // 允许手动编辑 URL 路径
                            Pattern = "EventDetail/{{ ContentItem.DisplayText | slugify }}", // 默认 URL 模式使用标题的 slug
                            ShowHomepageOption = false // 不显示设置为首页的选项
                        })
                    )
                );
            // 定义 EventDateTimePart
            await _contentDefinitionManager.AlterPartDefinitionAsync("EventDateTimePart", part => part
                .WithDescription("事件日期和时间")
                .WithField("EventDateTime", field => field
                    .OfType("DateTimeField")
                    .WithDisplayName("事件日期和时间")
                    .WithDescription("事件发生的日期和时间。")
                    .WithEditor("DateTime")
                )
            );
            // 定义 EventCoverPicPart
            await _contentDefinitionManager.AlterPartDefinitionAsync("EventCoverPicPart", part => part
                .WithField("ImageUrl", field => field
                    .OfType(nameof(MediaField))
                    .WithDisplayName("Event Cover Pic")
                    .WithDescription("上传活动封面图片")
                )
            );
            // 迁移成功后返回当前版本号
            return 1;
        }

        // 如果以后需要修改这个内容类型（例如添加、删除或修改字段/部件），
        // 可以添加 UpdateFromX 方法。例如：
        // public int UpdateFrom1()
        // {
        //    // 在这里执行版本 2 的更改...
        //    _contentDefinitionManager.AlterTypeDefinitionAsync("Event", type => type
        //        .WithField("NewField", field => field
        //            .OfType("TextField")
        //            .WithDisplayName("新字段")
        //        )
        //    ).GetAwaiter().GetResult(); // 在 Update 方法中可能需要同步等待

        //    return 2; // 返回新的版本号
        // }
    }
}

