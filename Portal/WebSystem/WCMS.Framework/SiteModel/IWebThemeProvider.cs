using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public interface IWebThemeProvider : IDataProvider<WebTheme>
    {
        //IEnumerable<WebSkin> GetList(int templateId);
    }
}
