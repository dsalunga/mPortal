using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;

namespace WCMS.WebSystem.WebParts.RemoteIndexer
{
    public partial class RemoteIndexView : RemoteIndexerViewBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var sw = PerformanceLog.StartLog();

                var context = new WContext(this);
                var element = context.Element;

                int libraryId = libraryId = DataHelper.GetId(element.GetParameterValue(IndexerConstants.LibraryId));
                if (libraryId == -1)
                    libraryId = context.GetId(IndexerConstants.LibraryId);

                RemoteLibrary library = libraryId > 0 ? RemoteLibrary.Provider.Get(libraryId) : null;
                if (library != null)
                    Breadcrumb1.BuildPath(library);

                var id = context.GetId("Id");
                var item = RemoteItem.Provider.Get(id);
                if (item != null)
                {
                    //var rootPath = context.Element.GetParameterValue("RootPath", IndexerConstants.DefaultRoot);

                    //var currentPath = FromVirtualPath(context.Query.GetValue(IndexerConstants.PathKey, rootPath), rootPath);
                    //if (!currentPath.ToLower().StartsWith(rootPath.ToLower()))
                    //    currentPath = rootPath;

                    //var absPath = MapPath(currentPath);
                    //bool isFolder = FileHelper.IsFolder(absPath);

                    imgThumbnail.Src = item.IsDirectory ? "~/Content/Assets/Images/SkyDrive/NonEmptyDocumentFolder.png" : "~/Content/Assets/Images/SkyDrive/Default.png";

                    context.SetOpen("Download");

                    var userSession = WSession.Current.UserSession;
                    if (userSession != null)
                        context.Set("SessionId", userSession.SessionId.ToString());

                    //if (isFolder)
                    //{
                    //    var dir = new DirectoryInfo(absPath);

                    //    lblFileName.InnerHtml = dir.Name;
                    //    lblDateModified.InnerHtml = dir.LastWriteTime.ToString();
                    //    panelSize.Visible = false;
                    //}
                    //else
                    //{
                    //    var file = new FileInfo(absPath);

                    //    lblFileName.InnerHtml = file.Name;
                    //    lblSize.InnerHtml = FileHelper.GetSizeString(file.Length);
                    //    lblDateModified.InnerHtml = file.LastWriteTime.ToString();
                    //}

                    lblFileName.InnerHtml = item.Name;
                    lblSize.InnerHtml = item.SizeString;
                    lblDateModified.InnerHtml = item.DateModified.ToString();

                    var q = context.Query.Clone();
                    q.SetOpen("Download");
                    q.Set("Name", item.Name);
                    q.BaseAddress = context.Site.BuildAbsoluteUrl();

                    var permalink = q.BuildQuery();
                    linkPermalink.InnerHtml = permalink; //item.FullPath; // WebHelper.CombineAddress(WConfig.BaseAddress, item.RelativePath);
                    linkPermalink.HRef = permalink;

                    linkOpen.HRef = context.BuildQuery();
                    linkDownload.HRef = context.Set("Force", "true").BuildQuery();

                    PerformanceLog.EndLog(string.Format("RemoteIndexer-IndexView: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
                }
                else
                {
                    context.RemoveOpen();
                    context.Remove(WebColumns.Id);
                    context.Redirect();
                }
            }
        }
    }
}