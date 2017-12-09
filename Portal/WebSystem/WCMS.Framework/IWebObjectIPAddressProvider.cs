using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebObjectIPAddressProvider
    {
        WebObjectIPAddress Get(int id);
        IEnumerable<WebObjectIPAddress> GetList(int objectId, int recordId);
        int Update(WebObjectIPAddress item);
        bool Delete(int id);
    }
}
