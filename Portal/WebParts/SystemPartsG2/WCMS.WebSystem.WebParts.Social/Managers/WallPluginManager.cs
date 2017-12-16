using System.Linq;
using WCMS.Framework.Core;

namespace WCMS.Framework.Social.Providers
{
    public class WallPluginManager : StandardDataManager<WallPlugin>, IWallPluginProvider
    {
        protected IWallPluginProvider _provider;

        public WallPluginManager(IWallPluginProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public WallPlugin GetByEventType(int typeId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values.FirstOrDefault(i => i.EventTypeId == typeId);

            return _provider.GetByEventType(typeId);
        }
    }
}
