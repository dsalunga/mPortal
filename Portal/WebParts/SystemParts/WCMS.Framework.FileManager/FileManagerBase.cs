using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public class FileManagerBase : UserControl
    {
        public string ToVirtualPath(string currentPath, string rootPath)
        {
            return currentPath.Replace(rootPath.TrimEnd(new char[] { '/', '\\' }), FileManagerConstants.DefaultRoot).Replace("//", "/");
        }

        public string ToVirtualPath(string currentPath)
        {
            var context = new WContext(this);
            var rootPath = context.Element.GetParameterValue("RootPath", FileManagerConstants.DefaultRoot);

            return currentPath.Replace(rootPath.TrimEnd(new char[] { '/', '\\' }), FileManagerConstants.DefaultRoot);
        }

        public string FromVirtualPath(string virtualPath, string rootPath)
        {
            if (virtualPath != rootPath)
                return virtualPath.Replace(FileManagerConstants.DefaultRoot, rootPath.TrimEnd(new char[] { '/', '\\' }));

            return rootPath;
        }

        public string FromVirtualPath(string virtualPath)
        {
            WContext context = new WContext(this);
            var rootPath = context.Element.GetParameterValue("RootPath", FileManagerConstants.DefaultRoot);

            if (virtualPath != rootPath)
                return virtualPath.Replace(FileManagerConstants.DefaultRoot, rootPath.TrimEnd(new char[] { '/', '\\' }));

            return rootPath;
        }

        protected void DisplayStorageInfo(WContext context, HtmlGenericControl panelStorageInfo, Label lblStorageInfo)
        {
            panelStorageInfo.Visible = true;

            var element = context.Element;
            var rootPath = element.GetParameterValue(FileManagerConstants.RootPathKey, FileManagerConstants.DefaultRoot);
            long quotaValue = -1;

            var paramStorageQuota = element.GetParameterValue(FileManagerConstants.StorageQuotaKey);
            if (!string.IsNullOrEmpty(paramStorageQuota))
                quotaValue = FileHelper.GetSizeFromString(paramStorageQuota);

            if (!string.IsNullOrEmpty(rootPath))
            {
                double quotaWarningPercentage;
                if (!double.TryParse(element.GetParameterValue(FileManagerConstants.QuotaWarningPercentageKey), out quotaWarningPercentage))
                    quotaWarningPercentage = 85;

                long totalSize = FileHelper.GetDirectorySize(WebHelper.MapPath(rootPath));
                var percentUsage = quotaValue > 0 ? (totalSize / (double)quotaValue) * 100 : 0;

                lblStorageInfo.Text = string.Format("{0} of {1}", FileHelper.GetSizeString(totalSize), quotaValue > 0 ? FileHelper.GetSizeString(quotaValue) : FileManagerConstants.UNLIMITED);
                lblStorageInfo.ForeColor = percentUsage > quotaWarningPercentage ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            }
        }

        protected void DeleteFolder(string sFolder, string currentPath)
        {
            string sVirtualFolder = WebHelper.CombineAddress(currentPath, sFolder);

            // START DELETE
            Directory.Delete(WebHelper.MapPath(sVirtualFolder), true);
        }

        protected string GetCurrentPath()
        {
            WContext context = new WContext(this);
            var rootPath = context.Element
                .GetParameterValue("RootPath", FileManagerConstants.DefaultRoot)
                .TrimEnd(new char[] { '/', '\\' });

            var currentPath = FromVirtualPath(context.Query.GetValue(FileManagerConstants.PathKey, rootPath), rootPath);
            if (!currentPath.ToLower().StartsWith(rootPath.ToLower()))
                currentPath = rootPath;

            return currentPath;
        }
    }
}
