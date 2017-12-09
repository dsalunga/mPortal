using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebTextResourceManager: StandardDataManager<WebTextResource>, IWebTextResourceProvider
    {
        protected IWebTextResourceProvider _provider;

        public WebTextResourceManager(IWebTextResourceProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebTextResourceProvider Members

        public WebTextResource Get(string title)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));

            return _provider.Get(title);
        }

        public IEnumerable<WebTextResource> GetByDirectory(int directoryId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .Where(i => directoryId == -2 || i.DirectoryId == directoryId);

            return _provider.GetByDirectory(directoryId);
        }

        public IEnumerable<WebTextResource> GetList(int contentTypeId = -2)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => contentTypeId == -2 || i.ContentTypeId == contentTypeId);
            }

            return _provider.GetList(contentTypeId);
        }

        #endregion
    }
}
