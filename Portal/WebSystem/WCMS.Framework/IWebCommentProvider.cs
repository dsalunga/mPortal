using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebCommentProvider : IDataProvider<WebComment>
    {
        IEnumerable<WebComment> GetList(int userId = -2, int objectId = -2, int recordId = -2, int parentId = -2);
    }
}
