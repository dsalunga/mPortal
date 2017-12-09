using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebSiteIdentityProvider : IDataProvider<WebSiteIdentity>
    {
        IEnumerable<WebSiteIdentity> GetList(int siteId);
    }
}
