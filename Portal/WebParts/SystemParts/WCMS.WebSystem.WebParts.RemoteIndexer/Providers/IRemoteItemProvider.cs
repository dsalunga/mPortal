using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.RemoteIndexer.Providers
{
    public interface IRemoteItemProvider : IDataProvider<RemoteItem>
    {
        IEnumerable<RemoteItem> GetList(int libraryId, int parentId = -2);
    }
}
