using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;
using WCMS.Framework.Core.Manager;

namespace WCMS.Framework.Core
{
    public class WebGlobalPolicy : SecurableObject, ISelfManager
    {
        protected static IWebGlobalPolicyProvider _provider;

        static WebGlobalPolicy()
        {
            _provider = WebObject.ResolveManager<WebGlobalPolicy, IWebGlobalPolicyProvider>(WebObject.ResolveProvider<WebGlobalPolicy, IWebGlobalPolicyProvider>());
        }

        public WebGlobalPolicy()
        {
            Id = -1;
        }

        public List<WebObjectSecurityPermission> GetGlobalPermissions(int acctObjectId, int acctRecordId)
        {
            var perms = WebObjectSecurity.Provider.GetList(OBJECT_ID, Id, acctObjectId, acctRecordId, -1);

            List<WebObjectSecurityPermission> items = new List<WebObjectSecurityPermission>();
            foreach (var perm in perms)
            {
                items.AddRange(perm.ObjectSecurityPermissions);
            }

            return items;
        }

        public static IWebGlobalPolicyProvider Provider
        {
            get { return _provider; }
        }

        /// <summary>
        /// Check if the current user is permitted
        /// </summary>
        /// <param name="globalPolicyId"></param>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public static new bool IsUserPermitted(int globalPolicyId, int permissionId)
        {
            if (!WSession.Current.IsSiteManager)
                return false;

            if (WSession.Current.IsAdministrator)
                return true;

            var sitePolicy = _provider.Get(globalPolicyId);
            var perm = sitePolicy.TryGetUserPermission(permissionId);
            return (perm != null && perm.IsAllowed);
        }

        /// <summary>
        /// Check the current user is added to Global Policy
        /// </summary>
        /// <param name="globalPolicyId"></param>
        /// <returns></returns>
        public static bool IsUserAdded(int globalPolicyId)
        {
            if (WSession.Current.IsAdministrator)
                return true;

            var sitePolicy = _provider.Get(globalPolicyId);
            return sitePolicy.TryFindUser();
        }

        public static bool IsUserAdded(int userId, int globalPolicyId)
        {
            //if (WSession.Current.IsAdministrator)
            //    return true;

            var sitePolicy = _provider.Get(globalPolicyId);
            return sitePolicy.TryFindUser(userId);
        }

        #region IWebObject Members

        public override int OBJECT_ID
        {
            get { return WebObjects.WebGlobalPolicy; }
        }

        #endregion

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public bool Refresh()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
