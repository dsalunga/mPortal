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
    public partial class RenameFile : FileManagerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetAccess();

                var currentPath = GetCurrentPath();
                var absPath = WebHelper.MapPath(currentPath);
                bool isFolder = FileHelper.IsFolder(absPath);

                if (isFolder)
                {
                    txtOldName.Text = Path.GetFileName(absPath);
                }
                else
                {
                    txtOldName.Text = Path.GetFileName(absPath);
                }

                txtNewName.Text = txtOldName.Text;
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

        private void Return(string newPath = null)
        {
            WContext context = new WContext(this);
            context.SetOpen("View-file");
            if (!string.IsNullOrWhiteSpace(newPath))
                context.Set(FileManagerConstants.PathKey, ToVirtualPath(newPath));

            context.Redirect();
        }

        protected void cmdRename_Click(object sender, EventArgs e)
        {
            bool noErrors = true;

            string newName = txtNewName.Text.Trim();
            string newPath = string.Empty;
            if (!string.IsNullOrEmpty(newName))
            {
                var currentPath = GetCurrentPath();
                var absPath = WebHelper.MapPath(currentPath);
                bool isFolder = FileHelper.IsFolder(absPath);

                // Rename file or folder
                try
                {
                    if (isFolder)
                    {
                        newPath = FileHelper.Combine(FileHelper.GetFolder(currentPath, '/'), newName, '/');
                        var absNewFolder = WebHelper.MapPath(newPath);
                        if (!Directory.Exists(absNewFolder))
                        {
                            Directory.Move(absPath, absNewFolder);
                        }
                        else
                        {
                            lblMessage.Text = "The specified new folder name already exists.";
                            noErrors = false;
                        }
                    }
                    else
                    {
                        var currentFolder = FileHelper.GetFolder(currentPath, '/');
                        newPath = FileHelper.Combine(currentFolder, newName, '/');
                        var absNewFilePath = WebHelper.MapPath(newPath);
                        if (!File.Exists(absNewFilePath))
                        {
                            File.Move(absPath, absNewFilePath);

                            // Update the FileIdentity if there is
                            WContext context = new WContext(this);
                            var fileIdentity = FileIdentity.GetByPath(currentPath, context.ObjectId, context.RecordId);
                            if (fileIdentity != null)
                            {
                                fileIdentity.Name = newName;
                                fileIdentity.Update();
                            }
                        }
                        else
                        {
                            lblMessage.Text = "The specified new file name already exists.";
                            noErrors = false;
                        }
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
                Return(newPath);
        }
    }
}