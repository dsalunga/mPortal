using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class SiteMap : System.Web.UI.UserControl
    {
        // List non-orphan pages
        private List<int> nonOrphanList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BuildTreeView();
            }
        }

        private void BuildTreeView()
        {
            WContext context = new WContext(this);
            var siteId = context.GetId(WebColumns.SiteId);

            // # All Web Sites #
            TreeNode rootMenu = new TreeNode("All Web Sites");
            rootMenu.NavigateUrl = WConfig.DefaultSite.BuildRelativeUrl();
            rootMenu.ImageUrl = "~/Content/Assets/Images/Common/wf1.gif";
            t.Nodes.Add(rootMenu); 
            LoadRecursiveTree(-1, WSite.GetList(), rootMenu, siteId);
        }

        private bool LoadRecursiveTree(int parentId, IEnumerable<WSite> allSites, TreeNode nodeRoot, int siteId)
        {
            var sites = WSite.FilterPermittedWithChildren(allSites, parentId); //.FilterPermitted(allSites, parentId);

            foreach (WSite site in sites)
            {
                TreeNode menuSite = new TreeNode(site.Name);
                menuSite.ImageUrl = "~/Content/Assets/Images/TreeView/ws.gif";
                menuSite.NavigateUrl = site.BuildRelativeUrl();

                if (WSession.Current.IsAdministrator || site.Identity != WConstants.CentralID)
                {
                    // Process WebPages
                    if (site.Id == siteId)
                    {
                        menuSite.Text = string.Format("<strong>{0}</strong>", menuSite.Text);

                        nonOrphanList = new List<int>();

                        var pages = WPage.FilterPermitted(site.Id);

                        LoadRecursivePageTree(-1, pages, menuSite);

                        // Search for orphans
                        if (pages.Count() > nonOrphanList.Count)
                        {
                            List<WPage> orphanPages = new List<WPage>();
                            foreach (var page in pages)
                            {
                                if (!nonOrphanList.Contains(page.Id))
                                    orphanPages.Add(page);
                            }

                            if (orphanPages.Count > 0)
                            {
                                // There are orphans
                                List<int> orphanRoots = new List<int>();
                                foreach (WPage page in orphanPages)
                                {
                                    // Process root orphans
                                    if (orphanPages.Find(i => i.Id == page.ParentId) == null && !orphanRoots.Contains(page.ParentId))
                                    {
                                        orphanRoots.Add(page.ParentId);
                                        LoadRecursivePageTree(page.ParentId, orphanPages, menuSite);
                                    }
                                }
                            }
                        }

                        menuSite.CollapseAll();
                        menuSite.Expand();
                    }
                    else
                    {
                        // Do not process WebPages
                        WContext context = new WContext(this);
                        context.Set(WebColumns.SiteId, site.Id);

                        TreeNode viewPages = new TreeNode("View Site Map");
                        viewPages.ImageUrl = "~/Content/Assets/Images/Common/ico_pages.gif";
                        viewPages.NavigateUrl = context.BuildQuery();
                        menuSite.ChildNodes.Add(viewPages);

                        menuSite.Collapse();
                    }

                    // SubSites
                    LoadRecursiveTree(site.Id, allSites, menuSite, siteId);
                }

                nodeRoot.ChildNodes.Add(menuSite);
            }

            return (sites.Count > 0);
        }

        private void LoadRecursivePageTree(int parentId, IEnumerable<WPage> table, TreeNode tnRoot)
        {
            var pages = from p in table
                        where p.ParentId == parentId
                        select p;

            foreach (WPage page in pages)
            {
                TreeNode tn1 = new TreeNode(page.Name, page.Id.ToString());
                //tn1.ImageUrl = "~/Content/Assets/Images/TreeView/t.gif";
                tn1.NavigateUrl = page.BuildRelativeUrl();
                tnRoot.ChildNodes.Add(tn1);

                // Add to non-orphan list
                nonOrphanList.Add(page.Id);

                LoadRecursivePageTree(page.Id, table, tn1);
            }
        }
    }
}