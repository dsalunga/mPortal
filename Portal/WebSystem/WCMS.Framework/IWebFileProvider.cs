using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public interface IWebFileProvider : IDataProvider<WebFile>
    {
        WebFile Get(int folderId, int objectId, int recordId);
        WebFile Get(int objectId, int recordId);
        IEnumerable<WebFile> GetList(int folderId);
        IEnumerable<WebFile> GetList(int objectId, int recordId);
    }
}
