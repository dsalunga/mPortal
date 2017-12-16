using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public partial class ConfigFileManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DisplayStorageInfo();
            }
        }

        private void DisplayStorageInfo()
        {
            WContext context = new WContext(this.Request);
            var element = context.Element;

            var rootPath = element.GetParameterValue(FileManagerConstants.RootPathKey, FileManagerConstants.DefaultRoot);
            long quotaValue = -1;

            var paramStorageQuota = element.GetParameterValue(FileManagerConstants.StorageQuotaKey);
            if (!string.IsNullOrEmpty(paramStorageQuota))
                quotaValue = FileHelper.GetSizeFromString(paramStorageQuota);

            if (!string.IsNullOrEmpty(rootPath))
            {
                long totalSize = FileHelper.GetDirectorySize(WebHelper.MapPath(rootPath));

                lblStorageSize.InnerHtml = quotaValue > 0 ? FileHelper.GetSizeString(quotaValue) : FileManagerConstants.UNLIMITED;
                lblStorageUsage.InnerHtml = FileHelper.GetSizeString(totalSize);
                lblStorageFree.InnerHtml = quotaValue > 0 ? FileHelper.GetSizeString(quotaValue - totalSize) : FileManagerConstants.UNLIMITED;
            }
        }
    }
}