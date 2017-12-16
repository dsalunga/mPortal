using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.Photo.Providers;

namespace WCMS.WebSystem.WebParts.Photo
{
    public class AlbumLink : WebObjectBase, ISelfManager
    {
        private static IAlbumLinkProvider _provider = new AlbumLinkProvider();

        public AlbumLink()
        {
            SiteId = -1;
            ObjectId = -1;
            RecordId = -1;
            AlbumId = -1;
        }

        public int SiteId { get; set; }
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int AlbumId { get; set; }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public override int OBJECT_ID
        {
            get { return -1; }
        }

        public static IAlbumLinkProvider Provider { get { return _provider; } }
    }
}
