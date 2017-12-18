using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Controls
{
    public partial class DesignModeController : UserControl
    {
        // List non-orphan pages
        //private List<int> nonOrphanList;

        protected void Page_Load(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var page = WPage.Get(context.PageId);
            var site = page.Site;

            var panelExpanded = WSession.Current.InDesignPanelExpanded;

            designPanelContent.Style["display"] = panelExpanded ? "block" : "none";

            if (panelExpanded)
                panelMiniIcons.Style["display"] = "none";
            else
                panelMiniIcons.Style.Remove("display");

            //designMode.Style["top"] = WSession.Current.InDesignPanelTop + "px";
            //designMode.Style["left"] = WSession.Current.InDesignPanelLeft + "px";

            showHideDesignMode.InnerHtml = panelExpanded ? "&laquo;" : "&raquo;";

            #region Web Page

            if (WSession.Current.IsAdministrator || page.IsUserMgmtPermitted(Permissions.ManageContent))
            {
                bool canManageInstance = WSession.Current.IsAdministrator || page.IsUserMgmtPermitted(Permissions.ManageInstance);

                // # Web Page #
                //panelWebPage.Visible = true;

                //if (canManageInstance)
                //{
                //    // Page > New Page
                //    panelNewPage.Visible = true;
                //    linkNewPage.HRef = QueryParser.BuildQuery(CentralPages.WebSite, WebColumns.SiteId, page.SiteId);
                //}

                var query = context.Query.Clone();
                query.Set(WebColumns.SiteId, page.SiteId);
                query.Set(WebColumns.PageId, page.Id);

                var partControl = page.PartControlTemplate.PartControl;
                var admin = partControl.PartAdmin;
                //admins = partControl.Part.AdminControls.Where(i => i.IsVisible);

                // Web Page > Edit Mode
                var editModeUrl = query.BuildQuery(admin != null && admin.TemplateEngineId == TemplateEngineTypes.Razor ? CentralPages.LoaderRazor : CentralPages.LoaderMain);

                // Web Page > Edit Mode
                //var editModeUrl = query.BuildQuery(CentralPages.LoaderMain);

                panelPageEditMode.Visible = true;
                linkPageEditMode.HRef = editModeUrl;

                linkEditMode.HRef = editModeUrl;
                linkEditMode.Visible = true;

                //var configureUrl = new QueryParser(CentralPages.WebPageHome).Set(WebColumns.PageId, page.Id).Set(WebColumns.SiteId, site.Id).BuildQuery();

                //linkPageConfigure.HRef = configureUrl;
                //panelPageConfigure.Visible = true;

                //linkConfigure.HRef = configureUrl;
                //linkConfigure.Visible = true;

                //if (canManageInstance)
                //{
                //    // Web Page > Delete
                //    MenuItem mnuPageDelete = new MenuItem("Delete");
                //    mnuPageDelete.NavigateUrl = Request.RawUrl + "#";
                //    mnuPageDelete.ImageUrl = "~/Content/Assets/Images/Common/ico_exit.gif";
                //    mnuPage.ChildItems.Add(mnuPageDelete);
                //}
            }

            #endregion

            /*
            #region Web Site

            if (WSession.Current.IsAdministrator || site.IsUserMgmtPermitted(Permissions.ManageContent))
            {
                // # Web Site #
                //MenuItem mnuSite = new MenuItem("Web Site");
                //mnuSite.NavigateUrl = "~/Central/WebSiteHome.aspx?SiteId=" + page.SiteId;
                //mnuSite.ImageUrl = "~/Content/Assets/Images/Common/WebSite.gif";
                //mnSites.Items.Add(mnuSite);

                panelWebSite.Visible = true;

                if (WSession.Current.IsAdministrator || site.IsUserMgmtPermitted(Permissions.ManageInstance))
                {
                    // Web Site > Create Site
                    //MenuItem mnuSiteCreate = new MenuItem("New Site");
                    //mnuSiteCreate.NavigateUrl = "~/Central/WebSite.aspx?ParentSiteId=" + page.SiteId;
                    //mnuSiteCreate.ImageUrl = "~/Content/Assets/Images/Common/image_new.gif";
                    //mnuSite.ChildItems.Add(mnuSiteCreate);

                    panelNewSite.Visible = true;
                    linkNewSite.HRef = "~/Central/WebSite.aspx?ParentSiteId=" + page.SiteId;

                    // Web Site > Configure
                    //MenuItem mnuSiteConfig = new MenuItem("Configure");
                    //mnuSiteConfig.NavigateUrl = mnuSite.NavigateUrl;
                    //mnuSiteConfig.ImageUrl = "~/Content/Assets/Images/Common/ico_edit.gif";
                    //mnuSite.ChildItems.Add(mnuSiteConfig);

                    panelSiteConfigure.Visible = true;
                    linkSiteConfigure.HRef = "~/Central/WebSiteHome.aspx?SiteId=" + page.SiteId;

                    // Web Site > Delete
                    //MenuItem mnuSiteDelete = new MenuItem("Delete");
                    //mnuSiteDelete.NavigateUrl = Request.RawUrl + "#";
                    //mnuSiteDelete.ImageUrl = "~/Content/Assets/Images/Common/ico_exit.gif";
                    //mnuSite.ChildItems.Add(mnuSiteDelete);
                }
            }

            #endregion
            */

            #region All Web Sites

            // # All Web Sites #
            MenuItem rootMenu = new MenuItem("<strong>All Web Sites</strong>");
            rootMenu.NavigateUrl = WConfig.DefaultSite.BuildRelativeUrl();
            rootMenu.ImageUrl = "~/Content/Assets/Images/Common/wf1.gif";
            mnSites.Items.Add(rootMenu);
            LoadRecursiveTree(-1, WSite.GetList(), rootMenu, page);

            #endregion

            #region Administrator

            MenuItem rootAdmin = new MenuItem("Dashboard");
            rootAdmin.NavigateUrl = CentralPages.CentrlHome;
            rootAdmin.ImageUrl = "~/Content/Assets/Images/Common/Gear.gif";
            rootAdmin.Target = "_top";
            mnSites.Items.Add(rootAdmin);

            linkAdmin.HRef = CentralPages.CentrlHome;

            //if (WSession.Current.IsAdministrator)
            //{
            //    MenuItem setup = new MenuItem("Web Setup");
            //    setup.NavigateUrl = CentralPages.Setup;
            //    setup.ImageUrl = "~/Content/Assets/Images/Common/Actions/FullScreenHS.png";
            //    rootAdmin.ChildItems.Add(setup);

            //    MenuItem managerNode = new MenuItem("Manager UI");
            //    managerNode.NavigateUrl = CentralPages.WebSystemDashboard;
            //    managerNode.ImageUrl = "~/Content/Assets/Images/Common/Actions/ThumbnailView.png";
            //    rootAdmin.ChildItems.Add(managerNode);
            //}

            #endregion

            var q = new WQuery(WConfig.DefaultLoginPage);
            q.Set("Mode", "LogOff");
            linkLogOff.HRef = q.BuildQuery();
        }

        public void BindPanels(IEnumerable<WebTemplatePanel> panels)
        {
            cboPanels.DataSource = panels;
            cboPanels.DataBind();

            cboPanels.SelectedValue = WSession.Current.InDesign.ToString();
        }

        public bool ModeChangerVisible
        {
            get { return panelModeChanger.Visible; }
            set { panelModeChanger.Visible = value; }
        }

        private bool LoadRecursiveTree(int parentId, IEnumerable<WSite> allSites, MenuItem nodeRoot, WPage currentPage)
        {
            var sites = WSite.FilterPermittedWithChildren(allSites, parentId); //.FilterPermitted(allSites, parentId);

            foreach (WSite site in sites)
            {
                MenuItem menuSite = new MenuItem(site.Name);
                menuSite.ImageUrl = "~/Content/Assets/Images/TreeView/ws.gif";
                menuSite.NavigateUrl = site.BuildRelativeUrl();

                if (WSession.Current.IsAdministrator || site.Identity != WConstants.CentralID)
                {
                    // Process WebPages
                    if (site.Id == currentPage.SiteId)
                    {
                        menuSite.Text = string.Format("<strong>{0}</strong>", menuSite.Text);

                        /*
                        nonOrphanList = new List<int>();
                        var pages = WebPage.FilterPermitted(site.Id);

                        LoadRecursivePageTree(-1, pages, menuSite);

                        // Search for orphans
                        if (pages.Count > nonOrphanList.Count)
                        {
                            List<WebPage> orphanPages = new List<WebPage>();
                            foreach (var page in pages)
                            {
                                if (!nonOrphanList.Contains(page.Id))
                                    orphanPages.Add(page);
                            }

                            if (orphanPages.Count > 0)
                            {
                                // There are orphans
                                List<int> orphanRoots = new List<int>();
                                foreach (WebPage page in orphanPages)
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
                        */
                    }

                    // Create SiteMap link
                    MenuItem siteMap = new MenuItem("View Site Map");
                    siteMap.ImageUrl = "~/Content/Assets/Images/Common/ico_pages.gif";
                    siteMap.NavigateUrl = string.Format("{0}?SiteId={1}", CentralPages.SiteMap, site.Id);
                    menuSite.ChildItems.Add(siteMap);

                    // SubSites
                    LoadRecursiveTree(site.Id, allSites, menuSite, currentPage);
                }

                nodeRoot.ChildItems.Add(menuSite);
            }

            return (sites.Count > 0);
        }

        private void LoadRecursivePageTree(int parentId, List<WPage> table, MenuItem tnRoot)
        {
            var pages = from p in table
                        where p.ParentId == parentId
                        select p;

            foreach (WPage page in pages)
            {
                MenuItem tn1 = new MenuItem(page.Name, page.Id.ToString());
                tn1.ImageUrl = "~/Content/Assets/Images/TreeView/t.gif";
                tn1.NavigateUrl = page.BuildRelativeUrl();
                tnRoot.ChildItems.Add(tn1);

                // Add to non-orphan list
                //nonOrphanList.Add(page.Id);

                LoadRecursivePageTree(page.Id, table, tn1);
            }
        }

        protected void cboPanels_SelectedIndexChanged(object sender, EventArgs e)
        {
            int previewMode = DataHelper.GetInt32(cboPanels.SelectedValue);

            SetPreviewMode(previewMode);
        }

        private void SetPreviewMode(int previewMode)
        {
            WSession.Current.InDesign = previewMode;

            // This still needs improvement
            WebHelper.Redirect(Request.RawUrl, Context);
        }

        protected void cmdToggle_Click(object sender, ImageClickEventArgs e)
        {
            int current = DataHelper.GetInt32(cboPanels.SelectedValue);
            if (current != DesignerConstants.PreviewMode)
                SetPreviewMode(DesignerConstants.PreviewMode);
            else
                SetPreviewMode(DesignerConstants.AllPanels);
        }
    }
}