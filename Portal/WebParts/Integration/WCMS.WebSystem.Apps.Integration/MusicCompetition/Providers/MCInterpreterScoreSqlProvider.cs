using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            var obj = SqlHelper.ExecuteScalar("MCInterpreterScore_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@JudgeId", item.JudgeId),
                new SqlParameter("@VoiceQuality", item.VoiceQuality),
                new SqlParameter("@Interpretation", item.Interpretation),
                new SqlParameter("@StagePresence", item.StagePresence),
                new SqlParameter("@OverallImpact", item.OverallImpact),
                new SqlParameter("@DateModified", item.DateModified),
                new SqlParameter("@CandidateId", item.CandidateId),
                new SqlParameter("@CompetitionId", item.CompetitionId)
            );

            return UpdatePostProcess(item, obj);
        }

        public IEnumerable<MCInterpreterScore> GetList(int competitionId = -2, int candidateId = -2, int judgeId = -2)
        {
            var items = new List<MCInterpreterScore>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@CandidateId", candidateId),
                new SqlParameter("@JudgeId", judgeId),
                new SqlParameter("@CompetitionId", competitionId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
