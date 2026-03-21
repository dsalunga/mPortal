using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebSiteTree : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && WSession.Current.UserId > 0)
            {
                this.BuildTreeView();
            }
        }

        private void BuildTreeView()
        {
            // Start the stopwatch
            //Stopwatch localwatch = new Stopwatch();
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //localwatch.Start();

            bool showOrphan = WebRegistry.SelectNode("/System/ShowOrphan").Value == "true";
            bool serverMode = WebRegistry.SelectNode("/System/TreeViewServerMode").Value == "true";
            bool showSiteSecurity = WebRegistry.SelectNode("/System/ShowSiteSecurity").Value == "true";

            t.EnableClientScript = !serverMode;
            t.EnableViewState = serverMode;
            t.Nodes.Clear();

            //linkAboutMe.HRef += UserSession.UserId;

            //TreeNode tnRoot = new TreeNode(WConfig.SystemName);
            //tnRoot.NavigateUrl = CentralPages.WebSystemHome;

            // Templates
            //BuildTemplatesNode(tnRoot);
            //lblStatus.InnerHtml += "<br/>Web Templates: " + localwatch.Elapsed;
            //localwatch.Reset();
            //localwatch.Start();

            if (!WSession.Current.IsAdministrator)
            {
                var sitePolicy = WebGlobalPolicy.Provider.Get(GlobalPolicies.WebSiteManagement);
                var perm = sitePolicy.TryGetUserPermission(Permissions.ManageContent);

                if (perm != null && perm.IsAllowed)
                {
                    // WebSites
                    BuildWebSitesNode(t);
                }
            }
            else
            {
                BuildWebSitesNode(t);
            }

            //t.Nodes.Clear();
            //t.Nodes.Add(tnRoot);
            t.CollapseAll();

            if (t.Nodes.Count > 0)
                t.Nodes[0].Expanded = true;
        }

        private static void BuildWebSitesNode(TreeView treeView)
        {
            TreeNode webSitesNode = new TreeNode("Web Sites");
            webSitesNode.ImageUrl = "~/Content/Assets/Images/TreeView/wf1.gif";

            Func<int, IEnumerable<WSite>, TreeNode, bool> LoadRecursiveWebSites = null;
            LoadRecursiveWebSites = (int iParentID, IEnumerable<WSite> allSites, TreeNode rootNode) =>
            {
                var sites = allSites.Where(site => site.ParentId == iParentID);

                // Get the list of WebSites based on permission
                if (!WSession.Current.IsAdministrator)
                {
                    // Check WebPart permissions
                    sites = (from site in sites
                             where WebObjectSecurity.IsUserAdded(site) //.Provider.Get(WebObjects.WebSite, site.Id, WebObjects.WebUser, WSession.Current.UserId) != null
                             select site);
                }

                foreach (WSite site in sites)
                {
                    TreeNode siteNode = new TreeNode(site.Name);
                    siteNode.ImageUrl = "~/Content/Assets/Images/TreeView/ws.gif";
                    siteNode.NavigateUrl = string.Format("{0}?{1}={2}", CentralPages.WebSiteHome, WebColumns.SiteId, site.Id);

                    if (site.Identity != WConstants.CentralID)
                    {
                        // WebPages node
                        TreeNode pagesNode = new TreeNode("Web Pages");
                        pagesNode.ImageUrl = "~/Content/Assets/Images/TreeView/mp2.gif";
                        pagesNode.NavigateUrl = string.Format("{0}?{1}={2}", CentralPages.WebPages, WebColumns.SiteId, site.Id);

                        // Load Web Pages
                        {
                            Action<int, IEnumerable<WPage>, TreeNode, string> LoadRecursivePageTree = null;
                            LoadRecursivePageTree = (int parentId, IEnumerable<WPage> table, TreeNode pageRoot, string sTab) =>
                            {
                                sTab += WConstants.TAB;
                                var pages = from p in table
                                            where p.ParentId == parentId
                                            select p;

                                foreach (WPage page in pages)
                                {
                                    TreeNode pageNode = new TreeNode(page.Name, page.Id.ToString());
                                    pageNode.SelectAction = TreeNodeSelectAction.Select;
                                    pageNode.NavigateUrl = string.Format("{0}?{1}={2}&{3}={4}", CentralPages.WebPageHome, WebColumns.SiteId, page.SiteId, WebColumns.PageId, page.Id);
                                    pageRoot.ChildNodes.Add(pageNode);

                                    LoadRecursivePageTree(page.Id, table, pageNode, sTab);
                                }
                            };

                            // START RECURSIVE
                            var webPages = WPage.GetList(site.Id);
                            if (!WSession.Current.IsAdministrator)
                            {
                                // Check WebPages permissions
                                var securityPages = (from page in webPages
                                                     where WebObjectSecurity.IsUserAdded(page) //.Provider.Get(WebObjects.WebPage, page.Id, WebObjects.WebUser, WSession.Current.UserId) != null
                                                     select page);
                                if (securityPages.Count() > 0)
                                    webPages = securityPages;
                            }

                            LoadRecursivePageTree(-1, webPages, pagesNode, "");
                        }
                        siteNode.ChildNodes.Add(pagesNode);


                        TreeNode masterPagesNode = new TreeNode("Master Pages");
                        masterPagesNode.ImageUrl = "~/Content/Assets/Images/TreeView/mp1.gif";
                        masterPagesNode.NavigateUrl = string.Format("{0}?{1}={2}", CentralPages.WebMasterPages, WebColumns.SiteId, site.Id); //else tn4.SelectAction = TreeNodeSelectAction.Expand;

                        // LOAD ALL INSIDE PAGES
                        if (WSession.Current.IsAdministrator || WebUser.Get(WSession.Current.UserId).IsMemberOf(SystemGroups.SiteManagers))
                        {
                            var masterPages = WebMasterPage.GetList(site.Id);
                            if (masterPages != null)
                            {
                                var query = new QueryParser(HttpContext.Current);
                                query[WebColumns.SiteId] = site.Id.ToString();

                                foreach (WebMasterPage masterPage in masterPages)
                                {
                                    TreeNode tnSitePage = new TreeNode(masterPage.Name);
                                    tnSitePage.ImageUrl = "~/Content/Assets/Images/TreeView/pr.gif";

                                    query[WebColumns.MasterPageId] = masterPage.Id.ToString();
                                    tnSitePage.NavigateUrl = query.BuildQuery(CentralPages.WebMasterPageHome); //"WebMasterPageDesigner.aspx?SiteId=" + masterPage.SiteId + "&MasterPageId=" + masterPage.MasterPageId;

                                    //if (showOrphan)
                                    //{
                                    //    tn5 = new TreeNode("* Misplaced Items");
                                    //    tn5.NavigateUrl = "OrphanPageItems.aspx?SiteId=" + masterPage.SiteId + "&MasterPageId=" + masterPage.MasterPageId;
                                    //    tnSitePage.ChildNodes.Add(tn5);
                                    //}
                                    masterPagesNode.ChildNodes.Add(tnSitePage);
                                }
                            }
                        }

                        siteNode.ChildNodes.Add(masterPagesNode);

                        // Child websites
                        LoadRecursiveWebSites(site.Id, allSites, siteNode);
                    }

                    // Add site to parent node
                    rootNode.ChildNodes.Add(siteNode);
                }

                return (sites.Count() > 0);
            };

            // START RECURSIVE
            LoadRecursiveWebSites(-1, WSite.GetList(), webSitesNode);

            if (WSession.Current.IsAdministrator)
            {
                webSitesNode.NavigateUrl = CentralPages.WebSites;
                treeView.Nodes.Add(webSitesNode);
            }
            else
            {
                webSitesNode.SelectAction = TreeNodeSelectAction.Expand;
                if (webSitesNode.ChildNodes.Count > 0)
                {
                    treeView.Nodes.Add(webSitesNode);
                }
            }
        }
    }
}