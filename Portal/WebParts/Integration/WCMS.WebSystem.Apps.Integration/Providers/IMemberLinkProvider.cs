using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public interface IMemberLinkProvider : IDataProvider<MemberLink>
    {
        MemberLink GetByUserId(int userId);
        MemberLink GetByMemberId(int memberId);
        MemberLink Get(string externalIdNo);
        MemberLink Get(string externalIdNo, DateTime membershipDate);
        IEnumerable<MemberLink> GetList(int approved = -1, int celebrantsMonth = -1);
        IEnumerable<MemberLink> GetList(DateTime lastUpdate, int approved = -1);
    }
}
