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
    public partial class WebPartControlTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                QueryParser query = new QueryParser(this);
                int id = query.GetId(WebColumns.PartControlId);

                WebPartControl item = null;
                if (id > 0 && (item = WebPartControl.Get(id)) != null)
                    lblHeader.InnerHtml = item.Name;

                if (!Page.IsPostBack)
                {
                    query.Remove(WebColumns.PartControlTemplateId);

                    TabControl1.AddTab("tabMain", "General", query.BuildQuery(CentralPages.WebPartControlHome), CentralPages.WebPartControlHome);
                    TabControl1.AddTab("tabTemplates", "Templates", query.BuildQuery(CentralPages.WebPartControlTemplates), CentralPages.WebPartControlTemplates);
                }

                TabControl1.SelectTab(query.BasePath);
            }
        }
    }
}