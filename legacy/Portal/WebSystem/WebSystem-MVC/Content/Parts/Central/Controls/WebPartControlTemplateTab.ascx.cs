using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebPartControlTemplateTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                var query = new QueryParser(this);
                var id = query.GetId(WebColumns.PartControlTemplateId);

                WebPartControlTemplate item = null;
                if (id > 0 && (item = WebPartControlTemplate.Get(id)) != null)
                    lblHeader.InnerHtml = item.Name;

                if (!Page.IsPostBack)
                {
                    TabControl1.AddTab("tabMain", "Template", query.BuildQuery(CentralPages.WebPartControlTemplateHome), CentralPages.WebPartControlTemplateHome);
                    TabControl1.AddTab("tabPanels", "Panels", query.BuildQuery(CentralPages.WebPartTemplatePanels), CentralPages.WebPartTemplatePanels);
                }

                TabControl1.SelectTab(query.BasePath);
            }
        }
    }
}