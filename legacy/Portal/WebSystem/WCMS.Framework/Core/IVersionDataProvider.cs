using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public interface IVersionDataProvider<T> : IDataProvider<T> where T : IVersionWebObject
    {
        IDataProvider<T> BaseProvider { get; }
    }

    public interface IVersionDataProvider : IDataProvider
    {
    }
}
