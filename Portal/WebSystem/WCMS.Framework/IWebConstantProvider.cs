using System;
using System.Collections.Generic;

using WCMS.Framework;

namespace WCMS.Framework.Core
{
    public interface IWebConstantProvider : IDataProvider<WebConstant>
    {
        IEnumerable<WebConstant> GetList(string category);
        IEnumerable<WebConstant> GetList(int objectId);
    }
}
