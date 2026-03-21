using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class PanelTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);

            if (!Page.IsPostBack)
            {
                TabControl1.AddTab("tabPanel", "Panel Configuration", query.BuildQuery(CentralPages.WebPagePanel), CentralPages.WebPagePanel);
                TabControl1.AddTab("tabElements", "Page Elements", query.BuildQuery(CentralPages.WebPageElements), CentralPages.WebPageElements);
            }

            TabControl1.SelectTab(query.BasePath);
        }
    }
}