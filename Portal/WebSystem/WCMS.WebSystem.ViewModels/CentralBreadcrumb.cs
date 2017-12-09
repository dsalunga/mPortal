using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem
{
    public class CentralBreadcrumb
    {
        private const string ContainerFormat = "<ul class=\"breadcrumb\">{0}</ul>";
        private const string LinkAppendFormat = "<li><a href=\"{0}\">{1}</a></li>";
        private const string LinkAppendFormatTargetTop = "<li><a href=\"{0}\" target=\"_top\">{1}</a></li>";

        private const string LinkStaticHome = "<li><a href='/'>Home</a></li>";
        private const string LinkStaticFormat = "<li>{0}</li>";
        private const string LinkStaticFormat2 = "<li>{0}</li><li>{1}</li>";

        private WQuery _query;

        public CentralBreadcrumb(WQuery query)
        {
            _query = query;
        }

        public static string Render(WQuery query)
        {
            var breadcrumb = new CentralBreadcrumb(query);
            return breadcrumb.Render();
        }

        public string Render()
        {
            var query = _query;
            var builder = new StringBuilder();
            var basePath = query.BasePath; //.ToLower();
            int siteId = query.GetId(WebColumns.SiteId);
            //string basePathLowered = basePath.ToLower();

            query.Remove(WConstants.Load);

            builder.Append(LinkStaticHome);
            builder.AppendFormat(LinkAppendFormatTargetTop, siteId > 0 ? WQuery.BuildQuery(CentralPages.CentrlHome, WebColumns.SiteId, siteId) : CentralPages.CentrlHome, "Dashboard");

            if (basePath.StartsWith("/Central/", StringComparison.InvariantCultureIgnoreCase) || basePath.StartsWith(CentralPages.LoaderMain, StringComparison.InvariantCultureIgnoreCase))
                GeneratePath(query, builder, basePath);
            else if (basePath.EndsWith("/Setup.aspx", StringComparison.InvariantCultureIgnoreCase))
                builder.AppendFormat(LinkStaticFormat2, "Tools", "Online Setup");

            return string.Format(ContainerFormat, builder);
        }

        private void GeneratePath(WQuery query, StringBuilder builder, string basePath)
        {
            //string path = Path.GetFileName(basePath);

            #region Switch

            switch (basePath)
            {
                #region Tools

                case CentralPages.WebRegistry:
                case CentralPages.WebRegistryEntry:
                    builder.Append(GetWebRegistry(query));
                    break;

                case CentralPages.QueryAnalyzer:
                    builder.Append(GetQueryAnalyzer(query));
                    break;

                case CentralPages.WebObjectManager:
                    builder.Append(GetObjectManager(query));
                    break;

                case CentralPages.WebResourceManager:
                    builder.Append(GetResourceManager(query));
                    break;

                case CentralPages.FileManager:
                    builder.Append(GetFileManager(query));
                    break;

                case CentralPages.WebParameterSets:
                case CentralPages.WebParameterSetHome:
                case CentralPages.WebParameterSet:
                    builder.Append(GetWebParameterSet(query));
                    break;

                #endregion

                #region Security

                case CentralPages.WebRoles:
                    builder.Append(GetWebRoles(query));
                    break;

                case CentralPages.WebUsers:
                    builder.Append(GetWebUsers(query));
                    break;

                case CentralPages.WebGroups:
                case CentralPages.WebGroupHome:
                case CentralPages.WebGroupUsers:
                case CentralPages.SubscriptionManager:
                    builder.Append(GetWebGroups(query));
                    break;

                case CentralPages.WebPermissions:
                    builder.Append(GetWebPermissions(query));
                    break;

                case CentralPages.UserProfile:
                case CentralPages.WebUserHome:
                case CentralPages.WebUserGroups:
                case CentralPages.WebAddresses:
                    builder.Append(GetWebUserHome(query));
                    break;

                #endregion

                #region Web Sites

                case CentralPages.WebSites:
                    builder.Append(GetWebSites(query));
                    break;

                case CentralPages.WebSite:
                    builder.Append(GetWebSite(query));
                    break;

                case CentralPages.WebSiteHome:
                case CentralPages.WebIdentities:
                case CentralPages.WebIdentity:
                    builder.Append(GetWebSiteHome(query));
                    break;

                case CentralPages.WebPages:
                    builder.Append(GetWebPages(query));
                    break;

                case CentralPages.WebPage:
                    builder.Append(GetWebPage(query));
                    break;

                case CentralPages.WebPageHome:
                case CentralPages.WebChildPages:
                    builder.Append(GetWebPageHome(query));
                    break;

                case CentralPages.WebChildSites:
                    builder.Append(GetChildWebSites(query));
                    break;

                case CentralPages.WebMasterPages:
                    builder.Append(GetWebMasterPages(query));
                    break;

                case CentralPages.WebPagePanels:
                    builder.Append(GetWebPagePanels(query));
                    break;

                case CentralPages.WebMasterPageHome:
                    builder.Append(GetWebMasterPageHome(query));
                    break;

                case CentralPages.WebPageElements:
                    builder.Append(GetWebPageElements(query));
                    break;

                case CentralPages.WebPagePanelHome:
                case CentralPages.WebPagePanel:
                    builder.Append(GetWebPagePanelHome(query));
                    break;

                case CentralPages.WebPageElementHome:
                    builder.Append(GetWebPageElementHome(query));
                    break;

                case CentralPages.LoaderMain: // Loader
                case CentralPages.LoaderMain2:
                case CentralPages.LoaderRazor:
                    builder.Append(GetLoaderMain(query));
                    break;

                case CentralPages.WebPageElement:
                    builder.Append(GetWebPageElement(query));
                    break;

                case CentralPages.WebMasterPage:
                    builder.Append(GetWebMasterPage(query));
                    break;

                #endregion

                #region WebParts

                case CentralPages.WebParts:
                    builder.Append(GetWebParts(query));
                    break;

                case CentralPages.WebPart:
                    builder.Append(GetWebParts(query));
                    builder.Append(GetWebPart(query));
                    break;

                case CentralPages.WebPartHome:
                    builder.Append(GetWebParts(query));
                    builder.Append(GetWebPartHome(query));
                    break;

                case CentralPages.WebPartManagement:
                    builder.Append(GetWebParts(query));
                    builder.Append(GetWebPartManagement(query));
                    break;

                case CentralPages.WebPartControls:
                    builder.Append(GetWebParts(query));
                    //builder.Append(GetWebPartHome(query));
                    builder.Append(GetWebPartManagement(query));
                    builder.Append(GetWebPartControls(query));
                    break;

                case CentralPages.WebPartControlHome:
                    builder.Append(GetWebParts(query));
                    //builder.Append(GetWebPartHome(query));
                    builder.Append(GetWebPartManagement(query));
                    builder.Append(GetWebPartControls(query));
                    builder.Append(GetWebPartControlHome(query));
                    break;

                case CentralPages.WebPartControl:
                    builder.Append(GetWebParts(query));
                    //builder.Append(GetWebPartHome(query));
                    builder.Append(GetWebPartManagement(query));
                    builder.Append(GetWebPartControls(query));
                    builder.Append(GetWebPartControl(query));
                    break;

                case CentralPages.WebPartControlTemplates:
                    builder.Append(GetWebParts(query));
                    //builder.Append(GetWebPartHome(query));
                    builder.Append(GetWebPartManagement(query));
                    builder.Append(GetWebPartControls(query));
                    builder.Append(GetWebPartControlHome(query));
                    builder.Append(GetWebPartControlTemplates(query));
                    break;

                case CentralPages.WebPartControlTemplateHome:
                case CentralPages.WebPartTemplatePanels:
                case CentralPages.WebPartTemplatePanel:
                    builder.Append(GetWebParts(query));
                    //builder.Append(GetWebPartHome(query));
                    builder.Append(GetWebPartManagement(query));
                    builder.Append(GetWebPartControls(query));
                    builder.Append(GetWebPartControlHome(query));
                    builder.Append(GetWebPartControlTemplates(query));
                    builder.Append(GetWebPartControlTemplateHome(query));
                    break;

                case CentralPages.WebPartControlTemplate:
                    builder.Append(GetWebParts(query));
                    //builder.Append(GetWebPartHome(query));
                    builder.Append(GetWebPartManagement(query));
                    builder.Append(GetWebPartControls(query));
                    builder.Append(GetWebPartControlHome(query));
                    builder.Append(GetWebPartControlTemplates(query));
                    builder.Append(GetWebPartControlTemplate(query));
                    break;

                case CentralPages.WebPartConfig:
                    builder.Append(GetWebParts(query));
                    //builder.Append(GetWebPartHome(query));
                    builder.Append(GetWebPartManagement(query));
                    builder.Append(GetWebPartPage(query, CentralPages.WebPartConfig, "Configuration"));
                    break;

                case CentralPages.WebPartAdmin:
                case CentralPages.WebPartAdminHome:
                case CentralPages.WebPartAdminEntry:
                    builder.Append(GetWebParts(query));
                    //builder.Append(GetWebPartHome(query));
                    builder.Append(GetWebPartManagement(query));
                    builder.Append(GetWebPartPage(query, CentralPages.WebPartAdmin, "Administration"));
                    break;

                #endregion

                #region WebTemplates

                case CentralPages.WebTemplates:
                    builder.Append(GetWebTemplates(query));
                    break;

                case CentralPages.WebTemplate:
                    builder.Append(GetWebTemplate(query));
                    break;

                case CentralPages.WebTemplateHome:
                    builder.Append(GetWebTemplateHome(query));
                    break;

                case CentralPages.WebTemplatePanels:
                    builder.Append(GetWebTemplatePanels(query));
                    break;

                case CentralPages.WebThemes:
                    builder.Append(GetWebThemes(query));
                    break;

                case CentralPages.WebThemeHome:
                    builder.Append(GetWebThemeHome(query));
                    break;

                case CentralPages.WebTheme:
                    builder.Append(GetWebTheme(query));
                    break;

                case CentralPages.WebSkins:
                    builder.Append(GetWebSkins(query));
                    break;

                case CentralPages.WebSkinHome:
                    builder.Append(GetWebSkinHome(query));
                    break;

                case CentralPages.WebSkin:
                    builder.Append(GetWebSkin(query));
                    break;

                case CentralPages.WebTemplatePanel:
                    builder.Append(GetWebTemplatePanel(query));
                    break;

                #endregion

                case CentralPages.WebSecurity:
                case CentralPages.WebParameters:
                case CentralPages.WebParameter:
                case CentralPages.WebResources:
                case CentralPages.WebResource:
                    var keySource = query.GetDecode(ObjectKey.KeySource);
                    if (!string.IsNullOrEmpty(keySource))
                    {
                        //string source = keySource.ToLower();
                        //var sourceFileName = Path.GetFileName(source);

                        if (!keySource.Equals(basePath, StringComparison.InvariantCultureIgnoreCase))
                        {
                            var q = new WQuery(query);
                            q.Remove(ObjectKey.KeySource);
                            q.Remove(ObjectKey.KeyString);

                            GeneratePath(q, builder, keySource);
                        }
                    }

                    break;
            }

            #endregion
        }

        private string GetLoaderMain(WQuery query)
        {
            var sb = new StringBuilder();
            var adminId = query.GetId(WebColumns.PartAdminId);
            if (adminId > 0)
            {
                var q = query.Clone();
                q.Remove(WebColumns.PartAdminId);

                var admin = WebPartAdmin.Get(adminId);
                var item = admin.Part;

                sb.Append(string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebParts), "Apps"));

                q.Set(WebColumns.PartId, admin.PartId);
                sb.Append(string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebPartManagement), item.Name));

                sb.Append(string.Format(LinkAppendFormat, query.BuildQuery(admin.TemplateEngineId == TemplateEngineTypes.Razor ? CentralPages.LoaderRazor : CentralPages.LoaderMain), admin.Name));
            }
            else
            {
                int pageId = query.GetId(WebColumns.PageId);
                int id = query.GetId(WebColumns.PageElementId);

                if (id > 0)
                {
                    sb.Append(GetWebPageElements(query));
                    var item = WebPageElement.Get(id);
                    if (item != null)
                    {
                        if (item.ObjectId == WebObjects.WebMasterPage && pageId > 0)
                        {
                            var q = query.Clone();
                            q.Set(WebColumns.MasterPageId, item.RecordId);
                            sb.Append(GetWebMasterPageHome(q, false));
                        }

                        sb.Append(string.Format(LinkAppendFormat, query.BuildQuery(CentralPages.WebPageElementHome), item.Name));
                    }
                }
                else if (pageId > 0)
                {
                    sb.Append(GetWebPageHome(query));
                }

                sb.Append(string.Format(LinkAppendFormat, query.BuildQuery(), "Settings"));
            }

            return sb.ToString();
        }

        #region WebParts

        private string GetWebParts(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns.PartId);
            q.Remove(WebColumns.PartControlId);
            q.Remove(WebColumns.PartControlTemplateId);

            return string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebParts), "Apps");
        }

        private string GetWebPart(WQuery query)
        {
            int id = query.GetId(WebColumns.PartId);
            if (id > 0)
            {
                var sb = new StringBuilder();
                sb.Append(GetWebPartHome(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebPart), "Edit");

                return sb.ToString();
            }

            return string.Format(LinkAppendFormat, query.BuildQuery(CentralPages.WebPart), "New");
        }

        private string GetWebPartHome(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns._SiteId);
            q.Remove(WebColumns.PartControlId);
            q.Remove(WebColumns.PartControlTemplateId);

            int id = q.GetId(WebColumns.PartId);
            var item = WPart.Get(id);

            return string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebPartHome), item.Name);
        }

        private string GetWebPartManagement(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns._SiteId);
            q.Remove(WebColumns.PartControlId);
            q.Remove(WebColumns.PartControlTemplateId);

            int id = q.GetId(WebColumns.PartId);
            var item = WPart.Get(id);

            return string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebPartManagement), item.Name);
        }

        private string GetWebPartControls(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns.PartControlId);
            q.Remove(WebColumns.PartControlTemplateId);

            return string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebPartControls), "Controls");
        }

        private string GetWebPartControlHome(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns.PartControlTemplateId);

            int id = q.GetId(WebColumns.PartControlId);
            var item = WebPartControl.Get(id);

            return string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebPartControlHome), item.Name);
        }

        private string GetWebPartControl(WQuery query)
        {
            int id = query.GetId(WebColumns.PartControlId);
            if (id > 0)
            {
                var sb = new StringBuilder();
                sb.Append(GetWebPartControlHome(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebPartControl), "Edit");

                return sb.ToString();
            }
            else
            {
                return string.Format(LinkAppendFormat, query.BuildQuery(CentralPages.WebPartControl), "New");
            }
        }

        private string GetWebPartControlTemplates(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns.PartControlTemplateId);

            return string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebPartControlTemplates), "Templates");
        }

        private string GetWebPartControlTemplateHome(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns._SiteId);

            int id = q.GetId(WebColumns.PartControlTemplateId);
            var item = WebPartControlTemplate.Get(id);

            return string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebPartControlTemplateHome), item.Name);
        }

        private string GetWebPartControlTemplate(WQuery query)
        {
            int id = query.GetId(WebColumns.PartControlTemplateId);
            if (id > 0)
            {
                var sb = new StringBuilder();
                sb.Append(GetWebPartControlTemplateHome(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebPartControlTemplate), "Edit");

                return sb.ToString();
            }
            else
            {
                return string.Format(LinkAppendFormat, query.BuildQuery(CentralPages.WebPartControlTemplate), "New");
            }
        }

        private string GetWebPartPage(WQuery query, string page, string section)
        {
            int id = query.GetId(WebColumns.PartId);
            var item = WPart.Get(id);

            return string.Format(LinkAppendFormat, query.BuildQuery(page), section);
        }

        #endregion

        #region Tools

        private string GetTools(WQuery query)
        {
            return string.Format(LinkStaticFormat, "Tools");
        }

        private string GetWebRegistry(WQuery query)
        {
            var sb = new StringBuilder();

            var q = query.Clone();
            q.Remove(WebColumns.ParentId);

            sb.Append(GetTools(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebRegistry), "Registry");

            return sb.ToString();
        }

        private string GetWebParameterSet(WQuery query)
        {
            var sb = new StringBuilder();

            var q = query.Clone();
            q.Remove(WebColumns.ParameterSetId);

            sb.Append(GetTools(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebParameterSets), "Parameter Sets");

            var parameterSetId = query.GetId(WebColumns.ParameterSetId);
            WebParameterSet parameterSet = null;
            if (parameterSetId > 0 && (parameterSet = WebParameterSet.Provider.Get(parameterSetId)) != null)
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebParameterSetHome), parameterSet.Name);

            return sb.ToString();
        }

        private string GetQueryAnalyzer(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();

            sb.Append(GetTools(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.QueryAnalyzer), "Query Analyzer");

            return sb.ToString();
        }

        private string GetObjectManager(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();

            sb.Append(GetTools(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebObjectManager), "Object Manager");

            return sb.ToString();
        }

        private string GetResourceManager(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();

            sb.Append(GetTools(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebResourceManager), "Resource Manager");

            return sb.ToString();
        }

        private string GetFileManager(WQuery query)
        {
            var sb = new StringBuilder();

            var q = query.Clone();

            sb.Append(GetTools(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.FileManager), "File Manager");

            return sb.ToString();
        }

        #endregion

        #region Security

        private string GetSecurity(WQuery query)
        {
            return string.Format(LinkStaticFormat, "Security");
        }

        private string GetWebUsers(WQuery query)
        {
            var sb = new StringBuilder();

            var q = query.Clone();
            q.Remove(WebColumns.UserId);

            sb.Append(GetSecurity(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebUsers), "Users");

            return sb.ToString();
        }

        private string GetWebRoles(WQuery query)
        {
            var sb = new StringBuilder();

            var q = query.Clone();
            q.Remove(WebColumns.RoleId);

            sb.Append(GetSecurity(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebRoles), "Roles");

            return sb.ToString();
        }

        private string GetWebGroups(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();
            q.Remove(WebColumns.GroupId);

            sb.Append(GetSecurity(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebGroups), "Groups");

            return sb.ToString();
        }

        private string GetWebGroupHome(WQuery query)
        {
            var sb = new StringBuilder();

            Action<int> BuildGroupRecursive = null;

            BuildGroupRecursive = (id) =>
            {
                WebGroup group = null;
                if (id > 0 && (group = WebGroup.Get(id)) != null)
                {
                    sb.Insert(0, string.Format(LinkAppendFormat, query.BuildQuery(CentralPages.WebGroupHome), group.Name));

                    BuildGroupRecursive(group.ParentId);
                }
            };

            BuildGroupRecursive(query.GetId(WebColumns.GroupId));

            sb.Insert(0, GetWebGroups(query));

            return sb.ToString();
        }

        private string GetWebPermissions(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();
            q.Remove(WebColumns.PermissionId);

            sb.Append(GetSecurity(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebPermissions), "Permissions");

            return sb.ToString();
        }

        private string GetUserProfile(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();

            var id = q.GetId(WebColumns.UserId);
            var item = WebUser.Get(id);

            sb.Append(GetWebUsers(query));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.UserProfile), item.UserName);

            return sb.ToString();
        }

        private string GetWebUserHome(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();

            var id = q.GetId(WebColumns.UserId);
            var item = WebUser.Get(id);

            sb.Append(GetWebUsers(query));

            if (item != null)
                sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebUserHome), item.UserName);

            return sb.ToString();
        }

        #endregion

        #region Web Sites

        private string GetWebPage(WQuery query)
        {
            var sb = new StringBuilder();

            if (query.HasValue(WebColumns.PageId))
                sb.Append(GetWebPageHome(query));
            else
                sb.Append(GetWebPages(query));

            int id = query.GetId(WebColumns._PageId);
            sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebPage), id > 0 ? "Edit" : "New");

            return sb.ToString();
        }

        private string GetWebPageElement(WQuery query)
        {
            var sb = new StringBuilder();

            int id = query.GetId(WebColumns.PageElementId);

            if (id > 0)
                sb.Append(GetWebPageElementHome(query));
            else
                sb.Append(GetWebPageElements(query));

            sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebPageElement), id > 0 ? "Edit" : "New");

            return sb.ToString();
        }

        // WebPage Element Home
        private string GetWebPageElementHome(WQuery query)
        {
            var sb = new StringBuilder();

            int pageId = query.GetId(WebColumns.PageId);
            int masterPageId = query.GetId(WebColumns.MasterPageId);

            if (pageId > 0 || masterPageId > 0)
            {
                sb.Append(GetWebPageElements(query));

                int id = query.GetId(WebColumns.PageElementId);
                if (id > 0)
                {
                    var item = WebPageElement.Get(id);
                    if (item != null)
                    {
                        if (item.ObjectId == WebObjects.WebMasterPage && pageId > 0)
                        {
                            WQuery q = query.Clone();

                            q.Set(WebColumns.MasterPageId, item.RecordId);
                            sb.Append(GetWebMasterPageHome(q, false));
                        }

                        sb.Append(string.Format(LinkAppendFormat, query.BuildQuery(CentralPages.WebPageElementHome), item.Name));
                    }
                }
            }

            return sb.ToString();
        }

        // WebPage Elements
        private string GetWebPageElements(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();
            q.Remove(WebColumns.PageElementId);

            int pageId = q.GetId(WebColumns.PageId);
            if (pageId > 0)
                sb.Append(GetWebPageHome(q));
            else
                sb.Append(GetWebMasterPageHome(q));

            //int templatePanelId = q.GetId(WebColumns.TemplatePanelId);
            //if (templatePanelId > 0)
            sb.Append(string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebPageElements), "Elements"));

            return sb.ToString();
        }

        private string GetWebSites(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns.SiteId);
            q.Remove(WebColumns._SiteId);

            return string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebSites), "Sites");
        }

        private string GetWebSiteHome(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();

            int id = q.GetId(WebColumns.SiteId);
            WSite item = null;
            if (!(id > 0 && (item = WSite.Get(id)) != null))
            {
                int pageId = q.GetId(WebColumns.PageId);
                WPage page = null;
                if (pageId > 0 && (page = WPage.Get(pageId)) != null)
                    item = page.Site;
            }

            q.Remove(WebColumns.PageId);

            if (item != null)
            {
                do
                {
                    q.Set(WebColumns.SiteId, item.Id.ToString());
                    sb.Insert(0, string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebSiteHome), item.Name));
                }
                while ((item = item.Parent) != null);
            }

            sb.Insert(0, GetWebSites(q));
            return sb.ToString();
        }

        private string GetWebSite(WQuery query)
        {
            var sb = new StringBuilder();

            int id = query.GetId(WebColumns.SiteId);
            if (id > 0)
            {
                sb.Append(GetWebSiteHome(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebSite), "Edit");
            }
            else
            {
                sb.Insert(0, GetWebSites(query));
                sb.Append(string.Format(LinkAppendFormat, query.BuildQuery(CentralPages.WebSite), "New"));
            }

            return sb.ToString();
        }

        private string GetWebPages(WQuery query)
        {
            var q = query.Clone();
            var sb = new StringBuilder();
            var item = TryGetSite(q);

            q.Remove(WebColumns.PageId);

            if (item != null)
            {
                q.Set(WebColumns.SiteId, item.Id);
                sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebPages), "Pages");
            }

            sb.Insert(0, GetWebSiteHome(q));

            return sb.ToString();
        }

        private WSite TryGetSite(WQuery q)
        {
            int id = q.GetId(WebColumns.SiteId);
            WSite item = null;
            if (!(id > 0 && (item = WSite.Get(id)) != null))
            {
                int pageId = q.GetId(WebColumns.PageId);
                WPage page = null;
                if (pageId > 0 && (page = WPage.Get(pageId)) != null)
                    item = page.Site;
            }

            return item;
        }

        // WebPage Element Home
        private string GetWebPagePanelHome(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();

            sb.Append(GetWebPagePanels(query));

            int id = query.GetId(WebColumns.TemplatePanelId);
            if (id > 0)
            {
                var item = WebTemplatePanel.Get(id);
                if (item != null)
                    sb.Append(string.Format(LinkAppendFormat, query.BuildQuery(CentralPages.WebPagePanelHome), item.Name));
            }

            return sb.ToString();
        }

        private string GetWebPagePanels(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();
            q.Remove(WebColumns.TemplatePanelId);

            int pageId = q.GetId(WebColumns.PageId);
            if (pageId > 0)
                sb.Append(GetWebPageHome(q));
            else
                sb.Append(GetWebMasterPageHome(q));

            sb.Append(string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebPagePanels), "Panels"));

            return sb.ToString();
        }

        private string GetWebPageHome(WQuery query)
        {
            var sb = new StringBuilder();
            var q = query.Clone();
            int id = q.GetId(WebColumns.PageId);

            q.Remove(WebColumns.TemplatePanelId);

            var item = WPage.Get(id);
            if (item != null)
            {
                do
                {
                    q.Set(WebColumns.PageId, item.Id);
                    q.Set(WebColumns.SiteId, item.SiteId);
                    sb.Insert(0, string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebPageHome), item.Name));
                }
                while ((item = item.Parent) != null);
            }

            sb.Insert(0, GetWebPages(q));
            return sb.ToString();
        }

        private string GetWebMasterPages(WQuery query)
        {
            var sb = new StringBuilder();

            var q = query.Clone();
            q.Remove(WebColumns.MasterPageId);

            sb.Append(GetWebSiteHome(query));

            int siteId = q.GetId(WebColumns.SiteId);
            if (siteId == -1)
            {
                int pageId = q.GetId(WebColumns.PageId);
                if (pageId > 0)
                {
                    var page = WPage.Get(pageId);
                    if (page != null)
                        q.Set(WebColumns.SiteId, page.SiteId);

                    q.Remove(WebColumns.PageId);
                }
            }

            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebMasterPages), "Master Pages");
            return sb.ToString();
        }

        private string GetWebMasterPageHome(WQuery query, bool includeMasterPages = true)
        {
            var sb = new StringBuilder();
            int id = query.GetId(WebColumns.MasterPageId);

            query.Remove(WebColumns.PageElementId);

            if (id > 0)
            {
                var item = WebMasterPage.Get(id);
                if (item != null)
                {
                    if (includeMasterPages)
                    {
                        var q = query.Clone();
                        q.Remove(WebColumns.MasterPageId);

                        sb.Append(GetWebMasterPages(q));
                    }

                    sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebMasterPageHome), item.Name);
                }
            }

            return sb.ToString();
        }

        private string GetWebMasterPage(WQuery query)
        {
            var sb = new StringBuilder();

            int id = query.GetId(WebColumns.MasterPageId);
            if (id > 0)
            {
                sb.Append(GetWebMasterPageHome(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebMasterPage), "Edit");
            }
            else
            {
                sb.Append(GetWebSiteHome(query));
                sb.Append(GetWebMasterPages(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebMasterPage), "New");
            }

            return sb.ToString();
        }

        private string GetChildWebSites(WQuery query)
        {
            var sb = new StringBuilder();
            sb.Insert(0, GetWebSiteHome(query));

            sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebChildSites), "Sites");
            return sb.ToString();
        }

        #endregion

        #region WebTemplates

        private string GetWebTemplatePanel(WQuery query)
        {
            var sb = new StringBuilder();

            sb.Append(GetWebTemplatePanels(query));

            int id = query.GetId(WebColumns.TemplatePanelId);
            if (id > 0)
            {
                var item = WebTemplatePanel.Get(id);
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebTemplatePanel), item.Name);
            }
            else
            {
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebTemplatePanel), "New");
            }

            return sb.ToString();
        }

        private string GetWebTemplateHome(WQuery query)
        {
            int id = query.GetId(WebColumns.TemplateId);
            if (id > 0)
            {
                var sb = new StringBuilder();
                var item = WebTemplate.Get(id);

                sb.Append(GetWebTemplates(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebTemplateHome), item.Name);

                return sb.ToString();
            }

            return string.Empty;
        }

        private string GetWebTemplatePanels(WQuery query)
        {
            var sb = new StringBuilder();
            sb.Append(GetWebTemplateHome(query));
            sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebTemplatePanels), "Panels");

            return sb.ToString();
        }

        private string GetWebThemes(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns.ThemeId);

            return string.Format(LinkAppendFormat, q.BuildQuery(CentralPages.WebThemes), "Themes");
        }

        private string GetWebTemplates(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns.TemplateId);

            var sb = new StringBuilder();
            sb.Append(GetWebThemeHome(q));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebTemplates), "Templates");

            return sb.ToString();
        }

        private string GetWebSkins(WQuery query)
        {
            var q = query.Clone();
            q.Remove(WebColumns.SkinId);

            var sb = new StringBuilder();
            sb.Append(GetWebThemeHome(q));
            sb.AppendFormat(LinkAppendFormat, q.BuildQuery(CentralPages.WebSkins), "Skins");

            return sb.ToString();
        }

        private string GetWebSkinHome(WQuery query)
        {
            int id = query.GetId(WebColumns.SkinId);
            if (id > 0)
            {
                var sb = new StringBuilder();
                var item = WebSkin.Provider.Get(id);

                sb.Append(GetWebSkins(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebSkinHome), item.Name);

                return sb.ToString();
            }

            return string.Empty;
        }

        private string GetWebThemeHome(WQuery query)
        {
            int id = query.GetId(WebColumns.ThemeId);
            if (id > 0)
            {
                var sb = new StringBuilder();
                var item = WebTheme.Provider.Get(id);

                sb.Append(GetWebThemes(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebThemeHome), item.Name);

                return sb.ToString();
            }

            return string.Empty;
        }

        private string GetWebTheme(WQuery query)
        {
            var sb = new StringBuilder();

            int id = query.GetId(WebColumns.ThemeId);
            if (id > 0)
            {
                sb.Append(GetWebThemeHome(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebTheme), "Edit");
            }
            else
            {
                sb.Append(GetWebThemes(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebTheme), "New");
            }

            return sb.ToString();
        }

        private string GetWebSkin(WQuery query)
        {
            var sb = new StringBuilder();

            int id = query.GetId(WebColumns.SkinId);
            if (id > 0)
            {
                sb.Append(GetWebSkinHome(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebSkin), "Edit");
            }
            else
            {
                sb.Append(GetWebSkins(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebSkin), "New");
            }

            return sb.ToString();
        }

        private string GetWebTemplate(WQuery query)
        {
            var sb = new StringBuilder();
            int id = query.GetId(WebColumns.TemplateId);
            if (id > 0)
            {
                sb.Append(GetWebTemplateHome(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebTemplate), "Edit");
            }
            else
            {
                sb.Append(GetWebTemplates(query));
                sb.AppendFormat(LinkAppendFormat, query.BuildQuery(CentralPages.WebTemplate), "New");
            }

            return sb.ToString();
        }

        #endregion
    }
}
