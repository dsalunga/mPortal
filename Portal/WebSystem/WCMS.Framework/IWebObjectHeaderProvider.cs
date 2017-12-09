using System;
using System.Data;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebObjectHeaderProvider : IDataProvider<WebObjectHeader>
    {
        WebObjectHeader Get(int objectId, int recordId, int textResourceId);
        IEnumerable<WebObjectHeader> GetList(int objectId, int recordId);
        IEnumerable<WebObjectHeader> GetList(int textResourceId);
    }
}
