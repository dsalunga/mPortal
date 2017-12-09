using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework
{
    public class UserSessionBrowser
    {
        public UserSessionBrowser(string aspNetSessionID, int userId, int pageId)
        {
            AspNetSessionID = aspNetSessionID;
            UserId = userId;
            LastPageId = pageId;

            LastPageUrl = string.Empty;
            IPLocation = string.Empty;

            LastActivityDate = DateTime.Now;
            ActivityStartDate = DateTime.Now;
        }

        public string IPAddress { get; set; }
        public string IPLocation { get; set; }
        public string UserAgent { get; set; }
        public UserSession UserSession { get; set; }
        
        public string AspNetSessionID { get; set; }

        public int UserId { get; set; }
        public int LastPageId { get; set; }
        public string LastPageUrl { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime ActivityStartDate { get; set; }
    }
}
