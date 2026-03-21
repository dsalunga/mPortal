using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebThemeManager : StandardDataManager<WebTheme>, IWebThemeProvider
    {
        protected IWebThemeProvider _provider;

        public WebThemeManager(IWebThemeProvider provider)
            : base(provider)
        {
            _provider = provider;
        }
    }
}
