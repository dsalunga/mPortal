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
    public partial class WebPageHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //WContext context = new WContext(this);
            QueryParser query = new QueryParser(this);
            int id = query.GetId(WebColumns.PageId);
            int siteId = query.GetId(WebColumns.SiteId);

            WPage page = null;

            // Store the Page URL to hidden field
            if (id > 0 && (page = WPage.Get(id)) != null)
            {
                hidPageURL.Value = page.BuildRelativeUrl();
            }
            else
            {
                WSite site = WSite.Get(siteId);
                if (site.HomePage != null)
                    hidPageURL.Value = site.HomePage.BuildRelativeUrl();
                // else // No pages yet or no home page
            }

            if (!Page.IsPostBack)
            {
                if (page != null)
                {
                    lblName.InnerHtml = page.Name;
                    lblPublicAccess.InnerHtml = WebPublicAccess.Values[page.PublicAccess];
                    lblMgmtAccess.InnerHtml = WebMgmtAccess.Values[page.ManagementAccess];
                    lblActive.InnerHtml = page.IsActive ? "Yes" : "No";
                    lblRank.InnerHtml = page.Rank.ToString();
                    lblUseBuiltinTemplate.InnerHtml = page.UsePartTemplatePath == 1 ? "Yes" : "No";
                    lblPageType.InnerHtml = page.PageType == PageTypes.Dynamic ? "Dynamic" : "Static";

                    var partControlTemplate = page.PartControlTemplate;
                    if (partControlTemplate != null)
                        lblPart.InnerHtml = string.Format("{0} {1} {2}", partControlTemplate.Part.Name, WConstants.Arrow, partControlTemplate.Name);

                    var masterPage = page.MasterPage;
                    if (masterPage != null)
                        lblMasterPage.InnerHtml = masterPage.Name;

                    page.ExecuteUserMgmtActions(
                        () => { SetPermission(Permissions.ManageContent); },
                        () => { WHelper.ShowAccessDenied(page, query); }
                    );

                    //if (page.IsUserMgmtPermitted(Permissions.ManageInstance)) { }
                    //else if (page.IsUserMgmtPermitted(Permissions.ManageContent))
                    //    SetPermission(Permissions.ManageContent);
                    //else
                    //    WebSystemHelper.ShowAccessDenied(page, qs);

                    linkPageElements.HRef = query.BuildQuery(CentralPages.WebPageElements);
                    linkManageContent.HRef = query.BuildQuery(CentralPages.LoaderMain);
                    linkPanels.HRef = query.BuildQuery(CentralPages.WebPagePanels);
                    linkLinkedParts.HRef = query.BuildQuery(CentralPages.WebLinkedParts);


                    // ObjectKey
                    QueryParser q = new QueryParser(query);
                    q.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebPage, id));
                    q.Set(ObjectKey.KeySource, q.EncodedBasePath);

                    linkParameters.HRef = q.BuildQuery(CentralPages.WebParameters);
                    linkResources.HRef = q.BuildQuery(CentralPages.WebResources);
                    linkSecurity.HRef = q.BuildQuery(CentralPages.WebSecurity);

                    // Menu Manager
                    q = new QueryParser(query);
                    q.Set("PartConfigId", "84");
                    linkMenuManager.HRef = q.BuildQuery(CentralPages.LoaderMain);

                    // Comment Manager
                    //var paramObj = context.ParameterizedObject;
                    //var commentAdmin = paramObj.GetParameterValue("CommentAdminId");
                    //if (!string.IsNullOrEmpty(commentAdmin))
                    //{
                    q = new QueryParser(query);
                    q.Set(WConstants.Load, "../Common/AdminCommentManager");

                    linkCommentManager.HRef = q.BuildQuery(CentralPages.LoaderMain);

                    panelCommentManager.Visible = true;
                    //}

                    query.Set(WebColumns._PageId, query.Get(WebColumns.PageId));
                    linkConfigPage.HRef = query.BuildQuery(CentralPages.WebPage);

                    taskList.Visible = true;
                }
            }
        }

        private void SetPermission(int permissionId)
        {
            switch (permissionId)
            {
                case Permissions.ManageContent:
                    rowProperties.Visible = false;
                    rowDelete.Visible = false;
                    //rowResources.Visible = false;
                    rowSecurity.Visible = false;
                    rowParameters.Visible = false;
                    break;
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int id = query.GetId(WebColumns.PageId);

            WPage page = null;
            if (id > 0 && (page = WPage.Get(id)) != null)
            {
                page.Delete(true);

                query.Remove(WebColumns.PageId);
                query.Redirect(CentralPages.WebPages);
            }
        }
    }
}