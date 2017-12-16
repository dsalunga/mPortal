using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Content.Providers
{
    class WebContentManager : StandardDataManager<WebContent>, IWebContentProvider
    {
        protected IWebContentProvider _provider;

        public WebContentManager(IWebContentProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebContentProvider Members

        public IEnumerable<WebContent> GetList(int contentId, int versionOf, int versionNo, int directoryId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return (from i in _cache.ObjectCache.Values
                        where (contentId == -1 || i.Id == contentId)
                        && (versionNo == -1 ||
                            (versionNo == -2 && i.VersionNo > 0) ||
                            i.VersionNo == versionNo)
                        && (versionOf == -2 || i.VersionOf == versionOf)
                        && (directoryId == -2 || i.DirectoryId == directoryId)
                        select i);
            }

            return _provider.GetList(contentId, versionOf, versionNo, directoryId);
        }

        public WebContent Get(string title)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache
                    .ObjectCache
                    .Values
                    .FirstOrDefault(i => i.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));

            return _provider.Get(title);
        }

        public IEnumerable<WebContent> GetList(int siteId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return from i in _cache.ObjectCache.Values
                       where (siteId == -2 || i.SiteId == siteId)
                       select i;
            }

            return _provider.GetList(siteId);
        }

        #endregion
    }
}
