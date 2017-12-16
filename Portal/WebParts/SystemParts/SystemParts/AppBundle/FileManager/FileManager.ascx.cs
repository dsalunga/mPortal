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
    public partial class FileManagerController : FileManagerBase
    {
        private WContext context;
        private string rootPath;
        protected string currentPath;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            var watch = PerformanceLog.StartLog();

            context = new WContext(this);

            var element = context.Element;

            rootPath = element
                .GetParameterValue(FileManagerConstants.RootPathKey, FileManagerConstants.DefaultRoot)
                .TrimEnd(new char[] { '/', '\\' });

            currentPath = FromVirtualPath(context.Query.GetValue(FileManagerConstants.PathKey, rootPath), rootPath);
            if (!currentPath.ToLower().StartsWith(rootPath.ToLower()))
                currentPath = rootPath;

            if (!Page.IsPostBack)
            {
                var pageSize = DataHelper.GetInt32(element.GetParameterValue("PageSize"));
                if (pageSize > 0)
                    gridViewFolders.PageSize = pageSize;

                this.LoadData();

                if (this.SetAccess())
                {
                    try
                    {
                        DisplayStorageInfo(context, panelStorageInfo, lblStorageInfo);
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        cmdAddfiles.Enabled = false;
                        cmdCreatefolder.Enabled = false;

                        lblMessage.Text = ex.Message;
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }

            PerformanceLog.EndLog(string.Format("FileManager Load: {0}/{1}", context.ObjectId, context.RecordId), watch, context.PageId);
        }

        private bool SetAccess()
        {
            if (!context.Element.IsUserMgmtPermitted(Permissions.ManageContent)) //.GetPublicAccountPermissionMax() != Permissions.PublicWrite)
            {
                // Read only mode
                rowFolderControls.Visible = false;

                //gridViewFolders.Columns[0].Visible = false;
                //gridViewFolders.Columns[1].Visible = true;
                //gridViewFolders.Columns[gridViewFolders.Columns.Count - 1].Visible = false;

                return false;
            }

            return true;
        }

        private void LoadData()
        {
            var selectedPathFolders = ObjectDataSourceFolders.SelectParameters["defaultPath"];
            var currentPathFolders = ObjectDataSourceFolders.SelectParameters["selectedPath"];

            currentPathFolders.DefaultValue = currentPath;
            selectedPathFolders.DefaultValue = rootPath;

            hEnableVersion.Value = (DataHelper.GetBool(context.Element.GetParameterValue(FileManagerConstants.EnableVersioningKey, "false")) ? 1 : 0).ToString();
            hObjectId.Value = context.ObjectId.ToString();
            hRecordId.Value = context.RecordId.ToString();

            gridViewFolders.DataBind();
        }


        private bool DeleteFile(string sFile)
        {
            try
            {
                FileManagerFile.Delete(sFile, currentPath);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                return false;
            }

            return true;
        }

        protected void cmdRefresh_Click(object sender, System.EventArgs e)
        {
            context.Redirect();
        }

        //protected void cmdCreateLink_Click(object sender, System.EventArgs e)
        //{
        //    string link = txtLink.Text.Trim();
        //    string fileName = txtFilename.Text.Trim();

        //    if (link != string.Empty)
        //    {
        //        string linkFile;

        //        if (fileName == string.Empty)
        //            linkFile = "Default.aspx";
        //        else
        //            linkFile = fileName;

        //        if (!File.Exists(Server.MapPath(currentPath + linkFile)))
        //        {
        //            string sLinkSrc;

        //            using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Parts/FileManager/Default.aspx_")))
        //            {
        //                sLinkSrc = reader.ReadToEnd().Replace("[URL]", link);
        //                reader.Close();
        //            }

        //            using (StreamWriter writer = File.CreateText(Server.MapPath(currentPath + linkFile)))
        //            {
        //                writer.WriteLine(sLinkSrc);
        //                writer.Close();
        //            }

        //            gridViewFiles.DataBind();
        //        }
        //        else
        //        {
        //            lblMessage.Text = "# The specified file already exists. #";
        //        }
        //    }
        //}

        private void DownloadFile(string fileName)
        {
            // START DOWNLOAD
            string virtualFileAndPath = Path.Combine(currentPath, fileName);

            try
            {
                WebHelper.DownloadFile(virtualFileAndPath);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void GridViewFolders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string name = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "View-File":
                    currentPath = WebHelper.CombineAddress(currentPath, HttpUtility.UrlEncode(name));

                    context.Set(FileManagerConstants.PathKey, ToVirtualPath(currentPath, rootPath));
                    //context.Set("Return", HttpUtility.UrlEncode(Page.Request.RawUrl));
                    context.SetOpen(FileManagerConstants.ViewFile);
                    context.Redirect();
                    break;

                //case "Custom_Edit":
                //    QueryParser q = new QueryParser(CentralPages.TextEditor);
                //    q.SetSource(name);
                //    q.Set("Return", HttpUtility.UrlEncode(Page.Request.RawUrl));
                //    q.Redirect();
                //    break;

                //case "Custom_Delete":
                //    try
                //    {
                //        DeleteFolder(name, currentPath);
                //        gridViewFolders.DataBind();

                //        DisplayStorageInfo(context, panelStorageInfo, lblStorageInfo);
                //    }
                //    catch (Exception ex)
                //    {
                //        lblMessage.Text = ex.Message;
                //    }

                //    break;

                //case "Custom_Delete_File":
                //    if (this.DeleteFile(name))
                //    {
                //        gridViewFolders.DataBind();
                //        DisplayStorageInfo(context, panelStorageInfo, lblStorageInfo);
                //    }
                //    break;

                case "View-SubFolders":
                    currentPath = WebHelper.CombineAddress(currentPath, HttpUtility.UrlEncode(name) + "/");

                    context.Set(FileManagerConstants.PathKey, ToVirtualPath(currentPath, rootPath));
                    context.Redirect();
                    break;

                //case "Custom_Download":
                //    WebHelper.DownloadFolder(WebHelper.CombineAddress(currentPath, name));
                //    break;

                //case "Custom_Download_File":
                //    this.DownloadFile(name);
                //    break;
            }
        }

        public DataSet GetFolders(string selectedPath, string defaultPath, int enableVersion, int objectId, int recordId)
        {
            string coUser = string.Empty;
            FileIdentity fileIdentity = null;
            FileVersion version = null;
            WebUser user = null;
            bool enableVersioning = enableVersion == 1 && objectId > 0 && recordId > 0;

            string root = defaultPath;
            string path = string.IsNullOrEmpty(selectedPath) ? root : selectedPath;
            if (!path.ToLower().StartsWith(root.ToLower()))
                path = root;

            try
            {
                var result = from f in FileManagerFolder.GetList(path)
                             select new
                             {
                                 f.Name,
                                 f.DateModified,
                                 TypeName = "Folder",
                                 SizeString = "",
                                 Size = 0L,
                                 f.FullPath,
                                 IsFolder = true,
                                 CoUser = string.Empty
                             };

                var fileIdentities = enableVersioning ? FileIdentity.Provider.GetList(path.Trim('/'), objectId, recordId) : null;
                var files = FileManagerFile.GetList(path);

                result = result.Union(
                    from f in files
                    select new
                    {
                        f.Name,
                        f.DateModified,
                        TypeName = f.Extension,
                        SizeString = f.SizeString,
                        f.Size,
                        f.FullPath,
                        IsFolder = false,
                        CoUser = enableVersioning && (fileIdentity = fileIdentities.FirstOrDefault(file => file.Name.Equals(f.Name, StringComparison.InvariantCultureIgnoreCase))) != null &&
                            (version = fileIdentity.Versions.FirstOrDefault(v => v.Activity == FileActivities.CheckOut)) != null && (user = version.User) != null ?
                                "Checked-Out by " + user.FirstAndLastName : string.Empty
                    });

                return DataHelper.ToDataSet(result);
            }
            catch (Exception)
            {
                return DataHelper.GetEmptyDataSet();
            }
        }

        protected void cmdAddfiles_Click(object sender, EventArgs e)
        {
            context.SetOpen("Add-files");
            context.Redirect();
        }

        protected void cmdCreatefolder_Click(object sender, EventArgs e)
        {
            context.SetOpen("Create-folder");
            context.Redirect();
        }
    }
}