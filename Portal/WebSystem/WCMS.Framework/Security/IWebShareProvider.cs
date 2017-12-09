using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public interface IWebShareProvider : IDataProvider<WebShare>
    {
        IEnumerable<WebShare> GetList(int objectId, int recordId);
        WebShare Get(int objectId, int recordId, int shareObjectId, int shareRecordId);
    }
}
