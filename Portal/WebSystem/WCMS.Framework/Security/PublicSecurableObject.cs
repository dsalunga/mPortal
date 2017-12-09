using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public abstract class PublicSecurableObject : SecurableObject, IPublicSecurable
    {
        [ObjectColumn]
        public int PublicAccess { get; set; }

        [ObjectColumn]
        public int ManagementAccess { get; set; }

        //public abstract int Id { get; set; }

        public bool IsInheritEnabled
        {
            get { return WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Inherit); }
        }

        public bool IsAnonymousEnabled
        {
            get { return WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Anonymous); }
        }

        public bool IsAccountAccessEnabled
        {
            get { return WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Account); }
        }

        public abstract int GetPublicAccess(WSession session);

        public abstract int GetPublicAccess(int userId, string ip = "");

        public void ExecuteUserMgmtActions(Action contentMgmtAction, Action accessDenied, Action permitted = null)
        {
            if (this.IsUserMgmtPermitted(Permissions.ManageInstance))
            {
                if (permitted != null)
                    permitted();
            }
            else if (this.IsUserMgmtPermitted(Permissions.ManageContent))
                contentMgmtAction();
            else
                accessDenied();
        }

        public abstract bool IsUserMgmtPermitted(int permissionId);
        /// <summary>
        /// Returns true when allowed, false when not
        /// </summary>
        /// <returns></returns>
        protected int GetPublicAccessPartial(WSession session)
        {
            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Anonymous))
            {
                int userId = session.UserId;
                if (userId > 0 && (/*!WSession.UserSessions.Contains(userId) || */!WSession.UserSessions.BrowserCache.ContainsKey(WSession.Context.Session.SessionID)))
                    WSession.LogOff();

                return PublicAccessCheckResult.Granted;
            }
            else if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Account))
            {
                return GetPublicAccessAccount(session);
            }
            else if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.IPAddress))
            {
                return GetPublicAccessIpAddress() ? PublicAccessCheckResult.Granted : PublicAccessCheckResult.Denied;
            }
            else if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.AccountOrIPAddress))
            {
                if (GetPublicAccessIpAddress())
                    return PublicAccessCheckResult.Granted;
                return GetPublicAccessAccount(session);
            }
            else if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.AccountAndIPAddress))
            {
                if (GetPublicAccessIpAddress())
                    return GetPublicAccessAccount(session);
                // Return denied
            }
            else
            {
                throw new InvalidOperationException("Security permission type not supported: " + PublicAccess);
            }

            return PublicAccessCheckResult.Denied;
        }

        protected int GetPublicAccessPartial(int userId, string ip = "")
        {
            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Anonymous))
            {
                //if (userId > 0 && (/*!WSession.UserSessions.Contains(userId) || */!WSession.UserSessions.BrowserCache.ContainsKey(WSession.Context.Session.SessionID)))
                //WSession.LogOff();

                return PublicAccessCheckResult.Granted;
            }
            else if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Account))
            {
                if (userId > 0)
                    return GetPublicAccessAccount(userId);
            }
            else if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.IPAddress))
            {
                return !string.IsNullOrEmpty(ip) && GetPublicAccessIpAddress(ip) ? PublicAccessCheckResult.Granted : PublicAccessCheckResult.Denied;
            }
            else if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.AccountOrIPAddress))
            {
                if (!string.IsNullOrEmpty(ip) && GetPublicAccessIpAddress(ip))
                    return PublicAccessCheckResult.Granted;
                else if (userId > 0)
                    return GetPublicAccessAccount(userId);
            }
            else if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.AccountAndIPAddress))
            {
                if (!string.IsNullOrEmpty(ip) && userId > 0 && GetPublicAccessIpAddress(ip))
                    return GetPublicAccessAccount(userId);
                // Return denied
            }
            else
            {
                throw new InvalidOperationException("Security permission type not supported: " + PublicAccess);
            }

            return PublicAccessCheckResult.Denied;
        }

        public int GetPublicAccountPermissionMax()
        {
            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.Inherit))
            {
                if (OBJECT_ID == WebObjects.WebPage)
                {
                    var page = (this as WPage);
                    var parent = page.Parent;
                    if (parent != null)
                        return parent.GetPublicAccountPermissionMax();
                    else
                        return page.Site.GetPublicAccountPermissionMax();
                }
                else if (OBJECT_ID == WebObjects.WebPageElement)
                {
                    var element = (this as WebPageElement);
                    if (element.ObjectId == WebObjects.WebPage)
                    {
                        var parent = element.Page;
                        if (parent != null)
                            return parent.GetPublicAccountPermissionMax();

                    }
                    else
                    {
                        var parent = element.MasterPage;
                        if (parent != null)
                            return parent.GetPublicAccountPermissionMax();
                    }
                }
                else
                {
                    return (this as WSite).GetPublicAccountPermissionMax();
                }
            }
            else
            {
                return GetPublicAccountPermMaxPartial();
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Permissions value</returns>
        protected int GetPublicAccountPermMaxPartial()
        {
            WSession session = WSession.Current;
            int userId = session.UserId;
            if (userId < 1) return Permissions.PublicRead; //PublicAccessCheckResult.NotLoggedIn;
            var item = WebObjectSecurity.Provider.Get(OBJECT_ID, Id, WebObjects.WebUser, userId, 1);

            // #TODO: once design is final, check the Allow and Deny.

            // if user has no public permissions (check minimum read)... check Groups.
            if (item == null || item.Permissions.FirstOrDefault(p => p.Id == Permissions.PublicWrite) == null)
            {
                // Check by groups. This is an initial implementation.
                var groups = session.User.Groups;
                foreach (var group in groups)
                {
                    // break or stop loop when a PublicRead is found.
                    var security = WebObjectSecurity.Provider.Get(OBJECT_ID, Id, WebObjects.WebGroup, group.Id, 1);
                    if (security != null && security.Permissions.FirstOrDefault(p => p.Id == Permissions.PublicWrite) != null)
                    {
                        item = security;
                        break;
                    }
                }
            }

            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.AllAccountExceptEntries))
            {
                // Restrict account when present in the list
                if (item == null) return Permissions.PublicWrite; // PublicAccessCheckResult.Granted;
            }
            else
            {
                // Restrict when not present in the list
                if (item != null) return Permissions.PublicWrite; // PublicAccessCheckResult.Granted;
            }

            return Permissions.PublicRead; // Should be none?
        }

        private int GetPublicAccessAccount(WSession session)
        {
            if (session == null) session = WSession.Current;

            int userId = session.UserId;
            if (userId < 1) return PublicAccessCheckResult.NotLoggedIn;
            if (/*!WSession.UserSessions.Contains(userId) || */!WSession.UserSessions.BrowserCache.ContainsKey(WSession.Context.Session.SessionID))
            {
                WSession.LogOff();
                return PublicAccessCheckResult.NotLoggedIn;
            }

            return GetPublicAccessAccount(userId);
        }

        private int GetPublicAccessAccount(int userId)
        {
            //if (userId < 1) return PublicAccessCheckResult.NotLoggedIn;
            //if (/*!WSession.UserSessions.Contains(userId) || */!WSession.UserSessions.BrowserCache.ContainsKey(WSession.Context.Session.SessionID))
            //{
            //    WSession.LogOff();
            //    return PublicAccessCheckResult.NotLoggedIn;
            //}

            var item = WebObjectSecurity.Provider.Get(OBJECT_ID, Id, WebObjects.WebUser, userId, 1);

            // #TODO: once design is final, check the Allow and Deny.

            // if user has no public read...
            if (item == null || item.Permissions.FirstOrDefault(p => p.Id == Permissions.PublicRead) == null)
            {
                // Check by groups. This is an initial implementation.
                var groups = WebUser.Get(userId).Groups;
                var count = groups.Count();
                for (int i = 0; i < count; i++)
                {
                    var group = groups.ElementAt(i);
                    // break or stop loop when a PublicRead is found.
                    var security = WebObjectSecurity.Provider.Get(OBJECT_ID, Id, WebObjects.WebGroup, group.Id, 1);
                    if (security != null && security.Permissions.FirstOrDefault(p => p.Id == Permissions.PublicRead) != null)
                    {
                        item = security;
                        break;
                    }
                }
            }

            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.AllAccountExceptEntries))
            {
                // Restrict account when present in the list
                if (item == null) return PublicAccessCheckResult.Granted;
            }
            else
            {
                // Restrict when not present in the list
                if (item != null) return PublicAccessCheckResult.Granted;
            }

            return PublicAccessCheckResult.Denied;
        }

        private bool GetPublicAccessIpAddress(string ip = "")
        {
            var ipAddresses = WebObjectIPAddress.GetList(OBJECT_ID, Id);
            string ipAddress = string.IsNullOrEmpty(ip) ? WHelper.GetUserHostAddress() : ip;

            if (WebPublicAccess.IsEnabled(PublicAccess, WebPublicAccess.AllIPAddressExceptEntries))
            {
                // Restricted when present in the list
                return (ipAddresses.FirstOrDefault(i => i.IPAddress == ipAddress) == null);
            }
            else
            {
                return (ipAddresses.FirstOrDefault(i => i.IPAddress == ipAddress) != null);
            }
        }
    }
}
