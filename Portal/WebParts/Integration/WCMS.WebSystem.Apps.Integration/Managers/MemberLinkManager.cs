using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration.Managers
{
    public class MemberLinkManager : StandardDataManager<MemberLink>, IMemberLinkProvider
    {
        protected IMemberLinkProvider _provider;

        public MemberLinkManager(IMemberLinkProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public MemberLink GetByUserId(int userId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.UserId == userId);
            }

            return _provider.GetByUserId(userId);
        }

        public MemberLink GetByMemberId(int memberId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.MemberId == memberId);
            }

            return _provider.GetByMemberId(memberId);
        }

        public MemberLink Get(string externalIdNo)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.ExternalIdNo.Equals(externalIdNo, StringComparison.InvariantCultureIgnoreCase));
            }

            return _provider.Get(externalIdNo);
        }

        public MemberLink Get(string externalIdNo, DateTime membershipDate)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.ExternalIdNo.Equals(externalIdNo, StringComparison.InvariantCultureIgnoreCase) && i.MembershipDate == membershipDate);
            }

            return _provider.Get(externalIdNo, membershipDate);
        }

        public IEnumerable<MemberLink> GetList(int approved = -1, int celebrantsMonth = -1)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => (approved == -1 || i.Approved == approved)
                        && (celebrantsMonth == -1 || i.MembershipDate.Month == celebrantsMonth)
                    );
            }

            return _provider.GetList(approved, celebrantsMonth);
        }

        public IEnumerable<MemberLink> GetList(DateTime lastUpdate, int approved = -1)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => (approved == -1 || i.Approved == approved) && i.LastUpdate == lastUpdate);
            }

            return _provider.GetList(lastUpdate, approved);
        }
    }
}
