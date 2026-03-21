using System;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebTextResourceProvider : IDataProvider<WebTextResource>
    {
        IEnumerable<WebTextResource> GetByDirectory(int directoryId);
        WebTextResource Get(string title);
        IEnumerable<WebTextResource> GetList(int contentTypeId = -2);
    }
}
