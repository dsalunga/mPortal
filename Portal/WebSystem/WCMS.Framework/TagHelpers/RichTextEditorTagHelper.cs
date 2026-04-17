using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WCMS.Framework.TagHelpers
{
    /// <summary>
    /// Renders a textarea with TipTap OSS CDN integration.
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
            var encodedValue = System.Net.WebUtility.HtmlEncode(value);

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "wcms-editor-wrapper");
            output.Content.SetHtmlContent(
                $"<textarea id=\"{id}\" name=\"{name}\" style=\"display:none;\">{encodedValue}</textarea>" +
                $"<div id=\"{id}_editor\" style=\"height:{Height}px;border:1px solid #ccc;padding:0.5rem;\"></div>" +
                "<script src=\"https://cdn.jsdelivr.net/npm/@tiptap/core@2/dist/index.umd.js\"></script>" +
                "<script src=\"https://cdn.jsdelivr.net/npm/@tiptap/starter-kit@2/dist/index.umd.js\"></script>" +
                $"<script>" +
                $"(function(){{" +
                $"var ta=document.getElementById('{id}');" +
                $"var el=document.getElementById('{id}_editor');" +
                $"if(!ta||!el||typeof window.TiptapCore==='undefined')return;" +
                $"var editor=new window.TiptapCore.Editor({{" +
                $"element:el," +
                $"extensions:[window.TiptapStarterKit.StarterKit]," +
                $"content:ta.value," +
                $"onUpdate:function({{editor}}){{ta.value=editor.getHTML();}}" +
                $"}});" +
                $"window.wcmsEditors=window.wcmsEditors||{{}};" +
                $"window.wcmsEditors['{id}']=editor;" +
                $"}})();" +
                $"</script>");
        }
    }
}
