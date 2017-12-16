using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public partial class FileView : FileManagerBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var sw = PerformanceLog.StartLog();

                var context = new WContext(this);
                var element = context.Element;
                bool checkedOutByOtherUser = false;

                var currentPath = GetCurrentPath();
                var absPath = WebHelper.MapPath(currentPath);
                bool isFolder = FileHelper.IsFolder(absPath);
                bool isReadOnly = !element.IsUserMgmtPermitted(Permissions.ManageContent);

                #region Determine which Icon to display

                var extension = Path.GetExtension(absPath);
                var extIcon = string.Empty;
                if (isFolder)
                {
                    extIcon = "NonEmptyDocumentFolder";
                }
                else
                {
                    switch (extension.ToLower())
                    {
                        case ".doc":
                        case ".docx":
                        case ".rtf":
                            extIcon = "Doc";
                            break;

                        case ".txt":
                        case ".xml":
                        case ".cs":
                        case ".aspx":
                        case ".config":
                            extIcon = "Txt";
                            break;

                        case ".exe":
                            extIcon = "Exe";
                            break;

                        case ".xls":
                        case ".xlsx":
                            extIcon = "Xls";
                            break;

                        case ".ppt":
                        case ".pptx":
                            extIcon = "Ppt";
                            break;

                        case ".one":
                            extIcon = "One";
                            break;

                        case ".wmv":
                        case ".avi":
                        case ".divx":
                        case ".3gp":
                        case ".mp4":
                        case ".mov":
                            extIcon = "Video";
                            break;

                        case ".mp3":
                        case ".wav":
                        case ".midi":
                        case ".mp4a":
                            extIcon = "Audio";
                            break;

                        case ".zip":
                        case ".rar":
                        case ".cab":
                        case ".7z":
                        case ".tar":
                            extIcon = "Zip";
                            break;

                        default:
                            extIcon = "Default";
                            break;
                    }
                }

                imgThumbnail.Src = string.Format("~/Content/Assets/Images/SkyDrive/{0}.png", extIcon);

                #endregion

                if (isFolder)
                {
                    var dir = new DirectoryInfo(absPath);
                    lblFileName.InnerHtml = dir.Name;
                    lblDateModified.InnerHtml = dir.LastWriteTime.ToString();
                    panelSize.Visible = false;
                }
                else
                {
                    var file = new FileInfo(absPath);

                    lblFileName.InnerHtml = file.Name;
                    lblSize.InnerHtml = FileHelper.GetSizeString(file.Length);
                    lblDateModified.InnerHtml = file.LastWriteTime.ToString();

                    if (!isReadOnly)
                    {
                        var editorFiles = new string[] { ".txt", ".xml", ".cs", ".aspx", ".ascx", ".css", ".htm", ".html", ".xsd", ".config", ".js", ".cshtml" };
                        var archiveFiles = new string[] { ".zip", ".7z" };

                        // Enable Text Editor
                        if (editorFiles.Contains(extension))
                        {
                            context.SetOpen("Edit-file");
                            linkEdit.HRef = context.BuildQuery();
                            panelEdit.Visible = true;
                        }

                        // Archive file
                        if (archiveFiles.Contains(extension))
                            panelExtractHere.Visible = true;

                        // Check Versioning
                        var enableVersioning = DataHelper.GetBool(element.GetParameterValue(FileManagerConstants.EnableVersioningKey, "false"));
                        if (enableVersioning)
                        {
                            panelVersioning.Visible = true;
                            panelVersions.Visible = true;

                            bool checkedOut = false;
                            var fileIdentity = FileIdentity.GetByPath(currentPath, context.ObjectId, context.RecordId);
                            if (fileIdentity != null)
                            {
                                var checkedOutVer = fileIdentity.Versions.FirstOrDefault(i => i.Activity == FileActivities.CheckOut); ;
                                if (checkedOutVer != null)
                                {
                                    checkedOut = true;
                                    checkedOutByOtherUser = checkedOutVer.UserId != WSession.Current.UserId;
                                }

                                hFileId.Value = fileIdentity.Id.ToString();
                                gridVersions.DataBind();
                            }

                            if (checkedOut)
                                cmdCancelCheckOut.Visible = true;
                            else
                                cmdCheckOut.Visible = true;
                        }
                    }
                }

                if (!isReadOnly)
                {
                    // Rename link
                    context.SetOpen("Rename");
                    linkRename.HRef = context.BuildQuery();
                    panelRename.Visible = true;

                    // Delete link
                    panelDelete.Visible = !checkedOutByOtherUser;
                }

                linkPermalink.InnerHtml = WebHelper.CombineAddress(WConfig.BaseAddress, currentPath);
                linkPermalink.HRef = linkPermalink.InnerHtml;

                PerformanceLog.EndLog(string.Format("FileManager-View: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
            }
        }

        private void Return()
        {
            var query = new WQuery(this);
            query.Remove(WConstants.Open);

            var path = query.Get(FileManagerConstants.PathKey);
            query.Set(FileManagerConstants.PathKey, FileHelper.GetFolder(path, '/'));
            query.Redirect();
        }

        public DataSet SelectVersions(int fileId)
        {
            WebUser user = null;
            var versions = FileVersion.Provider.GetList(fileId);

            return DataHelper.ToDataSet(
                from version in versions
                select new
                {
                    version.Id,
                    version.VersionDate,
                    version.UserId,
                    version.Activity,
                    User = (user = WebUser.Get(version.UserId)) == null ? string.Empty : user.FirstAndLastName,
                    ActivityString = FileActivities.KeyValues[version.Activity]
                }
            );
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            var currentPath = GetCurrentPath();
            var absPath = WebHelper.MapPath(currentPath);
            var isFolder = FileHelper.IsFolder(absPath);

            if (isFolder)
                WebHelper.DownloadFolder(currentPath, Path.GetFileName(currentPath));
            else
                WebHelper.DownloadFile(currentPath);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var currentPath = GetCurrentPath();
            var absPath = WebHelper.MapPath(currentPath);
            var isFolder = FileHelper.IsFolder(absPath);

            if (isFolder)
            {
                Directory.Delete(absPath, true);
            }
            else
            {
                FileManagerFile.Delete(currentPath);

                var context = new WContext(this);
                var enableVersioning = DataHelper.GetBool(context.Element.GetParameterValue(FileManagerConstants.EnableVersioningKey, "false"));
                if (enableVersioning)
                {
                    FileIdentity file = FileIdentity.GetByPath(currentPath, context.ObjectId, context.RecordId);
                    if (file != null)
                    {
                        var coVersion = file.Versions.FirstOrDefault(v => v.Activity == FileActivities.CheckOut);
                        if (coVersion != null) // && (coVersion.UserId == WSession.Current.UserId || WSession.Current.IsAdministrator))
                        {
                            coVersion.Activity = FileActivities.Delete;
                            coVersion.VersionDate = DateTime.Now;
                            coVersion.UserId = WSession.Current.UserId;
                            coVersion.Update();
                        }
                        else
                        {
                            file.AddVersion(FileActivities.Delete, WSession.Current.UserId);
                        }
                    }
                    else
                    {
                        file = new FileIdentity();
                        file.ObjectId = context.ObjectId;
                        file.RecordId = context.RecordId;
                        file.EvalPath = currentPath;
                        file.Update();

                        file.AddVersion(FileActivities.Delete, WSession.Current.UserId);
                    }
                }
            }

            Return();
        }

        protected void cmdCheckOut_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var currentPath = GetCurrentPath();

            var file = FileIdentity.GetByPath(currentPath, context.ObjectId, context.RecordId);
            if (file == null)
            {
                file = new FileIdentity();
                file.ObjectId = context.ObjectId;
                file.RecordId = context.RecordId;
                file.EvalPath = currentPath;
                file.Update();
            }

            file.AddVersion(FileActivities.CheckOut, WSession.Current.UserId);

            context.Redirect();
        }

        protected void cmdCancelCheckOut_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var currentPath = GetCurrentPath();

            var file = FileIdentity.GetByPath(currentPath, context.ObjectId, context.RecordId);
            if (file != null)
            {
                var coVersion = file.Versions.FirstOrDefault(v => v.Activity == FileActivities.CheckOut);
                if (coVersion != null && (coVersion.UserId == WSession.Current.UserId || WSession.Current.IsAdministrator))
                {
                    coVersion.Delete();
                    context.Redirect();
                }
                else
                {
                    lblStatus.InnerHtml = "File is checked-out by other user. You cannot check-out this file.";
                }
            }
            else
            {
                lblStatus.InnerHtml = "Invalid file version.";
            }
        }

        protected void cmdExtractHere_Click(object sender, EventArgs e)
        {
            var currentPath = GetCurrentPath();
            var absPath = WebHelper.MapPath(currentPath);
            Compression.Extract(absPath, FileHelper.GetFolder(absPath, '\\'), true, true);
        }
    }
}