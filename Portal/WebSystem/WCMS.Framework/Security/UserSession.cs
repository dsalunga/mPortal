using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework
{
    [Serializable]
    public class UserSession
    {
        public UserSession(int userId, UserSessionBrowser browser)
        {
            UserId = userId;
            SessionId = Guid.NewGuid();
            AuthKey = Guid.NewGuid();

            LastBrowserSession = browser;
        }

        /// <summary>
        /// Internal reference used by Portal
        /// </summary>
        public Guid SessionId { get; set; }

        public UserSessionBrowser LastBrowserSession { get; set; }

        /// <summary>
        /// Volatile, one time-use reference used by Portal
        /// </summary>
        public Guid AuthKey { get; set; }
        public int UserId { get; set; }
    }
}
