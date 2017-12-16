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

namespace WCMS.WebSystem.WebParts.Article
{
    public partial class AdminSubscriptionManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BuildBreadcrumb();
            }
        }

        private void BuildBreadcrumb()
        {
            StringBuilder sb = new StringBuilder();
            WContext ctx = new WContext(this);
            int parentId = ctx.GetId(WebColumns.ParentId);

            while (parentId > 0)
            {
                WebGroup item = WebGroup.Get(parentId);
                if (item != null)
                {
                    ctx.Set(WebColumns.ParentId, item.Id);
                    sb.Insert(0, string.Format(@"&nbsp;<span id=""cms_path_separator"">{2}</span>&nbsp;<a href='{0}' title='{1}'>{1}</a>", ctx.BuildQuery(), item.Name, WConstants.Arrow));

                    parentId = item.ParentId;
                }
                else
                {
                    parentId = -1;
                }
            }

            ctx.Remove(WebColumns.ParentId);
            sb.Insert(0, string.Format("<a href='{0}' title='{1}'>{1}</a>", ctx.BuildQuery(), "Groups"));

            lblBreadcrumb.InnerHtml = sb.ToString();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new WQuery(this);
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.ParentId, id);
                    query.LoadAndRedirect("AdminSubscriptionManagerItems.ascx");
                    break;

                case "View_ChildNodes":
                    query.Set(WebColumns.ParentId, id);
                    query.Redirect();
                    break;
            }
        }

        protected void cmdUp_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            int parentId = query.GetId(WebColumns.ParentId);
            if (parentId > 0)
            {
                query.Set(WebColumns.ParentId, WebGroup.Get(parentId).ParentId);
                query.Redirect();
            }
        }

        public DataSet Get(int parentId)
        {
            return DataHelper.ToDataSet(WebGroup.Provider.GetList(parentId));
        }
    }
}