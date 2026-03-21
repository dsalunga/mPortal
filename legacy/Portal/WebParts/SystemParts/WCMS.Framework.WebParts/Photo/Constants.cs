using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.Photo
{
    public struct PhotoConstants
    {
        public const string GalleryPath = "/Content/Assets/Uploads/Image/Apps/Photo/";
        public const string TempPath = "/Content/Assets/Uploads/Image/Apps/Photo/temp/";

        public const string ResizedPrefix = "Resized.";

        public const int DefaultPhotoHeight = 75;
        public const int DefaultPhotoWidth = 112;
        public const int DefaultAlbumWidth = 250;
        public const int MaxPhotoWidth = 700;
    }
}
