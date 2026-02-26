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
    public class MCCandidateSqlProvider : GenericSqlDataProviderBase<MCCandidate>, IMCCandidateProvider
    {
        protected override string DeleteProcedure { get { return "MCCandidate_Del"; } }
        protected override string TableName { get { return "MCCandidate"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "MCCandidate_Get"; } }

        protected override MCCandidate From(IDataReader r, MCCandidate source)
        {
            MCCandidate item = source ?? new MCCandidate();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.Entry = DataUtil.Get(r, "Entry");
            item.Lyrics = DataUtil.Get(r, "Lyrics");
            item.SourceUrl2 = DataUtil.Get(r, "SourceUrl2");
            item.SourceUrl = DataUtil.Get(r, "SourceUrl");
            item.Lyricist = DataUtil.Get(r, "Lyricist");
            item.Interpreter = DataUtil.Get(r, "Interpreter");
            item.PhotoFile = DataUtil.Get(r, "PhotoFile");
            item.CompetitionId = DataUtil.GetId(r, "CompetitionId");
            item.Rank = DataUtil.GetInt32(r, WebColumns.Rank);
            item.WinnerRank = DataUtil.GetInt32(r, "WinnerRank");

            return item;
        }

        public override int Update(MCCandidate item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE MCCandidate SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("Entry") + " = @Entry, " +
                    DbSyntax.QuoteIdentifier("Lyrics") + " = @Lyrics, " +
                    DbSyntax.QuoteIdentifier("SourceUrl") + " = @SourceUrl, " +
                    DbSyntax.QuoteIdentifier("SourceUrl2") + " = @SourceUrl2, " +
                    DbSyntax.QuoteIdentifier("Lyricist") + " = @Lyricist, " +
                    DbSyntax.QuoteIdentifier("Interpreter") + " = @Interpreter, " +
                    DbSyntax.QuoteIdentifier("PhotoFile") + " = @PhotoFile, " +
                    DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId, " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank, " +
                    DbSyntax.QuoteIdentifier("WinnerRank") + " = @WinnerRank" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Entry", item.Entry),
                    DbHelper.CreateParameter("@Lyrics", item.Lyrics),
                    DbHelper.CreateParameter("@SourceUrl", item.SourceUrl),
                    DbHelper.CreateParameter("@SourceUrl2", item.SourceUrl2),
                    DbHelper.CreateParameter("@Lyricist", item.Lyricist),
                    DbHelper.CreateParameter("@Interpreter", item.Interpreter),
                    DbHelper.CreateParameter("@PhotoFile", item.PhotoFile),
                    DbHelper.CreateParameter("@CompetitionId", item.CompetitionId),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@WinnerRank", item.WinnerRank),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO MCCandidate (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Entry") + ", " +
                    DbSyntax.QuoteIdentifier("Lyrics") + ", " +
                    DbSyntax.QuoteIdentifier("SourceUrl") + ", " +
                    DbSyntax.QuoteIdentifier("SourceUrl2") + ", " +
                    DbSyntax.QuoteIdentifier("Lyricist") + ", " +
                    DbSyntax.QuoteIdentifier("Interpreter") + ", " +
                    DbSyntax.QuoteIdentifier("PhotoFile") + ", " +
                    DbSyntax.QuoteIdentifier("CompetitionId") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + ", " +
                    DbSyntax.QuoteIdentifier("WinnerRank") +
                    ") VALUES (@Name, @Entry, @Lyrics, @SourceUrl, @SourceUrl2, @Lyricist, @Interpreter, @PhotoFile, @CompetitionId, @Rank, @WinnerRank)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Entry", item.Entry),
                    DbHelper.CreateParameter("@Lyrics", item.Lyrics),
                    DbHelper.CreateParameter("@SourceUrl", item.SourceUrl),
                    DbHelper.CreateParameter("@SourceUrl2", item.SourceUrl2),
                    DbHelper.CreateParameter("@Lyricist", item.Lyricist),
                    DbHelper.CreateParameter("@Interpreter", item.Interpreter),
                    DbHelper.CreateParameter("@PhotoFile", item.PhotoFile),
                    DbHelper.CreateParameter("@CompetitionId", item.CompetitionId),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@WinnerRank", item.WinnerRank)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, o);
            }

            return UpdatePostProcess(item, item.Id);
        }

        public IEnumerable<MCCandidate> GetList(int competitionId)
        {
            List<MCCandidate> items = new List<MCCandidate>();

            var sql = "SELECT * FROM MCCandidate WHERE " + DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId";
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
