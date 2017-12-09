using System;
using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public interface IWebObjectProvider : IDataProvider<WebObject>
    {
        bool Update(List<WebObject> items);
    }
}
