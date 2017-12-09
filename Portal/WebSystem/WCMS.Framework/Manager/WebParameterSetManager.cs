using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebParameterSetManager : StandardDataManager<WebParameterSet>, IWebParameterSetProvider
    {
        protected IWebParameterSetProvider _provider;

        public WebParameterSetManager(IWebParameterSetProvider provider)
            : base(provider)
        {
            _provider = provider;
        }
    }
}
