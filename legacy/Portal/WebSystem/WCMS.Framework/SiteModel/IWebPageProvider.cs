using System;
using System.Data;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebPageProvider : IDataProvider<WPage>
    {
        int GetCount(int siteId);
        int GetMaxRank(int siteId);
        IEnumerable<WPage> GetList(int siteId, int parentId);
        IEnumerable<WPage> GetList(int siteId);
        WPage Get(int siteId, int parentId, string identity);
    }
}
