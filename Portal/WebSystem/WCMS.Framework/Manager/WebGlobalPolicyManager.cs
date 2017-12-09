using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebGlobalPolicyManager : StandardDataManager<WebGlobalPolicy>, IWebGlobalPolicyProvider
    {
        protected IWebGlobalPolicyProvider _provider;

        public WebGlobalPolicyManager(IWebGlobalPolicyProvider provider)
            : base(provider)
        {
            _provider = provider;
        }
    }
}
