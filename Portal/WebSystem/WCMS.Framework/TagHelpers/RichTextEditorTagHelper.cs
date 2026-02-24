using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WCMS.Framework.TagHelpers
{
    /// <summary>
    /// Renders a textarea with CKEditor 5 CDN integration.
    /// Usage: &lt;wcms-editor for="Model.Content" height="400" toolbar="full" /&gt;
    /// </summary>
    [HtmlTargetElement("wcms-editor")]
    public class RichTextEditorTagHelper : TagHelper
    {
        /// <summary>
        /// Model expression for two-way binding (asp-for pattern).
        /// </summary>
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Editor height in pixels. Default is 300.
        /// </summary>
        public int Height { get; set; } = 300;

        /// <summary>
        /// Toolbar preset: "basic" or "full". Default is "basic".
        /// </summary>
        public string Toolbar { get; set; } = "basic";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var name = For?.Name ?? "editor";
            var id = name.Replace(".", "_");
            var value = For?.Model?.ToString() ?? string.Empty;

            var toolbarItems = Toolbar?.ToLowerInvariant() == "full"
                ? "'heading', '|', 'bold', 'italic', 'underline', 'strikethrough', '|', " +
                  "'link', 'blockQuote', 'insertTable', '|', " +
                  "'bulletedList', 'numberedList', 'outdent', 'indent', '|', " +
                  "'imageUpload', 'mediaEmbed', '|', 'undo', 'redo'"
                : "'bold', 'italic', 'link', '|', 'bulletedList', 'numberedList', '|', 'undo', 'redo'";

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "wcms-editor-wrapper");
            output.Content.SetHtmlContent(
                $"<textarea id=\"{id}\" name=\"{name}\" style=\"display:none;\">{System.Net.WebUtility.HtmlEncode(value)}</textarea>" +
                $"<div id=\"{id}_editor\" style=\"height:{Height}px;\"></div>" +
                "<script src=\"https://cdn.ckeditor.com/ckeditor5/41.4.2/classic/ckeditor.js\"></script>" +
                $"<script>" +
                $"ClassicEditor.create(document.getElementById('{id}_editor'),{{toolbar:[{toolbarItems}]}})" +
                $".then(editor=>{{" +
                $"editor.setData(document.getElementById('{id}').value);" +
                $"editor.model.document.on('change:data',()=>{{document.getElementById('{id}').value=editor.getData();}});" +
                $"}})" +
                $".catch(err=>console.error(err));" +
                $"</script>");
        }
    }
}
