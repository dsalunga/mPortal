using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebAddressProvider : IDataProvider<WebAddress>
    {
        WebAddress Get(int objectId, int recordId, string tag);
        IEnumerable<WebAddress> GetList(int objectId, int recordId);
    }
}
