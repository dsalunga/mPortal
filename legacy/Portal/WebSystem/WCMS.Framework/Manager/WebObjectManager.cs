using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebObjectManager : StandardDataManager<WebObject>, IWebObjectProvider
    {
        protected IWebObjectProvider _provider;

        public WebObjectManager(IWebObjectProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebObjectProvider Members

        public bool Update(List<WebObject> items)
        {
            return _provider.Update(items);
        }

        #endregion
    }
}
