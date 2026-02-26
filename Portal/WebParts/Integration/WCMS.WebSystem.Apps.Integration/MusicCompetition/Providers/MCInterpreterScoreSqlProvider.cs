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
    public class MCInterpreterScoreSqlProvider : GenericSqlDataProviderBase<MCInterpreterScore>, IMCInterpreterScoreProvider
    {
        protected override string DeleteProcedure { get { return "MCInterpreterScore_Del"; } }
        protected override string TableName { get { return "MCInterpreterScore"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "MCInterpreterScore_Get"; } }

        protected override MCInterpreterScore From(IDataReader r, MCInterpreterScore source)
        {
            var item = source ?? new MCInterpreterScore();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.JudgeId = DataUtil.GetInt32(r, "JudgeId");
            item.VoiceQuality = DataUtil.GetInt32(r, "VoiceQuality");
            item.Interpretation = DataUtil.GetInt32(r, "Interpretation");
            item.StagePresence = DataUtil.GetInt32(r, "StagePresence");
            item.OverallImpact = DataUtil.GetInt32(r, "OverallImpact");
            item.DateModified = DataUtil.GetDateTime(r, "DateModified");
            item.CandidateId = DataUtil.GetId(r, "CandidateId");
            item.CompetitionId = DataUtil.GetId(r, "CompetitionId");

            return item;
        }

        public override int Update(MCInterpreterScore item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE MCInterpreterScore SET " +
                    DbSyntax.QuoteIdentifier("JudgeId") + " = @JudgeId, " +
                    DbSyntax.QuoteIdentifier("VoiceQuality") + " = @VoiceQuality, " +
                    DbSyntax.QuoteIdentifier("Interpretation") + " = @Interpretation, " +
                    DbSyntax.QuoteIdentifier("StagePresence") + " = @StagePresence, " +
                    DbSyntax.QuoteIdentifier("OverallImpact") + " = @OverallImpact, " +
                    DbSyntax.QuoteIdentifier("DateModified") + " = @DateModified, " +
                    DbSyntax.QuoteIdentifier("CandidateId") + " = @CandidateId, " +
                    DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@JudgeId", item.JudgeId),
                    DbHelper.CreateParameter("@VoiceQuality", item.VoiceQuality),
                    DbHelper.CreateParameter("@Interpretation", item.Interpretation),
                    DbHelper.CreateParameter("@StagePresence", item.StagePresence),
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
                sql = "INSERT INTO MCInterpreterScore (" +
                    DbSyntax.QuoteIdentifier("JudgeId") + ", " +
                    DbSyntax.QuoteIdentifier("VoiceQuality") + ", " +
                    DbSyntax.QuoteIdentifier("Interpretation") + ", " +
                    DbSyntax.QuoteIdentifier("StagePresence") + ", " +
                    DbSyntax.QuoteIdentifier("OverallImpact") + ", " +
                    DbSyntax.QuoteIdentifier("DateModified") + ", " +
                    DbSyntax.QuoteIdentifier("CandidateId") + ", " +
                    DbSyntax.QuoteIdentifier("CompetitionId") +
                    ") VALUES (@JudgeId, @VoiceQuality, @Interpretation, @StagePresence, @OverallImpact, @DateModified, @CandidateId, @CompetitionId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@JudgeId", item.JudgeId),
                    DbHelper.CreateParameter("@VoiceQuality", item.VoiceQuality),
                    DbHelper.CreateParameter("@Interpretation", item.Interpretation),
                    DbHelper.CreateParameter("@StagePresence", item.StagePresence),
                    DbHelper.CreateParameter("@OverallImpact", item.OverallImpact),
                    DbHelper.CreateParameter("@DateModified", item.DateModified),
                    DbHelper.CreateParameter("@CandidateId", item.CandidateId),
                    DbHelper.CreateParameter("@CompetitionId", item.CompetitionId)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, o);
            }

            return UpdatePostProcess(item, item.Id);
        }

        public IEnumerable<MCInterpreterScore> GetList(int competitionId = -2, int candidateId = -2, int judgeId = -2)
        {
            var items = new List<MCInterpreterScore>();

            var sql = "SELECT * FROM MCInterpreterScore WHERE " +
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
