using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebObjectSecurityProvider : IDataProvider<WebObjectSecurity>
    {
        WebObjectSecurity Get(int objectId, int recordId,
            int securityObjectId, int securityRecordId, int public2);
        IEnumerable<WebObjectSecurity> GetList(int objectId, int recordId);
        IEnumerable<WebObjectSecurity> GetList(int objectId, int recordId,
            int securityObjectId, int securityRecordId, int public2);
    }
}
