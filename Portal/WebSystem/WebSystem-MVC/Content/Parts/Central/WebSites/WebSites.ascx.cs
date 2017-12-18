using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebSitesController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                if (siteId > 0) { }
                else
                    WebSiteTab1.Visible = false;
            }
        }
    }
}