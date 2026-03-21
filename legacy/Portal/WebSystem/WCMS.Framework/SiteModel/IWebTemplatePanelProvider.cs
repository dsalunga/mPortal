using System;
using System.Data;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebTemplatePanelProvider : IDataProvider<WebTemplatePanel>
    {
        IEnumerable<WebTemplatePanel> GetList(int objectId = -2, int recordId = -2);
    }
}
