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
    public partial class WebTemplateHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!Page.IsPostBack)
                {
                    var query = new QueryParser(this);
                    var id = query.GetId(WebColumns.TemplateId);

                    WebTemplate item = null;
                    if (id > 0 && (item = WebTemplate.Get(id)) != null)
                        lblHeader.InnerHtml = item.Name;

                    TabControl1.AddTab("tabMain", "General", query.BuildQuery(CentralPages.WebTemplateHome), CentralPages.WebTemplateHome);
                    TabControl1.AddTab("tabPanels", "Panels", query.BuildQuery(CentralPages.WebTemplatePanels), CentralPages.WebTemplatePanels);
                    TabControl1.AddTab("tabThemes", "Skins", query.BuildQuery(CentralPages.WebSkins), CentralPages.WebSkins);

                    TabControl1.SelectTab(query.BasePath);
                }
            }
        }
    }
}