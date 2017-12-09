using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public interface IWebShortUrlProvider : IDataProvider<WebShortUrl>
    {
        WebShortUrl GetByPageId(int pageId);
        WebShortUrl Get(string name);
    }
}
