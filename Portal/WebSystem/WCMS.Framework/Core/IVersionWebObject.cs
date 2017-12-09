namespace WCMS.Framework.Core
{
    public interface IVersionWebObject : IWebObject
    {
        int Version { get; set; }
        int VersionOf { get; set; }
    }
}
