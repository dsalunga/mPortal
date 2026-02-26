using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

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
            var sql = "SELECT * FROM Registration WHERE " + DbSyntax.QuoteIdentifier("Name") + " = @Name";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Name", name)))
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
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.EntryDate = DataUtil.GetDateTime(r, "EntryDate");
            item.Country = DataUtil.Get(r, "Country");
            item.Locale = DataUtil.Get(r, "Locale");
            item.ExternalId = DataUtil.Get(r, "ExternalId");
            item.Designation = DataUtil.Get(r, "Designation");
            item.ArrivalDate = DataUtil.GetDateTime(r, "ArrivalDate");
            item.Airline = DataUtil.Get(r, "Airline");
            item.FlightNo = DataUtil.Get(r, "FlightNo");
            item.DepartureDate = DataUtil.GetDateTime(r, "DepartureDate");
            item.Address = DataUtil.Get(r, "Address");
            item.Age = DataUtil.GetInt32(r, "Age");
            item.PlaceType = DataUtil.Get(r, "PlaceType");
            item.Gender = DataUtil.Get(r, "Gender");

            return item;
        }

        public override int Update(GenericRegistration item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE Registration SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("EntryDate") + " = @EntryDate, " +
                    DbSyntax.QuoteIdentifier("Country") + " = @Country, " +
                    DbSyntax.QuoteIdentifier("Locale") + " = @Locale, " +
                    DbSyntax.QuoteIdentifier("ExternalId") + " = @ExternalId, " +
                    DbSyntax.QuoteIdentifier("Designation") + " = @Designation, " +
                    DbSyntax.QuoteIdentifier("ArrivalDate") + " = @ArrivalDate, " +
                    DbSyntax.QuoteIdentifier("Airline") + " = @Airline, " +
                    DbSyntax.QuoteIdentifier("FlightNo") + " = @FlightNo, " +
                    DbSyntax.QuoteIdentifier("DepartureDate") + " = @DepartureDate, " +
                    DbSyntax.QuoteIdentifier("Address") + " = @Address, " +
                    DbSyntax.QuoteIdentifier("Age") + " = @Age, " +
                    DbSyntax.QuoteIdentifier("PlaceType") + " = @PlaceType, " +
                    DbSyntax.QuoteIdentifier("Gender") + " = @Gender" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@EntryDate", item.EntryDate),
                    DbHelper.CreateParameter("@Country", item.Country),
                    DbHelper.CreateParameter("@Locale", item.Locale),
                    DbHelper.CreateParameter("@ExternalId", item.ExternalId),
                    DbHelper.CreateParameter("@Designation", item.Designation),
                    DbHelper.CreateParameter("@ArrivalDate", item.ArrivalDate),
                    DbHelper.CreateParameter("@Airline", item.Airline),
                    DbHelper.CreateParameter("@FlightNo", item.FlightNo),
                    DbHelper.CreateParameter("@DepartureDate", item.DepartureDate),
                    DbHelper.CreateParameter("@Address", item.Address),
                    DbHelper.CreateParameter("@Age", item.Age),
                    DbHelper.CreateParameter("@PlaceType", item.PlaceType),
                    DbHelper.CreateParameter("@Gender", item.Gender),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO Registration (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("EntryDate") + ", " +
                    DbSyntax.QuoteIdentifier("Country") + ", " +
                    DbSyntax.QuoteIdentifier("Locale") + ", " +
                    DbSyntax.QuoteIdentifier("ExternalId") + ", " +
                    DbSyntax.QuoteIdentifier("Designation") + ", " +
                    DbSyntax.QuoteIdentifier("ArrivalDate") + ", " +
                    DbSyntax.QuoteIdentifier("Airline") + ", " +
                    DbSyntax.QuoteIdentifier("FlightNo") + ", " +
                    DbSyntax.QuoteIdentifier("DepartureDate") + ", " +
                    DbSyntax.QuoteIdentifier("Address") + ", " +
                    DbSyntax.QuoteIdentifier("Age") + ", " +
                    DbSyntax.QuoteIdentifier("PlaceType") + ", " +
                    DbSyntax.QuoteIdentifier("Gender") +
                    ") VALUES (@Name, @EntryDate, @Country, @Locale, @ExternalId, @Designation, @ArrivalDate, @Airline, @FlightNo, @DepartureDate, @Address, @Age, @PlaceType, @Gender)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@EntryDate", item.EntryDate),
                    DbHelper.CreateParameter("@Country", item.Country),
                    DbHelper.CreateParameter("@Locale", item.Locale),
                    DbHelper.CreateParameter("@ExternalId", item.ExternalId),
                    DbHelper.CreateParameter("@Designation", item.Designation),
                    DbHelper.CreateParameter("@ArrivalDate", item.ArrivalDate),
                    DbHelper.CreateParameter("@Airline", item.Airline),
                    DbHelper.CreateParameter("@FlightNo", item.FlightNo),
                    DbHelper.CreateParameter("@DepartureDate", item.DepartureDate),
                    DbHelper.CreateParameter("@Address", item.Address),
                    DbHelper.CreateParameter("@Age", item.Age),
                    DbHelper.CreateParameter("@PlaceType", item.PlaceType),
                    DbHelper.CreateParameter("@Gender", item.Gender)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, o);
            }

            return UpdatePostProcess(item, item.Id);
        }

        #endregion

        protected override string DeleteProcedure { get { return "Registration_Del"; } }
        protected override string TableName { get { return "Registration"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "Registration_Get"; } }
    }
}
