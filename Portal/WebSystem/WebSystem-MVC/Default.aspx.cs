using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Diagnostics;

using WCMS.WebSystem.Controls;
using WCMS.WebSystem.WebParts.Central.Controls;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem
{
    public partial class DefaultViewController : WLoaderPageBase
    {
        private int pageId = -1;
        private string pageTitle = string.Empty;

        private Stopwatch stopWatch = null;

        private StringBuilder resources = null;
        private WQuery query = null;
        private WPage page = null;
        private WSite site = null;

        private HashSet<int> partResources = null;
        private HashSet<int> controlResources = null;
        private HashSet<int> templateResources = null;

        protected void Page_Unload(object sender, EventArgs e)
        {
            PerformanceLog.EndLog("Page Unload", stopWatch, pageId);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            stopWatch = PerformanceLog.StartLog();

            // Search for PageId
            pageId = DataHelper.GetId(Request, WebColumns.PageIdInternal);
            if (pageId == -1)
            {
                var baseTemplate = LoadControl("~/Content/Themes/Default/ForAjaxControlToolkit.ascx");
                this.Controls.Add(baseTemplate);
                return;
            }

            page = WHelper.GetPageOrDefault(pageId);
            if (page == null)
                return;

            site = page.Site;
            query = new WQuery(this);
            resources = new StringBuilder();
            var session = WSession.Current;

            if (!CheckAccess(page, query, session))
                return;

            if (page.GetEvalTypeId() == PageTypes.Static)
            {
                LoadStaticPage(page, session);
                return;
            }

            var masterPage = page.MasterPage;
            if (masterPage == null)
                throw new Exception("MasterPage is NULL");
            var template = masterPage.Template; // Null error encountered

            // Load template
            IEnumerable<WebTemplatePanel> designerPanels = null;
            IEnumerable<WebTemplatePanel> panels = new List<WebTemplatePanel>();
            var TemplateControls = new Dictionary<int, Control>();

            //Control container = null;
            Func<Control, WebTemplate, ObjectPair<Control, WebTemplate>> LoadTemplateRecursive = null;
            LoadTemplateRecursive = (ctrl, tmpl) =>
            {
                var templateControl = LoadControl(string.Format("~/Content/Themes/{0}/{1}", tmpl.Identity, tmpl.FileName));
                var templateProvider = templateControl as IObjectValueProvider;
                if (templateProvider != null)
                    templateProvider.Values = Values;

                //if (container == null)
                //    container = templateControl;
                TemplateControls.Add(tmpl.Id, templateControl);

                // Collect all panels
                panels = panels.Concat(tmpl.Panels);
                var parentTmpl = tmpl.Parent;
                if (parentTmpl != null)
                {
                    // load self
                    var panel = parentTmpl.PrimaryPanel;
                    if (panel != null)
                    {
                        // Get the parent control and add the child control. Return parent
                        var parentPair = LoadTemplateRecursive(templateControl, parentTmpl);
                        var panelCtrl = parentPair.Object1.FindControl(panel.PanelName);
                        if (panelCtrl != null)
                            panelCtrl.Controls.Add(templateControl);
                        return parentPair;
                    }
                }
                return new ObjectPair<Control, WebTemplate>(templateControl, tmpl);
            };


            var ctrlTmplPair = LoadTemplateRecursive(this, template);
            if (ctrlTmplPair.Object2.IsStandalone)
            {
                //container = ctrlTmplPair.Object1;
                this.Controls.Add(ctrlTmplPair.Object1);
            }
            else
            {
                var baseTemplate = LoadControl(WConstants.DEFAULT_THEME_TEMPLATE);
                var baseTemplateProvider = baseTemplate as IObjectValueProvider;
                if (baseTemplateProvider != null)
                    baseTemplateProvider.Values = Values;

                this.Controls.Add(baseTemplate);

                Form.Controls.Add(ctrlTmplPair.Object1);
            }

            #region Persist PageId in Hidden Field

            var hPageId = new HiddenField();
            hPageId.ClientIDMode = ClientIDMode.Static;
            hPageId.ID = WConstants.HIDDEN_PAGE_ID;
            hPageId.Value = page.Id.ToString();
            this.Form.Controls.Add(hPageId);

            #endregion

            // WebPart Resources
            partResources = new HashSet<int>();
            controlResources = new HashSet<int>();
            templateResources = new HashSet<int>();

            // Permission-related flags
            bool inRenderMode = (!session.IsInDesign || (site.Identity == WConstants.CentralID && !WConfig.EnableInlineEditor));
            bool pageContentMgmtPermitted = false;
            bool masterPageContentMgmtPermitted = false;

            #region Render inline-editor control

            if (session.IsLoggedIn)
            {
                // Log Activity
                WSession.UserSessions.Update(session.UserId, page.Id);
                DesignModeController modeCtrl = null;
                if (this.Header != null && session.IsSiteManager) // || WebGlobalPolicy.IsUserAdded(GlobalPolicies.WebSiteManagement)))
                {
                    var identity = page.Site.Identity;
                    if (identity != WConstants.CentralID || (identity == WConstants.CentralID && WConfig.EnableInlineEditor))
                    {
                        modeCtrl = LoadControl(WSConstants.DesignModeControlPath) as DesignModeController;
                        modeCtrl.ID = "modeCtrl";
                        modeCtrl.ClientIDMode = ClientIDMode.Static;
                        this.Form.Controls.Add(modeCtrl);

                        // Check is user has ManageContent permission for Page or MasterPage
                        pageContentMgmtPermitted = page.IsUserMgmtPermitted(Permissions.ManageContent);
                        masterPageContentMgmtPermitted = masterPage.IsUserMgmtPermitted(Permissions.ManageContent);
                        if (pageContentMgmtPermitted || masterPageContentMgmtPermitted)
                        {
                            if (pageContentMgmtPermitted && masterPageContentMgmtPermitted)
                            {
                                // Has all permissions
                                modeCtrl.BindPanels(panels);
                                if (!inRenderMode)
                                    designerPanels = panels;
                            }
                            else
                            {
                                var panel = template.PrimaryPanel;
                                if (pageContentMgmtPermitted)
                                {
                                    // Add only default panel
                                    var items = new List<WebTemplatePanel>();
                                    items.Add(panel);
                                    modeCtrl.BindPanels(items);
                                    if (!inRenderMode)
                                        designerPanels = items;
                                }
                                else
                                {
                                    // Add only MasterPage panels
                                    var items = panels.Where(i => i.Id != panel.Id);
                                    modeCtrl.BindPanels(items);
                                    if (!inRenderMode)
                                        designerPanels = items;
                                }
                            }
                            modeCtrl.ModeChangerVisible = true;
                        }
                        else
                        {
                            if (!inRenderMode)
                                designerPanels = new List<WebTemplatePanel>();
                            modeCtrl.ModeChangerVisible = false;
                        }
                    }
                }
            }

            #endregion

            bool pageLoaded = false;
            Action<Control> LoadPage = (panelControl) =>
            {
                CreateElementControl(page, panelControl, session);
                SetValueRange(page.Parameters);
                pageLoaded = true;
            };

            int primaryPanelId = template.PrimaryPanelId;
            if (inRenderMode || this.Header == null)
            {
                #region Page is in render mode

                // Load the page content

                // Load PageElements
                var pageElements = from el in page.Elements
                                   where el.IsActive
                                   select (IPageElement)el;

                // Load Panels, only Override or Add
                var pagePanels = from p in page.Panels
                                 where !p.Inherit
                                 select p;

                // Load MasterPageElements
                var items = from i in masterPage.Elements
                            where i.IsActive
                            select (IPageElement)i;

                foreach (var panel in panels)
                {
                    var panelControl = TemplateControls[panel.RecordId].FindControl(panel.PanelName);
                    if (panelControl != null)
                    {
                        IEnumerable<IPageElement> panelElements = null;
                        var pagePanel = pagePanels.SingleOrDefault(pp => pp.TemplatePanelId == panel.Id);

                        // Get all Page-Elements
                        if (pagePanel != null) // && !pagePanel.Inherit)
                        {
                            panelElements = from element in pageElements
                                                 .Where(element => element.TemplatePanelId == panel.Id)
                                            select (IPageElement)element;
                        }

                        // Get all MasterPage-Elements
                        if (pagePanel == null || !pagePanel.Override)
                        {
                            var masterPageItems = from i in items
                                                      .Where(element => element.TemplatePanelId == panel.Id)
                                                  select (IPageElement)i;

                            // Add MasterPage-Elements to Page-Elements
                            panelElements = (pagePanel != null && panelElements != null && pagePanel.Add) ?
                                panelElements.Concat(masterPageItems) :
                                masterPageItems;
                        }

                        panelElements = panelElements.OrderBy(i => i.Rank);

                        int panelCount = panelElements.Count();
                        if (!pageLoaded && panel.Id == primaryPanelId && panelCount == 0)
                        {
                            LoadPage(panelControl);
                        }
                        else
                        {
                            // Load PageItems
                            for (int i = 0; i < panelCount; i++)
                            {
                                var item = panelElements.ElementAt(i);
                                if (!pageLoaded && panel.Id == primaryPanelId && (page.Rank <= item.Rank || (/* if last loop */ page.Rank > item.Rank && panelCount == i + 1)))
                                {
                                    if (page.Rank <= item.Rank)
                                    {
                                        // Page is found first
                                        LoadPage(panelControl);
                                        CreateElementControl(item, panelControl, session);
                                    }
                                    else
                                    {
                                        // Last loop and element if found first
                                        CreateElementControl(item, panelControl, session);
                                        LoadPage(panelControl);
                                    }
                                }
                                else
                                {
                                    CreateElementControl(item, panelControl, session);
                                }
                            }
                        }
                    }
                }

                #endregion
            }
            else
            {
                int containerIDCounter = 0;
                int inDesign = session.InDesign;

                // Check is user has ManageInstance permission for Page or MasterPage
                bool pageInstanceMgmtPermitted = false;
                bool masterPageInstanceMgmtPermitted = false;

                if (inDesign != DesignerConstants.PreviewMode)
                {
                    pageInstanceMgmtPermitted = page.IsUserMgmtPermitted(Permissions.ManageInstance);
                    masterPageInstanceMgmtPermitted = masterPage.IsUserMgmtPermitted(Permissions.ManageInstance);
                }

                #region Design Mode methods

                // Load the page content
                Action<Control, bool> LoadPageDesign = (panelControl, inDesignMode) =>
                {
                    if (inDesignMode)
                    {
                        var designQuery = new QueryParser(this);
                        designQuery.Set(WebColumns.SiteId, page.SiteId);
                        designQuery.Set(WebColumns.PageId, page.Id);

                        // Setup Page controls
                        var control = (PartDesignTemplate)LoadControl(WSConstants.ElementDesignerPath);
                        control.LabelModuleName.InnerHtml = page.IsActive ? page.Name : page.Name + " (<strong>Disabled</strong>)";
                        control.ConfigureUrl = designQuery.BuildQuery(CentralPages.WebPageHome);

                        control.DeleteCell.Visible = false;
                        control.EditModeUrl = designQuery.BuildQuery(CentralPages.LoaderMain);
                        control.SetAsWebPage();

                        #region For Drag-Drop Feature

                        control.ContainerID = string.Format("{0}#Container#{1}", page.Panel.Name, containerIDCounter);
                        control.ItemID = string.Format("{0}#Item#{1}", page.Panel.Name, containerIDCounter);

                        containerIDCounter++;

                        #endregion

                        var toolBox = (PlaceHolderToolbox)panelControl;
                        toolBox.PlaceHolder.Controls.Add(control);

                        // Setup Page content
                        CreateElementControl(page, control.ItemContainer, session);

                        // Change Panel properties because it is the default
                        toolBox.SetAsDefault();

                        pageLoaded = true;

                        string tooltipText = string.Format("Page, Rank: {0}", page.Rank);
                        if (!page.IsActive)
                            tooltipText = string.Format("{0}, Active: No", tooltipText);

                        control.Tooltip = tooltipText;
                    }
                    else
                    {
                        LoadPage(panelControl);
                    }
                };

                // Load Page Element
                Action<Control, IPageElement, bool> LoadElementDesign = (panelControl, item, inDesignMode) =>
                {
                    if (inDesignMode)
                    {
                        var designQuery = new QueryParser(this);
                        designQuery.Set(WebColumns.PageElementId, item.Id);
                        designQuery.Set(WebColumns.TemplatePanelId, item.TemplatePanelId);
                        designQuery.Set(WebColumns.PageId, page.Id);
                        designQuery.Set(WebColumns.SiteId, page.SiteId);

                        // Setup MasterPageItem controls
                        var control = (PartDesignTemplate)LoadControl(WSConstants.ElementDesignerPath);
                        control.LabelModuleName.InnerHtml = item.Active == 1 ? item.Name : item.Name + " (<strong>Disabled</strong>)";
                        control.ConfigureUrl = designQuery.BuildQuery(CentralPages.WebPageElementHome);
                        control.ImageDelete.CommandArgument = item.Id.ToString();
                        control.EditModeUrl = designQuery.BuildQuery(CentralPages.LoaderMain);

                        string toolTipText = string.Empty;

                        // Set Permission
                        var element = item as WebPageElement;
                        if (element != null)
                        {
                            if (element.OwnerIsPage) // Owned by Page
                            {
                                if (!pageInstanceMgmtPermitted)
                                    control.SetPermission(Permissions.ManageContent);

                                control.OwnerType = WebObjects.WebPage;
                                toolTipText = "Owner: Page";
                            }
                            else // Owned by MasterPage
                            {
                                if (!masterPageInstanceMgmtPermitted)
                                    control.SetPermission(Permissions.ManageContent);

                                control.OwnerType = WebObjects.WebMasterPage;
                                toolTipText = "Owner: MasterPage";
                            }
                        }

                        toolTipText = string.Format("{0}, Rank: {1}", toolTipText, item.Rank);
                        if (!item.IsActive)
                            toolTipText = string.Format("{0}, Active: No", toolTipText);

                        control.Tooltip = toolTipText;

                        #region Added 29-Jan-2010

                        control.ContainerID = string.Format("{0}#Container#{1}", item.Panel.Name, containerIDCounter);
                        control.ItemID = string.Format("{0}#Item#{1}", item.Panel.Name, containerIDCounter);

                        containerIDCounter++;

                        #endregion

                        var toolBox = (PlaceHolderToolbox)panelControl;
                        toolBox.PlaceHolder.Controls.Add(control);

                        // Setup MasterPageItem content
                        CreateElementControl(item, control.ItemContainer, session);
                    }
                    else
                    {
                        CreateElementControl(item, panelControl, session);
                    }
                };

                #endregion

                #region Page is in design mode

                query.Set(WebColumns.PageId, page.Id);
                query.Set(WebColumns.MasterPageId, masterPage.Id);

                // Load PageItems
                var items = masterPage.Elements;
                var pagePanels = page.Panels;
                var pageElements = page.Elements;

                foreach (var panel in panels)
                {
                    var panelControl = TemplateControls[panel.RecordId].FindControl(panel.PanelName);
                    if (panelControl != null)
                    {
                        Control panelToUse = null;
                        PlaceHolderToolbox toolbox = null;

                        var pagePanel = pagePanels.FirstOrDefault(pp => pp.TemplatePanelId == panel.Id);

                        bool inDesignMode = false;

                        if (inDesign != DesignerConstants.PreviewMode &&
                            ((inDesign == panel.Id || inDesign == DesignerConstants.AllPanels) && (designerPanels.Count() > 0 && designerPanels.FirstOrDefault(i => i.Id == panel.Id) != null)))
                        {
                            // Panel in Design Mode

                            // Setup MasterPage panel controls
                            toolbox = (PlaceHolderToolbox)LoadControl(WSConstants.PanelDesignerPath);

                            query.Set(WebColumns.TemplatePanelId, panel.Id);
                            toolbox.NewElementUrl = query.BuildQuery(CentralPages.WebPageElement);
                            toolbox.ViewElementsUrl = query.BuildQuery(CentralPages.WebPageElements);
                            toolbox.PanelConfigUrl = query.BuildQuery(CentralPages.WebPagePanel);
                            toolbox.PanelName.InnerHtml = panel.Name;

                            toolbox.SetPermission(pageInstanceMgmtPermitted || masterPageInstanceMgmtPermitted ? Permissions.ManageInstance : Permissions.ManageContent);

                            panelToUse = toolbox;
                            inDesignMode = true;

                            var usageType = pagePanel != null ? pagePanel.UsageTypeId : PanelUsage.Inherit;

                            toolbox.PanelUsageType = usageType;

                            if (panel.Id == primaryPanelId)
                                toolbox.Tooltip = string.Format("Panel Usage: {0}, Default: Yes", PanelUsage.GetText(usageType));
                            else
                                toolbox.Tooltip = string.Format("Panel Usage: {0}", PanelUsage.GetText(usageType));
                        }
                        else
                        {
                            // Panel in Render Mode
                            panelToUse = panelControl;
                        }

                        IEnumerable<IPageElement> panelElements = new List<IPageElement>();

                        // Try to add elements
                        if (pagePanel != null && !pagePanel.Inherit)
                        {
                            panelElements = from element in pageElements.Where(element => element.TemplatePanelId == panel.Id)
                                            where inDesignMode || element.IsActive
                                            select (IPageElement)element;
                        }

                        if (pagePanel == null || !pagePanel.Override)
                        {
                            var masterPageItems = from i in items.Where(element => element.TemplatePanelId == panel.Id)
                                                  where inDesignMode || i.IsActive
                                                  select (IPageElement)i;

                            if (pagePanel != null && pagePanel.Add)
                            {
                                if (panelElements.Count() > 0)
                                    panelElements = panelElements.Concat(masterPageItems); // Combine
                                else
                                    panelElements = masterPageItems; // Use only MasterPage elements
                            }
                            else
                            {
                                // Use only MasterPage elements
                                panelElements = masterPageItems;
                            }
                        }

                        panelElements = panelElements.OrderBy(i => i.Rank);

                        int panelCount = panelElements.Count();
                        if (!pageLoaded && panel.Id == primaryPanelId && panelCount == 0)
                        {
                            LoadPageDesign(panelToUse, inDesignMode);
                        }
                        else
                        {
                            // Load PageItems
                            for (int i = 0; i < panelCount; i++)
                            {
                                var item = panelElements.ElementAt(i);
                                if (!pageLoaded && panel.Id == primaryPanelId && (page.Rank <= item.Rank || (/* if last loop */ page.Rank > item.Rank && panelCount == i + 1)))
                                {
                                    if (page.Rank <= item.Rank)
                                    {
                                        // Page is found first
                                        LoadPageDesign(panelToUse, inDesignMode);
                                        LoadElementDesign(panelToUse, item, inDesignMode);
                                    }
                                    else
                                    {
                                        // Last loop and element if found first
                                        LoadElementDesign(panelToUse, item, inDesignMode);
                                        LoadPageDesign(panelToUse, inDesignMode);
                                    }
                                }
                                else
                                {
                                    LoadElementDesign(panelToUse, item, inDesignMode);
                                }
                            }
                        }

                        // Need to check, otherwise it will add itself to itself
                        if (inDesignMode)
                            panelControl.Controls.Add(panelToUse);
                    }
                }

                // Page designer and dragdrop style
                resources.AppendLine(WebHelper.CreateCssLinkText(WSConstants.PageDesignerCss));
                resources.AppendLine(WebHelper.CreateCssLinkText(WSConstants.DragDropCss));
                resources.AppendLine(WebHelper.CreateJavaScriptLinkText(WSConstants.DragDropJs));

                #endregion
            }

            #region Load Resources

            var pageHeader = WHelper.LoadResources(page);
            if (pageHeader != null)
                resources.AppendLine(pageHeader.ToString());

            // Part Resources
            foreach (int id in partResources)
            {
                pageHeader = WHelper.LoadResources(WebObjects.WebPart, id);
                if (pageHeader != null)
                    resources.AppendLine(pageHeader.ToString());
            }

            // Part Control Resources
            foreach (int id in controlResources)
            {
                pageHeader = WHelper.LoadResources(WebObjects.WebPartControl, id);
                if (pageHeader != null)
                    resources.AppendLine(pageHeader.ToString());
            }

            // Part Control Template Resources
            foreach (int id in templateResources)
            {
                pageHeader = WHelper.LoadResources(WebObjects.WebPartControlTemplate, id);
                if (pageHeader != null)
                    resources.AppendLine(pageHeader.ToString());
            }

            #endregion

            // Setup Page Title
            //Header.Title = page.BuildTitle(pageTitle);

            #region Url Fix

            // Change form's action to use virtual URL (.NET 3.5 SP1 onwards only)
            if (!Request.RawUrl.ToLower().Equals("/default.aspx?"))
                this.Form.Action = Request.RawUrl;

            #endregion

            SetValue("Title", page.BuildTitle(pageTitle));
            SetValue("Resources", resources);
            SetValue("IsHomePage", DataHelper.ToString(site.HomePageId == page.Id, BoolStrings.ZeroOne));

            SetValue(WebColumns.UserId, session.UserId);
            //SetValue(WebColumns.PageId, page.Id);
            //SetValue(WebColumns.SiteId, page.SiteId);
            //SetValue("PageName", page.Name);
            SetValue("Page", page);
            SetValue("Site", site);

            if (this.Header != null)
                this.Header.DataBind();

            PerformanceLog.AddLogRunning("Page Init Completed", stopWatch, pageId);
        }

        private Control CreateElementControl(IPageElement item, Control containerRef, WSession session, bool fromStaticLoader = false)
        {
            Control controlToLoad = null;
            try
            {
                var partTemplate = item.PartControlTemplate;
                var partControl = partTemplate.PartControl;
                var part = partControl.Part;

                if (!part.IsActive)
                    return null;

                string openRequest = query.Get(WConstants.Open);

                if (item.OBJECT_ID == WebObjects.WebPage)
                {
                    var debugTemplateId = query.GetId(WConstants.DebugTemplateId);

                    // Check if there is a request to load another WebPartControl
                    if (!string.IsNullOrEmpty(openRequest))
                    {
                        var tempTemplate = part.GetTemplateFromIdentity(partTemplate.Id, openRequest);
                        if (tempTemplate != null)
                        {
                            // The requested Template was found
                            partTemplate = tempTemplate;
                            partControl = tempTemplate.PartControl;
                        }
                    }
                    else if (debugTemplateId > 0 && session.IsAdministrator)
                    {
                        var debugTemplate = WebPartControlTemplate.Get(debugTemplateId);
                        if (debugTemplate != null)
                        {
                            partTemplate = debugTemplate;
                            partControl = debugTemplate.PartControl;
                            part = partControl.Part;
                        }
                    }
                }

                // Enlist the partControl for later rendering of resources
                if (!fromStaticLoader)
                {
                    partResources.Add(partControl.PartId);
                    controlResources.Add(partControl.Id);
                    templateResources.Add(partTemplate.Id);
                }

                string templatePath = item.UsePartTemplatePath == WebBoolean.Yes && !string.IsNullOrEmpty(partTemplate.Path) ? partTemplate.GetRenderPath() :
                    string.Format(WSConstants.PART_PATH_FORMAT, item.MasterPage.Template.Identity, part.Identity, partControl.Identity, partTemplate.FileName);

                controlToLoad = LoadControl(templatePath);
                controlToLoad.ID = WContext.GenerateControlId(item.OBJECT_ID, item.Id);

                var controlProvider = controlToLoad as IObjectValueProvider;
                if (controlProvider != null)
                {
                    controlProvider.Values = Values;
                    //controlProvider.Sets = Sets;
                }

                if (containerRef != null)
                {
                    // FormatString
                    string formatString = WebParameter.GetStringValue(item.OBJECT_ID, item.Id, WConstants.FormatStringKey);
                    if (!string.IsNullOrEmpty(formatString))
                    {
                        if (formatString.IndexOf(Substituter.DefaultContentToken) == -1)
                            formatString += Substituter.DefaultContentToken;

                        formatString = formatString.Replace(Substituter.DefaultContentToken, WConstants.ReplacerString);

                        // Substituter
                        var context = WContext.GetInstance(controlToLoad); // new WContext(controlToLoad); // TODO: Create one object in the page
                        formatString = Substituter.Substitute(formatString, context.ValueProvider);

                        Literal paddingLiteral;
                        var paddingArray = formatString.Split(WConstants.ReplacerChar);
                        int padLen = paddingArray.Length;
                        for (int i = 0; i < paddingArray.Length; i++)
                        {
                            string padding = paddingArray[i];
                            paddingLiteral = new Literal();
                            paddingLiteral.Text = padding;

                            containerRef.Controls.Add(paddingLiteral);
                            if (padLen == 1 || i != padLen - 1)
                                containerRef.Controls.Add(controlToLoad);
                        }
                    }
                    else
                    {
                        containerRef.Controls.Add(controlToLoad);
                    }

                    if (item.OBJECT_ID == WebObjects.WebPage)
                    {
                        var pagePart = controlToLoad as IWebPartControl;
                        if (pagePart != null)
                        {
                            var title = pagePart.PageTitleOverride;
                            if (title != null)
                                pageTitle = title;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Failed loading control
                var lblError = new Label();
                lblError.Text = string.Format(WSConstants.LoadingErrorMsg, ex);
                lblError.ForeColor = Color.Red;
                controlToLoad = lblError;
                if (containerRef != null)
                    containerRef.Controls.Add(controlToLoad);
            }

            return controlToLoad;
        }

        private void LoadStaticPage(WPage page, WSession session)
        {
            var container = LoadControl(page.PartControlTemplate.Path);
            container.ID = WContext.GenerateControlId(page.OBJECT_ID, page.Id);

            // Load PageElements
            var items = from el in page.Elements
                        where el.IsActive
                        select (IPageElement)el;

            if (items.Count() > 0)
            {
                var partTemplate = page.PartControlTemplate;
                var panels = partTemplate.Panels;
                foreach (var panel in panels)
                {
                    var panelControl = container.FindControl(panel.PanelName);
                    if (panelControl != null)
                    {
                        var panelElements = from i in items
                                                 .Where(element => element.TemplatePanelId == panel.Id)
                                            select (IPageElement)i;

                        panelElements = panelElements.OrderBy(i => i.Rank);
                        int panelCount = panelElements.Count();
                        // Load PageItems
                        for (int i = 0; i < panelCount; i++)
                        {
                            var item = panelElements.ElementAt(i);
                            CreateElementControl(item, panelControl, session, true);
                        }
                    }
                }
            }

            this.Controls.Add(container);
            RenderToolbar(page, session);
        }

        private void RenderToolbar(WPage page, WSession session) /*, WebMasterPage masterPage)*/
        {
            if (session.UserId > 0)
            {
                // Log Activity
                WSession.UserSessions.Update(session.UserId, page.Id);
                DesignModeController modeCtrl = null;
                if (this.Header != null && session.IsSiteManager) // || WebGlobalPolicy.IsUserAdded(GlobalPolicies.WebSiteManagement)))
                {
                    var identity = page.Site.Identity;
                    if (identity != WConstants.CentralID || (identity == WConstants.CentralID && WConfig.EnableInlineEditor))
                    {
                        modeCtrl = LoadControl(WSConstants.DesignModeControlPath) as DesignModeController;
                        modeCtrl.ID = "modeCtrl";
                        modeCtrl.ClientIDMode = ClientIDMode.Static;
                        this.Form.Controls.Add(modeCtrl);

                        // Check is user has ManageContent permission for Page or MasterPage
                        #region Commented Code
                        /*
                        var pageContentMgmtPermitted = page.IsUserMgmtPermitted(Permissions.ManageContent);
                        var masterPageContentMgmtPermitted = masterPage == null ? false : masterPage.IsUserMgmtPermitted(Permissions.ManageContent);

                        if (pageContentMgmtPermitted || masterPageContentMgmtPermitted)
                        {
                            if (pageContentMgmtPermitted && masterPageContentMgmtPermitted)
                            {
                                // Has all permissions
                                modeCtrl.BindPanels(panels);

                                if (!inRenderMode)
                                    designerPanels = panels;
                            }
                            else
                            {
                                var panel = template.PrimaryPanel;
                                if (pageContentMgmtPermitted)
                                {
                                    // Add only default panel
                                    var items = new List<WebTemplatePanel>();
                                    items.Add(panel);

                                    modeCtrl.BindPanels(items);

                                    if (!inRenderMode)
                                        designerPanels = items;
                                }
                                else
                                {
                                    // Add only MasterPage panels
                                    var items = panels.Where(i => i.Id != panel.Id);
                                    modeCtrl.BindPanels(items);

                                    if (!inRenderMode)
                                        designerPanels = items;
                                }
                            }

                            modeCtrl.ModeChangerVisible = true;
                        }
                        else
                        {
                            if (!inRenderMode)
                                designerPanels = new List<WebTemplatePanel>();

                            modeCtrl.ModeChangerVisible = false;
                        }
                        */
                        #endregion

                        modeCtrl.ModeChangerVisible = false;
                    }
                }
            }
        }
    }
}