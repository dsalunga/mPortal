using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.WebSystem.Controls;
using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebGroupTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new QueryParser(this);
                int id = query.GetId(WebColumns.GroupId);
                int parentId = query.GetId(WebColumns.ParentId);

                if (0 > id)
                {
                    id = parentId;
                    query.Set(WebColumns.GroupId, id);
                }

                WebGroup item = null;
                if (id > 0 && (item = WebGroup.Get(id)) != null)
                    lblHeader.InnerHtml = item.Name;

                string groupTabText = parentId > 0 ? "Children" : "Groups";
                if (parentId > 0)
                {
                    TabControl1.AddTab("tabGeneral", "General", query.BuildQuery(CentralPages.WebGroupHome), CentralPages.WebGroupHome);
                    TabControl1.AddTab("tabUsers", "Users", query.BuildQuery(CentralPages.WebGroupUsers), CentralPages.WebGroupUsers);
                }

                TabControl1.AddTab("tabGroups", groupTabText, query.BuildQuery(CentralPages.WebGroups), CentralPages.WebGroups);
                if (parentId > 0)
                    TabControl1.AddTab("tabSubscribe", "Subscriptions", query.BuildQuery(CentralPages.SubscriptionManager), CentralPages.SubscriptionManager);

                // ObjectKey
                var q = query.Clone();
                q.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebGroup, id));
                q.Set(ObjectKey.KeySource, WebHelper.UrlEncode(query.BuildQuery(CentralPages.WebGroupHome)));

                TabControl1.AddTab("tabParameters", "Parameters", q.BuildQuery(CentralPages.WebParameters), CentralPages.WebParameters);
                TabControl1.SelectTab(query.BasePath);
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
                WebGroup item = WebGroup.Get(parentId);
                if (item != null)
                {
                    context.Set(WebColumns.ParentId, item.Id);
                    context.Set(WebColumns.GroupId, item.Id);
                    sb.Insert(0, string.Format(@"&nbsp;<span id=""cms_path_separator"">{2}</span>&nbsp;<a href='{0}' title='{1}'>{1}</a>", context.BuildQuery(), item.Name, WConstants.Arrow));

                    parentId = item.ParentId;
                }
                else
                {
                    parentId = -1;
                }
            }

            context.Remove(WebColumns.ParentId);
            sb.Insert(0, string.Format("<a href='{0}' title='{1}'>{1}</a>", context.BuildQuery(), "Groups"));

            lblBreadcrumb.InnerHtml = sb.ToString();
        }
    }
}