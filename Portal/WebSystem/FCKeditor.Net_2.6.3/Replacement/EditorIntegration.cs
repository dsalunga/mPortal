namespace FredCK.FCKeditorV2;

public sealed class RichTextEditorOptions
{
    public string Id { get; init; } = "editor";
    public string Name { get; init; } = "editor";
    public string InitialValue { get; init; } = string.Empty;
    public string CssClass { get; init; } = "wcms-richtext";
    public bool ReadOnly { get; init; }
}

public static class RichTextEditorRenderer
{
    public static string RenderTextarea(RichTextEditorOptions options)
    {
        var readOnlyFlag = options.ReadOnly ? " readonly" : string.Empty;
        return $"<textarea id=\"{options.Id}\" name=\"{options.Name}\" class=\"{options.CssClass}\"{readOnlyFlag}>{options.InitialValue}</textarea>";
    }
}
