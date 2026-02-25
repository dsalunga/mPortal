namespace WCMS.Framework.RichTextEditor;

/// <summary>
/// Options for rendering a rich text editor (CKEditor 5 replacement for legacy FCKeditor).
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
/// Renders a rich text editor textarea with CKEditor 5 CDN integration.
/// Replaces the legacy FCKeditor 2.6.3 server-side rendering.
/// </summary>
public static class RichTextEditorRenderer
{
    /// <summary>
    /// Renders a textarea element that CKEditor 5 will enhance on the client side.
    /// Include the CKEditor 5 CDN script and call <see cref="RenderInitScript"/> to activate.
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
    /// Returns the CKEditor 5 CDN script tag (ClassicEditor from jsdelivr CDN).
    /// Include this once per page, typically in the layout or section scripts.
    /// </summary>
    public static string RenderCdnScript()
    {
        return "<script src=\"https://cdn.jsdelivr.net/npm/@ckeditor/ckeditor5-build-classic@43.3.1/build/ckeditor.js\" " +
               "integrity=\"sha384-\" crossorigin=\"anonymous\"></script>";
    }

    /// <summary>
    /// Returns a JavaScript snippet that initializes CKEditor 5 on the given textarea element.
    /// </summary>
    public static string RenderInitScript(RichTextEditorOptions options)
    {
        var readOnlyJs = options.ReadOnly ? "\n            editor.enableReadOnlyMode('wcms-readonly');" : string.Empty;
        return $@"<script>
    (function() {{
        if (typeof ClassicEditor !== 'undefined') {{
            ClassicEditor.create(document.querySelector('#{options.Id}'), {{
                toolbar: ['heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList',
                          '|', 'outdent', 'indent', '|', 'blockQuote', 'insertTable',
                          'undo', 'redo']
            }}).then(editor => {{{readOnlyJs}
                window.wcmsEditors = window.wcmsEditors || {{}};
                window.wcmsEditors['{options.Id}'] = editor;
            }}).catch(console.error);
        }}
    }})();
</script>";
    }
}
