using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class MCCandidateSqlProvider : GenericSqlDataProviderBase<MCCandidate>, IMCCandidateProvider
    {
        protected override string DeleteProcedure { get { return "MCCandidate_Del"; } }
        protected override string SelectProcedure { get { return "MCCandidate_Get"; } }

        protected override MCCandidate From(IDataReader r, MCCandidate source)
        {
            MCCandidate item = source ?? new MCCandidate();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.Entry = DataHelper.Get(r, "Entry");
            item.Lyrics = DataHelper.Get(r, "Lyrics");
            item.SourceUrl2 = DataHelper.Get(r, "SourceUrl2");
            item.SourceUrl = DataHelper.Get(r, "SourceUrl");
            item.Lyricist = DataHelper.Get(r, "Lyricist");
            item.Interpreter = DataHelper.Get(r, "Interpreter");
            item.PhotoFile = DataHelper.Get(r, "PhotoFile");
            item.CompetitionId = DataUtil.GetId(r, "CompetitionId");
            item.Rank = DataUtil.GetInt32(r, WebColumns.Rank);
            item.WinnerRank = DataUtil.GetInt32(r, "WinnerRank");

            return item;
        }

        public override int Update(MCCandidate item)
        {
            var obj = SqlHelper.ExecuteScalar("MCCandidate_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Entry", item.Entry),
                new SqlParameter("@Lyrics", item.Lyrics),
                new SqlParameter("@SourceUrl", item.SourceUrl),
                new SqlParameter("@SourceUrl2", item.SourceUrl2),
                new SqlParameter("@Lyricist", item.Lyricist),
                new SqlParameter("@Interpreter", item.Interpreter),
                new SqlParameter("@PhotoFile", item.PhotoFile),
                new SqlParameter("@CompetitionId", item.CompetitionId),
                new SqlParameter("@Rank", item.Rank),
                new SqlParameter("@WinnerRank", item.WinnerRank)
            );

            return UpdatePostProcess(item, obj);
        }

        public IEnumerable<MCCandidate> GetList(int competitionId)
        {
            List<MCCandidate> items = new List<MCCandidate>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@CompetitionId", competitionId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
