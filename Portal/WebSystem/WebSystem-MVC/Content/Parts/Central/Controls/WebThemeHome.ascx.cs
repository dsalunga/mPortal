using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.WebSystem.Controls;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebThemeHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!Page.IsPostBack)
                {
                    var query = new WQuery(this);
                    int id = query.GetId(WebColumns.ThemeId);

                    WebTheme item = null;
                    if (id > 0 && (item = WebTheme.Provider.Get(id)) != null)
                        lblHeader.InnerHtml = item.Name;

                    TabControl1.AddTab("tabMain", "General", query.BuildQuery(CentralPages.WebThemeHome), CentralPages.WebThemeHome);
                    TabControl1.AddTab("tabTemplates", "Templates", query.BuildQuery(CentralPages.WebTemplates), CentralPages.WebTemplates);
                    TabControl1.AddTab("tabThemes", "Skins", query.BuildQuery(CentralPages.WebSkins), CentralPages.WebSkins);

                    TabControl1.SelectTab(query.BasePath);
                }
            }
        }
    }
}