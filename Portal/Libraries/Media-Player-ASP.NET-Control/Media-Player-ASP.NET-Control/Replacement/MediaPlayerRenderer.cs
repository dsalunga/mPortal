namespace Media_Player_ASP.NET_Control;

public sealed class MediaPlayerOptions
{
    public string SourceUrl { get; init; } = string.Empty;
    public string MimeType { get; init; } = "video/mp4";
    public bool AutoPlay { get; init; }
    public bool Loop { get; init; }
}

public static class MediaPlayerRenderer
{
    public static string RenderVideoTag(MediaPlayerOptions options)
    {
        var autoPlay = options.AutoPlay ? " autoplay" : string.Empty;
        var loop = options.Loop ? " loop" : string.Empty;
        return $"<video controls{autoPlay}{loop}><source src=\"{options.SourceUrl}\" type=\"{options.MimeType}\" /></video>";
    }
}
