using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebSitesControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PopulateCombo();

                // Root site
                if (!IfRootSiteAuthorOrAllowed())
                {
                    cmdAdd.Enabled = false;
                    cmdDelete.Enabled = false;
                    cmdMove.Enabled = false;
                }
            }
        }

        private bool IsRootSiteAuthor()
        {
            if (WSession.Current.IsAdministrator)
                return true;

            return WebGlobalPolicy.IsUserPermitted(GlobalPolicies.WebSiteManagement, Permissions.AuthorWebSite);
        }

        private bool IfRootSiteAuthorOrAllowed()
        {
            bool allowed = true;
            int id = DataHelper.GetId(Request, WebColumns.SiteId);
            if (id > 0)
            {
                var site = WSite.Get(id);
                if (site != null)
                    return site.IsUserPermitted(Permissions.ManageInstance, 0);
                else
                    return false;
            }
            else
            {
                if (!IsRootSiteAuthor())
                    allowed = false;
            }

            return allowed;
        }

        private bool IsSubSiteAuthor(WSite parent)
        {
            //if (IsRootSiteAuthor())
            //{
            //    //parent.GetObjectSecurities
            //}

            return false;
        }

        private void PopulateCombo()
        {
            string portalName = WebRegistry.SelectNode(WebRegistry.WebNamePath).Value;

            cboSites.Items.Clear();

            // COMBO BOX
            ListItem itemRoot = new ListItem(portalName, "-1");
            cboSites.Items.Add(itemRoot);
            {
                // START RECURSIVE
                LoadRecursiveTree(-1, WSite.GetList(), "");
            }
        }

        private void LoadRecursiveTree(int iParentID, IEnumerable<WSite> allSites, string sTab)
        {
            sTab += WConstants.TAB;

            var sites = allSites.Where(site => site.ParentId == iParentID);
            foreach (WSite site in sites)
            {
                // COMBO BOX
                ListItem item1 = new ListItem(sTab + WConstants.TAB + site.Name, site.Id.ToString());
                cboSites.Items.Add(item1);

                LoadRecursiveTree(site.Id, allSites, sTab);
            }
        }

        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            if (IfRootSiteAuthorOrAllowed())
            {
                var query = new WQuery(this);
                query.Set("ParentSiteId", query.Get(WebColumns.SiteId));

                query.Redirect(CentralPages.WebSite);
            }
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                if (IfRootSiteAuthorOrAllowed())
                {
                    var siteIds = DataHelper.ParseCommaSeparatedIdList(sChecked);
                    foreach (var id in siteIds)
                    {
                        var site = WSite.Get(id);
                        if (site != null)
                            site.Delete();
                    }

                    QueryParser query = new QueryParser(this);
                    query.Redirect();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int siteId = DataHelper.GetId(e.CommandArgument);
            var query = new WQuery(this);
            query.Set("ParentSiteId", query.Get(WebColumns.SiteId));
            query.Set(WebColumns.SiteId, siteId);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Redirect(CentralPages.WebSiteHome);
                    break;

                case "Custom_Delete":
                    if (IfRootSiteAuthorOrAllowed())
                    {
                        var site = WSite.Get(siteId);
                        if (site != null)
                        {
                            site.Delete();
                            QueryParser.StaticRedirect();
                        }
                    }
                    break;

                case "Load_Bindings":
                    query.Redirect(CentralPages.WebIdentities);
                    break;
            }
        }

        protected void cmdMove_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                if (IfRootSiteAuthorOrAllowed())
                {
                    var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);
                    int parentId = int.Parse(cboSites.SelectedValue);

                    foreach (int id in ids)
                    {
                        WSite site = WSite.Get(id);
                        site.ParentId = parentId;
                        site.Update();
                    }

                    QueryParser query = new QueryParser(this);
                    query.Redirect();
                }
            }
        }

        protected void cmdGO_Click(object sender, EventArgs e) { }

        public static DataSet GetWebSites(int siteId)
        {
            var sites = WSite.GetList(siteId);

            if (!WSession.Current.IsAdministrator) //WebGlobalPolicy.IsUserPermitted(GlobalPolicies.WebSiteManagement, Permissions.FullControl))
            {
                if (siteId > 0) { } // why???
                else // if (!WebGlobalPolicy.IsUserPermitted(GlobalPolicies.WebSiteManagement, Permissions.AuthorWebSite))
                    sites = WSite.FilterPermittedWithChildren(sites, -1, true);
            }

            var items = from site in sites
                        select new
                        {
                            site.Id,
                            site.Name,
                            RelativeUrl = site.BuildRelativeUrl(),
                            site.Rank,
                            site.Active
                        };


            return DataHelper.ToDataSet(items);
        }
    }
}