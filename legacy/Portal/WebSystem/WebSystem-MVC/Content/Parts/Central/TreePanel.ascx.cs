using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class TreePanel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack && WSession.Current.UserId > 0)
                this.BuildTreeView();
        }

        private void BuildTreeView()
        {
            //bool showOrphan = WebRegistry.SelectNode("/System/ShowOrphan").ValueBool;
            //bool showSiteSecurity = WebRegistry.SelectNode("/System/ShowSiteSecurity").ValueBool;

            bool serverMode = WebRegistry.SelectNode("/System/TreeViewServerMode").ValueBool;

            t.EnableClientScript = !serverMode;
            t.EnableViewState = serverMode;
            t.Nodes.Clear();

            TreeNode tnRoot = new TreeNode(WConfig.SystemName);
            tnRoot.NavigateUrl = CentralPages.WebSystemDashboard;

            if (!WSession.Current.IsAdministrator)
            {
                // WebSites
                if (WebGlobalPolicy.IsUserAdded(GlobalPolicies.WebSiteManagement))
                    BuildWebSitesNode(tnRoot);
            }
            else
            {
                BuildWebSitesNode(tnRoot);
            }

            // WebParts
            BuildWebPartsNode(tnRoot);

            // Tools
            if (WSession.Current.IsAdministrator)
                BuildToolsNode(tnRoot);

            BuildSecurityNode(tnRoot);

            t.Nodes.Clear();
            t.Nodes.Add(tnRoot);

            //t.CollapseAll();
            //t.Nodes[0].Expanded = true;
        }

        private void BuildWebSitesNode(TreeNode tnRoot)
        {
            TreeNode webSitesNode = new TreeNode("Web Sites");
            webSitesNode.ImageUrl = "~/Content/Assets/Images/TreeView/wf1.gif";

            WContext context = new WContext(this);
            int siteId = context.GetId(WebColumns.SiteId);

            webSitesNode.Expanded = siteId > 0;

            // Definition: Recursive Site Loader
            Func<int, IEnumerable<WSite>, TreeNode, bool> LoadRecursiveWebSites = null;
            LoadRecursiveWebSites = (int parentId, IEnumerable<WSite> allSites, TreeNode rootNode) =>
            {
                List<WSite> sites = WSite.FilterPermittedWithChildren(allSites, parentId, true); //.FilterPermitted(allSites, iParentID);

                foreach (WSite site in sites)
                {
                    TreeNode siteNode = new TreeNode(site.Name);
                    siteNode.ImageUrl = "~/Content/Assets/Images/TreeView/ws.gif";
                    siteNode.NavigateUrl = string.Format("{0}?{1}={2}", CentralPages.WebSiteHome, WebColumns.SiteId, site.Id);

                    //if (site.Identity != WebConstants.CentralID)
                    //{
                    #region Load WebPages

                    // WebPages node
                    TreeNode pagesNode = new TreeNode("Web Pages");
                    pagesNode.ImageUrl = "~/Content/Assets/Images/TreeView/mp2.gif";
                    pagesNode.NavigateUrl = string.Format("{0}?{1}={2}", CentralPages.WebPages, WebColumns.SiteId, site.Id);

                    // Load Web Pages
                    if (site.Id == siteId)
                    {
                        // List non-orphan pages
                        List<int> nonOrphanList = new List<int>();

                        Action<int, IEnumerable<WPage>, TreeNode> LoadRecursivePageTree = null;
                        LoadRecursivePageTree = (int parentId2, IEnumerable<WPage> table, TreeNode pageRoot) =>
                        {
                            //sTab += WebConstants.TAB;
                            var pages = from p in table
                                        where p.ParentId == parentId2
                                        select p;

                            foreach (WPage page in pages)
                            {
                                TreeNode pageNode = new TreeNode(page.Name, page.Id.ToString());
                                pageNode.SelectAction = TreeNodeSelectAction.Select;
                                pageNode.NavigateUrl = string.Format("{0}?{1}={2}&{3}={4}", CentralPages.WebPageHome, WebColumns.SiteId, page.SiteId, WebColumns.PageId, page.Id);
                                pageRoot.ChildNodes.Add(pageNode);

                                // Add to non-orphan list
                                nonOrphanList.Add(page.Id);

                                LoadRecursivePageTree(page.Id, table, pageNode);
                            }
                        };


                        var webPages = WPage.FilterPermitted(site.Id);

                        // START RECURSIVE
                        LoadRecursivePageTree(-1, webPages, pagesNode);

                        // Search for orphans
                        if (webPages.Count() > nonOrphanList.Count)
                        {
                            List<WPage> orphanPages = new List<WPage>();
                            foreach (var page in webPages)
                            {
                                if (!nonOrphanList.Contains(page.Id))
                                    orphanPages.Add(page);
                            }

                            if (orphanPages.Count > 0)
                            {
                                TreeNode orphanNode = new TreeNode("* Orphan", "");
                                orphanNode.SelectAction = TreeNodeSelectAction.Expand;
                                pagesNode.ChildNodes.Add(orphanNode);

                                // There are orphans
                                List<int> orphanRoots = new List<int>();
                                foreach (WPage page in orphanPages)
                                {
                                    // Process root orphans
                                    if (orphanPages.Find(i => i.Id == page.ParentId) == null && !orphanRoots.Contains(page.ParentId))
                                    {
                                        orphanRoots.Add(page.ParentId);
                                        LoadRecursivePageTree(page.ParentId, orphanPages, orphanNode);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        context.Set(WebColumns.SiteId, site.Id);

                        pagesNode.ChildNodes.Add(
                            new TreeNode
                            {
                                Text = "View Pages",
                                ImageUrl = "~/Content/Assets/Images/Common/ico_pages.gif",
                                Target = "_self",
                                NavigateUrl = context.BuildQuery()
                            }
                        );
                    }

                    siteNode.ChildNodes.Add(pagesNode);

                    if (site.Id != siteId)
                        siteNode.CollapseAll();

                    pagesNode.CollapseAll();
                    pagesNode.Expanded = true;

                    #endregion

                    #region Load MasterPages

                    // LOAD ALL INSIDE PAGES
                    if (WSession.Current.IsAdministrator || WebGlobalPolicy.IsUserAdded(GlobalPolicies.WebSiteManagement))
                    {
                        TreeNode masterPagesNode = new TreeNode("Master Pages");
                        masterPagesNode.ImageUrl = "~/Content/Assets/Images/TreeView/mp1.gif";
                        masterPagesNode.NavigateUrl = string.Format("{0}?{1}={2}", CentralPages.WebMasterPages, WebColumns.SiteId, site.Id); //else tn4.SelectAction = TreeNodeSelectAction.Expand;
                        masterPagesNode.Expanded = false;

                        var masterPages = WebMasterPage.FilterPermitted(site.Id);
                        if (masterPages != null)
                        {
                            QueryParser query = new QueryParser(HttpContext.Current);
                            query.Set(WebColumns.SiteId, site.Id);

                            foreach (WebMasterPage masterPage in masterPages)
                            {
                                TreeNode tnSitePage = new TreeNode(masterPage.Name);
                                tnSitePage.ImageUrl = "~/Content/Assets/Images/TreeView/pr.gif";

                                query.Set(WebColumns.MasterPageId, masterPage.Id);
                                tnSitePage.NavigateUrl = query.BuildQuery(CentralPages.WebMasterPageHome);

                                //if (showOrphan)
                                //{
                                //    tn5 = new TreeNode("* Misplaced Items");
                                //    tn5.NavigateUrl = "OrphanPageItems.aspx?SiteId=" + masterPage.SiteId + "&MasterPageId=" + masterPage.MasterPageId;
                                //    tnSitePage.ChildNodes.Add(tn5);
                                //}

                                masterPagesNode.ChildNodes.Add(tnSitePage);
                            }
                        }

                        if (masterPagesNode.ChildNodes.Count > 0 || site.IsUserPermitted(Permissions.ManageInstance, 0))
                            siteNode.ChildNodes.Add(masterPagesNode);
                    }

                    #endregion

                    // Menus node
                    TreeNode settingsNode = new TreeNode("Settings");
                    settingsNode.ImageUrl = "~/Content/Assets/Images/Common/Modules2.gif";
                    settingsNode.SelectAction = TreeNodeSelectAction.Expand;
                    settingsNode.Expanded = false;

                    var parts = WPart.GetList();
                    var admins = from admin in WebPartAdmin.GetList()
                                    where admin.IsInSiteContext && admin.IsActive
                                    select admin;

                    foreach (var part in parts)
                    {
                        if (part.IsActive)
                        {
                            var partAdmins = from admin in admins
                                             where admin.PartId == part.Id
                                             select admin;

                            foreach (var admin in partAdmins)
                            {
                                TreeNode adminNode = new TreeNode(admin.Name);
                                adminNode.ImageUrl = "~/Content/Assets/Images/TreeView/cl.gif";
                                adminNode.NavigateUrl = string.Format("~/Content/Parts/Central/?PartAdminId={0}&SiteId={1}", admin.Id, site.Id);
                                settingsNode.ChildNodes.Add(adminNode);
                            }
                        }
                    }

                    //settingsNode.NavigateUrl = string.Format("/Content/Parts/Central/?PartAdminId=66&{0}={1}", WebColumns.SiteId, site.Id);
                    siteNode.ChildNodes.Add(settingsNode);

                    // Child websites
                    LoadRecursiveWebSites(site.Id, allSites, siteNode);
                    //}

                    // Add site to parent node
                    rootNode.ChildNodes.Add(siteNode);
                }

                return (sites.Count > 0);
            };

            // START RECURSIVE
            LoadRecursiveWebSites(-1, WSite.GetList(), webSitesNode);

            if (webSitesNode.ChildNodes.Count > 0)
            {
                webSitesNode.NavigateUrl = CentralPages.WebSites;
                tnRoot.ChildNodes.Add(webSitesNode);
            }
        }

        private static void BuildWebPartsNode(TreeNode tnRoot)
        {
            if (WebGlobalPolicy.IsUserAdded(GlobalPolicies.WebPartManagement)) // NOT CONTENT USER
            {
                bool hasMgmtPermission = WebGlobalPolicy.IsUserPermitted(GlobalPolicies.WebPartManagement, Permissions.ManageInstance);

                TreeNode partsNode = new TreeNode("Web Parts");
                partsNode.ImageUrl = "~/Content/Assets/Images/Common/Modules2.gif";

                var parts = WPart.GetList();
                var admin = WebPartAdmin.GetList();

                // Limit the list if not an Admin
                if (!WSession.Current.IsAdministrator)
                {
                    // Check WebPart permissions
                    parts = from p in parts
                            where WebObjectSecurity.IsUserAdded(p)
                            select p;
                }

                // Main loop: iterate all WebParts
                foreach (WPart part in parts)
                {
                    // Skip inactive WebParts
                    if (!part.IsActive)
                        continue;

                    TreeNode partNode = new TreeNode(part.Name);
                    var adminParts = admin.Where(item => item.PartId == part.Id && item.IsVisible && item.IsActive);
                    Func<int, IEnumerable<WebPartAdmin>, TreeNode, int, bool> LoadRecursiveParts = null;

                    // Definition
                    LoadRecursiveParts = (int parentId, IEnumerable<WebPartAdmin> items, TreeNode node, int partId) =>
                    {
                        // Get all AdminParts in the specific level (having same parents)
                        var levelParts = items.Where(item => item.PartId == partId && item.ParentId == parentId);
                        if (!WSession.Current.IsAdministrator)
                        {
                            // Check permission
                            var securityParts = from p in levelParts
                                                where WebObjectSecurity.IsUserAdded(p)
                                                select p;

                            if (securityParts.Count() > 0)
                                levelParts = securityParts;
                        }

                        foreach (WebPartAdmin partAdmin in levelParts)
                        {
                            if (partAdmin.ParentId != 0)
                            {
                                TreeNode adminNode = new TreeNode(partAdmin.Name);
                                adminNode.ImageUrl = "~/Content/Assets/Images/TreeView/cl.gif";
                                adminNode.NavigateUrl = "~/Content/Parts/Central/?PartAdminId=" + partAdmin.Id;

                                LoadRecursiveParts(partAdmin.Id, items, adminNode, partId);
                                node.ChildNodes.Add(adminNode);
                            }
                        }

                        return levelParts.Count() > 0;
                    };

                    if (LoadRecursiveParts(-1, adminParts, partNode, part.Id))
                    {
                        partsNode.ChildNodes.Add(partNode);
                        partNode.ImageUrl = "~/Content/Assets/Images/TreeView/mo.gif";

                        if (hasMgmtPermission)
                            partNode.NavigateUrl = string.Format("{0}{1}{2}", CentralPages.WebPartHome, "?PartId=", part.Id);
                        else
                            partNode.SelectAction = TreeNodeSelectAction.Expand;
                    }
                }

                // Whether to allow to Manage WebParts
                if (hasMgmtPermission)
                {
                    partsNode.NavigateUrl = CentralPages.WebParts;
                    tnRoot.ChildNodes.Add(partsNode);
                }
                else
                {
                    partsNode.SelectAction = TreeNodeSelectAction.Expand;
                    if (partsNode.ChildNodes.Count > 0)
                        tnRoot.ChildNodes.Add(partsNode);
                }

                partsNode.CollapseAll();
            }
        }

        private static void BuildToolsNode(TreeNode tnRoot)
        {
            TreeNode toolsNode = new TreeNode("Tools");
            toolsNode.ImageUrl = "~/Content/Assets/Images/Common/Tools.gif";
            toolsNode.SelectAction = TreeNodeSelectAction.Expand;
            toolsNode.Expanded = false;
            {
                TreeNode toolNode = null;

                toolNode = new TreeNode("Online Setup");
                toolNode.NavigateUrl = CentralPages.Setup;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/ico_tools.gif";
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Short URL Manager");
                toolNode.NavigateUrl = CentralPages.ShortUrlManager;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/Actions/CanvasScaleHS.png";
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Web Registry");
                toolNode.NavigateUrl = CentralPages.WebRegistry;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/mp2.gif";
                toolsNode.ChildNodes.Add(toolNode);

                //tn2 = new TreeNode("IP Restrictions");
                //tn2.NavigateUrl = "~/Content/Admin/IPAddresses.aspx";
                //tn1.ChildNodes.Add(tn2);

                //toolNode = new TreeNode("Event Manager");
                //toolNode.NavigateUrl = CentralPages.EventManager;
                //toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Query Analyzer");
                toolNode.NavigateUrl = CentralPages.QueryAnalyzer;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/view.gif";
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("File Manager");
                toolNode.NavigateUrl = CentralPages.FileManager;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/webfolder.gif";
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("SMTP Diagnostics");
                toolNode.NavigateUrl = CentralPages.SmtpAnalyzer;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/ico_postnew.gif";
                toolsNode.ChildNodes.Add(toolNode);

                // Manages all data stores like database tables, xml files, etc
                toolNode = new TreeNode("Web Object Manager");
                toolNode.NavigateUrl = CentralPages.WebObjectManager; // Entity Manager?
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/Actions/OrganizerHS.png";
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Data Structure Explorer");
                toolNode.NavigateUrl = CentralPages.WebDataExplorer;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/Actions/RelationshipsHS.png";
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Parameter Sets");
                toolNode.NavigateUrl = CentralPages.WebParameterSets;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/Actions/WindowsHS.png";
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Themes & Templates");
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/Actions/MultiplePagesHS.png";
                toolNode.NavigateUrl = CentralPages.WebThemes;
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Resource Manager");
                toolNode.NavigateUrl = CentralPages.WebResourceManager;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/Objects.gif";
                toolsNode.ChildNodes.Add(toolNode);
            }
            tnRoot.ChildNodes.Add(toolsNode);
        }

        private static void BuildSecurityNode(TreeNode tnRoot)
        {
            var isAdmin = WSession.Current.IsAdministrator;

            TreeNode secNode = new TreeNode("Security");
            secNode.ImageUrl = "~/Content/Assets/Images/TreeView/l.gif";
            secNode.SelectAction = TreeNodeSelectAction.Expand;
            secNode.Expanded = false;
            {
                // SUPER ADMIN MODULES
                TreeNode securityNode = new TreeNode("My Profile");
                securityNode.NavigateUrl = CentralPages.WebUserHome + "?UserId=" + WSession.Current.UserId;
                securityNode.ImageUrl = "~/Content/Assets/Images/Common/ico_edit2.gif";
                secNode.ChildNodes.Add(securityNode);

                if (isAdmin)
                {
                    securityNode = new TreeNode("Users");
                    securityNode.NavigateUrl = CentralPages.WebUsers;
                    securityNode.ImageUrl = "~/Content/Assets/Images/Common/ico_persons.gif";
                    secNode.ChildNodes.Add(securityNode);
                }

                if (isAdmin)
                {
                    //tn2 = new TreeNode("Security Audits");
                    //tn2.SelectAction = TreeNodeSelectAction.None;
                    //tn1.ChildNodes.Add(tn2);

                    securityNode = new TreeNode("Groups");
                    securityNode.NavigateUrl = CentralPages.WebGroups;
                    securityNode.ImageUrl = "~/Content/Assets/Images/TreeView/u.gif";
                    secNode.ChildNodes.Add(securityNode);

                    // Define global permissions like access to WebPart Management, Portal management, etc
                    securityNode = new TreeNode("Global Policy");
                    securityNode.NavigateUrl = CentralPages.WebGlobalPolicy;
                    securityNode.ImageUrl = "~/Content/Assets/Images/Common/lock.gif";
                    secNode.ChildNodes.Add(securityNode);

                    securityNode = new TreeNode("Roles");
                    securityNode.NavigateUrl = CentralPages.WebRoles;
                    securityNode.ImageUrl = "~/Content/Assets/Images/TreeView/u.gif";
                    secNode.ChildNodes.Add(securityNode);

                    securityNode = new TreeNode("Permissions");
                    securityNode.NavigateUrl = CentralPages.WebPermissions;
                    securityNode.ImageUrl = "~/Content/Assets/Images/Common/ico_lock.gif";
                    secNode.ChildNodes.Add(securityNode);

                    /*
                    tn2 = new TreeNode("System Events");
                    tn2.NavigateUrl = "~/Content/Admin/SystemEvents.aspx";
                    tn1.ChildNodes.Add(tn2);
                    */
                }
            }
            tnRoot.ChildNodes.Add(secNode);
        }

        //protected void cmdSync_Click(object sender, EventArgs e)
        //{
        //    this.BuildTreeView();
        //}
    }
}