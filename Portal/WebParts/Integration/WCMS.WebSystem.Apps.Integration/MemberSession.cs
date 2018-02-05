using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MemberSession
    {
        public MemberSession()
        {
            AuthKey = Guid.NewGuid();
            SessionStartDate = DateTime.Now;
            MemberId = -1;
            UserId = -1;
        }

        public MemberSession(Guid authKey, int memberId, int userId, DateTime sessionStartDate)
        {
            this.AuthKey = authKey;
            this.MemberId = memberId;
            this.UserId = userId;
            this.SessionStartDate = sessionStartDate;
        }

        public Guid AuthKey { get; set; }
        public int MemberId { get; set; }
        public int UserId { get; set; }
        public DateTime SessionStartDate { get; set; }
    }
}
