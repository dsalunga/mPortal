using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WCMS.Framework.TagHelpers
{
    /// <summary>
    /// Renders an HTML5 date input with fallback.
    /// Usage: &lt;wcms-datepicker for="Model.StartDate" min="2024-01-01" max="2025-12-31" /&gt;
    /// </summary>
    [HtmlTargetElement("wcms-datepicker")]
    public class DatePickerTagHelper : TagHelper
    {
        /// <summary>
        /// Model expression for two-way binding (asp-for pattern).
        /// </summary>
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Minimum selectable date (yyyy-MM-dd).
        /// </summary>
        public string Min { get; set; }

        /// <summary>
        /// Maximum selectable date (yyyy-MM-dd).
        /// </summary>
        public string Max { get; set; }

        /// <summary>
        /// Date display format. Default is yyyy-MM-dd.
        /// </summary>
        public string Format { get; set; } = "yyyy-MM-dd";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var name = For?.Name ?? "date";
            var id = name.Replace(".", "_");

            string value = string.Empty;
            try
            {
                if (For?.Model is DateTime dt)
                {
                    value = dt.ToString(Format);
                }
                else if (For?.Model is DateTimeOffset dto)
                {
                    value = dto.ToString(Format);
                }
                else if (For?.Model != null)
                {
                    value = For.Model.ToString();
                }
            }
            catch (FormatException)
            {
                // Fall back to ISO 8601 if the custom format is invalid
                if (For?.Model is DateTime dtFallback)
                    value = dtFallback.ToString("yyyy-MM-dd");
                else if (For?.Model is DateTimeOffset dtoFallback)
                    value = dtoFallback.ToString("yyyy-MM-dd");
            }

            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("type", "date");
            output.Attributes.SetAttribute("id", id);
            output.Attributes.SetAttribute("name", name);
            output.Attributes.SetAttribute("class", "form-control wcms-datepicker");

            if (!string.IsNullOrEmpty(value))
                output.Attributes.SetAttribute("value", value);

            if (!string.IsNullOrEmpty(Min))
                output.Attributes.SetAttribute("min", Min);

            if (!string.IsNullOrEmpty(Max))
                output.Attributes.SetAttribute("max", Max);

            output.Attributes.SetAttribute("data-format", Format);

            output.PostElement.SetHtmlContent(
                $"<script>" +
                $"(function(){{" +
                $"var el=document.getElementById('{id}');" +
                $"if(el.type!=='date'){{" +
                $"el.setAttribute('placeholder','{Format}');" +
                $"el.setAttribute('pattern','\\\\d{{4}}-\\\\d{{2}}-\\\\d{{2}}');" +
                $"}}" +
                $"}})()" +
                $"</script>");
        }
    }
}
