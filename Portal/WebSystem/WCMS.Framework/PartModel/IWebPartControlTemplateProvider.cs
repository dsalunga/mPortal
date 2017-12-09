using System;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebPartControlTemplateProvider : IDataProvider<WebPartControlTemplate>
    {
        WebPartControlTemplate Get(int partControlId, string identity);
        IEnumerable<WebPartControlTemplate> GetList(int partControlId);
    }
}
