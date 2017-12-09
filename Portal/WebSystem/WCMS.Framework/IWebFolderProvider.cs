using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public interface IWebFolderProvider : IDataProvider<WebFolder>
    {
        IEnumerable<WebFolder> GetList(int parentId);
        IEnumerable<WebFolder> GetList(int objectId, int siteId);
        WebFolder Get(int parentId, string name);
    }
}
