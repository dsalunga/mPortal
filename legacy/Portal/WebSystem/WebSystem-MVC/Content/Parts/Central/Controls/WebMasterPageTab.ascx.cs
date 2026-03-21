using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebMasterPageTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                var query = new QueryParser(this);
                int siteId = query.GetId(WebColumns.SiteId);

                WSite site = null;
                if (siteId > 0 && (site = WSite.Get(siteId)) != null)
                {
                    linkHeader.InnerHtml = site.Name;
                    linkHeader.HRef = site.BuildRelativeUrl(); // string.Format("<a href=\"{0}\">{0}</a>", site.BuildRelativeUrl());
                    //tableWebSite.Visible = true;
                }
                else
                {
                    if ((site = WSite.Get(siteId)) != null)
                    {
                        linkHeader.InnerHtml = site.Name;
                        linkHeader.HRef = query.BuildQuery();
                    }
                }

                if (siteId > 0 && !Page.IsPostBack)
                {
                    TabControl1.AddTab("tabMain", "General", query.BuildQuery(CentralPages.WebMasterPageHome), CentralPages.WebMasterPageHome);
                    TabControl1.AddTab("tabPreview", "Preview", false);
                }

                TabControl1.SelectTab(query.BasePath);
            }
        }
    }
}