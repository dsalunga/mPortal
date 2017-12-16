using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Photo.Providers
{
    public interface IAlbumProvider : IDataProvider<Album>
    {
        Album Get(string title);
        IEnumerable<Album> GetList(int objectId, int recordId);
    }
}
