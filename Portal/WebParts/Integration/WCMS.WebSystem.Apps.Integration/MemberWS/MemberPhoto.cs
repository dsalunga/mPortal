using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration.ExternalMemberWS
{
    public partial class MemberPhoto
    {
        private string _photoPath;
        public string PhotoPath
        {
            get
            {
                if (string.IsNullOrEmpty(_photoPath))
                {
                    if (string.IsNullOrEmpty(PhotoFileName))
                    {
                        _photoPath = WConstants.NoPhotoThumb;
                    }
                    else
                    {
                        string baseUrl = WebRegistry.SelectNodeValue(MemberConstants.BaseUrlKey);
                        string photoBase = WebRegistry.SelectNodeValue(MemberConstants.PhotoPathKey);
                        _photoPath = baseUrl + Path.Combine(photoBase, PhotoFileName);
                    }
                }

                return _photoPath;
            }
        }

        //public static string GetNoPhotoPath()
        //{
        //    return WebRegistry.SelectNodeValue(MemberConstants.NoPhotoPathKey);
        //}
    }
}
