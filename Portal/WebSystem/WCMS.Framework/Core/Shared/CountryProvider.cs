using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Shared
{
    public class CountryProvider : GenericSqlDataProviderBase<Country>, ICountryProvider
    {
        protected override string IdParameter { get { return "CountryCode"; } }
        protected override string SelectProcedure { get { return "Country_Get"; } }
        protected override string DeleteProcedure { get { return "Country_Del"; } }

        protected override Country From(IDataReader r, Country source)
        {
            var item = source ?? new Country();
            item.Id = DataUtil.GetId(r, "CountryCode");
            item.CountryName = DataUtil.Get(r, "CountryName");
            item.RegionCode = DataUtil.GetId(r, "RegionCode");
            item.Description = DataUtil.Get(r, WebColumns.Description);
            item.ISOCode = DataUtil.Get(r, "ISOCode");
            item.DialingCode = DataUtil.GetInt32(r, "DialingCode");
            item.MaxPhoneDigit = DataUtil.GetInt32(r, "MaxPhoneDigit");
            item.ISOCode3 = DataUtil.Get(r, "ISOCode3");
            item.ISONumeric = DataUtil.Get(r, "ISONumeric");
            item.ShortName = DataUtil.Get(r, "ShortName");

            return item;
        }

        public override int Update(Country item)
        {
            throw new NotImplementedException();
        }

        #region ICountryProvider Members

        public Country Get(string countryName)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@CountryName", countryName)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public Country GetByISOCode(string isoCode)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ISOCode", isoCode)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        #endregion
    }
}
