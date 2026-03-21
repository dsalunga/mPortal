using System;
using System.Collections.Generic;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Content.Providers
{
    public interface IWebContentProvider : IDataProvider<WebContent>
    {
        IEnumerable<WebContent> GetList(int contentId, int versionOf, int versionNo, int directoryId);
        IEnumerable<WebContent> GetList(int siteId);
        WebContent Get(string title);
    }
}
