using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Descriptors;

namespace OrchardCore.Cms.EventModule
{
    public class EventShapeTableProvider : IShapeTableProvider
    {
        // Change the return type from Task to ValueTask
        public ValueTask DiscoverAsync(ShapeTableBuilder builder)
        {
            // 为 Content 类型添加替代选项
            builder.Describe("Content")
                .OnDisplaying(displaying =>
                {
                    // Use Shape.TryGetProperty for safer access
                    if (displaying.Shape.TryGetProperty("ContentItem", out ContentItem contentItem))
                    {
                        // 检查是否为事件内容类型（请确保 "Events" 是正确的技术名称）
                        // Use the actual Content Type name, e.g., "EventsPart" if that's what you defined
                        if (contentItem.ContentType == "EventsPart") // Example: Changed "Events" to "EventsPart"
                        {
                            // 添加基于内容类型的替代选项
                            // Format: Content__[ContentType]
                            displaying.Shape.Metadata.Alternates.Add($"Content__{contentItem.ContentType}");

                            // 获取显示类型（Summary、Detail 等）
                            // Use Shape.TryGetProperty for safer access
                            if (displaying.Shape.TryGetProperty("DisplayType", out string displayType) && !string.IsNullOrEmpty(displayType))
                            {
                                // 添加基于显示类型的替代选项
                                // Format: Content_[DisplayType]__[ContentType]
                                displaying.Shape.Metadata.Alternates.Add($"Content_{displayType}__{contentItem.ContentType}");

                                // 为特定事件添加更具体的替代选项（基于 ContentItemId）
                                // ContentItemId is already a string, no need for extra processing
                                // Format: Content_[DisplayType]__[ContentType]__[ContentItemId]
                                displaying.Shape.Metadata.Alternates.Add($"Content_{displayType}__{contentItem.ContentType}__{contentItem.ContentItemId}");
                            }
                        }
                    }
                });

            // 为事件部件添加替代选项 (Assuming your part is named EventsPart)
            // If you have a specific shape for the part itself (often used when rendering the part directly)
            builder.Describe("EventsPart") // Use the technical name of your Content Part
                .OnDisplaying(displaying =>
                {
                    // Accessing properties might differ depending on how the shape is built.
                    // Use TryGetProperty to safely access the ContentItem associated with the shape.
                    if (displaying.Shape.TryGetProperty("ContentItem", out ContentItem contentItem))
                    {
                        // Add alternate based on the part name itself
                        // Format: [PartName]__[ContentType]
                        displaying.Shape.Metadata.Alternates.Add($"EventsPart__{contentItem.ContentType}");

                        // Get display type if available
                        if (displaying.Shape.TryGetProperty("DisplayType", out string displayType) && !string.IsNullOrEmpty(displayType))
                        {
                            // Add alternate based on display type and content type
                            // Format: [PartName]_[DisplayType]__[ContentType]
                            displaying.Shape.Metadata.Alternates.Add($"EventsPart_{displayType}__{contentItem.ContentType}");
                        }
                    }
                    // Attempt to get ContentItem from Model if not directly on shape properties
                    // This might be necessary depending on how the shape was constructed.
                    else if (displaying.Shape.TryGetProperty("Model", out dynamic model) && model?.ContentItem is ContentItem modelContentItem)
                    {
                        contentItem = modelContentItem;
                        // Add alternate based on the part name itself
                        // Format: [PartName]__[ContentType]
                        displaying.Shape.Metadata.Alternates.Add($"EventsPart__{contentItem.ContentType}");

                        // Get display type if available (might be on Model or Shape)
                        string displayType = model?.DisplayType ?? displaying.Shape.Metadata?.DisplayType;
                        if (!string.IsNullOrEmpty(displayType))
                        {
                            // Add alternate based on display type and content type
                            // Format: [PartName]_[DisplayType]__[ContentType]
                            displaying.Shape.Metadata.Alternates.Add($"EventsPart_{displayType}__{contentItem.ContentType}");
                        }
                    }
                });

            // Return ValueTask.CompletedTask instead of Task.CompletedTask
            return ValueTask.CompletedTask;
        }
    }
}