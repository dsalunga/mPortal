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

namespace WCMS.WebSystem.WebParts.RemoteIndexer
{
    public partial class RemoteLibraryView : RemoteIndexerViewBase
    {
        private WContext context;

        protected string currentPath;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            context = new WContext(this);

            if (!Page.IsPostBack)
            {
                PerformanceLog.Log(() =>
                {
                    var element = context.Element;

                    int libraryId = libraryId = DataHelper.GetId(element.GetParameterValue(IndexerConstants.LibraryId));
                    if (libraryId == -1)
                    {
                        libraryId = context.GetId(IndexerConstants.LibraryId);

                        //cboRemoteLibraries.Items.Clear();
                        cboRemoteLibraries.DataBind();

                        panelLibraries.Visible = true;

                        if (libraryId > 0)
                            cboRemoteLibraries.SelectedValue = libraryId.ToString();
                    }

                    RemoteLibrary library = libraryId > 0 ? RemoteLibrary.Provider.Get(libraryId) : null;
                    if (library != null)
                    {
                        var path = context.Get("Path");
                        if (!string.IsNullOrEmpty(path))
                        {
                            // Refresh page with Id parameter
                            var item = RemoteItem.Find(library, path);
                            if (item != null)
                            {
                                var query = context.Query;
                                query.Remove("Path");
                                query.Set("Id", item.Id);
                                if (!item.IsDirectory)
                                    query.SetOpen("Item");

                                query.Redirect();

                                return 0;
                            }
                        }

                        hLibraryId.Value = libraryId.ToString();

                        var pageSize = DataHelper.GetInt32(element.GetParameterValue("PageSize"));
                        if (pageSize > 0)
                            gridViewFolders.PageSize = pageSize;

                        var downloadUrl = element.GetParameterValue("DownloadUrl", "");
                        if (!string.IsNullOrEmpty(downloadUrl))
                            hDownloadUrl.Value = downloadUrl;

                        var timeOut = DataHelper.GetInt32(element.GetParameterValue("DownloadTimeOut", "0"));
                        if (timeOut > 0)
                            hDownloadTimeOut.Value = timeOut.ToString();

                        gridViewFolders.DataBind();
                        Breadcrumb1.BuildPath(library);

                        lblLastIndexDate.InnerHtml = library.LastIndexDate.ToString();

                        // Display of UserName and Password
                        var displayUserNamePwd = element.GetParameterValue(IndexerConstants.DisplayUserNamePassword);
                        if (!string.IsNullOrWhiteSpace(displayUserNamePwd))
                        {
                            panelUserNamePwd.Visible = true;
                            lblUserNamePwd.InnerHtml = displayUserNamePwd;
                        }
                    }

                    return 0;
                }, string.Format("RemoteEndexer-LibraryView: {0}/{1}", context.ObjectId, context.RecordId), context.PageId);
            }
        }

        protected void GridViewFolders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            int libraryId = DataHelper.GetId(cboRemoteLibraries.SelectedValue);

            switch (e.CommandName)
            {
                //    case "View-File":
                //        query.Set(PathKey, ToVirtualPath(currentPath, rootPath));
                //        query.SetOpen("View-file");
                //        query.Redirect();
                //        break;

                case "View-Folders":
                    if (libraryId > 0)
                        context.Set("LibraryId", libraryId);

                    context.Set(WebColumns.Id, id);
                    context.Redirect();
                    break;

                case "Download-File":
                    RemoteLibraryHelper.InvokeDownload(id, true, DataHelper.GetInt32(hDownloadTimeOut.Value, 0));
                    break;
            }
        }

        public IEnumerable<RemoteLibrary> GetActiveLibraries()
        {
            return RemoteLibrary.Provider.GetList().Where(i => i.IsActive);
        }

        public DataSet GetIndexes(int libraryId, int parentId, string keyword)
        {
            var query = new WQuery(true);
            query.Set("Id", "{0}");

            var itemQuery = query.Clone();
            itemQuery.SetOpen("Item");

            var library = RemoteLibrary.Provider.Get(libraryId);
            if (library != null)
            {
                string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
                bool isKWLEmpty = string.IsNullOrEmpty(kwl);

                var result = from i in RemoteItem.Provider.GetList(libraryId, isKWLEmpty ? parentId : parentId > 0 ? parentId : -2)
                             where (isKWLEmpty || i.FullPath.ToLower().Contains(kwl)
                                               || (kwl.Contains(" ") &&
                                                       (i.FullPath.ToLower().Contains(kwl.Replace(" ", "_"))
                                                     || i.FullPath.ToLower().Contains(kwl.Replace(" ", "-")))
                                                  )
                                    )
                             select new
                             {
                                 i.Id,
                                 i.Name,
                                 i.DateModified,
                                 TypeName = i.IsDirectory ? "Folder" : i.Extension,
                                 i.SizeString,
                                 i.Size,
                                 FullPath = i.BuildDisplayPath(library),
                                 IsFolder = i.IsDirectory,
                                 FolderLinkFormat = query.BuildQuery(),
                                 ItemUrl = itemQuery.Set("Id", i.Id).BuildQuery()
                             };

                return DataHelper.ToDataSet(result);
            }

            return DataHelper.GetEmptyDataSet();
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {

        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
        }

        protected void cboRemoteLibraries_SelectedIndexChanged(object sender, EventArgs e)
        {
            int libraryId = context.GetId("LibraryId");
            int newLibraryId = DataHelper.GetId(cboRemoteLibraries.SelectedValue);
            if (libraryId > 0 && libraryId != newLibraryId)
            {
                context.Remove(WebColumns.Id);
            }

            context.Set("LibraryId", newLibraryId);
            context.Redirect();
        }
    }
}