using System;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebPartControlProvider : IDataProvider<WebPartControl>
    {
        WebPartControl Get(int partId, string identity);
        IEnumerable<WebPartControl> GetList(int partId);
        IEnumerable<WebPartControl> GetListByParentId(int parentId);
    }
}
