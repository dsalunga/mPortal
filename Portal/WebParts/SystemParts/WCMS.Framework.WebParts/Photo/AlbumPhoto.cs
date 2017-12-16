using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Photo.Providers;

namespace WCMS.WebSystem.WebParts.Photo
{
    public class AlbumPhoto : WebObjectBase, ISelfManager
    {
        private static IAlbumPhotoProvider _provider;

        static AlbumPhoto()
        {
            _provider = new AlbumPhotoProvider();
        }

        public AlbumPhoto()
        {
            AlbumId = -1;
            SiteId = -1;
            Active = 1;
            DateCreated = DateTime.Now;
        }

        #region Properties

        public string Caption { get; set; }
        public string PhotoName { get; set; }
        public DateTime DateCreated { get; set; }
        public int SiteId { get; set; }
        public int AlbumId { get; set; }
        public int Active { get; set; }

        public bool IsActive
        {
            get { return Active == 1; }
            set { Active = true ? 1 : 0; }
        }

        public override int OBJECT_ID
        {
            get { return -1; }
        }

        public static IAlbumPhotoProvider Provider
        {
            get { return _provider; }
        }

        public Album Album
        {
            get
            {
                if (AlbumId > 0)
                    return Album.Provider.Get(AlbumId);

                return null;
            }
        }

        public string RelativePhotoPath
        {
            get
            {
                var album = Album;
                return album.RelativeAlbumFolder + PhotoName;
            }
        }

        public string AbsoluteImagePath
        {
            get { return WSession.MapPath(RelativePhotoPath); }
        }

        public string RelativePhotoThumbPath
        {
            get
            {
                var album = Album;
                return album.RelativeAlbumFolder + PhotoConstants.ResizedPrefix + PhotoName;
            }
        }

        public string AbsoluteImageThumbPath
        {
            get { return WSession.MapPath(RelativePhotoThumbPath); }
        }

        #endregion

        public bool RecreateThumbnail()
        {
            var album = Album;

            return ImageUtil.GenerateThumbnail(
                album.AbsoluteAlbumFolder + PhotoName,
                WSession.MapPath(album.RelativeAlbumFolder + PhotoConstants.ResizedPrefix + PhotoName),
                album.PhotoWidth, album.PhotoHeight
            );
        }

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            var photoPath = this.AbsoluteImagePath;
            var photoThumbPath = this.AbsoluteImageThumbPath;

            if (File.Exists(photoPath))
                File.Delete(photoPath);

            if (File.Exists(photoThumbPath))
                File.Delete(photoThumbPath);

            return _provider.Delete(this.Id);
        }

        public static bool Delete(int id)
        {
            AlbumPhoto item = AlbumPhoto.Provider.Get(id);
            if (item != null)
                return item.Delete();

            return false;
        }

        #endregion
    }
}
