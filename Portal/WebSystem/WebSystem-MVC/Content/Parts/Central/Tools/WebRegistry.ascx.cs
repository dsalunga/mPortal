using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class WebRegistryController : System.Web.UI.UserControl
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
                WebRegistry item = WebRegistry.Get(parentId);
                if (item != null)
                {
                    context.Set(WebColumns.ParentId, item.Id);
                    sb.Insert(0, string.Format(@"&nbsp;<span id=""cms_path_separator"">{2}</span>&nbsp;<a href='{0}' title='{1}'>{1}</a>", context.BuildQuery(), item.Key, WConstants.Arrow));

                    parentId = item.ParentId;
                }
                else
                {
                    parentId = -1;
                }
            }

            context.Remove(WebColumns.ParentId);
            sb.Insert(0, string.Format("<a href='{0}' title='{1}'>{1}</a>", context.BuildQuery(), "Web Registry"));

            lblBreadcrumb.InnerHtml = sb.ToString();
        }

        protected void cmdAddFull_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            query.Redirect(CentralPages.WebRegistryEntry);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int registryId = DataHelper.GetId(e.CommandArgument);
            QueryParser query = new QueryParser(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.RegistryId, registryId);
                    query.Redirect(CentralPages.WebRegistryEntry);
                    break;

                case "View_ChildNodes":
                    query.Set(WebColumns.ParentId, registryId);
                    query.Redirect();
                    break;

                case "Custom_Delete":
                    WebRegistry.Delete(registryId);
                    GridView1.DataBind();
                    break;
            }
        }

        public DataSet Select(int parentId) //, int startRowIndex, int maximumRows, string sortExpression)
        {
            QueryParser query = new QueryParser(true);

            var items = WebRegistry.GetByParentId(parentId);

            return DataHelper.ToDataSet(from i in items
                                        select new
                                        {
                                            i.Id,
                                            i.Key,
                                            Value = (i.Value.Length > WConstants.PreviewChars) ? i.Value.Substring(0, WConstants.PreviewChars) + "..." : i.Value,
                                            NameUrl = query.Set(WebColumns.RegistryId, i.Id).BuildQuery(CentralPages.WebRegistryEntry)
                                        });
        }

        protected void cmdUp_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int parentId = query.GetId(WebColumns.ParentId);

            if (parentId > 0)
            {
                query.Set(WebColumns.ParentId, WebRegistry.Get(parentId).ParentId.ToString());
                query.Redirect();
            }
        }
    }
}