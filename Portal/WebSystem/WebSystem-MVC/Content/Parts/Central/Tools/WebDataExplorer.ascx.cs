using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class WebDataExplorer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BuildBreadcrumb();
            }
        }

        private void BuildBreadcrumb()
        {
            StringBuilder sb = new StringBuilder();
            WContext context = new WContext(this);
            int parentId = context.GetId(WebColumns.ParentId);

            while (parentId > 0)
            {
                WebFolder item = WebFolder.Provider.Get(parentId);
                if (item != null)
                {
                    context.Set(WebColumns.ParentId, item.Id);
                    sb.Insert(0, string.Format(@"&nbsp;<span id=""cms_path_separator"">{2}</span>&nbsp;<a href='{0}' title='{1}'>{1}</a>", context.BuildQuery(), item.Name, WConstants.Arrow));

                    parentId = item.ParentId;
                }
                else
                {
                    parentId = -1;
                }
            }

            context.Remove(WebColumns.ParentId);
            sb.Insert(0, string.Format("<a href='{0}' title='{1}'>{1}</a>", context.BuildQuery(), "Data Explorer"));

            lblBreadcrumb.InnerHtml = sb.ToString();
        }

        protected void cmdAddFull_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            query.Redirect(CentralPages.WebFolder);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            QueryParser query = new QueryParser(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.FolderId, id);
                    query.Redirect(CentralPages.WebFolder);
                    break;

                case "View_ChildNodes":
                    query.Set(WebColumns.ParentId, id);
                    query.Redirect();
                    break;

                case "Custom_Delete":
                    WebFolder.Provider.Delete(id);
                    GridView1.DataBind();
                    break;
            }
        }

        protected void cmdUp_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int parentId = query.GetId(WebColumns.ParentId);

            if (parentId > 0)
            {
                query.Set(WebColumns.ParentId, WebFolder.Provider.Get(parentId).ParentId);
                query.Redirect();
            }
        }

        public DataSet Select(int parentId) //, int startRowIndex, int maximumRows, string sortExpression)
        {
            var items = WebFolder.Provider.GetList(parentId);

            return DataHelper.ToDataSet(items);
        }

        public DataSet SelectFiles(int folderId) //, int startRowIndex, int maximumRows, string sortExpression)
        {
            var items = from i in WebFile.Provider.GetList(folderId)
                        select new
                        {
                            i.Id,
                            i.RecordId,
                            i.ObjectId,
                            i.Name,
                            ObjectName = WebObject.Get(i.ObjectId).FriendlyNameEval
                        };

            return DataHelper.ToDataSet(items);
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}