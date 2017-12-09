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
    public class WebMasterPage : PublicSecurableObject, INamedValueProvider, ISelfManager
    {
        private static IWebMasterPageProvider _manager; // = DataAccess.CreateProvider<IWebMasterPageProvider>();

        static WebMasterPage()
        {
            _manager = WebObject.ResolveManager<WebMasterPage, IWebMasterPageProvider>(WebObject.ResolveProvider<WebMasterPage, IWebMasterPageProvider>());
        }

        public WebMasterPage()
        {
            PublicAccess = 1;
            SiteId = -1;
            TemplateId = -1;
            OwnerPageId = -1;
            SkinId = -1;
            ThemeId = -1;
            ParentId = -1;
        }

        public override int OBJECT_ID { get { return WebObjects.WebMasterPage; } }

        [ObjectColumn]
        public int SiteId { get; set; }

        [ObjectColumn]
        public int TemplateId { get; set; }

        [ObjectColumn]
        public int OwnerPageId { get; set; }

        [ObjectColumn]
        public int SkinId { get; set; }

        [ObjectColumn]
        public int ThemeId { get; set; }

        [ObjectColumn]
        public int ParentId { get; set; }

        public WebSkin Skin { get { return WebSkin.Provider.Get(this.SkinId); } }

        #region IPublicSecurable Members

        public override int GetPublicAccess(WSession session)
        {
            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Inherit))
            {
                var parent = Site;
                if (parent != null)
                    return parent.GetPublicAccess(session);

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
                var parent = Site;
                if (parent != null)
                    return parent.GetPublicAccess(userId, ip);

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
                var parent = Site;
                if (parent != null)
                {
                    return parent.IsUserMgmtPermitted(permissionId);
                }

                return false;
            }
            else
            {
                return IsUserPermitted(permissionId, 0);
            }
        }

        #endregion


        public WSite Site { get { return WSite.Get(SiteId); } }

        public WebTemplate Template { get { return WebTemplate.Get(TemplateId); } }

        public WPage OwnerPage
        {
            get
            {
                if (OwnerPageId > 0)
                    return WPage.Get(OwnerPageId);

                return null;
            }
        }

        public WebMasterPage Parent
        {
            get { return (ParentId > 0) ? _manager.Get(ParentId) : null; }
        }

        public IEnumerable<WebPageElement> Elements
        {
            get { return WebPageElement.GetList(Id, WebObjects.WebMasterPage); }
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        #region Static Methods

        public static IEnumerable<WebMasterPage> FilterPermitted(int siteId)
        {
            IEnumerable<WebMasterPage> masterPages = WebMasterPage.GetList(siteId);

            if (!WSession.Current.IsAdministrator)
            {
                var site = WSite.Get(siteId);
                if (!site.IsUserPermitted(Permissions.ManageContent, 0))
                {
                    // Check WebPages permissions
                    masterPages = from masterPage in masterPages
                                  where masterPage.IsUserPermitted(Permissions.ManageContent, 0) //.IsCurrentUserAdded(page)
                                  select masterPage;
                }
            }

            return masterPages;
        }

        public static IEnumerable<WebMasterPage> GetList(int siteId)
        {
            return _manager.GetList(siteId);
        }

        public static WebMasterPage Get(int masterPageId)
        {
            return _manager.Get(masterPageId);
        }

        public static bool Delete(int masterPageId)
        {
            return _manager.Delete(masterPageId);
        }

        #endregion

        #region INamedValueProvider Members

        public string GetValue(string key)
        {
            switch (key)
            {
                case WebMasterPageKeys.Id:
                    return Id.ToString();

                case WebMasterPageKeys.Name:
                    return this.Name;

                //case WebMasterPageKeys.Active:
                //    return Active.ToString();

                //case WebMasterPageKeys.Rank:
                //    return Rank.ToString();
            }

            return GetParameterValue(key);
        }

        public bool ContainsKey(string key)
        {
            switch (key)
            {
                case WebMasterPageKeys.Id:
                case WebMasterPageKeys.Name:
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


        public bool Delete()
        {
            this.DeleteRelatedObjects();
            this.DeleteSecurityObjects();

            var elements = this.Elements;

            for (int i = elements.Count() - 1; i >= 0; i--)
            {
                var element = elements.ElementAt(i);
                element.Delete();
            }

            return Delete(this.Id);
        }
    }

    public struct WebMasterPageKeys
    {
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Active = "Active";
        public const string Rank = "Rank";
    }
}
