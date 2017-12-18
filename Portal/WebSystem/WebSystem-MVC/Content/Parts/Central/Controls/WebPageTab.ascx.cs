using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebPageTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                var query = new WQuery(this);
                if (!IsPostBack)
                {
                    int pageId = query.GetId(WebColumns.PageId);
                    WPage page = null;
                    if (pageId > 0 && (page = WPage.Get(pageId)) != null)
                    {
                        var pageUrl = page.BuildRelativeUrl();
                        linkHeader.InnerHtml = page.Name;
                        linkHeader.HRef = pageUrl; // string.Format("<a href=\"{0}\">{1}</a>", pageUrl, pageUrl == "/" ? string.Format("/ ({0})", page.Name) : pageUrl);
                        //tableWebSite.Visible = true;
                    }
                    else
                    {
                        if ((page = WPage.Get(pageId)) != null)
                            linkHeader.InnerHtml = page.Name;
                    }

                    string tabSitesText = pageId > 0 ? "Subsites" : "Sites";
                    if (page != null)
                        WebPageViewModel.BuildTabs(page, query, TabControl1);
                }

                TabControl1.SelectTab(query.BasePath);
            }
        }
    }
}