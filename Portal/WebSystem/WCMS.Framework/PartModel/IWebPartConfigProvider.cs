using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebPartConfigProvider : IDataProvider<WebPartConfig>
    {
        //IEnumerable<WebPartConfig> GetList();
        //WebPartConfig Get(int partConfigId);
        IEnumerable<WebPartConfig> GetList(int partId);
        //int Update(WebPartConfig item);
        //bool Delete(int partConfigId);
    }
}
