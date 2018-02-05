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
    public class MusicCompetitionProvider : GenericSqlDataProviderBase<MCCompetition>, IMusicCompetitionProvider
    {
        protected override string DeleteProcedure { get { return "MusicCompetition_Del"; } }
        protected override string SelectProcedure { get { return "MusicCompetition_Get"; } }

        protected override MCCompetition From(IDataReader r, MCCompetition source)
        {
            var item = source ?? new MCCompetition();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.Judges = DataHelper.Get(r, "Judges");
            item.ScoreLocked = DataUtil.GetInt32(r, "ScoreLocked");
            item.CompetitionDate = DataUtil.GetDateTime(r, "CompetitionDate");
            item.VoteLocked = DataUtil.GetInt32(r, "VoteLocked");
            item.VoteMasked = DataUtil.GetInt32(r, "VoteMasked");
            item.PeoplesChoiceId = DataUtil.GetId(r, "PeoplesChoiceId");
            item.BestInterpreterId = DataUtil.GetId(r, "BestInterpreterId");

            return item;
        }

        public override int Update(MCCompetition item)
        {
            var obj = SqlHelper.ExecuteScalar("MusicCompetition_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Judges", item.Judges),
                new SqlParameter("@ScoreLocked", item.ScoreLocked),
                new SqlParameter("@CompetitionDate", item.CompetitionDate),
                new SqlParameter("@VoteLocked", item.VoteLocked),
                new SqlParameter("@VoteMasked", item.VoteMasked),
                new SqlParameter("@BestInterpreterId", item.BestInterpreterId),
                new SqlParameter("@PeoplesChoiceId", item.PeoplesChoiceId)
            );

            return UpdatePostProcess(item, obj);
        }
    }
}
