
using WCMS.Framework.Core;

namespace WCMS.Framework.Social.Providers
{
    public interface IWallPluginProvider : IDataProvider<WallPlugin>
    {
        WallPlugin GetByEventType(int typeId);
    }
}
