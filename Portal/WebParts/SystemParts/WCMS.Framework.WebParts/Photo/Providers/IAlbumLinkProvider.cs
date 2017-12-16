using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Photo.Providers
{
    public interface IAlbumLinkProvider : IDataProvider<AlbumLink>
    {
        IEnumerable<AlbumLink> GetList(int objectId, int recordId);
    }
}
