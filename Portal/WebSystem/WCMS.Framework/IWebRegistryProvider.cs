using System;
using System.Data;
using System.Collections.Generic;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public interface IWebRegistryProvider : IDataProvider<WebRegistry>
    {
        bool Delete(string key);
        WebRegistry Get(string key);
        WebRegistry Get(string key, int parentId);
        IEnumerable<WebRegistry> GetList(int parentId);
    }
}
