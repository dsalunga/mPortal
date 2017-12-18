using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebPagesController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //sUserID = (Roles.IsUserInRole("Content Managers")) ? Membership.GetUser().ProviderUserKey.ToString() : null;
            int siteId = DataHelper.GetId(Request, WebColumns.SiteId);

            if (!IsPostBack)
            {
                //hiddenUserID.Value = sUserID;

                //if (Roles.IsUserInRole("Content Managers"))
                //{
                //    cmdAddFull.Enabled = false;
                //    cmdDelete.Enabled = false;
                //    cmdMove.Enabled = false;

                //    cmdEdit.Enabled = false;
                //}

                //bool serverMode = SystemSettings.GetSettings("System.TreeViewServerMode") == "true";

                //tvSections.EnableClientScript = !serverMode;
                //tvSections.EnableViewState = serverMode;
            }

            // Store the Page URL to hidden field
            int currentPageId = DataHelper.GetId(Request[WebColumns.PageId]); //tvSections.SelectedValue);
            if (currentPageId > 0)
            {
                WPage page = WPage.Get(currentPageId);
                if (page != null)
                    hidPageURL.Value = page.BuildRelativeUrl();
            }
            else
            {
                WSite site = WSite.Get(siteId);

                if (site.HomePage != null)
                    hidPageURL.Value = site.HomePage.BuildRelativeUrl();

                //else
                //{
                //    // No pages yet or no home page
                //}
            }
        }
    }
}