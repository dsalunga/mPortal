using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public interface ISharableContent
    {
        IEnumerable<WebShare> GetShares();
        WebShare AddShare(int shareObjectId, int shareRecordId, AllowSharing allow);
        void RemoveShare(IWebObject item);
    }
}
