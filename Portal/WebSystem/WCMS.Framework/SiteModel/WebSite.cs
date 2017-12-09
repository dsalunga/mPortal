using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework.Core;
using WCMS.Framework.Core.Manager;

namespace WCMS.Framework
{
    /// <summary>
    /// Summary description for Site
    /// </summary>
    public class WSite : PublicSecurableObject, INamedValueProvider, ISelfManager
    {
        private static IWebSiteProvider _manager;

        static WSite()
        {
            _manager = WebObject.ResolveManager<WSite, IWebSiteProvider>(WebObject.ResolveProvider<WSite, IWebSiteProvider>());
        }

        public WSite()
        {
            Id = -1;
            ParentId = -1;
            HomePageId = -1;
            DefaultMasterPageId = -1;
            PublicAccess = 1;
            ThemeId = -1;
            SkinId = -1;
            PrimaryIdentityId = -1;

            HostName = string.Empty;
            PageTitleFormat = string.Empty;
            BaseAddress = string.Empty;
            Title = string.Empty;
        }

        #region Properties

        public override int OBJECT_ID
        {
            get { return WebObjects.WebSite; }
        }

        [ObjectColumn]
        public int Rank { get; set; }

        [ObjectColumn]
        public int Active { get; set; }

        [ObjectColumn]
        public string Identity { get; set; }

        [ObjectColumn]
        public string Title { get; set; }

        [ObjectColumn]
        public int ParentId { get; set; }

        [ObjectColumn]
        public int HomePageId { get; set; }

        [ObjectColumn]
        public int DefaultMasterPageId { get; set; }

        [ObjectColumn]
        public string HostName { get; set; }

        [ObjectColumn]
        public string LoginPage { get; set; }

        [Obsolete]
        [ObjectColumn]
        public string AccessDeniedPage { get; set; }

        [ObjectColumn]
        public string PageTitleFormat { get; set; }

        [ObjectColumn]
        public string BaseAddress { get; set; }

        [ObjectColumn]
        public int ThemeId { get; set; }

        [ObjectColumn]
        public int SkinId { get; set; }

        [ObjectColumn]
        public int PrimaryIdentityId { get; set; }

        #endregion

        #region IPublicSecurable Members

        /// <summary>
        /// Returns true when allowed, false when not
        /// </summary>
        /// <returns></returns>
        public override int GetPublicAccess(WSession session)
        {
            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Inherit))
            {
                var parent = Parent;
                if (parent != null)
                {
                    return parent.GetPublicAccess(session);
                }

                return PublicAccessCheckResult.Denied;
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
                {
                    return parent.GetPublicAccess(userId, ip);
                }

                return PublicAccessCheckResult.Denied;
            }
            else
            {
                return GetPublicAccessPartial(userId, ip);
            }
        }

        public override bool IsUserMgmtPermitted(int permissionId)
        {
            if (ManagementAccess == WebMgmtAccess.Inherit)
            {
                var parent = Parent;
                if (parent != null)
                    return parent.IsUserMgmtPermitted(permissionId);

                //return WebGlobalPolicy.IsUserPermitted(GlobalPolicies.WebSiteManagement, Permissions.FullControl);
            }

            return IsUserPermitted(permissionId, 0);
        }

        #endregion

        #region Properties

        public bool IsActive
        {
            get { return Active == 1; }
            set { Active = value ? 1 : 0; }
        }

        public static IWebSiteProvider Provider { get { return _manager; } }

        public string AbsLoginUrl
        {
            get
            {
                if (string.IsNullOrEmpty(LoginPage))
                    return WConfig.DefaultLoginPage;

                return LoginPage;
            }
        }

        public string ShortName { get { return GetParameterValue("ShortName"); } }

        public string EvaluatedTitle
        {
            get
            {
                if (!string.IsNullOrEmpty(Title))
                    return Title;

                return Name;
            }
        }

        public IEnumerable<WebMasterPage> MasterPages
        {
            get { return WebMasterPage.GetList(Id); }
        }

        public IEnumerable<WebSiteIdentity> Identities
        {
            get { return WebSiteIdentity.Provider.GetList(Id); }
        }

        public WSite Parent
        {
            get
            {
                if (ParentId < 1)
                    return null;

                return WSite.Get(ParentId);
            }
        }

        /// <summary>
        /// All websites should have a default identity if they have at least 1
        /// </summary>
        /// <returns></returns>
        public WebSiteIdentity GetPrimaryIdentity()
        {
            if (PrimaryIdentityId > 0)
                return WebSiteIdentity.Provider.Get(PrimaryIdentityId);

            return null;
        }

        public IEnumerable<WSite> Children
        {
            get { return WSite.GetList(this.Id); }
        }

        public bool HasChildren
        {
            get { return Children.Count() > 0; }
        }

        public WPage HomePage
        {
            get
            {
                if (HomePageId > 0)
                    return WPage.Get(HomePageId);

                return null;
            }
        }

        public WebMasterPage DefaultMasterPage
        {
            get
            {
                if (DefaultMasterPageId > 0)
                    return WebMasterPage.Get(DefaultMasterPageId);

                return null;
            }
        }

        public IEnumerable<WPage> Pages
        {
            get { return WPage.GetList(Id); }
        }

        public IEnumerable<WPage> RootPages
        {
            get { return WPage.GetList(Id, -1); }
        }

        //public string GetBaseAddress()
        //{
        //    if (string.IsNullOrEmpty(BaseAddress))
        //    {
        //        var identity = GetPrimaryIdentity();
        //        return identity != null ? identity.Build(true) : WConfig.BaseAddress;
        //    }

        //    return BaseAddress;
        //}

        #endregion

        #region Operators

        public static explicit operator WSite(DbDataReader r)
        {
            var item = new WSite();
            item.Id = DataHelper.GetId(r[WebColumns.SiteId].ToString());
            item.Name = r["Name"].ToString();
            item.Rank = Convert.ToInt32(r["Rank"].ToString());
            item.Active = Convert.ToInt32(r["Active"].ToString());
            item.Identity = r["Identity"].ToString();
            item.Title = r["Title"].ToString();
            item.ParentId = DataHelper.GetId(r["ParentId"].ToString());
            item.HomePageId = DataHelper.GetId(r["HomePageId"].ToString());
            item.DefaultMasterPageId = DataHelper.GetId(r["DefaultMasterPageId"].ToString());
            item.HostName = r["HostName"].ToString();
            item.PublicAccess = DataHelper.GetInt32(r["PublicAccess"]);
            item.LoginPage = r["LoginPage"].ToString();
            item.AccessDeniedPage = r["AccessDeniedPage"].ToString();
            item.PageTitleFormat = r["PageTitleFormat"].ToString();
            item.ManagementAccess = DataHelper.GetInt32(r, "ManagementAccess");
            item.BaseAddress = DataHelper.Get(r, "BaseAddress");
            item.ThemeId = DataHelper.GetId(r, WebColumns.ThemeId);
            item.SkinId = DataHelper.GetId(r, WebColumns.SkinId);
            item.PrimaryIdentityId = DataHelper.GetId(r, "PrimaryIdentityId");

            return item;
        }

        #endregion

        #region Methods

        public string BuildRelativeUrl()
        {
            return WebRewriter.BuildUrl(this);
        }

        public string BuildAbsoluteUrl()
        {
            return WebRewriter.BuildUrl(this, true);
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        //public bool IsCurrentUserPermitted(int permissionId)
        //{
        //    if (WSession.Current.IsAdministrator)
        //        return true;

        //    return WebObjectSecurity.IsCurrentUserPermitted(permissionId, this);
        //}

        #endregion

        #region Static Methods

        public static IEnumerable<WSite> FilterPermitted(List<WSite> allSites, int parentId)
        {
            var sites = allSites.Where(site => site.ParentId == parentId);
            // Get the list of WebSites based on permission
            if (!WSession.Current.IsAdministrator)
            {
                // Check WebSite permissions
                sites = from site in sites
                        where WebObjectSecurity.IsUserPermitted(Permissions.ManageContent, site, 0)
                        select site;
            }
            return sites;
        }

        public static bool IsUserSiteAuthor(int siteId)
        {
            var site = WSite.Get(siteId);
            if (site != null)
                return site.IsUserPermitted(Permissions.ManageInstance, 0);

            return false;
        }

        public static List<WSite> FilterPermittedWithChildren(IEnumerable<WSite> sites, int parentId)
        {
            return FilterPermittedWithChildren(sites, parentId, false);
        }

        public static List<WSite> FilterPermittedWithChildren(IEnumerable<WSite> sites, int parentId, bool includeMasterPages)
        {
            var permitted = new List<WSite>();
            var notPermitted = new List<WSite>();

            //List<WebSite> sites = allSites.FindAll(site => site.ParentId == parentId);
            foreach (var site in sites)
            {
                if (site.ParentId == parentId)
                {
                    if (site.IsUserMgmtPermitted(Permissions.ManageContent))
                        permitted.Add(site);
                    else
                        notPermitted.Add(site);
                }
            }

            // If there are non-permitted sites (ideally, those without permissions. 
            // Should be implemented once deny permission is available)
            if (notPermitted.Count > 0)
            {
                // Base the permission on page permission
                var pagePermissions = WebObjectSecurity.GetListByObjectTypeAndUser(WSession.Current.UserId, WebObjects.WebPage, 0);
                IEnumerable<WebObjectSecurity> masterPagePermissions = null;

                if (includeMasterPages)
                    masterPagePermissions = WebObjectSecurity.GetListByObjectTypeAndUser(WSession.Current.UserId, WebObjects.WebMasterPage, 0);

                foreach (var site in notPermitted)
                {
                    bool siteAdded = false;
                    foreach (var pagePermission in pagePermissions)
                    {
                        // Check a if permitted if it has a permitted page
                        var page = WPage.Get(pagePermission.RecordId);
                        if (page != null && page.SiteId == site.Id && pagePermission.IsPermitted(Permissions.ManageContent)) // page.IsUserMgmtPermitted(Permissions.ManageContent))
                        {
                            permitted.Add(site);
                            siteAdded = true;
                            break;
                        }
                    }

                    if (includeMasterPages && !siteAdded)
                    {
                        foreach (var masterPagePermission in masterPagePermissions)
                        {
                            // Check a if permitted if it has a permitted page
                            var masterPage = WebMasterPage.Get(masterPagePermission.RecordId);
                            if (masterPage != null && masterPage.SiteId == site.Id && masterPagePermission.IsPermitted(Permissions.ManageContent)) //masterPage.IsUserMgmtPermitted(Permissions.ManageContent))
                            {
                                permitted.Add(site);
                                break;
                            }
                        }
                    }
                }
            }


            // Get the list of WebSites based on permission
            //if (!WSession.Current.IsAdministrator)
            //{
            //    // Check WebSite permissions
            //    sites = (from site in sites
            //             where WebObjectSecurity.IsCurrentUserPermitted(Permissions.ManageContent, site)
            //             select site).ToList();
            //}

            return permitted;
        }

        public static WSite Get(int id)
        {
            return _manager.Get(id);
        }

        public static bool Delete(int siteId)
        {
            return _manager.Delete(siteId);
        }

        public static int GetCount()
        {
            return _manager.GetCount();
        }

        public static int GetMaxRank()
        {
            return _manager.GetMaxRank();
        }

        public static IEnumerable<WSite> GetList()
        {
            return _manager.GetList();
        }

        public static IEnumerable<WSite> GetList(int parentId)
        {
            return _manager.GetList(parentId);
        }

        #endregion

        #region INamedValueProvider Members

        public string GetValue(string key)
        {
            switch (key)
            {
                case WebSiteKeys.Id:
                    return Id.ToString();

                case WebSiteKeys.Name:
                    return this.Name;

                case WebSiteKeys.Url:
                case WebSiteKeys.AbsoluteUrl:
                    return BuildAbsoluteUrl();

                case WebSiteKeys.RelativeUrl:
                    return BuildRelativeUrl();

                case WebSiteKeys.HomeUrl:
                    return HomePage.BuildAbsoluteUrl();

                case WebSiteKeys.Identity:
                    return Identity;

                case WebSiteKeys.LoginUrl:
                    return LoginPage;

                case WebSiteKeys.Active:
                    return Active.ToString();

                case WebSiteKeys.Rank:
                    return Rank.ToString();

                case WebSiteKeys.Title:
                    return HomePage.BuildTitle();
            }

            return GetParameterValue(key);
        }

        public bool ContainsKey(string key)
        {
            switch (key)
            {
                case WebSiteKeys.Id:
                case WebSiteKeys.Name:
                case WebSiteKeys.Url:
                case WebSiteKeys.AbsoluteUrl:
                case WebSiteKeys.RelativeUrl:
                case WebSiteKeys.HomeUrl:
                case WebSiteKeys.Identity:
                case WebSiteKeys.LoginUrl:
                case WebSiteKeys.Active:
                case WebSiteKeys.Rank:
                case WebSiteKeys.Title:
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

        public string GetNestedParameterValue(string name)
        {
            var value = GetParameterValue(name, null);
            if (string.IsNullOrEmpty(value))
            {
                var parent = Parent;
                if (parent != null)
                    return parent.GetNestedParameterValue(name);

                return null;
            }

            return value;
        }


        public bool Delete()
        {
            this.DeleteRelatedObjects();
            this.DeleteSecurityObjects();

            var masterPages = this.MasterPages;
            for (int i = masterPages.Count() - 1; i >= 0; i--)
                masterPages.ElementAt(i).Delete();

            var identities = this.Identities;
            for (int i = identities.Count() - 1; i >= 0; i--)
                identities.ElementAt(i).Delete();

            return Delete(this.Id);
        }

        public string GetPartConfig(string partIdentity, string key)
        {
            return this.GetParameterValue(string.Format("{0}.{1}", partIdentity, key));
        }

        public WebParameter GetParameter(int partId, string key)
        {
            var part = WPart.Get(partId);
            return this.GetParameter(string.Format("{0}.{1}", part.Identity, key));
        }
    }

    public struct WebSiteKeys
    {
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Url = "Url";
        public const string AbsoluteUrl = "AbsoluteUrl";
        public const string RelativeUrl = "RelativeUrl";
        public const string HomeUrl = "HomeUrl";
        public const string Identity = "Identity";
        public const string LoginUrl = "LoginUrl";
        public const string Active = "Active";
        public const string Rank = "Rank";
        public const string Title = "Title";
    }
}