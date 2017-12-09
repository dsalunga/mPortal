using System;
using System.Data;
using System.Collections.Generic;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public interface IWebSiteProvider : IDataProvider<WSite>
    {
        IEnumerable<WSite> GetList(int parentId);
        WSite Get(string identity);
        int GetMaxRank();
    }
}
