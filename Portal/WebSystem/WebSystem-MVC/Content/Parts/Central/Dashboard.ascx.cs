using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebSystemHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lWebAppName.Text = WebRegistry.SelectNode(WebRegistry.WebNamePath).Value;

                defaultWebSite.InnerHtml = WConfig.DefaultSite.Name;
                defaultWebSite.HRef = WConfig.DefaultSite.BuildAbsoluteUrl();
            }
        }
    }
}