using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebAddressProvider : GenericSqlDataProviderBase<WebAddress>, IWebAddressProvider
    {
        #region IWebAddressProvider Members

        public WebAddress Get(int objectId, int recordId, string tag)
        {
            var sql = "SELECT * FROM WebAddress WHERE " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " +
                    DbSyntax.QuoteIdentifier("Tag") + " = @Tag";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@Tag", tag)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public IEnumerable<WebAddress> GetList(int objectId, int recordId)
        {
            List<WebAddress> items = new List<WebAddress>();

            var sql = "SELECT * FROM WebAddress WHERE " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        protected override WebAddress From(IDataReader r, WebAddress source)
        {
            WebAddress item = source ?? new WebAddress();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.AddressLine1 = DataUtil.Get(r, "AddressLine1");
            item.AddressLine2 = DataUtil.Get(r, "AddressLine2");
            item.CityTown = DataUtil.Get(r, "CityTown");
            item.StateProvince = DataUtil.Get(r, "StateProvince");
            item.StateProvinceCode = DataUtil.GetId(r, "StateProvinceCode");
            item.CountryCode = DataUtil.GetId(r, "CountryCode");
            item.ZipCode = DataUtil.Get(r, "ZipCode");
            item.PhoneNumber = DataUtil.Get(r, "PhoneNumber");
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.Tag = DataUtil.Get(r, "Tag");
            item.LastUpdated = DataUtil.GetDateTime(r, "LastUpdated");

            return item;
        }

        #endregion

        #region IDataProvider<WebAddress> Members

        public override int Update(WebAddress item)
        {
            item.LastUpdated = DateTime.Now;

            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebAddress SET " +
                    DbSyntax.QuoteIdentifier("AddressLine1") + " = @AddressLine1" + ", " +
                    DbSyntax.QuoteIdentifier("AddressLine2") + " = @AddressLine2" + ", " +
                    DbSyntax.QuoteIdentifier("CityTown") + " = @CityTown" + ", " +
                    DbSyntax.QuoteIdentifier("StateProvince") + " = @StateProvince" + ", " +
                    DbSyntax.QuoteIdentifier("StateProvinceCode") + " = @StateProvinceCode" + ", " +
                    DbSyntax.QuoteIdentifier("CountryCode") + " = @CountryCode" + ", " +
                    DbSyntax.QuoteIdentifier("ZipCode") + " = @ZipCode" + ", " +
                    DbSyntax.QuoteIdentifier("PhoneNumber") + " = @PhoneNumber" + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId" + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId" + ", " +
                    DbSyntax.QuoteIdentifier("Tag") + " = @Tag" + ", " +
                    DbSyntax.QuoteIdentifier("LastUpdated") + " = @LastUpdated" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@AddressLine1", item.AddressLine1),
                    DbHelper.CreateParameter("@AddressLine2", item.AddressLine2),
                    DbHelper.CreateParameter("@CityTown", item.CityTown),
                    DbHelper.CreateParameter("@StateProvince", item.StateProvince),
                    DbHelper.CreateParameter("@StateProvinceCode", item.StateProvinceCode),
                    DbHelper.CreateParameter("@CountryCode", item.CountryCode),
                    DbHelper.CreateParameter("@ZipCode", item.ZipCode),
                    DbHelper.CreateParameter("@PhoneNumber", item.PhoneNumber),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Tag", item.Tag),
                    DbHelper.CreateParameter("@LastUpdated", item.LastUpdated),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebAddress (" +
                    DbSyntax.QuoteIdentifier("AddressLine1") + ", " +
                    DbSyntax.QuoteIdentifier("AddressLine2") + ", " +
                    DbSyntax.QuoteIdentifier("CityTown") + ", " +
                    DbSyntax.QuoteIdentifier("StateProvince") + ", " +
                    DbSyntax.QuoteIdentifier("StateProvinceCode") + ", " +
                    DbSyntax.QuoteIdentifier("CountryCode") + ", " +
                    DbSyntax.QuoteIdentifier("ZipCode") + ", " +
                    DbSyntax.QuoteIdentifier("PhoneNumber") + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("Tag") + ", " +
                    DbSyntax.QuoteIdentifier("LastUpdated") +
                    ") VALUES (@AddressLine1, @AddressLine2, @CityTown, @StateProvince, @StateProvinceCode, @CountryCode, @ZipCode, @PhoneNumber, @ObjectId, @RecordId, @Tag, @LastUpdated)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@AddressLine1", item.AddressLine1),
                    DbHelper.CreateParameter("@AddressLine2", item.AddressLine2),
                    DbHelper.CreateParameter("@CityTown", item.CityTown),
                    DbHelper.CreateParameter("@StateProvince", item.StateProvince),
                    DbHelper.CreateParameter("@StateProvinceCode", item.StateProvinceCode),
                    DbHelper.CreateParameter("@CountryCode", item.CountryCode),
                    DbHelper.CreateParameter("@ZipCode", item.ZipCode),
                    DbHelper.CreateParameter("@PhoneNumber", item.PhoneNumber),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Tag", item.Tag),
                    DbHelper.CreateParameter("@LastUpdated", item.LastUpdated)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #endregion

        protected override string TableName { get { return "WebAddress"; } }


        protected override string IdColumn { get { return "Id"; } }



        protected override string SelectProcedure
        {
            get { return "WebAddress_Get"; }
        }

        protected override string DeleteProcedure
        {
            get { return "WebAddress_Del"; }
        }
    }
}
