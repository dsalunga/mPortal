using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class MemberLinkSqlProvider : GenericSqlDataProviderBase<MemberLink>, IMemberLinkProvider
    {
        protected override string DeleteProcedure { get { return "MemberLink_Del"; } }
        protected override string SelectProcedure { get { return "MemberLink_Get"; } }
        protected override string IdParameter { get { return "MemberLinkId"; } }

        #region IDataProvider<MemberLink> Members

        public MemberLink Get(string externalIdNo, DateTime membershipDate)
        {
            using (var r = SqlHelper.ExecuteReader("MemberLink_Get",
                new SqlParameter("@ExternalIdNo", externalIdNo),
                new SqlParameter("@MembershipDate", membershipDate)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public MemberLink Get(string externalIdNo)
        {
            using (var r = SqlHelper.ExecuteReader("MemberLink_Get",
                new SqlParameter("@ExternalIdNo", externalIdNo)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        protected override MemberLink From(IDataReader r, MemberLink source)
        {
            var item = source ?? new MemberLink();
            item.MemberLinkId = DataUtil.GetId(r["MemberLinkId"]);
            item.UserId = DataUtil.GetId(r["UserId"]);
            item.MemberId = DataUtil.GetId(r["MemberId"]);
            item.LocaleId = DataUtil.GetId(r, "LocaleId");
            item.ExternalIdNo = r["ExternalIdNo"].ToString();

            item.HomeAddressLine1 = r["HomeAddressLine1"].ToString();
            item.HomeAddressLine2 = r["HomeAddressLine2"].ToString();
            item.HomeAddressStateCode = DataUtil.GetId(r["HomeAddressStateCode"]);
            item.HomeAddressZipCode = r["HomeAddressZipCode"].ToString();
            item.MobileNumber = r["MobileNumber"].ToString();
            item.HomePhone = r["HomePhone"].ToString();

            var homeCountry = DataUtil.GetId(r["HomeAddressCountryCode"]);
            if (homeCountry > 0)
                item.LocaleCountryCode = homeCountry;

            item.WorkAddressLine1 = r["WorkAddressLine1"].ToString();
            item.WorkAddressLine2 = r["WorkAddressLine2"].ToString();
            item.WorkAddressStateCode = DataUtil.GetId(r["WorkAddressStateCode"]);
            item.WorkAddressCountryCode = DataUtil.GetId(r["WorkAddressCountryCode"]);
            item.WorkAddressZipCode = r["WorkAddressZipCode"].ToString();

            item.WorkDesignation = r["WorkDesignation"].ToString();
            item.WorkPhone = r["WorkPhone"].ToString();
            item.Nickname = r["Nickname"].ToString();
            item.LastUpdate = (DateTime)r["LastUpdate"];
            item.PhotoPath = r["PhotoPath"].ToString();
            item.MembershipDate = DataUtil.GetDateTime(r, "MembershipDate");

            item.Approved = DataUtil.GetInt32(r, "Approved");
            item.Private = DataUtil.GetInt32(r, "Private");
            item.Locale = r["Locale"].ToString();
            item.AdditionalInfo = DataHelper.Get(r, "AdditionalInfo");

            return item;
        }

        public IEnumerable<MemberLink> GetList(int approved = -1, int celebrantsMonth = -1)
        {
            var items = new List<MemberLink>();

            using (var r = SqlHelper.ExecuteReader("MemberLink_Get",
                new SqlParameter("@Approved", approved),
                new SqlParameter("@CelebrantsMonth", celebrantsMonth)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<MemberLink> GetList(DateTime lastUpdate, int approved = -1)
        {
            var items = new List<MemberLink>();
            using (var r = SqlHelper.ExecuteReader("MemberLink_Get",
                new SqlParameter("@Approved", approved),
                new SqlParameter("@LastUpdate", lastUpdate)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public override int Update(MemberLink item)
        {
            if (item.MembershipDate < WConstants.DateTimeMinValue)
                item.MembershipDate = WConstants.DateTimeMinValue;

            var obj = SqlHelper.ExecuteScalar("MemberLink_Set",
                new SqlParameter("@MemberLinkId", item.MemberLinkId),
                new SqlParameter("@UserId", item.UserId),
                new SqlParameter("@MemberId", item.MemberId),
                new SqlParameter("@ExternalIdNo", item.ExternalIdNo),

                new SqlParameter("@HomeAddressLine1", item.HomeAddressLine1),
                new SqlParameter("@HomeAddressLine2", item.HomeAddressLine2),
                new SqlParameter("@HomeAddressStateCode", item.HomeAddressStateCode),
                new SqlParameter("@HomeAddressCountryCode", item.LocaleCountryCode),
                new SqlParameter("@HomeAddressZipCode", item.HomeAddressZipCode),

                new SqlParameter("@MobileNumber", item.MobileNumber),
                new SqlParameter("@HomePhone", item.HomePhone),

                new SqlParameter("@WorkAddressLine1", item.WorkAddressLine1),
                new SqlParameter("@WorkAddressLine2", item.WorkAddressLine2),
                new SqlParameter("@WorkAddressStateCode", item.WorkAddressStateCode),
                new SqlParameter("@WorkAddressCountryCode", item.WorkAddressCountryCode),
                new SqlParameter("@WorkAddressZipCode", item.WorkAddressZipCode),

                new SqlParameter("@WorkDesignation", item.WorkDesignation),
                new SqlParameter("@WorkPhone", item.WorkPhone),
                new SqlParameter("@Nickname", item.Nickname),
                new SqlParameter("@LastUpdate", item.LastUpdate),
                new SqlParameter("@PhotoPath", item.GetPhotoPathIfNull()),
                new SqlParameter("@MembershipDate", item.MembershipDate),

                new SqlParameter("@Approved", item.Approved),
                new SqlParameter("@Private", item.Private),
                new SqlParameter("@Locale", item.Locale),
                new SqlParameter("@AdditionalInfo", item.AdditionalInfo),
                new SqlParameter("@LocaleId", item.LocaleId)
            );

            item.MemberLinkId = DataUtil.GetId(obj);
            return item.MemberLinkId;
        }

        public MemberLink GetByUserId(int userId)
        {
            using (var r = SqlHelper.ExecuteReader("MemberLink_Get",
                new SqlParameter("@UserId", userId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public MemberLink GetByMemberId(int memberId)
        {
            using (var r = SqlHelper.ExecuteReader("MemberLink_Get",
                new SqlParameter("@MemberId", memberId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        #endregion
    }
}
