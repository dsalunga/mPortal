using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public partial class CreateFolder : FileManagerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetAccess();
            }
        }

        private void SetAccess()
        {
            WContext context = new WContext(this);
            if (!context.Element.IsUserMgmtPermitted(Permissions.ManageContent))  //if (!WSession.Current.IsAdministrator && ctx.PageElement.GetPublicAccountPermissionMax() != Permissions.PublicWrite)
            {
                // Read only mode
                Return();
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Return();
        }

        private void Return()
        {
            var context = new WQuery(this);
            context.Remove(WConstants.Open);
            context.Redirect();
        }

        protected void cmdUploadNow_Click(object sender, EventArgs e)
        {
            var noErrors = true;
            var context = new WQuery(this);
            var currentPath = GetCurrentPath();

            string newFolder = txtNewFolder.Text.Trim();
            if (!string.IsNullOrEmpty(newFolder))
            {
                try
                {
                    string virtualFolder = Path.Combine(currentPath, newFolder);
                    if (!Directory.Exists(WebHelper.MapPath(virtualFolder)))
                    {
                        // CREATE FOLDER

                        Directory.CreateDirectory(WebHelper.MapPath(virtualFolder));

                    }
                    else
                    {
                        lblMessage.Text = "The specified folder already exists.";
                        noErrors = false;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    noErrors = false;
                }
            }
            else
            {
                noErrors = false;
            }

            if (noErrors)
                Return();
        }
    }
}