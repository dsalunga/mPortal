namespace WCMS.Framework.Core
{
    public interface IVersionDataManager<T> : IDataManager<T> where T : IVersionWebObject
    {
        IVersionDataProvider<T> VersionProvider { get; }
    }
}
