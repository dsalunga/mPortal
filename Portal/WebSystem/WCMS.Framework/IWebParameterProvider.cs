using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public interface IWebParameterProvider : IDataProvider<WebParameter>
    {
        IEnumerable<WebParameter> GetList(int objectId, int recordId);
        WebParameter Get(int objectId, int recordId, string name);
    }
}
