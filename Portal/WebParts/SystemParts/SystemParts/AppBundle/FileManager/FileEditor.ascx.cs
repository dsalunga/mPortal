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
    public partial class FileEditor : FileManagerBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var path = GetCurrentPath();
                if (!string.IsNullOrEmpty(path))
                    txtContent.Text = FileHelper.ReadFile(WebHelper.MapPath(path));
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.ReturnParentPage();
        }

        private void ReturnParentPage()
        {
            WContext query = new WContext(this);
            query.SetOpen("View-file");
            query.Redirect();
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            var path = GetCurrentPath();

            if (!string.IsNullOrEmpty(path))
            {
                FileHelper.WriteFile(txtContent.Text, WebHelper.MapPath(path));
            }

            this.ReturnParentPage();
        }
    }
}