using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.Windows
{
    public partial class LinkBrowser : System.Web.UI.Page
    {
        private int _siteId;
        private QueryParser query;

        protected void Page_Load(object sender, EventArgs e)
        {
            hControlName.Value = Request["Control"];

            this.PopulateTreePortal();
        }

        private void PopulateTreePortal()
        {
            query = new QueryParser(this);

            string portalName = WConfig.SystemName;
            int partId = query.GetId(WebColumns.PartId);

            _siteId = query.GetId(WebColumns.SiteId);

            // TREE VIEW
            var nodePortal = new TreeNode(portalName, "-1");
            nodePortal.SelectAction = TreeNodeSelectAction.Expand;
            TreeView1.Nodes.Add(nodePortal);

            var sites = WSite.GetList();

            // START RECURSIVE
            LoadRecursiveWebSites(-1, sites, nodePortal, partId);

            //TreeView1.CollapseAll();
            TreeView1.Nodes[0].Expanded = true;
        }

        private bool LoadRecursiveWebSites(int parentId, IEnumerable<WSite> sites, TreeNode rootNode, int partId)
        {
            var nodeAdded = false;
            var subSites = sites.Where(item => item.ParentId == parentId);
            foreach (var site in subSites)
            {
                if (WSession.Current.IsAdministrator || site.Identity != WConstants.CentralID)
                {
                    var page = site.HomePage;
                    // TREE VIEW
                    var siteNode = new TreeNode(site.Name, site.Id.ToString());

                    // Add Pages
                    bool hasValidChild = true;
                    if (partId > 0 || _siteId == site.Id)
                    {
                        var pages = WPage.GetList(site.Id);
                        hasValidChild = LoadRecursiveWebPages(-1, pages, siteNode, partId);
                    }
                    else
                    {
                        query.Set(WebColumns.SiteId, site.Id);
                        siteNode.ChildNodes.Add(
                            new TreeNode
                            {
                                Text = "View Pages",
                                ImageUrl = "~/Content/Assets/Images/Common/ico_pages.gif",
                                Target = "_self",
                                NavigateUrl = query.BuildQuery()
                            }
                        );
                    }

                    // Add Children Sites
                    if (LoadRecursiveWebSites(site.Id, sites, siteNode, partId) && !hasValidChild)
                        hasValidChild = true;

                    var valid = partId == -1 || (partId > 0 && page != null && page.PartControlTemplate.Part.Id == partId);
                    if (hasValidChild || valid)
                    {
                        if (valid)
                            siteNode.NavigateUrl = string.Format("javascript:ReturnLink('{0}');", site.BuildRelativeUrl());
                        else
                            siteNode.SelectAction = TreeNodeSelectAction.Expand;

                        siteNode.ImageUrl = "~/Content/Assets/Images/Common/WebSite.gif";
                        siteNode.CollapseAll();

                        if (_siteId == site.Id)
                            siteNode.Expand();

                        rootNode.ChildNodes.Add(siteNode);

                        if (!nodeAdded)
                            nodeAdded = true;
                    }
                }
            }

            return nodeAdded;
        }

        private bool LoadRecursiveWebPages(int parentId, IEnumerable<WPage> pages, TreeNode rootNode, int partId)
        {
            var nodeAdded = false;
            var subPages = pages.Where(item => item.ParentId == parentId);
            foreach (var page in subPages)
            {
                // TREE VIEW
                var pageNode = new TreeNode(page.Name, page.Id.ToString());
                var hasValidChild = LoadRecursiveWebPages(page.Id, pages, pageNode, partId);
                var partTemplate = page.PartControlTemplate;

                bool valid = true;
                if (partId == -1 || (valid = partTemplate != null && partTemplate.Part.Id == partId) || hasValidChild)
                {
                    if (valid)
                        pageNode.NavigateUrl = string.Format("javascript:ReturnLink('{0}');", page.BuildRelativeUrl());
                    else
                        pageNode.SelectAction = hasValidChild ? TreeNodeSelectAction.Expand : TreeNodeSelectAction.None;

                    rootNode.ChildNodes.Add(pageNode);
                    if (!nodeAdded)
                        nodeAdded = true;
                }
            }

            return nodeAdded;
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

        }
    }
}