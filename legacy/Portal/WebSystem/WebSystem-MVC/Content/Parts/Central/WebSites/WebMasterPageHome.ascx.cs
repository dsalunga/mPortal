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
    public partial class WebMasterPageHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new QueryParser(this);
                int id = query.GetId(WebColumns.MasterPageId);
                if (id > 0)
                {
                    var masterPage = WebMasterPage.Get(id);
                    if (masterPage != null)
                    {
                        lblName.InnerHtml = masterPage.Name;
                        lblPublicAccess.InnerHtml = WebPublicAccess.Values[masterPage.PublicAccess];
                        lblMgmtAccess.InnerHtml = WebMgmtAccess.Values[masterPage.ManagementAccess];

                        var template = masterPage.Template;
                        if (template != null)
                            lblTemplateName.InnerHtml = template.Name;
                        
                        //if (page.IsHighestPermission(Permissions.ManageContent, Permissions.ManageInstance))
                        //    SetPermission(Permissions.ManageContent);

                        //if (masterPage.IsUserMgmtPermitted(Permissions.ManageInstance)) { }
                        //else if (masterPage.IsUserMgmtPermitted(Permissions.ManageContent))
                        //    SetPermission(Permissions.ManageContent);
                        //else
                        //    WebSystemHelper.ShowAccessDenied(masterPage.Site, qs);

                        masterPage.ExecuteUserMgmtActions(
                            () => { SetPermission(Permissions.ManageContent); },
                            () => { WHelper.ShowAccessDenied(masterPage.Site, query); }
                        );
                    }

                    linkProperties.HRef = query.BuildQuery(CentralPages.WebMasterPage);
                    linkPanels.HRef = query.BuildQuery(CentralPages.WebPagePanels);

                    // ObjectKey
                    var q = new QueryParser(query);
                    q.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebMasterPage, id));
                    q.Set(ObjectKey.KeySource, query.EncodedBasePath);

                    linkParameters.HRef = q.BuildQuery(CentralPages.WebParameters);
                    linkResource.HRef = q.BuildQuery(CentralPages.WebResources);
                    linkSecurity.HRef = q.BuildQuery(CentralPages.WebSecurity);

                    // Page Elements
                    query.Set(WebColumns.MasterPageId, masterPage.Id);
                    query.Set(WebColumns.TemplatePanelId, masterPage.Template.PrimaryPanelId);
                    query.BasePath = CentralPages.WebPageElements;

                    linkElements.HRef = query.BuildQuery();
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

            WebMasterPage master = null;

            int id = query.GetId(WebColumns.MasterPageId);
            if (id > 0 && (master = WebMasterPage.Get(id)) != null)
            {
                master.Delete();

                query.Remove(WebColumns.MasterPageId);
                query.Redirect(CentralPages.WebMasterPages);
            }
        }
    }
}