using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Interfaces;

namespace WCMS.WebSystem.Apps.Integration.Registration
{
    public class MemberLinkProvider
    {
        private static IDataProvider _provider;

        static MemberLinkProvider()
        {
            _provider = WebObject.ResolveProvider(typeof(MemberLink));
        }

        public MemberLink Get(int memberLinkId)
        {
            return _provider.Get<MemberLink>(memberLinkId);
        }

        public MemberLink Get(int userId, int memberId)
        {
            return _provider.Get<MemberLink>(
                new QueryFilterElement { Name = "UserId", NullValue = -1, Value = userId },
                new QueryFilterElement { Name = "MemberId", NullValue = -1, Value = memberId });
        }

        public List<MemberLink> GetList()
        {
            return _provider.GetList<MemberLink>();
        }

        public int Update(MemberLink item)
        {
            return _provider.Update<MemberLink>(item);
        }

        public bool Delete(int memberLinkId)
        {
            return _provider.Delete<MemberLink>(memberLinkId);
        }
    }
}
