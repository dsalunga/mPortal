using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public class MChapterSqlProvider : GenericSqlDataProviderBase<MChapter>, IMChapterProvider
    {
        protected override string DeleteProcedure { get { return "MChapter_Del"; } }
        protected override string TableName { get { return "MChapter"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "MChapter_Get"; } }

        protected override MChapter From(IDataReader r, MChapter source)
        {
            var item = source ?? new MChapter();
            item.Id = DataUtil.GetId(r, WebColumns.Id, true);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.Address = DataUtil.Get(r, "Address");
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId, true);
            item.ChapterType = DataUtil.GetInt32(r, "ChapterType", true);
            item.Latitude = DataUtil.GetDouble(r, "Latitude", true);
            item.Longitude = DataUtil.GetDouble(r, "Longitude", true);
            item.AccessType = DataUtil.GetInt32(r, "AccessType", true);
            item.CountryCode = DataUtil.GetInt32(r, "CountryCode", true);
            item.Email = DataUtil.Get(r, WebColumns.Email);
            item.Mobile = DataUtil.Get(r, "Mobile");
            item.Telephone = DataUtil.Get(r, "Telephone");
            item.ServiceSchedule = DataUtil.Get(r, "ServiceSchedule");
            item.MoreInfo = DataUtil.Get(r, "MoreInfo");
            item.Extra = DataUtil.Get(r, "Extra");
            item.DistrictNo = DataUtil.GetInt32(r, "DistrictNo");
            item.DivisionId = DataUtil.GetId(r, "DivisionId");
            item.LocaleId = DataUtil.GetId(r, "LocaleId");
            item.LastUpdate = DataUtil.GetDateTime(r, "LastUpdate");

            return item;
        }

        public override int Update(MChapter item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("MChapter") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId, " +
                    DbSyntax.QuoteIdentifier("Address") + " = @Address, " +
                    DbSyntax.QuoteIdentifier("ChapterType") + " = @ChapterType, " +
                    DbSyntax.QuoteIdentifier("Latitude") + " = @Latitude, " +
                    DbSyntax.QuoteIdentifier("Longitude") + " = @Longitude, " +
                    DbSyntax.QuoteIdentifier("AccessType") + " = @AccessType, " +
                    DbSyntax.QuoteIdentifier("CountryCode") + " = @CountryCode, " +
                    DbSyntax.QuoteIdentifier("Email") + " = @Email, " +
                    DbSyntax.QuoteIdentifier("Mobile") + " = @Mobile, " +
                    DbSyntax.QuoteIdentifier("Telephone") + " = @Telephone, " +
                    DbSyntax.QuoteIdentifier("ServiceSchedule") + " = @ServiceSchedule, " +
                    DbSyntax.QuoteIdentifier("MoreInfo") + " = @MoreInfo, " +
                    DbSyntax.QuoteIdentifier("Extra") + " = @Extra, " +
                    DbSyntax.QuoteIdentifier("DistrictNo") + " = @DistrictNo, " +
                    DbSyntax.QuoteIdentifier("DivisionId") + " = @DivisionId, " +
                    DbSyntax.QuoteIdentifier("LocaleId") + " = @LocaleId, " +
                    DbSyntax.QuoteIdentifier("LastUpdate") + " = @LastUpdate" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@Address", item.Address),
                    DbHelper.CreateParameter("@ChapterType", item.ChapterType),
                    DbHelper.CreateParameter("@Latitude", item.Latitude),
                    DbHelper.CreateParameter("@Longitude", item.Longitude),
                    DbHelper.CreateParameter("@AccessType", item.AccessType),
                    DbHelper.CreateParameter("@CountryCode", item.CountryCode),
                    DbHelper.CreateParameter("@Email", item.Email),
                    DbHelper.CreateParameter("@Mobile", item.Mobile),
                    DbHelper.CreateParameter("@Telephone", item.Telephone),
                    DbHelper.CreateParameter("@ServiceSchedule", item.ServiceSchedule),
                    DbHelper.CreateParameter("@MoreInfo", item.MoreInfo),
                    DbHelper.CreateParameter("@Extra", item.Extra),
                    DbHelper.CreateParameter("@DistrictNo", item.DistrictNo),
                    DbHelper.CreateParameter("@DivisionId", item.DivisionId),
                    DbHelper.CreateParameter("@LocaleId", item.LocaleId),
                    DbHelper.CreateParameter("@LastUpdate", item.LastUpdate),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("MChapter") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("Address") + ", " +
                    DbSyntax.QuoteIdentifier("ChapterType") + ", " +
                    DbSyntax.QuoteIdentifier("Latitude") + ", " +
                    DbSyntax.QuoteIdentifier("Longitude") + ", " +
                    DbSyntax.QuoteIdentifier("AccessType") + ", " +
                    DbSyntax.QuoteIdentifier("CountryCode") + ", " +
                    DbSyntax.QuoteIdentifier("Email") + ", " +
                    DbSyntax.QuoteIdentifier("Mobile") + ", " +
                    DbSyntax.QuoteIdentifier("Telephone") + ", " +
                    DbSyntax.QuoteIdentifier("ServiceSchedule") + ", " +
                    DbSyntax.QuoteIdentifier("MoreInfo") + ", " +
                    DbSyntax.QuoteIdentifier("Extra") + ", " +
                    DbSyntax.QuoteIdentifier("DistrictNo") + ", " +
                    DbSyntax.QuoteIdentifier("DivisionId") + ", " +
                    DbSyntax.QuoteIdentifier("LocaleId") + ", " +
                    DbSyntax.QuoteIdentifier("LastUpdate") +
                    ") VALUES (@Name, @ParentId, @Address, @ChapterType, @Latitude, @Longitude, @AccessType, @CountryCode, @Email, @Mobile, @Telephone, @ServiceSchedule, @MoreInfo, @Extra, @DistrictNo, @DivisionId, @LocaleId, @LastUpdate)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@Address", item.Address),
                    DbHelper.CreateParameter("@ChapterType", item.ChapterType),
                    DbHelper.CreateParameter("@Latitude", item.Latitude),
                    DbHelper.CreateParameter("@Longitude", item.Longitude),
                    DbHelper.CreateParameter("@AccessType", item.AccessType),
                    DbHelper.CreateParameter("@CountryCode", item.CountryCode),
                    DbHelper.CreateParameter("@Email", item.Email),
                    DbHelper.CreateParameter("@Mobile", item.Mobile),
                    DbHelper.CreateParameter("@Telephone", item.Telephone),
                    DbHelper.CreateParameter("@ServiceSchedule", item.ServiceSchedule),
                    DbHelper.CreateParameter("@MoreInfo", item.MoreInfo),
                    DbHelper.CreateParameter("@Extra", item.Extra),
                    DbHelper.CreateParameter("@DistrictNo", item.DistrictNo),
                    DbHelper.CreateParameter("@DivisionId", item.DivisionId),
                    DbHelper.CreateParameter("@LocaleId", item.LocaleId),
                    DbHelper.CreateParameter("@LastUpdate", item.LastUpdate)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public IEnumerable<MChapter> GetList(int parentId)
        {
            var items = new List<MChapter>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("MChapter") + " WHERE " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }
            return items;
        }

        public IEnumerable<MChapter> GetListByLocaleId(int localeId)
        {
            var items = new List<MChapter>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("MChapter") + " WHERE " + DbSyntax.QuoteIdentifier("LocaleId") + " = @LocaleId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@LocaleId", localeId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }
            return items;
        }

        public MChapter GetByLocaleId(int localeId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("MChapter") + " WHERE " + DbSyntax.QuoteIdentifier("LocaleId") + " = @LocaleId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@LocaleId", localeId)))
            {
                if (r.Read())
                    return From(r);
            }
            return null;
        }

        public MChapter Get(string name, int parentId = -2)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("MChapter") + " WHERE " + DbSyntax.QuoteIdentifier("Name") + " = @Name";
            var parmList = new List<DbParameter> { DbHelper.CreateParameter("@Name", name) };
            if (parentId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId"; parmList.Add(DbHelper.CreateParameter("@ParentId", parentId)); }

            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parmList.ToArray()))
            {
                if (r.Read())
                    return From(r);
            }
            return null;
        }
    }
}
