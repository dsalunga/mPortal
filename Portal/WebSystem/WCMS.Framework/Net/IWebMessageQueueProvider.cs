using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Net
{
    public interface IWebMessageQueueProvider : IDataProvider<WebMessageQueue>
    {
        IEnumerable<WebMessageQueue> GetList(int status);
    }
}
