using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.Photo
{
    public class AlbumEqualityComparer : IEqualityComparer<Album>
    {
        public bool Equals(Album x, Album y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(Album obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
