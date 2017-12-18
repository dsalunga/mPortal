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
    public partial class HeaderPanel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                panelSecurityManager.Visible = true;
                panelSiteManager.Visible = true;

                var isAdmin = WSession.Current.IsAdministrator;
                if (isAdmin)
                {
                    panelPartManager.Visible = true;
                    panelWebTools.Visible = true;

                    panelWebRegistry.Visible = true;

                    panelWebFolder.Visible = true;
                    panelWebOffice.Visible = true;
                }

                if (isAdmin)
                    panelWebGroup.Visible = true;
            }
        }
    }
}