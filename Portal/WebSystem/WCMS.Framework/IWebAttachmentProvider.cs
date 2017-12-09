using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IWebAttachmentProvider : IDataProvider<WebAttachment>
    {
        IEnumerable<WebAttachment> GetList(int userId = -2, int objectId = -2, int recordId = -2);
        IEnumerable<WebAttachment> GetList(string batchGuid = null);
    }
}
