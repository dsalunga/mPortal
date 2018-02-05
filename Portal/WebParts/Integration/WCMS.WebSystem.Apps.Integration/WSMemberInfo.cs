using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    public class WSMemberInfo : WSUserInfo
    {
        public int MemberId { get; set; }
        public string ExternalIdNo { get; set; }

        public WSMemberInfo()
        {
            ResetInfo();
        }

        public WSMemberInfo(WebUser user)
            : base(user)
        {
            ResetInfo();
        }

        public WSMemberInfo(WebUser user, MemberLink link)
            : base(user)
        {
            this.MemberId = link.MemberId;
            this.ExternalIdNo = link.ExternalIdNo;
        }


        private void ResetInfo()
        {
            MemberId = -1;
            ExternalIdNo = string.Empty;
        }
    }
}
