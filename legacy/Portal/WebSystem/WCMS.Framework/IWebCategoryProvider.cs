using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public interface IWebCategoryProvider : IDataProvider<WebCategory>
    {
        IEnumerable<WebCategory> GetList(int objectId, int parentId = -2);
    }
}
