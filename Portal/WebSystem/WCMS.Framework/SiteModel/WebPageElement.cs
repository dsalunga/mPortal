using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework.Core;
using WCMS.Framework.Core.Manager;

namespace WCMS.Framework
{
    /// <summary>
    /// Note: Use ObjectId to determine the owner if Page or MasterPage
    /// </summary>
    public class WebPageElement : PageElementBase, INameWebObject, INamedValueProvider, ISelfManager
    {
        private static IWebPageElementProvider _manager;

        public override int OBJECT_ID
        {
            get { return WebObjects.WebPageElement; }
        }

        static WebPageElement()
        {
            _manager = WebObject.ResolveManager<WebPageElement, IWebPageElementProvider>(WebObject.ResolveProvider<WebPageElement, IWebPageElementProvider>());
        }

        public WebPageElement()
        {
            Id = -1;
            RecordId = -1;
            TemplatePanelId = -1;
            PartControlTemplateId = -1;
        }

        [ObjectColumn]
        public int RecordId { get; set; }

        /// <summary>
        /// Can be used to determine if this is owned by a Page or a MasterPage
        /// </summary>
        [ObjectColumn]
        public int ObjectId { get; set; }

        [ObjectColumn]
        public override int TemplatePanelId { get; set; }

        /// <summary>
        /// Returns true when allowed, false when not
        /// </summary>
        /// <returns></returns>
        public override int GetPublicAccess(WSession session)
        {
            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Inherit))
            {
                PublicSecurableObject parent = Page;
                if (parent != null)
                {
                    return parent.GetPublicAccess(session);
                }
                else
                {
                    parent = this.MasterPage;

                    if (parent != null)
                        return this.MasterPage.GetPublicAccess(session);
                    else
                        return PublicAccessCheckResult.Denied;
                }
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
                PublicSecurableObject parent = Page;
                if (parent != null)
                {
                    return parent.GetPublicAccess(userId, ip);
                }
                else
                {
                    parent = this.MasterPage;

                    if (parent != null)
                        return this.MasterPage.GetPublicAccess(userId, ip);
                    else
                        return PublicAccessCheckResult.Denied;
                }
            }
            else
            {
                return GetPublicAccessPartial(userId, ip);
            }
        }

        public bool OwnerIsPage
        {
            get { return ObjectId == WebObjects.WebPage; }
        }

        public override bool IsUserMgmtPermitted(int permissionId)
        {
            if (ManagementAccess == WebMgmtAccess.Inherit)
            {
                PublicSecurableObject parent = Page;
                if (parent != null)
                {
                    return parent.IsUserMgmtPermitted(permissionId);
                }
                else
                {
                    parent = this.MasterPage;
                    if (parent != null)
                        return parent.IsUserMgmtPermitted(permissionId);
                    else
                        return false;
                }
            }
            else
            {
                return IsUserPermitted(permissionId, 0);
            }
        }

        public override WPage Page
        {
            get
            {
                WPage page = null;
                if (ObjectId == WebObjects.WebPage)
                    page = WPage.Get(RecordId);
                else
                {
                    page = this.MasterPage.Site.HomePage;
                    if (page == null)
                        return null;
                }

                if (page == null)
                    throw new Exception("WebPage in PageElement is null, Element: " + Id);

                return page;
            }
        }

        public override WSite Site
        {
            get
            {
                switch (ObjectId)
                {
                    case WebObjects.WebPage:
                        return Page.Site;

                    case WebObjects.WebMasterPage:
                        return MasterPage.Site;

                    default:
                        throw new NotSupportedException("MasterPageId get: Unknown ObjectId");
                }
            }
        }

        public override WebTemplatePanel Panel
        {
            get { return WebTemplatePanel.Get(TemplatePanelId); }
        }

        public new bool IsActive
        {
            get { return Active == 1; }
        }

        //public override WebObjectContent ObjectContent
        //{
        //    get { return WebObjectContent.GetByObjectId(WebObjects.WebPageElement, Id); }
        //}

        public override int MasterPageId
        {
            get
            {
                switch (ObjectId)
                {
                    case WebObjects.WebPage:
                        return Page.MasterPageId;

                    case WebObjects.WebMasterPage:
                        return RecordId;

                    default:
                        throw new NotSupportedException("MasterPageId get: Unknown ObjectId");
                }
            }

            set { }
        }

        public override WebMasterPage MasterPage
        {
            get
            {
                switch (ObjectId)
                {
                    case WebObjects.WebPage:
                        return Page.MasterPage;

                    case WebObjects.WebMasterPage:
                        return WebMasterPage.Get(RecordId);

                    default:
                        throw new NotSupportedException("MasterPage get: Unknown ObjectId");
                }
            }
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        #region Static Methods

        public static IEnumerable<WebPageElement> GetList(int recordId, int objectId)
        {
            return _manager.GetList(recordId, objectId);
        }

        public static IEnumerable<WebPageElement> GetList(int recordId, int objectId, int templatePanelId)
        {
            return _manager.GetList(recordId, objectId, templatePanelId);
        }

        public static WebPageElement Get(int PageElementId)
        {
            return _manager.Get(PageElementId);
        }

        public static int GetCount(int recordId, int objectId, int templatePanelId)
        {
            return _manager.GetCount(recordId, objectId, templatePanelId);
        }

        public static int GetMaxRank(int recordId, int objectId, int templatePanelId)
        {
            return _manager.GetMaxRank(recordId, objectId, templatePanelId);
        }

        public static bool Delete(int id)
        {
            return _manager.Delete(id);
        }

        #endregion

        #region INamedValueProvider Members

        public string GetValue(string key)
        {
            switch (key)
            {
                case WebPageElementKeys.Id:
                    return Id.ToString();

                case WebPageElementKeys.Name:
                case WebPageElementKeys.Title:
                    return this.Name;

                case WebPageElementKeys.Active:
                    return Active.ToString();

                case WebPageElementKeys.Rank:
                    return Rank.ToString();
            }

            return string.Empty;
        }

        public bool ContainsKey(string key)
        {
            switch (key)
            {
                case WebPageElementKeys.Id:
                case WebPageElementKeys.Name:
                case WebPageElementKeys.Title:
                case WebPageElementKeys.Active:
                case WebPageElementKeys.Rank:
                    return true;
            }

            return true;
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

            return Delete(this.Id);
        }
    }

    public struct WebPageElementKeys
    {
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Title = "Title";
        public const string Active = "Active";
        public const string Rank = "Rank";
    }
}
