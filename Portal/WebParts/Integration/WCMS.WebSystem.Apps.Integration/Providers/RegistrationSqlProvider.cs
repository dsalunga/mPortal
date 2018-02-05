using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration
{
    public class RegistrationSqlProvider : GenericSqlDataProviderBase<GenericRegistration>, IRegistrationProvider
    {
        #region IDataProvider<Registration> Members

        public GenericRegistration Get(string name)
        {
            using (var r = SqlHelper.ExecuteReader("Registration_Get",
                new SqlParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        protected override GenericRegistration From(IDataReader r, GenericRegistration source)
        {
            GenericRegistration item = source ?? new GenericRegistration();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.EntryDate = DataUtil.GetDateTime(r, "EntryDate");
            item.Country = DataHelper.Get(r, "Country");
            item.Locale = DataHelper.Get(r, "Locale");
            item.ExternalId = DataHelper.Get(r, "ExternalId");
            item.Designation = DataHelper.Get(r, "Designation");
            item.ArrivalDate = DataUtil.GetDateTime(r, "ArrivalDate");
            item.Airline = DataHelper.Get(r, "Airline");
            item.FlightNo = DataHelper.Get(r, "FlightNo");
            item.DepartureDate = DataUtil.GetDateTime(r, "DepartureDate");
            item.Address = DataHelper.Get(r, "Address");
            item.Age = DataUtil.GetInt32(r, "Age");
            item.PlaceType = DataHelper.Get(r, "PlaceType");
            item.Gender = DataHelper.Get(r, "Gender");

            return item;
        }

        public override int Update(GenericRegistration item)
        {
            var obj = SqlHelper.ExecuteScalar("Registration_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@EntryDate", item.EntryDate),
                new SqlParameter("@Country", item.Country),
                new SqlParameter("@Locale", item.Locale),
                new SqlParameter("@ExternalId", item.ExternalId),
                new SqlParameter("@Designation", item.Designation),
                new SqlParameter("@ArrivalDate", item.ArrivalDate),
                new SqlParameter("@Airline", item.Airline),
                new SqlParameter("@FlightNo", item.FlightNo),
                new SqlParameter("@DepartureDate", item.DepartureDate),
                new SqlParameter("@Address", item.Address),
                new SqlParameter("@Age", item.Age),
                new SqlParameter("@PlaceType", item.PlaceType),
                new SqlParameter("@Gender", item.Gender)
            );

            return UpdatePostProcess(item, obj);
        }

        #endregion

        protected override string DeleteProcedure { get { return "Registration_Del"; } }
        protected override string SelectProcedure { get { return "Registration_Get"; } }
    }
}
