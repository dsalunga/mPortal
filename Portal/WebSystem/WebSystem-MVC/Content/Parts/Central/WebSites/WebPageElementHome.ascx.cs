using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.WebSystem.ViewModel;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebPageElementHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser query = new QueryParser(this);
                int id = query.GetId(WebColumns.PageElementId);

                linkConfigPage.HRef = query.BuildQuery(CentralPages.WebPageElement);
                linkManageContent.HRef = query.BuildQuery(CentralPages.LoaderMain);

                WebPageElement item = null;
                if (id > 0 && (item = WebPageElement.Get(id)) != null)
                {
                    //lblTitle.InnerHtml = item.Name;

                    Action setPermissions = () =>
                    {
                        rowDelete.Visible = false;
                        rowProperties.Visible = false;
                        rowSecurity.Visible = false;
                        rowParameters.Visible = false;
                    };

                    // Set Security
                    PublicSecurableObject parent = null;
                    if (item.OwnerIsPage)
                    {
                        parent = item.Page;

                        //if (page.IsUserMgmtPermitted(Permissions.ManageInstance)) { }
                        //else if (page.IsUserMgmtPermitted(Permissions.ManageContent))
                        //{
                        //    rowDelete.Visible = false;
                        //    rowProperties.Visible = false;
                        //    rowSecurity.Visible = false;
                        //}
                        //else
                        //{
                        //    WebSystemHelper.ShowAccessDenied(page, qs);
                        //}
                    }
                    else
                    {
                        parent = item.MasterPage;

                        //masterPage.ExecuteUserMgmtActions(
                        //    () => { setPermissions(); },
                        //    () => { WebSystemHelper.ShowAccessDenied(masterPage.Site, qs); }
                        //);

                        //if (masterPage.IsUserMgmtPermitted(Permissions.ManageInstance)) { }
                        //else if (masterPage.IsUserMgmtPermitted(Permissions.ManageContent))
                        //{
                        //    rowDelete.Visible = false;
                        //    rowProperties.Visible = false;
                        //    rowSecurity.Visible = false;
                        //}
                        //else
                        //{
                        //    WebSystemHelper.ShowAccessDenied(masterPage.Site, qs);
                        //}
                    }

                    parent.ExecuteUserMgmtActions(
                        () => { setPermissions(); },
                        () => { WHelper.ShowAccessDenied(parent, query); }
                    );

                    lblName.InnerHtml = item.Name;
                    lblPublicAccess.InnerHtml = WebPublicAccess.Values[item.PublicAccess];
                    lblMgmtAccess.InnerHtml = WebMgmtAccess.Values[item.ManagementAccess];
                    lblActive.InnerHtml = item.IsActive ? "Yes" : "No";
                    lblRank.InnerHtml = item.Rank.ToString();
                    lblOwner.InnerHtml = item.ObjectId == WebObjects.WebMasterPage ? "Master Page" : "Web Page";

                    var partControlTemplate = item.PartControlTemplate;
                    if (partControlTemplate != null)
                        lblPart.InnerHtml = string.Format("{0} {1} {2}", partControlTemplate.Part.Name, WConstants.Arrow, partControlTemplate.Name);

                    var panel = item.Panel;
                    if (panel != null)
                    {
                        lblPanel.InnerHtml = panel.Name;

                        var q = query.Clone();
                        q.Remove(WebColumns.PageElementId);
                        q.Set(WebColumns.TemplatePanelId, panel.Id);

                        lblPanel.HRef = q.BuildQuery(CentralPages.WebPagePanelHome);
                    }
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);

            WebPageElement element = null;

            int id = query.GetId(WebColumns.PageElementId);
            if (id > 0 && (element = WebPageElement.Get(id)) != null)
            {
                element.Delete();

                query.Remove(WebColumns.PageElementId);
                query.Redirect(CentralPages.WebPageElements);
            }
        }
    }
}