using System;
using System.Data;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebPageElementProvider : IDataProvider<WebPageElement>
    {
        int GetCount(int recordId, int objectId, int templatePanelId);
        int GetMaxRank(int recordId, int objectId, int templatePanelId);
        IEnumerable<WebPageElement> GetList(int recordId, int objectId);
        IEnumerable<WebPageElement> GetList(int recordId, int objectId, int templatePanelId);
    }
}
