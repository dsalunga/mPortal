using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class MemberLinkSqlProvider : GenericSqlDataProviderBase<MemberLink>, IMemberLinkProvider
    {
        protected override string DeleteProcedure { get { return "MemberLink_Del"; } }
        protected override string TableName { get { return "MemberLink"; } }

        protected override string IdColumn { get { return "MemberLinkId"; } }


        protected override string SelectProcedure { get { return "MemberLink_Get"; } }
        protected override string IdParameter { get { return "MemberLinkId"; } }

        #region IDataProvider<MemberLink> Members

        public MemberLink Get(string externalIdNo, DateTime membershipDate)
        {
            var sql = "SELECT * FROM MemberLink WHERE " +
                DbSyntax.QuoteIdentifier("ExternalIdNo") + " = @ExternalIdNo AND " +
                DbSyntax.QuoteIdentifier("MembershipDate") + " = @MembershipDate";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ExternalIdNo", externalIdNo),
                DbHelper.CreateParameter("@MembershipDate", membershipDate)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public MemberLink Get(string externalIdNo)
        {
            var sql = "SELECT * FROM MemberLink WHERE " + DbSyntax.QuoteIdentifier("ExternalIdNo") + " = @ExternalIdNo";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ExternalIdNo", externalIdNo)))
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
            item.AdditionalInfo = DataUtil.Get(r, "AdditionalInfo");

            return item;
        }

        public IEnumerable<MemberLink> GetList(int approved = -1, int celebrantsMonth = -1)
        {
            var items = new List<MemberLink>();

            var monthExpr = DbHelper.Provider == DatabaseProvider.PostgreSql
                ? "EXTRACT(MONTH FROM " + DbSyntax.QuoteIdentifier("MembershipDate") + ")"
                : "MONTH(" + DbSyntax.QuoteIdentifier("MembershipDate") + ")";
            var sql = "SELECT * FROM MemberLink WHERE " +
                "(@Approved = -1 OR " + DbSyntax.QuoteIdentifier("Approved") + " = @Approved) AND " +
                "(@CelebrantsMonth = -1 OR " + monthExpr + " = @CelebrantsMonth)";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Approved", approved),
                DbHelper.CreateParameter("@CelebrantsMonth", celebrantsMonth)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<MemberLink> GetList(DateTime lastUpdate, int approved = -1)
        {
            var items = new List<MemberLink>();

            var sql = "SELECT * FROM MemberLink WHERE " +
                DbSyntax.QuoteIdentifier("LastUpdate") + " >= @LastUpdate AND " +
                "(@Approved = -1 OR " + DbSyntax.QuoteIdentifier("Approved") + " = @Approved)";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Approved", approved),
                DbHelper.CreateParameter("@LastUpdate", lastUpdate)))
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

            string sql;
            DbParameter[] parms;

            if (item.MemberLinkId > 0)
            {
                sql = "UPDATE MemberLink SET " +
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId, " +
                    DbSyntax.QuoteIdentifier("MemberId") + " = @MemberId, " +
                    DbSyntax.QuoteIdentifier("ExternalIdNo") + " = @ExternalIdNo, " +
                    DbSyntax.QuoteIdentifier("HomeAddressLine1") + " = @HomeAddressLine1, " +
                    DbSyntax.QuoteIdentifier("HomeAddressLine2") + " = @HomeAddressLine2, " +
                    DbSyntax.QuoteIdentifier("HomeAddressStateCode") + " = @HomeAddressStateCode, " +
                    DbSyntax.QuoteIdentifier("HomeAddressCountryCode") + " = @HomeAddressCountryCode, " +
                    DbSyntax.QuoteIdentifier("HomeAddressZipCode") + " = @HomeAddressZipCode, " +
                    DbSyntax.QuoteIdentifier("MobileNumber") + " = @MobileNumber, " +
                    DbSyntax.QuoteIdentifier("HomePhone") + " = @HomePhone, " +
                    DbSyntax.QuoteIdentifier("WorkAddressLine1") + " = @WorkAddressLine1, " +
                    DbSyntax.QuoteIdentifier("WorkAddressLine2") + " = @WorkAddressLine2, " +
                    DbSyntax.QuoteIdentifier("WorkAddressStateCode") + " = @WorkAddressStateCode, " +
                    DbSyntax.QuoteIdentifier("WorkAddressCountryCode") + " = @WorkAddressCountryCode, " +
                    DbSyntax.QuoteIdentifier("WorkAddressZipCode") + " = @WorkAddressZipCode, " +
                    DbSyntax.QuoteIdentifier("WorkDesignation") + " = @WorkDesignation, " +
                    DbSyntax.QuoteIdentifier("WorkPhone") + " = @WorkPhone, " +
                    DbSyntax.QuoteIdentifier("Nickname") + " = @Nickname, " +
                    DbSyntax.QuoteIdentifier("LastUpdate") + " = @LastUpdate, " +
                    DbSyntax.QuoteIdentifier("PhotoPath") + " = @PhotoPath, " +
                    DbSyntax.QuoteIdentifier("MembershipDate") + " = @MembershipDate, " +
                    DbSyntax.QuoteIdentifier("Approved") + " = @Approved, " +
                    DbSyntax.QuoteIdentifier("Private") + " = @Private, " +
                    DbSyntax.QuoteIdentifier("Locale") + " = @Locale, " +
                    DbSyntax.QuoteIdentifier("AdditionalInfo") + " = @AdditionalInfo, " +
                    DbSyntax.QuoteIdentifier("LocaleId") + " = @LocaleId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("MemberLinkId") + " = @MemberLinkId";
                parms = new[] {
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@MemberId", item.MemberId),
                    DbHelper.CreateParameter("@ExternalIdNo", item.ExternalIdNo),
                    DbHelper.CreateParameter("@HomeAddressLine1", item.HomeAddressLine1),
                    DbHelper.CreateParameter("@HomeAddressLine2", item.HomeAddressLine2),
                    DbHelper.CreateParameter("@HomeAddressStateCode", item.HomeAddressStateCode),
                    DbHelper.CreateParameter("@HomeAddressCountryCode", item.LocaleCountryCode),
                    DbHelper.CreateParameter("@HomeAddressZipCode", item.HomeAddressZipCode),
                    DbHelper.CreateParameter("@MobileNumber", item.MobileNumber),
                    DbHelper.CreateParameter("@HomePhone", item.HomePhone),
                    DbHelper.CreateParameter("@WorkAddressLine1", item.WorkAddressLine1),
                    DbHelper.CreateParameter("@WorkAddressLine2", item.WorkAddressLine2),
                    DbHelper.CreateParameter("@WorkAddressStateCode", item.WorkAddressStateCode),
                    DbHelper.CreateParameter("@WorkAddressCountryCode", item.WorkAddressCountryCode),
                    DbHelper.CreateParameter("@WorkAddressZipCode", item.WorkAddressZipCode),
                    DbHelper.CreateParameter("@WorkDesignation", item.WorkDesignation),
                    DbHelper.CreateParameter("@WorkPhone", item.WorkPhone),
                    DbHelper.CreateParameter("@Nickname", item.Nickname),
                    DbHelper.CreateParameter("@LastUpdate", item.LastUpdate),
                    DbHelper.CreateParameter("@PhotoPath", item.GetPhotoPathIfNull()),
                    DbHelper.CreateParameter("@MembershipDate", item.MembershipDate),
                    DbHelper.CreateParameter("@Approved", item.Approved),
                    DbHelper.CreateParameter("@Private", item.Private),
                    DbHelper.CreateParameter("@Locale", item.Locale),
                    DbHelper.CreateParameter("@AdditionalInfo", item.AdditionalInfo),
                    DbHelper.CreateParameter("@LocaleId", item.LocaleId),
                    DbHelper.CreateParameter("@MemberLinkId", item.MemberLinkId)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO MemberLink (" +
                    DbSyntax.QuoteIdentifier("UserId") + ", " +
                    DbSyntax.QuoteIdentifier("MemberId") + ", " +
                    DbSyntax.QuoteIdentifier("ExternalIdNo") + ", " +
                    DbSyntax.QuoteIdentifier("HomeAddressLine1") + ", " +
                    DbSyntax.QuoteIdentifier("HomeAddressLine2") + ", " +
                    DbSyntax.QuoteIdentifier("HomeAddressStateCode") + ", " +
                    DbSyntax.QuoteIdentifier("HomeAddressCountryCode") + ", " +
                    DbSyntax.QuoteIdentifier("HomeAddressZipCode") + ", " +
                    DbSyntax.QuoteIdentifier("MobileNumber") + ", " +
                    DbSyntax.QuoteIdentifier("HomePhone") + ", " +
                    DbSyntax.QuoteIdentifier("WorkAddressLine1") + ", " +
                    DbSyntax.QuoteIdentifier("WorkAddressLine2") + ", " +
                    DbSyntax.QuoteIdentifier("WorkAddressStateCode") + ", " +
                    DbSyntax.QuoteIdentifier("WorkAddressCountryCode") + ", " +
                    DbSyntax.QuoteIdentifier("WorkAddressZipCode") + ", " +
                    DbSyntax.QuoteIdentifier("WorkDesignation") + ", " +
                    DbSyntax.QuoteIdentifier("WorkPhone") + ", " +
                    DbSyntax.QuoteIdentifier("Nickname") + ", " +
                    DbSyntax.QuoteIdentifier("LastUpdate") + ", " +
                    DbSyntax.QuoteIdentifier("PhotoPath") + ", " +
                    DbSyntax.QuoteIdentifier("MembershipDate") + ", " +
                    DbSyntax.QuoteIdentifier("Approved") + ", " +
                    DbSyntax.QuoteIdentifier("Private") + ", " +
                    DbSyntax.QuoteIdentifier("Locale") + ", " +
                    DbSyntax.QuoteIdentifier("AdditionalInfo") + ", " +
                    DbSyntax.QuoteIdentifier("LocaleId") +
                    ") VALUES (@UserId, @MemberId, @ExternalIdNo, @HomeAddressLine1, @HomeAddressLine2, @HomeAddressStateCode, @HomeAddressCountryCode, @HomeAddressZipCode, @MobileNumber, @HomePhone, @WorkAddressLine1, @WorkAddressLine2, @WorkAddressStateCode, @WorkAddressCountryCode, @WorkAddressZipCode, @WorkDesignation, @WorkPhone, @Nickname, @LastUpdate, @PhotoPath, @MembershipDate, @Approved, @Private, @Locale, @AdditionalInfo, @LocaleId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("MemberLinkId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@MemberId", item.MemberId),
                    DbHelper.CreateParameter("@ExternalIdNo", item.ExternalIdNo),
                    DbHelper.CreateParameter("@HomeAddressLine1", item.HomeAddressLine1),
                    DbHelper.CreateParameter("@HomeAddressLine2", item.HomeAddressLine2),
                    DbHelper.CreateParameter("@HomeAddressStateCode", item.HomeAddressStateCode),
                    DbHelper.CreateParameter("@HomeAddressCountryCode", item.LocaleCountryCode),
                    DbHelper.CreateParameter("@HomeAddressZipCode", item.HomeAddressZipCode),
                    DbHelper.CreateParameter("@MobileNumber", item.MobileNumber),
                    DbHelper.CreateParameter("@HomePhone", item.HomePhone),
                    DbHelper.CreateParameter("@WorkAddressLine1", item.WorkAddressLine1),
                    DbHelper.CreateParameter("@WorkAddressLine2", item.WorkAddressLine2),
                    DbHelper.CreateParameter("@WorkAddressStateCode", item.WorkAddressStateCode),
                    DbHelper.CreateParameter("@WorkAddressCountryCode", item.WorkAddressCountryCode),
                    DbHelper.CreateParameter("@WorkAddressZipCode", item.WorkAddressZipCode),
                    DbHelper.CreateParameter("@WorkDesignation", item.WorkDesignation),
                    DbHelper.CreateParameter("@WorkPhone", item.WorkPhone),
                    DbHelper.CreateParameter("@Nickname", item.Nickname),
                    DbHelper.CreateParameter("@LastUpdate", item.LastUpdate),
                    DbHelper.CreateParameter("@PhotoPath", item.GetPhotoPathIfNull()),
                    DbHelper.CreateParameter("@MembershipDate", item.MembershipDate),
                    DbHelper.CreateParameter("@Approved", item.Approved),
                    DbHelper.CreateParameter("@Private", item.Private),
                    DbHelper.CreateParameter("@Locale", item.Locale),
                    DbHelper.CreateParameter("@AdditionalInfo", item.AdditionalInfo),
                    DbHelper.CreateParameter("@LocaleId", item.LocaleId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.MemberLinkId = DataUtil.GetId(obj);
            }

            return item.MemberLinkId;
        }

        public MemberLink GetByUserId(int userId)
        {
            var sql = "SELECT * FROM MemberLink WHERE " + DbSyntax.QuoteIdentifier("UserId") + " = @UserId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@UserId", userId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public MemberLink GetByMemberId(int memberId)
        {
            var sql = "SELECT * FROM MemberLink WHERE " + DbSyntax.QuoteIdentifier("MemberId") + " = @MemberId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@MemberId", memberId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        #endregion
    }
}
