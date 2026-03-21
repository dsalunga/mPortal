using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebGroupProvider : IDataProvider<WebGroup>
    {
        WebGroup Get(string name);
        WebGroup Get(int parentId, string name);
        IEnumerable<WebGroup> GetList(int parentId);
    }
}
