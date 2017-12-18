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

namespace WCMS.WebSystem.WebParts.Central.Misc
{
    public partial class WebOfficesController : System.Web.UI.UserControl
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
            WContext context = new WContext(this);
            int parentId = context.GetId(WebColumns.ParentId);

            while (parentId > 0)
            {
                WebOffice item = WebOffice.Get(parentId);
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
            sb.Insert(0, string.Format("<a href='{0}' title='{1}'>{1}</a>", context.BuildQuery(), "Offices"));

            lblBreadcrumb.InnerHtml = sb.ToString();
        }

        protected void cmdAddFull_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            query.Redirect(CentralPages.WebOffice);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.OfficeId, id);
                    query.Redirect(CentralPages.WebOfficeHome);
                    break;

                case "Custom_Delete":
                    if (id > 0)
                    {
                        WebOffice.Delete(id);
                        GridView1.DataBind();
                    }
                    break;

                case "View_ChildNodes":
                    query.Set(WebColumns.ParentId, id);
                    query.Redirect();
                    break;
            }
        }

        protected void cmdUp_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int parentId = query.GetId(WebColumns.ParentId);

            if (parentId > 0)
            {
                query.Set(WebColumns.ParentId, WebOffice.Get(parentId).ParentId);
                query.Redirect();
            }
        }

        public DataSet Get(int parentId)
        {
            return DataHelper.ToDataSet(WebOffice.GetList(parentId));
        }

    }
}