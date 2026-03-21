using System;
using System.Collections.Generic;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Content.Providers
{
    public interface IWebObjectContentProvider : IDataProvider<WebObjectContent>
    {
        WebObjectContent GetByObjectId(int objectId, int recordId);
        IEnumerable<WebObjectContent> GetList(int objectId);
    }
}
