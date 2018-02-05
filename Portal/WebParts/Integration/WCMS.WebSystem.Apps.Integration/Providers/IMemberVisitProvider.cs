using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public interface IMemberVisitProvider : IDataProvider<MemberVisit>
    {
        IEnumerable<MemberVisit> GetList(int groupId);
        IEnumerable<MemberVisit> GetListByUserId(int userId);
        IEnumerable<MemberVisit> GetListByTag(string tag);
        IEnumerable<MemberVisit> GetList(int groupId = -1, int userId = -2, string tag = null);
    }
}
