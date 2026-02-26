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
    public class MCSongScoreSqlProvider : GenericSqlDataProviderBase<MCSongScore>, IMCSongScoreProvider
    {
        protected override string DeleteProcedure { get { return "MCSongScore_Del"; } }
        protected override string TableName { get { return "MCSongScore"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "MCSongScore_Get"; } }

        protected override MCSongScore From(IDataReader r, MCSongScore source)
        {
            var item = source ?? new MCSongScore();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.JudgeId = DataUtil.GetId(r, "JudgeId");
            item.Musicality = DataUtil.GetInt32(r, "Musicality");
            item.LyricsMessage = DataUtil.GetInt32(r, "LyricsMessage");
            item.OverallImpact = DataUtil.GetInt32(r, "OverallImpact");
            item.DateModified = DataUtil.GetDateTime(r, "DateModified");
            item.CandidateId = DataUtil.GetId(r, "CandidateId");
            item.CompetitionId = DataUtil.GetId(r, "CompetitionId");

            return item;
        }

        public override int Update(MCSongScore item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("MCSongScore") + " SET " +
                    DbSyntax.QuoteIdentifier("JudgeId") + " = @JudgeId, " +
                    DbSyntax.QuoteIdentifier("Musicality") + " = @Musicality, " +
                    DbSyntax.QuoteIdentifier("LyricsMessage") + " = @LyricsMessage, " +
                    DbSyntax.QuoteIdentifier("OverallImpact") + " = @OverallImpact, " +
                    DbSyntax.QuoteIdentifier("DateModified") + " = @DateModified, " +
                    DbSyntax.QuoteIdentifier("CandidateId") + " = @CandidateId, " +
                    DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@JudgeId", item.JudgeId),
                    DbHelper.CreateParameter("@Musicality", item.Musicality),
                    DbHelper.CreateParameter("@LyricsMessage", item.LyricsMessage),
                    DbHelper.CreateParameter("@OverallImpact", item.OverallImpact),
                    DbHelper.CreateParameter("@DateModified", item.DateModified),
                    DbHelper.CreateParameter("@CandidateId", item.CandidateId),
                    DbHelper.CreateParameter("@CompetitionId", item.CompetitionId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("MCSongScore") + " (" +
                    DbSyntax.QuoteIdentifier("JudgeId") + ", " +
                    DbSyntax.QuoteIdentifier("Musicality") + ", " +
                    DbSyntax.QuoteIdentifier("LyricsMessage") + ", " +
                    DbSyntax.QuoteIdentifier("OverallImpact") + ", " +
                    DbSyntax.QuoteIdentifier("DateModified") + ", " +
                    DbSyntax.QuoteIdentifier("CandidateId") + ", " +
                    DbSyntax.QuoteIdentifier("CompetitionId") +
                    ") VALUES (@JudgeId, @Musicality, @LyricsMessage, @OverallImpact, @DateModified, @CandidateId, @CompetitionId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@JudgeId", item.JudgeId),
                    DbHelper.CreateParameter("@Musicality", item.Musicality),
                    DbHelper.CreateParameter("@LyricsMessage", item.LyricsMessage),
                    DbHelper.CreateParameter("@OverallImpact", item.OverallImpact),
                    DbHelper.CreateParameter("@DateModified", item.DateModified),
                    DbHelper.CreateParameter("@CandidateId", item.CandidateId),
                    DbHelper.CreateParameter("@CompetitionId", item.CompetitionId)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, o);
            }

            return item.Id;
        }

        public IEnumerable<MCSongScore> GetList(int competitionId = -2, int candidateId = -2, int judgeId = -2)
        {
            List<MCSongScore> items = new List<MCSongScore>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("MCSongScore") + " WHERE " +
                "(@CandidateId = -2 OR " + DbSyntax.QuoteIdentifier("CandidateId") + " = @CandidateId) AND " +
                "(@JudgeId = -2 OR " + DbSyntax.QuoteIdentifier("JudgeId") + " = @JudgeId) AND " +
                "(@CompetitionId = -2 OR " + DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId)";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@CandidateId", candidateId),
                DbHelper.CreateParameter("@JudgeId", judgeId),
                DbHelper.CreateParameter("@CompetitionId", competitionId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
