using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Diagnostics;
using WCMS.WebSystem.WebParts.RemoteIndexer;

namespace WCMS.WebSystem.Content.Parts.FileManager
{
    public partial class RemoteIndexRecentUpdates : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var watch = PerformanceLog.StartLog();

                var context = new WContext(this);
                var element = context.Element;

                var libraryId = DataHelper.GetInt32(element.GetParameterValue("LibraryId"));
                if (libraryId > 0)
                {
                    var maxItems = DataHelper.GetInt32(element.GetParameterValue("MaxItems"), 15);
                    var libraryPageUrl = element.GetParameterValue("LibraryPageUrl");

                    var items = GetIndexes(libraryId, context.Query, maxItems, libraryPageUrl);

                    gridIndexes.DataSource = items;
                    gridIndexes.DataBind();
                }

                PerformanceLog.EndLog(string.Format("RemoteIndexer Recent-Updates Load: {0}/{1}", context.ObjectId, context.RecordId), watch, context.PageId);
            }
        }

        public DataSet GetIndexes(int libraryId, WQuery query = null, int maxItems = 15, string libraryPageUrl = "", int parentId = -2, string keyword = "")
        {
            query.Set("Id", "{0}");
            query.Set("LibraryId", libraryId);

            if (!string.IsNullOrEmpty(libraryPageUrl))
                query.BasePath = libraryPageUrl;

            var itemQuery = query.Clone();
            itemQuery.SetOpen("Item");

            var library = RemoteLibrary.Provider.Get(libraryId);
            if (library != null)
            {
                string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
                bool isKWLEmpty = string.IsNullOrEmpty(kwl);

                var result = from i in RemoteItem.Provider.GetList(libraryId, parentId).OrderByDescending(i => i.IndexDateModified)
                             where (isKWLEmpty || i.FullPath.ToLower().Contains(kwl)
                                               || (kwl.Contains(" ") &&
                                                       (i.FullPath.ToLower().Contains(kwl.Replace(" ", "_"))
                                                     || i.FullPath.ToLower().Contains(kwl.Replace(" ", "-")))
                                                  )
                                    )
                             select i;

                return DataHelper.ToDataSet(from i in result.Take(maxItems)
                                            select new
                                            {
                                                i.Id,
                                                Name = i.Name.Replace('_', ' '),
                                                /*i.DateModified,*/
                                                DateModifiedString = i.IndexDateModified.ToString(@"dd-MMM ") + i.IndexDateModified.ToString(@"h\:mmt").ToLower(),
                                                TypeName = i.IsDirectory ? "Folder" : i.Extension,
                                                SizeString = i.IsDirectory ? "" : i.SizeString,
                                                Size = i.IsDirectory ? 0L : i.Size,
                                                FullPath = i.BuildDisplayPath(library),
                                                IsFolder = i.IsDirectory,
                                                FolderLinkFormat = query.BuildQuery(),
                                                ItemUrl = itemQuery.Set("Id", i.Id).BuildQuery()
                                            });
            }

            return DataHelper.GetEmptyDataSet();
        }
    }
}