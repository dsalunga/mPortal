using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.WebParts.RemoteIndexer;

namespace WCMS.WebSystem.WebParts.RemoteIndexer.Controls
{
    public partial class IndexerBreadcrumb : RemoteIndexerViewBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BuildPath(RemoteLibrary library)
        {
            if (library != null)
            {
                StringBuilder navText = new StringBuilder();

                var query = new WContext(this.Parent);
                var pathSeparator = query.Element.GetParameterValue("PathSeparator", WConstants.Arrow);
                var rootText = query.Element.GetParameterValue("RootText", "&lt;root&gt;");
                var parentId = query.GetId(WebColumns.Id);

                //query.Remove(WConstants.OpenKey);

                Action<int> BuildPathRecursive = null;
                BuildPathRecursive = (currentParentId) =>
                {
                    if (currentParentId > 0)
                    {
                        RemoteItem item = RemoteItem.Provider.Get(currentParentId);

                        if (item != null)
                        {
                            query.Set(WebColumns.Id, currentParentId);

                            if (!item.IsDirectory)
                                query.Set(WConstants.Open, "Item");

                            navText.Insert(0, string.Format(@"&nbsp;<span class=""separator"">{3}</span> <a href='{0}' title='{1}'>{2}</a>",
                                    query.BuildQuery(), item.Name, item.Name, pathSeparator));

                            if (!item.IsDirectory)
                                query.Remove(WConstants.Open);

                            BuildPathRecursive(item.ParentId);
                        }
                    }
                    else
                    {
                        query.Remove(WConstants.Open);
                        query.Remove(WebColumns.Id);
                        navText.Insert(0, string.Format("<a href='{0}' title='{1}'><strong>{2}</strong></a>", query.BuildQuery(), library.Name, library.Name));
                    }
                };

                BuildPathRecursive(parentId);

                lRoot.Text = navText.ToString();
            }
        }
    }
}