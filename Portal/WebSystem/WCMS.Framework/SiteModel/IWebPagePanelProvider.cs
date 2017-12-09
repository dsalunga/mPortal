using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;

namespace WCMS.Framework.Core
{
    public interface IWebPagePanelProvider : IDataProvider<WebPagePanel>
    {
        IEnumerable<WebPagePanel> GetList(int pageId);
        WebPagePanel Get(int templatePanelId, int pageId);
    }
}
