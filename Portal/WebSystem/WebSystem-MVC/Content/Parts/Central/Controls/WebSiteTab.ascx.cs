using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebSiteTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Visible) //TabControl1.Visible)
            {
                var query = new WQuery(this);
                if (!IsPostBack)
                {
                    int siteId = query.GetId(WebColumns.SiteId);
                    WSite site = null;
                    if (siteId > 0 && (site = WSite.Get(siteId)) != null)
                    {
                        var siteUrl = site.BuildRelativeUrl();
                        linkHeader.InnerHtml = site.Name;
                        linkHeader.HRef = siteUrl; // string.Format("<a href=\"{0}\">{1}</a>", siteUrl, siteUrl == "/" ? string.Format("/ ({0})", site.Name) : siteUrl);
                        //tableWebSite.Visible = true;
                    }
                    else
                    {
                        linkHeader.InnerHtml = WebRegistry.SelectNode("/System/Title").Value;
                    }

                    if (siteId > 0)
                        WebSiteViewModel.BuildTabs(siteId, query, TabControl1);
                }

                TabControl1.SelectTab(query.BasePath);
            }
        }
    }
}