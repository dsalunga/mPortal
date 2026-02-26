using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class MCComposerProvider : GenericSqlDataProviderBase<MCComposer>, IMCComposerProvider
    {
        protected override string DeleteProcedure
        {
            get { return "MCComposer_Del"; }
        }

        protected override MCComposer From(IDataReader r, MCComposer source)
        {
            var item = source ?? new MCComposer();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.Entry = DataUtil.Get(r, "Entry");
            item.Locale = DataUtil.Get(r, WebColumns.Locale);
            item.Work = DataUtil.Get(r, WebColumns.Work);
            item.Description = DataUtil.Get(r, WebColumns.Description);
            item.PhotoFile = DataUtil.Get(r, "PhotoFile");
            item.NickName = DataUtil.Get(r, "NickName");
            item.Active = DataUtil.GetInt32(r, WebColumns.Active);
            item.CompetitionId = DataUtil.GetId(r, "CompetitionId");

            return item;
        }

        protected override string TableName { get { return "MCComposer"; } }


        protected override string IdColumn { get { return "Id"; } }



        protected override string SelectProcedure
        {
            get { return "MCComposer_Get"; }
        }

        public override int Update(MCComposer item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE MCComposer SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("Entry") + " = @Entry, " +
                    DbSyntax.QuoteIdentifier("Locale") + " = @Locale, " +
                    DbSyntax.QuoteIdentifier("Work") + " = @Work, " +
                    DbSyntax.QuoteIdentifier("Description") + " = @Description, " +
                    DbSyntax.QuoteIdentifier("PhotoFile") + " = @PhotoFile, " +
                    DbSyntax.QuoteIdentifier("NickName") + " = @NickName, " +
                    DbSyntax.QuoteIdentifier("Active") + " = @Active, " +
                    DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Entry", item.Entry),
                    DbHelper.CreateParameter("@Locale", item.Locale),
                    DbHelper.CreateParameter("@Work", item.Work),
                    DbHelper.CreateParameter("@Description", item.Description),
                    DbHelper.CreateParameter("@PhotoFile", item.PhotoFile),
                    DbHelper.CreateParameter("@NickName", item.NickName),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@CompetitionId", item.CompetitionId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO MCComposer (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Entry") + ", " +
                    DbSyntax.QuoteIdentifier("Locale") + ", " +
                    DbSyntax.QuoteIdentifier("Work") + ", " +
                    DbSyntax.QuoteIdentifier("Description") + ", " +
                    DbSyntax.QuoteIdentifier("PhotoFile") + ", " +
                    DbSyntax.QuoteIdentifier("NickName") + ", " +
                    DbSyntax.QuoteIdentifier("Active") + ", " +
                    DbSyntax.QuoteIdentifier("CompetitionId") +
                    ") VALUES (@Name, @Entry, @Locale, @Work, @Description, @PhotoFile, @NickName, @Active, @CompetitionId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Entry", item.Entry),
                    DbHelper.CreateParameter("@Locale", item.Locale),
                    DbHelper.CreateParameter("@Work", item.Work),
                    DbHelper.CreateParameter("@Description", item.Description),
                    DbHelper.CreateParameter("@PhotoFile", item.PhotoFile),
                    DbHelper.CreateParameter("@NickName", item.NickName),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@CompetitionId", item.CompetitionId)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, o);
            }

            return UpdatePostProcess(item, item.Id);
        }

        public IEnumerable<MCComposer> GetList(int competitionId)
        {
            List<MCComposer> items = new List<MCComposer>();

            var sql = "SELECT * FROM MCComposer WHERE " + DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@CompetitionId", competitionId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
