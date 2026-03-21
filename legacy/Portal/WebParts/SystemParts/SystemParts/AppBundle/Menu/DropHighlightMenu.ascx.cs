using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

public partial class _Sections_STDMENU_DropHighlightMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebPartContext ctx = new WebPartContext(this);
        int siteId = ctx.Page.SiteId;

        MenuItem miHome = new MenuItem("Home");
        miHome.NavigateUrl = ctx.Page.Site.GenerateRelativeUrl();
        Menu1.Items.Add(miHome);

        var pages = WebPage.GetList(siteId);
        foreach (var page in pages)
        {
            if (page.IsActive)
            {
                MenuItem miItem = new MenuItem(page.Name);
                miItem.NavigateUrl = page.GenerateRelativeUrl();
                Menu1.Items.Add(miItem);
            }
        }
    }
}