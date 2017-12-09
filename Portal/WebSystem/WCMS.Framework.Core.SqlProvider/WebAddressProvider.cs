using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebAddressProvider : GenericSqlDataProviderBase<WebAddress>, IWebAddressProvider
    {
        #region IWebAddressProvider Members

        public WebAddress Get(int objectId, int recordId, string tag)
        {
            using (var r = SqlHelper.ExecuteReader("WebAddress_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@Tag", tag)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public IEnumerable<WebAddress> GetList(int objectId, int recordId)
        {
            List<WebAddress> items = new List<WebAddress>();

            using (var r = SqlHelper.ExecuteReader("WebAddress_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        protected override WebAddress From(IDataReader r, WebAddress source)
        {
            WebAddress item = source ?? new WebAddress();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.AddressLine1 = DataHelper.Get(r, "AddressLine1");
            item.AddressLine2 = DataHelper.Get(r, "AddressLine2");
            item.CityTown = DataHelper.Get(r, "CityTown");
            item.StateProvince = DataHelper.Get(r, "StateProvince");
            item.StateProvinceCode = DataHelper.GetId(r, "StateProvinceCode");
            item.CountryCode = DataHelper.GetId(r, "CountryCode");
            item.ZipCode = DataHelper.Get(r, "ZipCode");
            item.PhoneNumber = DataHelper.Get(r, "PhoneNumber");
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.Tag = DataHelper.Get(r, "Tag");
            item.LastUpdated = DataHelper.GetDateTime(r, "LastUpdated");

            return item;
        }

        #endregion

        #region IDataProvider<WebAddress> Members

        public override int Update(WebAddress item)
        {
            item.LastUpdated = DateTime.Now;

            var obj = SqlHelper.ExecuteScalar("WebAddress_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@AddressLine1", item.AddressLine1),
                new SqlParameter("@AddressLine2", item.AddressLine2),
                new SqlParameter("@CityTown", item.CityTown),
                new SqlParameter("@StateProvince", item.StateProvince),
                new SqlParameter("@StateProvinceCode", item.StateProvinceCode),
                new SqlParameter("@CountryCode", item.CountryCode),
                new SqlParameter("@ZipCode", item.ZipCode),
                new SqlParameter("@PhoneNumber", item.PhoneNumber),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@Tag", item.Tag),
                new SqlParameter("@LastUpdated", item.LastUpdated)
            );

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        #endregion

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
