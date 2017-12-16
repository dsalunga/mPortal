using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.RemoteIndexer
{
    public class RemoteIndexerViewBase : UserControl
    {
        public string ToVirtualPath(string currentPath, string rootPath)
        {
            return currentPath.Replace(rootPath, IndexerConstants.DefaultRoot);
        }

        public string FromVirtualPath(string virtualPath, string rootPath)
        {
            if (virtualPath != rootPath)
                return virtualPath.Replace(IndexerConstants.DefaultRoot, rootPath);

            return rootPath;
        }

        protected void DeleteFolder(string sFolder, string currentPath)
        {
            string sVirtualFolder = WebHelper.CombineAddress(currentPath, sFolder);

            // START DELETE
            Directory.Delete(MapPath(sVirtualFolder), true);
        }
    }
}
