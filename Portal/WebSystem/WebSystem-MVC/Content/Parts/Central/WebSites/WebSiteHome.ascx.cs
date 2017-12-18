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
    public partial class WebSiteHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new WQuery(this);
                int id = query.GetId(WebColumns.SiteId);

                // Check management permissions
                WSite site = null;
                if (id > 0 && (site = WSite.Get(id)) != null)
                {
                    site.ExecuteUserMgmtActions(
                        () => { SetPermission(Permissions.ManageContent); },
                        () => { WHelper.ShowAccessDenied(site, query); }
                    );

                    //if (site.IsUserPermitted(Permissions.ManageInstance, 0)) { }
                    //else if (site.IsUserPermitted(Permissions.ManageContent, 0))
                    //    SetPermission(Permissions.ManageContent);
                    //else
                    //    WebSystemHelper.ShowAccessDenied(site, qs);

                    lblName.InnerHtml = site.Name;
                    lblPublicAccess.InnerHtml = WebPublicAccess.Values[site.PublicAccess];
                    lblMgmtAccess.InnerHtml = WebMgmtAccess.Values[site.ManagementAccess];
                    lblActive.InnerHtml = site.IsActive ? "Yes" : "No";
                    lblRank.InnerHtml = site.Rank.ToString();
                    lblIdentity.InnerHtml = site.Identity;

                    var masterPage = site.DefaultMasterPage;
                    if (masterPage != null)
                        lblMasterPage.InnerHtml = masterPage.Name;

                    var homePage = site.HomePage;
                    if (homePage != null)
                        lblHomePage.InnerHtml = homePage.Name; //string.Format("{0} (<a href=\"{1}\">{1}</a>)", homePage.Name, homePage.BuildRelativeUrl());

                    if (site.PrimaryIdentityId > 0)
                    {
                        var binding = site.GetPrimaryIdentity();
                        if (binding != null)
                        {
                            var bindingUrl = binding.Build();
                            fieldPrimaryBinding.Visible = true;
                            lblPrimaryBinding.InnerHtml = bindingUrl;
                            lblPrimaryBinding.HRef = bindingUrl;
                        }
                    }
                }

                linkBindings.HRef = query.BuildQuery(CentralPages.WebIdentities);

                // ObjectKey
                var q = new WQuery(query);
                q.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebSite, id));
                q.Set(ObjectKey.KeySource, query.EncodedBasePath);

                linkParameters.HRef = q.BuildQuery(CentralPages.WebParameters);
                linkResources.HRef = q.BuildQuery(CentralPages.WebResources);
                linkSecurity.HRef = q.BuildQuery(CentralPages.WebSecurity);

                query.Set(WebColumns._SiteId, id);
                linkProperties.HRef = query.BuildQuery(CentralPages.WebSite);
            }
        }

        private void SetPermission(int permissionId)
        {
            switch (permissionId)
            {
                case Permissions.ManageContent:
                    rowProperties.Visible = false;
                    rowDelete.Visible = false;
                    rowBindings.Visible = false;
                    //rowResources.Visible = false;
                    rowSecurity.Visible = false;
                    rowParameters.Visible = false;
                    break;
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);

            WSite site = null;
            int siteId = query.GetId(WebColumns.SiteId);
            if (siteId > 0 && (site = WSite.Get(siteId)) != null)
            {
                int parentId = site.ParentId;
                site.Delete();

                if (parentId > 0)
                {
                    query.Set(WebColumns.SiteId, parentId);
                    query.Redirect();
                }
                else
                {
                    query.Remove(WebColumns.SiteId);
                    query.Redirect(CentralPages.WebSites);
                }
            }
        }
    }
}