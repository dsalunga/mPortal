using System;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebTemplateProvider : IDataProvider<WebTemplate>
    {
        IEnumerable<WebTemplate> GetList(int themeId);
    }
}
