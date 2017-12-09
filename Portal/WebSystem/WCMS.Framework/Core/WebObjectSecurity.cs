using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    /// <summary>
    /// Represents a security link between an Object and a User or Group (Role)
    /// </summary>
    public class WebObjectSecurity : IWebObject, ISelfManager
    {
        private static IWebObjectSecurityProvider _provider;

        static WebObjectSecurity()
        {
            _provider = WebObject.ResolveManager<WebObjectSecurity, IWebObjectSecurityProvider>(WebObject.ResolveProvider<WebObjectSecurity, IWebObjectSecurityProvider>());
        }

        public WebObjectSecurity()
        {
            Id = -1;
            ObjectId = -1;
            RecordId = -1;
            SecurityObjectId = -1;
            SecurityRecordId = -1;
            Public = 0;
        }

        #region Properties

        [ObjectColumn(true)]
        public int Id { get; set; }

        /// <summary>
        /// ObjectId of the Object. e.g. WebSite, WebPage, etc
        /// </summary>
        [ObjectColumn(NullValue = -1)]
        public int ObjectId { get; set; }

        /// <summary>
        /// RecordId of the Object. e.g. WebSite, WebPage, etc
        /// </summary>
        [ObjectColumn(NullValue = -1)]
        public int RecordId { get; set; }

        /// <summary>
        /// ObjectId of the security object. i.e. User and Group
        /// </summary>
        [ObjectColumn(NullValue = -1)]
        public int SecurityObjectId { get; set; }


        /// <summary>
        /// RecordId of the security object. i.e. User and Group
        /// </summary>
        [ObjectColumn(NullValue = -1)]
        public int SecurityRecordId { get; set; }

        [ObjectColumn]
        public int Public { get; set; }

        public INameWebObject Object
        {
            get { return WebObject.GetEntity(ObjectId, RecordId); }
        }

        public static IWebObjectSecurityProvider Provider
        {
            get { return _provider; }
        }

        /// <summary>
        /// Returns the User or Group
        /// </summary>
        public INameWebObject SecurityEntity
        {
            get { return WebObject.GetEntity(SecurityObjectId, SecurityRecordId); }
        }

        public IEnumerable<WebObjectSecurityPermission> ObjectSecurityPermissions
        {
            get
            {
                return WebObjectSecurityPermission.GetList(Id);
            }
        }

        public IEnumerable<WebPermission> Permissions
        {
            get
            {
                return (from i in ObjectSecurityPermissions
                        select i.Permission);
            }
        }

        public WebObjectSecurityPermission AddPermission(int permissionId, int allow)
        {
            if (this.Id > 0)
            {
                WebObjectSecurityPermission perm = new WebObjectSecurityPermission();
                perm.ObjectSecurityId = this.Id;
                perm.PermissionId = permissionId;
                perm.Allow = allow;
                perm.Update();

                return perm;
            }

            throw new Exception("Save this object first before adding any permission!");
        }

        public bool IsPermitted(int permissionId)
        {
            //var permissions = Permissions;
            //if (permissions.Count > 0)
            //{
            //    return permissions.Find(p => p.Id == permissionId) != null;
            //}

            //return false;

            return IsPermitted(Permissions, permissionId);
        }

        #endregion

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return Delete(this.Id);
        }

        #endregion

        #region Static Methods

        public static bool IsPermitted(IEnumerable<WebPermission> permissions, int permissionId)
        {
            if (permissions.Count() > 0)
                return permissions.FirstOrDefault(p => p.Id == permissionId) != null;

            return false;
        }

        public static WebObjectSecurity Get(int id)
        {
            return _provider.Get(id);
        }

        public static WebObjectSecurity Get(int objectId, int recordId,
            int securityObjectId, int securityRecordId, int public2)
        {
            return _provider.Get(objectId, recordId, securityObjectId, securityRecordId, public2);
        }

        public static IEnumerable<WebObjectSecurity> GetList()
        {
            return _provider.GetList();
        }

        //public static List<WebObjectSecurity> GetList(int objectId, int recordId,
        //    int securityObjectId, int securityRecordId)
        //{
        //    return _provider.GetList(objectId, recordId, securityObjectId, securityRecordId);
        //}

        //public static WebObjectSecurity Get(int objectId, int recordId,
        //    int securityObjectId, int securityRecordId)
        //{
        //    return _provider.Get(objectId, recordId, securityObjectId, securityRecordId);
        //}

        public static DataSet GetDataSet(int objectId, int recordId,
            int securityObjectId, int securityRecordId, int public2)
        {
            var items = _provider.GetList(objectId, recordId, securityObjectId, securityRecordId, public2);

            INameWebObject oSObject = null;
            INameWebObject oSSecurityEntity = null;

            var subItems = from item in items
                           where // Check, for deleted items but ref. not yet removed (need cleanup)
                                (oSObject = item.Object) != null &&
                                (oSSecurityEntity = item.SecurityEntity) != null
                           select new
                           {
                               item.Id,
                               item.ObjectId,
                               item.RecordId,
                               RecordName = oSObject.Name,
                               item.SecurityObjectId,
                               item.SecurityRecordId,
                               SecurityEntityName = oSSecurityEntity.Name,
                               SecurityEntityFullName = oSSecurityEntity.OBJECT_ID == WebObjects.WebUser ? ((WebUser)oSSecurityEntity).FirstAndLastName : string.Empty,
                               item.Public
                           };

            return DataHelper.ToDataSet(subItems);
        }

        /// <summary>
        /// Check if the current user is permitted
        /// </summary>
        /// <param name="permissionId"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsUserPermitted(int permissionId, IWebObject obj, int isPublic)
        {
            return IsUserPermitted(WSession.Current.UserId, permissionId, obj, isPublic);
        }

        /// <summary>
        /// Checks permission directly, does not include inheritance.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionId"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsUserPermitted(int userId, int permissionId, IWebObject obj, int isPublic)
        {
            //bool isPermitted = true;
            //WebSite site = null;

            //if (obj.OBJECT_ID == WebObjects.WebPage)
            //{
            //    isPermitted = IsPermitted(GetUserPermissions(userId, obj), permissionId);
            //    if (!isPermitted)
            //    {
            //        var page = obj as WebPage;
            //        if (page != null && (site = page.Site) != null)
            //            isPermitted = IsUserPermitted(userId, permissionId, site);
            //    }
            //}
            //else if (obj.OBJECT_ID == WebObjects.WebMasterPage)
            //{
            //    isPermitted = IsPermitted(GetUserPermissions(userId, obj), permissionId);
            //    if (!isPermitted)
            //    {
            //        var masterPage = obj as WebMasterPage;
            //        if (masterPage != null && (site = masterPage.Site) != null)
            //            isPermitted = IsUserPermitted(userId, permissionId, site);
            //    }
            //}
            //else
            //{
            return IsPermitted(GetUserPermissions(userId, obj, isPublic), permissionId);
            //}

            //return isPermitted;
        }

        public static IEnumerable<WebPermission> GetUserPermissions(IWebObject obj, int isPublic)
        {
            return GetUserPermissions(WSession.Current.UserId, obj, isPublic);
        }

        public static IEnumerable<WebPermission> GetUserPermissions(int userId, IWebObject obj, int isPublic)
        {
            var sec = WebObjectSecurity.Get(obj.OBJECT_ID, obj.Id, WebObjects.WebUser, userId, isPublic);

            if (sec != null)
            {
                return sec.Permissions;
            }
            else
            {
                WebUser user = WebUser.Get(userId);
                if (user != null)
                {
                    var groups = user.Groups;
                    foreach (var group in groups)
                    {
                        var security = Get(obj.OBJECT_ID, obj.Id, WebObjects.WebGroup, group.Id, isPublic);
                        if (security != null)
                            return security.Permissions;
                    }
                }
            }

            return new List<WebPermission>();
        }

        public static IEnumerable<WebObjectSecurity> GetListByObjectTypeAndUser(int userId, int objectId, int isPublic)
        {
            List<WebObjectSecurity> items = new List<WebObjectSecurity>();

            var sec = Provider.GetList(objectId, -1, WebObjects.WebUser, userId, isPublic);

            if (sec.Count() > 0)
                items.AddRange(sec);

            WebUser user = WebUser.Get(userId);
            if (user != null)
            {
                var groups = user.Groups;
                foreach (var group in groups)
                {
                    var security = Provider.GetList(objectId, -1, WebObjects.WebGroup, group.Id, isPublic);
                    if (security.Count() > 0)
                        items.AddRange(security);
                }
            }

            return items;
        }

        public static bool IsUserAdded(IWebObject obj, int public2 = 0)
        {
            return IsUserAdded(WSession.Current.UserId, obj, public2);
        }

        public static bool IsUserAdded(int userId, IWebObject obj, int public2 = 0)
        {
            var sec = WebObjectSecurity.Get(obj.OBJECT_ID, obj.Id, WebObjects.WebUser, userId, public2);
            if (sec == null)
            {
                WebUser user = WebUser.Get(userId);
                if (user != null)
                {
                    var groups = user.Groups;
                    foreach (var group in groups)
                    {
                        sec = Get(obj.OBJECT_ID, obj.Id, WebObjects.WebGroup, group.Id, 0);
                        if (sec != null)
                            break;
                    }
                }
            }

            return sec != null;
        }

        public static bool Delete(int id)
        {
            // Select all WebObjectSecurityPermission objects
            var items = WebObjectSecurityPermission.GetList(id);
            if (items.Count() > 0)
            {
                for (int i = items.Count() - 1; i >= 0; i--)
                {
                    var item = items.ElementAt(i);
                    item.Delete();
                }
            }

            return _provider.Delete(id);
        }

        #endregion

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.WebObjectSecurity; }
        }

        #endregion


    }
}
