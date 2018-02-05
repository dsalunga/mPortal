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
    public class MCSongScoreSqlProvider : GenericSqlDataProviderBase<MCSongScore>, IMCSongScoreProvider
    {
        protected override string DeleteProcedure { get { return "MCSongScore_Del"; } }
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
            var obj = SqlHelper.ExecuteScalar("MCSongScore_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@JudgeId", item.JudgeId),
                new SqlParameter("@Musicality", item.Musicality),
                new SqlParameter("@LyricsMessage", item.LyricsMessage),
                new SqlParameter("@OverallImpact", item.OverallImpact),
                new SqlParameter("@DateModified", item.DateModified),
                new SqlParameter("@CandidateId", item.CandidateId),
                new SqlParameter("@CompetitionId", item.CompetitionId)
            );

            return UpdatePostProcess(item, obj);
        }

        public IEnumerable<MCSongScore> GetList(int competitionId = -2, int candidateId = -2, int judgeId = -2)
        {
            List<MCSongScore> items = new List<MCSongScore>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@CandidateId", candidateId),
                new SqlParameter("@JudgeId", judgeId),
                new SqlParameter("@CompetitionId", competitionId)
            ))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
