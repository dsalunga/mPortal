using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public interface IWebSkinProvider : IDataProvider<WebSkin>
    {
        IEnumerable<WebSkin> GetList(int objectId, int recordId);
    }
}
