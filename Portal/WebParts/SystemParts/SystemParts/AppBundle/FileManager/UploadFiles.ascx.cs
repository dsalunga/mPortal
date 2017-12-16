using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public partial class UploadFiles : FileManagerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetAccess();

                WContext context = new WContext(this);
                DisplayStorageInfo(context, panelStorageInfo, lblStorageInfo);

                panelPasswordAndArchive.Visible = DataHelper.GetBool(context.Element.GetParameterValue(FileManagerConstants.EnableUploadArchiveAndPasswordKey, "false"));
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
            var context = new WContext(this);
            var element = context.Element;

            var enableVersioning = DataHelper.GetBool(element.GetParameterValue(FileManagerConstants.EnableVersioningKey, "false"));
            var rootPath = element
                .GetParameterValue(FileManagerConstants.RootPathKey, FileManagerConstants.DefaultRoot)
                .TrimEnd(new char[] { '/', '\\' });

            #region Compute Storage Info

            long quotaValue = -1;
            long totalSize = 0;
            long totalAddedSize = 0;

            var paramStorageQuota = element.GetParameterValue(FileManagerConstants.StorageQuotaKey);
            if (!string.IsNullOrEmpty(paramStorageQuota))
                quotaValue = FileHelper.GetSizeFromString(paramStorageQuota);

            if (!string.IsNullOrEmpty(rootPath))
            {
                totalSize = FileHelper.GetDirectorySize(WebHelper.MapPath(rootPath));

                //lblStorageSize.InnerHtml = quotaValue > 0 ? FileHelper.GetSizeString(quotaValue) : FileManagerConstants.UNLIMITED;
                //lblStorageUsage.InnerHtml = FileHelper.GetSizeString(totalSize);
                //lblStorageFree.InnerHtml = quotaValue > 0 ? FileHelper.GetSizeString(quotaValue - totalSize) : FileManagerConstants.UNLIMITED;
            }

            #endregion

            bool noErrors = true;
            bool hasFile = false;

            var currentPath = FromVirtualPath(context.Query.GetValue(FileManagerConstants.PathKey, rootPath), rootPath);
            if (!currentPath.ToLower().StartsWith(rootPath.ToLower()))
                currentPath = rootPath;

            if (fileUploads.Controls.Count > 0)
            {
                string password = txtPassword.Text.Trim();
                bool setPassword = chkPassword.Checked && !string.IsNullOrEmpty(password);

                if (setPassword)
                {
                    // Check password complexity
                    var requirementXml = element.GetParameterValue(FileManagerConstants.PasswordComplexityRequirement);
                    if (!string.IsNullOrEmpty(requirementXml))
                    {
                        if (!PasswordComplexityRequirement.Parse(requirementXml).CheckComplexity(password))
                        {
                            lblMessage.Text = "Your password does not meet complexity requirements.";
                            return;
                        }
                    }
                }

                var archives = new List<string>();

                var combinedArchiveName = txtFilename.Text.Trim();
                var combineArchive = chkArchive.Checked && !string.IsNullOrEmpty(combinedArchiveName);
                if (!combinedArchiveName.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
                    combinedArchiveName = FileHelper.ChangeExtension(combinedArchiveName, ".zip");
                var combinedArchivePath = WebHelper.MapPath(Path.Combine(currentPath, combinedArchiveName));

                var combinedVersioningDone = false;

                foreach (var control in fileUploads.Controls)
                {
                    var fileUpload = control as FileUpload;
                    if (fileUpload != null)
                    {
                        if (fileUpload.HasFile)
                        {
                            if (!hasFile)
                                hasFile = true;

                            try
                            {
                                var newFile = Path.GetFileName(fileUpload.PostedFile.FileName);

                                if (string.IsNullOrEmpty(newFile))
                                    continue;

                                var newFilePath = Path.Combine(WebHelper.MapPath(currentPath), newFile);

                                // For extractor
                                if (chkDeployer.Checked && Compression.IsSupportedArchive(newFile))
                                    archives.Add(newFilePath);

                                if (setPassword || combineArchive)
                                {
                                    newFile = combineArchive ? combinedArchiveName : FileHelper.ChangeExtension(newFile, ".zip");
                                    newFilePath = combineArchive ? combinedArchivePath : Path.Combine(WebHelper.MapPath(currentPath), newFile);
                                }

                                if (0 >= quotaValue || ((quotaValue - (totalSize + totalAddedSize)) > fileUpload.PostedFile.ContentLength))
                                {
                                    var tempPath = WebHelper.MapPath(Path.Combine(WConfig.TempFolder, newFile));

                                    Action UploadTheFile = () =>
                                        {
                                            // Upload the file
                                            totalAddedSize += fileUpload.PostedFile.ContentLength;

                                            if (setPassword || combineArchive)
                                            {
                                                fileUpload.PostedFile.SaveAs(tempPath);

                                                Compression.Add(newFilePath, tempPath, true, password);

                                                File.Delete(tempPath);
                                            }
                                            else
                                            {
                                                fileUpload.PostedFile.SaveAs(newFilePath);
                                            }
                                        };

                                    // Check versioning
                                    if (enableVersioning && !combinedVersioningDone)
                                    {
                                        string newVirtualPath = FileHelper.Combine(currentPath, newFile);
                                        var file = FileIdentity.GetByPath(newVirtualPath, context.ObjectId, context.RecordId);

                                        // A file record already exists
                                        if (file != null && File.Exists(newFilePath))
                                        {
                                            // Check wether has a checked-out version by other users
                                            var version = file.Versions.FirstOrDefault(v => v.Activity == FileActivities.CheckOut);
                                            if (version != null)
                                            {
                                                // File has version
                                                if (version.UserId != WSession.Current.UserId)
                                                {
                                                    // File is checked-out by other users
                                                    lblMessage.Text = string.Format("The file \"{0}\" is currently checked-out by other user. Upload aborted.", newFile);
                                                    noErrors = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    // Seal the version
                                                    UploadTheFile();

                                                    version.Activity = FileActivities.CheckIn;
                                                    version.VersionDate = DateTime.Now;
                                                    version.Update();
                                                }
                                            }
                                            else
                                            {
                                                // File is not checked-out. Cancel the upload
                                                lblMessage.Text = string.Format("Check-out \"{0}\" first before you can upload a new version. Upload aborted.", newFile);
                                                noErrors = false;
                                                break;
                                            }
                                        }
                                        else if (File.Exists(newFilePath))
                                        {
                                            // File is not checked-out. Cancel the upload
                                            lblMessage.Text = string.Format("Check-out \"{0}\" first before you can upload a new version. Upload aborted.", newFile);
                                            noErrors = false;
                                            break;
                                        }
                                        else
                                        {
                                            // Create file and create an upload version
                                            UploadTheFile();

                                            // Create a file record and version
                                            if (file == null)
                                                file = new FileIdentity();

                                            file.ObjectId = context.ObjectId;
                                            file.RecordId = context.RecordId;
                                            file.EvalPath = newVirtualPath;
                                            file.Update();

                                            // Create a version
                                            file.AddVersion(FileActivities.Upload, WSession.Current.UserId);
                                        }

                                        if (combineArchive)
                                            combinedVersioningDone = true;
                                    }
                                    else
                                    {
                                        // Versioning is not enabled. Just upload the file
                                        UploadTheFile();
                                    }
                                }
                                else
                                {
                                    // Insufficient storage space

                                    lblMessage.Text = string.Format("There is no sufficient space remaining in your storage. You need {0} more to be able to upload the file \"{1}\".",
                                        FileHelper.GetSizeString((fileUpload.PostedFile.ContentLength - (quotaValue - (totalSize + totalAddedSize)))),
                                        fileUpload.PostedFile.FileName);

                                    noErrors = false;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                lblMessage.Text = ex.Message;

                                LogHelper.WriteLog(ex);

                                if (noErrors)
                                    noErrors = false;
                            }
                        }
                    }
                }

                if (chkDeployer.Checked && archives.Count > 0)
                    Compression.ExecuteExtractor(archives);

                if (noErrors && hasFile)
                    Return();
            }

            DisplayStorageInfo(context, panelStorageInfo, lblStorageInfo);
        }
    }
}