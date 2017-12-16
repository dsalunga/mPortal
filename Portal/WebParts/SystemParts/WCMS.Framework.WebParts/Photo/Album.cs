using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Photo.Providers;

namespace WCMS.WebSystem.WebParts.Photo
{
    public class Album : WebObjectBase, ISelfManager, ISharableContent
    {
        private static IAlbumProvider _provider;

        static Album()
        {
            _provider = new AlbumProvider();
        }

        public Album()
        {
            Width = PhotoConstants.DefaultAlbumWidth;
            PhotoHeight = PhotoConstants.DefaultPhotoHeight;
            PhotoWidth = PhotoConstants.DefaultPhotoWidth;
            Title = string.Empty;
        }

        #region Properties

        public string Title { get; set; }
        public string ImageFile { get; set; }
        public int Width { get; set; }
        public int PhotoHeight { get; set; }
        public string FolderName { get; set; }
        public int PhotoWidth { get; set; }
        public DateTime DateModified { get; set; }

        public static IAlbumProvider Provider
        {
            get { return _provider; }
        }

        public override int OBJECT_ID
        {
            get { return -1; }
        }

        public string RelativeAlbumFolder
        {
            get
            {
                string galleryPath = PhotoConstants.GalleryPath;
                if (!string.IsNullOrEmpty(FolderName))
                    galleryPath += FolderName + "/";

                return galleryPath;
            }
        }

        public string AbsoluteAlbumFolder
        {
            get
            {
                string galleryPath = PhotoConstants.GalleryPath;
                if (!string.IsNullOrEmpty(FolderName))
                    galleryPath += FolderName + "/";

                return WSession.MapPath(galleryPath);
            }
        }

        public string ThumbPath
        {
            get { return PhotoConstants.GalleryPath + "Resized." + ImageFile; }
        }

        public IEnumerable<AlbumPhoto> Photos
        {
            get
            {
                if (Id > 0)
                    return AlbumPhoto.Provider.GetList(Id);

                return new List<AlbumPhoto>();
            }
        }

        #endregion

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            var photos = this.Photos;
            if (photos.Count() > 0)
                foreach (var photo in photos)
                    photo.Delete();

            return _provider.Delete(this.Id);
        }

        #endregion

        public WebShare AddShare(int shareObjectId, int shareRecordId, AllowSharing allow)
        {
            WebShare item = new WebShare();
            item.ObjectId = OBJECT_ID;
            item.RecordId = this.Id;
            item.ShareObjectId = shareObjectId;
            item.ShareRecordId = shareRecordId;
            item.Allow = (int)allow;
            item.Update();

            return item;
        }

        public void RemoveShare(IWebObject item)
        {
            var share = WebShare.Provider.Get(this.OBJECT_ID, this.Id, item.OBJECT_ID, item.Id);
            if (share != null)
                share.Delete();
        }

        public IEnumerable<WebShare> GetShares()
        {
            return WebShare.Provider.GetList(this.OBJECT_ID, this.Id);
        }
    }
}
