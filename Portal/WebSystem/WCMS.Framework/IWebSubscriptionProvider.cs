using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public interface IWebSubscriptionProvider : IDataProvider<WebSubscription>
    {
        IEnumerable<WebSubscription> GetList(int objectId, int recordId, int partId, int pageId, int allow);
    }
}
