using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebPermissionManager : StandardDataManager<WebPermission>, IWebPermissionProvider
    {
        protected IWebPermissionProvider _provider;

        public WebPermissionManager(IWebPermissionProvider provider)
            : base(provider)
        {
            _provider = provider;
        }
    }
}
