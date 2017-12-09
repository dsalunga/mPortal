using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebObjectSecurityPermissionProvider : IDataProvider<WebObjectSecurityPermission>
    {
        IEnumerable<WebObjectSecurityPermission> GetList(int objectSecurityId);
    }
}
