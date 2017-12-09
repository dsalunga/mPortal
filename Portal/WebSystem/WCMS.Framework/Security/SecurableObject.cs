using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public abstract class SecurableObject : ParameterizedWebObject
    {
        ///// <summary>
        ///// Incomplete...
        ///// </summary>
        ///// <returns>Permissions value</returns>
        //public int GetAccountPermissionMax()
        //{
        //    int userId = WSession.Current.UserId;

        //    if (userId < 1) return Permissions.None; //PublicAccessCheckResult.NotLoggedIn;

        //    WebObjectSecurity item = WebObjectSecurity.Provider.Get(OBJECT_ID, Id, WebObjects.WebUser, userId);

        //    // #TODO: once design is final, check the Allow and Deny.

        //    // if user has no public permissions (check minimum read)... check Groups.
        //    if (item == null || item.Permissions.Find(p => p.Id == Permissions.PublicWrite) == null)
        //    {
        //        // Check by groups. This is an initial implementation.
        //        var groups = WSession.Current.User.Groups;
        //        foreach (var group in groups)
        //        {
        //            // break or stop loop when a PublicRead is found.
        //            var security = WebObjectSecurity.Provider.Get(OBJECT_ID, Id, WebObjects.WebGroup, group.Id);
        //            if (security != null && security.Permissions.Find(p => p.Id == Permissions.PublicWrite) != null)
        //            {
        //                item = security;
        //                break;
        //            }
        //        }
        //    }

        //    //if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.AllAccountExceptEntries))
        //    //{
        //    //    // Restrict account when present in the list
        //    //    if (item == null) return Permissions.PublicWrite; // PublicAccessCheckResult.Granted;
        //    //}
        //    //else
        //    //{

        //    //}

        //    // Restrict when not present in the list
        //    if (item != null) return Permissions.PublicWrite; // PublicAccessCheckResult.Granted;

        //    return Permissions.None; //PublicRead; // Should be none?
        //}

        public void DeleteSecurityObjects()
        {
            // Security
            var securities = this.GetObjectSecurities();
            for (int i = securities.Count() - 1; i >= 0; i--)
                securities.ElementAt(i).Delete();
        }

        /// <summary>
        /// Security for Objects tagged to User and Groups
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WebObjectSecurity> GetObjectSecurities()
        {
            return WebObjectSecurity.Provider.GetList(OBJECT_ID, Id);
        }

        public IEnumerable<WebObjectSecurity> GetObjectSecurities(int securityObjectId, int isPublic)
        {
            return WebObjectSecurity.Provider.GetList(OBJECT_ID, Id, securityObjectId, -1, isPublic);
        }

        public WebObjectSecurity AddObjectSecurity(int securityObjectId, int securityRecordId, int isPublic)
        {
            if (Id > 0)
            {
                WebObjectSecurity security = new WebObjectSecurity();
                security.ObjectId = this.OBJECT_ID;
                security.RecordId = this.Id;
                security.SecurityObjectId = securityObjectId;
                security.SecurityRecordId = securityRecordId;
                security.Public = isPublic;
                security.Update();

                return security;
            }

            throw new Exception("Save this object first before adding any object security!");
        }

        /// <summary>
        /// Object Security (Objects tagged to User and Groups) permissions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WebObjectSecurityPermission> GetObjectSecurityPermissions()
        {
            List<WebObjectSecurityPermission> items = new List<WebObjectSecurityPermission>();
            var perms = GetObjectSecurities();

            foreach (var perm in perms)
                items.AddRange(perm.ObjectSecurityPermissions);

            return items.Distinct();
        }

        public WebObjectSecurityPermission TryGetUserPermission(int userId, int permissionId)
        {
            WebObjectSecurityPermission per = null;

            var user = WebUser.Get(userId);
            if (user != null)
            {
                var secs = GetObjectSecurities();

                WebObjectSecurity item = secs.FirstOrDefault(i => i.ObjectId == OBJECT_ID && i.RecordId == Id &&
                    i.SecurityObjectId == WebObjects.WebUser && i.SecurityRecordId == user.Id);

                // #TODO: once design is final, check the Allow and Deny.

                // if user has no public read...
                if (item == null || (per = item.ObjectSecurityPermissions.FirstOrDefault(p => p.PermissionId == permissionId)) == null)
                {
                    // Check by groups. This is an initial implementation.
                    var groups = user.Groups;
                    foreach (var group in groups)
                    {
                        // break or stop loop when a PublicRead is found.
                        var security = WebObjectSecurity.Provider.Get(OBJECT_ID, Id, WebObjects.WebGroup, group.Id, -1);
                        if (security != null && (per = security.ObjectSecurityPermissions.FirstOrDefault(p => p.PermissionId == permissionId)) != null)
                            break;
                    }
                }
            }

            return per;
        }

        /// <summary>
        /// Get current user's permission
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public WebObjectSecurityPermission TryGetUserPermission(int permissionId)
        {
            return TryGetUserPermission(WSession.Current.UserId, permissionId);
        }

        public bool TryFindUser(int userId)
        {
            var user = WebUser.Get(userId);
            WebObjectSecurity item = null;

            if (user != null)
            {
                var secs = GetObjectSecurities();

                item = secs.FirstOrDefault(i => i.ObjectId == OBJECT_ID && i.RecordId == Id &&
                    i.SecurityObjectId == WebObjects.WebUser && i.SecurityRecordId == user.Id);

                // #TODO: once design is final, check the Allow and Deny.

                // if user has no public read...
                if (item == null)
                {
                    // Check by groups. This is an initial implementation.
                    var groups = user.Groups;
                    foreach (var group in groups)
                    {
                        // break or stop loop when a PublicRead is found.
                        item = WebObjectSecurity.Provider.Get(OBJECT_ID, Id, WebObjects.WebGroup, group.Id, -1);
                        if (item != null)
                            break;
                    }
                }
            }

            return item != null;
        }

        /// <summary>
        /// Check if current user has permission entries.
        /// </summary>
        /// <returns>True if user has</returns>
        public bool TryFindUser()
        {
            return TryFindUser(WSession.Current.UserId);
        }


        /// <summary>
        /// Check if the current user is permitted
        /// </summary>
        /// <param name="permissionId"></param>
        /// <param name="isPublic">Specifies if for Public/Front-End access (not Admin)</param>
        /// <returns>True if user is permitted</returns>
        public bool IsUserPermitted(int permissionId, int isPublic)
        {
            if (WSession.Current.IsAdministrator)
                return true;

            return WebObjectSecurity.IsUserPermitted(permissionId, this, isPublic);
        }

        public bool IsUserPermitted(int permissionId, bool isPublic)
        {
            return IsUserPermitted(permissionId, isPublic ? 1 : 0);
        }


        /// <summary>
        /// Checks the highest permission granter to current user
        /// </summary>
        /// <param name="highestPermissionId">Hishest permission that the user is supposed to be granted</param>
        /// <param name="nextNotPermittedId">Permission the user is not supposed to have</param>
        /// <returns></returns>
        public bool IsHighestPermission(int highestPermissionId, int nextNotPermittedId, int isPublic)
        {
            var permissions = WebObjectSecurity.GetUserPermissions(this, isPublic);

            if (WebObjectSecurity.IsPermitted(permissions, highestPermissionId) && !WebObjectSecurity.IsPermitted(permissions, nextNotPermittedId))
                return true;

            return false;
        }

        //public bool IsCurrentUserPermitted(int permissionId)
        //{
        //    return false;
        //}

        ///// <summary>
        ///// Incomplete...
        ///// </summary>
        ///// <returns></returns>
        //private int GetAccessAccount()
        //{
        //    int userId = WSession.Current.UserId;

        //    if (userId < 1) return PublicAccessCheckResult.NotLoggedIn;

        //    WebObjectSecurity item = WebObjectSecurity.Provider.Get(OBJECT_ID, Id, WebObjects.WebUser, userId);

        //    // #TODO: once design is final, check the Allow and Deny.

        //    // if user has no public read...
        //    if (item == null || item.Permissions.Find(p => p.Id == Permissions.PublicRead) == null)
        //    {
        //        // Check by groups. This is an initial implementation.
        //        var groups = WSession.Current.User.Groups;
        //        foreach (var group in groups)
        //        {
        //            // break or stop loop when a PublicRead is found.
        //            var security = WebObjectSecurity.Provider.Get(OBJECT_ID, Id, WebObjects.WebGroup, group.Id);
        //            if (security != null && security.Permissions.Find(p => p.Id == Permissions.PublicRead) != null)
        //            {
        //                item = security;
        //                break;
        //            }
        //        }
        //    }

        //    //if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.AllAccountExceptEntries))
        //    //{
        //    //    // Restrict account when present in the list
        //    //    if (item == null) return PublicAccessCheckResult.Granted;
        //    //}
        //    //else
        //    //{

        //    //}

        //    // Restrict when not present in the list
        //    if (item != null) return PublicAccessCheckResult.Granted;

        //    return PublicAccessCheckResult.Denied;
        //}
    }
}
