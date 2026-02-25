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
            using (var r = DbHelper.ExecuteReader("WebAddress_Get",
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

            using (var r = DbHelper.ExecuteReader("WebAddress_Get",
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

            var obj = DbHelper.ExecuteScalar("WebAddress_Set",
                DbHelper.CreateParameter("@Id", item.Id),
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
            );

            item.Id = DataUtil.GetId(obj);
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
