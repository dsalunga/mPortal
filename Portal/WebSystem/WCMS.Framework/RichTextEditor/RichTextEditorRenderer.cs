namespace WCMS.Framework.RichTextEditor;

/// <summary>
/// Options for rendering a rich text editor (TipTap OSS replacement for legacy FCKeditor).
/// </summary>
public sealed class RichTextEditorOptions
{
    public string Id { get; init; } = "editor";
    public string Name { get; init; } = "editor";
    public string InitialValue { get; init; } = string.Empty;
    public string CssClass { get; init; } = "wcms-richtext";
    public bool ReadOnly { get; init; }
    public int Height { get; init; } = 400;
}

/// <summary>
/// Renders a rich text editor textarea with TipTap OSS CDN integration.
/// Replaces the legacy FCKeditor 2.6.3 server-side rendering.
/// </summary>
public static class RichTextEditorRenderer
{
    /// <summary>
    /// Renders a textarea element that TipTap will enhance on the client side.
    /// Include the TipTap CDN scripts and call <see cref="RenderInitScript"/> to activate.
    /// </summary>
    public static string RenderTextarea(RichTextEditorOptions options)
    {
        var readOnlyAttr = options.ReadOnly ? " readonly" : string.Empty;
        var heightStyle = $" style=\"min-height:{options.Height}px\"";
        return $"<textarea id=\"{options.Id}\" name=\"{options.Name}\" " +
               $"class=\"{options.CssClass}\"{readOnlyAttr}{heightStyle}>" +
               $"{System.Net.WebUtility.HtmlEncode(options.InitialValue)}</textarea>";
    }

    /// <summary>
    /// Returns the TipTap CDN script tags (core + StarterKit bundle).
    /// Include this once per page, typically in the layout or section scripts.
    /// </summary>
    public static string RenderCdnScript()
    {
        return "<script src=\"https://cdn.jsdelivr.net/npm/@tiptap/core@2/dist/index.umd.js\"></script>" +
               "<script src=\"https://cdn.jsdelivr.net/npm/@tiptap/starter-kit@2/dist/index.umd.js\"></script>";
    }

    /// <summary>
    /// Returns a JavaScript snippet that initializes TipTap on the given textarea element.
    /// </summary>
    public static string RenderInitScript(RichTextEditorOptions options)
    {
        var editableJs = options.ReadOnly ? "editable: false," : string.Empty;
        return $@"<script>
    (function() {{
        var textarea = document.getElementById('{options.Id}');
        if (!textarea) return;
        var editorDiv = document.createElement('div');
        editorDiv.id = '{options.Id}_tiptap';
        editorDiv.style.minHeight = '{options.Height}px';
        editorDiv.style.border = '1px solid #ccc';
        editorDiv.style.padding = '0.5rem';
        textarea.style.display = 'none';
        textarea.parentNode.insertBefore(editorDiv, textarea.nextSibling);
        if (typeof window.TiptapCore !== 'undefined' && typeof window.TiptapStarterKit !== 'undefined') {{
            var editor = new window.TiptapCore.Editor({{
                element: editorDiv,
                extensions: [window.TiptapStarterKit.StarterKit],
                content: textarea.value,
                {editableJs}
                onUpdate: function({{ editor }}) {{
                    textarea.value = editor.getHTML();
                }}
            }});
            window.wcmsEditors = window.wcmsEditors || {{}};
            window.wcmsEditors['{options.Id}'] = editor;
        }}
    }})();
</script>";
    }
}
