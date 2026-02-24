using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WCMS.Framework.TagHelpers
{
    /// <summary>
    /// Container tag helper that renders Bootstrap 5 nav-tabs with tab panes.
    /// Usage: &lt;wcms-tabs id="myTabs"&gt;&lt;wcms-tab title="Tab1" active="true"&gt;...&lt;/wcms-tab&gt;&lt;/wcms-tabs&gt;
    /// </summary>
    [HtmlTargetElement("wcms-tabs")]
    public class TabControlTagHelper : TagHelper
    {
        /// <summary>
        /// Unique identifier for the tab control.
        /// </summary>
        public string Id { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tabs = new List<TabItemContext>();
            context.Items[typeof(TabControlTagHelper)] = tabs;

            // Process child <wcms-tab> elements so they register themselves
            await output.GetChildContentAsync();

            output.TagName = "div";
            output.Attributes.SetAttribute("id", Id);

            var navId = $"{Id}-nav";
            var contentId = $"{Id}-content";

            output.Content.Clear();

            // Render nav tabs
            output.Content.AppendHtml($"<ul class=\"nav nav-tabs\" id=\"{navId}\" role=\"tablist\">");
            for (int i = 0; i < tabs.Count; i++)
            {
                var tab = tabs[i];
                var tabId = $"{Id}-tab-{i}";
                var paneId = $"{Id}-pane-{i}";
                var activeClass = tab.Active ? " active" : "";
                var selected = tab.Active ? "true" : "false";

                output.Content.AppendHtml(
                    $"<li class=\"nav-item\" role=\"presentation\">" +
                    $"<button class=\"nav-link{activeClass}\" id=\"{tabId}\" data-bs-toggle=\"tab\" " +
                    $"data-bs-target=\"#{paneId}\" type=\"button\" role=\"tab\" " +
                    $"aria-controls=\"{paneId}\" aria-selected=\"{selected}\">{tab.Title}</button></li>");
            }
            output.Content.AppendHtml("</ul>");

            // Render tab panes
            output.Content.AppendHtml($"<div class=\"tab-content\" id=\"{contentId}\">");
            for (int i = 0; i < tabs.Count; i++)
            {
                var tab = tabs[i];
                var tabId = $"{Id}-tab-{i}";
                var paneId = $"{Id}-pane-{i}";
                var activeClass = tab.Active ? " show active" : "";

                output.Content.AppendHtml(
                    $"<div class=\"tab-pane fade{activeClass}\" id=\"{paneId}\" role=\"tabpanel\" " +
                    $"aria-labelledby=\"{tabId}\">");
                output.Content.AppendHtml(tab.Content);
                output.Content.AppendHtml("</div>");
            }
            output.Content.AppendHtml("</div>");
        }
    }

    /// <summary>
    /// Represents a single tab within a <see cref="TabControlTagHelper"/>.
    /// </summary>
    [HtmlTargetElement("wcms-tab", ParentTag = "wcms-tabs")]
    public class TabItemTagHelper : TagHelper
    {
        /// <summary>
        /// Display title of the tab.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Whether this tab is initially active.
        /// </summary>
        public bool Active { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var tabs = (List<TabItemContext>)context.Items[typeof(TabControlTagHelper)];

            tabs.Add(new TabItemContext
            {
                Title = Title,
                Active = Active,
                Content = childContent.GetContent()
            });

            // Suppress output; the parent renders everything
            output.SuppressOutput();
        }
    }

    internal class TabItemContext
    {
        public string Title { get; set; }
        public bool Active { get; set; }
        public string Content { get; set; }
    }
}
