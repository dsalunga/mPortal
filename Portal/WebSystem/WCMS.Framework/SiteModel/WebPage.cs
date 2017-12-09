using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework.Core.Manager;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public static class PageTypes
    {
        public const int Dynamic = 0;
        public const int Static = 1;
    }

    public class WPage : PageElementBase, INamedValueProvider, ISelfManager
    {
        private static IWebPageProvider _manager;

        static WPage()
        {
            _manager = WebObject.ResolveManager<WPage, IWebPageProvider>(WebObject.ResolveProvider<WPage, IWebPageProvider>());
        }

        public WPage()
        {
            Id = -1;
            SiteId = -1;
            Rank = 1;
            ParentId = -1;
            MasterPageId = -1;
            PartControlTemplateId = -1;
            SkinId = -1;
            ThemeId = -1;

            PageType = 0;
            UsePartTemplatePath = 1;

            Title = string.Empty;
        }

        #region Properties

        public override int OBJECT_ID { get { return WebObjects.WebPage; } }

        [ObjectColumn]
        public int SiteId { get; set; }

        [ObjectColumn]
        public string Identity { get; set; }

        [ObjectColumn]
        public int ParentId { get; set; }

        [ObjectColumn]
        public string Title { get; set; }

        [ObjectColumn]
        public override int MasterPageId { get; set; }

        [ObjectColumn]
        public int PageType { get; set; }

        [ObjectColumn]
        public int ThemeId { get; set; }

        [ObjectColumn]
        public int SkinId { get; set; }

        public WebSkin Skin { get { return WebSkin.Provider.Get(SkinId); } }
        public WebTheme Theme { get { return WebTheme.Provider.Get(ThemeId); } }

        public string EvaluatedTitle
        {
            get
            {
                if (!string.IsNullOrEmpty(Title))
                    return Title;

                return Name;
            }
        }

        private WSite _site;
        public override WSite Site
        {
            get
            {
                if (_site == null)
                    _site = WSite.Get(SiteId);

                return _site;
            }
        }

        public override WPage Page { get { return this; } }
        public static IWebPageProvider Provider { get { return _manager; } }

        public WPage Parent
        {
            get { return (ParentId > 0) ? _manager.Get(ParentId) : null; }
        }

        public string GetNestedParameterValue(string name)
        {
            var value = GetParameterValue(name, null);
            if (string.IsNullOrEmpty(value))
            {
                var parent = Parent;
                if (parent != null)
                    return parent.GetNestedParameterValue(name);

                return Site.GetNestedParameterValue(name);
            }

            return value;
        }

        public override WebMasterPage MasterPage
        {
            get
            {
                if (MasterPageId > 0)
                {
                    return WebMasterPage.Get(MasterPageId);
                }
                else if (MasterPageId == MasterPageOption.ParentPage)
                {
                    if (Parent != null)
                        return Parent.MasterPage;
                    else
                        return Site.DefaultMasterPage;
                }
                else if (MasterPageId == MasterPageOption.WebSite && Site.DefaultMasterPageId > 0)
                {
                    // Get the Site's default Master Page
                    return Site.DefaultMasterPage;
                }

                return null;
            }
        }

        public IEnumerable<WPage> Children { get { return GetListByParent(this.Id); } }
        public bool HasChildren { get { return Children.Count() > 0; } }

        public override WebTemplatePanel Panel
        {
            get
            {
                var masterPage = MasterPage;
                if (masterPage != null)
                    return masterPage.Template.PrimaryPanel;

                return null;
            }
        }


        //public override WebObjectContent ObjectContent
        //{
        //    get { return WebObjectContent.GetByObjectId(WebObjects.WebPage, Id); }
        //}

        public IEnumerable<WebPagePanel> Panels { get { return WebPagePanel.GetList(Id); } }
        public IEnumerable<WebPageElement> Elements { get { return WebPageElement.GetList(Id, WebObjects.WebPage); } }

        public int GetEvalTypeId()
        {
            var partTemplate = PartControlTemplate;
            return PageType == PageTypes.Static || partTemplate.IsStandalone ? PageTypes.Static : PageTypes.Dynamic;
        }

        public int GetEvalTemplateEngineId()
        {
            return PartControlTemplate.TemplateEngineId;
        }

        public override int TemplatePanelId
        {
            get
            {
                var panel = Panel;
                if (panel != null)
                    return panel.Id;

                return -1;
            }
            set { }
        }

        public new bool IsActive { get { return Active == 1; } }

        #endregion

        #region IPublicSecurable Members

        /// <summary>
        /// Returns true when allowed, false when not
        /// </summary>
        /// <returns>Should return a Permission int value?</returns>
        public override int GetPublicAccess(WSession session)
        {
            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Inherit))
            {
                var parent = Parent;
                if (parent != null)
                    return parent.GetPublicAccess(session);

                return Site.GetPublicAccess(session);
            }
            else
            {
                return GetPublicAccessPartial(session);
            }
        }

        public override int GetPublicAccess(int userId, string ip = "")
        {
            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Inherit))
            {
                var parent = Parent;
                if (parent != null)
                    return parent.GetPublicAccess(userId, ip);

                return Site.GetPublicAccess(userId, ip);
            }
            else
            {
                return GetPublicAccessPartial(userId, ip);
            }
        }

        public override bool IsUserMgmtPermitted(int permissionId)
        {
            if (WSession.Current.IsAdministrator)
                return true;

            if (ManagementAccess == WebMgmtAccess.Inherit)
            {
                WPage parent = Parent;
                if (parent != null)
                    return parent.IsUserMgmtPermitted(permissionId);

                return Site.IsUserMgmtPermitted(permissionId);
            }
            else
            {
                return IsUserPermitted(permissionId, 0);
            }
        }

        #endregion

        #region Instance Methods

        public string BuildTitle()
        {
            return BuildTitle(string.Empty);
        }

        public string BuildTitle(string partPageTitle)
        {
            string pageTitleFormat = Site.PageTitleFormat;

            string pageEvalTitle = EvaluatedTitle;
            string siteEvalTitle = Site.EvaluatedTitle;
            string title = string.Empty;

            if (string.IsNullOrEmpty(pageTitleFormat))
            {
                // No given format
                if (!string.IsNullOrEmpty(partPageTitle))
                {
                    var pageTitle = !string.IsNullOrEmpty(Title) ? Title : !string.IsNullOrEmpty(Site.Title) ? Site.Title : pageEvalTitle;
                    title = string.IsNullOrEmpty(pageTitle) ? partPageTitle : string.Format("{0} - {1}", partPageTitle, pageTitle);
                }
                else if (!string.IsNullOrEmpty(Title))
                    title = Title;
                else if (!string.IsNullOrEmpty(Site.Title))
                    title = string.IsNullOrEmpty(pageEvalTitle) ? Site.Title : string.Format("{0} - {1}", pageEvalTitle, siteEvalTitle);
                else // Check site name?
                    title = pageEvalTitle;
            }
            else
            {
                // Use the defined format
                title = pageTitleFormat;
                if (!string.IsNullOrEmpty(partPageTitle))
                    title = title.Replace("$(Page:Title)", partPageTitle);
                else
                    title = title.Replace("$(Page:Title)", pageEvalTitle);

                title = title.Replace("$(Site:Title)", siteEvalTitle);
            }

            return title;
        }

        public IEnumerable<WPage> GetListByParent(int parentId)
        {
            return from p in GetList(this.SiteId)
                   where p.ParentId == this.Id
                   select p;
        }

        public string BuildRelativeUrl()
        {
            return WebHelper.CombineAddress(Site.BuildRelativeUrl(), WebRewriter.BuildUrl(this));
        }

        public string BuildAbsoluteUrl()
        {
            return WebHelper.CombineAddress(Site.BuildAbsoluteUrl(), WebRewriter.BuildUrl(this));
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return Delete(false);
        }

        public bool Delete(bool recursive)
        {
            if (recursive)
            {
                var children = this.Children;
                if (children.Count() > 0)
                    for (int i = children.Count() - 1; i >= 0; i--)
                        children.ElementAt(i).Delete(true);
            }

            this.DeleteRelatedObjects();
            this.DeleteSecurityObjects();

            var elements = this.Elements;
            for (int i = elements.Count() - 1; i >= 0; i--)
                elements.ElementAt(i).Delete();

            var panels = this.Panels;
            for (int i = panels.Count() - 1; i >= 0; i--)
                panels.ElementAt(i).Delete();

            return Delete(Id);
        }

        #endregion

        #region Static Methods

        public static IEnumerable<WPage> FilterPermitted(int siteId)
        {
            return FilterPermitted(siteId, -2);
        }

        public static IEnumerable<WPage> FilterPermitted(int siteId, int parentId)
        {
            IEnumerable<WPage> pages = WPage.GetList(siteId, parentId);

            if (!WSession.Current.IsAdministrator)
            {
                var site = WSite.Get(siteId);
                if (!site.IsUserMgmtPermitted(Permissions.ManageContent))
                {
                    // Check WebPages permissions
                    pages = (from page in pages
                             where page.IsUserMgmtPermitted(Permissions.ManageContent) //.IsCurrentUserAdded(page)
                             select page);
                    //if (securityPages.Count > 0)
                    //    pages = securityPages;
                }
            }

            return pages;
        }

        //public static List<WebPage> FilterPermittedWithChildren(List<WebPage> pages, int parentId)
        //{
        //    List<WebPage> permitted = new List<WebPage>();
        //    List<WebPage> notPermitted = new List<WebPage>();

        //    foreach (var page in pages)
        //    {
        //        if (page.ParentId == parentId)
        //        {
        //            if (page.IsUserPermitted(Permissions.ManageContent))
        //                permitted.Add(page);
        //            else
        //                notPermitted.Add(page);
        //        }
        //    }

        //    // If there are non-permitted sites (ideally, those without permissions. 
        //    // Should be implemented once deny permission is available)
        //    if (notPermitted.Count > 0)
        //    {
        //        // Base the permission on page permission
        //        var pagePermissions = WebObjectSecurity.Provider.GetList(WebObjects.WebPage, -1, WebObjects.WebUser, WSession.Current.UserId);

        //        foreach (var site in notPermitted)
        //        {
        //            foreach (var pagePermission in pagePermissions)
        //            {
        //                // Check a if permitted if it has a permitted page
        //                WebPage page = WebPage.Get(pagePermission.RecordId);
        //                if (page != null && page.SiteId == site.Id && page.IsUserPermitted(Permissions.ManageContent))
        //                {
        //                    permitted.Add(site);
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    return permitted;
        //}

        /// <summary>
        /// Tries to resolve the path to a WebPage. Path should not contain the site name.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static WPage SelectNode(int siteId, string path)
        {
            WPage parent = null;
            string[] nodeNames = path.Trim(new char[] { ' ', '/' }).Split('/');
            for (int i = 0; i < nodeNames.Length; i++)
            {
                string nodeName = nodeNames[i].Trim();
                if (string.IsNullOrEmpty(nodeName))
                    continue;
                parent = WPage.Provider.Get(siteId, parent == null ? -1 : parent.Id, nodeName);
            }
            return parent;
        }

        public static WPage Resolve(string pageUrl)
        {
            return WebRewriter.ResolvePage(pageUrl);
        }

        public static IEnumerable<WPage> GetList(int siteId, int parentId)
        {
            return _manager.GetList(siteId, parentId);
        }

        public static int GetCount(int siteId)
        {
            return _manager.GetCount(siteId);
        }

        public static int GetMaxRank(int siteId)
        {
            return _manager.GetMaxRank(siteId);
        }

        public static IEnumerable<WPage> GetList(int siteId)
        {
            return _manager.GetList(siteId);
        }

        public static WPage Get(int pageId)
        {
            if (pageId < 1)
                return null;

            return _manager.Get(pageId);
        }

        public static bool Delete(int pageId)
        {
            return _manager.Delete(pageId);
        }

        #endregion

        #region INamedValueProvider Members

        public string GetValue(string key)
        {
            switch (key)
            {
                case WebPageKeys.Id:
                    return Id.ToString();

                case WebPageKeys.Name:
                    return this.Name;

                case WebPageKeys.Url:
                case WebPageKeys.AbsoluteUrl:
                    return BuildAbsoluteUrl();

                case WebPageKeys.RelativeUrl:
                    return BuildRelativeUrl();

                case WebPageKeys.Identity:
                    return Identity;

                case WebPageKeys.Active:
                    return Active.ToString();

                case WebPageKeys.Rank:
                    return Rank.ToString();

                case WebPageKeys.Title:
                    return BuildTitle();
            }

            return GetParameterValue(key);
        }

        public bool ContainsKey(string key)
        {
            switch (key)
            {
                case WebPageKeys.Id:
                case WebPageKeys.Name:
                case WebPageKeys.Url:
                case WebPageKeys.AbsoluteUrl:
                case WebPageKeys.RelativeUrl:
                case WebPageKeys.Identity:
                case WebPageKeys.Active:
                case WebPageKeys.Rank:
                case WebPageKeys.Title:
                    return true;
            }

            return false;
        }

        public string this[string key]
        {
            get
            {
                return GetValue(key);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
